using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class FeedbackCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool? IsActive { get; set; }
    }
}
