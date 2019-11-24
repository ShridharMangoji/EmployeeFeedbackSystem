using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.CRUD
{
  public  class FeedbackCRUD
    {
        public static List<FeedbackCategory> GetAllFeedbackCategories()
        {
            using (var db = new Entities())
            {
                var result = db.FeedbackCategory.ToList();
                return result;
            }
        }

        public static Feedback GetFeedbackDetails(long feedbackID)
        {
            using (var db = new Entities())
            {
                var result = db.Feedback.Where(x=>x.Id==feedbackID).FirstOrDefault();
                return result;
            }
        }

        public static List<FeedbackEscalationMapping> GetFeedbackEscalationDetails(long feedbackID)
        {
            using (var db = new Entities())
            {
                var result = db.FeedbackEscalationMapping.Where(x => x.Id == feedbackID).OrderBy(x=>x.LastUpdate).ToList();
                return result;
            }
        }
    }
}
