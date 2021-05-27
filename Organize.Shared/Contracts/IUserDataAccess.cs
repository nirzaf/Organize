using Organize.Shared.Enitites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Organize.Shared.Contracts
{
    public interface IUserDataAccess
    {
        Task<User> AuthenticateAndGetUserAsync(User user);
        Task InsertUserAsync(User user);
        Task<bool> IsUserWithNameAvailableAsync(User user);
    }
}
