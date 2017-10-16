using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class SecurityGroup_ContactRelation 
    {

        #region private data
        private string _groupID;
        private int _contactID;

        #endregion

        #region Properties
        public string GroupID
        {
           get { return _groupID; }
           set { _groupID = value;}
        }
        public int ContactID
        {
           get { return _contactID; }
           set { _contactID = value;}
        }

        #endregion

        #region Ctor/init
        public SecurityGroup_ContactRelation() {}
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
                cmd.CommandText = "INSERT INTO SecurityGroup_ContactRelation (GroupID, ContactID) VALUES (@GroupID_PARAMS, @ContactID_PARAMS)";
                                cmd.Parameters.AddWithValue("@GroupID_PARAM", _groupID);
                cmd.Parameters.AddWithValue("@ContactID_PARAM", _contactID);

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
                cmd.CommandText = "SELECT GroupID, ContactID FROM SecurityGroup_ContactRelation WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _groupID = reader.GetString(0);
                _contactID = reader.GetInt32(1);

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
                cmd.CommandText = "UPDATE SecurityGroup_ContactRelation SET GroupID=@GroupID_PARAMS, ContactID=@ContactID_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@GroupID_PARAM", _groupID);
                cmd.Parameters.AddWithValue("@ContactID_PARAM", _contactID);

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
                cmd.CommandText = "DELETE FROM SecurityGroup_ContactRelation WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

