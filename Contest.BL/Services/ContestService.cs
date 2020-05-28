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
using System.Threading.Tasks;
using Contest.BL.Enums;

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

        public async Task<int> AddContest(AddContestDto dto)
        {
            if(!dto.IsValid())
                throw new ContestValidationException();

            var entity = _mapper.Map<AddContestDto, ContestEntity>(dto);
            entity.PublishDate = DateTime.UtcNow;
            
            _db.Contests.Add(entity);
            await _db.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<PagedListDto<ContestDto>> GetContests(GetContestsDto dto)
        {
            if(dto.PageNumber <= 0 || dto.PageSize <= 0)
                throw new BadRequestException();

            var query = _db.Contests
                           .Where(x => x.EndDate >= DateTime.UtcNow);

            if(!string.IsNullOrWhiteSpace(dto.Search))
                query = query.Where(e => e.SmallDescription.Contains(dto.Search));

            if(query == null)
                throw new NotFoundException();

            if(dto.Sort == ContestsSortType.Popular)
                query = query.OrderByDescending(e => e.Views);

            if(dto.Sort == ContestsSortType.New)
                query = query.OrderByDescending(e => e.PublishDate);

            if(dto.Sort == ContestsSortType.AlmostClosed)
                query = query.OrderBy(e => e.EndDate);

            var totalCount = query.Count();

            var entities = await query.Skip(dto.PageSize * (dto.PageNumber - 1))
                                      .Take(dto.PageSize)
                                      .ToListAsync();

            var contests = _mapper.Map<List<ContestEntity>, List<ContestDto>>(entities);

            foreach (var contest in contests)
            {
                contest.EndDateString = contest.EndDate.ParseToDateAndMonth();
                contest.PublishDateString = contest.PublishDate.ParseToTimeDifference();
            }

            return new PagedListDto<ContestDto>(dto.PageNumber, dto.PageSize, totalCount, contests);
        }

    }
}
