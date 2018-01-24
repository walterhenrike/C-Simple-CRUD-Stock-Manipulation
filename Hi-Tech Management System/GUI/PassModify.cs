using MetroFramework;
using System.Windows.Forms;
using Hi_Tech_Management_System.DataAccess;
using Hi_Tech_Management_System.Business;
using System;

namespace Hi_Tech_Management_System.GUI
{
    public partial class PassModify : MetroFramework.Forms.MetroForm
    {
        //*******************************************************
        // Password Modify Form
        // The user is here allowed to modify its own password.
        // If all the information provided is valid, the user
        // set a new password without having to ask for a MIS
        // manager to do it. 
        //*******************************************************

        public PassModify()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            var confirmResult = MetroMessageBox.Show(this, "Would you like to leave the password modification form?", "Confirm Close!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        //*******************************************************
        // Information validation section
        //*******************************************************
        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            if (Validator.Validation.IsNumber(txtUserName)) {
                if (
                    (Convert.ToInt32(txtUserName.Text) == GeneralDataManipulation.listOfEmployee[GeneralDataManipulation.userINDEX].EmpId)
                    &&
                    (txtNewPass1.Text == txtNewPass2.Text)
                    &&
                    (txtOldPass.Text == GeneralDataManipulation.userPass)
                    )
                {
                    var confirmResult = MetroMessageBox.Show(this, "Are you sure to modify your password?", "Confirm Modification!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        GeneralDataManipulation.listOfEmployee[GeneralDataManipulation.userINDEX].UserPassword = txtNewPass1.Text;
                        EmployeeDA.WriteUser(GeneralDataManipulation.listOfEmployee);
                        this.Close();
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "The password wasn't modified.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MetroMessageBox.Show(this, "Something is wrong, check entered data", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
