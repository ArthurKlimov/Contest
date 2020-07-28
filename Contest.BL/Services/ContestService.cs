using AutoMapper;
using Contest.BL.Dto;
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

        public async Task AddContest(ContestDto dto)
        {
            var entity = _mapper.Map<ContestDto, ContestEntity>(dto);
            entity.IsPublished = false;

            _db.Contests.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<PagedListDto<ContestDto>> GetContests(GetContestsDto dto)
        {
            var query = _db.Contests
                           .Where(x => x.EndDate >= DateTime.UtcNow && x.IsPublished == dto.IsPublished);

            if (!string.IsNullOrWhiteSpace(dto.Search))
                query = query.Where(e => e.Title.Contains(dto.Search));

            if (!string.IsNullOrWhiteSpace(dto.City))
                query = query.Where(x => x.City.Contains(dto.City) || x.AcrossCountry);

            if (dto.Sort == "Popular")
                query = query.OrderByDescending(e => e.Views);
            else if (dto.Sort == "Old")
                query = query.OrderBy(e => e.PublishDate);
            else if (dto.Sort == "AlmostClosed")
                query = query.OrderBy(e => e.EndDate);
            else
                query = query.OrderByDescending(e => e.PublishDate);

            var totalCount = query.Count();
            var entities = await query.Skip(dto.PageSize * (dto.PageNumber - 1))
                                      .Take(dto.PageSize)
                                      .ToListAsync();

            var contests = _mapper.Map<List<ContestEntity>, List<ContestDto>>(entities);
            foreach (var contest in contests)
            {
                contest.EndDateString = contest.EndDate.ParseToDateAndMonth();
                contest.PublishDateString = contest.PublishDate?.ParseToTimeDifference();
            }

            return new PagedListDto<ContestDto>(dto.PageNumber, dto.PageSize, totalCount, contests, dto.Sort, dto.Search);
        }

        public async Task DeleteContest(int id)
        {
            var entity = await _db.Contests.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new NotFoundException();

            _db.Contests.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task PublishContest(int id)
        {
            var entity = await _db.Contests.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new NotFoundException();

            entity.IsPublished = true;
            entity.PublishDate = DateTime.UtcNow;

            await _db.SaveChangesAsync();
        }

        public async Task HideContest(int id)
        {
            var entity = await _db.Contests.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new NotFoundException();

            entity.IsPublished = false;
            entity.PublishDate = null;

            await _db.SaveChangesAsync();
        }
    }
}
