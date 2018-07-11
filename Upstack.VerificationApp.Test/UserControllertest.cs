using Moq;
using System;
using Upstack.VerificationApp.API.Contracts;
using Upstack.VerificationApp.API.Controllers;
using Upstack.VerificationApp.API.Model;
using Xunit;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Upstack.VerificationApp.Test
{
    public class UserControllertest
    {
        private UserController userController;
        private IOptions<SendGridModel> sendgrid;
        private Mock<IUserRepository> _IUserRepositoryMock = new Mock<IUserRepository>();

        public UserControllertest()
        {
            userController = new UserController(_IUserRepositoryMock.Object, sendgrid);

        }
        [Fact]       
        public async Task post_shouldCreateUser()
        {
            var user = new UserBindingModel
            {
                Username = "famofash",
                Email = "famofash@gmail.com",
                Password = "testpassword"
            };
            var result = await userController.Post(user); 
            Assert.NotNull(result);
            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(200, createdResult.StatusCode);
        }

    }
}
