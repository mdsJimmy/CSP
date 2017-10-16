using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class SecurityUserAccount_OperateRecord 
    {

        #region private data
        private string _recordID;
        private string _moduleDesc;
        private string _functionDesc;
        private string _actionDesc;
        private string _fieldDesc;
        private string _fieldBefore;
        private string _fieldAfter;
        private string _name;
        private string _accountID;
        private DateTime _cDatetime;
        private int _top5;

        #endregion

        #region Properties
        public string RecordID
        {
           get { return _recordID; }
           set { _recordID = value;}
        }
        public string ModuleDesc
        {
           get { return _moduleDesc; }
           set { _moduleDesc = value;}
        }
        public string FunctionDesc
        {
           get { return _functionDesc; }
           set { _functionDesc = value;}
        }
        public string ActionDesc
        {
           get { return _actionDesc; }
           set { _actionDesc = value;}
        }
        public string FieldDesc
        {
           get { return _fieldDesc; }
           set { _fieldDesc = value;}
        }
        public string FieldBefore
        {
           get { return _fieldBefore; }
           set { _fieldBefore = value;}
        }
        public string FieldAfter
        {
           get { return _fieldAfter; }
           set { _fieldAfter = value;}
        }
        public string Name
        {
           get { return _name; }
           set { _name = value;}
        }
        public string AccountID
        {
           get { return _accountID; }
           set { _accountID = value;}
        }
        public DateTime cDatetime
        {
           get { return _cDatetime; }
           set { _cDatetime = value;}
        }
        public int Top5
        {
           get { return _top5; }
           set { _top5 = value;}
        }

        #endregion

        #region Ctor/init
        public SecurityUserAccount_OperateRecord() {}
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
                cmd.CommandText = "INSERT INTO SecurityUserAccount_OperateRecord (RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime, Top5) VALUES (@RecordID_PARAMS, @ModuleDesc_PARAMS, @FunctionDesc_PARAMS, @ActionDesc_PARAMS, @FieldDesc_PARAMS, @FieldBefore_PARAMS, @FieldAfter_PARAMS, @Name_PARAMS, @AccountID_PARAMS, @cDatetime_PARAMS, @Top5_PARAMS)";
                                cmd.Parameters.AddWithValue("@RecordID_PARAM", _recordID);
                cmd.Parameters.AddWithValue("@ModuleDesc_PARAM", _moduleDesc);
                cmd.Parameters.AddWithValue("@FunctionDesc_PARAM", _functionDesc);
                cmd.Parameters.AddWithValue("@ActionDesc_PARAM", _actionDesc);
                cmd.Parameters.AddWithValue("@FieldDesc_PARAM", _fieldDesc);
                cmd.Parameters.AddWithValue("@FieldBefore_PARAM", _fieldBefore);
                cmd.Parameters.AddWithValue("@FieldAfter_PARAM", _fieldAfter);
                cmd.Parameters.AddWithValue("@Name_PARAM", _name);
                cmd.Parameters.AddWithValue("@AccountID_PARAM", _accountID);
                cmd.Parameters.AddWithValue("@cDatetime_PARAM", _cDatetime);
                cmd.Parameters.AddWithValue("@Top5_PARAM", _top5);

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
                cmd.CommandText = "SELECT RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime, Top5 FROM SecurityUserAccount_OperateRecord WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _recordID = reader.GetString(0);
                _moduleDesc = reader.GetString(1);
                _functionDesc = reader.GetString(2);
                _actionDesc = reader.GetString(3);
                _fieldDesc = reader.GetString(4);
                _fieldBefore = reader.GetString(5);
                _fieldAfter = reader.GetString(6);
                _name = reader.GetString(7);
                _accountID = reader.GetString(8);
                _cDatetime = reader.GetDateTime(9);
                _top5 = reader.GetInt32(10);

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
                cmd.CommandText = "UPDATE SecurityUserAccount_OperateRecord SET RecordID=@RecordID_PARAMS, ModuleDesc=@ModuleDesc_PARAMS, FunctionDesc=@FunctionDesc_PARAMS, ActionDesc=@ActionDesc_PARAMS, FieldDesc=@FieldDesc_PARAMS, FieldBefore=@FieldBefore_PARAMS, FieldAfter=@FieldAfter_PARAMS, Name=@Name_PARAMS, AccountID=@AccountID_PARAMS, cDatetime=@cDatetime_PARAMS, Top5=@Top5_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@RecordID_PARAM", _recordID);
                cmd.Parameters.AddWithValue("@ModuleDesc_PARAM", _moduleDesc);
                cmd.Parameters.AddWithValue("@FunctionDesc_PARAM", _functionDesc);
                cmd.Parameters.AddWithValue("@ActionDesc_PARAM", _actionDesc);
                cmd.Parameters.AddWithValue("@FieldDesc_PARAM", _fieldDesc);
                cmd.Parameters.AddWithValue("@FieldBefore_PARAM", _fieldBefore);
                cmd.Parameters.AddWithValue("@FieldAfter_PARAM", _fieldAfter);
                cmd.Parameters.AddWithValue("@Name_PARAM", _name);
                cmd.Parameters.AddWithValue("@AccountID_PARAM", _accountID);
                cmd.Parameters.AddWithValue("@cDatetime_PARAM", _cDatetime);
                cmd.Parameters.AddWithValue("@Top5_PARAM", _top5);

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
                cmd.CommandText = "DELETE FROM SecurityUserAccount_OperateRecord WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

