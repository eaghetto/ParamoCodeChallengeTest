using System;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Core.Contracts;
using Sat.Recruitment.Domain.Models;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public async Task ValidateEscenarioNewUserSuccessful()
        {
            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.AddUser(new User())).ReturnsAsync(false);
            var userController = new UsersController(mockService.Object);

            var result = await userController.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public async Task ValidateEscenarioNewUserDuplicated()
        {
            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.AddUser(new User())).ReturnsAsync(true);
            var userController = new UsersController(mockService.Object);

            var result = await userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");


            Assert.True(!result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
