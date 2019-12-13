using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private ProtectedDocument[] docs = new ProtectedDocument[]
        {
            new ProtectedDocument{Title ="Q3 Budget",Author="Vasyl", Editor="Nastya"},
            new ProtectedDocument{Title="Project Plan",Author = "Bob",Editor="Vasyl"}
        };

        private IAuthorizationService authService;

        public DocumentController(IAuthorizationService auth)
        {
            authService = auth;
        }

        public ViewResult Index()
        {
            return View(docs);
        }

        public async Task<IActionResult>Edit(string title)
        {
            ProtectedDocument document = docs.FirstOrDefault(d => d.Title == title);
            AuthorizationResult authorizationResult = await authService.AuthorizeAsync(User, document, "AuthorsAndEditors");
            if(authorizationResult.Succeeded)
            {
                return View("Index", document);
            }
            else
            {
                return new ChallengeResult();
            }
        }
     
    }
}