using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace PushNotification
{
    class NotificationPayload
    {
        public static JProperty PayloadGenerator(string key, Dictionary<string, object> payloadValues)
        {
            var valuePayload = new JObject();
            foreach (var value in payloadValues)
            {
                valuePayload.Add( new JProperty(value.Key, value.Value));
            }
            var payload =new JProperty(key, valuePayload);
            return payload;
        }

        public static JProperty PayloadGenerator(string key, string value)
        {
            var payload = new JProperty(key, value);
            return payload;
        }

        //private static Dictionary<string, object> GetNotifyDictionary(ref Dictionary<string, object> param)
        //{
        //    Dictionary<string, object> notification = new Dictionary<string, object>();
        //    notification.Add(Constants.kTiTle, "UTI Buddy");
        //    notification.Add(Constants.kBody, GetTitleValue(ref param));
        //    notification.Add(Constants.kClickAction, "FCM_PLUGIN_ACTIVITY");
        //    notification.Add(Constants.kSound, "default");
        //    notification.Add(Constants.kIcon, "notification_icon");

        //    return notification;
        //}
    }
}
