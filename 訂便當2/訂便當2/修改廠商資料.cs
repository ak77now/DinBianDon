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
    public partial class 修改廠商資料 : Form
    {
        private SqlConnectionStringBuilder scsb;
        private string 欄位_廠商名;
        private string 欄位_電話;
        private string 欄位_地址;
        private int 所選取的廠商ID;

        public 修改廠商資料()
        {
            InitializeComponent();
        }

        private void 修改廠商資料_Load(object sender, EventArgs e)
        {
            scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "CR3-04";
            scsb.InitialCatalog = "DinBianDon2";
            scsb.IntegratedSecurity = true;
            依照載入加入ROW_NUMBER排序過的廠商資料表();
        }

        private void 依照載入加入ROW_NUMBER排序過的廠商資料表()
        {
            listBox1.Items.Clear();
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL =
                "select ROW_NUMBER() over(order by Company_ID) as ROWID,*" +
                " from Company as A";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(string.Format("{0,4} {1,4} {2,4} {3,4}", reader[1], reader[2], reader[3], reader[4]));
            }
            reader.Close();
            con.Close();
        }

        private void 點選tab(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                依照載入加入ROW_NUMBER排序過的廠商資料表();
            if (tabControl1.SelectedIndex == 1)
                依照載入加入ROW_NUMBER排序過的廠商資料表();
            if (tabControl1.SelectedIndex == 2)
                依照載入加入ROW_NUMBER排序過的廠商資料表();
        }

        #region 新增

        private void button1_Click(object sender, EventArgs e)
        {
            新增廠商資料();
            依照載入加入ROW_NUMBER排序過的廠商資料表();
            textBox5.Focus();
        }

        private void 新增廠商資料()
        {
            if (textBox5.Text.Length > 0 && textBox4.Text.Length > 0 && textBox7.Text.Length > 0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL =
                  "insert into Company(Company_name, Company_address, Company_phone) values('" +
                  textBox5.Text +
                  "', '" +
                  textBox7.Text + "','" +
                   textBox4.Text + "')";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.ExecuteNonQuery();
                con.Close();
                textBox5.Text = "";
                textBox4.Text = "";
                textBox7.Text = "";
                //textBox6.Focus();
            }
            else
            {
                MessageBox.Show("請將資料填齊全");
            }
        }

        //按下ENTER新增
        private void 按下ENTER(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        #endregion 新增

        #region 修改

        //修改
        private void button2_Click(object sender, EventArgs e)
        {
            修改已選取的廠商資料();
            依照載入加入ROW_NUMBER排序過的廠商資料表();
        }

        private void 修改已選取的廠商資料()
        {
            //將選取的資料內容複製到要修改的欄位上();
            int selected = listBox1.SelectedIndex + 1; //選取的序號
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("請選取欲編輯的目標");
                return;
            }
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0 && listBox1.SelectedIndex != -1)
            {
                try
                {
                    //檢查輸入的數字是否為數字
                    SqlConnection con = new SqlConnection(scsb.ToString());
                    con.Open();
                    string strSQL =
"UPDATE X" +
" SET X.Company_name = '" + textBox3.Text + "',X.Company_address = '" + textBox1.Text + "',X.Company_phone = '" + textBox2.Text + "'" +
" from" +

" (select *" +
" from" +
" (select ROW_NUMBER() over(order by Company_ID) as ROWID, *" +
" from Company) as A" +
" where" +
" ROWID = " + selected + ")   X";

                    SqlCommand cmd = new SqlCommand(strSQL, con);
                    int rows = cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show(String.Format("資料更新完畢, 共影響{0}筆資料", rows));

                    textBox3.Text = "";
                    textBox2.Text = "";
                    textBox1.Text = "";
                    textBox1.Focus(); //修改完後游標移動到指定的textbox位置
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    string str;
                    str = "Source:" + ex.Source;
                    str += "\n" + "Message:" + ex.Message;
                    MessageBox.Show("學生ID不能重複喔 !");
                }
            }
            else
            {
                MessageBox.Show("將欄位完整正確地填入");
            }
        }

        private void 將選取的資料內容複製到要修改的欄位上()
        {
            int selected = listBox1.SelectedIndex + 1; //選取的序號
            //listBox1.Items.Clear();
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL =
"select *" +
" from" +
" (select ROW_NUMBER() over(order by Company_ID) as ROWID, *" +
" from Company) as A" +
" where ROWID =" + selected;
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //textBox3.Text = string.Format("{0}", reader[2]);
                //textBox1.Text = string.Format("{0}", reader[3]);
                //textBox2.Text = string.Format("{0}", reader[4]);

                欄位_廠商名 = string.Format("{0}", reader[2]);
                欄位_地址 = string.Format("{0}", reader[3]);
                欄位_電話 = string.Format("{0}", reader[4]);
            }
            reader.Close();
            con.Close();
        }

        //按下ENTER修改
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(this, new EventArgs());
            }
        }

        #endregion 修改

        //刪除

        #region 刪除

        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("刪除廠商資料會連帶的將其所有產品一併刪除，確定嗎", "刪除所選取的廠商", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                查詢已選取廠商ID();//1.
                刪除已選取的廠商旗下的所有產品();//2.
                //前兩步驟是因為約束限制的關係所以才這樣做
                刪除已選取的廠商資料();
                依照載入加入ROW_NUMBER排序過的廠商資料表();
            }
            else if (dialogResult == DialogResult.No)
            {
                //MessageBox.Show("NO");
            }

        }

        private void 刪除已選取的廠商資料()
        {
            if (listBox1.SelectedIndex == -1)
                MessageBox.Show("請選取欲編輯的目標");
            if (listBox1.SelectedIndex != -1)
            {
                int selected = listBox1.SelectedIndex + 1; //選取的序號
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL =
  "DELETE FROM X" +
" from" +
        " (select *" +
        " from" +
" (select ROW_NUMBER() over(order by Company_ID) as ROWID, *" +
" from Company) as A" +
           " where ROWID = " + selected + ")    X";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                int rows = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(String.Format("資料更新完畢, 共影響{0}筆資料", rows));
            }
        }

        private void 查詢已選取廠商ID()
        {
            if (listBox1.SelectedIndex == -1)
                MessageBox.Show("請選取欲編輯的目標");
            if (listBox1.SelectedIndex != -1)
            {
                int selected = listBox1.SelectedIndex + 1; //選取的序號
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL =
" select *" +
" from" +
 " (select ROW_NUMBER() over(order by Company_ID) as ROWID, *" +
 " from Company) as A" +
" where ROWID = " + selected;
                SqlCommand cmd = new SqlCommand(strSQL, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    所選取的廠商ID = Convert.ToInt32($"{reader[1]}");
                    //MessageBox.Show($"{reader[1]}");
                }
                reader.Close();
                con.Close();
                ///////////////////////
            }
        }

        private void 刪除已選取的廠商旗下的所有產品()
        {
            int selected = listBox1.SelectedIndex + 1; //選取的序號
            //listBox1.Items.Clear();
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL =
" delete" +
" from Product" +
" where Company_ID = " + 所選取的廠商ID;
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //DELETE刪除
        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                button3_Click(this, new EventArgs());
            }           
        }

        #endregion 刪除

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                將選取的資料內容複製到要修改的欄位上();
                textBox3.Text = 欄位_廠商名;
                textBox1.Text = 欄位_地址;
                textBox2.Text = 欄位_電話;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
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