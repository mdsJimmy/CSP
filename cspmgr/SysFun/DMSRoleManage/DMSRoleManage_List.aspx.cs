using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MDS.Database;
using System.Data;

public partial class SysFun_DMSRoleManage_DMSRoleManage_List : BasePage
{
    public string IsAction1 = "0"; /*是否有功能頁籤的權限*/
    public string IsAction2 = "0"; /*是否有角色頁籤的權限*/
    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        //確認目前登入的使用者是否有功能頁籤的權限(預設頁籤頁面)
        if (CheckUserRight("62924E38-7902-4746-8B61-ACF2B3F4C337") == true)
            IsAction1 = "1";
        
        //確認目前登入的使用者是否有角色頁籤的權限
        if (CheckUserRight("4886277A-AB21-4701-BF94-B39E2CC54E19") == true)
            IsAction2 = "1";

        /*取得功能管理介面清單;*/
        GetRoleManageList();

        /*取得角色管理介面清單;*/
        GetRoleRelationList();
    }


    /// <summary>
    /// 取得功能管理介面清單;
    /// </summary>
    protected void GetRoleManageList() 
    {
        Database db = new Database();

        DataTable dt = new DataTable();
        DataTable dt_DMSRole = new DataTable();
        DataTable dt_DMSRoleDetail = new DataTable();
        
        int nRet = -1;
        int i, j;

        string HTML_TableRoleManage = "<table id=\"TableRoleManage\" class='table table-striped table-bordered table-hover' cellspacing='0'   cellpadding='0' >";
        string HTML_TableRoleManage_Title = "<tr><td class=\"ColumnTitle\">" + ParseWording("D0030") + "</td>";
        string HTML_TableRoleManage_Body = "";
        
        /*連線DB*/
        nRet = db.DBConnect();
        if (nRet == 0)
        {
            /*先取到權限表(這邊沒設定權限也不會error所以不判nRet)*/
            string strSQL = "SELECT DMSRoleID, DMSRoleName FROM DMSRole ";

            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSQL, db.getOcnn());
            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt_DMSRole);


            /*計算平均欄寬(多給第一個欄位5%的空間)*/
            int Field_Width = (95 / (dt_DMSRole.Rows.Count + 1));
            /*產生標提列第二~N個<td>, 以權限表區分*/
            for (i = 0; i < dt_DMSRole.Rows.Count; i++)
                HTML_TableRoleManage_Title += "<td class=\"ColumnTitle\" style=\"background-color:#ffffee;width:" + Field_Width + "%;\">" + dt_DMSRole.Rows[i]["DMSRoleName"].ToString() + "</td>";
            
            /*再取到權限表各權限細項(這邊沒設定權限也不會error所以不判nRet)*/
            strSQL = "SELECT DMSRoleID, 2 AS thisType, SysFuncID AS thisGUID FROM DMSRoleFunction " +
                "UNION ALL " +
                "SELECT DMSRoleID, 3 AS thisType, SysActionID AS thisGUID FROM DMSRoleAction ";

            System.Data.SqlClient.SqlCommand SqlCom2 = new System.Data.SqlClient.SqlCommand(strSQL, db.getOcnn());

            nRet = db.ExecQuerySQLCommand(SqlCom2, ref dt_DMSRoleDetail);
                
            /*再取權限總清單(這邊沒設定權限也不會error所以不判nRet)*/
            strSQL = "DECLARE @tmpSysModID varchar(50) /*暫存ModID用來判斷用*/ " +
                "DECLARE @myAction varchar(8000) /*暫存Action字串*/ " +
                "DECLARE @SysModID varchar(50) /*CURSOR暫存變數*/ " +
                "DECLARE @ModuleDesc nvarchar(50) /*CURSOR暫存變數*/ " +
                "DECLARE @SysFuncID varchar(50) /*CURSOR暫存變數*/ " +
                "DECLARE @FunctionDesc nvarchar(100) /*CURSOR暫存變數*/ " +
                "DECLARE @tmpTable TABLE( " +
                    "thisType int, /*1: ModID 2:FuncID 3:ActionID */ " +
                    "thisGUID varchar(50), " +
                    "thisName nvarchar(100), " +
                    "myAction varchar(8000) " +
                ") " +
                "SET @tmpSysModID = '' " +
                "DECLARE CurTemp CURSOR FOR " +
                   "SELECT SystemModule.SysModID, SystemModule.ModuleDesc, SystemFunction.SysFuncID, SystemFunction.FunctionDesc " +
                   "FROM SystemModule " +
                   "INNER JOIN SystemFunction ON SystemModule.SysModID = SystemFunction.SysModID AND SystemFunction.iDisplay = 1 " +
                   "WHERE SystemModule.iDisplay = 1 " +
                   "ORDER BY SystemModule.iOrder, SystemModule.SysModID, SystemFunction.iOrder " +
                "OPEN CurTemp " +
                "FETCH NEXT FROM CurTemp INTO @SysModID, @ModuleDesc, @SysFuncID, @FunctionDesc " +
                "WHILE (@@Fetch_Status = 0 ) " +
                "BEGIN " +
                    "/*第一次取得SysModID*/ " +
                    "IF @tmpSysModID <> @SysModID BEGIN " +
                        "SET @tmpSysModID = @SysModID " +
                        "INSERT INTO @tmpTable VALUES(1, @SysModID, '<b>' + @ModuleDesc + '</b>', '') " +
                    "END " +
                    "/*取得這筆SysFuncID的Action字串*/ " +
                    "SET @myAction = '' " +
                    "SELECT @myAction = @myAction + SysActionID + '||' FROM SystemAction WHERE SysFuncID = @SysFuncID " +
                    "IF (LEN(@myAction) > 0) BEGIN SET @myAction = LEFT(@myAction, LEN(@myAction) - 2) END " +
                    "/*取得這筆SysFuncID*/ " +
                    "INSERT INTO @tmpTable VALUES(2, @SysFuncID, '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + @FunctionDesc, @myAction) " +
                    "/*取得這筆SysFuncID的SysActionID*/ " +
                    "INSERT INTO @tmpTable " +
                        "SELECT 3 " +
                            ",SysActionID " +
                            ",'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;>&nbsp;&nbsp;' + ActionDesc " +
                            ",'' " +
                        "FROM SystemAction " +
                        "WHERE SysFuncID = @SysFuncID " +
                "FETCH NEXT FROM CurTemp INTO @SysModID, @ModuleDesc, @SysFuncID, @FunctionDesc " +
                "END " +
                "CLOSE CurTemp " +
                "DEALLOCATE CurTemp " +
                "/*輸出結果*/ " +
                "SELECT thisType, thisGUID, thisName, myAction FROM @tmpTable where thisGUID != ''";

            System.Data.SqlClient.SqlCommand SqlCom3 = new System.Data.SqlClient.SqlCommand(strSQL, db.getOcnn());

            nRet = db.ExecQuerySQLCommand(SqlCom3, ref dt);


            for (i = 0; i < dt.Rows.Count; i++)
            {
                /*<tr>*/
                HTML_TableRoleManage_Body += "<tr class=\"Column_" + (i % 2 == 0 ? "1" : "2") + "\" " +
                        "onmouseover=\"$(this).attr('className', 'Column_Over');\" " +
                        "onmouseout=\"$(this).attr('className', 'Column_" + (i % 2 == 0 ? "1" : "2") + "');\" >";
                
                /*第一個<td>*/
                HTML_TableRoleManage_Body += "<td class=\"Cell\" >" + dt.Rows[i]["thisName"].ToString() + "</td>";
                /*第二~N個<td>, 以權限表區分*/
                for (j = 0; j < dt_DMSRole.Rows.Count; j++)
                {
                    HTML_TableRoleManage_Body += "<td class=\"Cell\" style=\"text-align:center;\">";
                    /*Module行不做, 只check Function/Action*/
                    if (dt.Rows[i]["thisType"].ToString() != "1")
                    {
                        HTML_TableRoleManage_Body += "<input type=\"checkbox\" ";

                        /*判斷Function/Action, 並依Function/Action對應設定之*/
                        if (dt.Rows[i]["thisType"].ToString() == "2")
                        {
                            HTML_TableRoleManage_Body += "name=\"chkFunction\" ";
                            /*將跟此Function相關的Action清單送至<javascript>的onclick_Function進行處理*/
                            HTML_TableRoleManage_Body += "onclick=\"onclick_Function(this, '" + dt.Rows[i]["myAction"].ToString() + "', '" + dt_DMSRole.Rows[j]["DMSRoleID"].ToString() + "');\" ";
                        }
                        else if (dt.Rows[i]["thisType"].ToString() == "3")
                            HTML_TableRoleManage_Body += "name=\"chkAction\" ";

                        /*判斷各個DMSRoleID的權限勾選狀況*/
                        if (dt_DMSRoleDetail.Select("thisGUID='" + dt.Rows[i]["thisGUID"].ToString() + "' AND DMSRoleID = '" + dt_DMSRole.Rows[j]["DMSRoleID"].ToString() + "'").Length > 0)
                        {
                            HTML_TableRoleManage_Body += "checked ";
                        }

                        /*設定各checkbox的value為各功能的DMSRoleID^^thisGUID */
                        HTML_TableRoleManage_Body += "value=\"" + dt_DMSRole.Rows[j]["DMSRoleID"].ToString() + "^^" + dt.Rows[i]["thisGUID"].ToString() + "\" />";
                    }
                    HTML_TableRoleManage_Body += "</td>";
                }
                /*</tr>*/
                HTML_TableRoleManage_Body += "</tr>";
            }

            db.DBDisconnect();
        }
        else
            MessageBox(nRet.ToString());

        /*完成並秀出HTML*/
        HTML_TableRoleManage_Title += "</tr>";
        HTML_TableRoleManage += HTML_TableRoleManage_Title + HTML_TableRoleManage_Body + "</table>";
        DivRoleManage.InnerHtml = HTML_TableRoleManage;
        logger.Debug(HTML_TableRoleManage);
    }


    /// <summary>
    /// 取得角色管理介面清單;
    /// </summary>
    protected void GetRoleRelationList()
    {
        Database db = new Database();
        
        DataTable dt_DMSRole = new DataTable();
        DataTable dt_Relation = new DataTable();
        int nRet = -1;

        string HTML_TableRoleRelation = "<table id=\"TableRoleRelation\" class='table table-striped table-bordered table-hover' cellspacing='0'   cellpadding='0'>";
        string HTML_TableRoleRelation_Title = "<tr><td class=\"ColumnTitle\">" + ParseWording("D0055") + "</td>";
        string HTML_TableRoleRelation_Body = "";
        
        /*連線DB*/
        nRet = db.DBConnect();
        if (nRet == 0)
        {
            /*先取到權限表(這邊沒設定權限也不會error所以不判nRet)*/
            string strSQL = "SELECT DMSRoleID, DMSRoleName FROM DMSRole ";

            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSQL, db.getOcnn());

            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt_DMSRole);

            /*再取到權限關係表(這邊沒設定權限也不會error所以不判nRet)*/
            strSQL = "SELECT DMSRoleID, DMSRoleIDManaged FROM DMSRoleManage ";

            System.Data.SqlClient.SqlCommand SqlCom2 = new System.Data.SqlClient.SqlCommand(strSQL, db.getOcnn());
            nRet = db.ExecQuerySQLCommand(SqlCom2, ref dt_Relation);


            /*計算平均欄寬(多給第一個欄位5%的空間)*/
            int Field_Width = (95 / (dt_DMSRole.Rows.Count + 1));
            /*依權限表跑迴圈組出標題列及角色清單畫面*/
            for (int i = 0; i < dt_DMSRole.Rows.Count; i++) {
                /*產生標提列第二~N個<td>, 以權限表區分*/
                HTML_TableRoleRelation_Title += "<td class=\"ColumnTitle\" style=\"background-color:#ffffee;width:" + Field_Width + "%;\">" + dt_DMSRole.Rows[i]["DMSRoleName"].ToString() + "</td>";

                /*產生角色清單<tr>*/
                HTML_TableRoleRelation_Body += "<tr class=\"Column_" + (i % 2 == 0 ? "1" : "2") + "\" " +
                            "onmouseover=\"$(this).attr('className', 'Column_Over');\" " +
                            "onmouseout=\"$(this).attr('className', 'Column_" + (i % 2 == 0 ? "1" : "2") + "');\" >";
                /*第一個<td>*/
                HTML_TableRoleRelation_Body += "<td class=\"Cell\">" + dt_DMSRole.Rows[i]["DMSRoleName"].ToString() + "</td>";
                /*第二~N個<td>, 以權限表區分*/
                for (int j = 0; j < dt_DMSRole.Rows.Count; j++)
                {
                    HTML_TableRoleRelation_Body += "<td class=\"Cell\" style=\"text-align:center;\">";
                    HTML_TableRoleRelation_Body += "<input type=\"checkbox\" name=\"chkRoleRelation\" ";
                    /*判斷各個DMSRoleID的權限勾選狀況*/
                    if (dt_Relation.Select("DMSRoleID='" + dt_DMSRole.Rows[i]["DMSRoleID"].ToString() + "' AND DMSRoleIDManaged = '" + dt_DMSRole.Rows[j]["DMSRoleID"].ToString() + "'").Length > 0)
                    {
                        HTML_TableRoleRelation_Body += "checked ";
                    }
                    /*設定各checkbox的value為DMSRoleID^^DMSRoleIDManaged.. */
                    HTML_TableRoleRelation_Body += "value=\"" + dt_DMSRole.Rows[i]["DMSRoleID"].ToString() + "^^" + dt_DMSRole.Rows[j]["DMSRoleID"].ToString() + "\" />";
                    
                    HTML_TableRoleRelation_Body += "</td>";
                }
            
                /*產生角色清單</tr>*/
                HTML_TableRoleRelation_Body += "</tr>";
            }

            db.DBDisconnect();
        }
        else
            MessageBox(nRet.ToString());

        /*完成並秀出HTML*/
        HTML_TableRoleRelation_Title += "</tr>";
        HTML_TableRoleRelation += HTML_TableRoleRelation_Title + HTML_TableRoleRelation_Body + "</table>";
        DivRoleRelation.InnerHtml = HTML_TableRoleRelation;
        logger.Debug(HTML_TableRoleRelation);
    }


    /// <summary>
    /// 儲存功能變更
    /// </summary>
    /// <param name="StrFunction">勾選的Function字串(格式:權限ID^^FunctionID||權限ID^^FunctionID||權限ID^^FunctionID..)</param>
    /// <param name="StrAction">勾選的Action字串(格式:權限ID^^ActionID||權限ID^^ActionID||權限ID^^ActionID..)</param>
    /// <returns>nRet (0:成功, 非0:失敗)</returns>
    [System.Web.Services.WebMethod]
    public static CReturnData EdtRoleProcess(string StrFunction, string StrAction)
    {
        CReturnData myData = new CReturnData();

        Database db = new Database();
        DataTable dt = new DataTable();

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        if (myData.nRet == 0)
        {
            string strSQL = "DECLARE @tmpFunction TABLE(DMSRoleID int, SysFuncID varchar(50)) " +
                "DECLARE @tmpAction TABLE(DMSRoleID int, SysActionID varchar(50)) " +
                "/*取得新的Function清單*/ " +
                "INSERT INTO @tmpFunction(DMSRoleID, SysFuncID) " +
	                "SELECT LEFT([Value], CHARINDEX('^^', [Value], 0) - 1) AS DMSRoleID, SUBSTRING([Value], CHARINDEX('^^', [Value], 0) + 2, LEN([Value])) AS SysFuncID " +
                    "FROM dbo.UTILfn_Split('" + StrFunction + "', '||') " +
                "/*取得新的Action清單*/ " +
                "INSERT INTO @tmpAction(DMSRoleID, SysActionID) " +
	                "SELECT LEFT([Value], CHARINDEX('^^', [Value], 0) - 1) AS DMSRoleID, SUBSTRING([Value], CHARINDEX('^^', [Value], 0) + 2, LEN([Value])) AS SysActionID " +
                    "FROM dbo.UTILfn_Split('" + StrAction + "', '||') AS tblAction " +
                	
                "/*挑出新增的Function START==============================================*/ " +
                "/*先新增人員層*/ " +
                "INSERT INTO SecurityUserAccount_FunctionRole(AccountID, SysModID, SysFuncID) " +
	                "SELECT SecurityUserAccount.AccountID, SystemFunction.SysModID, tblFunction.SysFuncID " +
	                "FROM @tmpFunction AS tblFunction " +
	                "LEFT JOIN DMSRoleFunction ON tblFunction.DMSRoleID = DMSRoleFunction.DMSRoleID AND tblFunction.SysFuncID = DMSRoleFunction.SysFuncID " +
	                "INNER JOIN SystemFunction ON tblFunction.SysFuncID = SystemFunction.SysFuncID " +
	                "INNER JOIN SecurityUserAccount ON tblFunction.DMSRoleID = SecurityUserAccount.cRoleID " +
	                "WHERE DMSRoleFunction.SysFuncID IS NULL " +
                "/*再新增角色權限層*/ " +
                "INSERT INTO DMSRoleFunction(DMSRoleID, SysModID, SysFuncID) " +
	                "SELECT tblFunction.DMSRoleID, SystemFunction.SysModID, tblFunction.SysFuncID " +
	                "FROM @tmpFunction AS tblFunction " +
	                "LEFT JOIN DMSRoleFunction ON tblFunction.DMSRoleID = DMSRoleFunction.DMSRoleID AND tblFunction.SysFuncID = DMSRoleFunction.SysFuncID " +
	                "INNER JOIN SystemFunction ON tblFunction.SysFuncID = SystemFunction.SysFuncID " +
	                "WHERE DMSRoleFunction.SysFuncID IS NULL " +
                "/*挑出新增的Function END================================================*/ " +
                "/*挑出減少的Fucntion START==============================================*/ " +
                "/*先刪除人員層*/ " +
                "DELETE FROM SecurityUserAccount_FunctionRole " +
                "WHERE AccountID + '^^' + SysFuncID IN ( " +
	                "SELECT SecurityUserAccount.AccountID + '^^' + DMSRoleFunction.SysFuncID " +
	                "FROM DMSRoleFunction " +
	                "LEFT JOIN @tmpFunction AS tblFunction ON DMSRoleFunction.DMSRoleID = tblFunction.DMSRoleID AND DMSRoleFunction.SysFuncID = tblFunction.SysFuncID " +
	                "INNER JOIN SecurityUserAccount ON DMSRoleFunction.DMSRoleID = SecurityUserAccount.cRoleID " +
	                "WHERE tblFunction.SysFuncID IS NULL " +
                ") " +
                "/*再刪除角色權限層*/ " +
                "DELETE FROM DMSRoleFunction " +
                "WHERE CONVERT(varchar, DMSRoleID) + '^^' + SysFuncID IN ( " +
	                "SELECT CONVERT(varchar, DMSRoleFunction.DMSRoleID) + '^^' + DMSRoleFunction.SysFuncID " +
	                "FROM DMSRoleFunction " +
	                "LEFT JOIN @tmpFunction AS tblFunction ON DMSRoleFunction.DMSRoleID = tblFunction.DMSRoleID AND DMSRoleFunction.SysFuncID = tblFunction.SysFuncID " +
	                "WHERE tblFunction.SysFuncID IS NULL " +
                ") " +
                "/*挑出減少的Fucntion END*/ " +

                "/*挑出新增的Action START================================================*/ " +
                "/*先新增人員層*/ " +
                "INSERT INTO SecurityUserAccount_ActionRole(AccountID, SysModID, SysFuncID, SysActionID) " +
	                "SELECT SecurityUserAccount.AccountID, SystemAction.SysModID, SystemAction.SysFuncID, tblAction.SysActionID " +
	                "FROM @tmpAction AS tblAction " +
	                "LEFT JOIN DMSRoleAction ON tblAction.DMSRoleID = DMSRoleAction.DMSRoleID AND tblAction.SysActionID = DMSRoleAction.SysActionID " +
	                "INNER JOIN SystemAction ON tblAction.SysActionID = SystemAction.SysActionID " +
	                "INNER JOIN SecurityUserAccount ON tblAction.DMSRoleID = SecurityUserAccount.cRoleID " +
	                "WHERE DMSRoleAction.SysActionID IS NULL " +
                "/*再新增角色權限層*/ " +
                "INSERT INTO DMSRoleAction(DMSRoleID, SysModID, SysFuncID, SysActionID) " +
	                "SELECT tblAction.DMSRoleID, SystemAction.SysModID, SystemAction.SysFuncID, tblAction.SysActionID " +
	                "FROM @tmpAction AS tblAction " +
	                "LEFT JOIN DMSRoleAction ON tblAction.DMSRoleID = DMSRoleAction.DMSRoleID AND tblAction.SysActionID = DMSRoleAction.SysActionID " +
	                "INNER JOIN SystemAction ON tblAction.SysActionID = SystemAction.SysActionID " +
	                "WHERE DMSRoleAction.SysActionID IS NULL " +
                "/*挑出新增的Action END==================================================*/ " +
                "/*挑出減少的Action START================================================*/ " +
                "/*先刪除人員層*/ " +
                "DELETE FROM SecurityUserAccount_ActionRole " +
                "WHERE AccountID + '^^' + SysActionID IN ( " +
	                "SELECT SecurityUserAccount.AccountID + '^^' + DMSRoleAction.SysActionID " +
	                "FROM DMSRoleAction " +
	                "LEFT JOIN @tmpAction AS tblAction ON DMSRoleAction.DMSRoleID = tblAction.DMSRoleID AND DMSRoleAction.SysActionID = tblAction.SysActionID " +
	                "INNER JOIN SecurityUserAccount ON DMSRoleAction.DMSRoleID = SecurityUserAccount.cRoleID " +
	                "WHERE tblAction.SysActionID IS NULL " +
                ") " +
                "/*再刪除角色權限層*/ " +
                "DELETE FROM DMSRoleAction " +
                "WHERE CONVERT(varchar, DMSRoleID) + '^^' + SysActionID IN ( " +
	                "SELECT CONVERT(varchar, DMSRoleAction.DMSRoleID) + '^^' + DMSRoleAction.SysActionID " +
	                "FROM DMSRoleAction " +
	                "LEFT JOIN @tmpAction AS tblAction ON DMSRoleAction.DMSRoleID = tblAction.DMSRoleID AND DMSRoleAction.SysActionID = tblAction.SysActionID " +
	                "WHERE tblAction.SysActionID IS NULL " +
                ") " +
                "/*挑出減少的Action END==================================================*/";

            /*寫入DB*/
            db.BeginTranscation();
            db.AddDmsSqlCmd(strSQL);
            db.CommitTranscation();

            myData.nRet = db.nRet;
            myData.outMsg = db.outMsg;

            db.DBDisconnect();
            //string filepath = @"c:\temp\posttestLog\";
            //System.IO.File.WriteAllText(filepath + "EdtRoleProcess.txt", strSQL);
        }

        return myData;
    }


    /// <summary>
    /// 儲存角色關聯變更
    /// </summary>
    /// <param name="StrRoleRelation">角色關聯字串(權限ID^^關聯的權限ID||權限ID^^關聯的權限ID..)</param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static CReturnData EdtRelationProcess(string StrRoleRelation)
    {
        CReturnData myData = new CReturnData();

        Database db = new Database();
        DataTable dt = new DataTable();

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        if (myData.nRet == 0)
        {
            string strSQL = "TRUNCATE TABLE DMSRoleManage " +
                "INSERT INTO DMSRoleManage(DMSRoleID, DMSRoleIDManaged) " +
	                "SELECT LEFT([Value], CHARINDEX('^^', [Value], 0) - 1) AS DMSRoleID, SUBSTRING([Value], CHARINDEX('^^', [Value], 0) + 2, LEN([Value])) AS DMSRoleIDManaged " +
                    "FROM dbo.UTILfn_Split('" + StrRoleRelation + "', '||') ";

            /*寫入DB*/
            db.BeginTranscation();
            db.AddDmsSqlCmd(strSQL);
            db.CommitTranscation();

            myData.nRet = db.nRet;
            myData.outMsg = db.outMsg;

            db.DBDisconnect();
            //string filepath = @"c:\temp\posttestLog\";
            //System.IO.File.WriteAllText(filepath + "EdtRelationProcess.txt", strSQL);
        }
      
        return myData;
    }

}
