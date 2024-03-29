﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ClaimsController : Controller
    {
        [Authorize]
        public ViewResult Index()
        {
            return View(User?.Claims);
        }
    }
}