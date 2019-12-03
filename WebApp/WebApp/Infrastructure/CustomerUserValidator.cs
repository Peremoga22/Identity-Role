using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Infrastructure
{
    public class CustomerUserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            if (user.Email.ToLower().EndsWith("@outlook.com"))
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "Email DomainError",
                    Description = "Only example.com email addresses allowed",
                }));
            }
        }
    }
}
