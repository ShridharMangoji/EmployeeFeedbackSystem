using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.CRUD
{
   public static class DeviceCRUD
    {
        public static long AddDeviceIfNotExist(RegisteredDevice device)
        {
            using (var db = new Entities())
            {
                db.RegisteredDevice.Attach(device);
                if (device.Id > 0)
                {
                    db.Entry(device).State = EntityState.Modified;
                    db.Entry(device).Property(x => x.RegisteredOn).IsModified = false;
                }
                else
                    db.Entry(device).State = EntityState.Added;
                db.SaveChanges();
                return device.Id;
            }
        }


        public static bool VerifyOTP(string deviceID, long userID, string otp)
        {
            using (var db = new Entities())
            {
                var result = db.RegisteredDevice.Any(x => x.DeviceId == deviceID &&
                                x.UserId == userID && x.Otp == otp);
                return result;
            }
            
        }

        public static void NulifyOTP(string deviceID, long userID, string otp)
        {
            using (var db = new Entities())
            {
                var result = db.RegisteredDevice.Where(x => x.DeviceId == deviceID &&
                                x.UserId == userID && x.Otp == otp).FirstOrDefault();
                if(result!=null)
                {
                    result.Otp = string.Empty;
                    result.LastUpdate = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }
    }
}
