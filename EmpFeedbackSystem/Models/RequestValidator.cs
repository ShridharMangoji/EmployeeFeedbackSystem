using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpFeedbackSystem.Models
{
    public class RequestValidator
    {
        public static bool GenerateOTP(GenerateOTPReq req)
        {
            if (req == null || string.IsNullOrEmpty(req.device_id) || 
                string.IsNullOrEmpty(req.os_type) || 
                req.user_id>0)
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
                req.user_id > 0)
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
    }
}
