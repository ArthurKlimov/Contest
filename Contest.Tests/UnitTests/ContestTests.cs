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
        private IImageService _imageService;

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

            _contestService = new ContestService(_db, _mapper, _imageService);
        }

        [Test]
        public void AddContests()
        {
            var dto = new ContestDto(new DateTime(2020, 7, 1), "Тестовый розыгрыш", "Описание розагрыша", "https://vk.com/friends", null);

            for (var i = 0; i < 30; i++)
            {
                 _contestService.AddContest(dto);
            }

            Assert.True(true);
        }
    }
}
