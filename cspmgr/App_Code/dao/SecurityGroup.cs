using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class SecurityGroup 
    {

        #region private data
        private string _groupID;
        private string _groupName;
        private string _address;
        private string _tEL;
        private string _memo;

        #endregion

        #region Properties
        public string GroupID
        {
           get { return _groupID; }
           set { _groupID = value;}
        }
        public string GroupName
        {
           get { return _groupName; }
           set { _groupName = value;}
        }
        public string Address
        {
           get { return _address; }
           set { _address = value;}
        }
        public string TEL
        {
           get { return _tEL; }
           set { _tEL = value;}
        }
        public string Memo
        {
           get { return _memo; }
           set { _memo = value;}
        }

        #endregion

        #region Ctor/init
        public SecurityGroup() {}
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
                cmd.CommandText = "INSERT INTO SecurityGroup (GroupID, GroupName, Address, TEL, Memo) VALUES (@GroupID_PARAMS, @GroupName_PARAMS, @Address_PARAMS, @TEL_PARAMS, @Memo_PARAMS)";
                                cmd.Parameters.AddWithValue("@GroupID_PARAM", _groupID);
                cmd.Parameters.AddWithValue("@GroupName_PARAM", _groupName);
                cmd.Parameters.AddWithValue("@Address_PARAM", _address);
                cmd.Parameters.AddWithValue("@TEL_PARAM", _tEL);
                cmd.Parameters.AddWithValue("@Memo_PARAM", _memo);

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
                cmd.CommandText = "SELECT GroupID, GroupName, Address, TEL, Memo FROM SecurityGroup WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _groupID = reader.GetString(0);
                _groupName = reader.GetString(1);
                _address = reader.GetString(2);
                _tEL = reader.GetString(3);
                _memo = reader.GetString(4);

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
                cmd.CommandText = "UPDATE SecurityGroup SET GroupID=@GroupID_PARAMS, GroupName=@GroupName_PARAMS, Address=@Address_PARAMS, TEL=@TEL_PARAMS, Memo=@Memo_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@GroupID_PARAM", _groupID);
                cmd.Parameters.AddWithValue("@GroupName_PARAM", _groupName);
                cmd.Parameters.AddWithValue("@Address_PARAM", _address);
                cmd.Parameters.AddWithValue("@TEL_PARAM", _tEL);
                cmd.Parameters.AddWithValue("@Memo_PARAM", _memo);

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
                cmd.CommandText = "DELETE FROM SecurityGroup WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

