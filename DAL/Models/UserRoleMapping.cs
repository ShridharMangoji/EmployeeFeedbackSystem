using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class UserRoleMapping
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime LastUpdate { get; set; }

        public virtual UserRole Role { get; set; }
        public virtual Users User { get; set; }
    }
}
