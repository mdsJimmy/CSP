using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class SQLITE_CODE 
    {

        #region private data
        private int _sqltype;
        private string _description;
        private string _filename;
        private int _sECURITY_FILE;

        #endregion

        #region Properties
        public int sqltype
        {
           get { return _sqltype; }
           set { _sqltype = value;}
        }
        public string description
        {
           get { return _description; }
           set { _description = value;}
        }
        public string filename
        {
           get { return _filename; }
           set { _filename = value;}
        }
        public int SECURITY_FILE
        {
           get { return _sECURITY_FILE; }
           set { _sECURITY_FILE = value;}
        }

        #endregion

        #region Ctor/init
        public SQLITE_CODE() {}
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
                cmd.CommandText = "INSERT INTO SQLITE_CODE (sqltype, description, filename, SECURITY_FILE) VALUES (@sqltype_PARAMS, @description_PARAMS, @filename_PARAMS, @SECURITY_FILE_PARAMS)";
                                cmd.Parameters.AddWithValue("@sqltype_PARAM", _sqltype);
                cmd.Parameters.AddWithValue("@description_PARAM", _description);
                cmd.Parameters.AddWithValue("@filename_PARAM", _filename);
                cmd.Parameters.AddWithValue("@SECURITY_FILE_PARAM", _sECURITY_FILE);

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
                cmd.CommandText = "SELECT sqltype, description, filename, SECURITY_FILE FROM SQLITE_CODE WHERE sqltype=@sqltype_PARAM";
                                cmd.Parameters.AddWithValue("@sqltype_PARAM", _sqltype);

                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _sqltype = reader.GetInt32(0);
                _description = reader.GetString(1);
                _filename = reader.GetString(2);
                _sECURITY_FILE = reader.GetInt32(3);

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
                cmd.CommandText = "UPDATE SQLITE_CODE SET sqltype=@sqltype_PARAMS, description=@description_PARAMS, filename=@filename_PARAMS, SECURITY_FILE=@SECURITY_FILE_PARAMS WHERE sqltype=@sqltype_PARAM";
                                cmd.Parameters.AddWithValue("@sqltype_PARAM", _sqltype);
                cmd.Parameters.AddWithValue("@description_PARAM", _description);
                cmd.Parameters.AddWithValue("@filename_PARAM", _filename);
                cmd.Parameters.AddWithValue("@SECURITY_FILE_PARAM", _sECURITY_FILE);

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
                cmd.CommandText = "DELETE FROM SQLITE_CODE WHERE sqltype=@sqltype_PARAM";
                cmd.Parameters.AddWithValue("@sqltype_PARAM", _sqltype);

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

