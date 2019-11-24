using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Users
    {
        public Users()
        {
            FeedbackCreatedByNavigation = new HashSet<Feedback>();
            FeedbackCreatedForNavigation = new HashSet<Feedback>();
            FeedbackEscalationMapping = new HashSet<FeedbackEscalationMapping>();
            InverseCompany = new HashSet<Users>();
            RegisteredDevice = new HashSet<RegisteredDevice>();
            UserRoleMapping = new HashSet<UserRoleMapping>();
        }

        public long Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public long CompanyId { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime LastUpdate { get; set; }

        public virtual Users Company { get; set; }
        public virtual ICollection<Feedback> FeedbackCreatedByNavigation { get; set; }
        public virtual ICollection<Feedback> FeedbackCreatedForNavigation { get; set; }
        public virtual ICollection<FeedbackEscalationMapping> FeedbackEscalationMapping { get; set; }
        public virtual ICollection<Users> InverseCompany { get; set; }
        public virtual ICollection<RegisteredDevice> RegisteredDevice { get; set; }
        public virtual ICollection<UserRoleMapping> UserRoleMapping { get; set; }
    }
}
