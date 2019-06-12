using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using lab4.Models;

namespace lab4.Data
{
    public static class DbInitializer
    {
        public static void Initialize(Context db)
        {
            db.Database.EnsureCreated();

            if (db.TVShows.Any())
            {
                return;
            }

            Random rand = new Random();
            int id;
            string[] genres = { "Викторина", "Разговорное", "Здоровье", "Интервью", "Новости", "Шоу талантов", "Путешествия", "Юмор" };
            string[] description = { "18+", "12+", "0+", "16+", "10+", "5+" };
            for (int i = 0; i < 30; i++)
            {
                db.Genres.Add(new Genre
                {
                    NameGenre = genres[rand.Next(8)],
                    DescriptionOfGenre = description[rand.Next(6)]
                });
            }
            db.SaveChanges();

            string[] showName = { "Пусть говорят", "Жить здорово", "Вести", "Что? Кто? А?", "Решка и Пешка" };
            string[] showDesc = { "Учавствуют взрослые", "Учавствуют взрослые и дети", "Учавствуют дети" };
            for (int i = 0; i < 30; i++)
            {
                int disID = rand.Next(1, 6) - 1;
                db.TVShows.Add(new TVShow
                {
                    NameShow = showName[disID],
                    Duration = rand.Next(20, 60) + " мин.",
                    Rating = rand.Next(0, 100) + " %",
                    DescriptionShow = showDesc[rand.Next(3)],
                    GenreID = db.Genres.Where(o => o.NameGenre == genres[disID]).First().GenreID,
                    Genre = db.Genres.Where(o => o.NameGenre == genres[disID]).FirstOrDefault()
                });
            }
            db.SaveChanges();

            string[] guests = { "Андрей Малахов", "Елена Малышева", "Казимир", "Александр Лукашенко", "Оптимус", "Николай Баков" };
            for (int i = 0; i < 30; i++)
            {
                int disID = rand.Next(1, 6) - 1;
                db.SchedulesForWeek.Add(new ScheduleForWeek
                {
                    StartTime = rand.Next(23) + ":" + rand.Next(59),
                    GuestsEmployees = guests[rand.Next(6)],
                    TVShowID = db.TVShows.Where(o => o.NameShow == showName[disID]).First().TVShowID,
                    TVShow = db.TVShows.Where(o => o.NameShow == showName[disID]).FirstOrDefault()
                });
            }
            db.SaveChanges();

            string[] lfo = { "Зубенко Михаил Николаевич", "Малышева Полина Казимировна", "Лев Николаевич Толстой", "Шупкин Сергей Александрович", "Оптимус Бодя Александрович" };
            string[] organization = { "EPAM", "IBM", "Первый канал", "OP" };
            string[] goalOfRequest = { "Закрытие шоу", "Нарушение авторских прав", "Изменение возрастных ограничений" };
            for (int i = 0; i < 30; i++)
            {
                int disID = rand.Next(1, 6) - 1;
                db.CitizensAppeals.Add(new CitizensAppeal
                {
                    LFO = lfo[rand.Next(5)],
                    Organization = organization[rand.Next(4)],
                    GoalOfRequest = goalOfRequest[rand.Next(3)],
                    ScheduleForWeekID = db.SchedulesForWeek.Where(o => o.GuestsEmployees == guests[disID]).First().ScheduleForWeekID,
                    ScheduleForWeek = db.SchedulesForWeek.Where(o => o.GuestsEmployees == guests[disID]).FirstOrDefault()
                });
            }
            db.SaveChanges();
        }
    }
}
