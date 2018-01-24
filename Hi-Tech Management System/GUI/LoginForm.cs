using Hi_Tech_Management_System.Business;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Hi_Tech_Management_System.DataAccess;
using Hi_Tech_Management_System.GUI;
using MetroFramework;

namespace Hi_Tech_Management_System
{
    public partial class LoginForm : MetroFramework.Forms.MetroForm
    {
        //*******************************************************
        // Login Form
        // All users have to pass this login form in order to
        // have access to the program.
        // Only users who are allowed to use the program will
        // have access granted.
        // Each user will be able to have access only to its 
        // level in the company. Eg.: MIS will acess only MIS
        // section of the main form.
        //*******************************************************


        //*******************************************************
        // Control variables
        //*******************************************************
        public static int attempt;
        public bool selected;
        public static string username;
        public static string pass1;
        public static string pass2;
        public int indexUser;

        public LoginForm()
        {
            InitializeComponent();
        }

        //*******************************************************
        // Load of the form plus database loading
        //*******************************************************
        private void LoginForm_Load(object sender, EventArgs e)
        {
            Validator.Validation.frm = this;
            attempt = 0;
            GeneralDataManipulation.listOfEmployee = new List<Employee>();
            GeneralDataManipulation.listOfEmployee = EmployeeDA.ReadUsers();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            var confirmResult = MetroMessageBox.Show(this, "Would you like to close the application?", "Confirm Close!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                MetroMessageBox.Show(this, "The application wont be closed.");
            }
        }

        //*******************************************************
        // Information checker plus information transfering such
        // as user position, username and password for further
        // modification if the user intend to modify its own
        // password.
        // Username = UserID and that can only be modified by
        // an authorized MIS Manager.
        //*******************************************************
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Validator.Validation.IsNumber(txtUserName) &&
                Validator.Validation.IsPresent(txtPass) &&
                Validator.Validation.IsPresent(txtUserName))
            {
                int abcd = Convert.ToInt32(txtUserName.Text);
                int indexEmp = GeneralDataManipulation.listOfEmployee.FindIndex(r => r.EmpId.Equals(abcd));
                if ((indexEmp == -1) || (!GeneralDataManipulation.listOfEmployee[indexEmp].UseSystem))
                {
                    attempt++;
                    MetroMessageBox.Show(this, "Verify the username/password. Attempt " + attempt + " of 3.", "Invalid data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (attempt == 3)
                    {
                        MetroMessageBox.Show(this, "Invalid username/password entered multiple times. The application will shut down", "Invalid attempt to login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                    }
                }
                else if ((GeneralDataManipulation.listOfEmployee[indexEmp].UserPassword == txtPass.Text))
                {
                    Form MainForm = new Main_Form();
                    GeneralDataManipulation.userPosition = GeneralDataManipulation.listOfEmployee[indexEmp].EmpPosition;
                    GeneralDataManipulation.userID = GeneralDataManipulation.listOfEmployee[indexEmp].EmpId;
                    GeneralDataManipulation.userPass = GeneralDataManipulation.listOfEmployee[indexEmp].UserPassword;
                    GeneralDataManipulation.userINDEX = indexEmp;

                    this.Hide();
                    MainForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    attempt++;
                    MetroMessageBox.Show(this, "Verify the username/password. Attempt " + attempt + " of 3.", "Invalid data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (attempt == 3)
                    {
                        MetroMessageBox.Show(this, "Invalid username/password entered multiple times. The application will shut down", "Invalid attempt to login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                    }
                }
            }
            else
            {
                MetroMessageBox.Show(this, "Verify your entries!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            Form About = new AboutForm();
            About.ShowDialog();
        }
    }
}
