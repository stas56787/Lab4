using System.Linq;
using lab4.Models;

namespace lab4.ViewModels
{
    public class ScheduleForWeeksViewModel
    {
        public ScheduleForWeek ScheduleForWeekViewModel { get; set; }
        public IQueryable<ScheduleForWeek> PageViewModel { get; set; }
        public int PageNumber { get; set; }     
    }
}
