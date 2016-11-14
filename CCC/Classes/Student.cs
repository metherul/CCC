using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC
{
    public class Student
    {
        // Stuff that isn't going to change.
        public string Name { get; set; }
        public string StudentID { get; set; }
        public string ClassPeriod { get; set; }
        public string BakeryName { get; set; }
        public string GameVersion { get; set; }
        public string GameStartTime { get; set; }
        public bool Active { get; set; }

        // Stuff that will change.
        public string CookieCount { get; set; }
        public string CookieCountAllTime { get; set; }
        public string LastSaveCode { get; set; }
        public string AccessTimeStamp { get; set; }
        public string GameSaveTime { get; set; }
        public int AccessCount { get; set; }

        // Archives
        public List<string> CookieCountArchive { get; set; }
        public List<string> CookieCountAllTimeArchive { get; set; }
        public List<string> GameSaveTimeArchive { get; set; }
        public List<string> SaveCodeArchive { get; set; }
        public List<string> AccessTimeStampArchive { get; set; }

        public Student()
        {
            CookieCountArchive = new List<string>();
            CookieCountAllTimeArchive = new List<string>();
            GameSaveTimeArchive = new List<string>();
            SaveCodeArchive = new List<string>();
            AccessTimeStampArchive = new List<string>();
        }

        /// <summary>
        /// Will initialize a new student.
        /// </summary>
        public void Initialize(SaveCode saveCode)
        {
            BakeryName = saveCode.BakeryName;
            GameVersion = saveCode.GameVersion;
            GameStartTime = saveCode.StartTime;
            Active = true;
        }

        /// <summary>
        /// Will save current variables in archives.
        /// </summary>
        public void Archive()
        {
            if (CookieCount != null)
                CookieCountArchive.Add(CookieCount);

            if (CookieCountAllTime != null)
                CookieCountAllTimeArchive.Add(CookieCountAllTime);

            if (GameSaveTime != null)
                GameSaveTimeArchive.Add(GameSaveTime);

            if (LastSaveCode != null)
                SaveCodeArchive.Add(LastSaveCode);

            if (AccessTimeStamp != null)
                AccessTimeStampArchive.Add(AccessTimeStamp);
        }

        /// <summary>
        /// Updates variables in object. Calls Archive() before running.
        /// </summary>
        /// <param name="_cookieCount"></param>
        /// <param name="_cookieCountAllTime"></param>
        /// <param name="_lastSaveCode"></param>
        /// <param name="_accessTimeStamp"></param>
        public void Update(SaveCode saveCode)
        {
            Archive();

            CookieCount = saveCode.CookieCount;
            CookieCountAllTime = saveCode.CookieCountAllTime;
            GameSaveTime = saveCode.SaveTime;
            LastSaveCode = saveCode.LastSaveCode;
            AccessTimeStamp = saveCode.AccessTimeStamp;
            AccessCount++;
        }
    }
}
