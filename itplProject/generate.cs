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
using System.IO;

namespace itplProject
{
    public partial class generate : Form
    {
        string sqlQuery;
        OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/Developer/Desktop/itpl project/itpl_project.accdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader dr;

        public generate()
        {
            InitializeComponent();
        }

        private void gen_btn_Click(object sender, EventArgs e)
        {
            string department = comboBox1.Text;
            sqlQuery = "SELECT * FROM EMPLOYEES WHERE DEPARTMENT LIKE '" + department + "' ORDER BY last_name asc";
            cmd.CommandText = sqlQuery;
            cmd.Connection = cn;
            dr = cmd.ExecuteReader();
            using (StreamWriter sw = new StreamWriter("C:/Users/Developer/Desktop/itpl project/itplProject/payrolls/payroll_"+ department +"_"+ DateTime.Now.ToString("mm-dd-yy")+".txt"))
            {
                
                sw.WriteLine("Report Generated at : " + DateTime.Now);
                sw.WriteLine(" Department, Last Name, First Name, Salary");
                sw.WriteLine("---------------------------------");
                while (dr.Read())
                {
                    sw.Write(dr["department"].ToString());
                    sw.Write(", "+ dr["last_name"].ToString());
                    sw.Write(", " + dr["first_name"].ToString());
                    sw.Write(", " + dr["salary"].ToString());
                    sw.WriteLine();
                }
                sw.WriteLine("---------------------------------");
                sw.Close();
                dr.Close();
                this.Close();
                MessageBox.Show("Check your File Directory!");
            }
        }
        private void generate_Load(object sender, EventArgs e)
        {
            cn.Open();
        }
    }
}
