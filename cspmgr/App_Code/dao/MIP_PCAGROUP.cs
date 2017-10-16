using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_PCAGROUP 
    {

        #region private data
        private string _pCAGROUP_ID;
        private string _pCAGROUP_NAME;
        private int _cORDER;
        private int _cSTATUS;
        private DateTime _lDATE;
        private string _lUSER;
        private int _lOGINLIST;

        #endregion

        #region Properties
        public string PCAGROUP_ID
        {
           get { return _pCAGROUP_ID; }
           set { _pCAGROUP_ID = value;}
        }
        public string PCAGROUP_NAME
        {
           get { return _pCAGROUP_NAME; }
           set { _pCAGROUP_NAME = value;}
        }
        public int CORDER
        {
           get { return _cORDER; }
           set { _cORDER = value;}
        }
        public int CSTATUS
        {
           get { return _cSTATUS; }
           set { _cSTATUS = value;}
        }
        public DateTime LDATE
        {
           get { return _lDATE; }
           set { _lDATE = value;}
        }
        public string LUSER
        {
           get { return _lUSER; }
           set { _lUSER = value;}
        }
        public int LOGINLIST
        {
           get { return _lOGINLIST; }
           set { _lOGINLIST = value;}
        }

        #endregion

        #region Ctor/init
        public MIP_PCAGROUP() {}
        #endregion

        #region Data Access Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public void Insert(System.Data.SqlClient.SqlConnection connection)
        {
            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "INSERT INTO MIP_PCAGROUP (PCAGROUP_ID, PCAGROUP_NAME, CORDER, CSTATUS, LDATE, LUSER, LOGINLIST) VALUES (@PCAGROUP_ID_PARAMS, @PCAGROUP_NAME_PARAMS, @CORDER_PARAMS, @CSTATUS_PARAMS, @LDATE_PARAMS, @LUSER_PARAMS, @LOGINLIST_PARAMS)";
                                cmd.Parameters.AddWithValue("@PCAGROUP_ID_PARAM", _pCAGROUP_ID);
                cmd.Parameters.AddWithValue("@PCAGROUP_NAME_PARAM", _pCAGROUP_NAME);
                cmd.Parameters.AddWithValue("@CORDER_PARAM", _cORDER);
                cmd.Parameters.AddWithValue("@CSTATUS_PARAM", _cSTATUS);
                cmd.Parameters.AddWithValue("@LDATE_PARAM", _lDATE);
                cmd.Parameters.AddWithValue("@LUSER_PARAM", _lUSER);
                cmd.Parameters.AddWithValue("@LOGINLIST_PARAM", _lOGINLIST);

                cmd.ExecuteNonQuery();

            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void Load(System.Data.SqlClient.SqlConnection connection)
        {
            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "SELECT PCAGROUP_ID, PCAGROUP_NAME, CORDER, CSTATUS, LDATE, LUSER, LOGINLIST FROM MIP_PCAGROUP WHERE PCAGROUP_ID=@PCAGROUP_ID_PARAM";
                                cmd.Parameters.AddWithValue("@PCAGROUP_ID_PARAM", _pCAGROUP_ID);

                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _pCAGROUP_ID = reader.GetString(0);
                _pCAGROUP_NAME = reader.GetString(1);
                _cORDER = reader.GetInt32(2);
                _cSTATUS = reader.GetInt32(3);
                _lDATE = reader.GetDateTime(4);
                _lUSER = reader.GetString(5);
                _lOGINLIST = reader.GetInt32(6);

                }

                reader.Close();

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public void Update(System.Data.SqlClient.SqlConnection connection)
        {
            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "UPDATE MIP_PCAGROUP SET PCAGROUP_ID=@PCAGROUP_ID_PARAMS, PCAGROUP_NAME=@PCAGROUP_NAME_PARAMS, CORDER=@CORDER_PARAMS, CSTATUS=@CSTATUS_PARAMS, LDATE=@LDATE_PARAMS, LUSER=@LUSER_PARAMS, LOGINLIST=@LOGINLIST_PARAMS WHERE PCAGROUP_ID=@PCAGROUP_ID_PARAM";
                                cmd.Parameters.AddWithValue("@PCAGROUP_ID_PARAM", _pCAGROUP_ID);
                cmd.Parameters.AddWithValue("@PCAGROUP_NAME_PARAM", _pCAGROUP_NAME);
                cmd.Parameters.AddWithValue("@CORDER_PARAM", _cORDER);
                cmd.Parameters.AddWithValue("@CSTATUS_PARAM", _cSTATUS);
                cmd.Parameters.AddWithValue("@LDATE_PARAM", _lDATE);
                cmd.Parameters.AddWithValue("@LUSER_PARAM", _lUSER);
                cmd.Parameters.AddWithValue("@LOGINLIST_PARAM", _lOGINLIST);

                cmd.ExecuteNonQuery();

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        private void Delete(System.Data.SqlClient.SqlConnection connection)
        {
            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "DELETE FROM MIP_PCAGROUP WHERE PCAGROUP_ID=@PCAGROUP_ID_PARAM";
                cmd.Parameters.AddWithValue("@PCAGROUP_ID_PARAM", _pCAGROUP_ID);

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

