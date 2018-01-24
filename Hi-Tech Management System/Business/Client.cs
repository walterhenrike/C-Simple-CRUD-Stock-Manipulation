using System;

namespace Hi_Tech_Management_System.Business
{
    [Serializable]
    public class Client
    {
        private int clientId;
        private string clientName;
        private string clientStreet;
        private string clientEmail;
        private string clientCity;
        private string clientPostCode;
        private string clientPhone;
        private string clientFax;
        private decimal clientCredit;

        public Client(int clientId, string clientName, string clientStreet, string clientEmail, string clientCity, string clientPostCode, string clientPhone, string clientFax, decimal clientCredit)
        {
            this.clientId = clientId;
            this.clientName = clientName;
            this.clientStreet = clientStreet;
            this.clientEmail = clientEmail;
            this.clientCity = clientCity;
            this.clientPostCode = clientPostCode;
            this.clientPhone = clientPhone;
            this.clientFax = clientFax;
            this.clientCredit = clientCredit;
        }

        public Client ()
        {
            this.clientId = 0;
            this.clientName = "Default";
            this.clientStreet = "Default";
            this.clientEmail = "Default";
            this.clientCity = "Default";
            this.clientPostCode = "Default";
            this.clientPhone = "Default";
            this.clientFax = "Default";
            this.clientCredit = 0;
        }

        public int ClientId
        {
            get
            {
                return clientId;
            }

            set
            {
                clientId = value;
            }
        }

        public string ClientName
        {
            get
            {
                return clientName;
            }

            set
            {
                clientName = value;
            }
        }

        public string ClientStreet
        {
            get
            {
                return clientStreet;
            }

            set
            {
                clientStreet = value;
            }
        }

        public string ClientEmail
        {
            get
            {
                return clientEmail;
            }

            set
            {
                clientEmail = value;
            }
        }

        public string ClientCity
        {
            get
            {
                return clientCity;
            }

            set
            {
                clientCity = value;
            }
        }

        public string ClientPostCode
        {
            get
            {
                return clientPostCode;
            }

            set
            {
                clientPostCode = value;
            }
        }

        public string ClientPhone
        {
            get
            {
                return clientPhone;
            }

            set
            {
                clientPhone = value;
            }
        }

        public string ClientFax
        {
            get
            {
                return clientFax;
            }

            set
            {
                clientFax = value;
            }
        }

        public decimal ClientCredit
        {
            get
            {
                return clientCredit;
            }

            set
            {
                clientCredit = value;
            }
        }
    }
}
