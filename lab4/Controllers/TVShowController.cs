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
    public class TVShowController : Controller
    {
        private int pageSize = 5;
        private Context db;
        private TVShow _tvShow = new TVShow
        {
            NameShow = "",
            Duration = "",
            Rating = "",
            DescriptionShow = ""
    };

        public TVShowController(Context tvShowContext)
        {
            db = tvShowContext;
        }

        [HttpGet]
        public IActionResult Index(SortState sortOrder = SortState.No, int index = 0)
        {
            TVShow sessionTVShow = HttpContext.Session.GetObject<TVShow>("TVShow");
            string sessionSortState = HttpContext.Session.GetString("SortStateTVShow");
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

            if (sessionTVShow != null)
            {
                _tvShow = sessionTVShow;
            }

            if (sessionSortState != null)
                if (sortOrder == SortState.No)
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            ViewData["NameSort"] = sortOrder == SortState.NameDesc ? SortState.NameAsc : SortState.NameDesc;
            HttpContext.Session.SetString("SortStateTVShow", sortOrder.ToString());

            IQueryable<TVShow> tvShows = Sort(db.TVShows, sortOrder,
                _tvShow.NameShow, (int)page);
            TVShowsViewModel tvShowsView = new TVShowsViewModel
            {
                TVShowViewModel = _tvShow,
                PageViewModel = tvShows,
                PageNumber = (int)page
            };

            return View(tvShowsView);
        }

        [HttpPost]
        public IActionResult Index(TVShow tvShow)
        {
            var sessionSortState = HttpContext.Session.GetString("SortStateTVShow");
            SortState sortOrder = new SortState();
            if (sessionSortState != null)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            int? page = HttpContext.Session.GetInt32("Page");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("Page", 0);
            }

            IQueryable<TVShow> tvShows = Sort(db.TVShows, sortOrder,
                tvShow.NameShow, (int)page);
            HttpContext.Session.SetObject("Disease", tvShow);

            TVShowsViewModel tvShowsView = new TVShowsViewModel
            {
                TVShowViewModel = tvShow,
                PageViewModel = tvShows,
                PageNumber = (int)page
            };

            return View(tvShowsView);
        }

        private IQueryable<TVShow> Sort(IQueryable<TVShow> tvShows,
            SortState sortOrder, string name, int page)
        {
            switch (sortOrder)
            {
                case SortState.NameAsc:
                    tvShows = tvShows.OrderBy(s => s.NameShow);
                    break;
                case SortState.NameDesc:
                    tvShows = tvShows.OrderByDescending(s => s.NameShow);
                    break;
            }
            tvShows = tvShows.Where(o => o.NameShow.Contains(name ?? ""))
                .Skip(page * pageSize).Take(pageSize);
            return tvShows;
        }
    }
}