using AdsBoard.Domain.Abstract;
using AdsBoard.Domain.Entities;
using AdsBoard.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdsBoard.WebUI.Controllers
{
    public class CommentController : Controller
    {
        private ICommentRepository repository;
        public CommentController(ICommentRepository repo)
        {
            repository = repo;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostComment(string text, int adId)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment();
                comment.Name = User.Identity.Name;
                comment.Text = text;
                comment.adId = adId;
                
                repository.SaveComment(comment);
                return RedirectToAction("Details","Ad", new { adId =adId});
            }
            else
            {
                // Что-то не так со значениями данных
                return RedirectToAction("Details", "Ad", new { adId = adId });
            }
        }


    }
}