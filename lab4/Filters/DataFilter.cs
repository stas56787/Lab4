using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using lab4.Data;
using lab4.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace lab4.Filters
{
    public class DataFilter : Attribute, IActionFilter
    {
        private string type;
        Context db = new Context();
        public DataFilter(string type)
        {
            this.type = type;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (type == "addTVShow")
                context.HttpContext.Session.SetString("getTVShows", JsonConvert.SerializeObject(db.TVShows.ToList()));
            if (type == "getTVShows")
            {
                context.HttpContext.Session.SetString("getTVShows", JsonConvert.SerializeObject(db.TVShows.ToList()));
            }
            if (type == "addGenre")
                context.HttpContext.Session.SetString("getGenres", JsonConvert.SerializeObject(db.Genres.ToList()));
            if (type == "getGenres")
            {
                context.HttpContext.Session.SetString("getGenres", JsonConvert.SerializeObject(db.Genres.ToList()));
            }
            if (type == "addGenre")
            {
                context.HttpContext.Session.SetString("getGenres", JsonConvert.SerializeObject(db.Genres.Select(p => new Genre() { NameGenre = p.NameGenre, DescriptionOfGenre = p.DescriptionOfGenre }).ToList()));
                context.HttpContext.Session.SetString("getTVShows", JsonConvert.SerializeObject(db.TVShows.Select(p => new TVShow() { NameShow = p.NameShow, Duration = p.Duration, Rating = p.Rating }).ToList()));
                db.Genres.ToList();
                db.TVShows.ToList();
                context.HttpContext.Items.Add("getGenres", db.Genres.ToList());
            }
            if (type == "getGenres")
            {
                context.HttpContext.Session.SetString("getGenres", JsonConvert.SerializeObject(db.Genres.Select(p => new Genre() { NameGenre = p.NameGenre, DescriptionOfGenre = p.DescriptionOfGenre }).ToList()));
                context.HttpContext.Session.SetString("getTVShows", JsonConvert.SerializeObject(db.TVShows.Select(p => new TVShow() { NameShow = p.NameShow, Duration = p.Duration, Rating = p.Rating }).ToList()));
                db.Genres.ToList();
                db.TVShows.ToList();
                context.HttpContext.Items.Add("getMedicines", db.Genres.ToList());
            }

        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (type == "addTVShow" && context.HttpContext.Session.Keys.Contains("allow"))
            {
                db.TVShows.Add(JsonConvert.DeserializeObject<TVShow>(context.HttpContext.Session.GetString("addTVShow")));
                db.SaveChanges();
            }
            if (type == "addGenre" && context.HttpContext.Session.Keys.Contains("allow"))
            {
                db.Genres.Add(JsonConvert.DeserializeObject<Genre>(context.HttpContext.Session.GetString("addGenre")));
                db.SaveChanges();
            }
            if (type == "addGenre" && context.HttpContext.Session.Keys.Contains("allow"))
            {
                Genre Genre = JsonConvert.DeserializeObject<Genre>(context.HttpContext.Session.GetString("Genre"));
                string TVShow = context.HttpContext.Session.GetString("TVShow");
                db.Genres.Add(Genre);
                db.SaveChanges();
            }
        }

    }
}
