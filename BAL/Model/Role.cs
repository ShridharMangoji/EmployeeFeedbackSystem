using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.Model
{
   public abstract class Role
    {
        public abstract List<Feedback> GetMyFeedbacks();
    }
}
