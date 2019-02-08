using Cosapi.ElCosapino.UI.Admin.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Cosapi.ElCosapino.UI.Admin.Util
{
    public class FirebaseLogic
    {
        private String _ServerApiKey { get; set; }
        private String _SenderId { get; set; }

        public FirebaseLogic()
        {
            _ServerApiKey = AppSettings.Get("FCMApiKey");
            _SenderId = AppSettings.Get("FCMSenderId");
        }

        public FirebaseLogic(string serverApiKey, string senderId)
        {
            _ServerApiKey = serverApiKey;
            _SenderId = senderId;
        }

        public string SendNotification(string message, string deviceId)
        {
            var value = message;
            var webRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            webRequest.Method = "post";
            webRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
            webRequest.Headers.Add(string.Format("Authorization: key={0}", _ServerApiKey));
            webRequest.Headers.Add(string.Format("Sender: id={0}", _SenderId));

            var postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "";
            Console.WriteLine(postData);
            var byteArray = Encoding.UTF8.GetBytes(postData);
            webRequest.ContentLength = byteArray.Length;

            var dataStream = webRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var webResponse = webRequest.GetResponse();

            dataStream = webResponse.GetResponseStream();

            var streamReader = new StreamReader(dataStream);

            var responseFromServer = streamReader.ReadToEnd();

            streamReader.Close();
            dataStream.Close();
            webResponse.Close();
            return responseFromServer;
        }

        public FCMResponse EnviarNotificacionMBG(List<String> deviceTokens, String title, String body, String dataKey, String dataValue)
        {
            FCMResponse response = new FCMResponse();
            if (deviceTokens.Count == 0)
            {
                response.success = "0";
                return response;
            }

            try
            {
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    registration_ids = deviceTokens,
                    notification = new
                    {
                        title = title,
                        body = body,
                        dataKey = dataKey,
                        dataValue = dataValue
                    }
                };
                string jsonNotificationFormat = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", _ServerApiKey));
                tRequest.Headers.Add(string.Format("Sender: id={0}", _SenderId));
                tRequest.ContentLength = byteArray.Length;
                tRequest.ContentType = "application/json";
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String responseFromFirebaseServer = tReader.ReadToEnd();
                                response = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(responseFromFirebaseServer);
                                return response;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FCMResponse SendNotification(List<string> deviceTokens, string titulo, string mensaje, string dataKey, string dataValue)
        {
            var data = new
            {
                registration_ids = deviceTokens,
                notification = new
                {
                    title = titulo,
                    body = mensaje,
                    dataKey = dataKey,
                    dataValue = dataValue
                }
            };

            var jsonData = JsonConvert.SerializeObject(data);
            var byteArray = Encoding.UTF8.GetBytes(jsonData);

            var webRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            webRequest.Method = "post";
            webRequest.ContentType = " application/json";
            webRequest.ContentLength = byteArray.Length;

            webRequest.Headers.Add(string.Format("Authorization: key={0}", _ServerApiKey));
            webRequest.Headers.Add(string.Format("Sender: id={0}", _SenderId));

            var dataStreamResponse = webRequest.GetRequestStream();
            dataStreamResponse.Write(byteArray, 0, byteArray.Length);
            dataStreamResponse.Close();

            var webResponse = webRequest.GetResponse();

            dataStreamResponse = webResponse.GetResponseStream();

            var streamReader = new StreamReader(dataStreamResponse);
            var responseFromServer = streamReader.ReadToEnd();
            var response = JsonConvert.DeserializeObject<FCMResponse>(responseFromServer);

            streamReader.Close();
            dataStreamResponse.Close();
            webResponse.Close();


            return response;
        }

        public FCMResponse SendNotification(string deviceId, string titulo, string mensaje)
        {
            try
            {
                var data = new
                {
                    registration_ids = deviceId,
                    notification = new
                    {
                        title = titulo,
                        body = mensaje
                    }
                };

                var jsonData = JsonConvert.SerializeObject(data);
                var byteArray = Encoding.UTF8.GetBytes(jsonData);

                var webRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                webRequest.Method = "post";
                webRequest.ContentType = " application/json";
                webRequest.ContentLength = byteArray.Length;

                webRequest.Headers.Add(string.Format("Authorization: key={0}", _ServerApiKey));
                webRequest.Headers.Add(string.Format("Sender: id={0}", _SenderId));

                var dataStreamResponse = webRequest.GetRequestStream();
                dataStreamResponse.Write(byteArray, 0, byteArray.Length);
                dataStreamResponse.Close();

                var webResponse = webRequest.GetResponse();

                dataStreamResponse = webResponse.GetResponseStream();

                var streamReader = new StreamReader(dataStreamResponse);
                var responseFromServer = streamReader.ReadToEnd();
                var response = JsonConvert.DeserializeObject<FCMResponse>(responseFromServer);

                streamReader.Close();
                dataStreamResponse.Close();
                webResponse.Close();


                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

    public class FCMResponse
    {
        public string multicast_id { get; set; }
        public string success { get; set; }
        public string failure { get; set; }
        public string canonical_id { get; set; }
    }
}