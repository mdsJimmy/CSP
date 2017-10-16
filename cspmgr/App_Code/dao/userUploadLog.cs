using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class userUploadLog 
    {

        #region private data
        private int _version;
        private int _sqltype;
        private string _file_desc;
        private string _fileUploadOldName;
        private string _fileUploadNewName;
        private DateTime _datetime;
        private string _version_no;
        private byte[] _imageData;

        #endregion

        #region Properties
        public int version
        {
           get { return _version; }
           set { _version = value;}
        }
        public int sqltype
        {
           get { return _sqltype; }
           set { _sqltype = value;}
        }
        public string file_desc
        {
           get { return _file_desc; }
           set { _file_desc = value;}
        }
        public string fileUploadOldName
        {
           get { return _fileUploadOldName; }
           set { _fileUploadOldName = value;}
        }
        public string fileUploadNewName
        {
           get { return _fileUploadNewName; }
           set { _fileUploadNewName = value;}
        }
        public DateTime datetime
        {
           get { return _datetime; }
           set { _datetime = value;}
        }
        public string version_no
        {
           get { return _version_no; }
           set { _version_no = value;}
        }
        public byte[] imageData
        {
           get { return _imageData; }
           set { _imageData = value;}
        }

        #endregion

        #region Ctor/init
        public userUploadLog() {}
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
                cmd.CommandText = "INSERT INTO userUploadLog (version, sqltype, file_desc, fileUploadOldName, fileUploadNewName, datetime, version_no, imageData) VALUES (@version_PARAMS, @sqltype_PARAMS, @file_desc_PARAMS, @fileUploadOldName_PARAMS, @fileUploadNewName_PARAMS, @datetime_PARAMS, @version_no_PARAMS, @imageData_PARAMS)";
                                cmd.Parameters.AddWithValue("@version_PARAM", _version);
                cmd.Parameters.AddWithValue("@sqltype_PARAM", _sqltype);
                cmd.Parameters.AddWithValue("@file_desc_PARAM", _file_desc);
                cmd.Parameters.AddWithValue("@fileUploadOldName_PARAM", _fileUploadOldName);
                cmd.Parameters.AddWithValue("@fileUploadNewName_PARAM", _fileUploadNewName);
                cmd.Parameters.AddWithValue("@datetime_PARAM", _datetime);
                cmd.Parameters.AddWithValue("@version_no_PARAM", _version_no);
                cmd.Parameters.AddWithValue("@imageData_PARAM", _imageData);

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
                cmd.CommandText = "SELECT version, sqltype, file_desc, fileUploadOldName, fileUploadNewName, datetime, version_no, imageData FROM userUploadLog WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                _version = reader.GetInt32(0);
                _sqltype = reader.GetInt32(1);
                _file_desc = reader.GetString(2);
                _fileUploadOldName = reader.GetString(3);
                _fileUploadNewName = reader.GetString(4);
                _datetime = reader.GetDateTime(5);
                _version_no = reader.GetString(6);
                //_imageData = reader.GetBytes(7);
                _imageData = (byte[])reader[7];

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
                cmd.CommandText = "UPDATE userUploadLog SET version=@version_PARAMS, sqltype=@sqltype_PARAMS, file_desc=@file_desc_PARAMS, fileUploadOldName=@fileUploadOldName_PARAMS, fileUploadNewName=@fileUploadNewName_PARAMS, datetime=@datetime_PARAMS, version_no=@version_no_PARAMS, imageData=@imageData_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@version_PARAM", _version);
                cmd.Parameters.AddWithValue("@sqltype_PARAM", _sqltype);
                cmd.Parameters.AddWithValue("@file_desc_PARAM", _file_desc);
                cmd.Parameters.AddWithValue("@fileUploadOldName_PARAM", _fileUploadOldName);
                cmd.Parameters.AddWithValue("@fileUploadNewName_PARAM", _fileUploadNewName);
                cmd.Parameters.AddWithValue("@datetime_PARAM", _datetime);
                cmd.Parameters.AddWithValue("@version_no_PARAM", _version_no);
                cmd.Parameters.AddWithValue("@imageData_PARAM", _imageData);

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
                cmd.CommandText = "DELETE FROM userUploadLog WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

