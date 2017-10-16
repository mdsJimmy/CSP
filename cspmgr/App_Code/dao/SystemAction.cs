using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class SystemAction 
    {

        #region private data
        private string _sysActionID;
        private string _sysFuncID;
        private string _sysModID;
        private string _actionDesc;
        private string _buttonID;

        #endregion

        #region Properties
        public string SysActionID
        {
           get { return _sysActionID; }
           set { _sysActionID = value;}
        }
        public string SysFuncID
        {
           get { return _sysFuncID; }
           set { _sysFuncID = value;}
        }
        public string SysModID
        {
           get { return _sysModID; }
           set { _sysModID = value;}
        }
        public string ActionDesc
        {
           get { return _actionDesc; }
           set { _actionDesc = value;}
        }
        public string ButtonID
        {
           get { return _buttonID; }
           set { _buttonID = value;}
        }

        #endregion

        #region Ctor/init
        public SystemAction() {}
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
                cmd.CommandText = "INSERT INTO SystemAction (SysActionID, SysFuncID, SysModID, ActionDesc, ButtonID) VALUES (@SysActionID_PARAMS, @SysFuncID_PARAMS, @SysModID_PARAMS, @ActionDesc_PARAMS, @ButtonID_PARAMS)";
                                cmd.Parameters.AddWithValue("@SysActionID_PARAM", _sysActionID);
                cmd.Parameters.AddWithValue("@SysFuncID_PARAM", _sysFuncID);
                cmd.Parameters.AddWithValue("@SysModID_PARAM", _sysModID);
                cmd.Parameters.AddWithValue("@ActionDesc_PARAM", _actionDesc);
                cmd.Parameters.AddWithValue("@ButtonID_PARAM", _buttonID);

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
                cmd.CommandText = "SELECT SysActionID, SysFuncID, SysModID, ActionDesc, ButtonID FROM SystemAction WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _sysActionID = reader.GetString(0);
                _sysFuncID = reader.GetString(1);
                _sysModID = reader.GetString(2);
                _actionDesc = reader.GetString(3);
                _buttonID = reader.GetString(4);

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
                cmd.CommandText = "UPDATE SystemAction SET SysActionID=@SysActionID_PARAMS, SysFuncID=@SysFuncID_PARAMS, SysModID=@SysModID_PARAMS, ActionDesc=@ActionDesc_PARAMS, ButtonID=@ButtonID_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@SysActionID_PARAM", _sysActionID);
                cmd.Parameters.AddWithValue("@SysFuncID_PARAM", _sysFuncID);
                cmd.Parameters.AddWithValue("@SysModID_PARAM", _sysModID);
                cmd.Parameters.AddWithValue("@ActionDesc_PARAM", _actionDesc);
                cmd.Parameters.AddWithValue("@ButtonID_PARAM", _buttonID);

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
                cmd.CommandText = "DELETE FROM SystemAction WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

