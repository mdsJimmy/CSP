using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_EMAIL 
    {

        #region private data
        private int _mAIL_ID;
        private int _aPP_TYPE;
        private string _sOURCE;
        private string _eMAIL_ADDR;
        private int _mAINORCC;
        private string _nOTETXT;
        private DateTime _lDATA;
        private string _lUSER;

        #endregion

        #region Properties
        public int MAIL_ID
        {
           get { return _mAIL_ID; }
           set { _mAIL_ID = value;}
        }
        public int APP_TYPE
        {
           get { return _aPP_TYPE; }
           set { _aPP_TYPE = value;}
        }
        public string SOURCE
        {
           get { return _sOURCE; }
           set { _sOURCE = value;}
        }
        public string EMAIL_ADDR
        {
           get { return _eMAIL_ADDR; }
           set { _eMAIL_ADDR = value;}
        }
        public int MAINORCC
        {
           get { return _mAINORCC; }
           set { _mAINORCC = value;}
        }
        public string NOTETXT
        {
           get { return _nOTETXT; }
           set { _nOTETXT = value;}
        }
        public DateTime LDATA
        {
           get { return _lDATA; }
           set { _lDATA = value;}
        }
        public string LUSER
        {
           get { return _lUSER; }
           set { _lUSER = value;}
        }

        #endregion

        #region Ctor/init
        public MIP_EMAIL() {}
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
                cmd.CommandText = "INSERT INTO MIP_EMAIL (MAIL_ID, APP_TYPE, SOURCE, EMAIL_ADDR, MAINORCC, NOTETXT, LDATA, LUSER) VALUES (@MAIL_ID_PARAMS, @APP_TYPE_PARAMS, @SOURCE_PARAMS, @EMAIL_ADDR_PARAMS, @MAINORCC_PARAMS, @NOTETXT_PARAMS, @LDATA_PARAMS, @LUSER_PARAMS)";
                                cmd.Parameters.AddWithValue("@MAIL_ID_PARAM", _mAIL_ID);
                cmd.Parameters.AddWithValue("@APP_TYPE_PARAM", _aPP_TYPE);
                cmd.Parameters.AddWithValue("@SOURCE_PARAM", _sOURCE);
                cmd.Parameters.AddWithValue("@EMAIL_ADDR_PARAM", _eMAIL_ADDR);
                cmd.Parameters.AddWithValue("@MAINORCC_PARAM", _mAINORCC);
                cmd.Parameters.AddWithValue("@NOTETXT_PARAM", _nOTETXT);
                cmd.Parameters.AddWithValue("@LDATA_PARAM", _lDATA);
                cmd.Parameters.AddWithValue("@LUSER_PARAM", _lUSER);

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
                cmd.CommandText = "SELECT MAIL_ID, APP_TYPE, SOURCE, EMAIL_ADDR, MAINORCC, NOTETXT, LDATA, LUSER FROM MIP_EMAIL WHERE MAIL_ID=@MAIL_ID_PARAM";
                                cmd.Parameters.AddWithValue("@MAIL_ID_PARAM", _mAIL_ID);

                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _mAIL_ID = reader.GetInt32(0);
                _aPP_TYPE = reader.GetInt32(1);
                _sOURCE = reader.GetString(2);
                _eMAIL_ADDR = reader.GetString(3);
                _mAINORCC = reader.GetInt32(4);
                _nOTETXT = reader.GetString(5);
                _lDATA = reader.GetDateTime(6);
                _lUSER = reader.GetString(7);

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
                cmd.CommandText = "UPDATE MIP_EMAIL SET MAIL_ID=@MAIL_ID_PARAMS, APP_TYPE=@APP_TYPE_PARAMS, SOURCE=@SOURCE_PARAMS, EMAIL_ADDR=@EMAIL_ADDR_PARAMS, MAINORCC=@MAINORCC_PARAMS, NOTETXT=@NOTETXT_PARAMS, LDATA=@LDATA_PARAMS, LUSER=@LUSER_PARAMS WHERE MAIL_ID=@MAIL_ID_PARAM";
                                cmd.Parameters.AddWithValue("@MAIL_ID_PARAM", _mAIL_ID);
                cmd.Parameters.AddWithValue("@APP_TYPE_PARAM", _aPP_TYPE);
                cmd.Parameters.AddWithValue("@SOURCE_PARAM", _sOURCE);
                cmd.Parameters.AddWithValue("@EMAIL_ADDR_PARAM", _eMAIL_ADDR);
                cmd.Parameters.AddWithValue("@MAINORCC_PARAM", _mAINORCC);
                cmd.Parameters.AddWithValue("@NOTETXT_PARAM", _nOTETXT);
                cmd.Parameters.AddWithValue("@LDATA_PARAM", _lDATA);
                cmd.Parameters.AddWithValue("@LUSER_PARAM", _lUSER);

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
                cmd.CommandText = "DELETE FROM MIP_EMAIL WHERE MAIL_ID=@MAIL_ID_PARAM";
                cmd.Parameters.AddWithValue("@MAIL_ID_PARAM", _mAIL_ID);

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

