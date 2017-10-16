using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_MSG_DETAIL 
    {

        #region private data
        private int _mIP_MSG_ID;
        private int _mSG_NO;
        private string _mSG_SOURCE;
        private string _pCA_MSG_ID;
        private string _aCCOUNTID;
        private string _rESERVATION;
        private DateTime _oNLINE_TIME;
        private DateTime _oFFLINE_TIME;
        private string _dOPUSH;
        private string _mSG_KIND_1;
        private string _mSG_KIND_2;
        private string _mSG_TITLE;
        private string _mSG_CONTENT;
        private string _rEAD_STATE;
        private DateTime _rEAD_TIME;
        private DateTime _lDATE;
        private string _rEC_STA;
        private int _pUSHED;

        #endregion

        #region Properties
        public int MIP_MSG_ID
        {
           get { return _mIP_MSG_ID; }
           set { _mIP_MSG_ID = value;}
        }
        public int MSG_NO
        {
           get { return _mSG_NO; }
           set { _mSG_NO = value;}
        }
        public string MSG_SOURCE
        {
           get { return _mSG_SOURCE; }
           set { _mSG_SOURCE = value;}
        }
        public string PCA_MSG_ID
        {
           get { return _pCA_MSG_ID; }
           set { _pCA_MSG_ID = value;}
        }
        public string ACCOUNTID
        {
           get { return _aCCOUNTID; }
           set { _aCCOUNTID = value;}
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
        public string READ_STATE
        {
           get { return _rEAD_STATE; }
           set { _rEAD_STATE = value;}
        }
        public DateTime READ_TIME
        {
           get { return _rEAD_TIME; }
           set { _rEAD_TIME = value;}
        }
        public DateTime LDATE
        {
           get { return _lDATE; }
           set { _lDATE = value;}
        }
        public string REC_STA
        {
           get { return _rEC_STA; }
           set { _rEC_STA = value;}
        }
        public int PUSHED
        {
           get { return _pUSHED; }
           set { _pUSHED = value;}
        }

        #endregion

        #region Ctor/init
        public MIP_MSG_DETAIL() {}
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
                cmd.CommandText = "INSERT INTO MIP_MSG_DETAIL (MIP_MSG_ID, MSG_NO, MSG_SOURCE, PCA_MSG_ID, ACCOUNTID, RESERVATION, ONLINE_TIME, OFFLINE_TIME, DOPUSH, MSG_KIND_1, MSG_KIND_2, MSG_TITLE, MSG_CONTENT, READ_STATE, READ_TIME, LDATE, REC_STA, PUSHED) VALUES (@MIP_MSG_ID_PARAMS, @MSG_NO_PARAMS, @MSG_SOURCE_PARAMS, @PCA_MSG_ID_PARAMS, @ACCOUNTID_PARAMS, @RESERVATION_PARAMS, @ONLINE_TIME_PARAMS, @OFFLINE_TIME_PARAMS, @DOPUSH_PARAMS, @MSG_KIND_1_PARAMS, @MSG_KIND_2_PARAMS, @MSG_TITLE_PARAMS, @MSG_CONTENT_PARAMS, @READ_STATE_PARAMS, @READ_TIME_PARAMS, @LDATE_PARAMS, @REC_STA_PARAMS, @PUSHED_PARAMS)";
                                cmd.Parameters.AddWithValue("@MIP_MSG_ID_PARAM", _mIP_MSG_ID);
                cmd.Parameters.AddWithValue("@MSG_NO_PARAM", _mSG_NO);
                cmd.Parameters.AddWithValue("@MSG_SOURCE_PARAM", _mSG_SOURCE);
                cmd.Parameters.AddWithValue("@PCA_MSG_ID_PARAM", _pCA_MSG_ID);
                cmd.Parameters.AddWithValue("@ACCOUNTID_PARAM", _aCCOUNTID);
                cmd.Parameters.AddWithValue("@RESERVATION_PARAM", _rESERVATION);
                cmd.Parameters.AddWithValue("@ONLINE_TIME_PARAM", _oNLINE_TIME);
                cmd.Parameters.AddWithValue("@OFFLINE_TIME_PARAM", _oFFLINE_TIME);
                cmd.Parameters.AddWithValue("@DOPUSH_PARAM", _dOPUSH);
                cmd.Parameters.AddWithValue("@MSG_KIND_1_PARAM", _mSG_KIND_1);
                cmd.Parameters.AddWithValue("@MSG_KIND_2_PARAM", _mSG_KIND_2);
                cmd.Parameters.AddWithValue("@MSG_TITLE_PARAM", _mSG_TITLE);
                cmd.Parameters.AddWithValue("@MSG_CONTENT_PARAM", _mSG_CONTENT);
                cmd.Parameters.AddWithValue("@READ_STATE_PARAM", _rEAD_STATE);
                cmd.Parameters.AddWithValue("@READ_TIME_PARAM", _rEAD_TIME);
                cmd.Parameters.AddWithValue("@LDATE_PARAM", _lDATE);
                cmd.Parameters.AddWithValue("@REC_STA_PARAM", _rEC_STA);
                cmd.Parameters.AddWithValue("@PUSHED_PARAM", _pUSHED);

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
                cmd.CommandText = "SELECT MIP_MSG_ID, MSG_NO, MSG_SOURCE, PCA_MSG_ID, ACCOUNTID, RESERVATION, ONLINE_TIME, OFFLINE_TIME, DOPUSH, MSG_KIND_1, MSG_KIND_2, MSG_TITLE, MSG_CONTENT, READ_STATE, READ_TIME, LDATE, REC_STA, PUSHED FROM MIP_MSG_DETAIL WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _mIP_MSG_ID = reader.GetInt32(0);
                _mSG_NO = reader.GetInt32(1);
                _mSG_SOURCE = reader.GetString(2);
                _pCA_MSG_ID = reader.GetString(3);
                _aCCOUNTID = reader.GetString(4);
                _rESERVATION = reader.GetString(5);
                _oNLINE_TIME = reader.GetDateTime(6);
                _oFFLINE_TIME = reader.GetDateTime(7);
                _dOPUSH = reader.GetString(8);
                _mSG_KIND_1 = reader.GetString(9);
                _mSG_KIND_2 = reader.GetString(10);
                _mSG_TITLE = reader.GetString(11);
                _mSG_CONTENT = reader.GetString(12);
                _rEAD_STATE = reader.GetString(13);
                _rEAD_TIME = reader.GetDateTime(14);
                _lDATE = reader.GetDateTime(15);
                _rEC_STA = reader.GetString(16);
                _pUSHED = reader.GetInt32(17);

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
                cmd.CommandText = "UPDATE MIP_MSG_DETAIL SET MIP_MSG_ID=@MIP_MSG_ID_PARAMS, MSG_NO=@MSG_NO_PARAMS, MSG_SOURCE=@MSG_SOURCE_PARAMS, PCA_MSG_ID=@PCA_MSG_ID_PARAMS, ACCOUNTID=@ACCOUNTID_PARAMS, RESERVATION=@RESERVATION_PARAMS, ONLINE_TIME=@ONLINE_TIME_PARAMS, OFFLINE_TIME=@OFFLINE_TIME_PARAMS, DOPUSH=@DOPUSH_PARAMS, MSG_KIND_1=@MSG_KIND_1_PARAMS, MSG_KIND_2=@MSG_KIND_2_PARAMS, MSG_TITLE=@MSG_TITLE_PARAMS, MSG_CONTENT=@MSG_CONTENT_PARAMS, READ_STATE=@READ_STATE_PARAMS, READ_TIME=@READ_TIME_PARAMS, LDATE=@LDATE_PARAMS, REC_STA=@REC_STA_PARAMS, PUSHED=@PUSHED_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@MIP_MSG_ID_PARAM", _mIP_MSG_ID);
                cmd.Parameters.AddWithValue("@MSG_NO_PARAM", _mSG_NO);
                cmd.Parameters.AddWithValue("@MSG_SOURCE_PARAM", _mSG_SOURCE);
                cmd.Parameters.AddWithValue("@PCA_MSG_ID_PARAM", _pCA_MSG_ID);
                cmd.Parameters.AddWithValue("@ACCOUNTID_PARAM", _aCCOUNTID);
                cmd.Parameters.AddWithValue("@RESERVATION_PARAM", _rESERVATION);
                cmd.Parameters.AddWithValue("@ONLINE_TIME_PARAM", _oNLINE_TIME);
                cmd.Parameters.AddWithValue("@OFFLINE_TIME_PARAM", _oFFLINE_TIME);
                cmd.Parameters.AddWithValue("@DOPUSH_PARAM", _dOPUSH);
                cmd.Parameters.AddWithValue("@MSG_KIND_1_PARAM", _mSG_KIND_1);
                cmd.Parameters.AddWithValue("@MSG_KIND_2_PARAM", _mSG_KIND_2);
                cmd.Parameters.AddWithValue("@MSG_TITLE_PARAM", _mSG_TITLE);
                cmd.Parameters.AddWithValue("@MSG_CONTENT_PARAM", _mSG_CONTENT);
                cmd.Parameters.AddWithValue("@READ_STATE_PARAM", _rEAD_STATE);
                cmd.Parameters.AddWithValue("@READ_TIME_PARAM", _rEAD_TIME);
                cmd.Parameters.AddWithValue("@LDATE_PARAM", _lDATE);
                cmd.Parameters.AddWithValue("@REC_STA_PARAM", _rEC_STA);
                cmd.Parameters.AddWithValue("@PUSHED_PARAM", _pUSHED);

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
                cmd.CommandText = "DELETE FROM MIP_MSG_DETAIL WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

