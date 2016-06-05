
            //////////////////////////////////////////////////////////////////////////////////////////資料庫的連線物件(放FormLoad)
            scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "CR3-04";
            scsb.InitialCatalog = "DinBianDon";//連線的資料庫名稱
            scsb.IntegratedSecurity = true;//windows整合驗證
                                           /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            /////Sql物件的基本用法
            SqlConnection con = new SqlConnection(scsb.ToString());//連線物件，參數掛入連線字串
            con.Open();

            //↓↓↓↓↓↓ SQL連線指令
            string strSQL = "select top 500 * from Student";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            //↑↑↑↑↑↑
            SqlDataReader reader = cmd.ExecuteReader();// SqlDataReader接收查詢後的結果

            /* //////DataTable物件load入reader的資料///////////////////
             DataTable mydatatable = new DataTable();
             mydatatable.Load(reader);
             comboBox1.DataSource = mydatatable;
             comboBox1.DisplayMember = "Student_name";
             /////////////////////////功能同下面的while loop////////////////////*/

            while (reader.Read())
            {
                comboBox1.Items.Add(reader["Student_name"]);
                //                label1.Text = (string)reader["Student_name"];
            }

            /////使用完要關閉聯繫///////////
            reader.Close();        ////////////
            con.Close();            ////////////
            ///////////////////////////////////////////