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
        string studentDatabase = "Students";

        Database()
        {
            documentStore = new DocumentStore { Url = databaseURL }.Initialize();
            documentSession = documentStore.OpenSession(studentDatabase);

            Console.WriteLine("Database has been initialized");
        }

        /// <summary>
        /// Inserts a new student into the database
        /// </summary>
        /// <param name="student"></param>
        void Insert(Student student)
        {

        }

        /// <summary>
        /// Will update user's current info in the database.
        /// </summary>
        /// <param name="_name">Student name.</param>
        /// <param name="_studentID">Student ID.</param>
        /// <param name="_saveCode">Cookie Clicker save code.</param>
        void Update(string _name, string _studentID, string _saveCode)
        {
            var name = _name;
            var studentID = _studentID;
            var saveCode = _saveCode;
            var saveObject = SaveCode.Decode(saveCode);

            IList<Student> databaseItems = (from items in documentSession.Query<Student>()
                                            where items.Name == name
                                            where items.StudentID == studentID
                                            select items).ToList();

            foreach (Student item in databaseItems)
                item.Update(saveObject);

            documentSession.SaveChanges();
        }
    }
}
