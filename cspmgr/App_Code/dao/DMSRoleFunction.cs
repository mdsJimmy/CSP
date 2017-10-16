using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class DMSRoleFunction 
    {

        #region private data
        private string _dMSRoleID;
        private string _sysModID;
        private string _sysFuncID;

        #endregion

        #region Properties
        public string DMSRoleID
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

        #endregion

        #region Ctor/init
        public DMSRoleFunction() {}
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
                cmd.CommandText = "INSERT INTO DMSRoleFunction (DMSRoleID, SysModID, SysFuncID) VALUES (@DMSRoleID_PARAMS, @SysModID_PARAMS, @SysFuncID_PARAMS)";
                                cmd.Parameters.AddWithValue("@DMSRoleID_PARAM", _dMSRoleID);
                cmd.Parameters.AddWithValue("@SysModID_PARAM", _sysModID);
                cmd.Parameters.AddWithValue("@SysFuncID_PARAM", _sysFuncID);

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
                cmd.CommandText = "SELECT DMSRoleID, SysModID, SysFuncID FROM DMSRoleFunction WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _dMSRoleID = reader.GetString(0);
                _sysModID = reader.GetString(1);
                _sysFuncID = reader.GetString(2);

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
                cmd.CommandText = "UPDATE DMSRoleFunction SET DMSRoleID=@DMSRoleID_PARAMS, SysModID=@SysModID_PARAMS, SysFuncID=@SysFuncID_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@DMSRoleID_PARAM", _dMSRoleID);
                cmd.Parameters.AddWithValue("@SysModID_PARAM", _sysModID);
                cmd.Parameters.AddWithValue("@SysFuncID_PARAM", _sysFuncID);

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
                cmd.CommandText = "DELETE FROM DMSRoleFunction WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

