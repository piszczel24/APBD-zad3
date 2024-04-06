using System;

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

    public static bool IsBirthDateValid(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        var age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

        return age >= 21;
    }
}