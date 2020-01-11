//using PushNotification.DB;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PushNotification
{
    public class HTTPRequester
    {
        public static void PostDataAsync(string inputData, string apiKey)
        {
                try
                {
                    string gcmURL = "https://fcm.googleapis.com/fcm/send";
                    HttpWebRequest request = HttpWebRequest.Create(gcmURL) as HttpWebRequest;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                       SecurityProtocolType.Tls11 |
                                       SecurityProtocolType.Tls12;
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.Headers.Add(string.Format("Authorization: key={0}", apiKey));

                    byte[] uploadDataByte = System.Text.Encoding.UTF8.GetBytes(inputData);
                    using (Stream inpuStream = request.GetRequestStream())
                    {
                        inpuStream.Write(uploadDataByte, 0, uploadDataByte.Length);
                    }

                    using (WebResponse response = request.GetResponse())
                    {
                        using (Stream outputStream = response.GetResponseStream())
                        {
                           
                        }
                    }
                }
                catch (Exception e)
                {
                   
                }
        }

    }
}
