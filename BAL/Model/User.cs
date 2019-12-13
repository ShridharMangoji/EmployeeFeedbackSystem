using System.Collections.Generic;
using DAL.Models;

namespace BAL.Model
{
    class User : Role
    {
        public override List<Feedback> GetMyFeedbacks()
        {
            return null;
        }
    }
}
