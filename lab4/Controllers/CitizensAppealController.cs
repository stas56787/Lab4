using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using lab4.Data;
using lab4.Models;
using lab4.ViewModels;
using lab4.Filters;

namespace lab4.Controllers
{
    [CatchExceptionFilter]
    public class CitizensAppealController : Controller
    {
        private int pageSize = 5;
        private Context db;
        private CitizensAppeal _citizensAppeal = new CitizensAppeal
        {
            LFO = "",
            Organization = "",
            GoalOfRequest = ""
        };

        public CitizensAppealController(Context CitizensAppealContext)
        {
            db = CitizensAppealContext;
        }

        [HttpGet]
        public IActionResult Index(SortState sortOrder = SortState.No, int index = 0)
        {
            CitizensAppeal sessionCitizensAppeal = HttpContext.Session.GetObject<CitizensAppeal>("Treatment");
            string sessionSortState = HttpContext.Session.GetString("SortStateCitizensAppeal");
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

            if (sessionCitizensAppeal != null)
            {
                _citizensAppeal = sessionCitizensAppeal;
            }

            if (sessionSortState != null)
                if (sortOrder == SortState.No)
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            ViewData["NameSort"] = sortOrder == SortState.NameDesc ? SortState.NameAsc : SortState.NameDesc;
            HttpContext.Session.SetString("SortState", sortOrder.ToString());
            IQueryable<CitizensAppeal> CitizensAppeals = Sort(db.CitizensAppeals, sortOrder,
                _citizensAppeal.LFO, (int)page);
            CitizensAppealsViewModel CitizensAppealsView = new CitizensAppealsViewModel
            {
                CitizensAppealViewModel = _citizensAppeal,
                PageViewModel = CitizensAppeals,
                PageNumber = (int)page
            };

            return View(CitizensAppealsView);
        }

        [HttpPost]
        public IActionResult Index(CitizensAppeal citizensAppeal)
        {
            var sessionSortState = HttpContext.Session.GetString("SortStateCitizensAppeal");
            SortState sortOrder = new SortState();
            if (sessionSortState != null)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            int? page = HttpContext.Session.GetInt32("Page");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("Page", 0);
            }

            IQueryable<CitizensAppeal> citizensAppeals = Sort(db.CitizensAppeals, sortOrder,
                citizensAppeal.LFO, (int)page);
            HttpContext.Session.SetObject("CitizensAppeal", citizensAppeal);

            CitizensAppealsViewModel citizensAppealsView = new CitizensAppealsViewModel
            {
                CitizensAppealViewModel = citizensAppeal,
                PageViewModel = citizensAppeals,
                PageNumber = (int)page
            };

            return View(citizensAppealsView);
        }

        private IQueryable<CitizensAppeal> Sort(IQueryable<CitizensAppeal> CitizensAppeals,
            SortState sortOrder, string name, int page)
        {
            switch (sortOrder)
            {
                case SortState.NameAsc:
                    CitizensAppeals = CitizensAppeals.OrderBy(s => s.LFO);
                    break;
                case SortState.NameDesc:
                    CitizensAppeals = CitizensAppeals.OrderByDescending(s => s.LFO);
                    break;
            }
            CitizensAppeals = CitizensAppeals.Where(o => o.LFO.Contains(name ?? ""))
                .Skip(page * pageSize).Take(pageSize);
            return CitizensAppeals;
        }
    }
}