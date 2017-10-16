using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_PCAGROUP_DEPT 
    {

        #region private data
        private string _pCAGROUP_ID;
        private string _dEPT_ID;

        #endregion

        #region Properties
        public string PCAGROUP_ID
        {
           get { return _pCAGROUP_ID; }
           set { _pCAGROUP_ID = value;}
        }
        public string DEPT_ID
        {
           get { return _dEPT_ID; }
           set { _dEPT_ID = value;}
        }

        #endregion

        #region Ctor/init
        public MIP_PCAGROUP_DEPT() {}
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
                cmd.CommandText = "INSERT INTO MIP_PCAGROUP_DEPT (PCAGROUP_ID, DEPT_ID) VALUES (@PCAGROUP_ID_PARAMS, @DEPT_ID_PARAMS)";
                                cmd.Parameters.AddWithValue("@PCAGROUP_ID_PARAM", _pCAGROUP_ID);
                cmd.Parameters.AddWithValue("@DEPT_ID_PARAM", _dEPT_ID);

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
                cmd.CommandText = "SELECT PCAGROUP_ID, DEPT_ID FROM MIP_PCAGROUP_DEPT WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _pCAGROUP_ID = reader.GetString(0);
                _dEPT_ID = reader.GetString(1);

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
                cmd.CommandText = "UPDATE MIP_PCAGROUP_DEPT SET PCAGROUP_ID=@PCAGROUP_ID_PARAMS, DEPT_ID=@DEPT_ID_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@PCAGROUP_ID_PARAM", _pCAGROUP_ID);
                cmd.Parameters.AddWithValue("@DEPT_ID_PARAM", _dEPT_ID);

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
                cmd.CommandText = "DELETE FROM MIP_PCAGROUP_DEPT WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

