using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MIP_LINK 
    {

        #region private data
        private int _lINK_ID;
        private int _cSTATUS;
        private string _tITLE;
        private string _uRL;
        private int _cORDER;
        private DateTime _lDATE;
        private string _lUSER;

        #endregion

        #region Properties
        public int LINK_ID
        {
           get { return _lINK_ID; }
           set { _lINK_ID = value;}
        }
        public int CSTATUS
        {
           get { return _cSTATUS; }
           set { _cSTATUS = value;}
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

        #endregion

        #region Ctor/init
        public MIP_LINK() {}
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
                cmd.CommandText = "INSERT INTO MIP_LINK (LINK_ID, CSTATUS, TITLE, URL, CORDER, LDATE, LUSER) VALUES (@LINK_ID_PARAMS, @CSTATUS_PARAMS, @TITLE_PARAMS, @URL_PARAMS, @CORDER_PARAMS, @LDATE_PARAMS, @LUSER_PARAMS)";
                                cmd.Parameters.AddWithValue("@LINK_ID_PARAM", _lINK_ID);
                cmd.Parameters.AddWithValue("@CSTATUS_PARAM", _cSTATUS);
                cmd.Parameters.AddWithValue("@TITLE_PARAM", _tITLE);
                cmd.Parameters.AddWithValue("@URL_PARAM", _uRL);
                cmd.Parameters.AddWithValue("@CORDER_PARAM", _cORDER);
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
                cmd.CommandText = "SELECT LINK_ID, CSTATUS, TITLE, URL, CORDER, LDATE, LUSER FROM MIP_LINK WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _lINK_ID = reader.GetInt32(0);
                _cSTATUS = reader.GetInt32(1);
                _tITLE = reader.GetString(2);
                _uRL = reader.GetString(3);
                _cORDER = reader.GetInt32(4);
                _lDATE = reader.GetDateTime(5);
                _lUSER = reader.GetString(6);

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
                cmd.CommandText = "UPDATE MIP_LINK SET LINK_ID=@LINK_ID_PARAMS, CSTATUS=@CSTATUS_PARAMS, TITLE=@TITLE_PARAMS, URL=@URL_PARAMS, CORDER=@CORDER_PARAMS, LDATE=@LDATE_PARAMS, LUSER=@LUSER_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@LINK_ID_PARAM", _lINK_ID);
                cmd.Parameters.AddWithValue("@CSTATUS_PARAM", _cSTATUS);
                cmd.Parameters.AddWithValue("@TITLE_PARAM", _tITLE);
                cmd.Parameters.AddWithValue("@URL_PARAM", _uRL);
                cmd.Parameters.AddWithValue("@CORDER_PARAM", _cORDER);
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
                cmd.CommandText = "DELETE FROM MIP_LINK WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

