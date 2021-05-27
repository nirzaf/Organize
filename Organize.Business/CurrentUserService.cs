using Organize.Shared.Contracts;
using Organize.Shared.Enitites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organize.Business
{
    public class CurrentUserService : ICurrentUserService
    {
        public User CurrentUser { get; set; }
    }
}
