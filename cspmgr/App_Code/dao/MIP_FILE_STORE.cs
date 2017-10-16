using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_FILE_STORE 
    {

        #region private data
        private int _fILE_INDEX;
        private string _fILE_NEW_NAME;
        private string _fILE_ORI_NAME;
        private string _fILE_MD5;
        private byte[] _fILE_IMG;
        private int _rECSTA;
        private DateTime _lDATE;
        private string _lUSER;

        #endregion

        #region Properties
        public int FILE_INDEX
        {
           get { return _fILE_INDEX; }
           set { _fILE_INDEX = value;}
        }
        public string FILE_NEW_NAME
        {
           get { return _fILE_NEW_NAME; }
           set { _fILE_NEW_NAME = value;}
        }
        public string FILE_ORI_NAME
        {
           get { return _fILE_ORI_NAME; }
           set { _fILE_ORI_NAME = value;}
        }
        public string FILE_MD5
        {
           get { return _fILE_MD5; }
           set { _fILE_MD5 = value;}
        }
        public byte[] FILE_IMG
        {
           get { return _fILE_IMG; }
           set { _fILE_IMG = value;}
        }
        public int RECSTA
        {
           get { return _rECSTA; }
           set { _rECSTA = value;}
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

        #endregion

        #region Ctor/init
        public MIP_FILE_STORE() {}
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
                cmd.CommandText = "INSERT INTO MIP_FILE_STORE (FILE_INDEX, FILE_NEW_NAME, FILE_ORI_NAME, FILE_MD5, FILE_IMG, RECSTA, LDATE, LUSER) VALUES (@FILE_INDEX_PARAMS, @FILE_NEW_NAME_PARAMS, @FILE_ORI_NAME_PARAMS, @FILE_MD5_PARAMS, @FILE_IMG_PARAMS, @RECSTA_PARAMS, @LDATE_PARAMS, @LUSER_PARAMS)";
                                cmd.Parameters.AddWithValue("@FILE_INDEX_PARAM", _fILE_INDEX);
                cmd.Parameters.AddWithValue("@FILE_NEW_NAME_PARAM", _fILE_NEW_NAME);
                cmd.Parameters.AddWithValue("@FILE_ORI_NAME_PARAM", _fILE_ORI_NAME);
                cmd.Parameters.AddWithValue("@FILE_MD5_PARAM", _fILE_MD5);
                cmd.Parameters.AddWithValue("@FILE_IMG_PARAM", _fILE_IMG);
                cmd.Parameters.AddWithValue("@RECSTA_PARAM", _rECSTA);
                cmd.Parameters.AddWithValue("@LDATE_PARAM", _lDATE);
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
                cmd.CommandText = "SELECT FILE_INDEX, FILE_NEW_NAME, FILE_ORI_NAME, FILE_MD5, FILE_IMG, RECSTA, LDATE, LUSER FROM MIP_FILE_STORE WHERE FILE_INDEX=@FILE_INDEX_PARAM";
                                cmd.Parameters.AddWithValue("@FILE_INDEX_PARAM", _fILE_INDEX);

                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _fILE_INDEX = reader.GetInt32(0);
                _fILE_NEW_NAME = reader.GetString(1);
                _fILE_ORI_NAME = reader.GetString(2);
                _fILE_MD5 = reader.GetString(3);
                //_fILE_IMG = reader.GetByte(4);
                _fILE_IMG = (byte[])reader[4];
                _rECSTA = reader.GetInt32(5);
                _lDATE = reader.GetDateTime(6);
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
                cmd.CommandText = "UPDATE MIP_FILE_STORE SET FILE_INDEX=@FILE_INDEX_PARAMS, FILE_NEW_NAME=@FILE_NEW_NAME_PARAMS, FILE_ORI_NAME=@FILE_ORI_NAME_PARAMS, FILE_MD5=@FILE_MD5_PARAMS, FILE_IMG=@FILE_IMG_PARAMS, RECSTA=@RECSTA_PARAMS, LDATE=@LDATE_PARAMS, LUSER=@LUSER_PARAMS WHERE FILE_INDEX=@FILE_INDEX_PARAM";
                                cmd.Parameters.AddWithValue("@FILE_INDEX_PARAM", _fILE_INDEX);
                cmd.Parameters.AddWithValue("@FILE_NEW_NAME_PARAM", _fILE_NEW_NAME);
                cmd.Parameters.AddWithValue("@FILE_ORI_NAME_PARAM", _fILE_ORI_NAME);
                cmd.Parameters.AddWithValue("@FILE_MD5_PARAM", _fILE_MD5);
                cmd.Parameters.AddWithValue("@FILE_IMG_PARAM", _fILE_IMG);
                cmd.Parameters.AddWithValue("@RECSTA_PARAM", _rECSTA);
                cmd.Parameters.AddWithValue("@LDATE_PARAM", _lDATE);
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
                cmd.CommandText = "DELETE FROM MIP_FILE_STORE WHERE FILE_INDEX=@FILE_INDEX_PARAM";
                cmd.Parameters.AddWithValue("@FILE_INDEX_PARAM", _fILE_INDEX);

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

