using System;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Controllers;
using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repository;

namespace RunGroopWebAppTests.Controller
{
	public class ClubControllerTests
	{
        private readonly IClubRepository _iClubRepository;
        private readonly IPhotoService _iPhotoRepository;
        private readonly ClubController _clubController;

        public ClubControllerTests()
		{
			//Dependencies
			_iClubRepository = A.Fake<IClubRepository>();
			_iPhotoRepository = A.Fake<IPhotoService>();

			//SUT
            _clubController = new ClubController(_iClubRepository, _iPhotoRepository);

        }

        [Theory]
		[InlineData(-1, 1, 6)]
		[InlineData(1, 2, 6)]

		public void ClubController_Index_ReturnsSuccess(int category, int page, int pageSize)
		{
			//Arrange
			var clubs = A.Fake<IEnumerable<Club>>();
			A.CallTo(() => _iClubRepository.GetSliceAsync((page - 1) * pageSize, pageSize)).Returns(clubs);
			A.CallTo(() => _iClubRepository.GetClubsByCategoryAndSliceAsync((ClubCategory)category, (page - 1) * pageSize, pageSize)).Returns(clubs);

			var categoryCount = 5;
			A.CallTo(() => _iClubRepository.GetCountAsync()).Returns(categoryCount);
			A.CallTo(() => _iClubRepository.GetCountByCategoryAsync((ClubCategory)category)).Returns(categoryCount);

			//Act
			var result = _clubController.Index(category, page, pageSize);

			//Assert
			result.Should().BeOfType<Task<IActionResult>>();

		}
	}
}

