using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MDS.Database;

public partial class DMSMainManage_UserInfo_RoleManage : BasePage
{
    string myGroupID = "";

    string myTargerRoleID = "";
    string myTargerRoleName = "";
    
    Database db = new Database();
    DataTable dt = new DataTable();
    int nRet = -1;
    string outMsg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        /*取得使用者GroupID*/
        myGroupID = Session["ParentGroupID"] == null ?"":Session["ParentGroupID"].ToString();

        /*取得選取的角色權限等級*/
        if (!string.IsNullOrEmpty(Request.QueryString["TargerRoleID"]))
            myTargerRoleID = Request.QueryString["TargerRoleID"];
        /*取得選取的角色名稱*/
        if (!string.IsNullOrEmpty(Request.QueryString["TargerRoleName"]))
            myTargerRoleName = Request.QueryString["TargerRoleName"];
        
        /*取得權限清單資料SQL*/
        string StrSQL = "DECLARE @tmpSysModID varchar(50) /*暫存ModID用來判斷用*/ " +
            "DECLARE @SysModID varchar(50) " +
            "DECLARE @ModuleDesc nvarchar(50) " +
            "DECLARE @SysFuncID varchar(50) " +
            "DECLARE @FunctionDesc nvarchar(100) " +
            "DECLARE @tmpTable TABLE( " +
	            "thisType int, /*1: ModID 2:FuncID 3:ActionID */ " +
	            "thisGUID varchar(50), " +
	            "thisName nvarchar(100) " +
            ") " +
            "SET @tmpSysModID = '' " +
            "DECLARE CurTemp CURSOR FOR " +
               "SELECT tblA.SysModID, SystemModule.ModuleDesc, tblA.SysFuncID, SystemFunction.FunctionDesc " +
               "FROM DMSRoleFunction AS tblA " +
               "INNER JOIN SystemModule ON tblA.SysModID = SystemModule.SysModID AND SystemModule.iDisplay = 1 " +
               "INNER JOIN SystemFunction ON tblA.SysFuncID = SystemFunction.SysFuncID AND SystemFunction.iDisplay = 1 " +
               "WHERE tblA.DMSRoleID =@myTargerRoleID_1 " +
               "ORDER BY SystemModule.iOrder, SystemModule.SysModID, SystemFunction.iOrder " +
            "OPEN CurTemp " + 
            "FETCH NEXT FROM CurTemp INTO @SysModID, @ModuleDesc, @SysFuncID, @FunctionDesc " +
            "WHILE (@@Fetch_Status = 0 ) " +
            "BEGIN " +
	            "/*第一次取得SysModID*/ " +
	            "IF @tmpSysModID <> @SysModID BEGIN " +
		            "SET @tmpSysModID = @SysModID " +
		            "INSERT INTO @tmpTable VALUES(1, @SysModID, '<b>' + @ModuleDesc + '</b>') " +
	            "END " +
	            "/*取得這筆SysFuncID*/ " +
                "INSERT INTO @tmpTable VALUES(2, @SysFuncID, '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + @FunctionDesc) " +
	            "/*取得這筆SysFuncID的SysActionID*/ " +
	            "INSERT INTO @tmpTable " +
		            "SELECT 3 " +
			            ",tblA.SysActionID " +
                        ",'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;>&nbsp;&nbsp;' + SystemAction.ActionDesc " +
		            "FROM DMSRoleAction AS tblA " +
		            "INNER JOIN SystemAction ON tblA.SysActionID = SystemAction.SysActionID " +
                    "WHERE tblA.SysFuncID = @SysFuncID AND DMSRoleID =@myTargerRoleID_2 " +
            "FETCH NEXT FROM CurTemp INTO @SysModID, @ModuleDesc, @SysFuncID, @FunctionDesc " +
            "END " +
            "CLOSE CurTemp " +
            "DEALLOCATE CurTemp " +

            "/*輸出結果*/ " +
            "SELECT thisType, thisGUID, thisName FROM @tmpTable ";

        //Response.Write(StrSQL);
        //Response.End();
        
        /*產出權限清單html START*/
        string RoleList_html = "<table  class='table table-striped table-bordered table-hover' cellspacing='0' width='100%'>";
        RoleList_html += "<thead><tr>";
        RoleList_html += "<th  >" + ParseWording("B0016") + "</td>";
        RoleList_html += "<th >" + myTargerRoleName + "</td>";
        RoleList_html += "</tr></thead>";



        /*查詢DB*/
        System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());
        SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@myTargerRoleID_1",(myTargerRoleID)));
        SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@myTargerRoleID_2", (myTargerRoleID)));



        /*連線DB*/
        nRet = db.DBConnect();
        if (nRet == 0)
        {
            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);

            outMsg = db.outMsg;
            if (nRet == 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    RoleList_html += "<tr >";

                    RoleList_html += "<td >";
                    RoleList_html += dt.Rows[i]["thisName"].ToString();
                    RoleList_html += "</td>";

                    RoleList_html += "<td >";
                    if (dt.Rows[i]["thisType"].ToString() == "1")
                        RoleList_html += "";
                    if (dt.Rows[i]["thisType"].ToString() == "2")
                        RoleList_html += "<input type=\"checkbox\" id=\"FunctionCheckbox\" value=\"" +(dt.Rows[i]["thisGUID"].ToString()) + "\" checked>";
                    if (dt.Rows[i]["thisType"].ToString() == "3")
                        RoleList_html += "<input type=\"checkbox\" id=\"ActionCheckbox\" value=\"" +(dt.Rows[i]["thisGUID"].ToString()) + "\" checked>";

                    RoleList_html += "</tr>";
                }
            }
            else
            {
                Response.Write("nRet-> " +MDS.Utility.NUtility.HtmlEncode(nRet.ToString())+ "<BR>");
                Response.Write("outMsg-> " + MDS.Utility.NUtility.HtmlEncode(outMsg) + "<BR>");
            }

            db.DBDisconnect();
        }
        RoleList_html += "<tfoot><tr>";
        RoleList_html += "<th >" + ParseWording("B0016") + "</td>";
        RoleList_html += "<th  >" +(myTargerRoleName) + "</td>";
        RoleList_html += "</tr></tfoot>";
        RoleList_html += "</table>";
        /*產出權限清單html END*/

        /*寫入權限清單html*/
        RoleList.InnerHtml = RoleList_html;
    }
}
