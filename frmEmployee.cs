using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program_8
{
    public partial class frmEmployee : Form
    {
        // Create a list
        List<clsEmployee> myEmployees = new List<clsEmployee>();

        public frmEmployee()
        {
            InitializeComponent();
        }

        private void frmEmployeeInfo_Load(object sender, EventArgs e)
        {
            // Call the add button upon loading the form
            btnAdd_Click(sender, e);
        }

        void ClearControls()
        {
            // Clear each textbox value and set the values of the numeric updowns to 0;
            foreach (Control control in grpDetail.Controls)
            {
                if (control is TextBox)
                    ((TextBox)control).Clear();
                if (control is NumericUpDown)
                    ((NumericUpDown)control).Value = 0;
            }

            // Clear labels
            lblYearlyHours.Text = "0";
            lblYearlySalary.Text = "0";
        }

        void EnableControls(bool enabled)
        {
            // Set the Add, Edit, and Delete buttons equal to the boolean passed
            btnAdd.Enabled = enabled;
            btnEdit.Enabled = enabled;
            btnDelete.Enabled = enabled;

            // Set the listbox equal to the boolean passed
            lstEmployees.Enabled = enabled;

            // Set the groupbox to the opposite of the boolean passed
            grpDetail.Enabled = !enabled;
        }

        void LoadEmployeeDetail(int ID)
        {
            // Check to see if the ID is a valid number
            if (ID >= 0 && ID < myEmployees.Count)
            {
                // Get the employee based on the ID
                clsEmployee employee = myEmployees[ID];

                // Set the text property of the textbox controls to the employee's details
                txtLastName.Text = employee.LastName;
                txtFirstName.Text = employee.FirstName;

                // Set the value property of the numeric updowns
                numRate.Value = (decimal)employee.Rate;
                numHours.Value = employee.WeeklyHours;

                // Update the labels
                lblYearlyHours.Text = employee.AnnualHours.ToString();
                lblYearlySalary.Text = employee.AnnualRate.ToString("C");
            }
            else
            {
                // If the ID is out of bounds, display an error message
                MessageBox.Show("Error: Invalid Employee ID");
            }
        }

        void DisplayEmployeeList()
        {
            // Clear the listbox
            lstEmployees.Items.Clear();

            // Loop through the myEmployee list
            for (int i = 0; i < myEmployees.Count; i++)
            {
                lstEmployees.Items.Add(myEmployees[i]);
            }

            // Check if the employee count is greater than 0
            if (myEmployees.Count > 0)
            {
                // Set the selected index to 0
                lstEmployees.SelectedIndex = 0;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Disable controls
            EnableControls(false);

            // Clear the text and numeric up-down controls in grpDetail
            ClearControls();

            // Set the tag of the update button to "A"
            btnUpdate.Tag = "A";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Check to see if an employee was selected
            if (lstEmployees.SelectedIndex >= 0)
            {
                // Disable controls
                EnableControls(false);

                // Get the selected employee's ID from the list's SelectedIndex
                int selectedIndex = lstEmployees.SelectedIndex;

                // Load the employee's details into the groupbox
                LoadEmployeeDetail(selectedIndex);

                // Set the tag of the update button to "E"
                btnUpdate.Tag = "E";
            }
            else
            {
                // Show an error message if no employee is selected
                MessageBox.Show("Please select an employee to edit.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Check to see if an employee was selected
            if (lstEmployees.SelectedIndex >= 0)
            {
                // Ask the user if they are sure they want to delete the employee
                DialogResult result = MessageBox.Show("Are you sure you want to delete the selected employee?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Check the user's response
                if (result == DialogResult.Yes)
                { // Get the selected employee's ID from the list's SelectedIndex
                    int selectedIndex = lstEmployees.SelectedIndex;

                    // Remove the employee from the myEmployees list
                    myEmployees.RemoveAt(selectedIndex);

                    // Clear the text and numeric up-down controls in grpDetail
                    ClearControls();

                    //Refresh the employee list
                    DisplayEmployeeList();
                }
            }
            else {
                // Show an error message if no employee is selected
                MessageBox.Show("Please select an employee to delete.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Enable the controls
            EnableControls(true);

            // Clear the text and numeric up-down controls in grpDetail
            ClearControls();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Declare the rate variable
            double rate = (double)numRate.Value;

            // Check if all required fields are filled
            if (string.IsNullOrEmpty(txtLastName.Text) ||
                string.IsNullOrEmpty(txtFirstName.Text) ||
                numHours.Value <= 0 ||
                rate <= 0)
            {
                MessageBox.Show("Please ensure all fields are filled out correctly", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Determine the action based on the Tag property of btnUpdate
            string action = btnUpdate.Tag.ToString();
            if (action == "A")
            {
                // Add new employee
                clsEmployee newEmployee = new clsEmployee
                {
                    LastName = txtLastName.Text,
                    FirstName = txtFirstName.Text,
                    WeeklyHours = (int)numHours.Value,
                    Rate = rate
                };

                myEmployees.Add(newEmployee);
                // Update labels for new employee
                lblYearlyHours.Text = newEmployee.AnnualHours.ToString();
                lblYearlySalary.Text = newEmployee.AnnualRate.ToString("C");
            }
            else if (action == "E")
            {
                // Edit existing employee
                int selectedIndex = lstEmployees.SelectedIndex;
                if (selectedIndex >= 0 && selectedIndex < myEmployees.Count)
                {
                    clsEmployee employee = myEmployees[selectedIndex];
                    employee.LastName = txtLastName.Text;
                    employee.FirstName = txtFirstName.Text;
                    employee.WeeklyHours = (int)numHours.Value;
                    employee.Rate = rate;

                    // Update labels for edited employee 
                    lblYearlyHours.Text = employee.AnnualHours.ToString();
                    lblYearlySalary.Text = employee.AnnualRate.ToString("C");
                }
                else
                {
                    MessageBox.Show("Invalid selection for editing.");
                    return;
                }
            }

            // Refresh the employee list and re-enable controls
            DisplayEmployeeList();
            EnableControls(true);

            // Change the selected index to the most recently selected employee
            lstEmployees.SelectedIndex = lstEmployees.Items.Count - 1;
        }

        private void lstEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if an item is selected
            if (lstEmployees.SelectedIndex >= 0)
            {
                // Load the selected employee's details
                LoadEmployeeDetail(lstEmployees.SelectedIndex);
            }
        }
    }
}