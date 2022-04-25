using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackOverflow25.Data;
using StackOverflow25.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflow25.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connString;
        public HomeController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var questionsRepo = new QuestionsRepository(_connString);
            return View(new HomeViewModel 
            { 
                Questions = questionsRepo.GetAllQuestions()
            });
        }

        [Authorize]
        public IActionResult AskAQuestion()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AskAQuestion(Question question, List<string> tags)
        {
            var repo = new QuestionsRepository(_connString);
            question.UserId = repo.GetUserIdByEmail(User.Identity.Name);
            repo.AddQuestion(question, tags);
            return Redirect("/home");
        }

        public IActionResult ViewQuestion(int id)
        {
            var repo = new QuestionsRepository(_connString);
            Question question = repo.GetQuestionById(id);
            if (question == null)
            {
                return Redirect("/home");
            }

            return View(new ViewQuestionViewModel
            {
                Question = question,
                CanLike = User.Identity.IsAuthenticated ? !repo.AlreadyLiked(id, repo.GetUserIdByEmail(User.Identity.Name)) : false
            });
        }

        [Authorize]
        [HttpPost]
        public void LikeQuestion(int questionId)
        {
            var repo = new QuestionsRepository(_connString);
            repo.LikeQuestion(questionId, repo.GetUserIdByEmail(User.Identity.Name));
        }

        public IActionResult GetLikes(int questionId)
        {
            var repo = new QuestionsRepository(_connString);
            return Json(repo.GetLikes(questionId));
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddAnswer(string text, int questionId)
        {
            var repo = new QuestionsRepository(_connString);
            repo.AddAnswer(new Answer
            {
                Text = text,
                QuestionId = questionId,
                UserId = repo.GetUserIdByEmail(User.Identity.Name)
            });
            return Redirect($"/home/viewquestion?id={questionId}");
        }
    }
}
