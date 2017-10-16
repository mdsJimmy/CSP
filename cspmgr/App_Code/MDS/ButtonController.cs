using MDS.Database;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// ButtonController 的摘要描述
/// </summary>
public class ButtonController
{

    

    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private ContentPlaceHolder ContentPlaceHolder = null;
    private string pageFunctionName = "";
    private string accountID = "";


    public ButtonController(ContentPlaceHolder ContentPlaceHolder, string pageFunctionName,string accountID)
    {
        this.ContentPlaceHolder = ContentPlaceHolder;
        this.pageFunctionName = pageFunctionName;
        this.accountID = accountID;
    }

  
    public  void quarryDB_defaultButton_false()
    {


        
        Database db = new Database();
        DataTable dt = new DataTable();
        SqlCommand sqlcmd = null; ;

        int conn = 1;
        conn = db.DBConnect();
        if (conn == 0)
        {
            try
            {
                string strQry = @"select 

                                a.SysActionID
                                ,a.ActionDesc
                                ,a.SysFuncID
                                ,b.FunctionDesc
                                ,a.SysModID 
                                ,a.ButtonID
                                from 
                                SystemAction a
                                ,SystemFunction b  

                                where 
                                a.SysFuncID=b.SysFuncID 
                                and b.FunctionDesc=@FunctionDesc ";

                using (sqlcmd = new SqlCommand(strQry, db.getOcnn()))
                {
                    sqlcmd.Parameters.Add(new SqlParameter("@FunctionDesc", pageFunctionName));
                    int rowNum = db.ExecQuerySQLCommand(sqlcmd, ref dt);

                    if (rowNum != 0) throw new Exception("代碼：" + db.nRet + ";原因：" + db.outMsg + ";page名：" + pageFunctionName);
                    foreach (DataRow row in dt.Rows)
                    {


                        if (ContentPlaceHolder.FindControl(row["ButtonID"].ToString()) != null) {

                            ContentPlaceHolder.FindControl(row["ButtonID"].ToString()).Visible = false;
                        }

                    }
                }





            }
            catch (Exception ex)
            {
               
                throw ex;
            }
            finally {

                dt.Dispose();
                db.DBDisconnect();
                
            }
          
        }
        else
        {
            dt.Dispose();
            db.DBDisconnect();
            
        }

    }

    public  void quarryDB_AccountID_Btuuon_true() {
        
        Database db = new Database();
        DataTable dt = new DataTable();
        SqlCommand sqlcmd = null; ;
        
        int conn = 1;
        conn = db.DBConnect();
        if (conn == 0)
        {
            try
            {
                string strQry = @"select 
                                    c.AccountID
                                    ,a.SysActionID
                                    ,a.ActionDesc
                                    ,a.SysFuncID
                                    ,b.FunctionDesc
                                    ,a.SysModID 
                                    ,a.ButtonID
                                    from 
                                    SystemAction a
                                    ,SystemFunction b  
                                    ,SecurityUserAccount_ActionRole c
                                    where 
                                    a.SysFuncID=b.SysFuncID 
                                    and a.SysActionID=c.SysActionID
                                    and AccountID=@AccountID
                                    and b.FunctionDesc = @pageFunctionName
                                    order  by  AccountID";

                using (sqlcmd = new SqlCommand(strQry, db.getOcnn()))
                {

                    sqlcmd.Parameters.Add(new SqlParameter("@AccountID", accountID));
                    sqlcmd.Parameters.Add(new SqlParameter("@pageFunctionName", pageFunctionName));


                    int rowNum = db.ExecQuerySQLCommand(sqlcmd, ref dt);

                    if (rowNum != 0) throw new Exception("代碼：" + db.nRet + ";原因：" + db.outMsg );
                    foreach (DataRow row in dt.Rows)
                    {

                        if (ContentPlaceHolder.FindControl(row["ButtonID"].ToString()) != null)
                        {

                            ContentPlaceHolder.FindControl(row["ButtonID"].ToString()).Visible = true;
                        }


                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally {


                dt.Dispose();
                db.DBDisconnect();

            }
         
        }
        else {
            dt.Dispose();
            db.DBDisconnect();
           
        }



    }



    
}