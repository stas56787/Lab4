using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using lab4.Models;

namespace lab4.Data
{
    public class ScheduleForWeekContext
    {
        public static List<ScheduleForWeek> GetAll()
        {
            List<ScheduleForWeek> all = new List<ScheduleForWeek>();
            using (Context db = new Context())
            {
                all = db.SchedulesForWeek.ToList();
            }

            return all;
        }

        public static List<ScheduleForWeek> GetPage(int pageNumber, int sizeOfPage)
        {
            List<ScheduleForWeek> all = new List<ScheduleForWeek>();
            using (Context db = new Context())
            {
                all = db.SchedulesForWeek.Include(t => t.StartTime).
                    Skip(pageNumber * sizeOfPage).Take(sizeOfPage).ToList();
            }
            return all;
        }

        public static void AddScheduleForWeek(ScheduleForWeek scheduleForWeekToAdd)
        {
            using (Context db = new Context())
            {
                db.SchedulesForWeek.Add(scheduleForWeekToAdd);
                db.SaveChanges();
            }
        }

        public static void UpdataScheduleForWeek(ScheduleForWeek scheduleForWeek)
        {
            using (Context db = new Context())
            {
                if (scheduleForWeek != null)
                {
                    db.SchedulesForWeek.Update(scheduleForWeek);
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteScheduleForWeek(ScheduleForWeek scheduleForWeekToDelete)
        {
            using (Context db = new Context())
            {
                if (scheduleForWeekToDelete != null)
                {
                    db.SchedulesForWeek.Remove(scheduleForWeekToDelete);
                    db.SaveChanges();
                }
            }
        }

        public static List<ScheduleForWeek> FindScheduleForWeeks(int? scheduleForWeekID, string startTime,
            string guestsEmployees)
        {
            List<ScheduleForWeek> scheduleForWeek = new List<ScheduleForWeek>();
            using (Context db = new Context())
            {
                if (scheduleForWeekID != null)
                {
                    scheduleForWeek = db.SchedulesForWeek.Where(k => k.ScheduleForWeekID == scheduleForWeekID).ToList();
                }
                if (startTime != null)
                {
                    if (scheduleForWeek.Count != 0)
                    {
                        scheduleForWeek = scheduleForWeek.Where(k => k.StartTime == startTime).ToList();
                    }
                    else
                    {
                        scheduleForWeek = db.SchedulesForWeek.Where(k => k.StartTime == startTime).ToList();
                    }
                }
                if (guestsEmployees != null)
                {
                    if (scheduleForWeek.Count != 0)
                    {
                        scheduleForWeek = scheduleForWeek.Where(k => k.GuestsEmployees == guestsEmployees).ToList();
                    }
                    else
                    {
                        scheduleForWeek = db.SchedulesForWeek.Where(k => k.GuestsEmployees == guestsEmployees).ToList();
                    }
                }
            }
            return scheduleForWeek;
        }

        public static ScheduleForWeek FindScheduleForWeekById(int id)
        {
            ScheduleForWeek scheduleForWeek = null;
            using (Context db = new Context())
            {
                scheduleForWeek = db.SchedulesForWeek.Where(k => k.ScheduleForWeekID == id).ToList().FirstOrDefault();
            }
            return scheduleForWeek;
        }
    }
}
