using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class FeedbackEscalationMapping
    {
        public long Id { get; set; }
        public long FeedbackId { get; set; }
        public long EscalatedUserId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime LastUpdate { get; set; }

        public virtual Users EscalatedUser { get; set; }
        public virtual Feedback Feedback { get; set; }
    }
}
