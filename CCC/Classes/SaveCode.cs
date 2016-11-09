using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC
{
    public class SaveCode
    {
        public string GameVersion { get; set; }
        public string StartTime { get; set; }
        public string BakeryName { get; set; }
        public string CookieCount { get; set; }
        public string CookieCountAllTime { get; set; }
        public string LastSaveCode { get; set; }
        public string AccessTimeStamp { get; set; }
        public string SaveTime { get; set; }

        /// <summary>
        /// Will decode a Cookie Clicker save code and return a SaveCode object.
        /// </summary>
        /// <param name="_saveCode"></param>
        /// <returns></returns>
        static public SaveCode Decode(string _saveCode)
        {
            var saveCode = _saveCode;
            var decodedCode = saveCode.Replace("%2521END%2521", "").Replace("%253D", "=").Replace("%2F", @"/");
            var saveObject = new SaveCode();
            string temp;
            string gameVersion;
            string startTime;
            string saveTime;
            string bakeryName;
            string cookieCount;
            string cookieCountAllTime;
            string[] decodedArray = Encoding.UTF8.GetString(Convert.FromBase64String(decodedCode)).Split(';');

            // Yum.
            // First off we're going to get the Game Version.
            temp = decodedArray[0];
             gameVersion = temp.Substring(0, temp.IndexOf("|"));
            Console.WriteLine("GAME VERSION: " + gameVersion);

            // Starting time.
            startTime = temp.Substring(temp.IndexOf("|") + 2);
            startTime = FromUnixTime(Convert.ToDouble(startTime));
            Console.WriteLine("START TIME: " + startTime);

            // Save time.
            temp = decodedArray[2];
            saveTime = FromUnixTime(Convert.ToDouble(temp));
            Console.WriteLine("SAVE TIME: " + saveTime);

            // Bakery name.
            temp = decodedArray[3];
            bakeryName = temp.Substring(0, temp.IndexOf("|")) + "'s bakery";
            Console.WriteLine("BAKERY NAME: " + bakeryName);

            // Cookie count.
            temp = decodedArray[3];
            cookieCount = temp.Substring(temp.IndexOf("|") + 1);
            cookieCount = cookieCount.Substring(cookieCount.IndexOf("|") + 1);

            if (!cookieCount.Contains("+"))
            {
                cookieCount = Math.Round(Convert.ToDecimal(cookieCount)).ToString();
            }

            Console.WriteLine("COOKIE COUNT: " + cookieCount);

            // Cookie count of all time.
            temp = decodedArray[4];
            cookieCountAllTime = temp.Substring(temp.IndexOf("|") + 1);

            if (!cookieCountAllTime.Contains("+"))
            {
                cookieCountAllTime = Math.Round(Convert.ToDecimal(cookieCountAllTime)).ToString();
            }

            // Serialize into a SaveCode object.
            saveObject.GameVersion = gameVersion;
            saveObject.StartTime = startTime;
            saveObject.BakeryName = bakeryName;
            saveObject.CookieCount = cookieCount;
            saveObject.CookieCountAllTime = cookieCountAllTime;
            saveObject.LastSaveCode = saveCode;
            saveObject.AccessTimeStamp = DateTime.Now.ToString();
            saveObject.SaveTime = saveTime;

            return saveObject;
        }

        /// <summary>
        /// Converts inputted Unix time to something understandable.
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        static string FromUnixTime(double unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();

            return dtDateTime.ToString();
        }
    }
}
