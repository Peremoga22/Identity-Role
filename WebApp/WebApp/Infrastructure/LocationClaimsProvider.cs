using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp.Infrastructure
{
    public class LocationClaimsProvider : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if(principal !=null && !principal.HasClaim(c=>c.Type==ClaimTypes.PostalCode))
            {
                ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
                if(identity!=null &&identity.IsAuthenticated && identity.Name !=null)
                {
                    if(identity.Name.ToLower()=="vasyl")
                    {
                        identity.AddClaims(new Claim[]
                        {
                            CreateClaim(ClaimTypes.PostalCode,"80250")                            
                        });
                    }
                    else
                    {
                        identity.AddClaims(new Claim[]
                        {
                            CreateClaim(ClaimTypes.StateOrProvince,"80350")
                        });
                    }
                }
            }
            return Task.FromResult(principal);
        }
        private static Claim CreateClaim(string type, string value) => new Claim(type, value, ClaimValueTypes.String, "RemoteClaims");
    }
}
