using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using lab4.Models;

namespace lab4.Data
{
    public class GenreContext
    {
        public static List<Genre> GetAll()
        {
            List<Genre> all = new List<Genre>();
            using (Context db = new Context())
            {
                all = db.Genres.ToList();
            }

            return all;
        }

        public static List<Genre> GetPage(int pageNumber, int sizeOfPage)
        {
            List<Genre> all = new List<Genre>();
            using (Context db = new Context())
            {
                all = db.Genres.Include(t => t.NameGenre).
                    Skip(pageNumber * sizeOfPage).Take(sizeOfPage).ToList();
            }
            return all;
        }

        public static void AddGenre(Genre genre)
        {
            using (Context db = new Context())
            {
                db.Genres.Add(genre);
                db.SaveChanges();
            }
        }

        public static void UpdataGenre(Genre genre)
        {
            using (Context db = new Context())
            {
                if (genre != null)
                {
                    db.Genres.Update(genre);
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteGenre(Genre genre)
        {
            using (Context db = new Context())
            {
                if (genre != null)
                {
                    db.Genres.Remove(genre);
                    db.SaveChanges();
                }
            }
        }

        public static List<Genre> FindGenre(string nameGenre, string descriptionOfGenre)
        {
            List<Genre> genre = new List<Genre>();
            using (Context db = new Context())
            {
                if (nameGenre != null)
                {
                    genre = db.Genres.Where(k => k.NameGenre == nameGenre).ToList();
                }
                if (descriptionOfGenre != null)
                {
                    if (genre.Count != 0)
                    {
                        genre = genre.Where(k => k.DescriptionOfGenre == descriptionOfGenre).ToList();
                    }
                    else
                    {
                        genre = db.Genres.Where(k => k.DescriptionOfGenre == descriptionOfGenre).ToList();
                    }
                }            }
            return genre;
        }

        public static Genre FindGenreById(int id)
        {
            Genre genre = null;
            using (Context db = new Context())
            {
                genre = db.Genres.Where(k => k.GenreID == id).ToList().FirstOrDefault();
            }
            return genre;
        }
    }
}
