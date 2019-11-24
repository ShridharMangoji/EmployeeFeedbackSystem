using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class FeedbackStatus
    {
        public FeedbackStatus()
        {
            Feedback = new HashSet<Feedback>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Feedback> Feedback { get; set; }
    }
}
