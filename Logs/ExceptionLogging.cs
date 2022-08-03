using System;
using System.IO;

public static class ExceptionLogging
{

    private static string ErrorlineNo, Errormsg, extype, exurl, ErrorLocation;

    public static void Logs(Exception ex, string currentUrl)
    {
        var line = Environment.NewLine + Environment.NewLine;

        ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
        Errormsg = ex.GetType().Name.ToString();
        extype = ex.GetType().ToString();
        exurl = currentUrl;
        ErrorLocation = ex.Message.ToString();

        try
        {
            string filepath = Path.Combine(@"C:\LogcLoopTask\Logs\");  //Text File Path

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);

            }
            filepath = filepath + DateTime.Today.ToString("dd-MM-yyyy") + ".txt";   //Text File Name
            if (!File.Exists(filepath))
            {
                File.Create(filepath).Dispose();

            }
            using (StreamWriter sw = File.AppendText(filepath))
            {
                string errorMessage = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Error Line No :" + " " + ErrorlineNo + line + "Error Message:" + " " + Errormsg + line + "Exception Type:" + " " + extype + line + "Error Location :" + " " + ErrorLocation + line + " Error Page Url:" + " " + exurl + line;

                sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                sw.WriteLine(line);
                sw.WriteLine(errorMessage);
                sw.WriteLine("--------------------------------*End*------------------------------------------");
                sw.WriteLine(line);
                sw.Flush();
                sw.Close();

            }

        }
        catch (Exception e)
        {
            e.ToString();

        }
    }

}