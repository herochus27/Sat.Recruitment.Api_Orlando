using Models;
using Sat.Recruitment.Api.Controllers;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserTest
    {
        [Fact]
        public void IsDuplicateUserUsingSameEmail()
        {
            var userController = new UserController();
            var user = new User
            {

                Name = "orlando",
                Email = "Juan@marmol.com",
                Address = "70 metros este",
                Phone = "85856106",
                UserType = "Normal",
                Money = 8000

            };
            var result = userController.Create(user);

            Assert.True(!result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }

        [Fact]
        public void IsDuplicateUserUsingSamePhone()
        {
            var userController = new UserController();
            var user = new User
            {

                Name = "orlando",
                Email = "Juan@marmol.com",
                Address = "70 metros este",
                Phone = "+5491154762312",
                UserType = "Normal",
                Money = 8000

            };
            var result = userController.Create(user);

            Assert.True(!result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }

        [Fact]
        public void IsGettingList()
        {
            var userController = new UserController();
            var result = userController.Get();

            Assert.True(result.IsSuccess);
        }
    }
}
