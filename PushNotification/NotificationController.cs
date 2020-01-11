using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PushNotification
{
    public class NotificationController
    {
        public static void NotifyDevice(int type, int feedbackId, string message, List<string> regIds)
        {
            Task.Factory.StartNew(() =>
            {
                var notificationPayload = GetNotifyDictionary(message);
                var messagePayload = GetMessageDictionary(type, feedbackId);
                new AndroidNotification().SendPushNotification(notificationPayload, messagePayload, regIds);
            });
        }

        private static Dictionary<string, object> GetNotifyDictionary(string message)
        {
            Dictionary<string, object> notification = new Dictionary<string, object>();
            notification.Add(Constants.kTiTle, "mFeed");
            notification.Add(Constants.kBody, message);
            notification.Add(Constants.kClickAction, "FCM_PLUGIN_ACTIVITY");
            notification.Add(Constants.kSound, "default");
            notification.Add(Constants.kIcon, "notification_icon");

            return notification;
        }

        private static Dictionary<string, object> GetMessageDictionary(int type, int id)
        {
            Dictionary<string, object> notification = new Dictionary<string, object>();
            notification.Add(Constants.kType, type);
            notification.Add(Constants.kId, id);
            return notification;
        }
    }
}
