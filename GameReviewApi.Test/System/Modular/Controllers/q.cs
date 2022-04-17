﻿using FluentAssertions;
using GameReviewApi.Controllers;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Service.Interfaces;
using GameReviewApi.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Controllers
{
    public class q
    {

        private readonly Mock<IUserService> userService = new Mock<IUserService>();

        //[Fact]
        //public void Test_GET_AReservations_BadRequest()
        //{
        //    // Arrange
        //    int id = 0;
        //    var mockRepo = new Mock<IRepository>();
        //    mockRepo.Setup(repo => repo[It.IsAny<int>()]).Returns<int>((a) => Single(a));
        //    var controller = new ReservationController(mockRepo.Object);

        //    // Act
        //    var result = controller.Get(id);

        //    // Assert
        //    var actionResult = Assert.IsType<ActionResult<Reservation>>(result);
        //    Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        //}

        /// <summary>
        /// Проверяем, что из внутреннего каталога возвращается не null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetUsers_NotNullResult()
        {
            /// Arrange
            var userService = new Mock<IUserService>();
            userService.Setup(_ => _.GetAsyncService()).ReturnsAsync(UserMockData.Get());
            UserController _userController = new UserController(userService.Object);
            /// Act
            var result = await _userController.GetUsers();
            /// Assert
            Assert.NotNull(result);
        }

    }
}
