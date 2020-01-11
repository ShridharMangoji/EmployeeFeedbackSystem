using DAL.Models;

namespace EmpFeedbackSystem.Models
{
    public class BaseRequest
    {
        public string device_id { get; set; }

        public string os_type { get; set; }
        public long user_id { get; set; }
        public string token { get; set; }
        public long feedback_id { get; set; }

        public string fcm_token { get; set; }
    }


    public class GenerateOTPReq:BaseRequest
    {
        public new long user_id { get; set; }
     
    }

    public class VerifyOTPReq : BaseRequest
    {

        public short otp { get; set; }
    }

    public class FeedbackHistoryReq : BaseRequest
    {
        public long feedback_id { get; set; }

    }

    public class FeedbackEscalationTeamReq : BaseRequest
    {
        public long feedback_id { get; set; }

    }

    public class FeedbackListReq : BaseRequest
    {
        public long escalated_user_id { get; set; }

    }

    public class UpdateFeedbackReq : BaseRequest
    {
        public long feedback_id { get; set; }

        public Feedback feedback_info { get; set; }
    }

    public class ReplyReq : BaseRequest
    {
        public string reply { get; set; }

    }

}
