using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class RegisteredDevice
    {
        public long Id { get; set; }
        public string DeviceId { get; set; }
        public long UserId { get; set; }
        public string OsType { get; set; }
        public string Otp { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime LastUpdate { get; set; }
        public string FcmToken { get; set; }
        
        public virtual Users User { get; set; }
    }
}
