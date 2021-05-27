using Organize.Shared.Contracts;
using Organize.Shared.Enitites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Organize.WebAPIAccess
{
    public class WebAPIUserDataAccess : IUserDataAccess
    {
        private WebAPIAccess _webAPIAccess;

        public WebAPIUserDataAccess(IPersistenceService persistenceService)
        {
            _webAPIAccess = (WebAPIAccess)persistenceService;
        }

        public async Task<User> AuthenticateAndGetUserAsync(User user)
        {
            return await _webAPIAccess.AuthenticateAndGetUserAsync(user);
        }

        public async Task InsertUserAsync(User user)
        {
            await _webAPIAccess.InsertAsync(user);
        }

        public Task<bool> IsUserWithNameAvailableAsync(User user)
        {
            return Task.FromResult(false);
        }
    }
}
