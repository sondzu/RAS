﻿using HakunaMatata.Models;
using HakunaMatata.Models.DataModels;
using HakunaMatata.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;


namespace HakunaMatata.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRealEstateServices _services;
        public HomeController(ILogger<HomeController> logger, IRealEstateServices services)
        {
            _logger = logger;
            _services = services;
        }

        public IActionResult Index()
        {
            var cityList = _services.GetCityList();
            cityList = cityList.Concat(new[] { new City { Id = 0, CityName = "Chọn thành phố" } });
            cityList = cityList.OrderBy(c => c.Id);

            var typeList = _services.GetRealEstateTypeList();
            typeList = typeList.Concat(new[] { new RealEstateType { Id = 0, RealEstateTypeName = "Loại phòng" } });
            typeList = typeList.OrderBy(t => t.Id);

            var Top4 = _services.GetList();
            Top4 = Top4.OrderByDescending(c => c.Id).Take(4).ToList();
            ViewBag.Top4 = Top4;
            ViewData["Cities"] = new SelectList(cityList, "Id", "CityName");
            ViewData["Types"] = new SelectList(typeList, "Id", "RealEstateTypeName");
            ViewData["Types1"] = new SelectList(typeList, "Id", "RealEstateTypeName");
            return View();
        }
        public IActionResult NewList()
        {
            var newsListInfo = _services.GetListNews();
            return View(newsListInfo);
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
    }
}
