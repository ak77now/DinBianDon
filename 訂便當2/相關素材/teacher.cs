using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //ADO.net

namespace WindowsFormsApplication21
{
    public partial class Form1 : Form
    {
        SqlConnectionStringBuilder scsb;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            scsb = new SqlConnectionStringBuilder();
            //Data Source=.;Initial Catalog=csharp1;Integrated Security=True
            scsb.DataSource = "CR3-Teacher";
            scsb.InitialCatalog = "csharp1";
            scsb.IntegratedSecurity = true;

            showDataGridView1();
        }

        private void showDataGridView1()
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select top 500 id,姓名,電話,Email,地址,婚姻狀態,生日 from persons";
            SqlCommand cmd = new SqlCommand(strSQL, con);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string strQueryID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select top 100 * from persons where id = @QUERYID";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.AddWithValue("@QUERYID", strQueryID);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                tb姓名.Text = String.Format("{0}", reader["姓名"]);
                tb電話.Text = String.Format("{0}", reader["電話"]);
                tb地址.Text = String.Format("{0}", reader["地址"]);
                tbEmail.Text = String.Format("{0}", reader["Email"]);
                dtp生日.Value = (DateTime)reader["生日"];
                chk婚姻狀態.Checked = (bool)reader["婚姻狀態"];
            }

            reader.Close();
            con.Close();
        }

        private void btn資料筆數_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select top 500 * from persons";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();

            string strOutput = "";
            int i = 0;

            while (reader.Read())
            {
                strOutput += String.Format("{0},{1},{2}\n",
                    reader["id"], reader["姓名"], reader["電話"]);
                i++;
            }

            strOutput += "資料筆數:" + i.ToString();
            reader.Close();
            con.Close();

            MessageBox.Show(strOutput);
        }

        private void btn搜尋資料_Click(object sender, EventArgs e)
        {
            if (tb姓名.Text.Length > 0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "select top 100 * from persons where 姓名 like @SearchName";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchName", "%"+tb姓名.Text +"%");
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    tb姓名.Text = String.Format("{0}", reader["姓名"]);
                    tb電話.Text = String.Format("{0}", reader["電話"]);
                    tb地址.Text = String.Format("{0}", reader["地址"]);
                    tbEmail.Text = String.Format("{0}", reader["Email"]);
                    dtp生日.Value = (DateTime)reader["生日"];
                    chk婚姻狀態.Checked = (bool)reader["婚姻狀態"];
                }
                else
                {
                    MessageBox.Show("查無此人");
                    tb姓名.Text = "";
                    tb電話.Text = "";
                    tb地址.Text = "";
                    tbEmail.Text = "";
                    dtp生日.Value = DateTime.Now;
                    chk婚姻狀態.Checked = false;
                }

            } else
            {
                MessageBox.Show("請輸入姓名搜尋");
            }
        }

        private void btn更新資料_Click(object sender, EventArgs e)
        {
            if (tb姓名.Text.Length > 0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "update persons set 電話=@NewPhone, 地址=@NewAddress," +
                    " Email=@NewEmail, 生日=@NewBirthday, 婚姻狀態=@NewMarriage" +
                    " where 姓名=@SearchName";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchName", tb姓名.Text);
                cmd.Parameters.AddWithValue("@NewPhone", tb電話.Text);
                cmd.Parameters.AddWithValue("@NewAddress", tb地址.Text);
                cmd.Parameters.AddWithValue("@NewEmail", tbEmail.Text);
                cmd.Parameters.AddWithValue("@NewBirthday", (DateTime)dtp生日.Value);
                cmd.Parameters.AddWithValue("@NewMarriage", (bool)chk婚姻狀態.Checked);

                int rows = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(String.Format("資料更新完畢, 共影響{0}筆資料", rows));
            }
            else
            {
                MessageBox.Show("請輸入姓名");
            }
        }

        private void btn新增資料_Click(object sender, EventArgs e)
        {
            if (tb姓名.Text.Length > 0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "insert into persons values(" +
                    "@NewName, @NewPhone, @NewAddress, @NewEmail, @NewBirthday, @NewMarriage)";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@NewName", tb姓名.Text);
                cmd.Parameters.AddWithValue("@NewPhone", tb電話.Text);
                cmd.Parameters.AddWithValue("@NewAddress", tb地址.Text);
                cmd.Parameters.AddWithValue("@NewEmail", tbEmail.Text);
                cmd.Parameters.AddWithValue("@NewBirthday", (DateTime)dtp生日.Value);
                cmd.Parameters.AddWithValue("@NewMarriage", (bool)chk婚姻狀態.Checked);

                int rows = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(String.Format("資料新增完畢, 共影響{0}筆資料", rows));
            }
            else
            {
                MessageBox.Show("請輸入姓名");
            }
        }

        private void btn刪除資料_Click(object sender, EventArgs e)
        {
            if (tb姓名.Text.Length > 0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "delete from persons where 姓名=@OldName";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@OldName", tb姓名.Text);
          
                int rows = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(String.Format("資料刪除完畢, 共影響{0}筆資料", rows));
                tb姓名.Text = "";
                tb電話.Text = "";
                tb地址.Text = "";
                tbEmail.Text = "";
                dtp生日.Value = DateTime.Now;
                chk婚姻狀態.Checked = false;
            }
            else
            {
                MessageBox.Show("請輸入姓名");
            }
        }

     
    }
}
