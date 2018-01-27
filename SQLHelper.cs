using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FYJ
{
   public class SQLHelper
    {
        public static string SqlFilter(string str)
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            List<Regex> regList = new List<Regex>();
            regList.Add(new Regex(@"select\s+", RegexOptions.IgnoreCase));
            regList.Add(new Regex(@"insert\s+", RegexOptions.IgnoreCase));
            regList.Add(new Regex(@"delete\s+", RegexOptions.IgnoreCase));
            regList.Add(new Regex(@"update\s+", RegexOptions.IgnoreCase));
            regList.Add(new Regex(@"drop\s+", RegexOptions.IgnoreCase));
            regList.Add(new Regex(@"exec\s+", RegexOptions.IgnoreCase));
            regList.Add(new Regex(@"truncate\s+", RegexOptions.IgnoreCase));
         
            regList.Add(new Regex(@";", RegexOptions.IgnoreCase));
    
          
            foreach (Regex reg in regList)
            {
                str = reg.Replace(str, "");
            }

            return str;
        }
    }
}
