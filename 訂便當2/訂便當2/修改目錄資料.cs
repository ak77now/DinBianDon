using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 訂便當2
{
    public partial class 修改目錄資料 : Form
    {
        SqlConnectionStringBuilder scsb;
        int 下拉選擇的店家ID;
        int 目前產品ID的最大值;
        int 選取的產品ID;
        int 選取的產品的廠商ID;
        int 選取的產品的價錢;
        string 選取的產品名;

        public 修改目錄資料()
        {
            InitializeComponent();
        }



        private void 修改目錄資料_Load(object sender, EventArgs e)
        {
            scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "CR3-04";
            scsb.InitialCatalog = "DinBianDon2";
            scsb.IntegratedSecurity = true;
            載入廠商資料();
            取得目前產品ID的最大值();
            label9.Text = "(手動新增產品ID建議值為:" + (目前產品ID的最大值 + 1).ToString()+")";
            
        }
        private void 載入廠商資料()
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
                //                comboBox1.Items.Add(string.Format("店家ID:{0}   店家名稱:(1}", reader[1], reader[2]));
                comboBox1.Items.Add(string.Format("{0}", reader[2]));

            }
            reader.Close();
            con.Close();
        }

        private void 事件_選擇廠商(object sender, EventArgs e)
        {
            取得選擇的廠商ID(); //下拉選擇的店家ID就是
            label14.Text = "店家ID:" + Convert.ToString(下拉選擇的店家ID);
            //MessageBox.Show(Convert.ToString(下拉選擇的店家ID));
            載入選擇過店家後的目錄至listbox();
        }
        private void 取得選擇的廠商ID()
        {
            int selected = comboBox1.SelectedIndex + 1; //選取的序號
            listBox1.Items.Clear();
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL =
  "select *" +
  " from" +
  " (select ROW_NUMBER() over(order by Company_ID) as ROWID, *" +
  " from Company) as A" +
  " where ROWID = " + @selected;
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.AddWithValue("@selected", selected);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                下拉選擇的店家ID = Convert.ToInt32(string.Format("{0}", reader[1]));
            }

            reader.Close();
            con.Close();
        }

        private void 載入選擇過店家後的目錄至listbox()
        {
            listBox1.Items.Clear();
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL =
"select ROW_NUMBER()over(order by Product_ID) as ROWID,*" +
" from Product as A" +
" where Company_ID = " + @下拉選擇的店家ID;
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.AddWithValue("@下拉選擇的店家ID", 下拉選擇的店家ID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(string.Format("{0,4}{1,10}{2,10}", reader[1], reader[2], reader[3]));
            }

            reader.Close();
            con.Close();
        }

        private void 取得目前產品ID的最大值()
        {
            int selected = comboBox1.SelectedIndex + 1; //選取的序號
            listBox1.Items.Clear();
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL =
"select max(Product_ID)" +
" from Product";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                目前產品ID的最大值 = Convert.ToInt32(string.Format("{0}", reader[0]));
            }

            reader.Close();
            con.Close();
        }
        private void 查詢所選取的資訊()
        {
            int selected = listBox1.SelectedIndex + 1; //選取的序號
            SqlConnection con2 = new SqlConnection(scsb.ToString());
            con2.Open();
            string strSQL2 =
"(select *" +
" from" +
" (select ROW_NUMBER()over(order by Product_ID) as ROWID, *" +
" from Product where Company_ID = " + @下拉選擇的店家ID + " ) as A" +
" where ROWID = " + @selected + ")";
            SqlCommand cmd = new SqlCommand(strSQL2, con2);
            cmd.Parameters.AddWithValue("@下拉選擇的店家ID", 下拉選擇的店家ID);
            cmd.Parameters.AddWithValue("@selected", selected);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                選取的產品ID = Convert.ToInt32(string.Format("{0}", reader[1]));
                選取的產品名 = string.Format("{0}", reader[2]);
                選取的產品的價錢 = Convert.ToInt32(string.Format("{0}", reader[3]));
                選取的產品的廠商ID = Convert.ToInt32(string.Format("{0}", reader[4]));
                //MessageBox.Show(string.Format("{0}", reader[1]));
                //MessageBox.Show(string.Format("{0}", reader[4]));
            }
            reader.Close();
            con2.Close();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region 新增

        private void button1_Click(object sender, EventArgs e)
        {
            新增目錄資料();
            載入選擇過店家後的目錄至listbox();
            textBox6.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";
            textBox4.Text = "";
            textBox6.Focus(); //修改完後游標移動到指定的textbox位置
            if (checkBox1.Checked == true)
                textBox5.Focus();

        }
        private void 新增目錄資料()
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("請先下拉左側選單查看店家");
                return;
            }

            if (checkBox1.Checked == true)
            {
                textBox6.Text = (目前產品ID的最大值 + 1).ToString();
                textBox4.Text = 下拉選擇的店家ID.ToString();
            }

            if (textBox5.Text.Length > 0 && textBox4.Text.Length > 0 && textBox7.Text.Length > 0 && textBox6.Text.Length > 0)
            {
                int value;


                try
                {
                    //檢查輸入的數字是否為數字
                    if ((int.TryParse(textBox6.Text, out value)) && (int.TryParse(textBox7.Text, out value)) && (int.TryParse(textBox4.Text, out value)))
                    {
                        SqlConnection con = new SqlConnection(scsb.ToString());
                        con.Open();
                        string strSQL =
"insert into Product(Product_ID, Product_name, Product_price, Company_ID)" +
"values(" + Convert.ToInt32(textBox6.Text) + ", '" + textBox5.Text + "', " + Convert.ToInt32(textBox7.Text) + ", " + Convert.ToInt32(textBox4.Text) + ")";
                        SqlCommand cmd = new SqlCommand(strSQL, con);
                        
                        int rows = cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show(String.Format("資料更新完畢, 共影響{0}筆資料", rows));
                        取得目前產品ID的最大值();

                    }
                    else
                    {
                        MessageBox.Show("產品ID與售價請填數字喔，請檢查一下");
                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    string str;
                    str = "Source:" + ex.Source;
                    str += "\n" + "Message:" + ex.Message;
                    MessageBox.Show("產品ID不能重複喔 !");
                }
            }
            else
            {
                MessageBox.Show("請將資料填齊全、並下拉左邊選單");
            }


        }

        private void ENTER後新增資料(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }

        }

        //快速新增
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            {
                textBox6.Visible = false;
                textBox4.Visible = false;
                label6.Visible = false;
                label4.Visible = false;

            }
            if (checkBox1.Checked == false)
            {
                textBox6.Visible = true;
                textBox4.Visible = true;
                label6.Visible = true;
                label4.Visible = true;
            }
        }

        #endregion

        #region 修改
        private void button2_Click(object sender, EventArgs e)
        {
            修改所選取的項目();
            載入選擇過店家後的目錄至listbox();
        }
        private void 修改所選取的項目()
        {
            查詢所選取的資訊();

            int selected = listBox1.SelectedIndex + 1; //選取的序號
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("請先下拉左側選單查看店家");
                return;
            }
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("請點選左側商品");
                return;
            }


            if (textBox8.Text.Length > 0 && textBox3.Text.Length > 0 && textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                int value;


                try
                {
                    //檢查輸入的數字是否為數字
                    if ((int.TryParse(textBox8.Text, out value)) && (int.TryParse(textBox1.Text, out value)) && (int.TryParse(textBox2.Text, out value)))
                    {
                        SqlConnection con = new SqlConnection(scsb.ToString());
                        con.Open();
                        string strSQL =
"UPDATE X" +
" SET X.Product_name = '" + textBox3.Text + "',X.Product_price = " + Convert.ToInt32(textBox1.Text) + ",Company_ID = " + Convert.ToInt32(textBox2.Text) +
" from" +
" (select *" +
" from" +
" (select ROW_NUMBER()over(order by Product_ID) as ROWID, *" +
" from Product where Company_ID = " + 下拉選擇的店家ID + " ) as A" +
" where ROWID = " + selected + ") as X";

                        SqlCommand cmd2 = new SqlCommand(strSQL, con);
                        int rows = cmd2.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show(String.Format("資料更新完畢, 共影響{0}筆資料", rows));
                        取得目前產品ID的最大值();

                    }
                    else
                    {
                        MessageBox.Show("產品ID與售價請填數字喔，請檢查一下");
                    }

                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    string str;
                    str = "Source:" + ex.Source;
                    str += "\n" + "Message:" + ex.Message;
                    MessageBox.Show("產品ID不能重複喔 !");
                }
            }
            else
            {
                MessageBox.Show("請將資料填齊全、並下拉左邊選單");
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                查詢所選取的資訊();
                textBox8.Text = 選取的產品ID.ToString();
                textBox3.Text = 選取的產品名.ToString();
                textBox1.Text = 選取的產品的價錢.ToString();
                textBox2.Text = 選取的產品的廠商ID.ToString();
            }
        }
        private void 按下ENTER修改目錄資料(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (tabControl1.SelectedIndex == 1))
            {
                button2_Click(this, new EventArgs());
            }

        }

        #endregion

        #region 刪除

        private void button3_Click(object sender, EventArgs e)
        {
            刪除已選取的資料();
            載入選擇過店家後的目錄至listbox();
        }
        private void 刪除已選取的資料()
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("請先選取欲刪除的目標");
                return;
            }
            int selected = listBox1.SelectedIndex + 1; //選取的序號
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL =
" delete FROM X" +
" from" +
" (select *" +
" from" +
" (select ROW_NUMBER()over(order by Product_ID) as ROWID, *" +
 " from Product where Company_ID =" + 下拉選擇的店家ID + ") as A" +
" where ROWID =" + selected + ") as X";


            SqlCommand cmd = new SqlCommand(strSQL, con);
            int rows = cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show(String.Format("資料更新完畢, 共影響{0}筆資料", rows));

        }

        private void Delete刪除(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) && (tabControl1.SelectedIndex == 2))
            {
                button3_Click(this, new EventArgs());
            }
        }
        #endregion

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
