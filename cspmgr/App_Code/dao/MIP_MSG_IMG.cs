using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_MSG_IMG 
    {

        #region private data
        private string _lUSER;
        private int _fILE_INDEX;

        #endregion

        #region Properties
        public string LUSER
        {
           get { return _lUSER; }
           set { _lUSER = value;}
        }
        public int FILE_INDEX
        {
           get { return _fILE_INDEX; }
           set { _fILE_INDEX = value;}
        }

        #endregion

        #region Ctor/init
        public MIP_MSG_IMG() {}
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
                cmd.CommandText = "INSERT INTO MIP_MSG_IMG (LUSER, FILE_INDEX) VALUES (@LUSER_PARAMS, @FILE_INDEX_PARAMS)";
                                cmd.Parameters.AddWithValue("@LUSER_PARAM", _lUSER);
                cmd.Parameters.AddWithValue("@FILE_INDEX_PARAM", _fILE_INDEX);

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
                cmd.CommandText = "SELECT LUSER, FILE_INDEX FROM MIP_MSG_IMG WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _lUSER = reader.GetString(0);
                _fILE_INDEX = reader.GetInt32(1);

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
                cmd.CommandText = "UPDATE MIP_MSG_IMG SET LUSER=@LUSER_PARAMS, FILE_INDEX=@FILE_INDEX_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@LUSER_PARAM", _lUSER);
                cmd.Parameters.AddWithValue("@FILE_INDEX_PARAM", _fILE_INDEX);

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
                cmd.CommandText = "DELETE FROM MIP_MSG_IMG WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

