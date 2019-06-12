using System.Linq;
using lab4.Models;

namespace lab4.ViewModels
{
    public enum SortState
    {
        No,
        NameAsc,
        NameDesc,
    }
    public class TVShowsViewModel
    {
        public TVShow TVShowViewModel { get; set; }
        public IQueryable<TVShow> PageViewModel { get; set; }
        public int PageNumber { get; set; }
    }
}
