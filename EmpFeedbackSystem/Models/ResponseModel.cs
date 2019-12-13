
using BAL.Model;
using DAL.Models;
using System.Collections.Generic;

namespace EmpFeedbackSystem.Models
{
    public class BaseResponse
    {
        public int status_code { get; set; }

        public string status_message { get; set; }
    }

    public class VerifyOTPResp: BaseResponse
    {
        public string token { get; set; }
        public string name { get; set; }
    }

    public class FeedbackCategoryResp : BaseResponse
    {
        public List<FeedbackCategory> feedback_categories { get; set; }
    }

    public class EscalationUserResp : BaseResponse
    {
        public List<UserModel> UserList { get; set; }
    }

    public class EscalatedUserListResp : BaseResponse
    {
        public List<UserModel> UserList { get; set; }
    }

    public class FeedbackHistoryResp : BaseResponse
    {
        public bool IsReplyRequired { get; set; }
        public bool IsChatHistoryAccessible { get; set; }

        public List<FeedbackReplyModel> ReplyList { get; set; }

        public bool IsEscalationAllowed { get; set; }
        public FeedbackModel FeedbackDetails { get; set; }
        public List<FeedbackEscalationMapping> FeedbackEscalationHistory { get; set; }
    }

    public class FeedbackListResp : BaseResponse
    {
        public List<Feedback> FeedbackList { get; set; }
    }

    public class FeedbackDetailListResp : BaseResponse
    {
        public bool IsEscalationRequired { get; set; }
        public List<FeedbackModel> FeedbackCreatedByMe { get; set; }
        public List<FeedbackModel> FeedbackCreatedForMe { get; set; }
        public List<FeedbackModel> FeedbackEscalatedToMe { get; set; }
    }

   
}
