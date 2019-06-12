using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab4.Models;

namespace lab4.Data
{
    public class TVShowsContext
    {
        public static List<TVShow> GetAll()
        {
            List<TVShow> all = new List<TVShow>();
            using (Context db = new Context())
            {
                all = db.TVShows.ToList();
            }

            return all;
        }

        public static void AddTVShow(TVShow tvShowToAdd)
        {
            using (Context db = new Context())
            {
                db.TVShows.Add(tvShowToAdd);
                db.SaveChanges();
            }
        }

        public static void UpdataTVShow(TVShow tvShow)
        {
            using (Context db = new Context())
            {
                if (tvShow != null)
                {
                    db.TVShows.Update(tvShow);
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteTVShow(TVShow tvShowToDelete)
        {
            using (Context db = new Context())
            {
                if (tvShowToDelete != null)
                {
                    db.TVShows.Remove(tvShowToDelete);
                    db.SaveChanges();
                }
            }
        }

        public static List<TVShow> FindTVShows(string tvShowName)
        {
            List<TVShow> tvShows = new List<TVShow>();
            using (Context db = new Context())
            {
                if (tvShowName != null && tvShowName != "")
                {
                    tvShows = db.TVShows.Where(k => k.NameShow == tvShowName).ToList();
                }
            }
            return tvShows;
        }

        public static TVShow FindTVShowById(int id)
        {
            TVShow tvShow = null;
            using (Context db = new Context())
            {
                tvShow = db.TVShows.Where(k => k.TVShowID == id).ToList().FirstOrDefault();
            }
            return tvShow;
        }
    }
}
