using AutoMapper;
using Contest.BL.Dto;
using Contest.BL.Dto.Contests;
using Contest.BL.Exceptions;
using Contest.BL.Extensions;
using Contest.BL.Interfaces;
using Contest.DA;
using Contest.DA.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml;

namespace Contest.BL.Services
{
    public class ContestService : IContestService
    {
        private readonly ContestContext _db;
        private readonly IMapper _mapper;

        public ContestService(ContestContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task AddContest(AddContestDto dto)
        {
            if (!dto.Validate())
                throw new ContestValidationException();

            var entity = _mapper.Map<ContestEntity>(dto);

            _db.Contests.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<ContestDto> GetContest(BaseContestDto dto)
        {
            var entity = await FindContest(dto.ContestId);
            var contest = _mapper.Map<ContestDto>(entity);

            return contest;
        }

        public async Task<PagedListDto<ContestDto>> GetAllContests(GetAllContestsDto dto)
        {
            if (dto.PageNumber <= 0 || dto.PageSize <= 0)
                throw new BadRequestException();

            var entities = await _db.Contests
                .Where(e => e.Status == ContestStatus.Published && e.EndDate <= DateTime.Now)
                .ToListAsync();

            if (entities == null)
                throw new NotFoundException();

            if (!string.IsNullOrWhiteSpace(dto.Search))
            {
                entities = entities.Where(e => e.SmallDescription.Contains(dto.Search)).ToList();
            }

            if (dto.IsPopular)
            {
                entities = entities.OrderByDescending(e => e.Views).ToList();
            }
            else if (dto.IsNew)
            {
                entities = entities.OrderByDescending(e => e.PublishDate).ToList();
            }
            else if (dto.IsAlmostClosed)
            {
                entities = entities.OrderBy(e => e.EndDate).ToList();
            }

            entities = entities
                .Take(dto.PageSize)
                .Skip(dto.PageSize * dto.PageNumber)
                .ToList();

            var contests = _mapper.Map<List<ContestEntity>, List<ContestDto>>(entities);

            return new PagedListDto<ContestDto>(dto.PageNumber, dto.PageSize, contests.Count, contests);
        }

        public async Task EditContest(ContestDto dto)
        {
            if (!dto.Validate())
                throw new ContestValidationException();

            var entity = await FindContest(dto.Id);
            if (entity == null)
                throw new NotFoundException();

            entity = _mapper.Map<ContestDto, ContestEntity>(dto);
            entity.Status = ContestStatus.Created;

            _db.Contests.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteContest(BaseContestDto dto)
        {
            var entity = await FindContest(dto.ContestId);

            _db.Contests.Remove(entity);
            await _db.SaveChangesAsync();
        }

        private async Task<ContestEntity> FindContest(int contestId)
        {
            if (contestId <= 0)
                throw new NotFoundException();

            var entity = await _db.Contests.FirstOrDefaultAsync(e => e.Id == contestId);
            if (entity == null)
                throw new NotFoundException();

            return entity;
        }
    }
}
