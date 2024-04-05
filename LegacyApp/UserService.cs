using System;

namespace LegacyApp;

public class UserService(IClientRepository clientRepository, ICreditLimitService creditLimitService)
{
    [Obsolete]
    public UserService() : this(new ClientRepository(), new UserCreditService())
    {
    }


    public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
    {
        if (!Validator.IsFirstNameAndLastNameValid(firstName, lastName)) return false;

        if (!Validator.IsEmailAddressValid(email)) return false;

        var now = DateTime.Now;
        var age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

        if (age < 21) return false;

        var client = clientRepository.GetById(clientId);

        var user = new User
        {
            Client = client,
            DateOfBirth = dateOfBirth,
            EmailAddress = email,
            FirstName = firstName,
            LastName = lastName
        };

        switch (client.Type)
        {
            case "VeryImportantClient":
                user.HasCreditLimit = false;
                break;
            case "ImportantClient":
            {
                var creditLimit = creditLimitService.GetCreditLimit(user.LastName, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
                break;
            }
            default:
            {
                user.HasCreditLimit = true;
                var creditLimit = creditLimitService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
                break;
            }
        }

        if (user.HasCreditLimit && user.CreditLimit < 500) return false;

        UserDataAccess.AddUser(user);
        return true;
    }
}