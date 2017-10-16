using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class SysAttribute 
    {

        #region private data
        private string _sECTION;
        private string _kEY;
        private string _dATA;
        private string _dataMemo;

        #endregion

        #region Properties
        public string SECTION
        {
           get { return _sECTION; }
           set { _sECTION = value;}
        }
        public string KEY
        {
           get { return _kEY; }
           set { _kEY = value;}
        }
        public string DATA
        {
           get { return _dATA; }
           set { _dATA = value;}
        }
        public string DataMemo
        {
           get { return _dataMemo; }
           set { _dataMemo = value;}
        }

        #endregion

        #region Ctor/init
        public SysAttribute() {}
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
                cmd.CommandText = "INSERT INTO SysAttribute (SECTION, KEY, DATA, DataMemo) VALUES (@SECTION_PARAMS, @KEY_PARAMS, @DATA_PARAMS, @DataMemo_PARAMS)";
                                cmd.Parameters.AddWithValue("@SECTION_PARAM", _sECTION);
                cmd.Parameters.AddWithValue("@KEY_PARAM", _kEY);
                cmd.Parameters.AddWithValue("@DATA_PARAM", _dATA);
                cmd.Parameters.AddWithValue("@DataMemo_PARAM", _dataMemo);

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
                cmd.CommandText = "SELECT SECTION, KEY, DATA, DataMemo FROM SysAttribute WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _sECTION = reader.GetString(0);
                _kEY = reader.GetString(1);
                _dATA = reader.GetString(2);
                _dataMemo = reader.GetString(3);

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
                cmd.CommandText = "UPDATE SysAttribute SET SECTION=@SECTION_PARAMS, KEY=@KEY_PARAMS, DATA=@DATA_PARAMS, DataMemo=@DataMemo_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@SECTION_PARAM", _sECTION);
                cmd.Parameters.AddWithValue("@KEY_PARAM", _kEY);
                cmd.Parameters.AddWithValue("@DATA_PARAM", _dATA);
                cmd.Parameters.AddWithValue("@DataMemo_PARAM", _dataMemo);

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
                cmd.CommandText = "DELETE FROM SysAttribute WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

