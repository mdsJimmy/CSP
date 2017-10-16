using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_MSG_TARGET 
    {

        #region private data
        private int _mSG_TARGET_ID;
        private int _mIP_MSG_NO;
        private string _pCAGROUP_ID;
        private string _dEPT_ID;
        private int _dTYPE;

        #endregion

        #region Properties
        public int MSG_TARGET_ID
        {
           get { return _mSG_TARGET_ID; }
           set { _mSG_TARGET_ID = value;}
        }
        public int MIP_MSG_NO
        {
           get { return _mIP_MSG_NO; }
           set { _mIP_MSG_NO = value;}
        }
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
        public int DTYPE
        {
           get { return _dTYPE; }
           set { _dTYPE = value;}
        }

        #endregion

        #region Ctor/init
        public MIP_MSG_TARGET() {}
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
                cmd.CommandText = "INSERT INTO MIP_MSG_TARGET (MSG_TARGET_ID, MIP_MSG_NO, PCAGROUP_ID, DEPT_ID, DTYPE) VALUES (@MSG_TARGET_ID_PARAMS, @MIP_MSG_NO_PARAMS, @PCAGROUP_ID_PARAMS, @DEPT_ID_PARAMS, @DTYPE_PARAMS)";
                                cmd.Parameters.AddWithValue("@MSG_TARGET_ID_PARAM", _mSG_TARGET_ID);
                cmd.Parameters.AddWithValue("@MIP_MSG_NO_PARAM", _mIP_MSG_NO);
                cmd.Parameters.AddWithValue("@PCAGROUP_ID_PARAM", _pCAGROUP_ID);
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
                cmd.CommandText = "SELECT MSG_TARGET_ID, MIP_MSG_NO, PCAGROUP_ID, DEPT_ID, DTYPE FROM MIP_MSG_TARGET WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _mSG_TARGET_ID = reader.GetInt32(0);
                _mIP_MSG_NO = reader.GetInt32(1);
                _pCAGROUP_ID = reader.GetString(2);
                _dEPT_ID = reader.GetString(3);
                _dTYPE = reader.GetInt32(4);

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
                cmd.CommandText = "UPDATE MIP_MSG_TARGET SET MSG_TARGET_ID=@MSG_TARGET_ID_PARAMS, MIP_MSG_NO=@MIP_MSG_NO_PARAMS, PCAGROUP_ID=@PCAGROUP_ID_PARAMS, DEPT_ID=@DEPT_ID_PARAMS, DTYPE=@DTYPE_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@MSG_TARGET_ID_PARAM", _mSG_TARGET_ID);
                cmd.Parameters.AddWithValue("@MIP_MSG_NO_PARAM", _mIP_MSG_NO);
                cmd.Parameters.AddWithValue("@PCAGROUP_ID_PARAM", _pCAGROUP_ID);
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
                cmd.CommandText = "DELETE FROM MIP_MSG_TARGET WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

