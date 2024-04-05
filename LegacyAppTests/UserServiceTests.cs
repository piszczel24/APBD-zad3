using LegacyApp;

namespace LegacyAppTests;

public class UserServiceTests
{
    private static readonly FakeClientRepository FakeClientRepository = new();
    private static readonly FakeCreditLimitService FakeCreditLimitService = new();

    [Fact]
    public void AddUser_Should_Return_False_When_Email_Without_At_And_Dot()
    {
        // Arrange
        const string firstName = "John";
        const string lastName = "Doe";
        var birthDate = new DateTime(1980, 1, 1);
        const int clientId = 1;
        const string email = "johndoegmailcom";
        var userService = new UserService(FakeClientRepository, FakeCreditLimitService);

        // Act
        var result = userService.AddUser(firstName, lastName, email, birthDate, clientId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_Is_Empty()
    {
        // Arrange
        const string firstName = "";
        const string lastName = "Doe";
        var birthDate = new DateTime(1980, 1, 1);
        const int clientId = 1;
        const string email = "johndoegmailcom";
        var userService = new UserService(FakeClientRepository, FakeCreditLimitService);

        // Act
        var result = userService.AddUser(firstName, lastName, email, birthDate, clientId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_LastName_Is_Empty()
    {
        // Arrange
        const string firstName = "John";
        const string lastName = "";
        var birthDate = new DateTime(1980, 1, 1);
        const int clientId = 1;
        const string email = "johndoegmailcom";
        var userService = new UserService(FakeClientRepository, FakeCreditLimitService);

        // Act
        var result = userService.AddUser(firstName, lastName, email, birthDate, clientId);

        // Assert
        Assert.False(result);
    }
}