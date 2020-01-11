using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
                    db.Entry(device).Property(x => x.Otp).IsModified = false;
                }
                else
                {
                    device.RegisteredOn = DateTime.Now;
                    db.Entry(device).State = EntityState.Added;
                }
                device.LastUpdate = DateTime.Now;
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
                if (result != null)
                {
                    result.Otp = string.Empty;
                    result.LastUpdate = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }

        public static RegisteredDevice GetDevice(string device_id,long userId, string osType)
        {
            using (var db = new Entities())
            {
                var result = db.RegisteredDevice.Where(x => x.DeviceId == device_id&&x.UserId==userId&&x.OsType==osType).Include(x => x.User).FirstOrDefault();
                return result;
            }
        }

        //public static RegisteredDevice GetDevice(string device_id, long user_id, string os_type)
        //{
        //    using (var db = new Entities())
        //    {
        //        var result = db.RegisteredDevice.Where(x => x.DeviceId == device_id
        //        && x.UserId == user_id && x.OsType == os_type).FirstOrDefault();

        //        return result;
        //    }
        //}

        public static void UpdateFcmToken(string fcmToken, string device_id, long user_id, string os_type)
        {
            using (var db = new Entities())
            {
                var result = db.RegisteredDevice.Where(x => x.DeviceId == device_id &&
                          x.OsType == os_type && x.UserId == user_id).FirstOrDefault();
                if (result != null)
                {
                    result.FcmToken = fcmToken;
                    result.LastUpdate = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }
    }
}
