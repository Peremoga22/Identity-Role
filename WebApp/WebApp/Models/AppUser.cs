using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public enum Cities
    {
        None,Lviv,Kyiv,London,Chicago
    }
    public enum QualitificationLevel
    {
        None,Basic,Advanced
    }

    public class AppUser : IdentityUser
    {
        public Cities Cities { get; set; }
        public QualitificationLevel QualitificationLevel { get; set; }
    }
}
