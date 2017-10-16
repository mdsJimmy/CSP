using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using MDS.Database;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

public partial class ChangePassword_ChangePassword_edt : BasePage 
{
    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected string myUserID = "";
    protected string strChangePW = "";
    protected static string strStatus = "";
    protected string strPwType = "";
    protected string strSameIDPW = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        /*取得使用者UserID*/
        myUserID = Session["UserID"].ToString();
        
        /*取得使用者資料*/
        GetUserData();
    }


    /// <summary>
    /// 取得使用者(自己)資料;
    /// </summary>
    /// <returns></returns>
    private void GetUserData()
    {
       
        Database db = new Database();
        DataTable dt = new DataTable();

        int nRet = -1;

        /*Get GetUserData START*/
        string StrSQL = @"SELECT tblA.GroupID, tblA.GroupID + ' ' + SecurityGroup.GroupName AS GroupInfo  
            , tblA.Name + '(' + tblA.AccountID +')' AS UserInfo  
            , CONVERT(varchar, tblA.[Password]) AS [Password]  
            , tblA.PWType as PWType  
            , case when tblA.PWLastUpdateTime IS NULL  then 'true' else 'false' end as isUpPwTime
			
            , case when DateDiff(Day,PWLastUpdateTime,getdate())>= @password_changeDays then 'true' else 'false' end as changePW  
			,tblA.PWLastUpdateTime
            FROM SecurityUserAccount AS tblA  
            INNER JOIN SecurityGroup ON tblA.GroupID = SecurityGroup.GroupID  
            WHERE tblA.AccountID =@AccountID ";

        /*連線DB*/
        nRet = db.DBConnect();
        if (nRet == 0)
        {
            
            SqlCommand SqlCom = new SqlCommand(StrSQL, db.getOcnn());
            SqlCom.Parameters.Add(new SqlParameter("@password_changeDays", Int32.Parse( ConfigurationManager.AppSettings["p000000d_changeDays"])));
            SqlCom.Parameters.Add(new SqlParameter("@AccountID", Session["UserID"].ToString()));
            
            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);

            if (nRet == 0)
            {
                if (dt.Rows.Count == 1)
                {
                    oGroupInfo.Text = dt.Rows[0]["GroupInfo"].ToString();
                    oUserInfo.Text = dt.Rows[0]["UserInfo"].ToString();
                    oHiddenOldPassword.Value = dt.Rows[0]["Password"].ToString();
                    strChangePW = dt.Rows[0]["changePW"].ToString();
                    strPwType = dt.Rows[0]["PWType"].ToString();
                    strSameIDPW = dt.Rows[0]["isUpPwTime"].ToString();

                }
                else
                {
                    /*查無此人*/
                    nRet = -3;
                }
                if (strPwType.Equals("1") && strChangePW.Equals("true"))
                {
                    //超過90天未更改密碼
                    strStatus = "-7";
                }
                else if (strSameIDPW.Equals("true") && !Session["UserID"].ToString().Equals("PCALTOP"))
                {
                    //第一次登入，密碼要改
                    strStatus = "-8";
                }
                else {
                    strStatus = "0";
                }
            }
        }

        /*有錯誤則跳出警示視窗*/
        if (nRet != 0) {
            MessageBox("查無此資料 代碼："+nRet);
            Response.Redirect("~/Verify.aspx");
        }
        /*Get GetUserData END*/
    }

    #region 原始 code
    ///// <summary>
    ///// 修改密碼
    ///// </summary>
    ///// <param name="NewPassword">新密碼</param>
    ///// <param name="AccountID">要修改的帳號</param>
    ///// <returns></returns>
    //[System.Web.Services.WebMethod]
    //public static CReturnData EdtProcess(string NewPassword, string AccountID)
    //{
    //    HttpContext.Current.Session["SHOWSUCCESS"] = "0";
    //    CReturnData myData = new CReturnData();

    //    Database db = new Database();
    //    DataTable dt = new DataTable();
    //    SqlCommand cmd = null;
    //    if (!isValidPassword(regexdPassword(NewPassword)))
    //    {
    //        myData.nRet = 820;
    //        myData.outMsg = "您使用的新密碼不符合系統密碼編碼原則,請重新選擇適合的新密碼不可含有特殊符號!";
    //    }
    //    else
    //    {
    //        /*連線DB*/
    //        myData.nRet = db.DBConnect();
    //        myData.outMsg = db.outMsg;

    //        if (myData.nRet == 0)
    //        {
    //            string StrSQL = "UPDATE SecurityUserAccount SET [Password] = CONVERT(varbinary,@Password),PWLastUpdateTime =GETDATE() WHERE AccountID =@AccountID";

    //            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());

    //            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("Password", System.Data.SqlDbType.VarBinary));
    //            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("AccountID", System.Data.SqlDbType.VarChar));


    //            SqlCom.Parameters["Password"].Value = Encoding.UTF8.GetBytes(NewPassword);
    //            SqlCom.Parameters["AccountID"].Value = AccountID;

    //            /*寫入DB*/

    //            int cnt = -1;
    //            myData.nRet = db.ExecNonQuerySQLCommand(SqlCom, ref cnt);

    //            myData.outMsg = db.outMsg;
    //            //改完密碼要變成不用改密碼把CHANGE_PASSWORD=0
    //            HttpContext.Current.Session["CHANGE_PASSWORD"] = "0";
    //            HttpContext.Current.Session["SHOWSUCCESS"] = "1";
    //            db.DBDisconnect();
    //        }
    //    }

    //    return myData;
    //}
    #endregion
    #region  code2   
    /// <summary>
    /// 修改密碼
    /// </summary>
    /// <param name="NewPassword">新密碼</param>
    /// <param name="AccountID">要修改的帳號</param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static CReturnData EdtProcess(string NewPassword, string AccountID)
    {
        HttpContext.Current.Session["SHOWSUCCESS"] = "0";
        CReturnData myData = new CReturnData();
        string s = "";

        Database db = new Database();
        DataTable dt = new DataTable();

        SqlCommand cmd = null;
        //if (!isValidPassword(regexdPassword(NewPassword)))
        //{
        //    myData.nRet = 820;
        //    myData.outMsg = "您使用的新密碼不符合系統密碼編碼原則,請重新選擇適合的新密碼不可含有特殊符號!";
        //}
        //else
        //{
            /*連線DB*/
            myData.nRet = db.DBConnect();
            myData.outMsg = db.outMsg;

            if (myData.nRet == 0)
            {
               try
                {

                    using (cmd = new SqlCommand())
                    {
                        cmd.Connection = db.getOcnn();
                        cmd.CommandText = "select CONVERT(varchar(20),bPassword)Password  from SecurityUserPwd where AccountID = @AccountID order by ldate desc";

                        cmd.Parameters.Add(new SqlParameter("Password", Encoding.UTF8.GetBytes(NewPassword)));
                        cmd.Parameters.Add(new SqlParameter("AccountID", AccountID));

                        myData.nRet = db.ExecQuerySQLCommand(cmd, ref dt);
                        myData.outMsg = db.outMsg;
                        cmd.Parameters.Clear();
                        int rownum = dt.Rows.Count;
                        if (rownum <=5)
                        {
                            for (int i = 0; i < rownum; i++)
                            {
                                if (dt.Rows[i][0].ToString().Equals(NewPassword)) {
                                    myData.nRet = -11;
                                    myData.outMsg = "密碼不得與前五次相同";
                                    return myData;
                                }
                            }
                        }
                        else {
                            strStatus = "-10";
                        }
                        
                        

                        using (cmd.Transaction = db.getOcnn().BeginTransaction())
                        {
                            

                           
                           




                            cmd.CommandText = "UPDATE SecurityUserAccount SET [Password] = CONVERT(varbinary,@Password),PWLastUpdateTime =GETDATE() WHERE AccountID =@AccountID";
                            cmd.Parameters.Add(new SqlParameter("Password", Encoding.UTF8.GetBytes(NewPassword)));
                            cmd.Parameters.Add(new SqlParameter("AccountID", AccountID));


                            int cnt = -1;
                            myData.nRet = db.ExecNonQuerySQLCommand(cmd, ref cnt);
                            myData.outMsg = db.outMsg;
                            cmd.Parameters.Clear();
                            //若要看 sql 是否有錯誤 將整句放在 sql 的程式 run 就懂了
                            cmd.CommandText = @"/*改寫使用者 kind 欄位 值 1:曾經使用過的 0:現正使用*/
                                                update SecurityUserPwd set kind=1 where kind=0 and AccountID =@AccountID;
                                                /*查詢該使用者是否有更正五次的記錄*/
                                                DECLARE @isCrctns smallint;
                                                set @isCrctns=  (select case when count(AccountID)>=5 then 0 else 1 end isCrctns from SecurityUserPwd where AccountID =@AccountID  )
                                                if @isCrctns=0 /*若該使用者更正五次*/
                                                    begin
                                                    delete from SecurityUserPwd where Ldate= (select min(Ldate) from SecurityUserPwd where AccountID =@AccountID )
                                                    /*寫入目前更正的密碼*/
                                                    insert into SecurityUserPwd 
                                                    ( AccountID, bPassword, iOrder,Kind,Ldate) values (@AccountID, @Password,1,0 ,getDate());
                                                    end
                                                ELSE/*若該使用者未更正五次*/
                                                    begin
                                                    insert into SecurityUserPwd 
                                                    ( AccountID, bPassword, iOrder,Kind,Ldate) values ( @AccountID, @Password,1,0 ,getDate());
                                                end";

                            cmd.Parameters.Add(new SqlParameter("@Password", Encoding.UTF8.GetBytes(NewPassword)));
                            cmd.Parameters.Add(new SqlParameter("@AccountID", AccountID));

                            myData.nRet = db.ExecNonQuerySQLCommand(cmd, ref cnt);
                            myData.outMsg = db.outMsg;

                            cmd.Transaction.Commit();

                        }



                    }
                }
                catch (Exception ex )
                {
                    cmd.Transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    HttpContext.Current.Session["CHANGE_PASSWORD"] = "0";
                    HttpContext.Current.Session["SHOWSUCCESS"] = "1";
                    db.DBDisconnect();
                }








            }
        //}end if 驗證特殊符號
       
        return myData;
    }
    #endregion



    public static bool isValidPassword(string pwd)
    {
        // byte[] pwd_byte = new byte[pwd.Length * sizeof(char)];

        // byte[] bytes = new byte[str.Length * sizeof(char)];
        //  System.Buffer.BlockCopy(pwd.ToCharArray(), 0, pwd_byte, 0, pwdLength);
        string strPassword_char = System.Configuration.ConfigurationManager.AppSettings["p000000d_char"];
        Char[] pwd_byte = pwd.ToCharArray();
        int pwdLength = pwd_byte.Length;
        //密碼 最少八位 
        if (pwdLength < Int32.Parse(strPassword_char) ) return false;

        int isAllTheSame = 0;
        int isAsc = 0;
        int isDsc = 0;

        for (int i = 0; i < pwdLength; i++)
        {


            bool isValidChar = false;
            byte result = Convert.ToByte(pwd_byte[i]);
             if (result >= 65 && result <= 90)
                {
                    isValidChar = true;
                }

                if (!isValidChar)
                {
                    if (result >= 97 && result <= 122)
                    {
                        isValidChar = true;
                    }
                }

                if (!isValidChar)
                {
                    if (result >= 48 && result <= 57)
                    {
                        isValidChar = true;
                    }
                }

                if (!isValidChar) return false;
            

            if (i > 0)
            {
                byte result_up = Convert.ToByte(pwd_byte[i-1]);

                if (result == result_up)
                {
                      isAllTheSame++;
                }
                else if (result == result_up + 1)
                {
                      isAsc++;
                }
                else if (result == result_up - 1)
                {
                      isDsc++;
                }
            }
        }

      

        pwdLength--;
        if (isAllTheSame >= pwdLength || isAsc >= pwdLength || isDsc >= pwdLength) 
            return false;
        else
            return true;


    }

    public static string regexdPassword(string pwd)
    {


        int i = 0;
        Regex regex = new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,50}$");
        MatchCollection matches = regex.Matches(pwd);
        if (matches.Count > 0)
        {

            foreach (Match match in matches)
            {
                // 將 Match 內所有值的集合傳給 GroupCollection groups
                GroupCollection groups = match.Groups;
                // 印出 Group 內 word 值
            }

            return pwd;
        }
        else
        {
            return "";
        }


      


    }



    
}

