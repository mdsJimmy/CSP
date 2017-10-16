using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class DMSRole 
    {

        #region private data
        private short _dMSRoleID;
        private string _dMSRoleName;

        #endregion

        #region Properties
        public short DMSRoleID
        {
           get { return _dMSRoleID; }
           set { _dMSRoleID = value;}
        }
        public string DMSRoleName
        {
           get { return _dMSRoleName; }
           set { _dMSRoleName = value;}
        }

        #endregion

        #region Ctor/init
        public DMSRole() {}
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
                cmd.CommandText = "INSERT INTO DMSRole (DMSRoleID, DMSRoleName) VALUES (@DMSRoleID_PARAMS, @DMSRoleName_PARAMS)";
                                cmd.Parameters.AddWithValue("@DMSRoleID_PARAM", _dMSRoleID);
                cmd.Parameters.AddWithValue("@DMSRoleName_PARAM", _dMSRoleName);

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
                cmd.CommandText = "SELECT DMSRoleID, DMSRoleName FROM DMSRole WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _dMSRoleID = reader.GetInt16(0);
                _dMSRoleName = reader.GetString(1);

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
                cmd.CommandText = "UPDATE DMSRole SET DMSRoleID=@DMSRoleID_PARAMS, DMSRoleName=@DMSRoleName_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@DMSRoleID_PARAM", _dMSRoleID);
                cmd.Parameters.AddWithValue("@DMSRoleName_PARAM", _dMSRoleName);

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
                cmd.CommandText = "DELETE FROM DMSRole WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

