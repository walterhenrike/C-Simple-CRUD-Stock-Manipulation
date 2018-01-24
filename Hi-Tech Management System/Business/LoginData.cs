using System;

namespace Hi_Tech_Management_System.Business
{
    [Serializable]
    public class LoginData : Employee
    {
        private string username;
        private string passwordhint;
        private string position;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string PasswordHint
        {
            get { return passwordhint; }
            set { passwordhint = value; }
        }

        public string Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        private string password;

        public LoginData()
        {
            this.password = "pass";
            this.username = "username";
            this.passwordhint = "hint";
            this.position = "position";
        }
        public LoginData(string username, string password, string passwordhint, string position)
        {
            this.passwordhint = passwordhint;
            this.username = username;
            this.password = password;
            this.position = position;
        }
    }
}
