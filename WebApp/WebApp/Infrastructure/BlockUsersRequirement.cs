using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Infrastructure
{
    public class BlockUsersRequirement :IAuthorizationRequirement
    {
        public BlockUsersRequirement(params string[] users)
        {
            BlockedUsers = users;
        }

        public string[] BlockedUsers { get; set; }
    }
}
