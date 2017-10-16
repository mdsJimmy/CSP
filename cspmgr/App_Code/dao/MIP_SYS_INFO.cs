using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_SYS_INFO 
    {

        #region private data
        private string _cKEY;
        private string _cVALUE;
        private string _cNOTE;
        private string _cSTATUS;
        private int _aPPLY4;

        #endregion

        #region Properties
        public string CKEY
        {
           get { return _cKEY; }
           set { _cKEY = value;}
        }
        public string CVALUE
        {
           get { return _cVALUE; }
           set { _cVALUE = value;}
        }
        public string CNOTE
        {
           get { return _cNOTE; }
           set { _cNOTE = value;}
        }
        public string CSTATUS
        {
           get { return _cSTATUS; }
           set { _cSTATUS = value;}
        }
        public int APPLY4
        {
           get { return _aPPLY4; }
           set { _aPPLY4 = value;}
        }

        #endregion

        #region Ctor/init
        public MIP_SYS_INFO() {}
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
                cmd.CommandText = "INSERT INTO MIP_SYS_INFO (CKEY, CVALUE, CNOTE, CSTATUS, APPLY4) VALUES (@CKEY_PARAMS, @CVALUE_PARAMS, @CNOTE_PARAMS, @CSTATUS_PARAMS, @APPLY4_PARAMS)";
                                cmd.Parameters.AddWithValue("@CKEY_PARAM", _cKEY);
                cmd.Parameters.AddWithValue("@CVALUE_PARAM", _cVALUE);
                cmd.Parameters.AddWithValue("@CNOTE_PARAM", _cNOTE);
                cmd.Parameters.AddWithValue("@CSTATUS_PARAM", _cSTATUS);
                cmd.Parameters.AddWithValue("@APPLY4_PARAM", _aPPLY4);

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
                cmd.CommandText = "SELECT CKEY, CVALUE, CNOTE, CSTATUS, APPLY4 FROM MIP_SYS_INFO WHERE CKEY=@CKEY_PARAM";
                                cmd.Parameters.AddWithValue("@CKEY_PARAM", _cKEY);

                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _cKEY = reader.GetString(0);
                _cVALUE = reader.GetString(1);
                _cNOTE = reader.GetString(2);
                _cSTATUS = reader.GetString(3);
                _aPPLY4 = reader.GetInt32(4);

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
                cmd.CommandText = "UPDATE MIP_SYS_INFO SET CKEY=@CKEY_PARAMS, CVALUE=@CVALUE_PARAMS, CNOTE=@CNOTE_PARAMS, CSTATUS=@CSTATUS_PARAMS, APPLY4=@APPLY4_PARAMS WHERE CKEY=@CKEY_PARAM";
                                cmd.Parameters.AddWithValue("@CKEY_PARAM", _cKEY);
                cmd.Parameters.AddWithValue("@CVALUE_PARAM", _cVALUE);
                cmd.Parameters.AddWithValue("@CNOTE_PARAM", _cNOTE);
                cmd.Parameters.AddWithValue("@CSTATUS_PARAM", _cSTATUS);
                cmd.Parameters.AddWithValue("@APPLY4_PARAM", _aPPLY4);

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
                cmd.CommandText = "DELETE FROM MIP_SYS_INFO WHERE CKEY=@CKEY_PARAM";
                cmd.Parameters.AddWithValue("@CKEY_PARAM", _cKEY);

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

