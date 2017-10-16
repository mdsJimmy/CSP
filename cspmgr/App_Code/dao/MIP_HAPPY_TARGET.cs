using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_HAPPY_TARGET 
    {

        #region private data
        private int _hAPPY_TARGET_ID;
        private int _hAPPY_ID;
        private string _pCAGROUP_ID;
        private string _dEPT_ID;
        private int _dTYPE;

        #endregion

        #region Properties
        public int HAPPY_TARGET_ID
        {
           get { return _hAPPY_TARGET_ID; }
           set { _hAPPY_TARGET_ID = value;}
        }
        public int HAPPY_ID
        {
           get { return _hAPPY_ID; }
           set { _hAPPY_ID = value;}
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
        public MIP_HAPPY_TARGET() {}
        #endregion

        #region Data Access Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public int Insert(System.Data.SqlClient.SqlCommand cmd)
        {
            cmd.CommandText = "INSERT INTO MIP_HAPPY_TARGET (HAPPY_TARGET_ID, HAPPY_ID, DEPT_ID, DTYPE) VALUES (@HAPPY_TARGET_ID_PARAMS, @HAPPY_ID_PARAMS, @DEPT_ID_PARAMS, @DTYPE_PARAMS)";
            cmd.Parameters.AddWithValue("@HAPPY_TARGET_ID_PARAMS", _hAPPY_TARGET_ID);
            cmd.Parameters.AddWithValue("@HAPPY_ID_PARAMS", _hAPPY_ID);
            cmd.Parameters.AddWithValue("@DEPT_ID_PARAMS", _dEPT_ID);
            cmd.Parameters.AddWithValue("@DTYPE_PARAMS", _dTYPE);

            return cmd.ExecuteNonQuery();

        }

        public int Insert_all(System.Data.SqlClient.SqlCommand cmd)
        {
            cmd.CommandText = "INSERT INTO MIP_HAPPY_TARGET (HAPPY_TARGET_ID, HAPPY_ID, DEPT_ID, DTYPE, PCAGROUP_ID) VALUES (@HAPPY_TARGET_ID_PARAMS, @HAPPY_ID_PARAMS, @DEPT_ID_PARAMS, @DTYPE_PARAMS, @PCAGROUP_ID_PARAMS)";
            cmd.Parameters.AddWithValue("@HAPPY_TARGET_ID_PARAMS", _hAPPY_TARGET_ID);
            cmd.Parameters.AddWithValue("@HAPPY_ID_PARAMS", _hAPPY_ID);
            cmd.Parameters.AddWithValue("@DEPT_ID_PARAMS", _dEPT_ID);
            cmd.Parameters.AddWithValue("@DTYPE_PARAMS", _dTYPE);
            cmd.Parameters.AddWithValue("@PCAGROUP_ID_PARAMS", _pCAGROUP_ID);

            return cmd.ExecuteNonQuery();

        }

        /// <summary>
        /// 
        /// </summary>
        public void Load(System.Data.SqlClient.SqlConnection connection)
        {
            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "SELECT HAPPY_TARGET_ID, HAPPY_ID, PCAGROUP_ID, DEPT_ID, DTYPE FROM MIP_HAPPY_TARGET WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _hAPPY_TARGET_ID = reader.GetInt32(0);
                _hAPPY_ID = reader.GetInt32(1);
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
                cmd.CommandText = "UPDATE MIP_HAPPY_TARGET SET HAPPY_TARGET_ID=@HAPPY_TARGET_ID_PARAMS, HAPPY_ID=@HAPPY_ID_PARAMS, PCAGROUP_ID=@PCAGROUP_ID_PARAMS, DEPT_ID=@DEPT_ID_PARAMS, DTYPE=@DTYPE_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@HAPPY_TARGET_ID_PARAM", _hAPPY_TARGET_ID);
                cmd.Parameters.AddWithValue("@HAPPY_ID_PARAM", _hAPPY_ID);
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
                cmd.CommandText = "DELETE FROM MIP_HAPPY_TARGET WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

