using Organize.Shared.Enitites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organize.Shared.Contracts
{
    public interface ICurrentUserService
    {
        User CurrentUser { get; set; }
    }
}
