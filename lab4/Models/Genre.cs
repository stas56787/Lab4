using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lab4.Models
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }
        public string NameGenre { get; set; }
        public string DescriptionOfGenre { get; set; }

        public virtual ICollection<TVShow> TVShows { get; set; }

        public Genre() { }

        public Genre(int GenreID, string NameGenre, string DescriptionOfGenre)
        {
            this.GenreID = GenreID;
            this.NameGenre = NameGenre;
            this.DescriptionOfGenre = DescriptionOfGenre;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Genre;

            if (obj == null)
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }

            return this.GenreID == item.GenreID;
        }

        public override int GetHashCode()
        {
            return this.GenreID.GetHashCode();
        }
    }
}
