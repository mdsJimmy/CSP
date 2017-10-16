using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class SecurityUserAccount_SecureKey 
    {

        #region private data
        private string _secureKey;
        private string _accounID;
        private int _loginType;
        private string _iPAddress;
        private DateTime _createDatetime;
        private DateTime _usedDatetime;
        private int _isUsed;

        #endregion

        #region Properties
        public string SecureKey
        {
           get { return _secureKey; }
           set { _secureKey = value;}
        }
        public string AccounID
        {
           get { return _accounID; }
           set { _accounID = value;}
        }
        public int LoginType
        {
           get { return _loginType; }
           set { _loginType = value;}
        }
        public string IPAddress
        {
           get { return _iPAddress; }
           set { _iPAddress = value;}
        }
        public DateTime CreateDatetime
        {
           get { return _createDatetime; }
           set { _createDatetime = value;}
        }
        public DateTime UsedDatetime
        {
           get { return _usedDatetime; }
           set { _usedDatetime = value;}
        }
        public int IsUsed
        {
           get { return _isUsed; }
           set { _isUsed = value;}
        }

        #endregion

        #region Ctor/init
        public SecurityUserAccount_SecureKey() {}
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
                cmd.CommandText = "INSERT INTO SecurityUserAccount_SecureKey (SecureKey, AccounID, LoginType, IPAddress, CreateDatetime, UsedDatetime, IsUsed) VALUES (@SecureKey_PARAMS, @AccounID_PARAMS, @LoginType_PARAMS, @IPAddress_PARAMS, @CreateDatetime_PARAMS, @UsedDatetime_PARAMS, @IsUsed_PARAMS)";
                                cmd.Parameters.AddWithValue("@SecureKey_PARAM", _secureKey);
                cmd.Parameters.AddWithValue("@AccounID_PARAM", _accounID);
                cmd.Parameters.AddWithValue("@LoginType_PARAM", _loginType);
                cmd.Parameters.AddWithValue("@IPAddress_PARAM", _iPAddress);
                cmd.Parameters.AddWithValue("@CreateDatetime_PARAM", _createDatetime);
                cmd.Parameters.AddWithValue("@UsedDatetime_PARAM", _usedDatetime);
                cmd.Parameters.AddWithValue("@IsUsed_PARAM", _isUsed);

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
                cmd.CommandText = "SELECT SecureKey, AccounID, LoginType, IPAddress, CreateDatetime, UsedDatetime, IsUsed FROM SecurityUserAccount_SecureKey WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _secureKey = reader.GetString(0);
                _accounID = reader.GetString(1);
                _loginType = reader.GetInt32(2);
                _iPAddress = reader.GetString(3);
                _createDatetime = reader.GetDateTime(4);
                _usedDatetime = reader.GetDateTime(5);
                _isUsed = reader.GetInt32(6);

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
                cmd.CommandText = "UPDATE SecurityUserAccount_SecureKey SET SecureKey=@SecureKey_PARAMS, AccounID=@AccounID_PARAMS, LoginType=@LoginType_PARAMS, IPAddress=@IPAddress_PARAMS, CreateDatetime=@CreateDatetime_PARAMS, UsedDatetime=@UsedDatetime_PARAMS, IsUsed=@IsUsed_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@SecureKey_PARAM", _secureKey);
                cmd.Parameters.AddWithValue("@AccounID_PARAM", _accounID);
                cmd.Parameters.AddWithValue("@LoginType_PARAM", _loginType);
                cmd.Parameters.AddWithValue("@IPAddress_PARAM", _iPAddress);
                cmd.Parameters.AddWithValue("@CreateDatetime_PARAM", _createDatetime);
                cmd.Parameters.AddWithValue("@UsedDatetime_PARAM", _usedDatetime);
                cmd.Parameters.AddWithValue("@IsUsed_PARAM", _isUsed);

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
                cmd.CommandText = "DELETE FROM SecurityUserAccount_SecureKey WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

