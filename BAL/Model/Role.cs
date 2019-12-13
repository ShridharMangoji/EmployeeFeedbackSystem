using DAL.Models;
using System.Collections.Generic;

namespace BAL.Model
{
    public abstract class Role
    {
        public abstract List<Feedback> GetMyFeedbacks();
    }
}
