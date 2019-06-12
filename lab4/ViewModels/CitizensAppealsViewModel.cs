using System.Linq;
using lab4.Models;

namespace lab4.ViewModels
{
    public class CitizensAppealsViewModel
    {
        public CitizensAppeal CitizensAppealViewModel { get; set; }
        public IQueryable<CitizensAppeal> PageViewModel { get; set; }
        public int PageNumber { get; set; }
    }
}
