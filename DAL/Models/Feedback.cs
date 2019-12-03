using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Feedback
    {
        public Feedback()
        {
            FeedbackChats = new HashSet<FeedbackChats>();
            FeedbackEscalationMapping = new HashSet<FeedbackEscalationMapping>();
        }

        public long Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public long CreatedBy { get; set; }
        public long CreatedFor { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdate { get; set; }
        public int StatusId { get; set; }
        public int FeedbackCategoryId { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual Users CreatedForNavigation { get; set; }
        public virtual FeedbackCategory FeedbackCategory { get; set; }
        public virtual FeedbackStatus Status { get; set; }
        public virtual ICollection<FeedbackChats> FeedbackChats { get; set; }
        public virtual ICollection<FeedbackEscalationMapping> FeedbackEscalationMapping { get; set; }
    }
}
