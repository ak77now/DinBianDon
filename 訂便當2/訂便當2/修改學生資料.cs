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
    public partial class 修改學生資料 : Form
    {
        private SqlConnectionStringBuilder scsb;
        int 目前學生ID最大值;

        public 修改學生資料()
        {
            InitializeComponent();
        }

        private void 修改學生資料_Load(object sender, EventArgs e)
        {
            scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "CR3-04";
            scsb.InitialCatalog = "DinBianDon2";
            scsb.IntegratedSecurity = true;
            查學生ID最大值();
            label13.Text = "(新增學生ID的建議值為" + (目前學生ID最大值 + 1).ToString()+")";
            載入學生資料表();
        }

        #region 載入資料

        private void 載入學生資料表()
        {
            listBox1.Items.Clear();
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select*from Student";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(string.Format("{0,4}{1,10}{2,20}", reader[0], reader[1], reader[2]));
            }
            reader.Close();
            con.Close();
        }

        private void 載入加入ROW_NUMBER排序過的學生資料表()
        {
            listBox1.Items.Clear();
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL =
" select *" +
" from" +
" (select ROW_NUMBER() OVER(ORDER BY Student_ID) AS ROWID, *" +
" from Student) as A";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(string.Format("{0,4}{1,10}{2,20}", reader[1], reader[2], reader[3]));
                //listBox1.Items.Add(string.Format("{0,2}{1,5}{2,20}{3,10}", reader[0], reader[1], reader[2], reader[3]));
            }
            reader.Close();
            con.Close();
        }

        private void 點選TAB頁(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                載入學生資料表();
            if (tabControl1.SelectedIndex == 1)
                載入加入ROW_NUMBER排序過的學生資料表();
            if (tabControl1.SelectedIndex == 2)
                載入加入ROW_NUMBER排序過的學生資料表();
        }

        #endregion 載入資料

        #region 新增

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            載入學生資料表();
            新增學生資料();
            載入學生資料表();
        }

        private void 新增學生資料()
        {
            //檢查欄位有沒有填滿
            if (textBox6.Text.Length > 0 && textBox5.Text.Length > 0 && textBox4.Text.Length > 0)
            {
                int value;
                try
                {
                    //檢查輸入的數字是否為數字
                    if ((int.TryParse(textBox6.Text, out value)) && (int.TryParse(textBox4.Text, out value)))
                    {
                        SqlConnection con = new SqlConnection(scsb.ToString());
                        con.Open();
                        string strSQL =
                           "insert into Student(Student_ID, Student_name, Student_class)" +
                            " values(" + Convert.ToInt32(textBox6.Text) + ", '" + textBox5.Text + "'," + Convert.ToInt32(textBox4.Text) + ")";
                        SqlCommand cmd = new SqlCommand(strSQL, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        textBox6.Text = "";
                        textBox5.Text = "";
                        textBox4.Text = "";
                        textBox6.Focus();
                    }
                    else
                    {
                        MessageBox.Show("學生ID與班級請填數字喔，請檢查一下");
                    }
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
                MessageBox.Show("將欄位完整且正確地填入");
            }
        }

        #endregion 新增

        #region 修改

        private void button2_Click(object sender, EventArgs e)
        {
            //listBox1.Items.Clear();
            //載入學生資料表();
            修改已選取的學生資料();
            //載入學生資料表();
        }

        private void 修改已選取的學生資料()
        {
            int selected = listBox1.SelectedIndex + 1; //選取的序號
            if (listBox1.SelectedIndex == -1)
                MessageBox.Show("請選取欲編輯的目標");
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0 && listBox1.SelectedIndex != -1)
            {
                int value;

                try
                {
                    //檢查輸入的數字是否為數字
                    if ((int.TryParse(textBox1.Text, out value)) && (int.TryParse(textBox3.Text, out value)))
                    {
                        SqlConnection con = new SqlConnection(scsb.ToString());
                        con.Open();
                        string strSQL =
    "UPDATE X" +
    " SET X.Student_ID = " + textBox1.Text + ", X.Student_name = '" + textBox2.Text + "',X.Student_class = " + textBox3.Text +
    " from" +
    " (select *" +
    " from" +
    " (select ROW_NUMBER() OVER(ORDER BY Student_ID) AS ROWID, *" +
    " from Student) as A" +
    " where" +
    " ROWID = " + selected + ") X";
                        SqlCommand cmd = new SqlCommand(strSQL, con);
                        int rows = cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show(String.Format("資料更新完畢, 共影響{0}筆資料", rows));
                        載入學生資料表();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox1.Focus(); //修改完後游標移動到指定的textbox位置
                    }
                    else
                    {
                        MessageBox.Show("學生ID與班級請填數字喔，請檢查一下");
                    }
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
                MessageBox.Show("將欄位完整且正確地填入");
            }

            #endregion 修改
        }

        #region 刪除

        private void button3_Click(object sender, EventArgs e)
        {
            刪除已選取的學生資料();
        }

        private void 刪除已選取的學生資料()
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
    " (select ROW_NUMBER() OVER(ORDER BY Student_ID) AS ROWID, *" +
    " from Student) as A" +
    " where" +
    " ROWID = " + selected + ") X";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                int rows = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(String.Format("資料更新完畢, 共影響{0}筆資料", rows));
                載入學生資料表();
            }
        }

        #endregion 刪除

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        ////////////////////輸入完後在textbox按下ENTER則執行按鈕動作///////////////////////////////////////////////////
        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(this, new EventArgs());
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

        private void listBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)//&& (tabControl1.SelectedIndex == 1))
            {
                button3_Click(this, new EventArgs());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                將選取的資料內容複製到要修改的欄位上();
            }
        }

        private void 將選取的資料內容複製到要修改的欄位上()
        {
            int selected = listBox1.SelectedIndex + 1; //選取的序號
            //listBox1.Items.Clear();
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL =
" select *" +
" from" +
" (select ROW_NUMBER() OVER(ORDER BY Student_ID) AS ROWID, *" +
" from Student) as A" +
" where ROWID=" + selected;
            SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = string.Format("{0}", reader[1]);
                textBox2.Text = string.Format("{0}", reader[2]);
                textBox3.Text = string.Format("{0}", reader[3]);
            }
            reader.Close();
            con.Close();
        }

        void 查學生ID最大值()
        {
            listBox1.Items.Clear();
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select max(Student_ID) from Student";
                        SqlCommand cmd = new SqlCommand(strSQL, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                目前學生ID最大值=Convert.ToInt32($"{reader[0]}");
            }
            
            reader.Close();
            con.Close();
        }
    }
}