
            //////////////////////////////////////////////////////////////////////////////////////////��Ʈw���s�u����(��FormLoad)
            scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "CR3-04";
            scsb.InitialCatalog = "DinBianDon";//�s�u����Ʈw�W��
            scsb.IntegratedSecurity = true;//windows��X����
                                           /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            /////Sql���󪺰򥻥Ϊk
            SqlConnection con = new SqlConnection(scsb.ToString());//�s�u����A�ѼƱ��J�s�u�r��
            con.Open();

            //������������ SQL�s�u���O
            string strSQL = "select top 500 * from Student";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            //������������
            SqlDataReader reader = cmd.ExecuteReader();// SqlDataReader�����d�᪺߫���G

            /* //////DataTable����load�Jreader�����///////////////////
             DataTable mydatatable = new DataTable();
             mydatatable.Load(reader);
             comboBox1.DataSource = mydatatable;
             comboBox1.DisplayMember = "Student_name";
             /////////////////////////�\��P�U����while loop////////////////////*/

            while (reader.Read())
            {
                comboBox1.Items.Add(reader["Student_name"]);
                //                label1.Text = (string)reader["Student_name"];
            }

            /////�ϥΧ��n�����pô///////////
            reader.Close();        ////////////
            con.Close();            ////////////
            ///////////////////////////////////////////