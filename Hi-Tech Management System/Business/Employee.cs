using System;

namespace Hi_Tech_Management_System.Business
{
    [Serializable]
    public class Employee
    {

        private int empId;
        private string empFName;
        private string empLName;
        private string empPhone;
        private string empEmail;
        private string empPosition;
        private string userPassword;
        private string workHours;
        private bool active;
        private bool useSystem;

        public Employee()
        {
            this.empId = 0;
            this.empFName = "Default";
            this.empLName = "Default";
            this.empPhone = "Default";
            this.empEmail = "Default";
            this.empPosition = "Default";
            this.userPassword = "Default";
            this.userPassword = "Default";
            this.workHours = "Default";
            this.active = true;
            this.useSystem = true;

        }

        public Employee(int empId, string empFName, string empLName, string empPhone, string empEmail, string empPosition, string userPassword, string workHours, bool active, bool useSystem)
        {
            this.empId = empId;
            this.empFName = empFName;
            this.empLName = empLName;
            this.empPhone = empPhone;
            this.empEmail = empEmail;
            this.empPosition = empPosition;
            this.userPassword = userPassword;
            this.workHours = workHours;
            this.active = active;
            this.useSystem = useSystem;
        }

        public int EmpId
        {
            get
            {
                return empId;
            }

            set
            {
                empId = value;
            }
        }

        public string EmpFName
        {
            get
            {
                return empFName;
            }

            set
            {
                empFName = value;
            }
        }

        public string EmpLName
        {
            get
            {
                return empLName;
            }

            set
            {
                empLName = value;
            }
        }

        public string EmpPhone
        {
            get
            {
                return empPhone;
            }

            set
            {
                empPhone = value;
            }
        }

        public string EmpEmail
        {
            get
            {
                return empEmail;
            }

            set
            {
                empEmail = value;
            }
        }

        public string EmpPosition
        {
            get
            {
                return empPosition;
            }

            set
            {
                empPosition = value;
            }
        }

        public string UserPassword
        {
            get
            {
                return userPassword;
            }

            set
            {
                userPassword = value;
            }
        }

        public string WorkHours
        {
            get
            {
                return workHours;
            }

            set
            {
                workHours = value;
            }
        }

        public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
            }
        }

        public bool UseSystem
        {
            get
            {
                return useSystem;
            }

            set
            {
                useSystem = value;
            }
        }
    }
}
