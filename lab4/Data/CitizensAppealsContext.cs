using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using lab4.Models;

namespace lab4.Data
{
    public class CitizensAppealsContext
    {
        public static List<CitizensAppeal> GetAll()
        {
            List<CitizensAppeal> all = new List<CitizensAppeal>();
            using (Context db = new Context())
            {
                all = db.CitizensAppeals.ToList();
            }

            return all;
        }

        public static List<CitizensAppeal> GetPage(int pageNumber, int sizeOfPage)
        {
            List<CitizensAppeal> all = new List<CitizensAppeal>();
            using (Context db = new Context())
            {
                all = db.CitizensAppeals.Include(t => t.LFO).
                    Skip(pageNumber * sizeOfPage).Take(sizeOfPage).ToList();
            }
            return all;
        }

        public static void AddCitizensAppeal(CitizensAppeal citizensAppealToAdd)
        {
            using (Context db = new Context())
            {
                db.CitizensAppeals.Add(citizensAppealToAdd);
                db.SaveChanges();
            }
        }

        public static void UpdataCitizensAppeal(CitizensAppeal citizensAppeal)
        {
            using (Context db = new Context())
            {
                if (citizensAppeal != null)
                {
                    db.CitizensAppeals.Update(citizensAppeal);
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteCitizensAppeal(CitizensAppeal citizensAppealToDelete)
        {
            using (Context db = new Context())
            {
                if (citizensAppealToDelete != null)
                {
                    db.CitizensAppeals.Remove(citizensAppealToDelete);
                    db.SaveChanges();
                }
            }
        }

        public static List<CitizensAppeal> FindCitizensAppeal(string lfo, string organization)
        {
            List<CitizensAppeal> citizensAppeal = new List<CitizensAppeal>();
            using (Context db = new Context())
            {
                if (lfo != null && lfo != "")
                {
                    citizensAppeal = db.CitizensAppeals.Where(k => k.LFO == lfo).ToList();
                }
                if (organization != null)
                {
                    if (citizensAppeal.Count != 0)
                    {
                        citizensAppeal = citizensAppeal.Where(k => k.Organization == organization).ToList();
                    }
                    else
                    {
                        citizensAppeal = db.CitizensAppeals.Where(k => k.Organization == organization).ToList();
                    }
                }
            }
            return citizensAppeal;
        }

        public static CitizensAppeal FindTreatment(int id)
        {
            CitizensAppeal citizensAppeal = null;
            using (Context db = new Context())
            {
                citizensAppeal = db.CitizensAppeals.Where(k => k.CitizensAppealID == id).ToList().FirstOrDefault();
            }
            return citizensAppeal;
        }
    }
}
