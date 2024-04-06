using System;

namespace LegacyApp;

public class UserService(
    IClientRepository clientRepository,
    ICreditLimitService creditLimitService,
    IUserDataAccess userDataAccess)
{
    [Obsolete]
    public UserService() : this(new ClientRepository(), new UserCreditService(), new UserDataAccessAdapter())
    {
    }


    public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
    {
        if (!Validator.IsFirstNameAndLastNameValid(firstName, lastName)) return false;

        if (!Validator.IsEmailAddressValid(email)) return false;

        if (!Validator.IsBirthDateValid(dateOfBirth)) return false;

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

        userDataAccess.AddUser(user);
        return true;
    }
}