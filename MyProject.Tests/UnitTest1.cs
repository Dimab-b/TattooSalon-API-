using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MockQueryable;
using MockQueryable.Moq;
using Moq;
using WebApiArchutecture.Application.DTOs.SignUpForDto;
using WebApiArchutecture.Application.DTOs.TattooDto;
using WebApiArchutecture.Application.DTOs.UserDto;
using WebApiArchutecture.Application.Features.Auth.Commands.Loggin;
using WebApiArchutecture.Application.Features.Auth.Commands.Logout;
using WebApiArchutecture.Application.Features.Auth.Commands.RefreshToken;
using WebApiArchutecture.Application.Features.Auth.Commands.Register;
using WebApiArchutecture.Application.Features.Tattoos.Commands.CreateSignUpFor;
using WebApiArchutecture.Application.Features.Tattoos.Commands.CreateTattoo;
using WebApiArchutecture.Application.Validators;
using WebApiArchutecture.Domain;
using WebApiArchutecture.Infrastructure.UnitOfWork;
using Xunit;
using Microsoft.Extensions.Logging;

namespace MyProject.Tests;

public class TestLogin
{
    [Fact]
    public async Task TestLoginWhenUserIsNotFound()
    {
        var uowMock = new Mock<IUnitOfWork>();
        var configMock = new Mock<IConfiguration>();
        var loggerMock = new Mock<ILogger<LoginUserCommandHandler>>();

        var loginCommand = new LoginUserCommand("Test", "Test");

        uowMock.Setup(u => u.Users.GetQueryable()).Returns(new List<User>().AsQueryable());


        var handler = new LoginUserCommandHandler(uowMock.Object, configMock.Object , loggerMock.Object);

        await Assert.ThrowsAnyAsync<System.Exception>(() => handler.Handle(loginCommand, CancellationToken.None));
    }
}

public class TestRegister
{
    [Fact]
    public async Task TestWhenUserIsBeAlready()
    {
        var uowMock = new Mock<IUnitOfWork>();
        
        var _mapperMock = new Mock<IMapper>();

        var loggerMock = new Mock<ILogger<RegisterUserCommandHandler>>();

        var registerCommand = new RegisterUserCommand
        (
             "Test",
             "Test",
             "Test",
             "Test",
             "Test"
        );

      
        uowMock.Setup(u => u.Users.GetQueryable()).Returns(new List<User> { new User { Username = "Test" } }.AsQueryable());


        var handler = new RegisterUserCommandHandler(uowMock.Object,  _mapperMock.Object , loggerMock.Object);

        await Assert.ThrowsAnyAsync<Exception>(() => handler.Handle(registerCommand, CancellationToken.None));
    }

    [Fact]
    public async Task TestPassNotConfirmed_NotCreating()
    {
        var uowMock = new Mock<IUnitOfWork>();
        var configMock = new Mock<IConfiguration>();
        var loggerMock = new Mock<ILogger<RegisterUserCommandHandler>>();
        var _mapperMock = new Mock<IMapper>();

        var registerdto = new RegisterUserCommand
        (
             "Test",
             "Test123",
             "Test",
             "Test",
             "Test"
        );
      
        uowMock.Setup(u => u.Users.GetQueryable()).Returns(new List<User>().AsQueryable());


        var handler = new RegisterUserCommandHandler(uowMock.Object, _mapperMock.Object , loggerMock.Object);
        await Assert.ThrowsAnyAsync<Exception>(() => handler.Handle(registerdto, CancellationToken.None));

        uowMock.Verify(x => x.Users.Create(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task UserIsCreated()
    {
        var uowMock = new Mock<IUnitOfWork>();
        var loggerMock = new Mock<ILogger<RegisterUserCommandHandler>>();
        var _mapperMock = new Mock<IMapper>();

        var registerCommand = new RegisterUserCommand("Test", "Test","test@gmail.com" ,"Test", "Test");
        var users = new List<User>();
        var mockUsers = users.BuildMock();
       
        uowMock.Setup(u => u.Users.GetQueryable()).Returns(mockUsers);

        _mapperMock.Setup(m => m.Map<UserReadDto>(It.IsAny<User>())).Returns(new UserReadDto());

        var handler = new RegisterUserCommandHandler(uowMock.Object,  _mapperMock.Object, loggerMock.Object);
        var result = await handler.Handle(registerCommand, CancellationToken.None);

        uowMock.Verify(x => x.Users.Create(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        uowMock.Verify(x => x.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

   
}

public class TestLogout
{
    [Fact]
    public async Task NoRefreshTokenLogout()
    {
        var uowMock = new Mock<IUnitOfWork>();
        var configMock = new Mock<IConfiguration>();
        var validatorMock = new Mock<IValidator<RegisterUserCommand>>();
        var loggerMock = new Mock<ILogger<LogoutCommandHandler>>();

        var refresh = "token";
        var users = new List<User>();
        var mockUsers = users.BuildMock();

        uowMock.Setup(u => u.Users.GetQueryable()).Returns(mockUsers);

        var command = new LogoutCommand(refresh);
        var handler = new LogoutCommandHandler(uowMock.Object , loggerMock.Object);

        await Assert.ThrowsAnyAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task GreatLogout()
    {
        var uowMock = new Mock<IUnitOfWork>();
        var configMock = new Mock<IConfiguration>();
        var validatorMock = new Mock<IValidator<RegisterUserCommand>>();
        var loggerMock = new Mock<ILogger<LogoutCommandHandler>>();

        var refresh = "token";
        var users = new List<User> { new User { RefreshToken = "token" } };
        var mockUsers = users.BuildMock();

        uowMock.Setup(U => U.Users.GetQueryable()).Returns(mockUsers);

        var command = new LogoutCommand(refresh);
        var handler = new LogoutCommandHandler(uowMock.Object , loggerMock.Object);

        await handler.Handle(command, CancellationToken.None);

        uowMock.Verify(x => x.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}

public class TestRefreshToken
{
    [Fact]
    public async Task ThrowException_WhenIsRevokedEqualTrue_RefreshToken()
    {
        var uowMock = new Mock<IUnitOfWork>();
        var configMock = new Mock<IConfiguration>();
        var validationMock = new Mock<IValidator<RegisterUserCommand>>();
        var loggerMock = new Mock<ILogger<RefreshTokenCommandHandler>>();

        string token = "3421321";
        var users = new List<User> { new User { IsRevoked = true, PasswordHash = "dsaafsd##;1", Role = "User", Telegram_Tag = "@fsd", RefreshToken = "3421321", TokenCreated = new DateTime(), TokenExpires = new DateTime(), Username = "test" } };
        var Mockusers = users.BuildMock();

        var command = new RefreshTokenCommand(token);
        var handler = new RefreshTokenCommandHandler(uowMock.Object, configMock.Object , loggerMock.Object);

        await Assert.ThrowsAnyAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
    }
}

public class ValidatorsTest
{
    private readonly UserRegisterDtoValidator _validator = new UserRegisterDtoValidator();
    private readonly SignUpForTattoCreateDtoValidator _validator1 = new SignUpForTattoCreateDtoValidator();
    private readonly TattooCreateDtoValidator _validator2 = new TattooCreateDtoValidator();

    [Fact]
    public void ShouldHaveException_WhenPasswordIsWrong_Register()
    {
        var model = new RegisterUserCommand("123", "@gfdsdas", "123", "User", "dimashyesos");

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void ShouldHaveError_TelegramTag_RegisterValidation()
    {
        var model = new RegisterUserCommand("UsER__3@GFJIOWAdfd", "UsER__3@GFJIOWAdfd", "tag", "username", "User");

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Telegram_Tag);
    }

    [Fact]
    public void ShouldHaveError_WhenUsernameIsEmpty()
    {
        var model = new RegisterUserCommand("", "UsER__3@GFJIOWAdfd", "UsER__3@GFJIOWAdfd", "@tag", "User");

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Username);
    }

    [Fact]
    public void ShouldHaveError_WhenNumberOfClientWasUncorrect()
    {
        var model = new CreateSignUpForCommand ( "0937366822", DateTime.UtcNow.AddYears(+2),  2, 2 );

        var result = _validator1.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.NumberOfClient);
    }

    [Fact]
    public void ShouldHaveError_WhenSessionsHaveMinus()
    {
        var model = new CreateSignUpForCommand ( "+380937356822", DateTime.UtcNow.AddYears(+2), -2,  2 );

        var result = _validator1.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Sessions);
    }

    [Fact]
    public void ShouldHaveError_WhenPriceIsTooLow()
    {
        var model = new CreateTattooCommand ("Japan",  Color.multicolored,  "20-10",  10 );

        var result = _validator2.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Price);
    }
}

public class ValidatorsCheckWithInlineDate
{
    private readonly UserRegisterDtoValidator _validator = new UserRegisterDtoValidator();

    [Theory]
    [InlineData("123", "@tag","dimasik@gmail.com", "username", "Password")]
    [InlineData("12DASSD@@3", "tag", "dimasik@gmail.com", "username", "Telegram_Tag")]
    [InlineData("12DASSD@@3", "@tag", "dimasik@gmail.com", "", "Username")]
    public void ShouldHaveError_RegisterValidator(string password, string telegramTag, string email,  string username, string expectedErrorField)
    {
        var model = new RegisterUserCommand(username, password, email,  password, telegramTag);

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(expectedErrorField);
    }
}