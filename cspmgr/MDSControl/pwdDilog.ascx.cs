using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MDSControl_pwdDilog : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public static string ParseWording(string WordingID)
    {
        string sWording = "";

        /*
        object o_LocalRs = base.GetLocalResourceObject(WordingID);
        
        if (o_LocalRs != null)
        {
            if (o_LocalRs.GetType() == typeof(string))
                sWording = o_LocalRs.ToString();
        }
        else
        {
            object o_Global = base.GetGlobalResourceObject("DMSWording", WordingID);
            if (o_Global != null)
            {
                if (o_Global.GetType() == typeof(string))
                    sWording = o_Global.ToString();
            }
        }
         */

        System.Resources.ResourceSet rs = Resources.DMSWording.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, true);

        object o = rs.GetObject(WordingID);


        if (o != null)
        {
            if (o.GetType() == typeof(string))
                sWording = o.ToString();
        }

        if (sWording == "")
            sWording = WordingID;

        return sWording;
    }


}