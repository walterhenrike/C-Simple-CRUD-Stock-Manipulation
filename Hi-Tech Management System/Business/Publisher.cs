using System;

namespace Hi_Tech_Management_System.Business
{
    [Serializable]
    public class Publisher
    {
        private string publisherName;
        private int publishID;
        private string publisherEmail;

        public Publisher(string publisherName, int publishID, string publisherEmail)
        {
            this.publisherName = publisherName;
            this.publishID = publishID;
            this.publisherEmail = publisherEmail;
        }

        public Publisher()
        {
            this.publisherEmail = "Default";
            this.publishID = 0;
            this.publisherName = "Default";
        }

        public string PublisherName
        {
            get
            {
                return publisherName;
            }

            set
            {
                publisherName = value;
            }
        }

        public int PublishID
        {
            get
            {
                return publishID;
            }

            set
            {
                publishID = value;
            }
        }

        public string PublisherEmail
        {
            get
            {
                return publisherEmail;
            }

            set
            {
                publisherEmail = value;
            }
        }
    }
}
