using System;
using System.Collections.Generic;
using System.Text;
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
