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
    public partial class Form5 : Form
    {
        private int Student_ID;
        private string 登入的名字;
        private int 登入的班級;
        private int 店家ID;
        private int 單價;
        private string 店家名稱;
        private int 選取的餐點;
        private int i; // 訂購數量的變數
        private int Product_ID;
        private string Product_name;
        private int 檢查用的訂單序號;

        private SqlConnectionStringBuilder scsb;

        public Form5()
        {
            InitializeComponent();
        }

        public Form5(string x, int y, int z)
        {
            InitializeComponent();
            登入的名字 = x;
            登入的班級 = y;
            Student_ID = z;
            label2.Text = "登入者:" + 登入的名字 + "         班級:" + Convert.ToString(登入的班級);
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "CR3-04";
            scsb.InitialCatalog = "DinBianDon2";
            scsb.IntegratedSecurity = true;
            取得Order_ID();
            查詢選擇的店家();
            查詢選擇的店家名稱();
            載入菜單();
            button2.Enabled = false;

            button1.Enabled = false;
        }

        private void 載入菜單()
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSLQ = "select * from Product where Company_ID =" + @店家ID;
            SqlCommand cmd = new SqlCommand(strSLQ, con);
            cmd.Parameters.AddWithValue("@店家ID", 店家ID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(string.Format("{0}  {1}", reader["Product_name"], reader["Product_price"]));
            }
            reader.Close();
            con.Close();
        }

        private void 查詢選擇的店家()
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSLQ =
                "select Company_ID" +
                " from Order_master" +
                " where Class=" + @登入的班級 + " and CAST(Order_date AS DATE) = CAST(GETDATE() AS DATE)";
            SqlCommand cmd = new SqlCommand(strSLQ, con);
            cmd.Parameters.AddWithValue("@登入的班級", 登入的班級);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string x = string.Format("{0}", reader[0]);
                店家ID = Convert.ToInt32(x);
            }
            reader.Close();
            con.Close();
        }

        private void 查詢選擇的店家名稱()
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSLQ =
               "select Company_name" +
                " from Company" +
                " where Company_ID = " + @店家ID;
            SqlCommand cmd = new SqlCommand(strSLQ, con);
            cmd.Parameters.AddWithValue("@店家ID", 店家ID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                店家名稱 = string.Format("{0}", reader[0]);
            }
            reader.Close();
            con.Close();
            label3.Text = "本次店家為:" + 店家名稱;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                取價();
                i++;
                label1.Text = Convert.ToString(i);
                button3.Enabled = true;
                button2.Enabled = true;
                label1.Text = Convert.ToString(單價 * i);
                if (單價 > 0)
                    button1.Enabled = true;
                else
                {
                    button1.Enabled = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            取價();
            i--;
            if (i == 0)
            {
                button2.Enabled = false;
            }

            label1.Text = Convert.ToString(i);
            label1.Text = Convert.ToString(單價 * i);

            if (單價 == 0)
                button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            選取的餐點 = listBox1.SelectedIndex + 1;
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSLQ = "select * from(select ROW_NUMBER() OVER(ORDER BY Product_ID) " +
                "AS ROWID, *from Product where Company_ID =" + @店家ID +
                ") as A where ROWID =" + @選取的餐點;
            SqlCommand cmd = new SqlCommand(strSLQ, con);
            cmd.Parameters.AddWithValue("@店家ID", 店家ID);
            cmd.Parameters.AddWithValue("@選取的餐點", 選取的餐點);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string temp = string.Format("您(點餐者名字:{4})點的餐點產品ID是{0}，餐點名稱為{1}，數量為{2}，共計{3}元，確定無誤嗎?", reader["Product_ID"], reader["Product_name"], i, 單價 * i, 登入的名字);
                //MessageBox.Show(temp);
                DialogResult = MessageBox.Show(temp, "訂單送出確定", MessageBoxButtons.OKCancel);
                Product_ID = Convert.ToInt32(string.Format("{0}", reader["Product_ID"]));
                Product_name = string.Format("{0}", reader["Product_ID"]);
            }
            reader.Close();
            con.Close();

            //MessageBox.Show(Convert.ToString(檢查用的訂單序號));
            //MessageBox.Show(Convert.ToString(Product_ID));
            //MessageBox.Show(Convert.ToString(單價 * i));
            //MessageBox.Show(Convert.ToString(i));
            //MessageBox.Show(Convert.ToString(Student_ID));

            新增至資料庫();
        }

        private void 新增至資料庫()
        {
            SqlConnection con2 = new SqlConnection(scsb.ToString());
            con2.Open();
            string strSLQ2 =
                "insert into Order_detail(Order_ID, Product_ID, Price, Quantity, Student_ID) VALUES(" +
                @檢查用的訂單序號 + "," + @Product_ID + "," + 單價 * i + "," + i + "," + @Student_ID + ")";

            SqlCommand cmd2 = new SqlCommand(strSLQ2, con2);
            cmd2.Parameters.AddWithValue("@檢查用的訂單序號", 檢查用的訂單序號);
            cmd2.Parameters.AddWithValue("@Product_ID", Product_ID);
            cmd2.Parameters.AddWithValue("@Student_ID", Student_ID);
            cmd2.ExecuteNonQuery();
            con2.Close();
        }

        private void 取價()
        {
            選取的餐點 = listBox1.SelectedIndex + 1;
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSLQ = "select * from(select ROW_NUMBER() OVER(ORDER BY Product_ID) " +
                "AS ROWID, *from Product where Company_ID =" + @店家ID +
                ") as A where ROWID =" + @選取的餐點;
            SqlCommand cmd = new SqlCommand(strSLQ, con);
            cmd.Parameters.AddWithValue("@店家ID", 店家ID);
            cmd.Parameters.AddWithValue("@選取的餐點", 選取的餐點);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                單價 = (int)reader["Product_price"];
            }
            label1.Text = Convert.ToString(單價);
            //MessageBox.Show(Convert.ToString(單價));
            reader.Close();
            con.Close();
        }

        private void 取得Order_ID()
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select MAX (Order_ID)from Order_master where class=" + @登入的班級;
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.AddWithValue("@登入的班級", 登入的班級);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                檢查用的訂單序號 = Convert.ToInt32(string.Format("{0}", reader[0]));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            i = 0;
            label1.Text = "";
            if (i == 0)
            {
                button2.Enabled = false;
            }
        }
    }
}