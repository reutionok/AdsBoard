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
    public class AdController : Controller
    {
        private IAdRepository repository;
        public int pageSize = 4;
        public AdController(IAdRepository repo)
        {
            repository = repo;
        }
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
        public ViewResult Edit(int adId)
        {
            Advertisement ad = repository.Advertisements
                .FirstOrDefault(a => a.AdId == adId);
            return View(ad);
        }
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
        public ViewResult Create()
        {
            return View("Edit", new Advertisement());
        }
        public FileContentResult GetImage(int adId)
        {
            Advertisement ad = repository.Advertisements
                .FirstOrDefault(a => a.AdId == adId);

            if (ad != null)
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
