using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using lab4.Models;

namespace lab4.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<TVShow> TVShows { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<ScheduleForWeek> SchedulesForWeek { get; set; }
        public IEnumerable<CitizensAppeal> CitizensAppeals { get; set; }
    }
}
