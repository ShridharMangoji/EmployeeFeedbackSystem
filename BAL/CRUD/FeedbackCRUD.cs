using BAL.Model;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.CRUD
{
    public class FeedbackCRUD
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
                var result = db.Feedback.Where(x => x.Id == feedbackID).FirstOrDefault();
                return result;
            }
        }

        public static List<FeedbackEscalationMapping> GetFeedbackEscalationDetails(long feedbackID)
        {
            using (var db = new Entities())
            {
                var result = db.FeedbackEscalationMapping.Where(x => x.Id == feedbackID).OrderBy(x => x.LastUpdate).ToList();
                return result;
            }
        }


        public static void AddFeedback(Feedback feedbackReq)
        {
            using (var db = new Entities())
            {
                db.Feedback.Add(feedbackReq);
                db.SaveChanges();
            }
        }
        public static void AddFeedbackEscalation(FeedbackEscalationMapping feedbackReq)
        {
            using (var db = new Entities())
            {
                db.FeedbackEscalationMapping.Add(feedbackReq);
                db.SaveChanges();
            }
        }

        public static List<UserModel> GetEscalatedUser(long user_id)
        {
            using (var db = new Entities())
            {
                var fUserlist = db.Feedback.Where(x => x.CreatedFor == user_id && x.StatusId != 3 && (DateTime.Now - x.CreatedOn).Days >= 7).Select(x =>
                         new UserModel()
                         {
                             ID = x.CreatedBy,
                             Name = x.CreatedByNavigation.Name
                         }).ToList();

                var cUserlist = db.Feedback.Where(x => x.CreatedBy == user_id && x.StatusId != 3 && (DateTime.Now-x.CreatedOn).Days >= 7).Select(x =>
                   new UserModel()
                   {
                       ID = x.CreatedFor,
                       Name = x.CreatedForNavigation.Name
                   }).ToList();

                var eUserlist = db.FeedbackEscalationMapping.Where(x => x.EscalatedUserId == user_id && x.Feedback.StatusId != 3 && (DateTime.Now-x.EscalatedUser.LastUpdate ).Days >= 7).Select(x =>
                   new UserModel()
                   {
                       ID = x.Feedback.CreatedBy,
                       Name = x.Feedback.CreatedByNavigation.Name
                   }).ToList();
                fUserlist.AddRange(cUserlist);
                fUserlist.AddRange(eUserlist);
                return fUserlist.Distinct().ToList();
            }
        }

        public static List<Feedback> GetFeedbackList(long user_id,long team_user_id)
        {
            using (var db = new Entities())
            {

                var cUserlist = db.Feedback.Where(x => x.CreatedBy == user_id && x.CreatedFor== team_user_id&& x.StatusId != 3 && (DateTime.Now - x.CreatedOn).Days >= 7)
                    .ToList();

                var eUserlist = db.FeedbackEscalationMapping.Where(x => x.EscalatedUserId == user_id && x.Feedback.StatusId != 3 && (DateTime.Now - x.EscalatedUser.LastUpdate).Days >= 7).Select(x =>
                    x.Feedback).ToList();
                //cUserlist.AddRange(cUserlist);
                cUserlist.AddRange(eUserlist);
                return cUserlist.Distinct().ToList();
            }
        }
    }
}
