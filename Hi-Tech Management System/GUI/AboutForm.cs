using System;
using MetroFramework.Forms;

namespace Hi_Tech_Management_System.GUI
{
    //*******************************************************
    // Simple about box to display some 
    // information regarding the company
    // and the programmer who developed
    // the application
    //*******************************************************
    public partial class AboutForm : MetroForm
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            txtAbout.AppendText("Hi-Tech Distribution Inc. (Virtual)");
            txtAbout.AppendText(Environment.NewLine);
            txtAbout.AppendText("7122 18th Montreal, Quebec");
            txtAbout.AppendText(Environment.NewLine);
            txtAbout.AppendText("H2A2M8");
            txtAbout.AppendText(Environment.NewLine);
            txtAbout.AppendText("Tel: (514) 721 - 8662");
            txtAbout.AppendText(Environment.NewLine);
            txtAbout.AppendText("Fax: (514) 777 - 8665");
            txtAbout.AppendText(Environment.NewLine);
            txtAbout.AppendText(Environment.NewLine);
            txtAbout.AppendText("Developed by: Walter Henrike");
            txtAbout.AppendText(Environment.NewLine);
            txtAbout.AppendText("LaSalle College - Autumn 2017");
            txtAbout.AppendText(Environment.NewLine);
            txtAbout.AppendText("Teacher: Quang Hoang Cao");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
