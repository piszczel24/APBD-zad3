namespace LegacyApp;

public static class Validator
{
    public static bool IsFirstNameAndLastNameValid(string firstName, string lastName)
    {
        return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName);
    }

    public static bool IsEmailAddressValid(string email)
    {
        return email.Contains('@') || email.Contains('.');
    }
}