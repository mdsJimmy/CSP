using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class ByPassLoginList 
    {

        #region private data
        private string _byPassLoginID;
        private string _byPassLoginIP;

        #endregion

        #region Properties
        public string ByPassLoginID
        {
           get { return _byPassLoginID; }
           set { _byPassLoginID = value;}
        }
        public string ByPassLoginIP
        {
           get { return _byPassLoginIP; }
           set { _byPassLoginIP = value;}
        }

        #endregion

        #region Ctor/init
        public ByPassLoginList() {}
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
                cmd.CommandText = "INSERT INTO ByPassLoginList (ByPassLoginID, ByPassLoginIP) VALUES (@ByPassLoginID_PARAMS, @ByPassLoginIP_PARAMS)";
                                cmd.Parameters.AddWithValue("@ByPassLoginID_PARAM", _byPassLoginID);
                cmd.Parameters.AddWithValue("@ByPassLoginIP_PARAM", _byPassLoginIP);

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
                cmd.CommandText = "SELECT ByPassLoginID, ByPassLoginIP FROM ByPassLoginList WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _byPassLoginID = reader.GetString(0);
                _byPassLoginIP = reader.GetString(1);

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
                cmd.CommandText = "UPDATE ByPassLoginList SET ByPassLoginID=@ByPassLoginID_PARAMS, ByPassLoginIP=@ByPassLoginIP_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@ByPassLoginID_PARAM", _byPassLoginID);
                cmd.Parameters.AddWithValue("@ByPassLoginIP_PARAM", _byPassLoginIP);

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
                cmd.CommandText = "DELETE FROM ByPassLoginList WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

