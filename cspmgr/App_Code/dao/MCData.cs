using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace mattraffel.com.CodeGenTest
{
    public partial class MCData 
    {

        #region private data
        private string _mCID;
        private string _groupID;
        private string _model;
        private string _mCNo;
        private string _maintainID;
        private string _iPAddress;
        private string _pU;
        private string _lU;
        private string _devicePoint;
        private string _sourceConnection;
        private int _aTMBandWidth;
        private string _sDGatewayID;
        private int _bandWidth;
        private string _mCAccountID;
        private short _mCFunc;
        private short _systemOS;
        private string _openTime;
        private string _bPoint;
        private string _detailAddress;
        private string _maintain_Money;
        private string _maintain_Sys;
        private string _maintain_Fix;
        private string _buyCaseNo;
        private DateTime _buyDate;
        private DateTime _openDate;
        private DateTime _installDate;
        private string _memo;
        private string _loginGorupID;

        #endregion

        #region Properties
        public string MCID
        {
           get { return _mCID; }
           set { _mCID = value;}
        }
        public string GroupID
        {
           get { return _groupID; }
           set { _groupID = value;}
        }
        public string Model
        {
           get { return _model; }
           set { _model = value;}
        }
        public string MCNo
        {
           get { return _mCNo; }
           set { _mCNo = value;}
        }
        public string MaintainID
        {
           get { return _maintainID; }
           set { _maintainID = value;}
        }
        public string IPAddress
        {
           get { return _iPAddress; }
           set { _iPAddress = value;}
        }
        public string PU
        {
           get { return _pU; }
           set { _pU = value;}
        }
        public string LU
        {
           get { return _lU; }
           set { _lU = value;}
        }
        public string DevicePoint
        {
           get { return _devicePoint; }
           set { _devicePoint = value;}
        }
        public string SourceConnection
        {
           get { return _sourceConnection; }
           set { _sourceConnection = value;}
        }
        public int ATMBandWidth
        {
           get { return _aTMBandWidth; }
           set { _aTMBandWidth = value;}
        }
        public string SDGatewayID
        {
           get { return _sDGatewayID; }
           set { _sDGatewayID = value;}
        }
        public int BandWidth
        {
           get { return _bandWidth; }
           set { _bandWidth = value;}
        }
        public string MCAccountID
        {
           get { return _mCAccountID; }
           set { _mCAccountID = value;}
        }
        public short MCFunc
        {
           get { return _mCFunc; }
           set { _mCFunc = value;}
        }
        public short SystemOS
        {
           get { return _systemOS; }
           set { _systemOS = value;}
        }
        public string OpenTime
        {
           get { return _openTime; }
           set { _openTime = value;}
        }
        public string bPoint
        {
           get { return _bPoint; }
           set { _bPoint = value;}
        }
        public string DetailAddress
        {
           get { return _detailAddress; }
           set { _detailAddress = value;}
        }
        public string Maintain_Money
        {
           get { return _maintain_Money; }
           set { _maintain_Money = value;}
        }
        public string Maintain_Sys
        {
           get { return _maintain_Sys; }
           set { _maintain_Sys = value;}
        }
        public string Maintain_Fix
        {
           get { return _maintain_Fix; }
           set { _maintain_Fix = value;}
        }
        public string BuyCaseNo
        {
           get { return _buyCaseNo; }
           set { _buyCaseNo = value;}
        }
        public DateTime BuyDate
        {
           get { return _buyDate; }
           set { _buyDate = value;}
        }
        public DateTime OpenDate
        {
           get { return _openDate; }
           set { _openDate = value;}
        }
        public DateTime InstallDate
        {
           get { return _installDate; }
           set { _installDate = value;}
        }
        public string Memo
        {
           get { return _memo; }
           set { _memo = value;}
        }
        public string LoginGorupID
        {
           get { return _loginGorupID; }
           set { _loginGorupID = value;}
        }

        #endregion

        #region Ctor/init
        public MCData() {}
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
                cmd.CommandText = "INSERT INTO MCData (MCID, GroupID, Model, MCNo, MaintainID, IPAddress, PU, LU, DevicePoint, SourceConnection, ATMBandWidth, SDGatewayID, BandWidth, MCAccountID, MCFunc, SystemOS, OpenTime, bPoint, DetailAddress, Maintain_Money, Maintain_Sys, Maintain_Fix, BuyCaseNo, BuyDate, OpenDate, InstallDate, Memo, LoginGorupID) VALUES (@MCID_PARAMS, @GroupID_PARAMS, @Model_PARAMS, @MCNo_PARAMS, @MaintainID_PARAMS, @IPAddress_PARAMS, @PU_PARAMS, @LU_PARAMS, @DevicePoint_PARAMS, @SourceConnection_PARAMS, @ATMBandWidth_PARAMS, @SDGatewayID_PARAMS, @BandWidth_PARAMS, @MCAccountID_PARAMS, @MCFunc_PARAMS, @SystemOS_PARAMS, @OpenTime_PARAMS, @bPoint_PARAMS, @DetailAddress_PARAMS, @Maintain_Money_PARAMS, @Maintain_Sys_PARAMS, @Maintain_Fix_PARAMS, @BuyCaseNo_PARAMS, @BuyDate_PARAMS, @OpenDate_PARAMS, @InstallDate_PARAMS, @Memo_PARAMS, @LoginGorupID_PARAMS)";
                                cmd.Parameters.AddWithValue("@MCID_PARAM", _mCID);
                cmd.Parameters.AddWithValue("@GroupID_PARAM", _groupID);
                cmd.Parameters.AddWithValue("@Model_PARAM", _model);
                cmd.Parameters.AddWithValue("@MCNo_PARAM", _mCNo);
                cmd.Parameters.AddWithValue("@MaintainID_PARAM", _maintainID);
                cmd.Parameters.AddWithValue("@IPAddress_PARAM", _iPAddress);
                cmd.Parameters.AddWithValue("@PU_PARAM", _pU);
                cmd.Parameters.AddWithValue("@LU_PARAM", _lU);
                cmd.Parameters.AddWithValue("@DevicePoint_PARAM", _devicePoint);
                cmd.Parameters.AddWithValue("@SourceConnection_PARAM", _sourceConnection);
                cmd.Parameters.AddWithValue("@ATMBandWidth_PARAM", _aTMBandWidth);
                cmd.Parameters.AddWithValue("@SDGatewayID_PARAM", _sDGatewayID);
                cmd.Parameters.AddWithValue("@BandWidth_PARAM", _bandWidth);
                cmd.Parameters.AddWithValue("@MCAccountID_PARAM", _mCAccountID);
                cmd.Parameters.AddWithValue("@MCFunc_PARAM", _mCFunc);
                cmd.Parameters.AddWithValue("@SystemOS_PARAM", _systemOS);
                cmd.Parameters.AddWithValue("@OpenTime_PARAM", _openTime);
                cmd.Parameters.AddWithValue("@bPoint_PARAM", _bPoint);
                cmd.Parameters.AddWithValue("@DetailAddress_PARAM", _detailAddress);
                cmd.Parameters.AddWithValue("@Maintain_Money_PARAM", _maintain_Money);
                cmd.Parameters.AddWithValue("@Maintain_Sys_PARAM", _maintain_Sys);
                cmd.Parameters.AddWithValue("@Maintain_Fix_PARAM", _maintain_Fix);
                cmd.Parameters.AddWithValue("@BuyCaseNo_PARAM", _buyCaseNo);
                cmd.Parameters.AddWithValue("@BuyDate_PARAM", _buyDate);
                cmd.Parameters.AddWithValue("@OpenDate_PARAM", _openDate);
                cmd.Parameters.AddWithValue("@InstallDate_PARAM", _installDate);
                cmd.Parameters.AddWithValue("@Memo_PARAM", _memo);
                cmd.Parameters.AddWithValue("@LoginGorupID_PARAM", _loginGorupID);

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
                cmd.CommandText = "SELECT MCID, GroupID, Model, MCNo, MaintainID, IPAddress, PU, LU, DevicePoint, SourceConnection, ATMBandWidth, SDGatewayID, BandWidth, MCAccountID, MCFunc, SystemOS, OpenTime, bPoint, DetailAddress, Maintain_Money, Maintain_Sys, Maintain_Fix, BuyCaseNo, BuyDate, OpenDate, InstallDate, Memo, LoginGorupID FROM MCData WHERE ";
                
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                if (true == reader.Read())
                {
                                    _mCID = reader.GetString(0);
                _groupID = reader.GetString(1);
                _model = reader.GetString(2);
                _mCNo = reader.GetString(3);
                _maintainID = reader.GetString(4);
                _iPAddress = reader.GetString(5);
                _pU = reader.GetString(6);
                _lU = reader.GetString(7);
                _devicePoint = reader.GetString(8);
                _sourceConnection = reader.GetString(9);
                _aTMBandWidth = reader.GetInt32(10);
                _sDGatewayID = reader.GetString(11);
                _bandWidth = reader.GetInt32(12);
                _mCAccountID = reader.GetString(13);
                _mCFunc = reader.GetInt16(14);
                _systemOS = reader.GetInt16(15);
                _openTime = reader.GetString(16);
                _bPoint = reader.GetString(17);
                _detailAddress = reader.GetString(18);
                _maintain_Money = reader.GetString(19);
                _maintain_Sys = reader.GetString(20);
                _maintain_Fix = reader.GetString(21);
                _buyCaseNo = reader.GetString(22);
                _buyDate = reader.GetDateTime(23);
                _openDate = reader.GetDateTime(24);
                _installDate = reader.GetDateTime(25);
                _memo = reader.GetString(26);
                _loginGorupID = reader.GetString(27);

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
                cmd.CommandText = "UPDATE MCData SET MCID=@MCID_PARAMS, GroupID=@GroupID_PARAMS, Model=@Model_PARAMS, MCNo=@MCNo_PARAMS, MaintainID=@MaintainID_PARAMS, IPAddress=@IPAddress_PARAMS, PU=@PU_PARAMS, LU=@LU_PARAMS, DevicePoint=@DevicePoint_PARAMS, SourceConnection=@SourceConnection_PARAMS, ATMBandWidth=@ATMBandWidth_PARAMS, SDGatewayID=@SDGatewayID_PARAMS, BandWidth=@BandWidth_PARAMS, MCAccountID=@MCAccountID_PARAMS, MCFunc=@MCFunc_PARAMS, SystemOS=@SystemOS_PARAMS, OpenTime=@OpenTime_PARAMS, bPoint=@bPoint_PARAMS, DetailAddress=@DetailAddress_PARAMS, Maintain_Money=@Maintain_Money_PARAMS, Maintain_Sys=@Maintain_Sys_PARAMS, Maintain_Fix=@Maintain_Fix_PARAMS, BuyCaseNo=@BuyCaseNo_PARAMS, BuyDate=@BuyDate_PARAMS, OpenDate=@OpenDate_PARAMS, InstallDate=@InstallDate_PARAMS, Memo=@Memo_PARAMS, LoginGorupID=@LoginGorupID_PARAMS WHERE ";
                                cmd.Parameters.AddWithValue("@MCID_PARAM", _mCID);
                cmd.Parameters.AddWithValue("@GroupID_PARAM", _groupID);
                cmd.Parameters.AddWithValue("@Model_PARAM", _model);
                cmd.Parameters.AddWithValue("@MCNo_PARAM", _mCNo);
                cmd.Parameters.AddWithValue("@MaintainID_PARAM", _maintainID);
                cmd.Parameters.AddWithValue("@IPAddress_PARAM", _iPAddress);
                cmd.Parameters.AddWithValue("@PU_PARAM", _pU);
                cmd.Parameters.AddWithValue("@LU_PARAM", _lU);
                cmd.Parameters.AddWithValue("@DevicePoint_PARAM", _devicePoint);
                cmd.Parameters.AddWithValue("@SourceConnection_PARAM", _sourceConnection);
                cmd.Parameters.AddWithValue("@ATMBandWidth_PARAM", _aTMBandWidth);
                cmd.Parameters.AddWithValue("@SDGatewayID_PARAM", _sDGatewayID);
                cmd.Parameters.AddWithValue("@BandWidth_PARAM", _bandWidth);
                cmd.Parameters.AddWithValue("@MCAccountID_PARAM", _mCAccountID);
                cmd.Parameters.AddWithValue("@MCFunc_PARAM", _mCFunc);
                cmd.Parameters.AddWithValue("@SystemOS_PARAM", _systemOS);
                cmd.Parameters.AddWithValue("@OpenTime_PARAM", _openTime);
                cmd.Parameters.AddWithValue("@bPoint_PARAM", _bPoint);
                cmd.Parameters.AddWithValue("@DetailAddress_PARAM", _detailAddress);
                cmd.Parameters.AddWithValue("@Maintain_Money_PARAM", _maintain_Money);
                cmd.Parameters.AddWithValue("@Maintain_Sys_PARAM", _maintain_Sys);
                cmd.Parameters.AddWithValue("@Maintain_Fix_PARAM", _maintain_Fix);
                cmd.Parameters.AddWithValue("@BuyCaseNo_PARAM", _buyCaseNo);
                cmd.Parameters.AddWithValue("@BuyDate_PARAM", _buyDate);
                cmd.Parameters.AddWithValue("@OpenDate_PARAM", _openDate);
                cmd.Parameters.AddWithValue("@InstallDate_PARAM", _installDate);
                cmd.Parameters.AddWithValue("@Memo_PARAM", _memo);
                cmd.Parameters.AddWithValue("@LoginGorupID_PARAM", _loginGorupID);

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
                cmd.CommandText = "DELETE FROM MCData WHERE ";

                cmd.ExecuteNonQuery();

            }

        }



        #endregion

    }
}

