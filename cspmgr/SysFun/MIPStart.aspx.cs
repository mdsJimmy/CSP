using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MDS.Database;
using System.Net;
public partial class SysFun_MIPStart : System.Web.UI.Page
{
    public string DefaultPageLink = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string SecureKey = "";
        string DMSRoleIDRight = "";
        string AccountID = "";


        DataTable dt = new DataTable();
        DataTable dtModule = new DataTable();
        DataTable dtFuncionRole = new DataTable();
        DataTable dtActionRole = new DataTable();

        int nRet = -1;
        string outMsg = "";
        int nAffectedRowCount = 0;
        string strSQL = "";

        SecureKey = Request.QueryString["SecureKey"];

        //xss:設置白名單，不是數字的SecureKey就設定成空白
        if (!MDS.Utility.NUtility.IsNumber(SecureKey))
        {
            SecureKey = "";
        }


        //SecureKey = "test";
        if (!string.IsNullOrEmpty(SecureKey))
        {
            MDS.Database.Database DmsSql = new Database();
            nRet = DmsSql.DBConnect();
            try
            {
                if (nRet == 0)
                {
                    ////取得預設頁面 START
                    //DefaultPageLink = "Right.aspx";
                    //strSQL = "SELECT Top 1 * FROM SystemModule Where (isDefault = 1) AND (PageLink <> '') Order By iOrder";
                    //nRet = DmsSql.ExecQuerySQLCommand(strSQL, ref dtModule);
                    //if (dtModule.Rows.Count > 0)
                    //{
                    //    DefaultPageLink = dtModule.Rows[0]["PageLink"].ToString().Trim();
                    //}
                    //dtModule.Reset();
                    ////取得預設頁面 END

                    //權限Session START
                    strSQL = "SELECT SecureKey, AccounID, LoginType, IPAddress, CreateDatetime, UsedDatetime, IsUsed FROM SecurityUserAccount_SecureKey Where SecureKey =@SecureKey";

                    //一律使用參數式SQL指令
                    System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSQL, DmsSql.getOcnn());
                    SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("SecureKey", MDS.Utility.NUtility.checkString(SecureKey)));
                    nRet = DmsSql.ExecQuerySQLCommand(SqlCom, ref dt);


                    if (dt.Rows.Count > 0)
                    {
                        strSQL = "DELETE FROM SecurityUserAccount_SecureKey Where SecureKey =@SecureKey";

                        //一律使用參數式SQL指令
                        System.Data.SqlClient.SqlCommand SqlCom2 = new System.Data.SqlClient.SqlCommand(strSQL, DmsSql.getOcnn());
                        SqlCom2.Parameters.Add(new System.Data.SqlClient.SqlParameter("SecureKey", MDS.Utility.NUtility.checkString(SecureKey)));
                        nRet = DmsSql.ExecNonQuerySQLCommand(SqlCom2, ref nAffectedRowCount);


                        AccountID = dt.Rows[0]["AccounID"].ToString().Trim();
                        dt.Reset();

                        strSQL = "SELECT SecurityUserAccount.GroupID AS ParentGroupID, " +
                                "SecurityUserAccount.AccountID, " +
                                "CONVERT(varchar(30), SecurityUserAccount.Password) AS 'Password', " +
                                "SecurityUserAccount.PWLastUpdateTime, " +
                                "SecurityUserAccount.Name, " +
                                "SecurityUserAccount.cRoleID AS DMSRoleID, " +
                                "SecurityUserAccount.PWType " +
                            "FROM SecurityUserAccount " +
                            "INNER JOIN DMSRole ON DMSRole.DMSRoleID = SecurityUserAccount.cRoleID " +
                            "WHERE SecurityUserAccount.AccountID =@AccountID";

                        //一律使用參數式SQL指令
                        System.Data.SqlClient.SqlCommand SqlCom3 = new System.Data.SqlClient.SqlCommand(strSQL, DmsSql.getOcnn());
                        SqlCom3.Parameters.Add(new System.Data.SqlClient.SqlParameter("AccountID", AccountID));
                        nRet = DmsSql.ExecQuerySQLCommand(SqlCom3, ref dt);

                        if (dt.Rows.Count > 0)
                        {
                            strSQL = "SELECT * FROM SecurityUserAccount_FunctionRole Where AccountID =@AccountID";

                            //一律使用參數式SQL指令
                            System.Data.SqlClient.SqlCommand SqlCom4 = new System.Data.SqlClient.SqlCommand(strSQL, DmsSql.getOcnn());
                            SqlCom4.Parameters.Add(new System.Data.SqlClient.SqlParameter("AccountID", AccountID));
                            nRet = DmsSql.ExecQuerySQLCommand(SqlCom4, ref dtFuncionRole);

                            if (dtFuncionRole.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtFuncionRole.Rows.Count; i++)
                                {
                                    DMSRoleIDRight = DMSRoleIDRight + dtFuncionRole.Rows[i]["SysFuncID"].ToString().Trim() + "||";
                                }
                            }
                            dtFuncionRole.Reset();

                            strSQL = "SELECT * FROM SecurityUserAccount_ActionRole Where AccountID=@AccountID";

                            //一律使用參數式SQL指令
                            System.Data.SqlClient.SqlCommand SqlCom5 = new System.Data.SqlClient.SqlCommand(strSQL, DmsSql.getOcnn());
                            SqlCom5.Parameters.Add(new System.Data.SqlClient.SqlParameter("AccountID", AccountID));
                            nRet = DmsSql.ExecQuerySQLCommand(SqlCom5, ref dtActionRole);
                           
                            if (dtActionRole.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtActionRole.Rows.Count; i++)
                                {
                                    DMSRoleIDRight = DMSRoleIDRight + dtActionRole.Rows[i]["SysActionID"].ToString().Trim() + "||";
                                }
                            }
                            dtActionRole.Reset();

                            if (DMSRoleIDRight != "")
                            {
                                Session["ParentGroupID"] = dt.Rows[0]["ParentGroupID"].ToString().Trim();
                                Session["UserID"] = dt.Rows[0]["AccountID"].ToString().Trim();
                                Session["Password"] = dt.Rows[0]["Password"].ToString().Trim();
                                Session["Name"] = dt.Rows[0]["Name"].ToString().Trim();
                                Session["DMSRoleID"] = dt.Rows[0]["DMSRoleID"].ToString().Trim();
                                Session["DMSRoleIDRight"] = DMSRoleIDRight;

                                if (Session["CHANGE_PASSWORD"].ToString() == "1")
                                {
                                    Response.Redirect("~/PersonalInfoManage/ChangePassword/ChangePassword_edt.aspx");
                                }

                            }
                            else
                            {
                                Response.Write("System Role Error.");
                                Response.End();
                            }
                        }
                    }
                    //// //權限Session END
                    ////// DmsSql.DBDisconnect();
                }
                else
                {
                    Response.Write("Connecting Database Server Failed.");
                    Response.End();
                }
            }
            finally
            {
                //權限Session END
                DmsSql.DBDisconnect();
            }
            //  }
            //  else
            //  {
            //      Response.Write("Secure Key is Empty.");
            //      Response.End();
        }
        dt.Dispose();
        dtModule.Dispose();
        dtFuncionRole.Dispose();
        dtActionRole.Dispose();
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {

    }

}