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
                req.user_id <= 0||req.otp<=0)
            {
                return false;
            }
            else
                return true;
        }

        internal static bool UserEscalationList(BaseRequest req)
        {
            if (req == null || string.IsNullOrEmpty(req.device_id) ||
                string.IsNullOrEmpty(req.os_type) ||
                req.user_id <= 0 )
            {
                return false;
            }
            else
                return true;
        }

        internal static bool GetFeedbackCategories(BaseRequest req)
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

        internal static bool FeedbackHistory(FeedbackHistoryReq req)
        {
            if (req == null || string.IsNullOrEmpty(req.device_id) ||
                string.IsNullOrEmpty(req.os_type) ||
                req.user_id <= 0 )
            {
                return false;
            }
            else
                return true;
        }

        internal static bool UpdateFeedback(UpdateFeedbackReq req)
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

        internal static bool TeamList(BaseRequest req)
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

        internal static bool EscalatedUserList(BaseRequest req)
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

        internal static bool FeedbackList(FeedbackListReq req)
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

        internal static bool FeedbackEscalationTeam(FeedbackEscalationTeamReq req)
        {
            if (req == null || string.IsNullOrEmpty(req.device_id) ||
               string.IsNullOrEmpty(req.os_type) ||
               req.user_id <= 0 )
            {
                return false;
            }
            else
                return true;
        }

        internal static bool ReplyToFeedback(ReplyReq req)
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
    }
}
