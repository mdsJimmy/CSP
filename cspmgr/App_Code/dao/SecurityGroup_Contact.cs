using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class SecurityGroup_Contact 
    {

        #region private data
        private int _contactID;
        private string _contactName;
        private string _title;
        private string _tel1;
        private string _tel2;
        private string _tel3;
        private string _eMail;
        private string _memo;

        #endregion

        #region Properties
        public int ContactID
        {
           get { return _contactID; }
           set { _contactID = value;}
        }
        public string ContactName
        {
           get { return _contactName; }
           set { _contactName = value;}
        }
        public string Title
        {
           get { return _title; }
           set { _title = value;}
        }
        public string Tel1
        {
           get { return _tel1; }
           set { _tel1 = value;}
        }
        public string Tel2
        {
           get { return _tel2; }
           set { _tel2 = value;}
        }
        public string Tel3
        {
           get { return _tel3; }
           set { _tel3 = value;}
        }
        public string EMail
        {
           get { return _eMail; }
           set { _eMail = value;}
        }
        public string Memo
        {
           get { return _memo; }
           set { _memo = value;}
        }

        #endregion

        #region Ctor/init
        public SecurityGroup_Contact() {}
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
                cmd.CommandText = "INSERT INTO SecurityGroup_Contact (ContactID, ContactName, Title, Tel1, Tel2, Tel3, EMail, Memo) VALUES (@ContactID_PARAMS, @ContactName_PARAMS, @Title_PARAMS, @Tel1_PARAMS, @Tel2_PARAMS, @Tel3_PARAMS, @EMail_PARAMS, @Memo_PARAMS)";
                                cmd.Parameters.AddWithValue("@ContactID_PARAM", _contactID);
                cmd.Parameters.AddWithValue("@ContactName_PARAM", _contactName);
                cmd.Parameters.AddWithValue("@Title_PARAM", _title);
                cmd.Parameters.AddWithValue("@Tel1_PARAM", _tel1);
                cmd.Parameters.AddWithValue("@Tel2_PARAM", _tel2);
                cmd.Parameters.AddWithValue("@Tel3_PARAM", _tel3);
                cmd.Parameters.AddWithValue("@EMail_PARAM", _eMail);
                cmd.Parameters.AddWithValue("@Memo_PARAM", _memo);

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
                cmd.CommandText = "SELECT ContactID, ContactName, Title, Tel1, Tel2, Tel3, EMail, Memo FROM SecurityGroup_Contact WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _contactID = reader.GetInt32(0);
                _contactName = reader.GetString(1);
                _title = reader.GetString(2);
                _tel1 = reader.GetString(3);
                _tel2 = reader.GetString(4);
                _tel3 = reader.GetString(5);
                _eMail = reader.GetString(6);
                _memo = reader.GetString(7);

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
                cmd.CommandText = "UPDATE SecurityGroup_Contact SET ContactID=@ContactID_PARAMS, ContactName=@ContactName_PARAMS, Title=@Title_PARAMS, Tel1=@Tel1_PARAMS, Tel2=@Tel2_PARAMS, Tel3=@Tel3_PARAMS, EMail=@EMail_PARAMS, Memo=@Memo_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@ContactID_PARAM", _contactID);
                cmd.Parameters.AddWithValue("@ContactName_PARAM", _contactName);
                cmd.Parameters.AddWithValue("@Title_PARAM", _title);
                cmd.Parameters.AddWithValue("@Tel1_PARAM", _tel1);
                cmd.Parameters.AddWithValue("@Tel2_PARAM", _tel2);
                cmd.Parameters.AddWithValue("@Tel3_PARAM", _tel3);
                cmd.Parameters.AddWithValue("@EMail_PARAM", _eMail);
                cmd.Parameters.AddWithValue("@Memo_PARAM", _memo);

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
                cmd.CommandText = "DELETE FROM SecurityGroup_Contact WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

