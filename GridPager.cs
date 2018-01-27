using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FYJ
{
    /// <summary>
    /// 
    /// </summary>
    /// <code> public class GridPager</code>
    public class GridPager
    {
        private int _pageSize = 10;
        /// <summary>
        /// 每页行数 默认10  设置为小于等于0的数将无效
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value > 0)
                {
                    _pageSize = value;
                }
            }
        }

        public int _currentPage = 1;
        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurrentPage
        {
            get
            {
                if (StartIndex != -1)
                {
                    return StartIndex / PageSize + 1;
                }
                return _currentPage;
            }
            set { _currentPage = value; }

        }

        /// <summary>
        /// 排序方式 asc 或者 desc
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 排序列
        /// </summary>
        public string OrderColumn { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalRows { get; set; }

        private string _queryString;
        /// <summary>
        /// 查询字符串 userName=xxxx&isAdmin=1   这样
        /// </summary>
        public string QueryString
        {
            get
            {
                return SQLHelper.SqlFilter(_queryString);
            }
            set
            {
                _queryString = value;
            }
        }


        public string OrderString
        {
            get
            {
                if (!String.IsNullOrEmpty(OrderColumn))
                {
                    return " order by " + SQLHelper.SqlFilter(OrderColumn) + " " + SQLHelper.SqlFilter(Order);
                }

                return "";
            }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages
        {
            get
            {
                if (TotalRows == 0)
                {
                    return 0;
                }
                return (int)Math.Ceiling((float)TotalRows / (float)PageSize);
            }
        }

        public IDictionary<string, object> Parameters
        {
            get;
            set;
        } = new Dictionary<string, object>();

        public string Where { get; set; }


        private int _startIndex = -1;
        /// <summary>
        /// 行数起   优先级高于属性CurrentPage
        /// </summary>
        public int StartIndex
        {
            get { return _startIndex; }
            set { _startIndex = value; }
        }

 
        /// <summary>
        /// 附加对象
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// 同StartIndex
        /// </summary>
        public int Offset
        {
            get
            {
                return StartIndex;
            }
            set
            {
                StartIndex = value;
            }
        }

        /// <summary>
        /// 同PageSize
        /// </summary>
        public int Limit
        {
            get
            {
                return PageSize;
            }
            set
            {
                PageSize = value;
            }
        }
    }
}
