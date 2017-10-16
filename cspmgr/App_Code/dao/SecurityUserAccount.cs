using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class SecurityUserAccount 
    {

        #region private data
        private string _groupID;
        private string _accountID;
        private string _name;
        private string _description;
        private byte[] _password;
        private bool _startup;
        private DateTime _pWLastUpdateTime;
        private short _pWType;
        private DateTime _createTime;
        private DateTime _dModifyTime;
        private short _iFailTimes;
        private DateTime _dLockTime;
        private short _cRoleID;
        private bool _aD_CheckFlag;
        private string _cCallID;
        private string _cPWD;

        #endregion

        #region Properties
        public string GroupID
        {
           get { return _groupID; }
           set { _groupID = value;}
        }
        public string AccountID
        {
           get { return _accountID; }
           set { _accountID = value;}
        }
        public string Name
        {
           get { return _name; }
           set { _name = value;}
        }
        public string Description
        {
           get { return _description; }
           set { _description = value;}
        }
        public byte[] Password
        {
           get { return _password; }
           set { _password = value;}
        }
        public bool Startup
        {
           get { return _startup; }
           set { _startup = value;}
        }
        public DateTime PWLastUpdateTime
        {
           get { return _pWLastUpdateTime; }
           set { _pWLastUpdateTime = value;}
        }
        public short PWType
        {
           get { return _pWType; }
           set { _pWType = value;}
        }
        public DateTime CreateTime
        {
           get { return _createTime; }
           set { _createTime = value;}
        }
        public DateTime dModifyTime
        {
           get { return _dModifyTime; }
           set { _dModifyTime = value;}
        }
        public short iFailTimes
        {
           get { return _iFailTimes; }
           set { _iFailTimes = value;}
        }
        public DateTime dLockTime
        {
           get { return _dLockTime; }
           set { _dLockTime = value;}
        }
        public short cRoleID
        {
           get { return _cRoleID; }
           set { _cRoleID = value;}
        }
        public bool AD_CheckFlag
        {
           get { return _aD_CheckFlag; }
           set { _aD_CheckFlag = value;}
        }
        public string cCallID
        {
           get { return _cCallID; }
           set { _cCallID = value;}
        }
        public string cPWD
        {
           get { return _cPWD; }
           set { _cPWD = value;}
        }

        #endregion

        #region Ctor/init
        public SecurityUserAccount() {}
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
                cmd.CommandText = "INSERT INTO SecurityUserAccount (GroupID, AccountID, Name, Description, Password, Startup, PWLastUpdateTime, PWType, CreateTime, dModifyTime, iFailTimes, dLockTime, cRoleID, AD_CheckFlag, cCallID, cPWD) VALUES (@GroupID_PARAMS, @AccountID_PARAMS, @Name_PARAMS, @Description_PARAMS, @Password_PARAMS, @Startup_PARAMS, @PWLastUpdateTime_PARAMS, @PWType_PARAMS, @CreateTime_PARAMS, @dModifyTime_PARAMS, @iFailTimes_PARAMS, @dLockTime_PARAMS, @cRoleID_PARAMS, @AD_CheckFlag_PARAMS, @cCallID_PARAMS, @cPWD_PARAMS)";
                                cmd.Parameters.AddWithValue("@GroupID_PARAM", _groupID);
                cmd.Parameters.AddWithValue("@AccountID_PARAM", _accountID);
                cmd.Parameters.AddWithValue("@Name_PARAM", _name);
                cmd.Parameters.AddWithValue("@Description_PARAM", _description);
                cmd.Parameters.AddWithValue("@Password_PARAM", _password);
                cmd.Parameters.AddWithValue("@Startup_PARAM", _startup);
                cmd.Parameters.AddWithValue("@PWLastUpdateTime_PARAM", _pWLastUpdateTime);
                cmd.Parameters.AddWithValue("@PWType_PARAM", _pWType);
                cmd.Parameters.AddWithValue("@CreateTime_PARAM", _createTime);
                cmd.Parameters.AddWithValue("@dModifyTime_PARAM", _dModifyTime);
                cmd.Parameters.AddWithValue("@iFailTimes_PARAM", _iFailTimes);
                cmd.Parameters.AddWithValue("@dLockTime_PARAM", _dLockTime);
                cmd.Parameters.AddWithValue("@cRoleID_PARAM", _cRoleID);
                cmd.Parameters.AddWithValue("@AD_CheckFlag_PARAM", _aD_CheckFlag);
                cmd.Parameters.AddWithValue("@cCallID_PARAM", _cCallID);
                cmd.Parameters.AddWithValue("@cPWD_PARAM", _cPWD);

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
                cmd.CommandText = "SELECT GroupID, AccountID, Name, Description, Password, Startup, PWLastUpdateTime, PWType, CreateTime, dModifyTime, iFailTimes, dLockTime, cRoleID, AD_CheckFlag, cCallID, cPWD FROM SecurityUserAccount WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {

                _groupID = reader.GetString(0);
                _accountID = reader.GetString(1);
                _name = reader.GetString(2);
                _description = reader.GetString(3);
                //_password = reader.GetString(4);
                _password = (byte[])reader[4];
                _startup = reader.GetBoolean(5);
                _pWLastUpdateTime = reader.GetDateTime(6);
                _pWType = reader.GetInt16(7);
                _createTime = reader.GetDateTime(8);
                _dModifyTime = reader.GetDateTime(9);
                _iFailTimes = reader.GetInt16(10);
                _dLockTime = reader.GetDateTime(11);
                _cRoleID = reader.GetInt16(12);
                _aD_CheckFlag = reader.GetBoolean(13);
                _cCallID = reader.GetString(14);
                _cPWD = reader.GetString(15);

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
                cmd.CommandText = "UPDATE SecurityUserAccount SET GroupID=@GroupID_PARAMS, AccountID=@AccountID_PARAMS, Name=@Name_PARAMS, Description=@Description_PARAMS, Password=@Password_PARAMS, Startup=@Startup_PARAMS, PWLastUpdateTime=@PWLastUpdateTime_PARAMS, PWType=@PWType_PARAMS, CreateTime=@CreateTime_PARAMS, dModifyTime=@dModifyTime_PARAMS, iFailTimes=@iFailTimes_PARAMS, dLockTime=@dLockTime_PARAMS, cRoleID=@cRoleID_PARAMS, AD_CheckFlag=@AD_CheckFlag_PARAMS, cCallID=@cCallID_PARAMS, cPWD=@cPWD_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@GroupID_PARAM", _groupID);
                cmd.Parameters.AddWithValue("@AccountID_PARAM", _accountID);
                cmd.Parameters.AddWithValue("@Name_PARAM", _name);
                cmd.Parameters.AddWithValue("@Description_PARAM", _description);
                cmd.Parameters.AddWithValue("@Password_PARAM", _password);
                cmd.Parameters.AddWithValue("@Startup_PARAM", _startup);
                cmd.Parameters.AddWithValue("@PWLastUpdateTime_PARAM", _pWLastUpdateTime);
                cmd.Parameters.AddWithValue("@PWType_PARAM", _pWType);
                cmd.Parameters.AddWithValue("@CreateTime_PARAM", _createTime);
                cmd.Parameters.AddWithValue("@dModifyTime_PARAM", _dModifyTime);
                cmd.Parameters.AddWithValue("@iFailTimes_PARAM", _iFailTimes);
                cmd.Parameters.AddWithValue("@dLockTime_PARAM", _dLockTime);
                cmd.Parameters.AddWithValue("@cRoleID_PARAM", _cRoleID);
                cmd.Parameters.AddWithValue("@AD_CheckFlag_PARAM", _aD_CheckFlag);
                cmd.Parameters.AddWithValue("@cCallID_PARAM", _cCallID);
                cmd.Parameters.AddWithValue("@cPWD_PARAM", _cPWD);

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
                cmd.CommandText = "DELETE FROM SecurityUserAccount WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

