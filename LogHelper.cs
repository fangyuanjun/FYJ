using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FYJ
{
    public class LogHelper
    {
        public static void WriteLog(string log)
        {
            try
            {
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string filePath = Path.Combine(folderPath, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");

                StreamWriter f = File.AppendText(filePath);
                f.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "    " + log);
                f.Flush();
                f.Close();
            }
            catch
            {

            }
        }

        public static void WriteLog(Exception ex)
        {
            try
            {
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string filePath = Path.Combine(folderPath, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");

                StreamWriter f = File.AppendText(filePath);
                f.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "    " + ex.Message + "\n" + ex.StackTrace);
                f.Flush();
                f.Close();
            }
            catch
            {

            }
        }

        public static void WriteLog(Exception ex, string text)
        {
            try
            {
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string filePath = Path.Combine(folderPath, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");

                StreamWriter f = File.AppendText(filePath);
                f.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "    " + text + Environment.NewLine + ex.Message + "\n" + ex.StackTrace);
                f.Flush();
                f.Close();
            }
            catch
            {

            }
        }
    }
}
