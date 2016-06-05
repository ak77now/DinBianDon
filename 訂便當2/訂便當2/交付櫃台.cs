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
    public partial class 交付櫃台 : Form
    {
        private SqlConnectionStringBuilder scsb;
        private int 登入的班級;
        private int 訂購單序號;
        private string 值日生;

        public 交付櫃台()
        {
            InitializeComponent();
        }

        public 交付櫃台(int x, int y, string z)
        {
            InitializeComponent();
            登入的班級 = x;
            訂購單序號 = y;
            值日生 = z;
        }

        private void 交付櫃台_Load(object sender, EventArgs e)
        {
            scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "CR3-04";
            scsb.InitialCatalog = "DinBianDon2";
            scsb.IntegratedSecurity = true;
            gridview資料();
            載入訂單主檔資料();
        }

        private void gridview資料()
        {
            this.dataGridView1.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#E38EFF");
            this.dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);

            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL =
"select Product_name as 項目名,sum(Quantity) as 數量小計" +
" from" +
" (" +
" select *" +
 " from" +
" (select ROW_NUMBER() OVER(ORDER BY Order_ID, Item_ID) AS ROWID, Order_ID, Item_ID, Company_name, Product_name, Quantity, Price, Student_name" +
 " from Order_detail" +
 " inner join Student" +
 " on Order_detail.Student_ID = Student.Student_ID" +
 " inner" +
 " join Product" +

" on Order_detail.Product_ID = Product.Product_ID" +

" inner" +
 " join Company" +

" on Product.Company_ID = Company.Company_ID) as A" +
 " where Order_ID = " + 訂購單序號 +
 " ) as B" +
 " group by Product_name";

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

        private void 載入訂單主檔資料()
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL =
 "select Order_ID as 訂單序號,Company_name as 廠商名稱,Company_phone as 電話,Class as 訂購班級,Order_amount as 總計金額,Order_date as 訂購日期" +
" from Order_master" +
" inner join Company" +
" on Company.Company_ID = Order_master.Company_ID" +
" WHERE Class = " + 登入的班級 + " and CAST(Order_date AS DATE) = CAST(GETDATE() AS DATE)";

            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                label1.Text = string.Format("訂單序號:{0}", reader[0]);
                label2.Text = string.Format("廠商名稱:{0}", reader[1]);
                label3.Text = string.Format("電話:{0}", reader[2]);
                label4.Text = string.Format("班級:{0}", reader[3]);
                label5.Text = string.Format("總金額:{0}元", reader[4]);
                label6.Text = string.Format("日期:{0}", reader[5]);
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