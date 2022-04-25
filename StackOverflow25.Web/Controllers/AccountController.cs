using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StackOverflow25.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StackOverflow25.Web.Controllers
{
    public class AccountController : Controller
    {
        private string _connString;

        public AccountController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(string name, string email, string password)
        {
            var questionsRepo = new QuestionsRepository(_connString);
            questionsRepo.AddUser(name, email, password);
            return Redirect("/account/login");
        }

        public IActionResult LogIn()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            return View();            
        }

        [HttpPost]
        public IActionResult LogIn(string email, string password)
        {
            var repo = new QuestionsRepository(_connString);
            User user = repo.LogIn(email, password);
            if (user == null)
            {
                TempData["Message"] = "Invalid Login";
                return Redirect("/account/login");
            }

            var claims = new List<Claim>
            {
                new Claim("user", email)
            };

            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();

            return Redirect("/");
        }

        [Authorize]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/");
        }
    }
}
