using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class SecurityRelation 
    {

        #region private data
        private string _parentGroupID;
        private string _accountType;
        private string _accountID;

        #endregion

        #region Properties
        public string ParentGroupID
        {
           get { return _parentGroupID; }
           set { _parentGroupID = value;}
        }
        public string AccountType
        {
           get { return _accountType; }
           set { _accountType = value;}
        }
        public string AccountID
        {
           get { return _accountID; }
           set { _accountID = value;}
        }

        #endregion

        #region Ctor/init
        public SecurityRelation() {}
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
                cmd.CommandText = "INSERT INTO SecurityRelation (ParentGroupID, AccountType, AccountID) VALUES (@ParentGroupID_PARAMS, @AccountType_PARAMS, @AccountID_PARAMS)";
                                cmd.Parameters.AddWithValue("@ParentGroupID_PARAM", _parentGroupID);
                cmd.Parameters.AddWithValue("@AccountType_PARAM", _accountType);
                cmd.Parameters.AddWithValue("@AccountID_PARAM", _accountID);

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
                cmd.CommandText = "SELECT ParentGroupID, AccountType, AccountID FROM SecurityRelation WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _parentGroupID = reader.GetString(0);
                _accountType = reader.GetString(1);
                _accountID = reader.GetString(2);

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
                cmd.CommandText = "UPDATE SecurityRelation SET ParentGroupID=@ParentGroupID_PARAMS, AccountType=@AccountType_PARAMS, AccountID=@AccountID_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@ParentGroupID_PARAM", _parentGroupID);
                cmd.Parameters.AddWithValue("@AccountType_PARAM", _accountType);
                cmd.Parameters.AddWithValue("@AccountID_PARAM", _accountID);

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
                cmd.CommandText = "DELETE FROM SecurityRelation WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

