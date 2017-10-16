using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Deploy_Auth_Control 的摘要描述
/// </summary>
public class Deploy_Auth_Control
{
    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

	public Deploy_Auth_Control()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}


    /*
     透過使用者所屬角色判斷是否將ToolBarControl設定為Visible    
     * 角色屬於web.config內DEPLOY_AUTH_ROLES定義者，為Visible
     * 如role=1或3:
     *   <!--web.config可使用發布功能的角色設定	-->
     *   <add key="DEPLOY_AUTH_ROLES" value="1,3"/>
     */
    public static void checkDeployPower(string DMSRoleID, object ToolBarControl)
    {
 
        string strDEPLOY_AUTH_ROLES = System.Configuration.ConfigurationManager.AppSettings["DEPLOY_AUTH_ROLES"];

        if (strDEPLOY_AUTH_ROLES.IndexOf(DMSRoleID) > -1)
        {
            ((System.Web.UI.UserControl)ToolBarControl).Visible = true;
        }
        else
        {
            ((System.Web.UI.UserControl)ToolBarControl).Visible = false ;
        }    

    }

 
}