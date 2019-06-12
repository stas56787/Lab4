using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using lab4.Data;
using lab4.Models;
using lab4.ViewModels;
using lab4.Filters;
using Newtonsoft.Json;

namespace lab4.Controllers
{
    [CatchExceptionFilter]
    public class ScheduleForWeekController : Controller
    {
        private int pageSize = 5;
        private Context db;
        private ScheduleForWeek _scheduleForWeek = new ScheduleForWeek
        {
            StartTime = "",
            GuestsEmployees = ""
        };

        public ScheduleForWeekController(Context ScheduleForWeekContext)
        {
            db = ScheduleForWeekContext;
        }

        [HttpGet]
        public IActionResult Index(SortState sortOrder = SortState.No, int index = 0)
        {
            ScheduleForWeek sessionScheduleForWeek = HttpContext.Session.GetObject<ScheduleForWeek>("Patient");
            string sessionSortState = HttpContext.Session.GetString("SortStatePatient");
            int? page = HttpContext.Session.GetInt32("Page");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("Page", 0);
            }
            else
            {
                if (!(page < 1 && index < 0))
                    page += index;
                HttpContext.Session.SetInt32("Page", (int)page);
            }

            if (sessionScheduleForWeek != null)
            {
                _scheduleForWeek = sessionScheduleForWeek;
            }

            if (sessionSortState != null)
                if (sortOrder == SortState.No)
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            ViewData["NameSort"] = sortOrder == SortState.NameDesc ? SortState.NameAsc : SortState.NameDesc;
            HttpContext.Session.SetString("SortState", sortOrder.ToString());
            IQueryable<ScheduleForWeek> ScheduleForWeeks = Sort(db.SchedulesForWeek, sortOrder,
                _scheduleForWeek.StartTime, (int)page);
            ScheduleForWeeksViewModel scheduleForWeeksView = new ScheduleForWeeksViewModel
            {
                ScheduleForWeekViewModel = _scheduleForWeek,
                PageViewModel = ScheduleForWeeks,
                PageNumber = (int)page
            };

            return View(scheduleForWeeksView);
        }

        [HttpPost]
        public IActionResult Index(ScheduleForWeek scheduleForWeek)
        {
            var sessionSortState = HttpContext.Session.GetString("SortStateScheduleForWeek");
            SortState sortOrder = new SortState();
            if (sessionSortState != null)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            int? page = HttpContext.Session.GetInt32("Page");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("Page", 0);
            }

            IQueryable<ScheduleForWeek> scheduleForWeeks = Sort(db.SchedulesForWeek, sortOrder,
                 scheduleForWeek.StartTime, (int)page);
            HttpContext.Session.SetObject("scheduleForWeek", scheduleForWeek);

            ScheduleForWeeksViewModel scheduleForWeeksView = new ScheduleForWeeksViewModel
            {
                ScheduleForWeekViewModel = scheduleForWeek,
                PageViewModel = scheduleForWeeks,
                PageNumber = (int)page
            };

            return View(scheduleForWeeksView);
        }

        private IQueryable<ScheduleForWeek> Sort(IQueryable<ScheduleForWeek> ScheduleForWeeks,
            SortState sortOrder, string name, int page)
        {
            switch (sortOrder)
            {
                case SortState.NameAsc:
                    ScheduleForWeeks = ScheduleForWeeks.OrderBy(s => s.StartTime);
                    break;
                case SortState.NameDesc:
                    ScheduleForWeeks = ScheduleForWeeks.OrderByDescending(s => s.StartTime);
                    break;
            }
            ScheduleForWeeks = ScheduleForWeeks.Where(o => o.StartTime.Contains(name ?? ""))
                .Skip(page * pageSize).Take(pageSize);
            return ScheduleForWeeks;
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<ScheduleForWeek> scheduleForWeeks = ScheduleForWeekContext.GetPage(0, pageSize);
            return View(scheduleForWeeks);
        }

        [HttpPost]
        public string Add(string startTime, string guestsEmployees)
        {
            return "Расписание, с началом в " + startTime + " и приглашенными " + guestsEmployees + " успешно добавлено.";
        }
    }
}