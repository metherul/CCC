using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Document;

namespace CCC
{
    public class Database
    {
        IDocumentStore documentStore;
        IDocumentSession documentSession;

        string databaseURL = "http://localhost:8080/";
        string studentDatabase = "Student Information";

        public Database()
        {
            documentStore = new DocumentStore { Url = databaseURL }.Initialize();
            documentSession = documentStore.OpenSession(studentDatabase);

            Console.WriteLine("Database has been initialized");
        }

        /// <summary>
        /// Creates a new student into the database
        /// </summary>
        /// <param name="student"></param>
        public void Create(string _name, string _studentID, string _classPeriod)
        {
            var name = _name;
            var studentID = _studentID;
            var classPeriod = _classPeriod;
            Student student = new Student();

            if (Exists(name, studentID))
                return;

            student.Name = name;
            student.StudentID = studentID;
            student.ClassPeriod = classPeriod;
            student.Active = true;

            documentSession.Store(student);
            documentSession.SaveChanges();
            
        }

        /// <summary>
        /// Checks to see if a user exists in the database.
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_studentID"></param>
        /// <returns></returns>
        public bool Exists(string _name, string _studentID)
        {
            var name = _name;
            var studentID = _studentID;

            IList<Student> databaseItems = (from items in documentSession.Query<Student>()
                                            where items.Name == name
                                            where items.StudentID == studentID
                                            select items).ToList();

            if (databaseItems.Count > 0)
                return true;

            return false;
        }

        /// <summary>
        /// Initializes a Student object in the database.
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_studentID"></param>
        public void Update(string _name, string _studentID, string _saveCode)
        {
            var name = _name;
            var studentID = _studentID;

            SaveCode saveCode = SaveCode.Decode(_saveCode);

            IList<Student> databaseItems = (from items in documentSession.Query<Student>()
                                            where items.Name == name
                                            where items.StudentID == studentID
                                            select items).ToList();

            foreach (Student item in databaseItems)
            {
                item.Initialize(saveCode);
                item.Update(saveCode);
            }

            documentSession.SaveChanges();
        }
    }
}
