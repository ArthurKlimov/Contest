using AutoMapper;
using Contest.BL.Dto;
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
            var dto = new ContestDto()
            {
                SmallDescription = "Small description",
                FullDescription = "Full description",
                Link = "https://www.youtube.com/",
                Prize = "Вафельница",
                StartDate = DateTime.Now,
                EndDate = new DateTime(2021, 5, 5),
                PublishDate = DateTime.Now,
                Status = DA.Entities.ContestStatus.Created,
                Views = 0,
                Cover = ""
            };

            _contestService.AddContest(dto);

            Assert.DoesNotThrow(() => _contestService.AddContest(dto));
        }
    }
}
