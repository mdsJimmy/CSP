using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;

using MDS.Database;
using System.Text;

public partial class myTreeView : BasePage
{
    
    protected string myGroupID = "";
    string myTreeViewSQL = "";

    protected string myTargerUrl = ""; /*目標頁面(default:空白)*/
    string myTargetFrame = "frmMain"; /*目標框架頁(default:frmMain)*/
    protected string myTargerGroupID = ""; /*預設選取的節點(default:myGroupID)*/
    protected string myTreeViewSize = "180"; /*預設TreeView的寬度(default:180)*/

    Database db = new Database();
    DataTable dt = new DataTable();
    int nRet = -1;


    protected string ReplaceString = "";

    protected string CurrentIndex = ""; /*當前的GroupID的Index, 觸發 .click()事件用*/

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            /*接收Session*/
            //myGroupID = Session["ParentGroupID"]==null?"":Session["ParentGroupID"].ToString(); /*取得使用者GroupID*/


            myGroupID =(string)Session["ParentGroupID"]; /*取得使用者GroupID*/
            

            /*接收Request*/
            if (!string.IsNullOrEmpty(Request.QueryString["TargerUrl"]))
                myTargerUrl = Request.QueryString["TargerUrl"];
            if (!string.IsNullOrEmpty(Request.QueryString["TargetFrame"]))
                myTargetFrame = Request.QueryString["TargetFrame"];
            if (!string.IsNullOrEmpty(Request.QueryString["TargerGroupID"]))
                myTargerGroupID = Request.QueryString["TargerGroupID"];
            else
                myTargerGroupID = myGroupID;
            if (!string.IsNullOrEmpty(Request.QueryString["myTreeViewSize"]))
                myTreeViewSize = Request.QueryString["myTreeViewSize"];
            
            /*Select結果rank = 1的(Root)只能有一筆, 且rank = 1的會在第一筆(有order by)*/
            myTreeViewSQL = "SELECT tblA.ParentGroupID, tblA.GroupID, tblA.[Rank], SecurityGroup.GroupName, tblA.GroupSearchKey "
                + "FROM dbo.fn_GetGroupTree(@myGroupID) AS tblA "
                + "INNER JOIN SecurityGroup ON tblA.GroupID = SecurityGroup.GroupID order by 1";
            
            /*連線DB*/
            nRet = db.DBConnect();
            if (nRet == 0)
            {

                System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(myTreeViewSQL, db.getOcnn());
                SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@myGroupID", myGroupID));

                nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);
    
                if (nRet == 0)
                    GenTreeNode();
            } 
            dt.Reset();
            db.DBDisconnect();

        }
    }


    /// <summary>
    /// 建立TreeView之節點;目前只做到Rank ='3',且是最原始的組字串合成
    /// </summary>
    private void GenTreeNode()
    {
        if (dt.Rows.Count > 0)
        {
            DataRow[] MainRows = dt.Select("Rank = '1'");

            StringBuilder treeString = new StringBuilder(" var tree_data ={ ");
            for (int i = 0; i < MainRows.Length; i++)
            {
                if (i > 0)
                {
                    treeString.Append(",");
                }
                string type = "item";
                if (dt.Select("ParentGroupID = '" + TrimString.trimBad(MainRows[i]["GroupID"].ToString()) + "'").Length > 0)
                {
                    type = "folder";
                }
                treeString.Append("'").Append(TrimString.trimBad(MainRows[i]["GroupID"].ToString())).Append("'");
                treeString.Append(":{ text:'").Append(TrimString.trimBad(MainRows[i]["GroupName"].ToString())).Append("',type:'").Append(type).Append("',gid:'").Append(TrimString.trimBad(MainRows[i]["GroupID"].ToString())).Append("'}");

            }
            treeString.Append("};");

            for (int i = 0; i < MainRows.Length; i++)
            {
                string groupid = TrimString.trimBad(MainRows[i]["GroupID"].ToString());
                if (dt.Select("ParentGroupID = '" + groupid + "'").Length > 0)
                {
                    treeString.Append("tree_data['").Append(groupid).Append("']['additionalParameters']={'children': {");
                    DataRow[] DetailRows = dt.Select("Rank = '2' and ParentGroupID = '" + groupid + "'");
                    for (int di = 0; di < DetailRows.Length; di++)
                    {
                        if (di > 0)
                        {
                            treeString.Append(",");

                        }
                        string type = "item";
                        if (dt.Select("Rank = '2' and ParentGroupID = '" + groupid + "'").Length > 0)
                        {
                            type = "folder";
                        }
                        treeString.Append("'").Append(TrimString.trimBad(DetailRows[di]["GroupID"].ToString())).Append("'");
                        treeString.Append(":{ text:'").Append(TrimString.trimBad(DetailRows[di]["GroupName"].ToString())).Append("',type:'").Append(type).Append("',gid:'").Append(TrimString.trimBad(DetailRows[di]["GroupID"].ToString())).Append("'}");
                    }
                    treeString.Append("}");
                    treeString.Append("};");
                }
            }

            for (int i = 0; i < MainRows.Length; i++)
            {
                string groupid = TrimString.trimBad(MainRows[i]["GroupID"].ToString());
                if (dt.Select("ParentGroupID = '" + groupid + "'").Length > 0)
                {

                    DataRow[] DetailRows = dt.Select("Rank = '2' and ParentGroupID = '" + groupid + "'");

                    for (int di = 0; di < DetailRows.Length; di++)
                    {
                        string deatilGroup = TrimString.trimBad(DetailRows[di]["GroupID"].ToString());
                        treeString.Append("tree_data['").Append(groupid)
                            .Append("']['additionalParameters']['children']['")
                            .Append(MDS.Utility.NUtility.trimBad( deatilGroup)).Append("']['additionalParameters']={'children': {");

                        DataRow[] finallRows = dt.Select("Rank = '3' and ParentGroupID = '" + deatilGroup + "'");
                        for (int fi = 0; fi < finallRows.Length; fi++)
                        {
                            if (fi > 0)
                            {
                                treeString.Append(",");
                            }
                            string type = "item";
                            treeString.Append("'").Append(finallRows[fi]["GroupID"].ToString()).Append("'");
                            treeString.Append(":{ text:'").Append(finallRows[fi]["GroupName"].ToString()).Append("',type:'").Append(type).Append("',gid:'").Append(TrimString.trimBad(DetailRows[di]["GroupID"].ToString())).Append("'}");
                        }
                        treeString.Append("}");
                        treeString.Append("};");


                    }


                }
            }





            ReplaceString = treeString.ToString();
        }


    }
    

    //var tree_data = {
    //             'for-sale': { text: 'For Sale', type: 'folder' },
    //             'vehicles': { text: 'Vehicles', type: 'folder' },
    //             'rentals': { text: 'Rentals', type: 'folder' },
    //             'real-estate': { text: 'Real Estate', type: 'folder' },
    //             'pets': { text: 'Pets', type: 'folder' },
    //             'tickets': { text: 'Tickets', type: 'item' },
    //             'services': { text: 'Services', type: 'item' },
    //             'personals': { text: 'Personals', type: 'item' }
    //         }



    //tree_data['for-sale']['additionalParameters'] = {
    //               'children': {
    //                   'appliances': { text: 'Appliances', type: 'item' },
    //                   'arts-crafts': { text: 'Arts & Crafts', type: 'item' },
    //                   'clothing': { text: 'Clothing', type: 'item' },
    //                   'computers': { text: 'Computers', type: 'item' },
    //                   'jewelry': { text: 'Jewelry', type: 'item' },
    //                   'office-business': { text: 'Office & Business', type: 'item' },
    //                   'sports-fitness': { text: 'Sports & Fitness', type: 'item' }
    //               }
    //           }
    //            tree_data['vehicles']['additionalParameters']['children']['cars']['additionalParameters'] = {
    //                           'children': {
    //                               'classics': { text: 'Classics', type: 'item' },
    //                               'convertibles': { text: 'Convertibles', type: 'item' },
    //                               'coupes': { text: 'Coupes', type: 'item' },
    //                               'hatchbacks': { text: 'Hatchbacks', type: 'item' },
    //                               'hybrids': { text: 'Hybrids', type: 'item' },
    //                               'suvs': { text: 'SUVs', type: 'item' },
    //                               'sedans': { text: 'Sedans', type: 'item' },
    //                               'trucks': { text: 'Trucks', type: 'item' }
    //                           }
    //                       }

}
