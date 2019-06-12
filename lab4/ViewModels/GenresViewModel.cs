using System.Linq;
using lab4.Models;

namespace lab4.ViewModels
{
    public class GenresViewModel
    {
        public Genre GenreViewModel { get; set; }
        public IQueryable<Genre> PageViewModel { get; set; }
        public int PageNumber { get; set; }
    }
}
