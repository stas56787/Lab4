using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using lab4.Models;
using lab4.ViewModels;
using lab4.Services;
using lab4.Filters;

namespace lab4.Controllers
{
    [CatchExceptionFilter]
    public class HomeController : Controller
    {
        //private IMemoryCache _cache;
        public HomeController()
        {
            //_cache = memoryCache;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            HomeViewModel homeViewModel = TakeLast.GetHomeViewModel();
            return View(homeViewModel);
        }

        public IActionResult ToError()
        {
            return View("~/Views/Home/About.cshtml");
        }

       // [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult About3()
        {

            HomeViewModel homeViewModel = TakeLast.GetHomeViewModel();
            return View("~/Views/Home/About.cshtml", homeViewModel);
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult TVShow()
        {
            if (HttpContext.Session.Keys.Contains("nameShow"))
            {
                ViewBag.NameShow = HttpContext.Session.GetString("nameShow");
            }
            if (HttpContext.Session.Keys.Contains("duration"))
            {
                ViewBag.Duration = HttpContext.Session.GetString("duration");
            }
            if (HttpContext.Session.Keys.Contains("rating"))
            {
                ViewBag.Rating = HttpContext.Session.GetString("rating");
            }
            if (HttpContext.Session.Keys.Contains("descriptionShow"))
            {
                ViewBag.DescriptionShow = HttpContext.Session.GetString("descriptionShow");
            }
            return View("~/Views/TVShow/Index.cshtml");
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult CitizensAppeal()
        {
            return View("~/Views/CitizensAppeal/Index.cshtml");
        }
        [HttpGet]
        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult ScheduleForWeek()
        {
            return View("~/Views/ScheduleForWeek/Index.cshtml");
        }
        [HttpGet]
        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Genre()
        {
            //string medicineName = "", medicineDosage = "";
            //if (HttpContext.Request.Cookies.ContainsKey("medicineName"))
            //{
            //    medicineName = HttpContext.Request.Cookies["medicineName"];
            //}
            //if (HttpContext.Request.Cookies.ContainsKey("medicineDosage"))
            //{
            //    medicineDosage = HttpContext.Request.Cookies["medicineDosage"];
            //}
            //ViewBag.MedicineName = medicineName;
            //ViewBag.MedicineDosage = medicineDosage;
            return View("~/Views/Genre/Index.cshtml");
        }

        [HttpPost]
        public string TVShow(string nameShow, string duration,
            string rating, string descriptionShow)
        {
            HttpContext.Session.SetString("nameShow", nameShow);
            HttpContext.Session.SetString("duration", duration);
            HttpContext.Session.SetString("rating", rating);
            HttpContext.Session.SetString("descriptionShow", descriptionShow);
            return "Шоу \"" + nameShow + "\"" + ", длительностью \"" + duration +
                 "\" и рейтингом \"" + rating + "\" добавлено в базу";
        }

        [HttpPost]
        public string CitizensAppeal(string lfo, string organization, string goalOfRequest)
        {
            return "Обращение от \"" + lfo + "\" от организации \"" + organization + "\" добавлено в базу. \nЦель обращения: \"" + goalOfRequest + "\"";
        }

        [HttpPost]
        public string Genre(string nameGenre, string descriptionOfGenre)
        {
            //HttpContext.Response.Cookies.Append("medicineName", medicineName);
            //HttpContext.Response.Cookies.Append("medicineManufacturer", medicineManufacturer);
            //HttpContext.Response.Cookies.Append("medicineDosage", medicineDosage);
            return "Жанр \"" + nameGenre + "\" добавлен в базу.\nОписание: \"" + descriptionOfGenre + "\"";
        }

        [HttpPost]
        public string ScheduleForWeek(string startTime, string guestsEmployees)
        {
            return "Расписание с началом в " + startTime + " и приглашенными гостями: " + guestsEmployees;
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
