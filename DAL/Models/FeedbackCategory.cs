using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class FeedbackCategory
    {
        public FeedbackCategory()
        {
            Feedback = new HashSet<Feedback>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Feedback> Feedback { get; set; }
    }
}
