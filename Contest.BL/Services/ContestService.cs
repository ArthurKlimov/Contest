using AutoMapper;
using Contest.BL.Dto;
using Contest.BL.Exceptions;
using Contest.BL.Extensions;
using Contest.BL.Interfaces;
using Contest.DA;
using Contest.DA.Entities;
using System;
using System.Data;
using System.Reflection.Metadata.Ecma335;
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
            if (!dto.Validate())
                throw new ContestValidationException();

            var entity = _mapper.Map<ContestEntity>(dto);
            _db.Contests.Add(entity);
            await _db.SaveChangesAsync();
        }
    }
}
