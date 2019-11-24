using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            UserRoleMapping = new HashSet<UserRoleMapping>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }
        public int Scale { get; set; }

        public virtual ICollection<UserRoleMapping> UserRoleMapping { get; set; }
    }
}
