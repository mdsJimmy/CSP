using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class DMSRoleAction 
    {

        #region private data
        private short _dMSRoleID;
        private string _sysModID;
        private string _sysFuncID;
        private string _sysActionID;

        #endregion

        #region Properties
        public short DMSRoleID
        {
           get { return _dMSRoleID; }
           set { _dMSRoleID = value;}
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
        public DMSRoleAction() {}
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
                cmd.CommandText = "INSERT INTO DMSRoleAction (DMSRoleID, SysModID, SysFuncID, SysActionID) VALUES (@DMSRoleID_PARAMS, @SysModID_PARAMS, @SysFuncID_PARAMS, @SysActionID_PARAMS)";
                                cmd.Parameters.AddWithValue("@DMSRoleID_PARAM", _dMSRoleID);
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
                cmd.CommandText = "SELECT DMSRoleID, SysModID, SysFuncID, SysActionID FROM DMSRoleAction WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _dMSRoleID = reader.GetInt16(0);
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
                cmd.CommandText = "UPDATE DMSRoleAction SET DMSRoleID=@DMSRoleID_PARAMS, SysModID=@SysModID_PARAMS, SysFuncID=@SysFuncID_PARAMS, SysActionID=@SysActionID_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@DMSRoleID_PARAM", _dMSRoleID);
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
                cmd.CommandText = "DELETE FROM DMSRoleAction WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

