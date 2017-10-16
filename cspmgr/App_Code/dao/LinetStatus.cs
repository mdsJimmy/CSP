using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class LinetStatus 
    {

        #region private data
        private DateTime _dateTime;
        private string _accountID;
        private short _type;
        private short _status;
        private string _txDateTime;
        private string _transDateTime;

        #endregion

        #region Properties
        public DateTime DateTime
        {
           get { return _dateTime; }
           set { _dateTime = value;}
        }
        public string AccountID
        {
           get { return _accountID; }
           set { _accountID = value;}
        }
        public short Type
        {
           get { return _type; }
           set { _type = value;}
        }
        public short Status
        {
           get { return _status; }
           set { _status = value;}
        }
        public string TxDateTime
        {
           get { return _txDateTime; }
           set { _txDateTime = value;}
        }
        public string TransDateTime
        {
           get { return _transDateTime; }
           set { _transDateTime = value;}
        }

        #endregion

        #region Ctor/init
        public LinetStatus() {}
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
                cmd.CommandText = "INSERT INTO LinetStatus (DateTime, AccountID, Type, Status, TxDateTime, TransDateTime) VALUES (@DateTime_PARAMS, @AccountID_PARAMS, @Type_PARAMS, @Status_PARAMS, @TxDateTime_PARAMS, @TransDateTime_PARAMS)";
                                cmd.Parameters.AddWithValue("@DateTime_PARAM", _dateTime);
                cmd.Parameters.AddWithValue("@AccountID_PARAM", _accountID);
                cmd.Parameters.AddWithValue("@Type_PARAM", _type);
                cmd.Parameters.AddWithValue("@Status_PARAM", _status);
                cmd.Parameters.AddWithValue("@TxDateTime_PARAM", _txDateTime);
                cmd.Parameters.AddWithValue("@TransDateTime_PARAM", _transDateTime);

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
                cmd.CommandText = "SELECT DateTime, AccountID, Type, Status, TxDateTime, TransDateTime FROM LinetStatus WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _dateTime = reader.GetDateTime(0);
                _accountID = reader.GetString(1);
                _type = reader.GetInt16(2);
                _status = reader.GetInt16(3);
                _txDateTime = reader.GetString(4);
                _transDateTime = reader.GetString(5);

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
                cmd.CommandText = "UPDATE LinetStatus SET DateTime=@DateTime_PARAMS, AccountID=@AccountID_PARAMS, Type=@Type_PARAMS, Status=@Status_PARAMS, TxDateTime=@TxDateTime_PARAMS, TransDateTime=@TransDateTime_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@DateTime_PARAM", _dateTime);
                cmd.Parameters.AddWithValue("@AccountID_PARAM", _accountID);
                cmd.Parameters.AddWithValue("@Type_PARAM", _type);
                cmd.Parameters.AddWithValue("@Status_PARAM", _status);
                cmd.Parameters.AddWithValue("@TxDateTime_PARAM", _txDateTime);
                cmd.Parameters.AddWithValue("@TransDateTime_PARAM", _transDateTime);

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
                cmd.CommandText = "DELETE FROM LinetStatus WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

