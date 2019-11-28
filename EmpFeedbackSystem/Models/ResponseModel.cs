
using BAL.Model;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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
        public Feedback FeedbackDetails { get; set; }

        public List<FeedbackEscalationMapping> FeedbackEscalationHistory { get; set; }
    }

    public class FeedbackListResp : BaseResponse
    {
        public List<Feedback> FeedbackList { get; set; }
    }
}
