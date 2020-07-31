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
        public void AddContests()
        {
            var dto = new AddContestDto()
            {
                Title = "Новый розыгрыш пицц, суш и ноготочков от Артурчика",
                Link = "https://vk.com/venuz",
                City = "Москва",
                Organizator = "OOO ВЫХЛОП",
                IsAcrossCountry = false,
                EndDate = new DateTime(2020, 08, 31)
            };

            var count = 0;

            do
            {
                _contestService.AddContest(dto);
                count++;
            }
            while (count != 50);

            Assert.AreEqual(50, count);
        }
    }
}
