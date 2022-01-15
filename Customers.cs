﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using J_Sarad_C969_SchedulingApp.model;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace J_Sarad_C969_SchedulingApp
{
    
    public partial class Customers : Form
    {
        DataTable custTable;
        public int currentIndex;
        
        public Customers()
        {
            InitializeComponent();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            currentIndex = -1;
            DB.OpenConnection();
            string query = "select customerId as 'ID', customerName as 'Name', phone as 'Phone', address as 'Address', city as 'City', country as 'Country' from customer t1 inner join address t2 on t1.addressId=t2.addressId inner join city t3 on t2.cityId=t3.cityId inner join country t4 on t3.countryId=t4.countryId";
            DB.Query(query);
            custTable = new DataTable();
            DB.adp.Fill(custTable);
            //DB.FillTable(query);
            DB.CloseConnection();
            displayDGV();
        }

        private void displayDGV()
        {
            dgvCustomers.DataSource = custTable;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.ReadOnly = true;
            dgvCustomers.MultiSelect = false;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.DefaultCellStyle.SelectionBackColor = Color.Yellow;
            dgvCustomers.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCustomers.RowHeadersVisible = false;
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        { 
            currentIndex = e.RowIndex;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (currentIndex >= 0)
            {
                this.Hide();
                UpdateCustomer form = new UpdateCustomer();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please Select a Customer to Update");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddCustomer form = new AddCustomer();
            form.ShowDialog();
            /*foreach (Control txt in this.Controls)
            {
                if (txt is TextBox && (txt != txtCustID) && (!string.IsNullOrEmpty(txt.Text)))
                {
                    DB.OpenConnection();
                    string query = "INSERT INTO country (country, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES ('" + txtCustCountry.Text + "','" + DateTime.UtcNow + "','" + DB.currentUser + "','" + DateTime.UtcNow + "','" + DB.currentUser + ")";
                    DB.NonQuery(query);
                }
            }*/
        }
    }
}