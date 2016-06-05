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
            string strSQL = "select top 500 id,�m�W,�q��,Email,�a�},�B�ê��A,�ͤ� from persons";
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
                tb�m�W.Text = String.Format("{0}", reader["�m�W"]);
                tb�q��.Text = String.Format("{0}", reader["�q��"]);
                tb�a�}.Text = String.Format("{0}", reader["�a�}"]);
                tbEmail.Text = String.Format("{0}", reader["Email"]);
                dtp�ͤ�.Value = (DateTime)reader["�ͤ�"];
                chk�B�ê��A.Checked = (bool)reader["�B�ê��A"];
            }

            reader.Close();
            con.Close();
        }

        private void btn��Ƶ���_Click(object sender, EventArgs e)
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
                    reader["id"], reader["�m�W"], reader["�q��"]);
                i++;
            }

            strOutput += "��Ƶ���:" + i.ToString();
            reader.Close();
            con.Close();

            MessageBox.Show(strOutput);
        }

        private void btn�j�M���_Click(object sender, EventArgs e)
        {
            if (tb�m�W.Text.Length > 0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "select top 100 * from persons where �m�W like @SearchName";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchName", "%"+tb�m�W.Text +"%");
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    tb�m�W.Text = String.Format("{0}", reader["�m�W"]);
                    tb�q��.Text = String.Format("{0}", reader["�q��"]);
                    tb�a�}.Text = String.Format("{0}", reader["�a�}"]);
                    tbEmail.Text = String.Format("{0}", reader["Email"]);
                    dtp�ͤ�.Value = (DateTime)reader["�ͤ�"];
                    chk�B�ê��A.Checked = (bool)reader["�B�ê��A"];
                }
                else
                {
                    MessageBox.Show("�d�L���H");
                    tb�m�W.Text = "";
                    tb�q��.Text = "";
                    tb�a�}.Text = "";
                    tbEmail.Text = "";
                    dtp�ͤ�.Value = DateTime.Now;
                    chk�B�ê��A.Checked = false;
                }

            } else
            {
                MessageBox.Show("�п�J�m�W�j�M");
            }
        }

        private void btn��s���_Click(object sender, EventArgs e)
        {
            if (tb�m�W.Text.Length > 0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "update persons set �q��=@NewPhone, �a�}=@NewAddress," +
                    " Email=@NewEmail, �ͤ�=@NewBirthday, �B�ê��A=@NewMarriage" +
                    " where �m�W=@SearchName";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@SearchName", tb�m�W.Text);
                cmd.Parameters.AddWithValue("@NewPhone", tb�q��.Text);
                cmd.Parameters.AddWithValue("@NewAddress", tb�a�}.Text);
                cmd.Parameters.AddWithValue("@NewEmail", tbEmail.Text);
                cmd.Parameters.AddWithValue("@NewBirthday", (DateTime)dtp�ͤ�.Value);
                cmd.Parameters.AddWithValue("@NewMarriage", (bool)chk�B�ê��A.Checked);

                int rows = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(String.Format("��Ƨ�s����, �@�v�T{0}�����", rows));
            }
            else
            {
                MessageBox.Show("�п�J�m�W");
            }
        }

        private void btn�s�W���_Click(object sender, EventArgs e)
        {
            if (tb�m�W.Text.Length > 0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "insert into persons values(" +
                    "@NewName, @NewPhone, @NewAddress, @NewEmail, @NewBirthday, @NewMarriage)";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@NewName", tb�m�W.Text);
                cmd.Parameters.AddWithValue("@NewPhone", tb�q��.Text);
                cmd.Parameters.AddWithValue("@NewAddress", tb�a�}.Text);
                cmd.Parameters.AddWithValue("@NewEmail", tbEmail.Text);
                cmd.Parameters.AddWithValue("@NewBirthday", (DateTime)dtp�ͤ�.Value);
                cmd.Parameters.AddWithValue("@NewMarriage", (bool)chk�B�ê��A.Checked);

                int rows = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(String.Format("��Ʒs�W����, �@�v�T{0}�����", rows));
            }
            else
            {
                MessageBox.Show("�п�J�m�W");
            }
        }

        private void btn�R�����_Click(object sender, EventArgs e)
        {
            if (tb�m�W.Text.Length > 0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "delete from persons where �m�W=@OldName";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@OldName", tb�m�W.Text);
          
                int rows = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(String.Format("��ƧR������, �@�v�T{0}�����", rows));
                tb�m�W.Text = "";
                tb�q��.Text = "";
                tb�a�}.Text = "";
                tbEmail.Text = "";
                dtp�ͤ�.Value = DateTime.Now;
                chk�B�ê��A.Checked = false;
            }
            else
            {
                MessageBox.Show("�п�J�m�W");
            }
        }

     
    }
}
