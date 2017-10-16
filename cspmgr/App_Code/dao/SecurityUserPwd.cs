using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class SecurityUserPwd 
    {

        #region private data
        private string _accountID;
        private byte[] _bPassword;
        private short _iOrder;
        private int _kind;
        private DateTime _ldate;

        #endregion

        #region Properties
        public string AccountID
        {
           get { return _accountID; }
           set { _accountID = value;}
        }
        public byte[] bPassword
        {
           get { return _bPassword; }
           set { _bPassword = value;}
        }
        public short iOrder
        {
           get { return _iOrder; }
           set { _iOrder = value;}
        }
        public int Kind
        {
           get { return _kind; }
           set { _kind = value;}
        }
        public DateTime Ldate
        {
           get { return _ldate; }
           set { _ldate = value;}
        }

        #endregion

        #region Ctor/init
        public SecurityUserPwd() {}
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
                cmd.CommandText = "INSERT INTO SecurityUserPwd (AccountID, bPassword, iOrder, Kind, Ldate) VALUES (@AccountID_PARAMS, @bPassword_PARAMS, @iOrder_PARAMS, @Kind_PARAMS, @Ldate_PARAMS)";
                                cmd.Parameters.AddWithValue("@AccountID_PARAM", _accountID);
                cmd.Parameters.AddWithValue("@bPassword_PARAM", _bPassword);
                cmd.Parameters.AddWithValue("@iOrder_PARAM", _iOrder);
                cmd.Parameters.AddWithValue("@Kind_PARAM", _kind);
                cmd.Parameters.AddWithValue("@Ldate_PARAM", _ldate);

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
                cmd.CommandText = "SELECT AccountID, bPassword, iOrder, Kind, Ldate FROM SecurityUserPwd WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _accountID = reader.GetString(0);
                _bPassword = (byte[])reader[1];
                    _iOrder = reader.GetInt16(2);
                _kind = reader.GetInt32(3);
                _ldate = reader.GetDateTime(4);

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
                cmd.CommandText = "UPDATE SecurityUserPwd SET AccountID=@AccountID_PARAMS, bPassword=@bPassword_PARAMS, iOrder=@iOrder_PARAMS, Kind=@Kind_PARAMS, Ldate=@Ldate_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@AccountID_PARAM", _accountID);
                cmd.Parameters.AddWithValue("@bPassword_PARAM", _bPassword);
                cmd.Parameters.AddWithValue("@iOrder_PARAM", _iOrder);
                cmd.Parameters.AddWithValue("@Kind_PARAM", _kind);
                cmd.Parameters.AddWithValue("@Ldate_PARAM", _ldate);

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
                cmd.CommandText = "DELETE FROM SecurityUserPwd WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

