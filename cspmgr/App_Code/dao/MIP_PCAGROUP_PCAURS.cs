using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_PCAGROUP_PCAURS 
    {

        #region private data
        private string _pCAURS_ID;
        private string _pCAGROUP_ID;
        private DateTime _lDATE;
        private string _lUSER;

        #endregion

        #region Properties
        public string PCAURS_ID
        {
           get { return _pCAURS_ID; }
           set { _pCAURS_ID = value;}
        }
        public string PCAGROUP_ID
        {
           get { return _pCAGROUP_ID; }
           set { _pCAGROUP_ID = value;}
        }
        public DateTime LDATE
        {
           get { return _lDATE; }
           set { _lDATE = value;}
        }
        public string LUSER
        {
           get { return _lUSER; }
           set { _lUSER = value;}
        }

        #endregion

        #region Ctor/init
        public MIP_PCAGROUP_PCAURS() {}
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
                cmd.CommandText = "INSERT INTO MIP_PCAGROUP_PCAURS (PCAURS_ID, PCAGROUP_ID, LDATE, LUSER) VALUES (@PCAURS_ID_PARAMS, @PCAGROUP_ID_PARAMS, @LDATE_PARAMS, @LUSER_PARAMS)";
                                cmd.Parameters.AddWithValue("@PCAURS_ID_PARAM", _pCAURS_ID);
                cmd.Parameters.AddWithValue("@PCAGROUP_ID_PARAM", _pCAGROUP_ID);
                cmd.Parameters.AddWithValue("@LDATE_PARAM", _lDATE);
                cmd.Parameters.AddWithValue("@LUSER_PARAM", _lUSER);

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
                cmd.CommandText = "SELECT PCAURS_ID, PCAGROUP_ID, LDATE, LUSER FROM MIP_PCAGROUP_PCAURS WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _pCAURS_ID = reader.GetString(0);
                _pCAGROUP_ID = reader.GetString(1);
                _lDATE = reader.GetDateTime(2);
                _lUSER = reader.GetString(3);

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
                cmd.CommandText = "UPDATE MIP_PCAGROUP_PCAURS SET PCAURS_ID=@PCAURS_ID_PARAMS, PCAGROUP_ID=@PCAGROUP_ID_PARAMS, LDATE=@LDATE_PARAMS, LUSER=@LUSER_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@PCAURS_ID_PARAM", _pCAURS_ID);
                cmd.Parameters.AddWithValue("@PCAGROUP_ID_PARAM", _pCAGROUP_ID);
                cmd.Parameters.AddWithValue("@LDATE_PARAM", _lDATE);
                cmd.Parameters.AddWithValue("@LUSER_PARAM", _lUSER);

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
                cmd.CommandText = "DELETE FROM MIP_PCAGROUP_PCAURS WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

