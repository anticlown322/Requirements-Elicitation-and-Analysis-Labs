using FakeItEasy;
using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Kafka.Interfaces;
using UserMicroservice.Business.Models;
using UserMicroservice.Business.Services;
using UserMicroservice.WebAPI.Controllers;

namespace ControllerTests.Controllers;

public class UserControllerTests
{
    private readonly IUserService _userService = A.Fake<IUserService>();
    private readonly IKafkaProducer _kafkaProducer = A.Fake<IKafkaProducer>();
    private readonly ILogger<UserController> _logger = A.Fake<ILogger<UserController>>();

    [Fact]
    public async Task UserController_GetAll_ReturnOk()
    {
        #region Arrange
        
        UserController controller = new(_userService, _kafkaProducer, _logger);

        #endregion

        #region Act

        var result = await controller.GetAll();

        #endregion

        #region Assert

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        #endregion
    }
    
    [Fact]
    public async Task UserController_Create_ReturnCreated()
    {
        #region Arrange
        
        UserController controller = new(_userService, _kafkaProducer, _logger);
        UserModel userModel = new()
        {
            Balance = 100,
            Login = "login",
            IsVerified = false,
            Email = "kluwne@gamil.com",
            FirstName = "Kill",
            LastName = "Niggers",
            Phone = "+375336216209"
        }; 
        
        #endregion

        #region Act

        var result = await controller.Create(userModel);

        #endregion

        #region Assert

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(ActionResult<UserModel>));

        #endregion
    }
    
    [Fact]
    public async Task UserController_Create_ReturnOk()
    {
        #region Arrange
        
        UserController controller = new(_userService, _kafkaProducer, _logger);
        UserModel startUserModel = new()
        {
            Balance = 100,
            Login = "login",
            IsVerified = false,
            Email = "kluwne@gamil.com",
            FirstName = "Kill",
            LastName = "Niggers",
            Phone = "+375336216209"
        }; 
        UserModel updateUserModel = new()
        {
            Balance = 200,
            Login = "damnUfSheesh",
            IsVerified = false,
            Email = "notclownanymore@gamil.com",
            FirstName = "Kill",
            LastName = "Niggers",
            Phone = "+375336216209"
        }; 
        
        #endregion

        #region Act

        await controller.Create(startUserModel);
        var result = await controller.Update(0, updateUserModel);
        
        #endregion

        #region Assert

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
        
        #endregion
    }
    
    [Fact]
    public async Task UserController_GetById_ReturnOk()
    {
        #region Arrange
        
        UserController controller = new(_userService, _kafkaProducer, _logger);
        UserModel userModel = new()
        {
            Balance = 100,
            Login = "login",
            IsVerified = false,
            Email = "kluwne@gamil.com",
            FirstName = "Kill",
            LastName = "Niggers",
            Phone = "+375336216209"
        }; 
        
        #endregion

        #region Act

        await controller.Create(userModel);
        var result = await controller.GetById(1);
        
        #endregion

        #region Assert

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(ActionResult<UserModel>));

        #endregion
    }
    
    [Fact]
    public async Task UserController_GetById_ReturnNotFound()
    {
        #region Arrange
        
        UserController controller = new(_userService, _kafkaProducer, _logger);
        
        #endregion

        #region Act

        var result = await controller.GetById(666);
        
        #endregion

        #region Assert

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(ActionResult<UserModel>));
        result.Result.Should().BeOfType(typeof(NotFoundResult));

        #endregion
    }
    
    [Fact]
    public async Task UserController_RemoveById_ReturnOk()
    {
        #region Arrange
        
        UserController controller = new(_userService, _kafkaProducer, _logger);
        UserModel userModel = new()
        {
            Balance = 100,
            Login = "login",
            IsVerified = false,
            Email = "kluwne@gamil.com",
            FirstName = "Kill",
            LastName = "Niggers",
            Phone = "+375336216209"
        }; 
        
        #endregion

        #region Act

        await controller.Create(userModel);
        var result = await controller.RemoveById(0);
        
        #endregion

        #region Assert

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        #endregion
    }
}