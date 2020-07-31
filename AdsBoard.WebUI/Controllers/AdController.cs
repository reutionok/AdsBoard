using AdsBoard.Domain.Abstract;
using AdsBoard.Domain.Entities;
using AdsBoard.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Authorization.Mvc;

namespace AdsBoard.WebUI.Controllers
{
    public class AdController : Controller
    {
        private IAdRepository repository;
        private ICommentRepository repoComm;
        public int pageSize = 3;
        public AdController(IAdRepository repo, ICommentRepository repo2)
        {
            repository = repo;
            repoComm = repo2;
        }
        [AllowAnonymous]
        public ViewResult List(int page = 1)
        {
            AdListViewModel model = new AdListViewModel
            {
                Advertisements = repository.Advertisements
                    .OrderBy(ad => ad.AdId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Advertisements.Count()
                }
            };
            return View(model);
        }
        [Authorize]
        public ViewResult Details(int adId)
        {
            PostViewModel post = new PostViewModel
            {
                Advertisement = repository.Advertisements
                .FirstOrDefault(a => a.AdId == adId),
                Comments = repoComm.Comments.Where(a => a.adId == adId)

            };
            return View("Details", post);

        }
        [Authorize]
        public ViewResult Change()
        {
            AdListViewModel ads = new AdListViewModel
            {
                Advertisements = repository.Advertisements
                .Where(a => a.OwnerId == User.Identity.Name)
            };
            return View(ads);
        }
        [Authorize]
        public ViewResult Edit(int adId)
        {
            Advertisement ad = repository.Advertisements
                .FirstOrDefault(a => a.AdId == adId);
            return View(ad);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Advertisement ad, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    ad.ImageMimeType = image.ContentType;
                    ad.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(ad.ImageData, 0, image.ContentLength);
                }

                repository.SaveAd(ad);
                TempData["message"] = string.Format("Зміни оголошення збережено!");
                return RedirectToAction("List");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(ad);
            }
        }
        [Authorize]
        public ViewResult Create()
        {
            return View("Edit", new Advertisement());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(int adId)
        {
            Advertisement deletedAd = repository.DeleteAd(adId);
            if (deletedAd != null)
            {
                TempData["message"] = string.Format("Оголошення видалено");
            }
            return RedirectToAction("List");
        }
        public FileContentResult GetImage(int adId)
        {
            Advertisement ad = repository.Advertisements
                .FirstOrDefault(a => a.AdId == adId);

            if (ad != null && ad.ImageData != null)
            {

                return File(ad.ImageData, ad.ImageMimeType);

            }
            else
            {
                return null;
            }
        }

        
    }
}
    
    
    