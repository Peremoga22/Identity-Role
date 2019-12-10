using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger,UserManager<AppUser> userMgr)
        {
            _logger = logger;
            _userManager = userMgr;
        }       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize]
        public IActionResult Index() => View(GetData(nameof(Index)));
        [Authorize(Roles ="Users")]
        public IActionResult OtherAction() => View("Index", GetData(nameof(OtherAction)));

        private Dictionary<string, object> GetData(string actionName) => new Dictionary<string, object>
        {
            ["Action"] = actionName,
            ["User"] = HttpContext.User.Identity.Name,
            ["Authenticated"] = HttpContext.User.Identity.IsAuthenticated,
            ["Auth Type"] = HttpContext.User.Identity.AuthenticationType,
            ["In Users Role"] = HttpContext.User.IsInRole("Users"),
            ["City"] = CurrentUser.Result.Cities,
            ["Qualitification"] = CurrentUser.Result.QualitificationLevel
        };
        [Authorize]
        public async Task<IActionResult>UserProps()
        {
           
            return View(await CurrentUser);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UserProps([Required]Cities cities,[Required] QualitificationLevel qualitificationLevel)
        {
            if(ModelState.IsValid)
            {
                AppUser user = await CurrentUser;
                user.Cities = cities;
                user.QualitificationLevel = qualitificationLevel;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            return View(await CurrentUser);
        }

        private Task<AppUser>CurrentUser=> _userManager.FindByNameAsync(HttpContext.User.Identity.Name);



    }
}
