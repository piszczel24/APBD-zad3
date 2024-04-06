using LegacyApp;

namespace LegacyAppTests;

public class FakeUserDataAccess : IUserDataAccess
{
    private User? _addedUser;

    public void AddUser(User? user)
    {
        _addedUser = user;
    }
}