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
    public partial class Form2 : Form
    {
        private SqlConnectionStringBuilder scsb;
        private int Student_ID;
        private int 目前總計;
        private int 要刪除的項目;
        private int 訂單序號;
        private int 登入的班級;
        private int 項次序號;
        private int 檢查用的訂單序號;
        bool 是否已經開啟訂購 = false;

        private string 登入的名字 = "";

        public Form2()
        {
            InitializeComponent();
        }

        //從Form1帶來的參數
        public Form2(string x, int y, int z)
        {
            InitializeComponent();
            登入的名字 = x;
            登入的班級 = y;
            Student_ID = z;
            label1.Text = "登入者:" + 登入的名字 + "   身分:值日生" + "  班級:" + Convert.ToString(登入的班級);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "CR3-04";
            scsb.InitialCatalog = "DinBianDon2";
            scsb.IntegratedSecurity = true;
            取得Order_ID();
            檢查今日是否開啟訂購程序();
            //MessageBox.Show(Convert.ToString(檢查用的訂單序號));
            重新整理();
            //MessageBox.Show(Convert.ToString(目前總計));
        }

        public void 檢查今日是否開啟訂購程序()
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL =
                "select*from Order_master WHERE Class=" + @登入的班級 +
                " and CAST(Order_date AS DATE) = CAST(GETDATE() AS DATE)";
            //"select * from Order_master" +
            //"WHERE Class =" + 登入的班級 +
            //" and CAST(Order_date AS DATE) = CAST(GETDATE() AS DATE)";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();
            cmd.Parameters.AddWithValue("@登入的班級", 登入的班級);
            while (reader.Read())
            {
                //if (DateTime.Now.Date.ToString() == Convert.ToString(reader[0]))
                if ((Convert.ToString(reader[3]) == Convert.ToString(登入的班級)) && (DateTime.Now.Date.ToString() == Convert.ToString(reader[4])))
                //if (DateTime.Now.Date.ToString() == Convert.ToString(reader[4]))
                {
                    button3.Enabled = false;
                    button3.Text = "活動已發起";
                    MessageBox.Show(Convert.ToString("今日訂便當活動已經開啟!"));
                    是否已經開啟訂購 = true;
                    檢查用的訂單序號 = Convert.ToInt32(string.Format("{0}", reader[0]));
                }

            }
            reader.Close();
            con.Close();
            //    重新整理();
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

        private void 關閉並離開ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            //Environment.Exit(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            重新整理();
        }

        public void 重新整理()
        {
            if (是否已經開啟訂購==false)
            {
                return;
            }
            listBox1.Items.Clear();
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL =
"select *" +
" from" +
"(select ROW_NUMBER() OVER(ORDER BY Order_ID,Item_ID) AS ROWID,Order_ID,Item_ID ,Company_name,Product_name,Quantity,Price,Student_name" +
" from Order_detail" +
" inner join Student" +
" on Order_detail.Student_ID = Student.Student_ID" +
" inner join Product" +
" on Order_detail.Product_ID = Product.Product_ID" +
" inner join Company" +
" on Product.Company_ID=Company.Company_ID) as A" +
" where Order_ID=" + @檢查用的訂單序號;

            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.AddWithValue("@檢查用的訂單序號", 檢查用的訂單序號);
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                listBox1.Items.Add(string.Format("訂單序號:{0}  訂單項次:{1}  店家:{2}  產品名:{3}  數量:{4}  價錢:{5}  訂購人:{6}",
                    reader["Order_ID"],
                    reader["Item_ID"],
                    reader["Company_name"],
                    reader["Product_name"],
                    reader["Quantity"],
                    reader["Price"],
                    reader["Student_name"]
                    ));
            }
            reader.Close();
            con.Close();

            //另外開一個新的查詢指令，查詢訂單明細的金額總計
            目前總計的方法();
            將目前總計寫入訂單總檔();
        }

        private void 目前總計的方法()
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            string strSQLSum = "SELECT ISNULL (SUM (Price),0) as Sum FROM Order_detail where Order_ID=" + @檢查用的訂單序號;
            con.Open();
            SqlCommand cmd2 = new SqlCommand(strSQLSum, con);
            cmd2.Parameters.AddWithValue("@檢查用的訂單序號", 檢查用的訂單序號);
            SqlDataReader reader2 = cmd2.ExecuteReader();
            label2.Text = Convert.ToString("目前總計:");

            if (reader2.Read())
            {
                label2.Text += reader2[0].ToString() + "元";
            }
            //目前總計 = Convert.ToInt32(string.Format("{0}", reader2[0]));
            目前總計 = Convert.ToInt32(string.Format("{0}", reader2[0]));
            reader2.Close();
            con.Close();
        }

        private void 將目前總計寫入訂單總檔()
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL =
"update Order_master" +
" set Order_amount = " + @目前總計 +
" WHERE Class = " + @登入的班級 + " and CAST(Order_date AS DATE) = CAST(GETDATE() AS DATE)";

            SqlCommand cmd2 = new SqlCommand(strSQL, con);
            cmd2.Parameters.AddWithValue("@登入的班級", 登入的班級);
            cmd2.Parameters.AddWithValue("@目前總計", 目前總計);

            int rows = cmd2.ExecuteNonQuery();
            con.Close();
            //MessageBox.Show(String.Format("資料更新完畢, 共影響{0}筆資料", rows));
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            要刪除的項目 = listBox1.SelectedIndex + 1;
            listBox1.Items.Clear();
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL =
"select *" +
" from" +
"(select ROW_NUMBER() OVER(ORDER BY Order_ID,Item_ID) AS ROWID,Order_ID,Item_ID ,Company_name,Product_name,Quantity,Price,Student_name" +
" from Order_detail" +
" inner join Student" +
" on Order_detail.Student_ID = Student.Student_ID" +
" inner join Product" +
" on Order_detail.Product_ID = Product.Product_ID" +
" inner join Company" +
" on Product.Company_ID=Company.Company_ID" +
" where Order_ID=" + @檢查用的訂單序號 +
" ) as A" +
" where ROWID=" + @要刪除的項目;

            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.AddWithValue("@檢查用的訂單序號", 檢查用的訂單序號);
            cmd.Parameters.AddWithValue("@要刪除的項目", 要刪除的項目);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //MessageBox.Show(string.Format("{0},{1}", reader["Order_ID"], reader["Item_ID"]));
                訂單序號 = int.Parse(string.Format("{0}", reader["Order_ID"]));
                項次序號 = int.Parse(string.Format("{0}", reader["Item_ID"]));
                //MessageBox.Show(Convert.ToString(訂單序號) + Convert.ToString(項次序號));
            }
            //MessageBox.Show(Convert.ToString(訂單序號) + Convert.ToString(項次序號));
            reader.Close();
            con.Close();

            con.Open();
            string strSQLdeleteSelectedItem =
                "delete from Order_detail where Order_ID =" + 訂單序號 + " and Item_ID =" + 項次序號;
            SqlCommand cmd2 = new SqlCommand(strSQLdeleteSelectedItem, con);
            int rows = cmd2.ExecuteNonQuery();
            con.Close();
            MessageBox.Show(String.Format("資料刪除完畢, 共影響{0}筆資料", rows));

            重新整理();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 Form3 = new Form3(登入的班級, 檢查用的訂單序號, 登入的名字, Student_ID);
            this.Hide();
            Form3.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 Form4 = new Form4();
            Form4.ShowDialog();
        }

        private void 更新今日訂單明細的總額()
        {
            //SqlConnection con2 = new SqlConnection(scsb.ToString());
            //con2.Open();
            //string strSLQ2 =
            //    "insert into Order_master(Company_ID, Class) values(" +
            //    店家選擇 + "," + 登入的班級 + ")";
            //SqlCommand cmd2 = new SqlCommand(strSLQ2, con2);
            //cmd2.ExecuteNonQuery();
            //con2.Close();
            ////紀錄選定的店家();
            //Close();
        }

        private void 修改學生資料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            修改學生資料 updateStudentData = new 修改學生資料();
            updateStudentData.ShowDialog();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void 修改廠商資料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            修改廠商資料 updateConpanyData = new 修改廠商資料();
            updateConpanyData.ShowDialog();
        }

        private void 修改菜色資料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            修改目錄資料 menu = new 修改目錄資料();
            menu.ShowDialog();
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

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (是否已經開啟訂購 == false)
            {
                return;
            }
            
            //MessageBox.Show("登入的班級:"+ 登入的班級);
            //MessageBox.Show("檢查用的訂單序號:"+檢查用的訂單序號);
            //MessageBox.Show("登入的名字:" + 登入的名字);
            交付櫃台 pb = new 交付櫃台(登入的班級, 檢查用的訂單序號, 登入的名字);
            pb.ShowDialog();

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)//&& (tabControl1.SelectedIndex == 1))
            {
                button2_Click(this, new EventArgs());
            }
        }
    }
}