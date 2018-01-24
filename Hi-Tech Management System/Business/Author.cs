using System;

namespace Hi_Tech_Management_System.Business
{
    [Serializable]
    public class Author
    {
        public int AuthorID
        {
            get;
            set;
        }
        
        public string AuthorEmail
        {
            get;
            set;
        }

        public string AuthorFirstName
        {
            get;
            set;
        }

        public string AuthorLastName
        {
            get;
            set;
        }



        public Author()
        {
            AuthorID = 0;
            AuthorFirstName = "Default";
            AuthorLastName = "Default";
            AuthorEmail = "Default";
        }

        public Author(int authorID, string authorFirstName, string authorLastName, string authorEmail)
        {
            AuthorID = authorID;
            AuthorFirstName = authorFirstName;
            AuthorLastName = authorLastName;
            AuthorEmail = authorEmail;
        }
    }
}
