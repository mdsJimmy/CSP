using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_CODES 
    {

        #region private data
        private string _cKEY;
        private string _cNAME;
        private string _cLEVEL;
        private int _cSTATUS;
        private int _cORDER;
        private string _cNOTE;

        #endregion

        #region Properties
        public string CKEY
        {
           get { return _cKEY; }
           set { _cKEY = value;}
        }
        public string CNAME
        {
           get { return _cNAME; }
           set { _cNAME = value;}
        }
        public string CLEVEL
        {
           get { return _cLEVEL; }
           set { _cLEVEL = value;}
        }
        public int CSTATUS
        {
           get { return _cSTATUS; }
           set { _cSTATUS = value;}
        }
        public int CORDER
        {
           get { return _cORDER; }
           set { _cORDER = value;}
        }
        public string CNOTE
        {
           get { return _cNOTE; }
           set { _cNOTE = value;}
        }

        #endregion

        #region Ctor/init
        public MIP_CODES() {}
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
                cmd.CommandText = "INSERT INTO MIP_CODES (CKEY, CNAME, CLEVEL, CSTATUS, CORDER, CNOTE) VALUES (@CKEY_PARAMS, @CNAME_PARAMS, @CLEVEL_PARAMS, @CSTATUS_PARAMS, @CORDER_PARAMS, @CNOTE_PARAMS)";
                                cmd.Parameters.AddWithValue("@CKEY_PARAM", _cKEY);
                cmd.Parameters.AddWithValue("@CNAME_PARAM", _cNAME);
                cmd.Parameters.AddWithValue("@CLEVEL_PARAM", _cLEVEL);
                cmd.Parameters.AddWithValue("@CSTATUS_PARAM", _cSTATUS);
                cmd.Parameters.AddWithValue("@CORDER_PARAM", _cORDER);
                cmd.Parameters.AddWithValue("@CNOTE_PARAM", _cNOTE);

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
                cmd.CommandText = "SELECT CKEY, CNAME, CLEVEL, CSTATUS, CORDER, CNOTE FROM MIP_CODES WHERE CKEY=@CKEY_PARAM, CLEVEL=@CLEVEL_PARAM";
                                cmd.Parameters.AddWithValue("@CKEY_PARAM", _cKEY);
                cmd.Parameters.AddWithValue("@CLEVEL_PARAM", _cLEVEL);

                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _cKEY = reader.GetString(0);
                _cNAME = reader.GetString(1);
                _cLEVEL = reader.GetString(2);
                _cSTATUS = reader.GetInt32(3);
                _cORDER = reader.GetInt32(4);
                _cNOTE = reader.GetString(5);

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
                cmd.CommandText = "UPDATE MIP_CODES SET CKEY=@CKEY_PARAMS, CNAME=@CNAME_PARAMS, CLEVEL=@CLEVEL_PARAMS, CSTATUS=@CSTATUS_PARAMS, CORDER=@CORDER_PARAMS, CNOTE=@CNOTE_PARAMS WHERE CKEY=@CKEY_PARAM, CLEVEL=@CLEVEL_PARAM";
                                cmd.Parameters.AddWithValue("@CKEY_PARAM", _cKEY);
                cmd.Parameters.AddWithValue("@CNAME_PARAM", _cNAME);
                cmd.Parameters.AddWithValue("@CLEVEL_PARAM", _cLEVEL);
                cmd.Parameters.AddWithValue("@CSTATUS_PARAM", _cSTATUS);
                cmd.Parameters.AddWithValue("@CORDER_PARAM", _cORDER);
                cmd.Parameters.AddWithValue("@CNOTE_PARAM", _cNOTE);

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
                cmd.CommandText = "DELETE FROM MIP_CODES WHERE CKEY=@CKEY_PARAM, CLEVEL=@CLEVEL_PARAM";
                cmd.Parameters.AddWithValue("@CKEY_PARAM", _cKEY);
                cmd.Parameters.AddWithValue("@CLEVEL_PARAM", _cLEVEL);

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

