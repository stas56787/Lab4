using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using lab4.Data;
using lab4.Models;
using lab4.ViewModels;
using lab4.Filters;
using Newtonsoft.Json;

namespace lab4.Controllers
{
    [CatchExceptionFilter]
    public class GenreController : Controller
    {
        private int pageSize = 5;
        private Context db;
        private Genre _genre = new Genre
        {
            NameGenre = "",
            DescriptionOfGenre = ""
        };

        public GenreController(Context genreContext) {
            db = genreContext;
        }

        [HttpGet]
        public IActionResult Index(SortState sortOrder = SortState.No, int index = 0)
        {
            Genre sessionGenres = HttpContext.Session.GetObject<Genre>("Genres");
            string sessionSortState = HttpContext.Session.GetString("SortState");
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

            if (sessionGenres != null)
            {
                _genre = sessionGenres;
            }

            if (sessionSortState != null)
                if (sortOrder == SortState.No)
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            ViewData["NameSort"] = sortOrder == SortState.NameDesc ? SortState.NameAsc : SortState.NameDesc;

            HttpContext.Session.SetString("SortState", sortOrder.ToString());
            IQueryable<Genre> genre = Sort(db.Genres, sortOrder,
                _genre.NameGenre, (int)page);
            GenresViewModel genresView = new GenresViewModel
            {
                GenreViewModel = _genre,
                PageViewModel = genre,
                PageNumber = (int)page
            };

            return View(genresView);
        }

        [HttpPost]
        public IActionResult Index(Genre genre)
        {
            var sessionSortState = HttpContext.Session.GetString("SortState");
            SortState sortOrder = new SortState();
            if (sessionSortState != null)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            int? page = HttpContext.Session.GetInt32("Page");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("Page", 0);
            }

            IQueryable<Genre> genres = Sort(db.Genres, sortOrder,
                genre.NameGenre, (int)page);
            HttpContext.Session.SetObject("Genres", genre);

            GenresViewModel genresView = new GenresViewModel
            {
                GenreViewModel = genre,
                PageViewModel = genres,
                PageNumber = (int)page
            };

            return View(genresView);
        }

        private IQueryable<Genre> Sort(IQueryable<Genre> genres,
            SortState sortOrder, string name, int page)
        {
            switch (sortOrder)
            {
                case SortState.NameAsc:
                    genres = genres.OrderBy(s => s.NameGenre);
                    break;
                case SortState.NameDesc:
                    genres = genres.OrderByDescending(s => s.NameGenre);
                    break;
            }
            genres = genres.Where(o => o.NameGenre.Contains(name ?? "")).Skip(page * pageSize).Take(pageSize);
            return genres;
        }

        private void SetSessionGenres(string sessionGenres) {
            _genre.NameGenre = sessionGenres.Split(':')[0];
        }
    }
}