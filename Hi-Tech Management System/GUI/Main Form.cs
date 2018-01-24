using System;
using System.Collections.Generic;
using Hi_Tech_Management_System.Business;
using System.Windows.Forms;
using Hi_Tech_Management_System.DataAccess;
using MetroFramework;
using System.IO;
using System.Linq;
using MetroFramework.Forms;
using System.Threading;

namespace Hi_Tech_Management_System.GUI
{
    public partial class Main_Form : MetroForm
    {
        //*******************************************************
        // Main application form
        // This form contains all the base manipulation of the
        // program.
        //*******************************************************
        public Main_Form()
        {
            Thread t = new Thread(new ThreadStart(loading));
            t.Start();
            InitializeComponent();
            for (int i = 0; i < 350; i++)
            {
                Thread.Sleep(10);
            }
            t.Abort();
        }


        void loading ()
        {
            SplashScreen frm = new SplashScreen();
            Application.Run(frm);
        }
        //***********************************************************
        // General Buttons (Every user can click)
        // Logout, Exit, Change password
        //***********************************************************

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            Form PassModify = new PassModify();
            PassModify.ShowDialog();    
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form abcde = new LoginForm();
            this.Hide();
            abcde.ShowDialog();
        }

        private void btnCloseMain_Click(object sender, EventArgs e)
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

        //*****************************************************
        // MIS
        // From this part on and until else mentioned, 
        // the code will be related to the MIS section.
        //*****************************************************

        // DIsplays the list of employees
        public void DisplayLVEmp(List<Employee> listOfEmp)
        {
            lvEmployee.Items.Clear();
            foreach (Employee element in listOfEmp)
            {
                ListViewItem item = new ListViewItem(Convert.ToString(element.EmpId));
                item.SubItems.Add(Convert.ToString(element.EmpFName));
                item.SubItems.Add(Convert.ToString(element.EmpLName));
                item.SubItems.Add(Convert.ToString(element.EmpPosition));
                item.SubItems.Add(Convert.ToString(element.EmpPhone));
                item.SubItems.Add(Convert.ToString(element.EmpEmail));
                item.SubItems.Add(Convert.ToString(element.Active));
                item.SubItems.Add(Convert.ToString(element.WorkHours));
                item.SubItems.Add(Convert.ToString(element.UseSystem));
                lvEmployee.Items.Add(item);
            }
        }

        //*****************************************************
        // Check control variables
        //*****************************************************

        public int indexEmp;
        public int listSelect;
        public bool empSelect;
        public bool salesSelect;
        public bool authorSelect;
        public bool publisherSelect;
        public bool categorySelect;
        public bool typeSelect;
        public bool itemSelect;
        public bool orderSelect;
        public string imgpath;
        public string imgpathSelect;
        public bool imgselected;
        public string imgEdit;
        public int indexCmbItem;
        public int indexCmbClient;

        //*****************************************************
        // Clears all the fields after an user is deleted, 
        // edited or added
        //*****************************************************
        public void misClear()
        {
            txtEmpID.Clear();
            txtEmpFN.Clear();
            txtEmpLN.Clear();
            txtEmpEmail.Clear();
            cmbEmpPos.SelectedIndex = -1;
            txtEmpPhone.Clear();
            txtEmpPass.Clear();
            cmbMisActive.SelectedIndex = -1;
            cmbMisWorkHours.SelectedIndex = -1;
            cmbUseSystem.SelectedIndex = -1;
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            //*****************************************************
            //         Load MIS
            //*****************************************************
            GeneralDataManipulation.listOfEmployee = new List<Employee>();
            GeneralDataManipulation.listOfEmployee = EmployeeDA.ReadUsers();
            DisplayLVEmp(GeneralDataManipulation.listOfEmployee);
            //*****************************************************
            //         Load SALES
            //*****************************************************
            GeneralDataManipulation.listOfClients = new List<Client>();
            GeneralDataManipulation.listOfClients = ClientDA.ReadUsers();
            DisplayLVSales(GeneralDataManipulation.listOfClients);
            //*****************************************************
            //         Load INVENTORY
            //*****************************************************
            //GeneralDataManipulation.listOfAuthors = new List<Author>();
            //GeneralDataManipulation.listOfAuthors = AuthorDA.ReadUsers();
            //DisplayLVAuthor(GeneralDataManipulation.listOfAuthors);
            AuthorDA listauthors = new AuthorDA();
            GeneralDataManipulation.listOfAuthors = new List<Author>();
            GeneralDataManipulation.listOfAuthors = listauthors.ReadUsers();
            DisplayLVAuthor(GeneralDataManipulation.listOfAuthors);

            GeneralDataManipulation.listOfPublishers = new List<Publisher>();
            GeneralDataManipulation.listOfPublishers = PublisherDA.ReadUsers();
            DisplayLVPublisher(GeneralDataManipulation.listOfPublishers);

            GeneralDataManipulation.listOfCategory = new List<Category>();
            GeneralDataManipulation.listOfCategory = CategoryDA.ReadUsers();
            DisplayLVCategory(GeneralDataManipulation.listOfCategory);

            GeneralDataManipulation.listOfType = new List<ItemType>();
            GeneralDataManipulation.listOfType = ItemTypeDA.ReadUsers();
            DisplayLVType(GeneralDataManipulation.listOfType);

            GeneralDataManipulation.listOfItems = new List<Book>();
            GeneralDataManipulation.listOfItems = ItemsDA.ReadUsers();
            DisplayLVItems(GeneralDataManipulation.listOfItems);

            InventoryComboboxReload();

            //*****************************************************
            //         Load Order
            //*****************************************************
            GeneralDataManipulation.listOfOrders = new List<Order>();
            GeneralDataManipulation.listOfOrders = OrdersDA.ReadUsers();
            DisplayLvOrders(GeneralDataManipulation.listOfOrders);

            OrderComboboxReload();

            //*****************************************************
            //         Disable tabs according to user previleges
            //*****************************************************

            if (GeneralDataManipulation.userPosition == "MIS Manager")
            {
                mainTab.DisableTab(TabOrder);
                mainTab.DisableTab(TabSales);
                mainTab.DisableTab(TabInventory);
                mainTab.SelectedTab = TabMIS;
            }
            else if (GeneralDataManipulation.userPosition == "Sales Manager")
            {
                mainTab.DisableTab(TabOrder);
                mainTab.DisableTab(TabMIS);
                mainTab.DisableTab(TabInventory);
                mainTab.SelectedTab = TabSales;
            }
            else if(GeneralDataManipulation.userPosition == "Order Clerk")
            {
                mainTab.DisableTab(TabMIS);
                mainTab.DisableTab(TabSales);
                mainTab.DisableTab(TabInventory);
                mainTab.SelectedTab = TabOrder;
            }
            else if (GeneralDataManipulation.userPosition == "Inventory Controller")
            {
                mainTab.DisableTab(TabMIS);
                mainTab.DisableTab(TabSales);
                mainTab.DisableTab(TabOrder);
                mainTab.SelectedTab = TabInventory;
                metroTabControl1.SelectedTab = metroTabPage1;
            }
        }

        //*****************************************************
        // Validates the information for Employee tab
        //*****************************************************

        private bool isValidEmp()
        {
            if (
            Validator.Validation.IsPresent(txtEmpID) &&
            Validator.Validation.IsPresent(txtEmpFN) &&
            Validator.Validation.IsPresent(txtEmpLN) &&
            Validator.Validation.IsPresent(txtEmpEmail) &&
            Validator.Validation.IsPresent(cmbEmpPos) &&
            Validator.Validation.IsPresent(txtEmpPhone) &&
            Validator.Validation.IsPresent(txtEmpPass) &&
            Validator.Validation.IsPresent(cmbMisActive) &&
            Validator.Validation.IsPresent(cmbMisWorkHours) &&
            Validator.Validation.IsPresent(cmbUseSystem) &&

            Validator.Validation.IsNumber(txtEmpID) && 
            Validator.Validation.FieldSize(txtEmpID))
            {
                return true;
            }
            return false;
        }

        //*****************************************************
        //         Add new employee
        //*****************************************************
        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            if (isValidEmp())
            {
                if (GeneralDataManipulation.listOfEmployee.Any(x => x.EmpId == Convert.ToInt32(txtEmpID.Text)))
                {
                    MetroMessageBox.Show(this, "The entered value has to be UNIQUE!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Employee aEmp = new Employee();
                    aEmp.EmpId = Convert.ToInt32(txtEmpID.Text);
                    aEmp.EmpFName = txtEmpFN.Text;
                    aEmp.EmpLName = txtEmpLN.Text;
                    aEmp.EmpEmail = txtEmpEmail.Text;
                    aEmp.EmpPosition = cmbEmpPos.Text;
                    aEmp.EmpPhone = txtEmpPhone.Text;
                    aEmp.UserPassword = txtEmpPass.Text;
                    aEmp.Active = Convert.ToBoolean(cmbMisActive.Text);
                    aEmp.WorkHours = cmbMisWorkHours.Text;
                    aEmp.UseSystem = Convert.ToBoolean(cmbUseSystem.Text);

                    GeneralDataManipulation.listOfEmployee.Add(aEmp);
                    DisplayLVEmp(GeneralDataManipulation.listOfEmployee);
                    misClear();
                }
            }
        }

        //*****************************************************
        // Saves all modifications
        // to the MIS tab
        //*****************************************************

        private void btnSaveEmployee_Click(object sender, EventArgs e)
        {
            var confirmResult =  MetroMessageBox.Show(this,"Would you like to save the database to the file?\nIt will overwrite the previous database.", "Confirm Save!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                EmployeeDA.WriteUser(GeneralDataManipulation.listOfEmployee);
            }
            else
            {
                 MetroMessageBox.Show(this,"The data wasn't saved!");
            }
        }

        //*****************************************************
        // Removes the selected employee
        //*****************************************************
        private void btnRemoveEmployee_Click(object sender, EventArgs e)
        {
            var confirmResult =  MetroMessageBox.Show(this,"Are you sure to delete this item ?", "Confirm Delete!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if ((listSelect != -1) && (indexEmp != -1) && (empSelect == true))
                {
                    empSelect = false;
                    GeneralDataManipulation.listOfEmployee.RemoveAt(indexEmp);
                    this.lvEmployee.Items.RemoveAt(listSelect);
                     MetroMessageBox.Show(this,"The item was deleted.");
                }
                else
                {
                     MetroMessageBox.Show(this,"You must select an item to remove.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            misClear();              
        }

        //*****************************************************
        // When the user clicks on the list
        // all the info from the lv will be displayed
        // in the corresponding edit/combo box
        //*****************************************************
        private void lvEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvEmployee.SelectedItems.Count > 0)
            {
                empSelect = true;
                int abcd = Convert.ToInt32(lvEmployee.Items[lvEmployee.SelectedIndices[0]].Text);
                listSelect = lvEmployee.SelectedIndices[0];
                indexEmp = GeneralDataManipulation.listOfEmployee.FindIndex(r => r.EmpId.Equals(abcd));
                txtEmpID.Text = Convert.ToString(GeneralDataManipulation.listOfEmployee[indexEmp].EmpId);
                txtEmpFN.Text = GeneralDataManipulation.listOfEmployee[indexEmp].EmpFName;
                txtEmpLN.Text = GeneralDataManipulation.listOfEmployee[indexEmp].EmpLName;
                txtEmpEmail.Text = GeneralDataManipulation.listOfEmployee[indexEmp].EmpEmail;
                cmbEmpPos.Text = GeneralDataManipulation.listOfEmployee[indexEmp].EmpPosition;
                txtEmpPhone.Text = GeneralDataManipulation.listOfEmployee[indexEmp].EmpPhone;
                txtEmpPass.Text = GeneralDataManipulation.listOfEmployee[indexEmp].UserPassword;
                cmbMisWorkHours.Text = GeneralDataManipulation.listOfEmployee[indexEmp].WorkHours;
                cmbMisActive.Text = Convert.ToString(GeneralDataManipulation.listOfEmployee[indexEmp].Active);
                cmbUseSystem.Text = Convert.ToString(GeneralDataManipulation.listOfEmployee[indexEmp].UseSystem);
            }
        }

        //*****************************************************
        // Edits the employee's info according to the
        // user modification
        //*****************************************************
        private void btnEditEmployee_Click(object sender, EventArgs e)
        {
            if (isValidEmp())
            {
                var confirmResult = MetroMessageBox.Show(this, "Are you sure to MODIFY this item ?", "Confirm MODIFY!", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    if (GeneralDataManipulation.listOfEmployee.Any(x => x.EmpId == Convert.ToInt32(txtEmpID.Text)) &&
                    (Convert.ToString(GeneralDataManipulation.listOfEmployee[indexEmp].EmpId) != txtEmpID.Text))
                    {
                        MetroMessageBox.Show(this, "The entered value has to be UNIQUE!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        GeneralDataManipulation.listOfEmployee[indexEmp].EmpId = Convert.ToInt32(txtEmpID.Text);
                        GeneralDataManipulation.listOfEmployee[indexEmp].EmpFName = txtEmpFN.Text;
                        GeneralDataManipulation.listOfEmployee[indexEmp].EmpLName = txtEmpLN.Text;
                        GeneralDataManipulation.listOfEmployee[indexEmp].EmpEmail = txtEmpEmail.Text;
                        GeneralDataManipulation.listOfEmployee[indexEmp].EmpPosition = cmbEmpPos.Text;
                        GeneralDataManipulation.listOfEmployee[indexEmp].EmpPhone = txtEmpPhone.Text;
                        GeneralDataManipulation.listOfEmployee[indexEmp].UserPassword = txtEmpPass.Text;
                        GeneralDataManipulation.listOfEmployee[indexEmp].WorkHours = cmbMisWorkHours.Text;
                        GeneralDataManipulation.listOfEmployee[indexEmp].Active = Convert.ToBoolean(cmbMisActive.Text);
                        GeneralDataManipulation.listOfEmployee[indexEmp].UseSystem = Convert.ToBoolean(cmbUseSystem.Text);
                        misClear();
                        DisplayLVEmp(GeneralDataManipulation.listOfEmployee);
                    }
                }
            }
        }

        //*****************************************************
        // Lists all the employee's according to the database
        //*****************************************************

        private void btnRefreshEmployee_Click(object sender, EventArgs e)
        {
            GeneralDataManipulation.listOfEmployee = new List<Employee>();
            GeneralDataManipulation.listOfEmployee = EmployeeDA.ReadUsers();
            DisplayLVEmp(GeneralDataManipulation.listOfEmployee);
        }

        //*****************************************************
        // Search MIS (Code for search function MIS)
        //*****************************************************
        private void btnSearchEmployee_Click(object sender, EventArgs e)
        {
            int number = 0;
            string tSearch = txtEmployeeSearch.Text;
            if (cmbEmployeeSearch.Text == "Employee ID") {
                if (Int32.TryParse(tSearch, out number))
                {
                    var newlist = GeneralDataManipulation.listOfEmployee.FindAll(s => s.EmpId.Equals(Convert.ToInt32(tSearch)));
                    if (newlist.Count > 0) { DisplayLVEmp(newlist); }
                    else {  MetroMessageBox.Show(this,"Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else
                {
                     MetroMessageBox.Show(this,"You must enter an ID to search", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (cmbEmployeeSearch.Text == "First Name") {
                var newlist = GeneralDataManipulation.listOfEmployee.FindAll(s => s.EmpFName.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVEmp(newlist); }
                else {  MetroMessageBox.Show(this,"Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbEmployeeSearch.Text == "Last Name") {
                var newlist = GeneralDataManipulation.listOfEmployee.FindAll(s => s.EmpLName.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVEmp(newlist); }
                else {  MetroMessageBox.Show(this,"Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbEmployeeSearch.Text == "Position") {
                var newlist = GeneralDataManipulation.listOfEmployee.FindAll(s => s.EmpPosition.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVEmp(newlist); }
                else {  MetroMessageBox.Show(this,"Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbEmployeeSearch.Text == "Phone Number") {
                var newlist = GeneralDataManipulation.listOfEmployee.FindAll(s => s.EmpPhone.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVEmp(newlist); }
                else {  MetroMessageBox.Show(this,"Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbEmployeeSearch.Text == "E-Mail") {
                var newlist = GeneralDataManipulation.listOfEmployee.FindAll(s => s.EmpEmail.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVEmp(newlist); }
                else {  MetroMessageBox.Show(this,"Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbEmployeeSearch.Text == "Active") {
                var newlist = GeneralDataManipulation.listOfEmployee.FindAll(s => s.Active.Equals(Convert.ToBoolean(tSearch)));
                if (newlist.Count > 0) { DisplayLVEmp(newlist); }
                else {  MetroMessageBox.Show(this,"Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbEmployeeSearch.Text == "Work Hours") {
                var newlist = GeneralDataManipulation.listOfEmployee.FindAll(s => s.WorkHours.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVEmp(newlist); }
                else {  MetroMessageBox.Show(this,"Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }

        }
        //*****************************************************
        // Updates the list from the memory
        //*****************************************************

        private void btnUpdateList_Click(object sender, EventArgs e)
        {
            DisplayLVEmp(GeneralDataManipulation.listOfEmployee);
        }


        //*****************************************************
        // SALES
        // From this part on and until else mentioned, 
        // the code will be related to the SALES section.
        //*****************************************************

        //*****************************************************
        // Check control variables
        //*****************************************************
        public int indexSales;
        public int selectSales;

        //*****************************************************
        // Clears all the fields after an user is deleted, 
        // edited or added
        //*****************************************************
        public void salesClear()
        {
            txtSalesClientID.Clear();
            txtSalesClientName.Clear();
            txtSalesPhone.Clear();
            txtSalesFax.Clear();
            txtSalesAddress.Clear();
            txtSalesPostal.Clear();
            txtSalesCity.Clear();
            txtSalesEmail.Clear();
            txtSalesCred.Clear();
        }

        //****************************************************
        // Displays the list of clients
        //****************************************************
        public void DisplayLVSales(List<Client> listOfClients)
        {
            lvSales.Items.Clear();
            foreach (Client element in listOfClients)
            {
                ListViewItem item = new ListViewItem(Convert.ToString(element.ClientId));
                item.SubItems.Add(Convert.ToString(element.ClientName));
                item.SubItems.Add(Convert.ToString(element.ClientPhone));
                item.SubItems.Add(Convert.ToString(element.ClientFax));
                item.SubItems.Add(Convert.ToString(element.ClientStreet));
                item.SubItems.Add(Convert.ToString(element.ClientPostCode));
                item.SubItems.Add(Convert.ToString(element.ClientCity));
                item.SubItems.Add(Convert.ToString(element.ClientEmail));
                item.SubItems.Add(Convert.ToString(element.ClientCredit));
                lvSales.Items.Add(item);
            }
        }
        //*****************************************************
        // Validates the information for Sales tab
        //*****************************************************
        private bool isValidSales()
        {
            if (
            Validator.Validation.IsPresent(txtSalesClientID) &&
            Validator.Validation.IsPresent(txtSalesClientName) &&
            Validator.Validation.IsPresent(txtSalesPhone) &&
            Validator.Validation.IsPresent(txtSalesFax) &&
            Validator.Validation.IsPresent(txtSalesAddress) &&
            Validator.Validation.IsPresent(txtSalesPostal) &&
            Validator.Validation.IsPresent(txtSalesCity) &&
            Validator.Validation.IsPresent(txtSalesEmail) &&
            Validator.Validation.IsPresent(txtSalesCred) &&

            Validator.Validation.IsNumber(txtSalesClientID) &&
            Validator.Validation.IsNumber(txtSalesCred) &&

            Validator.Validation.FieldSize(txtSalesClientID))
            {
                return true;
            }
            return false;
        }

        //*****************************************************
        // Adds a new client
        //*****************************************************

        private void btnAddSales_Click(object sender, EventArgs e)
        {
            if (isValidSales())
            {
                if (GeneralDataManipulation.listOfClients.Any(x => x.ClientId == Convert.ToInt32(txtSalesClientID.Text)))
                {
                    MetroMessageBox.Show(this, "The entered value has to be UNIQUE!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Client aClient = new Client();

                    aClient.ClientId = Convert.ToInt32(txtSalesClientID.Text);
                    aClient.ClientName = txtSalesClientName.Text;
                    aClient.ClientPhone = txtSalesPhone.Text;
                    aClient.ClientFax = txtSalesFax.Text;
                    aClient.ClientStreet = txtSalesAddress.Text;
                    aClient.ClientPostCode = txtSalesPostal.Text;
                    aClient.ClientCity = txtSalesCity.Text;
                    aClient.ClientEmail = txtSalesEmail.Text;
                    aClient.ClientCredit = Decimal.Parse(txtSalesCred.Text);

                    GeneralDataManipulation.listOfClients.Add(aClient);
                    DisplayLVSales(GeneralDataManipulation.listOfClients);
                    salesClear();
                }
            }
        }
        //*****************************************************
        // Removes a client
        //*****************************************************
        private void btnRemSales_Click(object sender, EventArgs e)
        {
            var confirmResult =  MetroMessageBox.Show(this,"Are you sure to delete this item ?", "Confirm Delete!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if ((selectSales != -1) && (indexSales != -1) && (salesSelect == true))
                {
                    salesSelect = false;
                    GeneralDataManipulation.listOfClients.RemoveAt(indexSales);
                    this.lvSales.Items.RemoveAt(selectSales);
                     MetroMessageBox.Show(this,"The item was deleted.");
                }
                else
                {
                     MetroMessageBox.Show(this,"You must select an item to remove.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            salesClear();
        }

        //*****************************************************
        // Add information to all fields according to theose 
        // selected on the list
        //*****************************************************
        private void lvSales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSales.SelectedItems.Count > 0)
            {
                int abcd = Convert.ToInt32(lvSales.Items[lvSales.SelectedIndices[0]].Text);
                selectSales = lvSales.SelectedIndices[0];
                salesSelect = true;
                indexSales = GeneralDataManipulation.listOfClients.FindIndex(r => r.ClientId.Equals(abcd));
                txtSalesClientID.Text = Convert.ToString(GeneralDataManipulation.listOfClients[indexSales].ClientId);
                txtSalesClientName.Text = GeneralDataManipulation.listOfClients[indexSales].ClientName;
                txtSalesPhone.Text = GeneralDataManipulation.listOfClients[indexSales].ClientPhone;
                txtSalesFax.Text = GeneralDataManipulation.listOfClients[indexSales].ClientFax;
                txtSalesAddress.Text = GeneralDataManipulation.listOfClients[indexSales].ClientStreet;
                txtSalesPostal.Text = GeneralDataManipulation.listOfClients[indexSales].ClientPostCode;
                txtSalesCity.Text = GeneralDataManipulation.listOfClients[indexSales].ClientCity;
                txtSalesEmail.Text = GeneralDataManipulation.listOfClients[indexSales].ClientEmail;
                txtSalesCred.Text = Convert.ToString(GeneralDataManipulation.listOfClients[indexSales].ClientCredit);
            }
        }

        //*****************************************************
        // Saves the information to the database
        //*****************************************************
        private void btnSalesSave_Click(object sender, EventArgs e)
        {
            var confirmResult =  MetroMessageBox.Show(this,"Would you like to save the database to the file?\nIt will overwrite the previous database.", "Confirm Save!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                ClientDA.WriteUser(GeneralDataManipulation.listOfClients);
            }
            else
            {
                 MetroMessageBox.Show(this,"The data wasn't saved!");
            }
        }

        //*****************************************************
        // Reloads the information from the database
        //*****************************************************
        private void btnSalesReload_Click(object sender, EventArgs e)
        {
            GeneralDataManipulation.listOfClients = new List<Client>();
            GeneralDataManipulation.listOfClients = ClientDA.ReadUsers();
            DisplayLVSales(GeneralDataManipulation.listOfClients);
        }

        //*****************************************************
        // Refreshes the list from the memory
        //*****************************************************
        private void btnSalesRefresh_Click(object sender, EventArgs e)
        {
            DisplayLVSales(GeneralDataManipulation.listOfClients);
        }

        //*****************************************************
        // Edits the information from a selected user
        //*****************************************************
        private void btnEditSales_Click(object sender, EventArgs e)
        {
            if (isValidSales())
            {
                var confirmResult = MetroMessageBox.Show(this, "Are you sure to MODIFY this item ?", "Confirm MODIFY!", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    if (GeneralDataManipulation.listOfClients.Any(x => x.ClientId == Convert.ToInt32(txtSalesClientID.Text)) &&
                    (Convert.ToString(GeneralDataManipulation.listOfClients[indexSales].ClientId) != txtSalesClientID.Text))
                    {
                        MetroMessageBox.Show(this, "The entered value has to be UNIQUE!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        GeneralDataManipulation.listOfClients[indexSales].ClientId = Convert.ToInt32(txtSalesClientID.Text);
                        GeneralDataManipulation.listOfClients[indexSales].ClientName = txtSalesClientName.Text;
                        GeneralDataManipulation.listOfClients[indexSales].ClientPhone = txtSalesPhone.Text;
                        GeneralDataManipulation.listOfClients[indexSales].ClientFax = txtSalesFax.Text;
                        GeneralDataManipulation.listOfClients[indexSales].ClientStreet = txtSalesAddress.Text;
                        GeneralDataManipulation.listOfClients[indexSales].ClientPostCode = txtSalesPostal.Text;
                        GeneralDataManipulation.listOfClients[indexSales].ClientCity = txtSalesCity.Text;
                        GeneralDataManipulation.listOfClients[indexSales].ClientEmail = txtSalesEmail.Text;
                        GeneralDataManipulation.listOfClients[indexSales].ClientCredit = Convert.ToDecimal(txtSalesCred.Text);
                        DisplayLVSales(GeneralDataManipulation.listOfClients);
                        salesClear();
                    }
                }
            }
        }

        //*****************************************************
        // Search function for the sales tab
        //*****************************************************
        private void btnSearchSales_Click(object sender, EventArgs e)
        {
            int number = 0;
            string tSearch = txtSalesSearch.Text;
            if (cmbSalesSearch.Text == "Client ID")
            {
                if (Int32.TryParse(tSearch, out number))
                {
                    var newlist = GeneralDataManipulation.listOfClients.FindAll(s => s.ClientId.Equals(Convert.ToInt32(tSearch)));
                    if (newlist.Count > 0) { DisplayLVSales(newlist); }
                    else {  MetroMessageBox.Show(this,"Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else
                {
                     MetroMessageBox.Show(this,"You must enter an ID to search", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (cmbSalesSearch.Text == "Client Name")
            {
                var newlist = GeneralDataManipulation.listOfClients.FindAll(s => s.ClientName.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVSales(newlist); }
                else {  MetroMessageBox.Show(this,"Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbSalesSearch.Text == "Phone Number")
            {
                var newlist = GeneralDataManipulation.listOfClients.FindAll(s => s.ClientPhone.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVSales(newlist); }
                else {  MetroMessageBox.Show(this,"Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbSalesSearch.Text == "Fax Number")
            {
                var newlist = GeneralDataManipulation.listOfClients.FindAll(s => s.ClientFax.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVSales(newlist); }
                else {  MetroMessageBox.Show(this,"Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbSalesSearch.Text == "Address")
            {
                var newlist = GeneralDataManipulation.listOfClients.FindAll(s => s.ClientStreet.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVSales(newlist); }
                else {  MetroMessageBox.Show(this,"Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbSalesSearch.Text == "Postal Code")
            {
                var newlist = GeneralDataManipulation.listOfClients.FindAll(s => s.ClientPostCode.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVSales(newlist); }
                else {  MetroMessageBox.Show(this,"Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbSalesSearch.Text == "City")
            {
                var newlist = GeneralDataManipulation.listOfClients.FindAll(s => s.ClientCity.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVSales(newlist); }
                else {  MetroMessageBox.Show(this,"Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbSalesSearch.Text == "E - Mail")
            {
                var newlist = GeneralDataManipulation.listOfClients.FindAll(s => s.ClientEmail.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVSales(newlist); }
                else {  MetroMessageBox.Show(this,"Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbSalesSearch.Text == "Credit")
            {
                if (Int32.TryParse(tSearch, out number))
                {
                    var newlist = GeneralDataManipulation.listOfClients.FindAll(s => s.ClientCredit.Equals(Convert.ToDecimal(tSearch)));
                    if (newlist.Count > 0) { DisplayLVSales(newlist); }
                    else {  MetroMessageBox.Show(this,"Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else
                {
                     MetroMessageBox.Show(this,"You must enter a decimal number to search", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }         
        }
        //*****************************************************
        // INVENTORY
        // From this part on and until else mentioned, 
        // the code will be related to the INVENTORY section.
        //*****************************************************

        //*****************************************************
        // Check control variables
        //*****************************************************
        public int indexInvAuthor;
        public int selectInvAuthor;

        //********************************
        // Lists all the authors
        //********************************

        public void DisplayLVAuthor(List<Author> listOfAuthors)
        {
            lvInvAut.Items.Clear();
            foreach (Author element in listOfAuthors)
            {
                ListViewItem item = new ListViewItem(Convert.ToString(element.AuthorID));
                item.SubItems.Add(Convert.ToString(element.AuthorFirstName));
                item.SubItems.Add(Convert.ToString(element.AuthorLastName));
                item.SubItems.Add(Convert.ToString(element.AuthorEmail));
                lvInvAut.Items.Add(item);
            }
            InventoryComboboxReload();
        }

        //********************************
        // Clears all the fields on the
        // author form.
        //********************************

        public void AuthorClear()
        {
            txtInvAutID.Clear();
            txtInvAutLN.Clear();
            txtInvAutFN.Clear();
            txtInvAutEmail.Clear();
        }

        //*****************************************************
        // Validates the information for Author tab
        //*****************************************************
        private bool isValidAuthor()
        {
            if (Validator.Validation.IsPresent(txtInvAutID) &&
            Validator.Validation.IsPresent(txtInvAutFN) &&
            Validator.Validation.IsPresent(txtInvAutLN) &&
            Validator.Validation.IsPresent(txtInvAutEmail) && 
            Validator.Validation.IsNumber(txtInvAutID) && Validator.Validation.FieldSize(txtInvAutID))
            {
                return true;
            }
            return false;
        }

        //********************************
        // Author Management
        //********************************
        private void btnInvAuthorAdd_Click(object sender, EventArgs e)
        {
            if (isValidAuthor())
            {
                if (GeneralDataManipulation.listOfAuthors.Any(x => x.AuthorID == Convert.ToInt32(txtInvAutID.Text)))
                {
                    MetroMessageBox.Show(this, "The entered value has to be UNIQUE!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Author aAuthor = new Author();
                    aAuthor.AuthorID = Convert.ToInt32(txtInvAutID.Text);
                    aAuthor.AuthorFirstName = txtInvAutFN.Text;
                    aAuthor.AuthorLastName = txtInvAutLN.Text;
                    aAuthor.AuthorEmail = txtInvAutEmail.Text;
                    GeneralDataManipulation.listOfAuthors.Add(aAuthor);
                    DisplayLVAuthor(GeneralDataManipulation.listOfAuthors);
                    AuthorClear();
                }
            }
        }

        //********************************
        // Edits the selected item
        //********************************
        private void btnInvAutEdit_Click(object sender, EventArgs e)
        {
            if (isValidAuthor())
            {
                var confirmResult = MetroMessageBox.Show(this, "Are you sure to MODIFY this item ?", "Confirm MODIFY!", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    if (GeneralDataManipulation.listOfAuthors.Any(x => x.AuthorID == Convert.ToInt32(txtInvAutID.Text)) &&
                    (Convert.ToString(GeneralDataManipulation.listOfAuthors[indexInvAuthor].AuthorID) != txtInvAutID.Text))
                    {
                        MetroMessageBox.Show(this, "The entered value has to be UNIQUE!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        GeneralDataManipulation.listOfAuthors[indexInvAuthor].AuthorID = Convert.ToInt32(txtInvAutID.Text);
                        GeneralDataManipulation.listOfAuthors[indexInvAuthor].AuthorFirstName = txtInvAutFN.Text;
                        GeneralDataManipulation.listOfAuthors[indexInvAuthor].AuthorLastName = txtInvAutLN.Text;
                        GeneralDataManipulation.listOfAuthors[indexInvAuthor].AuthorEmail = txtInvAutEmail.Text;
                        AuthorClear();
                        DisplayLVAuthor(GeneralDataManipulation.listOfAuthors);
                    }
                }
            }
        }

        //********************************
        // Removes the item
        //********************************
        private void btnInvAutRem_Click(object sender, EventArgs e)
        {
            var confirmResult =  MetroMessageBox.Show(this,"Are you sure to delete this item ?", "Confirm Delete!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if ((selectInvAuthor != -1) && (indexInvAuthor != -1) && (authorSelect == true))
                {
                    authorSelect = false;
                    GeneralDataManipulation.listOfAuthors.RemoveAt(indexInvAuthor);
                    this.lvInvAut.Items.RemoveAt(selectInvAuthor);
                     MetroMessageBox.Show(this,"The item was deleted.");
                }
                else
                {
                     MetroMessageBox.Show(this,"You must select an item to remove.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            AuthorClear();
        }

        //********************************
        // Refreshes the list from the
        // backup
        //********************************
        private void btnInvAuthorRefresh_Click(object sender, EventArgs e)
        {
            DisplayLVAuthor(GeneralDataManipulation.listOfAuthors);
        }

        //********************************
        // Saves the author database
        //********************************
        private void btnInvAuthorSave_Click(object sender, EventArgs e)
        {
            var confirmResult = MetroMessageBox.Show(this, "Would you like to save the database to the file?\nIt will overwrite the previous database.", "Confirm Save!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                AuthorDA.WriteUser(GeneralDataManipulation.listOfAuthors);
            }
            else
            {
                MetroMessageBox.Show(this, "The data wasn't saved!");
            }
        }

        //********************************
        // Reloads from the database
        //********************************
        private void btnInvAuthorReload_Click(object sender, EventArgs e)
        {
            AuthorDA listauthors = new AuthorDA();
            GeneralDataManipulation.listOfAuthors = new List<Author>();
            GeneralDataManipulation.listOfAuthors = listauthors.ReadUsers();
            //GeneralDataManipulation.listOfAuthors = AuthorDA.ReadUsers();
            DisplayLVAuthor(GeneralDataManipulation.listOfAuthors);
        }

        //********************************
        // Search for author
        //********************************
        private void btnInvAutSearch_Click(object sender, EventArgs e)
        {
            int number = 0;
            string tSearch = txtInvAutSearch.Text;
            if (cmbInvAutSearch.Text == "Author ID")
            {
                if (Int32.TryParse(tSearch, out number))
                {
                    var newlist = GeneralDataManipulation.listOfAuthors.FindAll(s => s.AuthorID.Equals(Convert.ToInt32(tSearch)));
                    if (newlist.Count > 0) { DisplayLVAuthor(newlist); }
                    else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else
                {
                    MetroMessageBox.Show(this, "You must enter an ID to search", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (cmbInvAutSearch.Text == "First Name")
            {
                var newlist = GeneralDataManipulation.listOfAuthors.FindAll(s => s.AuthorFirstName.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVAuthor(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbInvAutSearch.Text == "Last Name")
            {
                var newlist = GeneralDataManipulation.listOfAuthors.FindAll(s => s.AuthorLastName.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVAuthor(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbInvAutSearch.Text == "E-Mail")
            {
                var newlist = GeneralDataManipulation.listOfAuthors.FindAll(s => s.AuthorEmail.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVAuthor(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }

        //*****************************************************
        // Sets all the information on the form when user
        // clicks on the listview
        //*****************************************************

        private void lvInvAut_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvInvAut.SelectedItems.Count > 0)
            {
                int abcd = Convert.ToInt32(lvInvAut.Items[lvInvAut.SelectedIndices[0]].Text);
                selectInvAuthor = lvInvAut.SelectedIndices[0];
                authorSelect = true;
                indexInvAuthor = GeneralDataManipulation.listOfAuthors.FindIndex(r => r.AuthorID.Equals(abcd));
                txtInvAutID.Text = Convert.ToString(GeneralDataManipulation.listOfAuthors[indexInvAuthor].AuthorID);
                txtInvAutFN.Text = GeneralDataManipulation.listOfAuthors[indexInvAuthor].AuthorFirstName;
                txtInvAutLN.Text = GeneralDataManipulation.listOfAuthors[indexInvAuthor].AuthorLastName;
                txtInvAutEmail.Text = GeneralDataManipulation.listOfAuthors[indexInvAuthor].AuthorEmail;
            }
        }

        //*****************************************************
        // Inventory / Publisher
        // From this part on and until else mentioned, 
        // the code will be related to the Inventory section.
        // Category Subsection
        //*****************************************************

        //*****************************************************
        // Check control variables
        //*****************************************************
        public int indexInvPub;
        public int selectInvPub;

        //********************************
        // Clears all the fields on the
        // author form.
        //********************************

        public void PublisherClear()
        {
            txtInvPubID.Clear();
            txtInvPubName.Clear();
            txtInvPubEmail.Clear();
        }


        //********************************
        // Lists all the publishers
        //********************************

        public void DisplayLVPublisher(List<Publisher> listOfPublisher)
        {
            lvInvPub.Items.Clear();
            foreach (Publisher element in listOfPublisher)
            {
                ListViewItem item = new ListViewItem(Convert.ToString(element.PublishID));
                item.SubItems.Add(Convert.ToString(element.PublisherName));
                item.SubItems.Add(Convert.ToString(element.PublisherEmail));
                lvInvPub.Items.Add(item);
            }
            InventoryComboboxReload();
        }

        //*****************************************************
        // Validates the information for Publisher tab
        //*****************************************************

        private bool isValidPub()
        {
            if (Validator.Validation.IsPresent(txtInvPubID) &&
            Validator.Validation.IsPresent(txtInvPubName) &&
            Validator.Validation.IsPresent(txtInvPubEmail) &&
            Validator.Validation.IsNumber(txtInvPubID) && Validator.Validation.FieldSize(txtInvPubID))
            {
                return true;
            }
            return false;
        }

        //********************************
        // Add new publisher
        //********************************

        private void btnInvPubAdd_Click(object sender, EventArgs e)
        {
            if (isValidPub())
            {
                if (GeneralDataManipulation.listOfPublishers.Any(x => x.PublishID == Convert.ToInt32(txtInvPubID.Text)))
                {
                    MetroMessageBox.Show(this, "The entered value has to be UNIQUE!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Publisher aPublisher = new Publisher();
                    aPublisher.PublishID = Convert.ToInt32(txtInvPubID.Text);
                    aPublisher.PublisherName = txtInvPubName.Text;
                    aPublisher.PublisherEmail = txtInvPubEmail.Text;

                    GeneralDataManipulation.listOfPublishers.Add(aPublisher);
                    DisplayLVPublisher(GeneralDataManipulation.listOfPublishers);
                    PublisherClear();
                }
            }
        }

        //********************************
        // Edit currently selected
        // publisher
        //********************************
        private void btnInvPubEdit_Click(object sender, EventArgs e)
        {
            if (isValidPub())
            {
                var confirmResult = MetroMessageBox.Show(this, "Are you sure to MODIFY this item ?", "Confirm MODIFY!", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    if (GeneralDataManipulation.listOfPublishers.Any(x => x.PublishID == Convert.ToInt32(txtInvPubID.Text)) &&
                    (Convert.ToString(GeneralDataManipulation.listOfPublishers[indexInvPub].PublishID) != txtInvPubID.Text))
                    {
                        MetroMessageBox.Show(this, "The entered value has to be UNIQUE!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        GeneralDataManipulation.listOfPublishers[indexInvPub].PublishID = Convert.ToInt32(txtInvPubID.Text);
                        GeneralDataManipulation.listOfPublishers[indexInvPub].PublisherName = txtInvPubName.Text;
                        GeneralDataManipulation.listOfPublishers[indexInvPub].PublisherEmail = txtInvPubEmail.Text;
                        PublisherClear();
                        DisplayLVPublisher(GeneralDataManipulation.listOfPublishers);
                    }
                }
            }
        }

        //********************************
        // Remove selected publisher
        //********************************
        private void btnInvPubRemove_Click(object sender, EventArgs e)
        {
            var confirmResult = MetroMessageBox.Show(this, "Are you sure to delete this item ?", "Confirm Delete!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if ((selectInvPub != -1) && (indexInvPub != -1) && (publisherSelect == true))
                {
                    publisherSelect = false;
                    GeneralDataManipulation.listOfPublishers.RemoveAt(indexInvPub);
                    this.lvInvAut.Items.RemoveAt(selectInvPub);
                    MetroMessageBox.Show(this, "The item was deleted.");
                }
                else
                {
                    MetroMessageBox.Show(this, "You must select an item to remove.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            PublisherClear();
        }

        //********************************
        // Search case
        //********************************
        private void btnInvPubSearch_Click(object sender, EventArgs e)
        {
            int number = 0;
            string tSearch = txtInvPubSearch.Text;
            if (cmbInvPubSearch.Text == "Publisher ID")
            {
                if (Int32.TryParse(tSearch, out number))
                {
                    var newlist = GeneralDataManipulation.listOfPublishers.FindAll(s => s.PublishID.Equals(Convert.ToInt32(tSearch)));
                    if (newlist.Count > 0) { DisplayLVPublisher(newlist); }
                    else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else
                {
                    MetroMessageBox.Show(this, "You must enter an ID to search", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (cmbInvPubSearch.Text == "Name")
            {
                var newlist = GeneralDataManipulation.listOfPublishers.FindAll(s => s.PublisherName.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVPublisher(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbInvPubSearch.Text == "E-Mail")
            {
                var newlist = GeneralDataManipulation.listOfPublishers.FindAll(s => s.PublisherEmail.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVPublisher(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }

        //********************************
        // Reloads list from backup
        //********************************
        private void btnInvPubRefresh_Click(object sender, EventArgs e)
        {
            DisplayLVPublisher(GeneralDataManipulation.listOfPublishers);
        }

        //********************************
        // Saves list to the database
        //********************************

        private void btnInvPubSave_Click(object sender, EventArgs e)
        {
            var confirmResult = MetroMessageBox.Show(this, "Would you like to save the database to the file?\nIt will overwrite the previous database.", "Confirm Save!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                PublisherDA.WriteUser(GeneralDataManipulation.listOfPublishers);
            }
            else
            {
                MetroMessageBox.Show(this, "The data wasn't saved!");
            }
        }

        //********************************
        // Reloads list from the database
        //********************************
        private void btnInvPubReload_Click(object sender, EventArgs e)
        {
            GeneralDataManipulation.listOfPublishers = new List<Publisher>();
            GeneralDataManipulation.listOfPublishers = PublisherDA.ReadUsers();
            DisplayLVPublisher(GeneralDataManipulation.listOfPublishers);
        }

        //********************************************************
        // Checkers for the listview
        // Displays the info of the selected publisher
        // in the fields when selected an item from the listview
        //********************************************************
        private void lvInvPub_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvInvPub.SelectedItems.Count > 0)
            {
                int abcd = Convert.ToInt32(lvInvPub.Items[lvInvPub.SelectedIndices[0]].Text);
                selectInvPub = lvInvPub.SelectedIndices[0];
                publisherSelect = true;
                indexInvPub = GeneralDataManipulation.listOfPublishers.FindIndex(r => r.PublishID.Equals(abcd));
                txtInvPubID.Text = Convert.ToString(GeneralDataManipulation.listOfPublishers[indexInvPub].PublishID);
                txtInvPubName.Text = GeneralDataManipulation.listOfPublishers[indexInvPub].PublisherName;
                txtInvPubEmail.Text = GeneralDataManipulation.listOfPublishers[indexInvPub].PublisherEmail;
            }
        }


        //*****************************************************
        // Inventory / Category
        // From this part on and until else mentioned, 
        // the code will be related to the Inventory section.
        // Category Subsection
        //*****************************************************


        //*****************************************************
        // Check control variables
        //*****************************************************
        public int indexInvCat;
        public int selectInvCat;

        //********************************
        // Clears all the fields on the
        // category form.
        //********************************

        public void CategoryClear()
        {
            txtInvCatID.Clear();
            txtInvCatName.Clear();
        }

        //********************************
        // Lists all the categories
        //********************************

        public void DisplayLVCategory(List<Category> listOfCategory)
        {
            lvInvCat.Items.Clear();
            foreach (Category element in listOfCategory)
            {
                ListViewItem item = new ListViewItem(Convert.ToString(element.CatID));
                item.SubItems.Add(Convert.ToString(element.CatName));
                lvInvCat.Items.Add(item);
            }
            InventoryComboboxReload();
        }

        //*****************************************************
        // Search function for the category section
        //*****************************************************
        private void btnInvCatSearch_Click(object sender, EventArgs e)
        {
            int number = 0;
            string tSearch = txtInvCatSearch.Text;
            if (cmbInvCatSearch.Text.Equals("Category ID"))
            {
                if (Int32.TryParse(tSearch, out number))
                {
                    var newlist = GeneralDataManipulation.listOfCategory.FindAll(s => s.CatID.Equals(Convert.ToInt32(tSearch)));
                    if (newlist.Count > 0) { DisplayLVCategory(newlist); }
                    else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else
                {
                    MetroMessageBox.Show(this, "You must enter an ID to search", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (cmbInvCatSearch.Text.Equals("Name"))
            {
                var newlist = GeneralDataManipulation.listOfCategory.FindAll(s => s.CatName.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVCategory(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }

        //*****************************************************
        // Adds a new category
        //*****************************************************

        private bool isValidCat()
        {
            if (Validator.Validation.IsPresent(txtInvCatID) &&
            Validator.Validation.IsPresent(txtInvCatName) &&
            Validator.Validation.IsNumber(txtInvCatID) && Validator.Validation.FieldSize(txtInvCatID))
            {
                return true;
            }
            return false;
        }

        //*****************************************************
        // Adds a new Category
        //*****************************************************
        private void btnInvCatAdd_Click(object sender, EventArgs e)
        {
            if (isValidCat())
            {
                if (GeneralDataManipulation.listOfCategory.Any(x => x.CatID == Convert.ToInt32(txtInvCatID.Text)))
                {
                    MetroMessageBox.Show(this, "The entered value has to be UNIQUE!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Category aCategory = new Category();
                    aCategory.CatID = Convert.ToInt32(txtInvCatID.Text);
                    aCategory.CatName = txtInvCatName.Text;

                    GeneralDataManipulation.listOfCategory.Add(aCategory);
                    DisplayLVCategory(GeneralDataManipulation.listOfCategory);
                    CategoryClear();
                }
            }
        }

        //*****************************************************
        // Removes the selected category
        //*****************************************************
        private void btnInvCatRemove_Click(object sender, EventArgs e)
        {
            var confirmResult = MetroMessageBox.Show(this, "Are you sure to delete this item ?", "Confirm Delete!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if ((selectInvCat != -1) && (indexInvCat != -1) && (categorySelect == true))
                {
                    categorySelect = false;
                    GeneralDataManipulation.listOfCategory.RemoveAt(indexInvCat);
                    this.lvInvCat.Items.RemoveAt(selectInvCat);
                    MetroMessageBox.Show(this, "The item was deleted.");
                }
                else
                {
                    MetroMessageBox.Show(this, "You must select an item to remove.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            CategoryClear();
        }

        //*****************************************************
        // Edits the selected category information
        //*****************************************************
        private void btnInvCatEdit_Click(object sender, EventArgs e)
        {
            if (isValidCat())
            {
                var confirmResult = MetroMessageBox.Show(this, "Are you sure to MODIFY this item ?", "Confirm MODIFY!", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    if (GeneralDataManipulation.listOfCategory.Any(x => x.CatID == Convert.ToInt32(txtInvCatID.Text)) &&
                   (Convert.ToString(GeneralDataManipulation.listOfCategory[indexInvCat].CatID) != txtInvCatID.Text))
                    {
                        MetroMessageBox.Show(this, "The entered value has to be UNIQUE!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        GeneralDataManipulation.listOfCategory[indexInvCat].CatID = Convert.ToInt32(txtInvCatID.Text);
                        GeneralDataManipulation.listOfCategory[indexInvCat].CatName = txtInvCatName.Text;
                        CategoryClear();
                        DisplayLVCategory(GeneralDataManipulation.listOfCategory);
                    }
                }
            }
        }

        //*****************************************************
        // Refreshs the listview
        //*****************************************************
        private void btnInvCatRefresh_Click(object sender, EventArgs e)
        {
            DisplayLVCategory(GeneralDataManipulation.listOfCategory);
        }

        //*****************************************************
        // Saves all the information to the database
        //*****************************************************
        private void btnInvCatSave_Click(object sender, EventArgs e)
        {
            var confirmResult = MetroMessageBox.Show(this, "Would you like to save the database to the file?\nIt will overwrite the previous database.", "Confirm Save!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                CategoryDA.WriteUser(GeneralDataManipulation.listOfCategory);
            }
            else
            {
                MetroMessageBox.Show(this, "The data wasn't saved!");
            }
        }

        //*****************************************************
        // Reloads the information from the database
        //*****************************************************
        private void btnInvCatReload_Click(object sender, EventArgs e)
        {
            GeneralDataManipulation.listOfCategory = new List<Category>();
            GeneralDataManipulation.listOfCategory = CategoryDA.ReadUsers();
            DisplayLVCategory(GeneralDataManipulation.listOfCategory);
        }


        //*****************************************************
        // Displays the information on the form according to
        // the selected item on the listview
        //*****************************************************
        private void lvInvCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvInvCat.SelectedItems.Count > 0)
            {
                int abcd = Convert.ToInt32(lvInvCat.Items[lvInvCat.SelectedIndices[0]].Text);
                selectInvCat = lvInvCat.SelectedIndices[0];
                categorySelect = true;
                indexInvCat = GeneralDataManipulation.listOfCategory.FindIndex(r => r.CatID.Equals(abcd));
                txtInvCatID.Text = Convert.ToString(GeneralDataManipulation.listOfCategory[indexInvCat].CatID);
                txtInvCatName.Text = GeneralDataManipulation.listOfCategory[indexInvCat].CatName;
            }
        }

        //*****************************************************
        // Inventory / Type
        // From this part on and until else mentioned, 
        // the code will be related to the Inventory section.
        // Type Subsection
        //*****************************************************


        //*****************************************************
        // Check control variables
        //*****************************************************
        public int indexInvType;
        public int selectInvType;

        //********************************
        // Clears all the fields on the
        // Type form.
        //********************************

        public void TypeClear()
        {
            txtInvTypeID.Clear();
            txtInvTypeName.Clear();
        }

        //********************************
        // Lists all the type
        //********************************

        public void DisplayLVType(List<ItemType> listOfType)
        {
            lvInvType.Items.Clear();
            foreach (ItemType element in listOfType)
            {
                ListViewItem item = new ListViewItem(Convert.ToString(element.TypeId));
                item.SubItems.Add(Convert.ToString(element.TypeName));
                lvInvType.Items.Add(item);
            }
            InventoryComboboxReload();
        }

        //*****************************************************
        // Adds a new type
        //*****************************************************

        private bool isValidType ()
        {
            if(Validator.Validation.IsPresent(txtInvTypeID) &&
            Validator.Validation.IsPresent(txtInvTypeName) &&
            Validator.Validation.IsNumber(txtInvTypeID) && Validator.Validation.FieldSize(txtInvTypeID))
            {
                return true;
            }
            return false;
        }

        //*****************************************************
        // Adds a new TYPE
        //*****************************************************
        private void btnInvTypeAdd_Click(object sender, EventArgs e)
        {
            if (isValidType())
            {
                if (GeneralDataManipulation.listOfType.Any(x => x.TypeId == Convert.ToInt32(txtInvTypeID.Text)))
                {
                    MetroMessageBox.Show(this, "The entered value has to be UNIQUE!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else {
                    ItemType aType = new ItemType();
                    aType.TypeId = Convert.ToInt32(txtInvTypeID.Text);
                    aType.TypeName = txtInvTypeName.Text;

                    GeneralDataManipulation.listOfType.Add(aType);
                    DisplayLVType(GeneralDataManipulation.listOfType);
                    TypeClear();
                }
            }
        }

        //*****************************************************
        // Edits the information according to the selected 
        // object
        //*****************************************************
        private void btnInvTypeEdit_Click(object sender, EventArgs e)
        {
            if (isValidType())
            {
                var confirmResult = MetroMessageBox.Show(this, "Are you sure to MODIFY this item ?", "Confirm MODIFY!", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    if (GeneralDataManipulation.listOfType.Any(x => x.TypeId == Convert.ToInt32(txtInvTypeID.Text)) &&
                    (Convert.ToString(GeneralDataManipulation.listOfType[indexInvType].TypeId) != txtInvTypeID.Text))
                    {
                        MetroMessageBox.Show(this, "The entered value has to be UNIQUE!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        GeneralDataManipulation.listOfType[indexInvType].TypeId = Convert.ToInt32(txtInvTypeID.Text);
                        GeneralDataManipulation.listOfType[indexInvType].TypeName = txtInvTypeName.Text;
                        TypeClear();
                        DisplayLVType(GeneralDataManipulation.listOfType);
                    }
                }
            }
        }

        //*****************************************************
        // Removes the selected Type
        //*****************************************************
        private void btnInvTypeRemove_Click(object sender, EventArgs e)
        {
            var confirmResult = MetroMessageBox.Show(this, "Are you sure to delete this item ?", "Confirm Delete!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if ((selectInvType != -1) && (indexInvType != -1) && (typeSelect == true))
                {
                    typeSelect = false;
                    GeneralDataManipulation.listOfType.RemoveAt(indexInvType);
                    this.lvInvType.Items.RemoveAt(selectInvType);
                    MetroMessageBox.Show(this, "The item was deleted.");
                }
                else
                {
                    MetroMessageBox.Show(this, "You must select an item to remove.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            TypeClear();
        }

        //*****************************************************
        // Search function for the TYPE
        //*****************************************************
        private void btnInvTypeSearch_Click(object sender, EventArgs e)
        {
            int number = 0;
            string tSearch = txtInvTypeSearch.Text;
            if (cmbInvTypeSearch.Text.Equals("Type ID"))
            {
                if (Int32.TryParse(tSearch, out number))
                {
                    var newlist = GeneralDataManipulation.listOfType.FindAll(s => s.TypeId.Equals(Convert.ToInt32(tSearch)));
                    if (newlist.Count > 0) { DisplayLVType(newlist); }
                    else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else
                {
                    MetroMessageBox.Show(this, "You must enter an ID to search", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (cmbInvTypeSearch.Text.Equals("Name"))
            {
                var newlist = GeneralDataManipulation.listOfType.FindAll(s => s.TypeName.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVType(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }

        //*****************************************************
        // Refreshs the listview Type
        //*****************************************************
        private void btnInvTypeRefresh_Click(object sender, EventArgs e)
        {
            DisplayLVType(GeneralDataManipulation.listOfType);
        }

        //*****************************************************
        // Saves all the information to the Type database
        //*****************************************************
        private void btnInvTypeSaveData_Click(object sender, EventArgs e)
        {
            var confirmResult = MetroMessageBox.Show(this, "Would you like to save the database to the file?\nIt will overwrite the previous database.", "Confirm Save!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                ItemTypeDA.WriteUser(GeneralDataManipulation.listOfType);
            }
            else
            {
                MetroMessageBox.Show(this, "The data wasn't saved!");
            }
        }

        //*****************************************************
        // Reloads the information from the Type database
        //*****************************************************
        private void btnInvTypeReloadData_Click(object sender, EventArgs e)
        {
            GeneralDataManipulation.listOfType = new List<ItemType>();
            GeneralDataManipulation.listOfType = ItemTypeDA.ReadUsers();
            DisplayLVType(GeneralDataManipulation.listOfType);
        }

        //*****************************************************
        // Displays all the information on the form
        // according to the selected object
        //*****************************************************
        private void lvInvType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvInvType.SelectedItems.Count > 0)
            {
                int abcd = Convert.ToInt32(lvInvType.Items[lvInvType.SelectedIndices[0]].Text);
                selectInvType = lvInvType.SelectedIndices[0];
                typeSelect = true;
                indexInvType = GeneralDataManipulation.listOfType.FindIndex(r => r.TypeId.Equals(abcd));
                txtInvTypeID.Text = Convert.ToString(GeneralDataManipulation.listOfType[indexInvType].TypeId);
                txtInvTypeName.Text = GeneralDataManipulation.listOfType[indexInvType].TypeName;
            }
        }

        //*****************************************************
        // Refresh the combobox in the item management
        // subsection with all the new stored information
        // either author, category, publisher or type
        //*****************************************************

        void InventoryComboboxReload()
        {
            cmbInvItemAuthor.Items.Clear();
            cmbInvItemCat.Items.Clear();
            cmbInvItemPub.Items.Clear();
            cmbInvItemType.Items.Clear();
            foreach (Author element in GeneralDataManipulation.listOfAuthors)
            {
                cmbInvItemAuthor.Items.Add(element.AuthorFirstName + " " + element.AuthorLastName);
            }
            foreach (Category element in GeneralDataManipulation.listOfCategory)
            {
                cmbInvItemCat.Items.Add(element.CatName);
            }
            foreach (Publisher element in GeneralDataManipulation.listOfPublishers)
            {
                cmbInvItemPub.Items.Add(element.PublisherName);
            }
            foreach (ItemType element in GeneralDataManipulation.listOfType)
            {
                cmbInvItemType.Items.Add(element.TypeName);
            }
        }

        //*****************************************************
        // Inventory / Item Management
        // From this part on and until else mentioned, 
        // the code will be related to the Inventory section.
        // Item Management Subsection
        //*****************************************************

        public int InvItemSelect;
        public int indexInvItem;

        //*****************************************************
        // Displays the items on the main Listview Items
        //*****************************************************
        public void DisplayLVItems(List<Book> listOfItems)
        {
            lvInvItem.Items.Clear();
            foreach (Book element in listOfItems)
            {
                ListViewItem item = new ListViewItem(Convert.ToString(element.BookISBN));
                item.SubItems.Add(Convert.ToString(element.BookTitle));
                item.SubItems.Add(Convert.ToString(element.BookQOH));
                item.SubItems.Add(Convert.ToString(element.BookPublishedDate));
                item.SubItems.Add(Convert.ToString(element.AuthorFirstName));
                item.SubItems.Add(Convert.ToString(element.BookCategory));
                item.SubItems.Add(Convert.ToString(element.ItemType));
                item.SubItems.Add(Convert.ToString(element.BookPublisher));
                item.SubItems.Add(Convert.ToString(element.BookPrice));
                lvInvItem.Items.Add(item);
            }
        }

        //*****************************************************
        // Clears all the fields
        //*****************************************************
        public void itemClear()
        {
            txtInvItemID.Clear();
            txtInvItemName.Clear();
            txtInvItemPrice.Clear();
            txtInvItemQOH.Clear();
            txtInvItemSearch.Clear();
            txtInvItemWeb.Clear();
            cmbInvItemAuthor.SelectedIndex = -1;
            cmbInvItemCat.SelectedIndex = -1;
            cmbInvItemPub.SelectedIndex = -1;
            cmbInvItemType.SelectedIndex = -1;
            picItem.Image = System.Drawing.Image.FromFile(@"..\..\Database\blank.png");
        }

        //*****************************************************
        // Validates the information for ITEM tab
        //*****************************************************
        private bool isValidItem()
        {
            if (
                Validator.Validation.IsPresent(txtInvItemID) &&
                Validator.Validation.IsPresent(txtInvItemName) &&
                Validator.Validation.IsPresent(txtInvItemQOH) &&
                Validator.Validation.IsPresent(cmbInvItemDate) &&
                Validator.Validation.IsPresent(cmbInvItemAuthor) &&
                Validator.Validation.IsPresent(cmbInvItemCat) &&
                Validator.Validation.IsPresent(cmbInvItemType) &&
                Validator.Validation.IsPresent(cmbInvItemPub) &&
                Validator.Validation.IsPresent(txtInvItemPrice) &&
                Validator.Validation.IsPresent(txtInvItemWeb) &&

                Validator.Validation.IsNumber(txtInvItemID) &&
                Validator.Validation.IsNumber(txtInvItemQOH) &&
                Validator.Validation.IsNumber(txtInvItemPrice) &&

                Validator.Validation.FieldSize(txtInvItemID))
            {
                return true;
            }
            return false;
        }

        //*****************************************************
        // Adds a new item
        //*****************************************************
        private void btnInvItemAdd_Click(object sender, EventArgs e)
        {
            if (isValidItem())
            {
                if (GeneralDataManipulation.listOfItems.Any(x => x.BookISBN == Convert.ToInt32(txtInvItemID.Text)))
                {
                    MetroMessageBox.Show(this, "The entered value has to be UNIQUE!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (imgselected == true)
                    {
                        Book aItem = new Book();
                        aItem.BookISBN = Convert.ToInt32(txtInvItemID.Text);
                        aItem.BookTitle = txtInvItemName.Text;
                        aItem.BookPublishedDate = cmbInvItemDate.Text;
                        aItem.AuthorFirstName = cmbInvItemAuthor.Text;
                        aItem.BookCategory = cmbInvItemCat.Text;
                        aItem.ItemType = cmbInvItemType.Text;
                        aItem.BookPublisher = cmbInvItemPub.Text;
                        aItem.BookQOH = Convert.ToInt32(txtInvItemQOH.Text);
                        aItem.BookPrice = Convert.ToDecimal(txtInvItemPrice.Text);
                        aItem.WebLink = txtInvItemWeb.Text;
                        aItem.BookImgPath = imgpath;
                        GeneralDataManipulation.listOfItems.Add(aItem);
                        imgselected = false;
                    }
                    else
                    {
                        var confirmResult = MetroMessageBox.Show(this, "You haven't choose an image.\nWould you like to add the item without one ?", "No image selected", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirmResult == DialogResult.Yes)
                        {
                            Book aItem = new Book();
                            aItem.BookISBN = Convert.ToInt32(txtInvItemID.Text);
                            aItem.BookTitle = txtInvItemName.Text;
                            aItem.BookPublishedDate = cmbInvItemDate.Text;
                            aItem.AuthorFirstName = cmbInvItemAuthor.Text;
                            aItem.BookCategory = cmbInvItemCat.Text;
                            aItem.ItemType = cmbInvItemType.Text;
                            aItem.BookPublisher = cmbInvItemPub.Text;
                            aItem.BookQOH = Convert.ToInt32(txtInvItemQOH.Text);
                            aItem.BookPrice = Convert.ToDecimal(txtInvItemPrice.Text);
                            aItem.WebLink = txtInvItemWeb.Text;
                            aItem.BookImgPath = @"..\..\DataBase\sorry.png";
                            GeneralDataManipulation.listOfItems.Add(aItem);
                            MetroMessageBox.Show(this, "The item was added without an image.\nYou can always edit it and modify in the future.", "No image selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    imgselected = false;
                    DisplayLVItems(GeneralDataManipulation.listOfItems);
                    itemClear();
                }
            }
        }

        //*****************************************************
        // Function for picture implementing
        //*****************************************************
        private void btnInvItemPic_Click(object sender, EventArgs e)
        {
            imgpath = @"..\..\DataBase\sorry.png";
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.FilterIndex = 1;
            dlg.Multiselect = false;
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.bmp) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.bmp";
            dlg.ShowDialog();


            imgpath = dlg.FileName;
            imgEdit = imgpath;

            if (imgpath.Length != 0)
            {
                imgselected = true;
                picItem.Image = System.Drawing.Image.FromFile(imgpath);
                if (!File.Exists(@"..\..\DataBase\" + Path.GetFileName(imgpath)))
                {
                    File.Copy(imgpath, @"..\..\DataBase\" + Path.GetFileName(imgpath));
                }
            }
            else
            {
                MetroMessageBox.Show(this,"Please select a valid image format.\nYou can leave it in blank for no image display.","Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        //*****************************************************
        // Saves all the information to the database
        //*****************************************************
        private void btnInvItemSaveData_Click(object sender, EventArgs e)
        {
            var confirmResult = MetroMessageBox.Show(this, "Would you like to save the database to the file?\nIt will overwrite the previous database.", "Confirm Save!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                ItemsDA.WriteUser(GeneralDataManipulation.listOfItems);
            }
            else
            {
                MetroMessageBox.Show(this, "The data wasn't saved!");
            }
        }

        //*****************************************************
        // Reloads the information from the database
        //*****************************************************

        private void btnInvItemReloadData_Click(object sender, EventArgs e)
        {
            GeneralDataManipulation.listOfItems = new List<Book>();
            GeneralDataManipulation.listOfItems = ItemsDA.ReadUsers();
            DisplayLVItems(GeneralDataManipulation.listOfItems);
        }

        //*****************************************************
        // Refreshs the listview of items with the temporary
        // data
        //*****************************************************
        private void btnInvItemRefreshData_Click(object sender, EventArgs e)
        {
            DisplayLVItems(GeneralDataManipulation.listOfItems);
        }

        //*****************************************************
        // Remove item function
        //*****************************************************
        private void btnInvItemRem_Click(object sender, EventArgs e)
        {
            var confirmResult = MetroMessageBox.Show(this, "Are you sure to delete this item ?", "Confirm Delete!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if ((InvItemSelect != -1) && (indexInvItem != -1) && (itemSelect == true))
                {
                    itemSelect = false;
                    GeneralDataManipulation.listOfItems.RemoveAt(indexInvItem);
                    this.lvInvItem.Items.RemoveAt(InvItemSelect);
                    MetroMessageBox.Show(this, "The item was deleted.");
                }
                else
                {
                    MetroMessageBox.Show(this, "You must select an item to remove.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //*****************************************************
        // Edit item function
        //*****************************************************
        private void btnInvItemEdit_Click(object sender, EventArgs e)
        {
            if (isValidItem())
            {
                var confirmResult = MetroMessageBox.Show(this, "Are you sure to MODIFY this item ?", "Confirm MODIFY!", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    if (GeneralDataManipulation.listOfItems.Any(x => x.BookISBN == Convert.ToInt32(txtInvItemID.Text)) &&
                    (Convert.ToString(GeneralDataManipulation.listOfItems[indexInvItem].BookISBN) != txtInvItemID.Text))
                    {
                        MetroMessageBox.Show(this, "The entered value has to be UNIQUE!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (itemSelect == true)
                        {
                            GeneralDataManipulation.listOfItems[indexInvItem].BookISBN = Convert.ToInt64(txtInvItemID.Text);
                            GeneralDataManipulation.listOfItems[indexInvItem].BookTitle = txtInvItemName.Text;
                            GeneralDataManipulation.listOfItems[indexInvItem].BookQOH = Convert.ToInt32(txtInvItemQOH.Text);
                            GeneralDataManipulation.listOfItems[indexInvItem].BookPublishedDate = cmbInvItemDate.Text;
                            GeneralDataManipulation.listOfItems[indexInvItem].AuthorFirstName = cmbInvItemAuthor.Text;
                            GeneralDataManipulation.listOfItems[indexInvItem].BookCategory = cmbInvItemCat.Text;
                            GeneralDataManipulation.listOfItems[indexInvItem].ItemType = cmbInvItemType.Text;
                            GeneralDataManipulation.listOfItems[indexInvItem].BookPublisher = cmbInvItemPub.Text;
                            GeneralDataManipulation.listOfItems[indexInvItem].BookPrice = Convert.ToDecimal(txtInvItemPrice.Text);
                            GeneralDataManipulation.listOfItems[indexInvItem].WebLink = txtInvItemWeb.Text;
                            if (imgEdit.Length != 0)
                            {
                                GeneralDataManipulation.listOfItems[indexInvItem].BookImgPath = imgEdit;
                            }
                            DisplayLVItems(GeneralDataManipulation.listOfItems);
                        }
                    }
                }
            }
        }

        //*****************************************************
        // Checkers and info display according to what the 
        // user has selected
        //*****************************************************
        private void lvInvItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvInvItem.SelectedItems.Count > 0)
            {
                
                itemSelect = true;
                int abcd = Convert.ToInt32(lvInvItem.Items[lvInvItem.SelectedIndices[0]].Text);
                InvItemSelect = lvInvItem.SelectedIndices[0];
                indexInvItem = GeneralDataManipulation.listOfItems.FindIndex(r => r.BookISBN.Equals(abcd));
                txtInvItemID.Text = Convert.ToString(GeneralDataManipulation.listOfItems[indexInvItem].BookISBN);
                txtInvItemName.Text = GeneralDataManipulation.listOfItems[indexInvItem].BookTitle;
                txtInvItemQOH.Text = Convert.ToString(GeneralDataManipulation.listOfItems[indexInvItem].BookQOH);
                cmbInvItemDate.Text = GeneralDataManipulation.listOfItems[indexInvItem].BookPublishedDate;
                cmbInvItemAuthor.Text = GeneralDataManipulation.listOfItems[indexInvItem].AuthorFirstName;
                cmbInvItemCat.Text = GeneralDataManipulation.listOfItems[indexInvItem].BookCategory;
                cmbInvItemType.Text = GeneralDataManipulation.listOfItems[indexInvItem].ItemType;
                cmbInvItemPub.Text = GeneralDataManipulation.listOfItems[indexInvItem].BookPublisher;
                txtInvItemPrice.Text = Convert.ToString(GeneralDataManipulation.listOfItems[indexInvItem].BookPrice);
                txtInvItemWeb.Text = GeneralDataManipulation.listOfItems[indexInvItem].WebLink;
                if (File.Exists(GeneralDataManipulation.listOfItems[indexInvItem].BookImgPath))
                {
                    picItem.Image = System.Drawing.Image.FromFile(@"..\..\DataBase\" + Path.GetFileName(GeneralDataManipulation.listOfItems[indexInvItem].BookImgPath));
                    imgEdit = @"..\..\DataBase\" + Path.GetFileName(GeneralDataManipulation.listOfItems[indexInvItem].BookImgPath);
                }
                else {
                    picItem.Image = System.Drawing.Image.FromFile(@"..\..\Database\sorry.png");
                    imgEdit = @"..\..\Database\sorry.png";
                }
            }
        }

        //*****************************************************
        // Runs the website when the user click the 
        // textbox button
        //*****************************************************
        private void txtInvItemWeb_ButtonClick(object sender, EventArgs e)
        {
            if (txtInvItemWeb.Text.Length != 0)
            {
                System.Diagnostics.Process.Start(txtInvItemWeb.Text);
            }
        }

        //*****************************************************
        // Search function for the INVENTORY tab
        // Item Management section
        //*****************************************************
        private void btnInvItemSearch_Click(object sender, EventArgs e)
        {
            int number = 0;
            string tSearch = txtInvItemSearch.Text;
            if (cmbInvItemSearch.Text == "Product ID/ISBN")
            {
                if (Int32.TryParse(tSearch, out number))
                {
                    var newlist = GeneralDataManipulation.listOfItems.FindAll(s => s.BookISBN.Equals(Convert.ToInt32(tSearch)));
                    if (newlist.Count > 0) { DisplayLVItems(newlist); }
                    else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else
                {
                    MetroMessageBox.Show(this, "You must enter an ID to search", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (cmbInvItemSearch.Text == "Product Name")
            {
                var newlist = GeneralDataManipulation.listOfItems.FindAll(s => s.BookTitle.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVItems(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbInvItemSearch.Text == "QOH")
            {
                var newlist = GeneralDataManipulation.listOfItems.FindAll(s => s.BookQOH.Equals(Convert.ToInt32(tSearch)));
                if (newlist.Count > 0) { DisplayLVItems(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbInvItemSearch.Text == "Published")
            {
                var newlist = GeneralDataManipulation.listOfItems.FindAll(s => s.BookPublishedDate.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVItems(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbInvItemSearch.Text == "Author")
            {
                var newlist = GeneralDataManipulation.listOfItems.FindAll(s => s.AuthorFirstName.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVItems(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbInvItemSearch.Text == "Category")
            {
                var newlist = GeneralDataManipulation.listOfItems.FindAll(s => s.BookCategory.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVItems(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbInvItemSearch.Text == "Type")
            {
                var newlist = GeneralDataManipulation.listOfItems.FindAll(s => s.ItemType.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVItems(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbInvItemSearch.Text == "Publisher")
            {
                var newlist = GeneralDataManipulation.listOfItems.FindAll(s => s.BookPublisher.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVItems(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbInvItemSearch.Text == "Unit Price")
            {
                var newlist = GeneralDataManipulation.listOfItems.FindAll(s => s.BookPrice.Equals(Convert.ToDecimal(tSearch)));
                if (newlist.Count > 0) { DisplayLVItems(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbInvItemSearch.Text == "Website")
            {
                var newlist = GeneralDataManipulation.listOfItems.FindAll(s => s.WebLink.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLVItems(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }

        //*****************************************************
        // ORDERS
        // From this part on and until else mentioned, 
        // the code will be related to the ORDERS section.
        //*****************************************************

        void OrderComboboxReload()
        {
            cmbOrderClient.Items.Clear();
            cmbOrderItem.Items.Clear();
            cmbOrderQuantity.Items.Clear();
            cmbOrderPlacedBy.Items.Clear();

            foreach (Client element in GeneralDataManipulation.listOfClients)
            {
                cmbOrderClient.Items.Add(element.ClientId + "," + element.ClientName);
            }
            foreach (Book element in GeneralDataManipulation.listOfItems)
            {
                cmbOrderItem.Items.Add(element.BookISBN + "," + element.BookTitle);
            }
            foreach (Employee element in GeneralDataManipulation.listOfEmployee)
            {
                if (element.EmpPosition == "Order Clerk")
                {
                    cmbOrderPlacedBy.Items.Add(element.EmpFName + " " + element.EmpLName);
                }
            }
        }

        //*****************************************************
        // Resets the entered data in the ORDERS data
        //*****************************************************
        void ordersClear()
        {
            cmbOrderClient.SelectedIndex = -1;
            cmbOrderItem.SelectedIndex = -1;
            cmbOrderQuantity.SelectedIndex = -1;
            cmbOrderPlacedBy.SelectedIndex = -1;
            cmbOrderedBy.SelectedIndex = -1;
            txtOrderID.Clear();
            txtOrderSearch.Clear();
            txtOrderTotal.Clear();
            txtOrderUnitPrice.Clear();
            txtClientCredit.Clear();
        }

        //*****************************************************
        // Function to display the information in the listview
        //*****************************************************
        public void DisplayLvOrders(List<Order> listOfOrders)
        {
            lvOrder.Items.Clear();
            foreach (Order element in listOfOrders)
            {
                ListViewItem item = new ListViewItem(Convert.ToString(element.OrderID));
                item.SubItems.Add(Convert.ToString(element.ClientID));
                item.SubItems.Add(Convert.ToString(element.ItemID));
                item.SubItems.Add(element.ItemName);
                item.SubItems.Add(Convert.ToString(element.UnitPrice));
                item.SubItems.Add(Convert.ToString(element.Quantity));
                item.SubItems.Add(element.OrderedBy);
                item.SubItems.Add(element.OrderDate);
                item.SubItems.Add(Convert.ToString(element.OrderTotal));
                item.SubItems.Add(element.PlacedBy);
                lvOrder.Items.Add(item);
            }
        }

        //*****************************************************
        // Validation function for the ORDER tab
        //*****************************************************
        private bool isValidOrder()
        {
            if (
            Validator.Validation.IsPresent(txtOrderID) &&
            Validator.Validation.IsPresent(cmbOrderClient) &&
            Validator.Validation.IsPresent(cmbOrderDate) &&
            Validator.Validation.IsPresent(cmbOrderItem) &&
            Validator.Validation.IsPresent(cmbOrderQuantity) &&
            Validator.Validation.IsPresent(cmbOrderedBy) &&
            Validator.Validation.IsPresent(cmbOrderPlacedBy) &&

            Validator.Validation.IsNumber(txtOrderID) && Validator.Validation.FieldSize(txtOrderID))
            {
                return true;
            }
            return false;
        }

        //*****************************************************
        // Adds a new order to the database
        //*****************************************************
        private void btnOrderAdd_Click(object sender, EventArgs e)
        {
            if (isValidOrder())
            {
                if (GeneralDataManipulation.listOfOrders.Any(x => x.OrderID == Convert.ToInt32(txtOrderID.Text)))
                {
                    MetroMessageBox.Show(this, "The entered value has to be UNIQUE!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string[] aClient = cmbOrderClient.Text.Split(',');
                    int bClient = Convert.ToInt32(aClient[0]);

                    string[] aItem = cmbOrderItem.Text.Split(',');
                    string bItemID = aItem[0];
                    string bItemName = aItem[1];

                    if (GeneralDataManipulation.listOfItems[indexCmbItem].BookQOH < Convert.ToInt32(cmbOrderQuantity.Text))
                    {
                        MetroMessageBox.Show(this, "The maximum quantity available for new orders is: " + GeneralDataManipulation.listOfItems[indexCmbItem].BookQOH, "Check quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);                        
                    }

                    else if (
                        (GeneralDataManipulation.listOfClients[indexCmbClient].ClientCredit >= Convert.ToDecimal(txtOrderTotal.Text)) &&
                        (GeneralDataManipulation.listOfItems[indexCmbItem].BookQOH >= Convert.ToInt32(cmbOrderQuantity.Text))
                        )
                    {
                        Order aOrder = new Order();

                        aOrder.OrderID = Convert.ToInt32(txtOrderID.Text);
                        aOrder.ClientID = bClient;
                        aOrder.ItemID = Convert.ToInt32(bItemID);
                        aOrder.ItemName = bItemName;
                        aOrder.UnitPrice = Convert.ToDecimal(txtOrderUnitPrice.Text);

                        aOrder.Quantity = Convert.ToInt32(cmbOrderQuantity.Text);
                        aOrder.OrderedBy = cmbOrderedBy.Text;
                        aOrder.OrderDate = cmbOrderDate.Text;
                        aOrder.OrderTotal = Convert.ToDecimal(txtOrderTotal.Text);
                        aOrder.PlacedBy = cmbOrderPlacedBy.Text;

                        GeneralDataManipulation.listOfOrders.Add(aOrder);

                        GeneralDataManipulation.listOfClients[indexCmbClient].ClientCredit -= Convert.ToDecimal(txtOrderTotal.Text);
                        GeneralDataManipulation.listOfItems[indexCmbItem].BookQOH -= Convert.ToInt32(cmbOrderQuantity.Text);

                        DisplayLvOrders(GeneralDataManipulation.listOfOrders);

                        ordersClear();
                    }
                }
            }
        }

        //*****************************************************
        // Validator/item place according to what is selected
        // on the combobox
        //*****************************************************
        private void cmbOrderItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOrderItem.SelectedIndex != -1)
            {
                cmbOrderQuantity.Items.Clear();
                txtOrderTotal.Clear();

                string[] aSel = cmbOrderItem.Text.Split(',');
                int abcd = Convert.ToInt32(aSel[0]);
                indexCmbItem = GeneralDataManipulation.listOfItems.FindIndex(r => r.BookISBN.Equals(abcd));
                txtOrderUnitPrice.Text = Convert.ToString(GeneralDataManipulation.listOfItems[indexCmbItem].BookPrice);
                if (GeneralDataManipulation.listOfItems[indexCmbItem].BookQOH == 0)
                {
                    MetroMessageBox.Show(this, "The selected item is out of stock.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                for (int i = 1; i <= GeneralDataManipulation.listOfItems[indexCmbItem].BookQOH; i++)
                {
                    cmbOrderQuantity.Items.Add(i);
                }
            }
        }

        //*****************************************************
        // Search function for the ORDER tab
        //*****************************************************
        private void btnOrderSearch_Click(object sender, EventArgs e)
        {
            decimal number = 0;
            string tSearch = txtOrderSearch.Text;
            if (cmbOrderSearch.Text == "Order ID")
            {
                if (Decimal.TryParse(tSearch, out number))
                {
                    var newlist = GeneralDataManipulation.listOfOrders.FindAll(s => s.OrderID.Equals(Convert.ToInt32(tSearch)));
                    if (newlist.Count > 0) { DisplayLvOrders(newlist); }
                    else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else
                {
                    MetroMessageBox.Show(this, "You must enter the ORDER ID to search", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (cmbOrderSearch.Text == "Client ID")
            {
                if (Decimal.TryParse(tSearch, out number))
                {
                    var newlist = GeneralDataManipulation.listOfOrders.FindAll(s => s.ClientID.Equals(Convert.ToInt32(tSearch)));
                    if (newlist.Count > 0) { DisplayLvOrders(newlist); }
                    else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else
                {
                    MetroMessageBox.Show(this, "You must enter the CLIENT ID to search", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (cmbOrderSearch.Text == "Item ID")
            {
                if (Decimal.TryParse(tSearch, out number))
                {
                    var newlist = GeneralDataManipulation.listOfOrders.FindAll(s => s.ItemID.Equals(Convert.ToInt32(tSearch)));
                    if (newlist.Count > 0) { DisplayLvOrders(newlist); }
                    else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else
                {
                    MetroMessageBox.Show(this, "You must enter the ITEM ID to search", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }


            if (cmbOrderSearch.Text == "Item Name")
            {
                var newlist = GeneralDataManipulation.listOfOrders.FindAll(s => s.ItemName.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLvOrders(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbOrderSearch.Text == "Unit Price")
            {
                if (Decimal.TryParse(tSearch, out number))
                {
                    var newlist = GeneralDataManipulation.listOfOrders.FindAll(s => s.UnitPrice.Equals(Convert.ToDecimal(tSearch)));
                    if (newlist.Count > 0) { DisplayLvOrders(newlist); }
                    else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else
                {
                    MetroMessageBox.Show(this, "You must enter a NUMBER to search", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (cmbOrderSearch.Text == "Quantity")
            {
                if (Decimal.TryParse(tSearch, out number))
                {
                    var newlist = GeneralDataManipulation.listOfOrders.FindAll(s => s.Quantity.Equals(Convert.ToInt32(tSearch)));
                    if (newlist.Count > 0) { DisplayLvOrders(newlist); }
                    else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else
                {
                    MetroMessageBox.Show(this, "You must enter a NUMBER to search", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (cmbOrderSearch.Text == "Ordered By")
            {
                var newlist = GeneralDataManipulation.listOfOrders.FindAll(s => s.OrderedBy.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLvOrders(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbOrderSearch.Text == "Date")
            {
                var newlist = GeneralDataManipulation.listOfOrders.FindAll(s => s.OrderDate.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLvOrders(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            if (cmbOrderSearch.Text == "Order Total")
            {
                if (Decimal.TryParse(tSearch, out number))
                {
                    var newlist = GeneralDataManipulation.listOfOrders.FindAll(s => s.OrderTotal.Equals(Convert.ToDecimal(tSearch)));
                    if (newlist.Count > 0) { DisplayLvOrders(newlist); }
                    else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else
                {
                    MetroMessageBox.Show(this, "You must enter a NUMBER to search", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (cmbOrderSearch.Text == "Placed By")
            {
                var newlist = GeneralDataManipulation.listOfOrders.FindAll(s => s.PlacedBy.Equals(tSearch));
                if (newlist.Count > 0) { DisplayLvOrders(newlist); }
                else { MetroMessageBox.Show(this, "Not found!", "Not found!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }

        //*****************************************************
        // Validator/item place according to what is selected
        // on the combobox
        //*****************************************************
        private void cmbOrderQuantity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOrderQuantity.SelectedIndex != -1)
            {
                txtOrderTotal.Text = Convert.ToString(Convert.ToInt32(cmbOrderQuantity.Text) * Convert.ToDecimal(txtOrderUnitPrice.Text)); 
            }
        }

        //*****************************************************
        // Validator/item place according to what is selected
        // on the combobox
        //*****************************************************
        private void cmbOrderClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOrderClient.SelectedIndex != -1)
            {
                txtClientCredit.Clear();
                string[] aSel = cmbOrderClient.Text.Split(',');
                int abcd = Convert.ToInt32(aSel[0]);
                indexCmbClient = GeneralDataManipulation.listOfClients.FindIndex(r => r.ClientId.Equals(abcd));
                txtClientCredit.Text = Convert.ToString(GeneralDataManipulation.listOfClients[indexCmbClient].ClientCredit);
            }
        }
        //*****************************************************
        // Saves the information to the ORDER database
        //*****************************************************
        private void btnOrderSave_Click(object sender, EventArgs e)
        {
            var confirmResult = MetroMessageBox.Show(this, "Would you like to save the database to the file?\nIt will overwrite the previous database.", "Confirm Save!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                OrdersDA.WriteUser(GeneralDataManipulation.listOfOrders);
                ItemsDA.WriteUser(GeneralDataManipulation.listOfItems);
                ClientDA.WriteUser(GeneralDataManipulation.listOfClients);
            }
            else
            {
                MetroMessageBox.Show(this, "The data wasn't saved!");
            }
        }

        //*****************************************************
        // Order checker variables
        //*****************************************************
        public int orderIndices;
        public int indexOrder;

        //*****************************************************
        // Validator/item place according to what is selected
        // on the listview ORDER
        //*****************************************************
        private void lvOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvOrder.SelectedItems.Count > 0)
            {
                orderSelect = true;
                int a = Convert.ToInt32(lvOrder.Items[lvOrder.SelectedIndices[0]].Text);
                orderIndices = lvOrder.SelectedIndices[0];
                indexOrder = GeneralDataManipulation.listOfOrders.FindIndex(r => r.OrderID.Equals(a));

                int b = Convert.ToInt32(lvOrder.Items[lvOrder.SelectedIndices[0]].SubItems[1].Text);
                indexSales = GeneralDataManipulation.listOfClients.FindIndex(r => r.ClientId.Equals(b));

                int c = Convert.ToInt32(lvOrder.Items[lvOrder.SelectedIndices[0]].SubItems[2].Text);
                indexInvItem = GeneralDataManipulation.listOfItems.FindIndex(r => r.BookISBN.Equals(c));

                txtOrderID.Text = Convert.ToString(a);
                cmbOrderedBy.Text = GeneralDataManipulation.listOfOrders[orderIndices].OrderedBy;
                cmbOrderPlacedBy.Text = GeneralDataManipulation.listOfOrders[orderIndices].PlacedBy;

                cmbOrderClient.Text = Convert.ToString(
                    GeneralDataManipulation.listOfClients[indexSales].ClientId + "," +
                    GeneralDataManipulation.listOfClients[indexSales].ClientName
                    );

                cmbOrderItem.Text = Convert.ToString(
                    GeneralDataManipulation.listOfItems[indexInvItem].BookISBN + "," +
                    GeneralDataManipulation.listOfItems[indexInvItem].BookTitle
                );

                cmbOrderDate.Text = GeneralDataManipulation.listOfOrders[indexOrder].OrderDate;

                cmbOrderQuantity.Items.Clear();

                for (int i = 1; i <= GeneralDataManipulation.listOfItems[indexCmbItem].BookQOH + 
                    GeneralDataManipulation.listOfOrders[indexOrder].Quantity; i++)
                {
                    cmbOrderQuantity.Items.Add(i);
                }
                cmbOrderQuantity.Text = Convert.ToString(GeneralDataManipulation.listOfOrders[indexOrder].Quantity);
            }
        }

        //*****************************************************
        // Removes a order and restores client credit and
        // item stock
        //*****************************************************
        private void btnOrderRem_Click(object sender, EventArgs e)
        {
            var confirmResult = MetroMessageBox.Show(this, "Are you sure to delete this item ?", "Confirm Delete!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if ((lvOrder.SelectedItems.Count > 0) && (orderSelect == true))
                {
                    orderSelect = false;
                    int a = Convert.ToInt32(lvOrder.Items[lvOrder.SelectedIndices[0]].Text);
                    orderIndices = lvOrder.SelectedIndices[0];
                    indexOrder = GeneralDataManipulation.listOfOrders.FindIndex(r => r.OrderID.Equals(a));

                    int b = Convert.ToInt32(lvOrder.Items[lvOrder.SelectedIndices[0]].SubItems[1].Text);
                    indexSales = GeneralDataManipulation.listOfClients.FindIndex(r => r.ClientId.Equals(b));

                    int c = Convert.ToInt32(lvOrder.Items[lvOrder.SelectedIndices[0]].SubItems[2].Text);
                    indexInvItem = GeneralDataManipulation.listOfItems.FindIndex(r => r.BookISBN.Equals(c));

                    GeneralDataManipulation.listOfClients[indexCmbClient].ClientCredit += Convert.ToDecimal(txtOrderTotal.Text);
                    GeneralDataManipulation.listOfItems[indexCmbItem].BookQOH += Convert.ToInt32(cmbOrderQuantity.Text);


                    GeneralDataManipulation.listOfOrders.RemoveAt(indexOrder);
                    this.lvOrder.Items.RemoveAt(orderIndices);
                    ordersClear();
                    OrderComboboxReload();
                }
                else
                {
                    MetroMessageBox.Show(this, "You must select an item to remove.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        //*****************************************************
        // Reloads the information from the database 
        //*****************************************************
        private void btnOrderReload_Click(object sender, EventArgs e)
        {
            GeneralDataManipulation.listOfEmployee = new List<Employee>();
            GeneralDataManipulation.listOfEmployee = EmployeeDA.ReadUsers();

            GeneralDataManipulation.listOfItems = new List<Book>();
            GeneralDataManipulation.listOfItems = ItemsDA.ReadUsers();

            GeneralDataManipulation.listOfClients = new List<Client>();
            GeneralDataManipulation.listOfClients = ClientDA.ReadUsers();

            GeneralDataManipulation.listOfOrders = new List<Order>();
            GeneralDataManipulation.listOfOrders = OrdersDA.ReadUsers();
            DisplayLvOrders(GeneralDataManipulation.listOfOrders);

            OrderComboboxReload();
            salesClear();
        }


        private void cmbOrderDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime abcd = Convert.ToDateTime(cmbOrderDate.Text);
            abcd = abcd.AddDays(3);
            cmbShippingDate.Enabled = true;
            cmbShippingDate.Text = Convert.ToString(abcd.Day + "/" + abcd.Month + "/" + abcd.Year);
            cmbShippingDate.Enabled = false;
        }

        //*****************************************************
        // Function to edit the order according to the selected
        // information
        //*****************************************************
        private void btnOrderEdit_Click(object sender, EventArgs e)
        {
            if (isValidOrder())
            {
                if (GeneralDataManipulation.listOfOrders.Any(x => x.OrderID == Convert.ToInt32(txtOrderID.Text)) &&
                    (Convert.ToString(GeneralDataManipulation.listOfOrders[indexOrder].OrderID) != txtOrderID.Text))
                {
                    MetroMessageBox.Show(this, "The entered value has to be UNIQUE!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var confirmResult = MetroMessageBox.Show(this, "Are you sure to MODIFY this item ?", "Confirm MODIFY!", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        if ((lvOrder.SelectedItems.Count > 0) && (orderSelect == true))
                        {
                            if (GeneralDataManipulation.listOfClients[indexCmbClient].ClientCredit < Convert.ToDecimal(txtOrderTotal.Text))
                            {
                                MetroMessageBox.Show(this, "The client has insuficient funds!");
                            }
                            else
                            {
                                orderSelect = false;
                                int a = Convert.ToInt32(lvOrder.Items[lvOrder.SelectedIndices[0]].Text);
                                orderIndices = lvOrder.SelectedIndices[0];
                                indexOrder = GeneralDataManipulation.listOfOrders.FindIndex(r => r.OrderID.Equals(a));

                                int b = Convert.ToInt32(lvOrder.Items[lvOrder.SelectedIndices[0]].SubItems[1].Text);
                                indexSales = GeneralDataManipulation.listOfClients.FindIndex(r => r.ClientId.Equals(b));

                                int c = Convert.ToInt32(lvOrder.Items[lvOrder.SelectedIndices[0]].SubItems[2].Text);
                                indexInvItem = GeneralDataManipulation.listOfItems.FindIndex(r => r.BookISBN.Equals(c));

                                GeneralDataManipulation.listOfClients[indexSales].ClientCredit += GeneralDataManipulation.listOfOrders[indexOrder].OrderTotal;
                                GeneralDataManipulation.listOfItems[indexInvItem].BookQOH += GeneralDataManipulation.listOfOrders[indexOrder].Quantity;

                                GeneralDataManipulation.listOfOrders.RemoveAt(indexOrder);
                                this.lvOrder.Items.RemoveAt(orderIndices);

                                string[] aClient = cmbOrderClient.Text.Split(',');
                                int bClient = Convert.ToInt32(aClient[0]);

                                string[] aItem = cmbOrderItem.Text.Split(',');
                                string bItemID = aItem[0];
                                string bItemName = aItem[1];


                                if (
                                    (GeneralDataManipulation.listOfClients[indexCmbClient].ClientCredit >= Convert.ToDecimal(txtOrderTotal.Text)) &&
                                    (GeneralDataManipulation.listOfItems[indexCmbItem].BookQOH >= Convert.ToInt32(cmbOrderQuantity.Text))
                                    )
                                {
                                    Order aOrder = new Order();

                                    aOrder.OrderID = Convert.ToInt32(txtOrderID.Text);
                                    aOrder.ClientID = bClient;
                                    aOrder.ItemID = Convert.ToInt32(bItemID);
                                    aOrder.ItemName = bItemName;
                                    aOrder.UnitPrice = Convert.ToDecimal(txtOrderUnitPrice.Text);

                                    aOrder.Quantity = Convert.ToInt32(cmbOrderQuantity.Text);
                                    aOrder.OrderedBy = cmbOrderedBy.Text;
                                    aOrder.OrderDate = cmbOrderDate.Text;
                                    aOrder.OrderTotal = Convert.ToDecimal(txtOrderTotal.Text);
                                    aOrder.PlacedBy = cmbOrderPlacedBy.Text;

                                    GeneralDataManipulation.listOfOrders.Add(aOrder);

                                    GeneralDataManipulation.listOfClients[indexCmbClient].ClientCredit -= Convert.ToDecimal(txtOrderTotal.Text);
                                    GeneralDataManipulation.listOfItems[indexCmbItem].BookQOH -= Convert.ToInt32(cmbOrderQuantity.Text);
                                }
                                DisplayLvOrders(GeneralDataManipulation.listOfOrders);

                                ordersClear();
                            }
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "You must select an item to MODIFY.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }          
            }
        }

        //*****************************************************
        // Refresh the listview Order with the information
        // from the backup
        //*****************************************************
        private void btnOrderRefresh_Click(object sender, EventArgs e)
        {
            DisplayLvOrders(GeneralDataManipulation.listOfOrders);
        }

        //*****************************************************
        // About the program
        //*****************************************************
        private void btnAbout_Click(object sender, EventArgs e)
        {
            Form About = new AboutForm();
            About.ShowDialog();
        }
    }
}
