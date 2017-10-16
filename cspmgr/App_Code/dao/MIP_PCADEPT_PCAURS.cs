using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_PCADEPT_PCAURS 
    {

        #region private data
        private string _pCAURS_ID;
        private string _dEPT_ID;
        private int _dTYPE;

        #endregion

        #region Properties
        public string PCAURS_ID
        {
           get { return _pCAURS_ID; }
           set { _pCAURS_ID = value;}
        }
        public string DEPT_ID
        {
           get { return _dEPT_ID; }
           set { _dEPT_ID = value;}
        }
        public int DTYPE
        {
           get { return _dTYPE; }
           set { _dTYPE = value;}
        }

        #endregion

        #region Ctor/init
        public MIP_PCADEPT_PCAURS() {}
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
                cmd.CommandText = "INSERT INTO MIP_PCADEPT_PCAURS (PCAURS_ID, DEPT_ID, DTYPE) VALUES (@PCAURS_ID_PARAMS, @DEPT_ID_PARAMS, @DTYPE_PARAMS)";
                                cmd.Parameters.AddWithValue("@PCAURS_ID_PARAM", _pCAURS_ID);
                cmd.Parameters.AddWithValue("@DEPT_ID_PARAM", _dEPT_ID);
                cmd.Parameters.AddWithValue("@DTYPE_PARAM", _dTYPE);

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
                cmd.CommandText = "SELECT PCAURS_ID, DEPT_ID, DTYPE FROM MIP_PCADEPT_PCAURS WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _pCAURS_ID = reader.GetString(0);
                _dEPT_ID = reader.GetString(1);
                _dTYPE = reader.GetInt32(2);

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
                cmd.CommandText = "UPDATE MIP_PCADEPT_PCAURS SET PCAURS_ID=@PCAURS_ID_PARAMS, DEPT_ID=@DEPT_ID_PARAMS, DTYPE=@DTYPE_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@PCAURS_ID_PARAM", _pCAURS_ID);
                cmd.Parameters.AddWithValue("@DEPT_ID_PARAM", _dEPT_ID);
                cmd.Parameters.AddWithValue("@DTYPE_PARAM", _dTYPE);

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
                cmd.CommandText = "DELETE FROM MIP_PCADEPT_PCAURS WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

