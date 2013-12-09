using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace itplProject
{
    public partial class employee : Form
    {

        string sqlQuery;
        OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/Developer/Desktop/itpl project/itpl_project.accdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader dr;

        public employee()
        {
            InitializeComponent();
        }

        private void employee_Load(object sender, EventArgs e)
        {
            date.Text = DateTime.Now.ToString("MM/dd/yyyy");
            time.Text = DateTime.Now.ToString("HH:mm tt");
            listView1.Columns.Add("id", 40, HorizontalAlignment.Center);
            listView1.Columns.Add("First Name", 160, HorizontalAlignment.Center);
            listView1.Columns.Add("Last Name", 160, HorizontalAlignment.Center);
            listView1.Columns.Add("Department", 300, HorizontalAlignment.Center);
            listView1.Columns.Add("Salary", 100, HorizontalAlignment.Center);
            cn.Open();
            label14.Hide();
            fill();
            clear();
            save_btn.Enabled = false;
            update_btn.Enabled = false;
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            generate generate_form = new generate();
            generate_form.Show();
        }

        void clear()
        {
            notify.Hide();
            fname_txtbox.Clear();
            lname_txtbox.Clear();
            department_combobox.SelectedIndex = -1;
            hourly_rate.SelectedIndex = -1;
            hours_logged.SelectedIndex = -1;
            ot_rate.SelectedIndex = -1;
            ot_hours.SelectedIndex = -1;
        }

        void fill()
        {
            listView1.Items.Clear();
            cmd.CommandText = "SELECT * FROM Employees ORDER BY department asc";
            cmd.Connection = cn;
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                ListViewItem list = new ListViewItem(dr[0].ToString());
                list.SubItems.Add(dr[1].ToString());
                list.SubItems.Add(dr[2].ToString());
                list.SubItems.Add(dr[3].ToString());
                list.SubItems.Add(dr[4].ToString());
                listView1.Items.AddRange(new ListViewItem[] { list });
            }
            dr.Close();
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            clear();
        }

        private int compute_salary()
        {
            int sss = 200, pagibig = 300, phil_health = 250, salary = 0;
            string hr_rt = hourly_rate.Text, hr = hours_logged.Text, ot_rt = ot_rate.Text, ot_hr = ot_hours.Text;
            int regular_pay, ot_pay;
            //Compute Salary
            regular_pay = Convert.ToInt16(hr_rt) * Convert.ToInt16(hr);
            ot_pay = Convert.ToInt16(ot_rt) * Convert.ToInt16(ot_hr);
            salary = regular_pay + ot_pay + sss + phil_health + pagibig;
            return salary;
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            string fname = fname_txtbox.Text, lname = lname_txtbox.Text, dept = department_combobox.Text;
            int salary = compute_salary();
            //insert commands
            sqlQuery = "INSERT INTO EMPLOYEES (first_name, last_name, department, salary) VALUES ('"+fname+"','"+lname+"','"+dept+"','"+salary+"')";
            cmd.CommandText = sqlQuery;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();
            notify.Text = "Employee Saved!";
            notify.Show();
            fill();
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            string id = label14.Text;
            string fname = fname_txtbox.Text, lname = lname_txtbox.Text, dept = department_combobox.Text;
            int salary = compute_salary();
            sqlQuery = "UPDATE EMPLOYEES SET EMPLOYEES.first_name ='"+fname+"',EMPLOYEES.last_name ='"+lname+"', EMPLOYEES.department ='"+dept+"', EMPLOYEES.salary ='"+salary+"' WHERE EMPLOYEES.id="+id+"";
            cmd.CommandText = sqlQuery;
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();
            notify.Text = "Employee Saved!";
            notify.Show();
            fill();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label14.Text = listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text;
            fname_txtbox.Text = listView1.Items[listView1.FocusedItem.Index].SubItems[1].Text;
            lname_txtbox.Text = listView1.Items[listView1.FocusedItem.Index].SubItems[2].Text;
            department_combobox.Text = listView1.Items[listView1.FocusedItem.Index].SubItems[3].Text;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time.Text = DateTime.Now.ToString("HH:mm tt");
        }

        private void fname_txtbox_TextChanged(object sender, EventArgs e)
        {
            if (fname_txtbox.Text != "" && lname_txtbox.Text != "")
            {
                save_btn.Enabled = true;
                update_btn.Enabled = true;
            }
            else {
                save_btn.Enabled = false;
                update_btn.Enabled = false;
            }
        }

        private void lname_txtbox_TextChanged(object sender, EventArgs e)
        {
            if (fname_txtbox.Text != "" && lname_txtbox.Text != "")
            {
                save_btn.Enabled = true;
                update_btn.Enabled = true;
            }
            else
            {
                save_btn.Enabled = false;
                update_btn.Enabled = false;
            }
        }
    }
}
