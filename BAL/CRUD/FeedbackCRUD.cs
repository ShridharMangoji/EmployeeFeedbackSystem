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

        public static FeedbackModel GetFeedbackDetails(long feedbackID)
        {
            using (var db = new Entities())
            {
                var result = db.Feedback.Where(x => x.Id == feedbackID)
                     .Select(
                    x => new FeedbackModel()
                    {
                        Id = x.Id,
                        CreatedBy = x.CreatedBy,
                        CreatedFor = x.CreatedFor,
                        FeedbackCategoryId = x.FeedbackCategoryId,
                        Message = x.Message,
                        statusId = x.StatusId,
                        CreatedOn = x.CreatedOn,
                        CreatedForName = x.CreatedForNavigation.Name,
                        FeedbackCategoryName = x.FeedbackCategory.Name,
                        StrCreatedOn = x.CreatedOn.ToLongDateString()
                    }).FirstOrDefault();
                return result;
            }
        }

        public static List<FeedbackEscalationMapping> GetFeedbackEscalationDetails(long feedbackID)
        {
            //return new List<FeedbackEscalationMapping>()
            //{
            //     new FeedbackEscalationMapping()
            //     {
            //          Message="abc"
            //     }
            //};
            using (var db = new Entities())
            {
                var result = db.FeedbackEscalationMapping.Where(x => x.FeedbackId == feedbackID).OrderBy(x => x.LastUpdate).ToList();
                return result;
            }
        }
        public static void updateFeedbackStatus(long feedbackID, int status)
        {
            using (var db = new Entities())
            {
                var feedback = db.Feedback.Where(x => x.Id == feedbackID).FirstOrDefault();
                if (feedback != null)
                {
                    feedback.StatusId = status;
                    db.SaveChanges();
                }
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

                var eUserlist = db.FeedbackEscalationMapping.Where(x => x.EscalatedUserId == user_id && (x.Feedback.StatusId != 3 || x.Feedback.StatusId != 4) && (DateTime.Now - x.EscalatedUser.LastUpdate).Days >= 7)
                    .Select(x =>
                    new UserModel()
                    {
                        ID = x.Feedback.CreatedFor,
                        Name = x.Feedback.CreatedForNavigation.Name
                    }).ToList();
                var check= eUserlist.GroupBy(x=>x.ID).Select(g=>g.First()).ToList();
                return check;
            }
        }
        public static List<FeedbackModel> MyFeedbacks(long user_id)
        {
            using (var db = new Entities())
            {
                var cUserlist = db.Feedback.Where(x => x.CreatedBy == user_id
                && x.StatusId != 3)
                   .Select(
                    x => new FeedbackModel()
                    {
                        Id = x.Id,
                        CreatedBy = x.CreatedBy,
                        CreatedFor = x.CreatedFor,
                        FeedbackCategoryId = x.FeedbackCategoryId,
                        Message = x.Message,
                        CreatedForName = x.CreatedForNavigation.Name,
                        FeedbackCategoryName = x.FeedbackCategory.Name,
                        statusId = x.StatusId,
                    })
                    .ToList();
                return cUserlist;
            }
        }
        public static List<FeedbackModel> FeedbacksCreatedForMe(long user_id)
        {
            using (var db = new Entities())
            {
                var cUserlist = db.Feedback.Where(x => x.CreatedFor == user_id
                && (x.StatusId != 3|| x.StatusId != 4))
                     .Select(
                    x => new FeedbackModel()
                    {
                        Id = x.Id,
                        CreatedBy = x.CreatedBy,
                        CreatedFor = x.CreatedFor,
                        FeedbackCategoryId = x.FeedbackCategoryId,
                        Message = x.Message,
                        statusId = x.StatusId,
                        CreatedForName = x.CreatedForNavigation.Name,
                        FeedbackCategoryName = x.FeedbackCategory.Name

                    })
                    .ToList();
                return cUserlist;
            }
        }
        public static List<FeedbackModel> FeedbacksEscalatedToMe(long user_id)
        {
            using (var db = new Entities())
            {
                var cUserlist = db.FeedbackEscalationMapping.Where(x => x.EscalatedUserId == user_id
                &&( x.Feedback.StatusId != 3|| x.Feedback.StatusId != 4))
                    .Select(
                    x => new FeedbackModel()
                    {
                        Id = x.Feedback.Id,
                        CreatedBy = x.Feedback.CreatedBy,
                        CreatedFor = x.Feedback.CreatedFor,
                        FeedbackCategoryId = x.Feedback.FeedbackCategoryId,
                        Message = x.Message,
                        CreatedForName = x.Feedback.CreatedForNavigation.Name,
                        FeedbackCategoryName = x.Feedback.FeedbackCategory.Name,
                        statusId = x.Feedback.StatusId,
                    })
                //Select(x => x.Feedback)
                    .ToList();
                return cUserlist;
            }
        }

        public static List<Feedback> GetFeedbackList(long user_id, long team_user_id)
        {
            using (var db = new Entities())
            {

                var cUserlist = db.Feedback.Where(x => x.CreatedBy == user_id && x.CreatedFor == team_user_id && (x.StatusId != 3|| x.StatusId != 4) && (DateTime.Now - x.CreatedOn).Days >= 7)
                    .ToList();

                var eUserlist = db.FeedbackEscalationMapping.Where(x => x.EscalatedUserId == user_id && (x.Feedback.StatusId != 3|| x.Feedback.StatusId != 4)&& (DateTime.Now - x.EscalatedUser.LastUpdate).Days >= 7).Select(x =>
                    x.Feedback).ToList();
                //cUserlist.AddRange(cUserlist);
                cUserlist.AddRange(eUserlist);
                return cUserlist.Distinct().ToList();
            }
        }


        public static void ReplyToFeedback(FeedbackChats replyRequest)
        {
            using (var db = new Entities())
            {
                db.FeedbackChats.Add(replyRequest);
                db.SaveChanges();
            }
        }

        public static bool IsALreadyRepliedToFeedback(long feedback_id, long user_id)
        {
            using (var db = new Entities())
            {
                return db.FeedbackChats.Any(x => x.FeedbackId == feedback_id && x.ReplyGivenBy == user_id);
            }
        }

        public static bool IsUserAccessibleForFeedbackChat(long feedback_id, long user_id)
        {
            using (var db = new Entities())
            {
                return db.Feedback.Any(x => x.CreatedFor == user_id || x.CreatedBy == user_id);
            }
        }


        public static bool IsReplyRequired(long feedback_id, long user_id)
        {
            bool isReplyRequired = false;
            using (var db = new Entities())
            {
                isReplyRequired = db.Feedback.Any(x =>x.Id==feedback_id&&( x.CreatedFor == user_id || x.CreatedBy == user_id));
                if(isReplyRequired)
                {
                    isReplyRequired = !IsALreadyRepliedToFeedback(feedback_id, user_id);
                }
            }
            return isReplyRequired;
        }

        public static List<FeedbackReplyModel> ReplyHistory(long feedback_id)
        {
            using (var db = new Entities())
            {
                return db.FeedbackChats.Where(x => x.FeedbackId == feedback_id).Select(x => new FeedbackReplyModel()
                {
                    replied_user_id = x.ReplyGivenBy,
                    reply_message = x.Reply
                }).ToList();
            }
        }


    }
}
