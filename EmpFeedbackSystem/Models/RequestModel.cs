using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpFeedbackSystem.Models
{
    public class BaseRequest
    {
        public string device_id { get; set; }

        public string os_type { get; set; }
        public long user_id { get; set; }
        public string token { get; set; }
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
}
