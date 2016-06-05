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

namespace 訂便當2
{
    public partial class Form3 : Form
    {
        private int Student_ID;
        private int 店家選擇;
        private int 登入的班級;
        private int 檢查用的訂單序號;
        private SqlConnectionStringBuilder scsb;
        private string 登入的名字;

        public Form3()
        {
            InitializeComponent();
        }

        public Form3(int x, int y, string z, int g)
        {
            InitializeComponent();
            登入的班級 = x;
            檢查用的訂單序號 = y;
            登入的名字 = z;
            Student_ID = g;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "CR3-04";
            scsb.InitialCatalog = "DinBianDon2";
            scsb.IntegratedSecurity = true;

            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select*from Company";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(string.Format("{0}", reader["Company_name"]));
            }
            reader.Close();
            con.Close();
        }

        private void 顯示gridview()
        {
            this.dataGridView1.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#E38EFF");
            //            this.dataGridView1.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("	#444444");
            this.dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);

            foreach (DataGridViewRow row in dataGridView1.Rows)

                dataGridView1.DataSource = null; // 每次重新載入gridview時先清除一下
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select*from Product where Company_ID=" + @店家選擇;
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.AddWithValue("@店家選擇", 店家選擇);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
            }
            reader.Close();
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            店家選擇 = comboBox1.SelectedIndex + 1;
            顯示gridview();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "insert into Order_master(Company_ID,Class) values('" + @店家選擇 +
                    "','" + @登入的班級 + "')";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@店家選擇", 店家選擇);
                cmd.Parameters.AddWithValue("@登入的班級", 登入的班級);
                con.Close();
                //紀錄選定的店家();
                //MessageBox.Show(Convert.ToString(檢查用的訂單序號));
                Form4 Form4 = new Form4(檢查用的訂單序號, 店家選擇, 登入的班級, 登入的名字, Student_ID);
                this.Hide();
                Form4.ShowDialog();
                this.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                SqlConnection con2 = new SqlConnection(scsb.ToString());
                con2.Open();
                string strSLQ2 =
                    "insert into Order_master(Company_ID, Class) values(" +
                    @店家選擇 + "," + @登入的班級 + ")";
                SqlCommand cmd2 = new SqlCommand(strSLQ2, con2);
                cmd2.Parameters.AddWithValue("@店家選擇", 店家選擇);
                cmd2.Parameters.AddWithValue("@登入的班級", 登入的班級);
                cmd2.ExecuteNonQuery();
                con2.Close();
                //紀錄選定的店家();
                Close();
            }
        }

        private void 紀錄選定的店家()
        {
            if (comboBox1.SelectedItem != null)
            {
                SqlConnection con2 = new SqlConnection(scsb.ToString());
                con2.Open();
                string strSLQ2 =
                    "insert into SOD(Selected_company) values(" + @店家選擇 + ")";
                SqlCommand cmd2 = new SqlCommand(strSLQ2, con2);
                cmd2.Parameters.AddWithValue("@店家選擇", 店家選擇);
                cmd2.ExecuteNonQuery();
                con2.Close();
            }
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