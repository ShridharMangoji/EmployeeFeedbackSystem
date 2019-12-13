namespace EmpFeedbackSystem.Models
{
    public class RequestValidator
    {
        public static bool GenerateOTP(GenerateOTPReq req)
        {
            if (req == null || string.IsNullOrEmpty(req.device_id) || 
                string.IsNullOrEmpty(req.os_type) || 
                req.user_id<=0)
            {
                return false;
            }
            else
                return true;
        }

        internal static bool VerifyOTP(VerifyOTPReq req)
        {
            if (req == null || string.IsNullOrEmpty(req.device_id) ||
                string.IsNullOrEmpty(req.os_type) ||
                req.user_id <= 0)
            {
                return false;
            }
            else
                return true;
        }

        internal static bool UserEscalationList(BaseRequest req)
        {
            return true;
        }

        internal static bool GetFeedbackCategories(BaseRequest req)
        {
            return true;
        }

        internal static bool FeedbackHistory(FeedbackHistoryReq req)
        {
            return true;
        }

        internal static bool UpdateFeedback(UpdateFeedbackReq req)
        {
            return true;
        }

        internal static bool TeamList(BaseRequest req)
        {
            return true;
        }

        internal static bool EscalatedUserList(BaseRequest req)
        {
            return true;
        }

        internal static bool FeedbackList(FeedbackListReq req)
        {
            return true;
        }

        internal static bool FeedbackEscalationTeam(FeedbackEscalationTeamReq req)
        {
            return true;
        }

        internal static bool ReplyToFeedback(ReplyReq req)
        {
            return true;
        }
    }
}
