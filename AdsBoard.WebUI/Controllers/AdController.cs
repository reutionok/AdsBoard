using AdsBoard.Domain.Abstract;
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
    }
}