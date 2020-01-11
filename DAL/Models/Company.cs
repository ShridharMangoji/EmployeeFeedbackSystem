using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Company
    {
        public Company()
        {
            Users = new HashSet<Users>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
