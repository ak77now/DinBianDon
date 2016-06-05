using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 scsb.DataSource = "CR3-04";
 scsb.DataSource = "CR3-04";
 *
 * */

namespace 訂便當2
{
    public partial class Form1 : Form
    {
        private SqlConnectionStringBuilder scsb;
        private int Student_ID = 0;
        private int 登入的班級 = 0;
        private int 檢查用的訂單序號;
        private string 值日生 = "";
        private string 登入的名字 = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "CR3-04";
            scsb.InitialCatalog = "DinBianDon2";
            scsb.IntegratedSecurity = true;
            取得Order_ID();
            ///////////////////////////////////////////////////////////////
        }

        public void 依照班級載入學生名字()
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select*from Student where Student_class=" + @登入的班級;
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.AddWithValue("@登入的班級", 登入的班級);
            SqlDataReader reader = cmd.ExecuteReader();
            ///////////////////////////////////////////////////////////////
            while (reader.Read())
            {
                comboBox1.Items.Add(string.Format("{0}", reader["Student_name"]));
            }
            reader.Close();
            con.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            值日生 = "";
            radioButton1.ForeColor = ColorTranslator.FromHtml("#99ff90");
            radioButton2.ForeColor = ColorTranslator.FromHtml("#888888");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            值日生 = "true";
            radioButton2.ForeColor = ColorTranslator.FromHtml("#99ff90");
            radioButton1.ForeColor = ColorTranslator.FromHtml("#888888");
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            if ((comboBox1.SelectedItem == null) || (comboBox2.SelectedItem == null))
            {
                MessageBox.Show("下拉選擇身分");
                return;
            }

            if (值日生 == "true")
            {
                this.Hide();
                Form2 Form2 = new Form2(登入的名字, 登入的班級, Student_ID);
                Form2.FormClosed += Form2_FormClosed;
                Form2.ShowDialog();
                //this.Close();
            }
            else
            {
                檢查今日是否開啟訂購程序();
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        public void 檢查今日是否開啟訂購程序()
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();

            //今天有發起便當且班級與自己登入的班級一樣時
            string strSQL =
                "select*from Order_master WHERE Class=" + @登入的班級 +
                " and CAST(Order_date AS DATE) = CAST(GETDATE() AS DATE)";

            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.AddWithValue("@登入的班級", 登入的班級);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //if (DateTime.Now.Date.ToString() == Convert.ToString(reader[0]))
                if ((Convert.ToString(reader[3]) == Convert.ToString(登入的班級)) && (DateTime.Now.Date.ToString() == Convert.ToString(reader[4])))
                //if (DateTime.Now.Date.ToString() == Convert.ToString(reader[4]))
                {
                    ///MessageBox.Show(Convert.ToString(檢查用的訂單序號));
                    檢查用的訂單序號 = Convert.ToInt32(string.Format("{0}", reader[0]));
                    Form5 Form5 = new Form5(登入的名字, 登入的班級, Student_ID);
                    this.Hide();
                    Form5.ShowDialog();
                    this.Close();
                }

                //MessageBox.Show(DateTime.Now.Date.ToString());
                //MessageBox.Show(Convert.ToString(reader[0]));
            }
            //MessageBox.Show(Convert.ToString(檢查用的訂單序號));
            MessageBox.Show("值日生尚未開放訂購");

            reader.Close();
            con.Close();
        }

        private void 取得Order_ID()
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select MAX (Order_ID)from Order_master";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                檢查用的訂單序號 = Convert.ToInt32(string.Format("{0}", reader[0]));
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            登入的班級 = comboBox2.SelectedIndex + 1;
            依照班級載入學生名字();
            label1.ForeColor = ColorTranslator.FromHtml("#888888");
            label1.ForeColor = ColorTranslator.FromHtml("#FF8888");
            label2.ForeColor = ColorTranslator.FromHtml("#888888");

            //label1.ForeColor = ColorTranslator.FromHtml("#99ff90");
            //label1.ForeColor = ColorTranslator.FromHtml("#888888");
            //label2.ForeColor = ColorTranslator.FromHtml("#99ff90");

            //MessageBox.Show(Convert.ToString(登入的班級));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = comboBox1.SelectedItem;
            登入的名字 = selected.ToString();
            //MessageBox.Show(string.Format(登入的名字));
            取得Student_ID();
            label2.ForeColor = ColorTranslator.FromHtml("#888888");
            label2.ForeColor = ColorTranslator.FromHtml("#FF8888");
            label1.ForeColor = ColorTranslator.FromHtml("#888888");

            //label2.ForeColor = ColorTranslator.FromHtml("#99ff90");
            //label2.ForeColor = ColorTranslator.FromHtml("#888888");
            //label1.ForeColor = ColorTranslator.FromHtml("#99ff90");
        }

        private void 取得Student_ID()
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select*from Student where Student_name='" + @登入的名字 + "'";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();
            cmd.Parameters.AddWithValue("@登入的名字", 登入的名字);
            ///////////////////////////////////////////////////////////////
            while (reader.Read())
            {
                Student_ID = Convert.ToInt32(string.Format("{0}", reader[0]));
                //MessageBox.Show(Convert.ToString(Student_ID));
            }
            reader.Close();
            con.Close();
        }

        //按下ESC離開視窗
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}