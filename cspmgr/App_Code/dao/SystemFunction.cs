using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class SystemFunction 
    {

        #region private data
        private string _sysFuncID;
        private string _sysModID;
        private string _functionDesc;
        private string _pageLink;
        private string _pic;
        private int _iOrder;
        private int _iDisplay;

        #endregion

        #region Properties
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
        public string FunctionDesc
        {
           get { return _functionDesc; }
           set { _functionDesc = value;}
        }
        public string PageLink
        {
           get { return _pageLink; }
           set { _pageLink = value;}
        }
        public string Pic
        {
           get { return _pic; }
           set { _pic = value;}
        }
        public int iOrder
        {
           get { return _iOrder; }
           set { _iOrder = value;}
        }
        public int iDisplay
        {
           get { return _iDisplay; }
           set { _iDisplay = value;}
        }

        #endregion

        #region Ctor/init
        public SystemFunction() {}
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
                cmd.CommandText = "INSERT INTO SystemFunction (SysFuncID, SysModID, FunctionDesc, PageLink, Pic, iOrder, iDisplay) VALUES (@SysFuncID_PARAMS, @SysModID_PARAMS, @FunctionDesc_PARAMS, @PageLink_PARAMS, @Pic_PARAMS, @iOrder_PARAMS, @iDisplay_PARAMS)";
                                cmd.Parameters.AddWithValue("@SysFuncID_PARAM", _sysFuncID);
                cmd.Parameters.AddWithValue("@SysModID_PARAM", _sysModID);
                cmd.Parameters.AddWithValue("@FunctionDesc_PARAM", _functionDesc);
                cmd.Parameters.AddWithValue("@PageLink_PARAM", _pageLink);
                cmd.Parameters.AddWithValue("@Pic_PARAM", _pic);
                cmd.Parameters.AddWithValue("@iOrder_PARAM", _iOrder);
                cmd.Parameters.AddWithValue("@iDisplay_PARAM", _iDisplay);

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
                cmd.CommandText = "SELECT SysFuncID, SysModID, FunctionDesc, PageLink, Pic, iOrder, iDisplay FROM SystemFunction WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _sysFuncID = reader.GetString(0);
                _sysModID = reader.GetString(1);
                _functionDesc = reader.GetString(2);
                _pageLink = reader.GetString(3);
                _pic = reader.GetString(4);
                _iOrder = reader.GetInt32(5);
                _iDisplay = reader.GetInt32(6);

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
                cmd.CommandText = "UPDATE SystemFunction SET SysFuncID=@SysFuncID_PARAMS, SysModID=@SysModID_PARAMS, FunctionDesc=@FunctionDesc_PARAMS, PageLink=@PageLink_PARAMS, Pic=@Pic_PARAMS, iOrder=@iOrder_PARAMS, iDisplay=@iDisplay_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@SysFuncID_PARAM", _sysFuncID);
                cmd.Parameters.AddWithValue("@SysModID_PARAM", _sysModID);
                cmd.Parameters.AddWithValue("@FunctionDesc_PARAM", _functionDesc);
                cmd.Parameters.AddWithValue("@PageLink_PARAM", _pageLink);
                cmd.Parameters.AddWithValue("@Pic_PARAM", _pic);
                cmd.Parameters.AddWithValue("@iOrder_PARAM", _iOrder);
                cmd.Parameters.AddWithValue("@iDisplay_PARAM", _iDisplay);

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
                cmd.CommandText = "DELETE FROM SystemFunction WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

