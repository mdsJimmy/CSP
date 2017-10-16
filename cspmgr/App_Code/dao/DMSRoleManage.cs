using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class DMSRoleManage 
    {

        #region private data
        private short _dMSRoleID;
        private short _dMSRoleIDManaged;

        #endregion

        #region Properties
        public short DMSRoleID
        {
           get { return _dMSRoleID; }
           set { _dMSRoleID = value;}
        }
        public short DMSRoleIDManaged
        {
           get { return _dMSRoleIDManaged; }
           set { _dMSRoleIDManaged = value;}
        }

        #endregion

        #region Ctor/init
        public DMSRoleManage() {}
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
                cmd.CommandText = "INSERT INTO DMSRoleManage (DMSRoleID, DMSRoleIDManaged) VALUES (@DMSRoleID_PARAMS, @DMSRoleIDManaged_PARAMS)";
                                cmd.Parameters.AddWithValue("@DMSRoleID_PARAM", _dMSRoleID);
                cmd.Parameters.AddWithValue("@DMSRoleIDManaged_PARAM", _dMSRoleIDManaged);

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
                cmd.CommandText = "SELECT DMSRoleID, DMSRoleIDManaged FROM DMSRoleManage WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _dMSRoleID = reader.GetInt16(0);
                _dMSRoleIDManaged = reader.GetInt16(1);

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
                cmd.CommandText = "UPDATE DMSRoleManage SET DMSRoleID=@DMSRoleID_PARAMS, DMSRoleIDManaged=@DMSRoleIDManaged_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@DMSRoleID_PARAM", _dMSRoleID);
                cmd.Parameters.AddWithValue("@DMSRoleIDManaged_PARAM", _dMSRoleIDManaged);

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
                cmd.CommandText = "DELETE FROM DMSRoleManage WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

