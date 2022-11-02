using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LazZiya.ImageResize;
using Homemade.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using Homemade.BLL.ViewModel.Driver;
using System.Device.Location;
namespace Homemade.BLL.General
{
    public class BLGeneral : BaseEntity
    {
        #region Declaretion
        private readonly IConfiguration _configuration;
        public BLGeneral(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion
        #region Hash
        public string KeyGeneratorNumbersOnly(int length)
        {
            int maxSize = length;
            char[] chars = new char[62];
            string a;
            a = "1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString();
        }
        #endregion   
        #region DatetimeNow
        public DateTime GetDateTimeNow()
        {
            var RESdate = DateTime.Now;
            return RESdate;
        }

        public DateTime GetDateTimeNow_UTC()
        {
            var RESdate = DateTime.Now;
            return RESdate;
        }

        #endregion   
        #region Rondom
        public string RandomNumber(int length)
        {
            const string chars = "0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string GenerateToken(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string GeneratePassword(int length)
        {
            return "123456";
        }
        #endregion
        #region Encode
        public string EncodeServerName(string serverName)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        }

        public string DecodeServerName(string encodedorderid)
        {
            var id = "0";
            try
            {
                id = Encoding.UTF8.GetString(Convert.FromBase64String(encodedorderid));
            }
            catch (Exception ex)
            {

                var ss = ex.Message;
            }
            return id;
        }
        #endregion

        public decimal CalcProductNet(decimal Discount, DateTime? StartDiscountDate, DateTime? EndDiscountDate, decimal Price)
        {
            return (Discount > 0 ? (StartDiscountDate <= DateTime.Now && EndDiscountDate >= DateTime.Now ? Math.Round(((Price * Discount) / 100), 2) : 0) : 0);
        }
        public double GetDistance(double F_Latitude, double F_Longitude, double S_Latitude, double S_Longitude)
        {
            var fCoord = new GeoCoordinate(F_Latitude, F_Longitude);
            var sCoord = new GeoCoordinate(S_Latitude, S_Longitude);
            var Km = fCoord.GetDistanceTo(sCoord);
            var dada = (Km / 1000);
            return (Km / 1000);
        }
        public bool IsEnglish(string inputstring)
        {
            var content = Regex.Replace(inputstring.TrimEnd().TrimStart(), @"\s+", " ");
            Regex regex = new Regex(@"[A-Za-z0-9 .,-=+(){}\[\]\\]");
            MatchCollection matches = regex.Matches(content);

            if (matches.Count.Equals(content.Length))
                return true;
            else
                return false;
        }
        #region SaveImage
        public void SaveImage(SaveImageModel saveImageModel)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(saveImageModel.base64);

                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                    // here because object dispose before saving so we should let it here
                    image.Scale(800, 600).SaveAs(saveImageModel.mainPath + saveImageModel.fileName);
                }
            }
            catch (Exception EX)
            {
            }
        }
        public string SaveImageIFromFile(SaveImageFormFileModel saveImageModel)
        {
            try
            {
                string ImagePath = saveImageModel.mainPath;
                if (saveImageModel.file != null)
                {
                    if (saveImageModel.file.Length > 0)
                    {
                        var twmOps = new TextWatermarkOptions
                        {
                            Location = TargetSpot.TopRight,

                            FontSize = 30,

                            FontStyle = FontStyle.Bold,

                            // Don't draw text
                            TextColor = Color.FromArgb(0, Color.White),

                            // Draw outline only, alpha (0 - 255)
                            OutlineColor = Color.FromArgb(125, Color.OrangeRed),

                            OutlineWidth = 1
                        };
                        var twmOps2 = new TextWatermarkOptions
                        {
                            Location = TargetSpot.BottomLeft,

                            FontSize = 30,

                            FontStyle = FontStyle.Bold,

                            // Don't draw text
                            TextColor = Color.FromArgb(0, Color.White),

                            // Draw outline only, alpha (0 - 255)
                            OutlineColor = Color.FromArgb(125, Color.OrangeRed),

                            OutlineWidth = 1
                        };
                        var iwmOps = new ImageWatermarkOptions
                        {
                            Location = TargetSpot.Center,

                            // 0 full transparent, 100 full color
                            Opacity = 20,

                        };


                        var fleftSidePicturefullPath = Path.Combine(ImagePath, saveImageModel.filename);


                        if (saveImageModel.file.Length > 0)
                        {
                            using (var stream = saveImageModel.file.OpenReadStream())
                            {
                                using (var img = Image.FromStream(stream))
                                {
                                    img.Scale(800, 600)
                              .AddTextWatermark(string.Concat("Homemade - شغل بيت"), twmOps)
                              .AddTextWatermark(" https://made-home.com", twmOps2)
                              .SaveAs(fleftSidePicturefullPath);
                                }
                            }
                        }

                        return saveImageModel.filename;

                    }
                    else
                    {
                        return "false";
                    }
                }
                else
                {
                    return "false";
                }
            }
            catch (Exception EX)
            {
                return "false";
            }
        }
        public string SaveIFromFile(SaveImageFormFileModel saveImageModel)
        {
            try
            {

                if (saveImageModel.file != null)
                {
                    if (saveImageModel.file.Length > 0)
                    {
                        using (var stream = saveImageModel.file.OpenReadStream())
                        {
                            using (var img = Image.FromStream(stream))
                            {
                                img.Scale(800, 600).SaveAs(saveImageModel.mainPath);
                            }
                        }
                        return saveImageModel.filename;
                    }
                    else
                    {
                        return "false";
                    }
                }
                else
                {
                    return "false";
                }
            }
            catch (Exception EX)
            {
                return "false";
            }
        }

        public void SaveImageWithStream(SaveImageModelStream saveImageModel)
        {
            Image image = Image.FromStream(saveImageModel.Stream);
            image.Scale(800, 600).SaveAs(saveImageModel.mainPath + saveImageModel.fileName);
        }

        public class SaveImageModel
        {
            public string base64 { get; set; }
            public string key { get; set; }
            public string mainPath { get; set; }
            public string fileName { get; set; }
        }
        public class SaveImageFormFileModel
        {
            public IFormFile file { get; set; }
            public string filename { get; set; }
            public string key { get; set; }
            public string mainPath { get; set; }
        }

        public class SaveImageModelStream : SaveImageModel
        {
            public Stream Stream { get; set; }

        }
        #endregion
        public string SendPush(string DeviceToken, int DeviceType, PushList _PushList)
        {
            try
            {
                string postData = "";
                if (!string.IsNullOrWhiteSpace(DeviceToken))
                {
                    if (DeviceType == (int)DeviceTypeEnum.IOS) // IOS
                    {
                        postData =
                        "{ \"registration_ids\": [ \"" + DeviceToken + "\" ], " +
                            "\"data\":" +
                                    "{" +
                                     "\"orderId\": \"" + _PushList.orderId + "\"," +
                                     "\"lng\": \"" + _PushList.lng + "\"," +
                                     "\"lat\":\"" + _PushList.lat + "\", " +
                                     "\"cusotmerAddress\": \"" + _PushList.cusotmerAddress + "\"," +
                                     "\"estimation\": \"" + _PushList.estimation + "\"," +
                                     "\"vendorName\": \"" + _PushList.vendorName + "\"," +
                                     "\"vendorLogo\": \"" + _PushList.vendorLogo + "\"," +
                                     "\"vendorAddress\": \"" + _PushList.vendorAddress + "\"," +
                                     "\"trackNo\": \"" + _PushList.trackNo + "\"," +
                                     "\"profit\": \"" + _PushList.profit + "\"," +
                                     "\"pushType\": \"" + _PushList.pushType + "\"," +
                                     "\"content_available\": \"" + _PushList.content_available + "\"," +
                                   "}" +
                              "\"priority\": \"" + _PushList.priority + "\"," +
                             "\"notification\":" +
                              "{" +
                               "\"title\": \"" + _PushList.title + "\"," +
                               "\"body\": \"" + _PushList.body + "\"," +
                               "\"sound\": \"" + _PushList.sound + "\"," +
                             "}" +
                         "}";
                    }
                    else if (DeviceType == (int)DeviceTypeEnum.Andriod) // Android
                    {
                        postData =
                        "{ \"registration_ids\": [ \"" + DeviceToken + "\" ], " +
                            "\"data\":" +
                                    "{" +
                                    "\"orderId\": \"" + _PushList.orderId + "\"," +
                                     "\"lng\": \"" + _PushList.lng + "\"," +
                                     "\"lat\":\"" + _PushList.lat + "\", " +
                                     "\"cusotmerAddress\": \"" + _PushList.cusotmerAddress + "\"," +
                                     "\"estimation\": \"" + _PushList.estimation + "\"," +
                                     "\"vendorName\": \"" + _PushList.vendorName + "\"," +
                                     "\"vendorLogo\": \"" + _PushList.vendorLogo + "\"," +
                                     "\"vendorAddress\": \"" + _PushList.vendorAddress + "\"," +
                                     "\"trackNo\": \"" + _PushList.trackNo + "\"," +
                                     "\"profit\": \"" + _PushList.profit + "\"," +
                                     "\"pushType\": \"" + _PushList.pushType + "\"," +
                                     "\"content_available\": \"" + _PushList.content_available + "\"," +
                                   "}"
                                   +
                                "\"priority\": \"" + _PushList.priority + "\"," +
                                "\"notification\":" +
                              "{" +
                               "\"title\": \"" + _PushList.title + "\"," +
                               "\"body\": \"" + _PushList.body + "\"," +
                               "\"sound\": \"" + _PushList.sound + "\"," +
                             "}" +
                         "}";
                    }
                }
                var response_ = PushNotification(postData, _PushList.serverKey);
                return response_;
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
        }
        public bool SendPush(string DeviceToken, int DeviceType, List<PushList> PushList)
        {
            try
            {
                foreach (var _PushList in PushList)
                {
                    string postData = "";
                    if (!string.IsNullOrWhiteSpace(DeviceToken))
                    {
                        if (DeviceType == (int)DeviceTypeEnum.IOS) // IOS
                        {
                            postData =
                            "{ \"registration_ids\": [ \"" + DeviceToken + "\" ], " +
                                "\"data\":" +
                                        "{" +
                                         "\"orderId\": \"" + _PushList.orderId + "\"," +
                                         "\"lng\": \"" + _PushList.lng + "\"," +
                                         "\"lat\":\"" + _PushList.lat + "\", " +
                                         "\"cusotmerAddress\": \"" + _PushList.cusotmerAddress + "\"," +
                                         "\"estimation\": \"" + _PushList.estimation + "\"," +
                                         "\"vendorName\": \"" + _PushList.vendorName + "\"," +
                                         "\"vendorLogo\": \"" + _PushList.vendorLogo + "\"," +
                                         "\"vendorAddress\": \"" + _PushList.vendorAddress + "\"," +
                                         "\"trackNo\": \"" + _PushList.trackNo + "\"," +
                                         "\"profit\": \"" + _PushList.profit + "\"," +
                                         "\"pushType\": \"" + _PushList.pushType + "\"," +
                                         "\"content_available\": \"" + _PushList.content_available + "\"," +
                                       "}" +
                                  "\"priority\": \"" + _PushList.priority + "\"," +
                                 "\"notification\":" +
                                  "{" +
                                   "\"title\": \"" + _PushList.title + "\"," +
                                   "\"body\": \"" + _PushList.body + "\"," +
                                   "\"sound\": \"" + _PushList.sound + "\"," +
                                 "}" +
                             "}";
                        }
                        else if (DeviceType == (int)DeviceTypeEnum.Andriod) // Android
                        {
                            postData =
                            "{ \"registration_ids\": [ \"" + DeviceToken + "\" ], " +
                                "\"data\":" +
                                        "{" +
                                        "\"orderId\": \"" + _PushList.orderId + "\"," +
                                         "\"lng\": \"" + _PushList.lng + "\"," +
                                         "\"lat\":\"" + _PushList.lat + "\", " +
                                         "\"cusotmerAddress\": \"" + _PushList.cusotmerAddress + "\"," +
                                         "\"estimation\": \"" + _PushList.estimation + "\"," +
                                         "\"vendorName\": \"" + _PushList.vendorName + "\"," +
                                         "\"vendorLogo\": \"" + _PushList.vendorLogo + "\"," +
                                         "\"vendorAddress\": \"" + _PushList.vendorAddress + "\"," +
                                         "\"trackNo\": \"" + _PushList.trackNo + "\"," +
                                         "\"profit\": \"" + _PushList.profit + "\"," +
                                         "\"pushType\": \"" + _PushList.pushType + "\"," +
                                         "\"content_available\": \"" + _PushList.content_available + "\"," +
                                       "}"
                                       +
                                    "\"priority\": \"" + _PushList.priority + "\"," +
                                    "\"notification\":" +
                                  "{" +
                                   "\"title\": \"" + _PushList.title + "\"," +
                                   "\"body\": \"" + _PushList.body + "\"," +
                                   "\"sound\": \"" + _PushList.sound + "\"," +
                                 "}" +
                             "}";
                        }
                    }
                    var response_ = PushNotification(postData, _PushList.serverKey);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string PushNotification(string json, string serverKey)
        {
            try
            {
                var result = "-1";
                var webAddr = "https://fcm.googleapis.com/fcm/send";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization:key=" + serverKey);
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
        }
        public void SendEmail(string email, string subject, string htmlMessage)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {
                var basicCredential = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"]);
                using (MailMessage message = new MailMessage())
                {
                    MailAddress fromAddress = new MailAddress(_configuration["Email:From"], "Home Made Accounts");
                    smtpClient.Port = Convert.ToInt32(_configuration["Email:Port"]); //465;
                    smtpClient.Host = _configuration["Email:Host"]; // "smtp.yandex.com";
                    smtpClient.UseDefaultCredentials = _configuration["Email:UseDefaultCredentials"] != null ? Convert.ToBoolean(_configuration["Email:UseDefaultCredentials"]) : false;
                    smtpClient.Credentials = basicCredential;
                    smtpClient.EnableSsl = _configuration["Email:EnableSsl"] != null ? Convert.ToBoolean(_configuration["Email:EnableSsl"]) : true;
                    message.From = fromAddress;
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    message.Body = htmlMessage;
                    message.To.Add(email);
                    try
                    {
                        smtpClient.Send(message);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }
        public class RootShorten
        {
            public string long_url { get; set; }
            public string domain { get; set; }
            public string group_guid { get; set; }
        }

        // OK
        public class References
        {
            public string group { get; set; }
        }

        public class RootShortenResponce
        {
            public DateTime created_at { get; set; }
            public string id { get; set; }
            public string link { get; set; }
            public List<object> custom_bitlinks { get; set; }
            public string long_url { get; set; }
            public bool archived { get; set; }
            public List<object> tags { get; set; }
            public List<object> deeplinks { get; set; }
            public References references { get; set; }
        }

        public class Error
        {
            public string field { get; set; }
            public string error_code { get; set; }
        }

        public class RootBadRequest
        {
            public string message { get; set; }
            public string resource { get; set; }
            public string description { get; set; }
            public List<Error> errors { get; set; }
        }


        public class RootFORBIDDEN
        {
            public string message { get; set; }
            public string resource { get; set; }
            public string description { get; set; }
        }



        public class ShortenLinkResponce
        {
            public bool Status { get; set; }
            public string Message { get; set; }
            public string Link { get; set; }
        }
    }
}
