using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace PushNotification
{
    public class AndroidNotification
    {
        public void SendPushNotification(Dictionary<string, object> notification, Dictionary<string, object> data, List<string> registrationIds)
        {
            int count = registrationIds.Count();
            if (count > 0)
            {
                int start = 0;
                int length = Constants.MAX_DEVICE_ALLOWED;
                do
                {
                    List<string> regIds = registrationIds.Skip(start).Take(length).ToList();
                    if (regIds != null && regIds.Count() > 0)
                    {
                        var strRegIds = string.Join(',', regIds);
                        var toRegIds = NotificationPayload.PayloadGenerator("to", strRegIds);
                        var notificationPayload = NotificationPayload.PayloadGenerator("notification", notification);
                        var messagePayload = NotificationPayload.PayloadGenerator("data", data);

                        string str = new JObject(toRegIds, notificationPayload, messagePayload).ToString(Newtonsoft.Json.Formatting.None);
                        HTTPRequester.PostDataAsync(str, Constants.PushNotificationApiKey);
                    }

                    start += length;
                } while (count >= start);
            }
        }
    }
}
    
