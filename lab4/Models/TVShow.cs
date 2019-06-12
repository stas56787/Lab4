using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lab4.Models
{
    public class TVShow
    {
        [Key]
        public int TVShowID { get; set; }
        public string NameShow { get; set; }
        public string Duration { get; set; }
        public string Rating { get; set; }
        public string DescriptionShow { get; set; }
        public int? GenreID { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual ICollection<ScheduleForWeek> ScheduleForWeeks { get; set; }

        public TVShow() { }

        public TVShow(int TVShowID, string NameShow, string Duration, string Rating, string DescriptionShow,
            int? GenreID)
        {
            this.TVShowID = TVShowID;
            this.NameShow = NameShow;
            this.Duration = Duration;
            this.Rating = Rating;
            this.DescriptionShow = DescriptionShow;
            this.GenreID = GenreID;
        }

        public override bool Equals(object obj)
        {
            var item = obj as TVShow;

            if (obj == null)
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }

            return this.TVShowID == item.TVShowID;
        }

        public override int GetHashCode()
        {
            return this.TVShowID.GetHashCode();
        }
    }
}
