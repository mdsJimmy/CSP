using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_HAPPY 
    {

        #region private data
        private int _hAPPY_ID;
        private int _cSTATUS;
        private int _fILE_KIND;
        private int _aPPLY_TARGET;
        private string _cKEY1;
        private string _cKEY2;
        private string _cKEY3;
        private string _tITLE;
        private string _uRL;
        private int _f_IDX;
        private string _f_NAME;
        private int _cORDER;
        private DateTime _lDATE;
        private string _lUSER;
        private int _sELECTALL;
        private int _iSTESTER;
        private DateTime _oNLINE_TIME;
        private DateTime _oFFLINE_TIME;

        #endregion

        #region Properties
        public int HAPPY_ID
        {
           get { return _hAPPY_ID; }
           set { _hAPPY_ID = value;}
        }
        public int CSTATUS
        {
           get { return _cSTATUS; }
           set { _cSTATUS = value;}
        }
        public int FILE_KIND
        {
           get { return _fILE_KIND; }
           set { _fILE_KIND = value;}
        }
        public int APPLY_TARGET
        {
           get { return _aPPLY_TARGET; }
           set { _aPPLY_TARGET = value;}
        }
        public string CKEY1
        {
           get { return _cKEY1; }
           set { _cKEY1 = value;}
        }
        public string CKEY2
        {
           get { return _cKEY2; }
           set { _cKEY2 = value;}
        }
        public string CKEY3
        {
           get { return _cKEY3; }
           set { _cKEY3 = value;}
        }
        public string TITLE
        {
           get { return _tITLE; }
           set { _tITLE = value;}
        }
        public string URL
        {
           get { return _uRL; }
           set { _uRL = value;}
        }
        public int F_IDX
        {
           get { return _f_IDX; }
           set { _f_IDX = value;}
        }
        public string F_NAME
        {
           get { return _f_NAME; }
           set { _f_NAME = value;}
        }
        public int CORDER
        {
           get { return _cORDER; }
           set { _cORDER = value;}
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
        public int ISTESTER
        {
           get { return _iSTESTER; }
           set { _iSTESTER = value;}
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

        #endregion

        #region Ctor/init
        public MIP_HAPPY() {}
        #endregion

        #region Data Access Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public int Insert(System.Data.SqlClient.SqlCommand cmd)
        {

            cmd.CommandText = "INSERT INTO MIP_HAPPY (HAPPY_ID, CSTATUS, APPLY_TARGET, CKEY1, CKEY2, CKEY3, TITLE,CORDER, LDATE, LUSER, SELECTALL, ISTESTER) VALUES (@HAPPY_ID_PARAMS, @CSTATUS_PARAMS, @APPLY_TARGET_PARAMS, @CKEY1_PARAMS, @CKEY2_PARAMS, @CKEY3_PARAMS, @TITLE_PARAMS, @CORDER_PARAMS, @LDATE_PARAMS, @LUSER_PARAMS, @SELECTALL_PARAMS, @ISTESTER_PARAMS)";
            cmd.Parameters.AddWithValue("@HAPPY_ID_PARAMS", _hAPPY_ID);
            cmd.Parameters.AddWithValue("@CSTATUS_PARAMS", _cSTATUS);
            cmd.Parameters.AddWithValue("@APPLY_TARGET_PARAMS", _aPPLY_TARGET);
            cmd.Parameters.AddWithValue("@CKEY1_PARAMS", _cKEY1);
            cmd.Parameters.AddWithValue("@CKEY2_PARAMS", _cKEY2);
            cmd.Parameters.AddWithValue("@CKEY3_PARAMS", _cKEY3);
            cmd.Parameters.AddWithValue("@TITLE_PARAMS", _tITLE);
            cmd.Parameters.AddWithValue("@CORDER_PARAMS", _cORDER);
            cmd.Parameters.AddWithValue("@LDATE_PARAMS", _lDATE);
            cmd.Parameters.AddWithValue("@LUSER_PARAMS", _lUSER);
            cmd.Parameters.AddWithValue("@SELECTALL_PARAMS", _sELECTALL);
            cmd.Parameters.AddWithValue("@ISTESTER_PARAMS", _iSTESTER);

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
                cmd.CommandText = "SELECT HAPPY_ID, CSTATUS, FILE_KIND, APPLY_TARGET, CKEY1, CKEY2, CKEY3, TITLE, URL, F_IDX, F_NAME, CORDER, LDATE, LUSER, SELECTALL, ISTESTER, ONLINE_TIME, OFFLINE_TIME FROM MIP_HAPPY WHERE HAPPY_ID=@HAPPY_ID_PARAM";
                                cmd.Parameters.AddWithValue("@HAPPY_ID_PARAM", _hAPPY_ID);

                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                _hAPPY_ID = reader.GetInt32(0);
                _cSTATUS = reader.GetInt32(1);
                _fILE_KIND = reader.GetInt32(2);
                _aPPLY_TARGET = reader.GetInt32(3);
                _cKEY1 = reader.GetString(4);
                _cKEY2 = reader.GetString(5);
                _cKEY3 = reader.GetString(6);
                _tITLE = reader.GetString(7);
                _uRL = reader.GetString(8);
                _f_IDX = reader.GetInt32(9);
                _f_NAME = reader.GetString(10);
                _cORDER = reader.GetInt32(11);
                _lDATE = reader.GetDateTime(12);
                _lUSER = reader.GetString(13);
                _sELECTALL = reader.GetInt32(14);
                _iSTESTER = reader.GetInt32(15);
                _oNLINE_TIME = reader.GetDateTime(16);
                _oFFLINE_TIME = reader.GetDateTime(17);

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
                cmd.CommandText = "UPDATE MIP_HAPPY SET HAPPY_ID=@HAPPY_ID_PARAMS, CSTATUS=@CSTATUS_PARAMS, FILE_KIND=@FILE_KIND_PARAMS, APPLY_TARGET=@APPLY_TARGET_PARAMS, CKEY1=@CKEY1_PARAMS, CKEY2=@CKEY2_PARAMS, CKEY3=@CKEY3_PARAMS, TITLE=@TITLE_PARAMS, URL=@URL_PARAMS, F_IDX=@F_IDX_PARAMS, F_NAME=@F_NAME_PARAMS, CORDER=@CORDER_PARAMS, LDATE=@LDATE_PARAMS, LUSER=@LUSER_PARAMS, SELECTALL=@SELECTALL_PARAMS, ISTESTER=@ISTESTER_PARAMS, ONLINE_TIME=@ONLINE_TIME_PARAMS, OFFLINE_TIME=@OFFLINE_TIME_PARAMS WHERE HAPPY_ID=@HAPPY_ID_PARAM";
                                cmd.Parameters.AddWithValue("@HAPPY_ID_PARAM", _hAPPY_ID);
                cmd.Parameters.AddWithValue("@CSTATUS_PARAM", _cSTATUS);
                cmd.Parameters.AddWithValue("@FILE_KIND_PARAM", _fILE_KIND);
                cmd.Parameters.AddWithValue("@APPLY_TARGET_PARAM", _aPPLY_TARGET);
                cmd.Parameters.AddWithValue("@CKEY1_PARAM", _cKEY1);
                cmd.Parameters.AddWithValue("@CKEY2_PARAM", _cKEY2);
                cmd.Parameters.AddWithValue("@CKEY3_PARAM", _cKEY3);
                cmd.Parameters.AddWithValue("@TITLE_PARAM", _tITLE);
                cmd.Parameters.AddWithValue("@URL_PARAM", _uRL);
                cmd.Parameters.AddWithValue("@F_IDX_PARAM", _f_IDX);
                cmd.Parameters.AddWithValue("@F_NAME_PARAM", _f_NAME);
                cmd.Parameters.AddWithValue("@CORDER_PARAM", _cORDER);
                cmd.Parameters.AddWithValue("@LDATE_PARAM", _lDATE);
                cmd.Parameters.AddWithValue("@LUSER_PARAM", _lUSER);
                cmd.Parameters.AddWithValue("@SELECTALL_PARAM", _sELECTALL);
                cmd.Parameters.AddWithValue("@ISTESTER_PARAM", _iSTESTER);
                cmd.Parameters.AddWithValue("@ONLINE_TIME_PARAM", _oNLINE_TIME);
                cmd.Parameters.AddWithValue("@OFFLINE_TIME_PARAM", _oFFLINE_TIME);

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
                cmd.CommandText = "DELETE FROM MIP_HAPPY WHERE HAPPY_ID=@HAPPY_ID_PARAM";
                cmd.Parameters.AddWithValue("@HAPPY_ID_PARAM", _hAPPY_ID);

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

