using LegacyApp;

namespace LegacyAppTests;

public class UserServiceTests
{
    [Fact]
    public void AddUser_Should_Return_False_When_Email_Without_At_And_Dot()
    {
        // Arrange
        const string firstName = "John";
        const string lastName = "Doe";
        var birthDate = new DateTime(1980, 1, 1);
        const int clientId = 1;
        const string email = "johndoegmailcom";
        var userService = new UserService();

        // Act
        var result = userService.AddUser(firstName, lastName, email, birthDate, clientId);

        // Assert
        Assert.False(result);
    }
}