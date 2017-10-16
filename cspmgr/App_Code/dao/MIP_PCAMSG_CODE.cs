using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_PCAMSG_CODE 
    {

        #region private data
        private string _pCAMSG_KEY;
        private string _pCAMSG_NAME;
        private string _mIPCKEY;
        private int _cSTATUS;
        private int _cORDER;

        #endregion

        #region Properties
        public string PCAMSG_KEY
        {
           get { return _pCAMSG_KEY; }
           set { _pCAMSG_KEY = value;}
        }
        public string PCAMSG_NAME
        {
           get { return _pCAMSG_NAME; }
           set { _pCAMSG_NAME = value;}
        }
        public string MIPCKEY
        {
           get { return _mIPCKEY; }
           set { _mIPCKEY = value;}
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

        #endregion

        #region Ctor/init
        public MIP_PCAMSG_CODE() {}
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
                cmd.CommandText = "INSERT INTO MIP_PCAMSG_CODE (PCAMSG_KEY, PCAMSG_NAME, MIPCKEY, CSTATUS, CORDER) VALUES (@PCAMSG_KEY_PARAMS, @PCAMSG_NAME_PARAMS, @MIPCKEY_PARAMS, @CSTATUS_PARAMS, @CORDER_PARAMS)";
                                cmd.Parameters.AddWithValue("@PCAMSG_KEY_PARAM", _pCAMSG_KEY);
                cmd.Parameters.AddWithValue("@PCAMSG_NAME_PARAM", _pCAMSG_NAME);
                cmd.Parameters.AddWithValue("@MIPCKEY_PARAM", _mIPCKEY);
                cmd.Parameters.AddWithValue("@CSTATUS_PARAM", _cSTATUS);
                cmd.Parameters.AddWithValue("@CORDER_PARAM", _cORDER);

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
                cmd.CommandText = "SELECT PCAMSG_KEY, PCAMSG_NAME, MIPCKEY, CSTATUS, CORDER FROM MIP_PCAMSG_CODE WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _pCAMSG_KEY = reader.GetString(0);
                _pCAMSG_NAME = reader.GetString(1);
                _mIPCKEY = reader.GetString(2);
                _cSTATUS = reader.GetInt32(3);
                _cORDER = reader.GetInt32(4);

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
                cmd.CommandText = "UPDATE MIP_PCAMSG_CODE SET PCAMSG_KEY=@PCAMSG_KEY_PARAMS, PCAMSG_NAME=@PCAMSG_NAME_PARAMS, MIPCKEY=@MIPCKEY_PARAMS, CSTATUS=@CSTATUS_PARAMS, CORDER=@CORDER_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@PCAMSG_KEY_PARAM", _pCAMSG_KEY);
                cmd.Parameters.AddWithValue("@PCAMSG_NAME_PARAM", _pCAMSG_NAME);
                cmd.Parameters.AddWithValue("@MIPCKEY_PARAM", _mIPCKEY);
                cmd.Parameters.AddWithValue("@CSTATUS_PARAM", _cSTATUS);
                cmd.Parameters.AddWithValue("@CORDER_PARAM", _cORDER);

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
                cmd.CommandText = "DELETE FROM MIP_PCAMSG_CODE WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

