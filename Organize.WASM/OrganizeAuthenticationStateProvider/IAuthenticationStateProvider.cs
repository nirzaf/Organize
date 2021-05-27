using Organize.Shared.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.OrganizeAuthenticationStateProvider
{
    public interface IAuthenticationStateProvider
    {
        void SetAuthenticatedState(User user);
        void UnsetUser();
    }
}
