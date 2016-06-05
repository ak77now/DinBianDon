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
    public partial class Form4 : Form
    {
        private int i; // 訂購數量的變數
        private int intOrder_max;
        private int Student_ID;
        private int 主檔用序號;
        private int 店家選擇;
        private int 單價;
        private int 登入的班級;
        private int 選取的餐點;
        private int 檢查用的訂單序號;
        private SqlConnectionStringBuilder scsb;
        private string strOrder_max;
        private string 登入的名字;

        /// ////////////////////// ///////////////////
        private int Product_ID;

        private string Product_name;

        public Form4()
        {
            InitializeComponent();
        }

        public Form4(int x, int y, int z, string t, int g)
        {
            InitializeComponent();
            檢查用的訂單序號 = x;
            店家選擇 = y;
            登入的班級 = z;
            登入的名字 = t;
            Student_ID = g;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(Convert.ToString(店家選擇), Convert.ToString(登入的班級));
            scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "CR3-04";
            scsb.InitialCatalog = "DinBianDon2";
            scsb.IntegratedSecurity = true;

            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSLQ = "select * from Product where Company_ID =" + @店家選擇;
            SqlCommand cmd = new SqlCommand(strSLQ, con);
            cmd.Parameters.AddWithValue("@店家選擇", 店家選擇);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(string.Format("{0}  {1}", reader["Product_name"], reader["Product_price"]));
            }
            reader.Close();
            con.Close();
        }

        private void 取價()
        {
            選取的餐點 = listBox1.SelectedIndex + 1;
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSLQ = "select * from(select ROW_NUMBER() OVER(ORDER BY Product_ID) " +
                "AS ROWID, *from Product where Company_ID =" + @店家選擇 +
                ") as A where ROWID =" + @選取的餐點;
            SqlCommand cmd = new SqlCommand(strSLQ, con);
            cmd.Parameters.AddWithValue("@店家選擇", 店家選擇);
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

        private void button3_Click(object sender, EventArgs e)
        {
            取價();
            i++;
            label1.Text = Convert.ToString(i);
            button3.Enabled = true;
            button2.Enabled = true;
            label1.Text = Convert.ToString(單價 * i);
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            選取的餐點 = listBox1.SelectedIndex + 1;
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSLQ = "select * from(select ROW_NUMBER() OVER(ORDER BY Product_ID) " +
                "AS ROWID, *from Product where Company_ID =" + @店家選擇 +
                ") as A where ROWID =" + @選取的餐點;
            SqlCommand cmd = new SqlCommand(strSLQ, con);
            cmd.Parameters.AddWithValue("@店家選擇", 店家選擇);
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
            主檔用序號 = 檢查用的訂單序號 + 1;
            查詢Order_max();
            intOrder_max = Convert.ToInt32(strOrder_max) + 1;
            //MessageBox.Show(Convert.ToString(intOrder_max));  檢查主單ID
            加一筆Order_master紀錄();

            新增至資料庫();
            this.Close();
        }

        private void 新增至資料庫()
        {
            SqlConnection con2 = new SqlConnection(scsb.ToString());
            con2.Open();
            string strSLQ2 =
                "insert into Order_detail(Order_ID, Product_ID, Price, Quantity, Student_ID) VALUES(" +
                @intOrder_max + "," + @Product_ID + "," + 單價 * i + "," + i + "," + @Student_ID + ")";

            SqlCommand cmd2 = new SqlCommand(strSLQ2, con2);
            cmd2.Parameters.AddWithValue("@intOrder_max", intOrder_max);
            cmd2.Parameters.AddWithValue("@Product_ID", Product_ID);
            cmd2.Parameters.AddWithValue("@Student_ID", Student_ID);
            cmd2.ExecuteNonQuery();
            con2.Close();
        }

        private void 加一筆Order_master紀錄()
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
        }

        private void 查詢Order_max()
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select MAX(Order_ID)from Order_master";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                strOrder_max = string.Format("{0}", reader[0]);
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