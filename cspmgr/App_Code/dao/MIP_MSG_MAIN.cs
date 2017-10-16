using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_MSG_MAIN 
    {

        #region private data
        private int _mIP_MSG_NO;
        private string _mSG_TARGET;
        private string _rESERVATION;
        private DateTime _oNLINE_TIME;
        private DateTime _oFFLINE_TIME;
        private string _dOPUSH;
        private string _mSG_KIND_1;
        private string _mSG_KIND_2;
        private string _mSG_TITLE;
        private  string _mSG_CONTENT;
        private string _mSG_STATE;
        private DateTime _lDATE;
        private string _lUSER;
        private int _sELECTALL;
        private int _pROCESSED;
        private int _iSNEW;

        #endregion

        #region Properties
        public int MIP_MSG_NO
        {
           get { return _mIP_MSG_NO; }
           set { _mIP_MSG_NO = value;}
        }
        public string MSG_TARGET
        {
           get { return _mSG_TARGET; }
           set { _mSG_TARGET = value;}
        }
        public string RESERVATION
        {
           get { return _rESERVATION; }
           set { _rESERVATION = value;}
        }
        public DateTime ONLINE_TIME
        {
           get { return _oNLINE_TIME; }
           set { _oNLINE_TIME = value;}
        }
        public DateTime OFFLINE_TIME
        {
           get { return _oFFLINE_TIME; }
           set { _oFFLINE_TIME = value;}
        }
        public string DOPUSH
        {
           get { return _dOPUSH; }
           set { _dOPUSH = value;}
        }
        public string MSG_KIND_1
        {
           get { return _mSG_KIND_1; }
           set { _mSG_KIND_1 = value;}
        }
        public string MSG_KIND_2
        {
           get { return _mSG_KIND_2; }
           set { _mSG_KIND_2 = value;}
        }
        public string MSG_TITLE
        {
           get { return _mSG_TITLE; }
           set { _mSG_TITLE = value;}
        }
        public string MSG_CONTENT
        {
           get { return _mSG_CONTENT; }
           set { _mSG_CONTENT = value;}
        }
        public string MSG_STATE
        {
           get { return _mSG_STATE; }
           set { _mSG_STATE = value;}
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
        public int SELECTALL
        {
           get { return _sELECTALL; }
           set { _sELECTALL = value;}
        }
        public int PROCESSED
        {
           get { return _pROCESSED; }
           set { _pROCESSED = value;}
        }
        public int ISNEW
        {
           get { return _iSNEW; }
           set { _iSNEW = value;}
        }

        #endregion

        #region Ctor/init
        public MIP_MSG_MAIN() {}
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
                cmd.CommandText = "INSERT INTO MIP_MSG_MAIN (MIP_MSG_NO, MSG_TARGET, RESERVATION, ONLINE_TIME, OFFLINE_TIME, DOPUSH, MSG_KIND_1, MSG_KIND_2, MSG_TITLE, MSG_CONTENT, MSG_STATE, LDATE, LUSER, SELECTALL, PROCESSED, ISNEW) VALUES (@MIP_MSG_NO_PARAMS, @MSG_TARGET_PARAMS, @RESERVATION_PARAMS, @ONLINE_TIME_PARAMS, @OFFLINE_TIME_PARAMS, @DOPUSH_PARAMS, @MSG_KIND_1_PARAMS, @MSG_KIND_2_PARAMS, @MSG_TITLE_PARAMS, @MSG_CONTENT_PARAMS, @MSG_STATE_PARAMS, @LDATE_PARAMS, @LUSER_PARAMS, @SELECTALL_PARAMS, @PROCESSED_PARAMS, @ISNEW_PARAMS)";
                                cmd.Parameters.AddWithValue("@MIP_MSG_NO_PARAM", _mIP_MSG_NO);
                cmd.Parameters.AddWithValue("@MSG_TARGET_PARAM", _mSG_TARGET);
                cmd.Parameters.AddWithValue("@RESERVATION_PARAM", _rESERVATION);
                cmd.Parameters.AddWithValue("@ONLINE_TIME_PARAM", _oNLINE_TIME);
                cmd.Parameters.AddWithValue("@OFFLINE_TIME_PARAM", _oFFLINE_TIME);
                cmd.Parameters.AddWithValue("@DOPUSH_PARAM", _dOPUSH);
                cmd.Parameters.AddWithValue("@MSG_KIND_1_PARAM", _mSG_KIND_1);
                cmd.Parameters.AddWithValue("@MSG_KIND_2_PARAM", _mSG_KIND_2);
                cmd.Parameters.AddWithValue("@MSG_TITLE_PARAM", _mSG_TITLE);
                cmd.Parameters.AddWithValue("@MSG_CONTENT_PARAM", _mSG_CONTENT);
                cmd.Parameters.AddWithValue("@MSG_STATE_PARAM", _mSG_STATE);
                cmd.Parameters.AddWithValue("@LDATE_PARAM", _lDATE);
                cmd.Parameters.AddWithValue("@LUSER_PARAM", _lUSER);
                cmd.Parameters.AddWithValue("@SELECTALL_PARAM", _sELECTALL);
                cmd.Parameters.AddWithValue("@PROCESSED_PARAM", _pROCESSED);
                cmd.Parameters.AddWithValue("@ISNEW_PARAM", _iSNEW);

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
                cmd.CommandText = "SELECT MIP_MSG_NO, MSG_TARGET, RESERVATION, ONLINE_TIME, OFFLINE_TIME, DOPUSH, MSG_KIND_1, MSG_KIND_2, MSG_TITLE, MSG_CONTENT, MSG_STATE, LDATE, LUSER, SELECTALL, PROCESSED, ISNEW FROM MIP_MSG_MAIN WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _mIP_MSG_NO = reader.GetInt32(0);
                _mSG_TARGET = reader.GetString(1);
                _rESERVATION = reader.GetString(2);
                _oNLINE_TIME = reader.GetDateTime(3);
                _oFFLINE_TIME = reader.GetDateTime(4);
                _dOPUSH = reader.GetString(5);
                _mSG_KIND_1 = reader.GetString(6);
                _mSG_KIND_2 = reader.GetString(7);
                _mSG_TITLE = reader.GetString(8);
                _mSG_CONTENT = reader.GetString(9);
                _mSG_STATE = reader.GetString(10);
                _lDATE = reader.GetDateTime(11);
                _lUSER = reader.GetString(12);
                _sELECTALL = reader.GetInt32(13);
                _pROCESSED = reader.GetInt32(14);
                _iSNEW = reader.GetInt32(15);

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
                cmd.CommandText = "UPDATE MIP_MSG_MAIN SET MIP_MSG_NO=@MIP_MSG_NO_PARAMS, MSG_TARGET=@MSG_TARGET_PARAMS, RESERVATION=@RESERVATION_PARAMS, ONLINE_TIME=@ONLINE_TIME_PARAMS, OFFLINE_TIME=@OFFLINE_TIME_PARAMS, DOPUSH=@DOPUSH_PARAMS, MSG_KIND_1=@MSG_KIND_1_PARAMS, MSG_KIND_2=@MSG_KIND_2_PARAMS, MSG_TITLE=@MSG_TITLE_PARAMS, MSG_CONTENT=@MSG_CONTENT_PARAMS, MSG_STATE=@MSG_STATE_PARAMS, LDATE=@LDATE_PARAMS, LUSER=@LUSER_PARAMS, SELECTALL=@SELECTALL_PARAMS, PROCESSED=@PROCESSED_PARAMS, ISNEW=@ISNEW_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@MIP_MSG_NO_PARAM", _mIP_MSG_NO);
                cmd.Parameters.AddWithValue("@MSG_TARGET_PARAM", _mSG_TARGET);
                cmd.Parameters.AddWithValue("@RESERVATION_PARAM", _rESERVATION);
                cmd.Parameters.AddWithValue("@ONLINE_TIME_PARAM", _oNLINE_TIME);
                cmd.Parameters.AddWithValue("@OFFLINE_TIME_PARAM", _oFFLINE_TIME);
                cmd.Parameters.AddWithValue("@DOPUSH_PARAM", _dOPUSH);
                cmd.Parameters.AddWithValue("@MSG_KIND_1_PARAM", _mSG_KIND_1);
                cmd.Parameters.AddWithValue("@MSG_KIND_2_PARAM", _mSG_KIND_2);
                cmd.Parameters.AddWithValue("@MSG_TITLE_PARAM", _mSG_TITLE);
                cmd.Parameters.AddWithValue("@MSG_CONTENT_PARAM", _mSG_CONTENT);
                cmd.Parameters.AddWithValue("@MSG_STATE_PARAM", _mSG_STATE);
                cmd.Parameters.AddWithValue("@LDATE_PARAM", _lDATE);
                cmd.Parameters.AddWithValue("@LUSER_PARAM", _lUSER);
                cmd.Parameters.AddWithValue("@SELECTALL_PARAM", _sELECTALL);
                cmd.Parameters.AddWithValue("@PROCESSED_PARAM", _pROCESSED);
                cmd.Parameters.AddWithValue("@ISNEW_PARAM", _iSNEW);

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
                cmd.CommandText = "DELETE FROM MIP_MSG_MAIN WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

