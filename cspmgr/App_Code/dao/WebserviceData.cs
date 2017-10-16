using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class WebserviceData 
    {

        #region private data
        private string _serviceID;
        private string _sUrl;
        private string _sNamespace;
        private string _sClassname;
        private string _aPName;
        private string _sDescription;
        private bool _sStatus;
        private DateTime _sCreateDatetime;
        private bool _eNCRYPTED;

        #endregion

        #region Properties
        public string ServiceID
        {
           get { return _serviceID; }
           set { _serviceID = value;}
        }
        public string sUrl
        {
           get { return _sUrl; }
           set { _sUrl = value;}
        }
        public string sNamespace
        {
           get { return _sNamespace; }
           set { _sNamespace = value;}
        }
        public string sClassname
        {
           get { return _sClassname; }
           set { _sClassname = value;}
        }
        public string APName
        {
           get { return _aPName; }
           set { _aPName = value;}
        }
        public string sDescription
        {
           get { return _sDescription; }
           set { _sDescription = value;}
        }
        public bool sStatus
        {
           get { return _sStatus; }
           set { _sStatus = value;}
        }
        public DateTime sCreateDatetime
        {
           get { return _sCreateDatetime; }
           set { _sCreateDatetime = value;}
        }
        public bool ENCRYPTED
        {
           get { return _eNCRYPTED; }
           set { _eNCRYPTED = value;}
        }

        #endregion

        #region Ctor/init
        public WebserviceData() {}
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
                cmd.CommandText = "INSERT INTO WebserviceData (ServiceID, sUrl, sNamespace, sClassname, APName, sDescription, sStatus, sCreateDatetime, ENCRYPTED) VALUES (@ServiceID_PARAMS, @sUrl_PARAMS, @sNamespace_PARAMS, @sClassname_PARAMS, @APName_PARAMS, @sDescription_PARAMS, @sStatus_PARAMS, @sCreateDatetime_PARAMS, @ENCRYPTED_PARAMS)";
                                cmd.Parameters.AddWithValue("@ServiceID_PARAM", _serviceID);
                cmd.Parameters.AddWithValue("@sUrl_PARAM", _sUrl);
                cmd.Parameters.AddWithValue("@sNamespace_PARAM", _sNamespace);
                cmd.Parameters.AddWithValue("@sClassname_PARAM", _sClassname);
                cmd.Parameters.AddWithValue("@APName_PARAM", _aPName);
                cmd.Parameters.AddWithValue("@sDescription_PARAM", _sDescription);
                cmd.Parameters.AddWithValue("@sStatus_PARAM", _sStatus);
                cmd.Parameters.AddWithValue("@sCreateDatetime_PARAM", _sCreateDatetime);
                cmd.Parameters.AddWithValue("@ENCRYPTED_PARAM", _eNCRYPTED);

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
                cmd.CommandText = "SELECT ServiceID, sUrl, sNamespace, sClassname, APName, sDescription, sStatus, sCreateDatetime, ENCRYPTED FROM WebserviceData WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _serviceID = reader.GetString(0);
                _sUrl = reader.GetString(1);
                _sNamespace = reader.GetString(2);
                _sClassname = reader.GetString(3);
                _aPName = reader.GetString(4);
                _sDescription = reader.GetString(5);
                _sStatus = reader.GetBoolean(6);
                _sCreateDatetime = reader.GetDateTime(7);
                _eNCRYPTED = reader.GetBoolean(8);

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
                cmd.CommandText = "UPDATE WebserviceData SET ServiceID=@ServiceID_PARAMS, sUrl=@sUrl_PARAMS, sNamespace=@sNamespace_PARAMS, sClassname=@sClassname_PARAMS, APName=@APName_PARAMS, sDescription=@sDescription_PARAMS, sStatus=@sStatus_PARAMS, sCreateDatetime=@sCreateDatetime_PARAMS, ENCRYPTED=@ENCRYPTED_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@ServiceID_PARAM", _serviceID);
                cmd.Parameters.AddWithValue("@sUrl_PARAM", _sUrl);
                cmd.Parameters.AddWithValue("@sNamespace_PARAM", _sNamespace);
                cmd.Parameters.AddWithValue("@sClassname_PARAM", _sClassname);
                cmd.Parameters.AddWithValue("@APName_PARAM", _aPName);
                cmd.Parameters.AddWithValue("@sDescription_PARAM", _sDescription);
                cmd.Parameters.AddWithValue("@sStatus_PARAM", _sStatus);
                cmd.Parameters.AddWithValue("@sCreateDatetime_PARAM", _sCreateDatetime);
                cmd.Parameters.AddWithValue("@ENCRYPTED_PARAM", _eNCRYPTED);

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
                cmd.CommandText = "DELETE FROM WebserviceData WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

