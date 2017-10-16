<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // 應用程式啟動時執行的程式碼
        Application["Visitors"] = 0;

        string log4netPath = Server.MapPath("~/log4net.config");
        log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(log4netPath));
		MIPLibrary.DBCUtil.CONNECTION_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;



        try
        {
            string path = Server.MapPath("~/UserUpLoad/temp/");
            // Determine whether the directory exists.
            if (System.IO.Directory.Exists(path))
            {
                Console.WriteLine("That path exists already.");
                return;
            }

            // Try to create the directory.
            System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(path);
            Console.WriteLine("The directory was created successfully at {0}.", System.IO.Directory.GetCreationTime(path));

            path = Server.MapPath("~/UserUpLoad/tempFile/");
            // Determine whether the directory exists.
            if (System.IO.Directory.Exists(path))
            {
                Console.WriteLine("That path exists already.");
                return;
            }

            // Try to create the directory.
            di = System.IO.Directory.CreateDirectory(path);
            Console.WriteLine("The directory was created successfully at {0}.", System.IO.Directory.GetCreationTime(path));
        
        
        
        
        
        }
        catch (Exception ex)
        {
            Console.WriteLine("The process failed: {0}", ex.ToString());
        }
        finally { }
        
        
	}
    
    void Application_End(object sender, EventArgs e) 
    {
        //  應用程式關閉時執行的程式碼
        Application["Visitors"] = 0;
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // 發生未處理錯誤時執行的程式碼
      

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 啟動新工作階段時執行的程式碼
        Application.Lock();
        Application["Visitors"] = int.Parse(Application["Visitors"].ToString()) + 1;
        Application.UnLock();
    }

    void Session_End(object sender, EventArgs e) 
    {
        // 工作階段結束時執行的程式碼。 
        // 注意: 只有在 Web.config 檔將 sessionstate 模式設定為 InProc 時，
        // 才會引發 Session_End 事件。如果將工作階段模式設定為 StateServer 
        // 或 SQLServer，就不會引發這個事件。
        Application.Lock();
        Application["Visitors"] = int.Parse(Application["Visitors"].ToString()) - 1;
        Application.UnLock();
    }
       
</script>
