﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace AyuboDriveApplication
{
    public partial class DayTourHireCalculaiton : Form
    {
        public DayTourHireCalculaiton()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            SqlConnection con = new SqlConnection("Data Source=LAPTOP-PS2GJUT8\\SQLEXPRESS;Initial Catalog=AyuboDriveApplication;Integrated Security=True");
            con.Open();

            SqlCommand cmd1 = new SqlCommand("select VehicleNumber from VEHICLE_DEATILS", con);
            cmd1.ExecuteNonQuery();

            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            sda1.Fill(dt1);

            comboBox1.Items.Add("-Select-");

            foreach (DataRow dr in dt1.Rows)
            {
                comboBox1.Items.Add(dr["VehicleNumber"].ToString());
            }

            comboBox1.SelectedIndex = 0;

            SqlCommand cmd2 = new SqlCommand("select PackageType from PACKAGE_TYPE_DROP_AND_PICKUP", con);
            cmd2.ExecuteNonQuery();

            DataTable dt2 = new DataTable();
            SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
            sda2.Fill(dt2);

            comboBox2.Items.Add("-Select-");

            foreach (DataRow dr in dt2.Rows)
            {
                comboBox2.Items.Add(dr["PackageType"].ToString());
            }

            comboBox2.SelectedIndex = 0;
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            login lf = new login();
            lf.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            textBox1.ResetText();
            textBox2.ResetText();
            label7.Text = "Base Hire Charge";
            label8.Text = "Waiting Charge";
            label9.Text = "Extra KM Charge";
            label10.Text = "Total Hire Value";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 0 && comboBox2.SelectedIndex != 0 && textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "")
            {

                if (Regex.IsMatch(textBox1.Text, "^[0-9]+$") == true && Regex.IsMatch(textBox2.Text, "^[0-9]+$") == true)
                {

                    bool errors = false;

                    if (dateTimePicker2.Value <= dateTimePicker1.Value)
                    {
                        errors = true;
                        MessageBox.Show("Please enter correct time range!");
                    }

                    if (Convert.ToInt32(textBox2.Text) <= Convert.ToInt32(textBox1.Text))
                    {
                        errors = true;
                        MessageBox.Show("Please enter correct kilometer range!");
                    }

                    if (errors == false)
                    {

                        string vehicleNo = comboBox1.SelectedItem.ToString();
                        string pkgType = comboBox2.SelectedItem.ToString();
                        DateTime startTime = dateTimePicker1.Value;
                        DateTime endTime = dateTimePicker2.Value;
                        int startKMreading = Convert.ToInt32(textBox1.Text);
                        int endKMreading = Convert.ToInt32(textBox2.Text);

                        Operation op = new Operation();
                        label7.Text = "Base Hire Charge = " + Convert.ToString(op.calculateDayTourHire(vehicleNo, pkgType, startTime, endTime, startKMreading, endKMreading)[0]);
                        label8.Text = "Waiting Charge = " + Convert.ToString(op.calculateDayTourHire(vehicleNo, pkgType, startTime, endTime, startKMreading, endKMreading)[1]);
                        label9.Text = "Extra KM Charge = " + Convert.ToString(op.calculateDayTourHire(vehicleNo, pkgType, startTime, endTime, startKMreading, endKMreading)[2]);
                        label10.Text = "Total Hire Value = " + Convert.ToString(op.calculateDayTourHire(vehicleNo, pkgType, startTime, endTime, startKMreading, endKMreading)[3]);
                    }
                }

                else
                {
                    MessageBox.Show("Please enter valid KM Reading values!");
                }
            }

            else
            {
                MessageBox.Show("Please enter all values!");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu mm = new MainMenu();
            mm.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            textBox1.ResetText();
            textBox2.ResetText();
            label7.Text = "Base Hire Charge";
            label8.Text = "Waiting Charge";
            label9.Text = "Extra KM Charge";
            label10.Text = "Total Hire Value";
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            login lf = new login();
            lf.Show();
            this.Hide();
        }
    }

}





