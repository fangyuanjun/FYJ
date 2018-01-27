using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYJ
{
   public class TableInfoAttribute : Attribute
    {
        public TableInfoAttribute(string tableName,string primaryName,string userIDName)
        {
            this.TableName = tableName;
            this.PrimaryName = primaryName;
            this.UserIDName = userIDName;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 主键名
        /// </summary>
        public string PrimaryName { get; set; }

        /// <summary>
        /// 用户ID列名
        /// </summary>
        public string UserIDName { get; set; }
    }
}
