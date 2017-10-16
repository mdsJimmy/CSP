using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_PCADEPT 
    {

        #region private data
        private string _dEPT_ID;
        private string _dEPT_NAME;
        private int _cORDER;
        private int _cSTATUS;
        private DateTime _lDATE;
        private string _lUSER;
        private string _nICK_NAME;

        #endregion

        #region Properties
        public string DEPT_ID
        {
           get { return _dEPT_ID; }
           set { _dEPT_ID = value;}
        }
        public string DEPT_NAME
        {
           get { return _dEPT_NAME; }
           set { _dEPT_NAME = value;}
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
        public string NICK_NAME
        {
           get { return _nICK_NAME; }
           set { _nICK_NAME = value;}
        }

        #endregion

        #region Ctor/init
        public MIP_PCADEPT() {}
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
                cmd.CommandText = "INSERT INTO MIP_PCADEPT (DEPT_ID, DEPT_NAME, CORDER, CSTATUS, LDATE, LUSER, NICK_NAME) VALUES (@DEPT_ID_PARAMS, @DEPT_NAME_PARAMS, @CORDER_PARAMS, @CSTATUS_PARAMS, @LDATE_PARAMS, @LUSER_PARAMS, @NICK_NAME_PARAMS)";
                                cmd.Parameters.AddWithValue("@DEPT_ID_PARAM", _dEPT_ID);
                cmd.Parameters.AddWithValue("@DEPT_NAME_PARAM", _dEPT_NAME);
                cmd.Parameters.AddWithValue("@CORDER_PARAM", _cORDER);
                cmd.Parameters.AddWithValue("@CSTATUS_PARAM", _cSTATUS);
                cmd.Parameters.AddWithValue("@LDATE_PARAM", _lDATE);
                cmd.Parameters.AddWithValue("@LUSER_PARAM", _lUSER);
                cmd.Parameters.AddWithValue("@NICK_NAME_PARAM", _nICK_NAME);

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
                cmd.CommandText = "SELECT DEPT_ID, DEPT_NAME, CORDER, CSTATUS, LDATE, LUSER, NICK_NAME FROM MIP_PCADEPT WHERE DEPT_ID=@DEPT_ID_PARAM";
                                cmd.Parameters.AddWithValue("@DEPT_ID_PARAM", _dEPT_ID);

                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _dEPT_ID = reader.GetString(0);
                _dEPT_NAME = reader.GetString(1);
                _cORDER = reader.GetInt32(2);
                _cSTATUS = reader.GetInt32(3);
                _lDATE = reader.GetDateTime(4);
                _lUSER = reader.GetString(5);
                _nICK_NAME = reader.GetString(6);

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
                cmd.CommandText = "UPDATE MIP_PCADEPT SET DEPT_ID=@DEPT_ID_PARAMS, DEPT_NAME=@DEPT_NAME_PARAMS, CORDER=@CORDER_PARAMS, CSTATUS=@CSTATUS_PARAMS, LDATE=@LDATE_PARAMS, LUSER=@LUSER_PARAMS, NICK_NAME=@NICK_NAME_PARAMS WHERE DEPT_ID=@DEPT_ID_PARAM";
                                cmd.Parameters.AddWithValue("@DEPT_ID_PARAM", _dEPT_ID);
                cmd.Parameters.AddWithValue("@DEPT_NAME_PARAM", _dEPT_NAME);
                cmd.Parameters.AddWithValue("@CORDER_PARAM", _cORDER);
                cmd.Parameters.AddWithValue("@CSTATUS_PARAM", _cSTATUS);
                cmd.Parameters.AddWithValue("@LDATE_PARAM", _lDATE);
                cmd.Parameters.AddWithValue("@LUSER_PARAM", _lUSER);
                cmd.Parameters.AddWithValue("@NICK_NAME_PARAM", _nICK_NAME);

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
                cmd.CommandText = "DELETE FROM MIP_PCADEPT WHERE DEPT_ID=@DEPT_ID_PARAM";
                cmd.Parameters.AddWithValue("@DEPT_ID_PARAM", _dEPT_ID);

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

