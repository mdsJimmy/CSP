using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class SystemModule 
    {

        #region private data
        private string _sysModID;
        private string _moduleDesc;
        private int _iOrder;
        private int _isDefault;
        private string _pageLink;
        private string _pic;
        private int _iDisplay;

        #endregion

        #region Properties
        public string SysModID
        {
           get { return _sysModID; }
           set { _sysModID = value;}
        }
        public string ModuleDesc
        {
           get { return _moduleDesc; }
           set { _moduleDesc = value;}
        }
        public int iOrder
        {
           get { return _iOrder; }
           set { _iOrder = value;}
        }
        public int isDefault
        {
           get { return _isDefault; }
           set { _isDefault = value;}
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
        public int iDisplay
        {
           get { return _iDisplay; }
           set { _iDisplay = value;}
        }

        #endregion

        #region Ctor/init
        public SystemModule() {}
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
                cmd.CommandText = "INSERT INTO SystemModule (SysModID, ModuleDesc, iOrder, isDefault, PageLink, Pic, iDisplay) VALUES (@SysModID_PARAMS, @ModuleDesc_PARAMS, @iOrder_PARAMS, @isDefault_PARAMS, @PageLink_PARAMS, @Pic_PARAMS, @iDisplay_PARAMS)";
                                cmd.Parameters.AddWithValue("@SysModID_PARAM", _sysModID);
                cmd.Parameters.AddWithValue("@ModuleDesc_PARAM", _moduleDesc);
                cmd.Parameters.AddWithValue("@iOrder_PARAM", _iOrder);
                cmd.Parameters.AddWithValue("@isDefault_PARAM", _isDefault);
                cmd.Parameters.AddWithValue("@PageLink_PARAM", _pageLink);
                cmd.Parameters.AddWithValue("@Pic_PARAM", _pic);
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
                cmd.CommandText = "SELECT SysModID, ModuleDesc, iOrder, isDefault, PageLink, Pic, iDisplay FROM SystemModule WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _sysModID = reader.GetString(0);
                _moduleDesc = reader.GetString(1);
                _iOrder = reader.GetInt32(2);
                _isDefault = reader.GetInt32(3);
                _pageLink = reader.GetString(4);
                _pic = reader.GetString(5);
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
                cmd.CommandText = "UPDATE SystemModule SET SysModID=@SysModID_PARAMS, ModuleDesc=@ModuleDesc_PARAMS, iOrder=@iOrder_PARAMS, isDefault=@isDefault_PARAMS, PageLink=@PageLink_PARAMS, Pic=@Pic_PARAMS, iDisplay=@iDisplay_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@SysModID_PARAM", _sysModID);
                cmd.Parameters.AddWithValue("@ModuleDesc_PARAM", _moduleDesc);
                cmd.Parameters.AddWithValue("@iOrder_PARAM", _iOrder);
                cmd.Parameters.AddWithValue("@isDefault_PARAM", _isDefault);
                cmd.Parameters.AddWithValue("@PageLink_PARAM", _pageLink);
                cmd.Parameters.AddWithValue("@Pic_PARAM", _pic);
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
                cmd.CommandText = "DELETE FROM SystemModule WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

