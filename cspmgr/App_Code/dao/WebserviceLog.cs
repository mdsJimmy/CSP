using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class WebserviceLog 
    {

        #region private data
        private string _logID;
        private string _deviceid;
        private string _phonetype;
        private string _webservicenode;
        private string _webservicemethod;
        private string _webserviceparams;
        private string _serverReturnData;
        private DateTime _sCreateDatetime;
        private DateTime _sRequestDatetime;
        private string _webserviceparamsDecode;
        private int _statuscode;
        private string _iD;
        private string _user_type;
        private string _version;

        #endregion

        #region Properties
        public string logID
        {
           get { return _logID; }
           set { _logID = value;}
        }
        public string deviceid
        {
           get { return _deviceid; }
           set { _deviceid = value;}
        }
        public string phonetype
        {
           get { return _phonetype; }
           set { _phonetype = value;}
        }
        public string webservicenode
        {
           get { return _webservicenode; }
           set { _webservicenode = value;}
        }
        public string webservicemethod
        {
           get { return _webservicemethod; }
           set { _webservicemethod = value;}
        }
        public string webserviceparams
        {
           get { return _webserviceparams; }
           set { _webserviceparams = value;}
        }
        public string serverReturnData
        {
           get { return _serverReturnData; }
           set { _serverReturnData = value;}
        }
        public DateTime sCreateDatetime
        {
           get { return _sCreateDatetime; }
           set { _sCreateDatetime = value;}
        }
        public DateTime sRequestDatetime
        {
           get { return _sRequestDatetime; }
           set { _sRequestDatetime = value;}
        }
        public string webserviceparamsDecode
        {
           get { return _webserviceparamsDecode; }
           set { _webserviceparamsDecode = value;}
        }
        public int statuscode
        {
           get { return _statuscode; }
           set { _statuscode = value;}
        }
        public string ID
        {
           get { return _iD; }
           set { _iD = value;}
        }
        public string user_type
        {
           get { return _user_type; }
           set { _user_type = value;}
        }
        public string version
        {
           get { return _version; }
           set { _version = value;}
        }

        #endregion

        #region Ctor/init
        public WebserviceLog() {}
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
                cmd.CommandText = "INSERT INTO WebserviceLog (logID, deviceid, phonetype, webservicenode, webservicemethod, webserviceparams, serverReturnData, sCreateDatetime, sRequestDatetime, webserviceparamsDecode, statuscode, ID, user_type, version) VALUES (@logID_PARAMS, @deviceid_PARAMS, @phonetype_PARAMS, @webservicenode_PARAMS, @webservicemethod_PARAMS, @webserviceparams_PARAMS, @serverReturnData_PARAMS, @sCreateDatetime_PARAMS, @sRequestDatetime_PARAMS, @webserviceparamsDecode_PARAMS, @statuscode_PARAMS, @ID_PARAMS, @user_type_PARAMS, @version_PARAMS)";
                                cmd.Parameters.AddWithValue("@logID_PARAM", _logID);
                cmd.Parameters.AddWithValue("@deviceid_PARAM", _deviceid);
                cmd.Parameters.AddWithValue("@phonetype_PARAM", _phonetype);
                cmd.Parameters.AddWithValue("@webservicenode_PARAM", _webservicenode);
                cmd.Parameters.AddWithValue("@webservicemethod_PARAM", _webservicemethod);
                cmd.Parameters.AddWithValue("@webserviceparams_PARAM", _webserviceparams);
                cmd.Parameters.AddWithValue("@serverReturnData_PARAM", _serverReturnData);
                cmd.Parameters.AddWithValue("@sCreateDatetime_PARAM", _sCreateDatetime);
                cmd.Parameters.AddWithValue("@sRequestDatetime_PARAM", _sRequestDatetime);
                cmd.Parameters.AddWithValue("@webserviceparamsDecode_PARAM", _webserviceparamsDecode);
                cmd.Parameters.AddWithValue("@statuscode_PARAM", _statuscode);
                cmd.Parameters.AddWithValue("@ID_PARAM", _iD);
                cmd.Parameters.AddWithValue("@user_type_PARAM", _user_type);
                cmd.Parameters.AddWithValue("@version_PARAM", _version);

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
                cmd.CommandText = "SELECT logID, deviceid, phonetype, webservicenode, webservicemethod, webserviceparams, serverReturnData, sCreateDatetime, sRequestDatetime, webserviceparamsDecode, statuscode, ID, user_type, version FROM WebserviceLog WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _logID = reader.GetString(0);
                _deviceid = reader.GetString(1);
                _phonetype = reader.GetString(2);
                _webservicenode = reader.GetString(3);
                _webservicemethod = reader.GetString(4);
                _webserviceparams = reader.GetString(5);
                _serverReturnData = reader.GetString(6);
                _sCreateDatetime = reader.GetDateTime(7);
                _sRequestDatetime = reader.GetDateTime(8);
                _webserviceparamsDecode = reader.GetString(9);
                _statuscode = reader.GetInt32(10);
                _iD = reader.GetString(11);
                _user_type = reader.GetString(12);
                _version = reader.GetString(13);

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
                cmd.CommandText = "UPDATE WebserviceLog SET logID=@logID_PARAMS, deviceid=@deviceid_PARAMS, phonetype=@phonetype_PARAMS, webservicenode=@webservicenode_PARAMS, webservicemethod=@webservicemethod_PARAMS, webserviceparams=@webserviceparams_PARAMS, serverReturnData=@serverReturnData_PARAMS, sCreateDatetime=@sCreateDatetime_PARAMS, sRequestDatetime=@sRequestDatetime_PARAMS, webserviceparamsDecode=@webserviceparamsDecode_PARAMS, statuscode=@statuscode_PARAMS, ID=@ID_PARAMS, user_type=@user_type_PARAMS, version=@version_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@logID_PARAM", _logID);
                cmd.Parameters.AddWithValue("@deviceid_PARAM", _deviceid);
                cmd.Parameters.AddWithValue("@phonetype_PARAM", _phonetype);
                cmd.Parameters.AddWithValue("@webservicenode_PARAM", _webservicenode);
                cmd.Parameters.AddWithValue("@webservicemethod_PARAM", _webservicemethod);
                cmd.Parameters.AddWithValue("@webserviceparams_PARAM", _webserviceparams);
                cmd.Parameters.AddWithValue("@serverReturnData_PARAM", _serverReturnData);
                cmd.Parameters.AddWithValue("@sCreateDatetime_PARAM", _sCreateDatetime);
                cmd.Parameters.AddWithValue("@sRequestDatetime_PARAM", _sRequestDatetime);
                cmd.Parameters.AddWithValue("@webserviceparamsDecode_PARAM", _webserviceparamsDecode);
                cmd.Parameters.AddWithValue("@statuscode_PARAM", _statuscode);
                cmd.Parameters.AddWithValue("@ID_PARAM", _iD);
                cmd.Parameters.AddWithValue("@user_type_PARAM", _user_type);
                cmd.Parameters.AddWithValue("@version_PARAM", _version);

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
                cmd.CommandText = "DELETE FROM WebserviceLog WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

