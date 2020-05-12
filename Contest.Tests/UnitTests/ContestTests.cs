using AutoMapper;
using Contest.BL.Dto;
using Contest.BL.Dto.Contests;
using Contest.BL.Interfaces;
using Contest.BL.Mappings;
using Contest.BL.Services;
using Contest.DA;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace Contest.Tests.UnitTests
{
    [TestFixture]
    public class ContestTests
    {
        private IContestService _contestService;
        private ContestContext _db;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var mappingConfiguration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ContestProfile());
            });
            _mapper = mappingConfiguration.CreateMapper();

            var connectionString = "Data Source=.\\SQLEXPRESS; Initial Catalog=ContestDb; Integrated Security=True;MultipleActiveResultSets=True";
            var options = new DbContextOptionsBuilder<ContestContext>().UseSqlServer(connectionString).Options;
            _db = new ContestContext(options);

            _contestService = new ContestService(_db, _mapper);
        }

        [Test]
        public void AddContest()
        {
            var dto = new AddContestDto()
            {
                SmallDescription = "",
                FullDescription = "Full description",
                Link = "https://www.youtube.com/",
                StartDate = DateTime.UtcNow,
                EndDate = new DateTime(2020, 5, 5),
            };

            Assert.DoesNotThrow(() => _contestService.AddContest(dto));
        }
    }
}
