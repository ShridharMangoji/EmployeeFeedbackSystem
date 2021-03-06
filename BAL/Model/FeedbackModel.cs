﻿using DAL.Models;
using System;

namespace BAL.Model
{
    public class FeedbackModel
    {
        public long Id { get; set; }
        public string Subject { get; set; }
        public long statusId { get; set; }
        public string Message { get; set; }
        public long CreatedBy { get; set; }
        public long CreatedFor { get; set; }
        public long FeedbackCategoryId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string StrCreatedOn { get; set; }
        public string CreatedForName { get; set; }
        public string FeedbackCategoryName { get; set; }

        public string EscalatedUserName { get; set; }
    }

    public class FeedbackReplyModel
    {
        public string reply_message { get; set; }
        public long replied_user_id { get; set; }

    }

    public class FeedbackEscalationModel: FeedbackEscalationMapping
    {
        public string feedback_escalated_username { get; set; }

    }

}
