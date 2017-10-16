using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_PCAURS 
    {

        #region private data
        private string _pCAURS_ID;
        private int _cSTATUS;
        private string _r001;
        private string _r002;
        private DateTime _lDATE;
        private string _lUSER;
        private int _iSTESTER;

        #endregion

        #region Properties
        public string PCAURS_ID
        {
           get { return _pCAURS_ID; }
           set { _pCAURS_ID = value;}
        }
        public int CSTATUS
        {
           get { return _cSTATUS; }
           set { _cSTATUS = value;}
        }
        public string R001
        {
           get { return _r001; }
           set { _r001 = value;}
        }
        public string R002
        {
           get { return _r002; }
           set { _r002 = value;}
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
        public int ISTESTER
        {
           get { return _iSTESTER; }
           set { _iSTESTER = value;}
        }

        #endregion

        #region Ctor/init
        public MIP_PCAURS() {}
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
                cmd.CommandText = "INSERT INTO MIP_PCAURS (PCAURS_ID, CSTATUS, R001, R002, LDATE, LUSER, ISTESTER) VALUES (@PCAURS_ID_PARAMS, @CSTATUS_PARAMS, @R001_PARAMS, @R002_PARAMS, @LDATE_PARAMS, @LUSER_PARAMS, @ISTESTER_PARAMS)";
                                cmd.Parameters.AddWithValue("@PCAURS_ID_PARAM", _pCAURS_ID);
                cmd.Parameters.AddWithValue("@CSTATUS_PARAM", _cSTATUS);
                cmd.Parameters.AddWithValue("@R001_PARAM", _r001);
                cmd.Parameters.AddWithValue("@R002_PARAM", _r002);
                cmd.Parameters.AddWithValue("@LDATE_PARAM", _lDATE);
                cmd.Parameters.AddWithValue("@LUSER_PARAM", _lUSER);
                cmd.Parameters.AddWithValue("@ISTESTER_PARAM", _iSTESTER);

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
                cmd.CommandText = "SELECT PCAURS_ID, CSTATUS, R001, R002, LDATE, LUSER, ISTESTER FROM MIP_PCAURS WHERE PCAURS_ID=@PCAURS_ID_PARAM";
                                cmd.Parameters.AddWithValue("@PCAURS_ID_PARAM", _pCAURS_ID);

                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _pCAURS_ID = reader.GetString(0);
                _cSTATUS = reader.GetInt32(1);
                _r001 = reader.GetString(2);
                _r002 = reader.GetString(3);
                _lDATE = reader.GetDateTime(4);
                _lUSER = reader.GetString(5);
                _iSTESTER = reader.GetInt32(6);

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
                cmd.CommandText = "UPDATE MIP_PCAURS SET PCAURS_ID=@PCAURS_ID_PARAMS, CSTATUS=@CSTATUS_PARAMS, R001=@R001_PARAMS, R002=@R002_PARAMS, LDATE=@LDATE_PARAMS, LUSER=@LUSER_PARAMS, ISTESTER=@ISTESTER_PARAMS WHERE PCAURS_ID=@PCAURS_ID_PARAM";
                                cmd.Parameters.AddWithValue("@PCAURS_ID_PARAM", _pCAURS_ID);
                cmd.Parameters.AddWithValue("@CSTATUS_PARAM", _cSTATUS);
                cmd.Parameters.AddWithValue("@R001_PARAM", _r001);
                cmd.Parameters.AddWithValue("@R002_PARAM", _r002);
                cmd.Parameters.AddWithValue("@LDATE_PARAM", _lDATE);
                cmd.Parameters.AddWithValue("@LUSER_PARAM", _lUSER);
                cmd.Parameters.AddWithValue("@ISTESTER_PARAM", _iSTESTER);

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
                cmd.CommandText = "DELETE FROM MIP_PCAURS WHERE PCAURS_ID=@PCAURS_ID_PARAM";
                cmd.Parameters.AddWithValue("@PCAURS_ID_PARAM", _pCAURS_ID);

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

