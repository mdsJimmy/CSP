using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class SecurityUserAccount_ActionRole 
    {

        #region private data
        private string _accountID;
        private string _sysModID;
        private string _sysFuncID;
        private string _sysActionID;

        #endregion

        #region Properties
        public string AccountID
        {
           get { return _accountID; }
           set { _accountID = value;}
        }
        public string SysModID
        {
           get { return _sysModID; }
           set { _sysModID = value;}
        }
        public string SysFuncID
        {
           get { return _sysFuncID; }
           set { _sysFuncID = value;}
        }
        public string SysActionID
        {
           get { return _sysActionID; }
           set { _sysActionID = value;}
        }

        #endregion

        #region Ctor/init
        public SecurityUserAccount_ActionRole() {}
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
                cmd.CommandText = "INSERT INTO SecurityUserAccount_ActionRole (AccountID, SysModID, SysFuncID, SysActionID) VALUES (@AccountID_PARAMS, @SysModID_PARAMS, @SysFuncID_PARAMS, @SysActionID_PARAMS)";
                                cmd.Parameters.AddWithValue("@AccountID_PARAM", _accountID);
                cmd.Parameters.AddWithValue("@SysModID_PARAM", _sysModID);
                cmd.Parameters.AddWithValue("@SysFuncID_PARAM", _sysFuncID);
                cmd.Parameters.AddWithValue("@SysActionID_PARAM", _sysActionID);

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
                cmd.CommandText = "SELECT AccountID, SysModID, SysFuncID, SysActionID FROM SecurityUserAccount_ActionRole WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _accountID = reader.GetString(0);
                _sysModID = reader.GetString(1);
                _sysFuncID = reader.GetString(2);
                _sysActionID = reader.GetString(3);

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
                cmd.CommandText = "UPDATE SecurityUserAccount_ActionRole SET AccountID=@AccountID_PARAMS, SysModID=@SysModID_PARAMS, SysFuncID=@SysFuncID_PARAMS, SysActionID=@SysActionID_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@AccountID_PARAM", _accountID);
                cmd.Parameters.AddWithValue("@SysModID_PARAM", _sysModID);
                cmd.Parameters.AddWithValue("@SysFuncID_PARAM", _sysFuncID);
                cmd.Parameters.AddWithValue("@SysActionID_PARAM", _sysActionID);

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
                cmd.CommandText = "DELETE FROM SecurityUserAccount_ActionRole WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

