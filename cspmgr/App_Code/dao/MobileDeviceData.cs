using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MobileDeviceData 
    {

        #region private data
        private string _appname;
        private string _accountID;
        private string _deviceid;
        private string _pushid;
        private string _phonetype;
        private bool _status;
        private DateTime _createdatetime;
        private string _pHONE_OS;
        private string _pHONE_NAME;
        private string _pHONE_MODEL;
        private DateTime _updatedatetime;

        #endregion

        #region Properties
        public string appname
        {
           get { return _appname; }
           set { _appname = value;}
        }
        public string AccountID
        {
           get { return _accountID; }
           set { _accountID = value;}
        }
        public string deviceid
        {
           get { return _deviceid; }
           set { _deviceid = value;}
        }
        public string pushid
        {
           get { return _pushid; }
           set { _pushid = value;}
        }
        public string phonetype
        {
           get { return _phonetype; }
           set { _phonetype = value;}
        }
        public bool status
        {
           get { return _status; }
           set { _status = value;}
        }
        public DateTime createdatetime
        {
           get { return _createdatetime; }
           set { _createdatetime = value;}
        }
        public string PHONE_OS
        {
           get { return _pHONE_OS; }
           set { _pHONE_OS = value;}
        }
        public string PHONE_NAME
        {
           get { return _pHONE_NAME; }
           set { _pHONE_NAME = value;}
        }
        public string PHONE_MODEL
        {
           get { return _pHONE_MODEL; }
           set { _pHONE_MODEL = value;}
        }
        public DateTime updatedatetime
        {
           get { return _updatedatetime; }
           set { _updatedatetime = value;}
        }

        #endregion

        #region Ctor/init
        public MobileDeviceData() {}
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
                cmd.CommandText = "INSERT INTO MobileDeviceData (appname, AccountID, deviceid, pushid, phonetype, status, createdatetime, PHONE_OS, PHONE_NAME, PHONE_MODEL, updatedatetime) VALUES (@appname_PARAMS, @AccountID_PARAMS, @deviceid_PARAMS, @pushid_PARAMS, @phonetype_PARAMS, @status_PARAMS, @createdatetime_PARAMS, @PHONE_OS_PARAMS, @PHONE_NAME_PARAMS, @PHONE_MODEL_PARAMS, @updatedatetime_PARAMS)";
                                cmd.Parameters.AddWithValue("@appname_PARAM", _appname);
                cmd.Parameters.AddWithValue("@AccountID_PARAM", _accountID);
                cmd.Parameters.AddWithValue("@deviceid_PARAM", _deviceid);
                cmd.Parameters.AddWithValue("@pushid_PARAM", _pushid);
                cmd.Parameters.AddWithValue("@phonetype_PARAM", _phonetype);
                cmd.Parameters.AddWithValue("@status_PARAM", _status);
                cmd.Parameters.AddWithValue("@createdatetime_PARAM", _createdatetime);
                cmd.Parameters.AddWithValue("@PHONE_OS_PARAM", _pHONE_OS);
                cmd.Parameters.AddWithValue("@PHONE_NAME_PARAM", _pHONE_NAME);
                cmd.Parameters.AddWithValue("@PHONE_MODEL_PARAM", _pHONE_MODEL);
                cmd.Parameters.AddWithValue("@updatedatetime_PARAM", _updatedatetime);

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
                cmd.CommandText = "SELECT appname, AccountID, deviceid, pushid, phonetype, status, createdatetime, PHONE_OS, PHONE_NAME, PHONE_MODEL, updatedatetime FROM MobileDeviceData WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _appname = reader.GetString(0);
                _accountID = reader.GetString(1);
                _deviceid = reader.GetString(2);
                _pushid = reader.GetString(3);
                _phonetype = reader.GetString(4);
                _status = reader.GetBoolean(5);
                _createdatetime = reader.GetDateTime(6);
                _pHONE_OS = reader.GetString(7);
                _pHONE_NAME = reader.GetString(8);
                _pHONE_MODEL = reader.GetString(9);
                _updatedatetime = reader.GetDateTime(10);

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
                cmd.CommandText = "UPDATE MobileDeviceData SET appname=@appname_PARAMS, AccountID=@AccountID_PARAMS, deviceid=@deviceid_PARAMS, pushid=@pushid_PARAMS, phonetype=@phonetype_PARAMS, status=@status_PARAMS, createdatetime=@createdatetime_PARAMS, PHONE_OS=@PHONE_OS_PARAMS, PHONE_NAME=@PHONE_NAME_PARAMS, PHONE_MODEL=@PHONE_MODEL_PARAMS, updatedatetime=@updatedatetime_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@appname_PARAM", _appname);
                cmd.Parameters.AddWithValue("@AccountID_PARAM", _accountID);
                cmd.Parameters.AddWithValue("@deviceid_PARAM", _deviceid);
                cmd.Parameters.AddWithValue("@pushid_PARAM", _pushid);
                cmd.Parameters.AddWithValue("@phonetype_PARAM", _phonetype);
                cmd.Parameters.AddWithValue("@status_PARAM", _status);
                cmd.Parameters.AddWithValue("@createdatetime_PARAM", _createdatetime);
                cmd.Parameters.AddWithValue("@PHONE_OS_PARAM", _pHONE_OS);
                cmd.Parameters.AddWithValue("@PHONE_NAME_PARAM", _pHONE_NAME);
                cmd.Parameters.AddWithValue("@PHONE_MODEL_PARAM", _pHONE_MODEL);
                cmd.Parameters.AddWithValue("@updatedatetime_PARAM", _updatedatetime);

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
                cmd.CommandText = "DELETE FROM MobileDeviceData WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

