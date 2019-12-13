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

        public ViewResult Index()
        {
            return View(docs);
        }

        public ViewResult Edit(string title)
        {
            return View("Index", docs.FirstOrDefault(d => d.Title == title));
        }
    }
}