using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class FeedbackChats
    {
        public long Id { get; set; }
        public long FeedbackId { get; set; }
        public string Reply { get; set; }
        public DateTime LastUpdate { get; set; }
        public long ReplyGivenBy { get; set; }

        public virtual Feedback Feedback { get; set; }
        public virtual Users ReplyGivenByNavigation { get; set; }
    }
}
