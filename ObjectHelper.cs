using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FYJ
{
    public static class ObjectHelper
    {
        public static bool IsNullOrEmptyOrDbNullOrWhiteSpace(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return true;
            }

            if (String.IsNullOrWhiteSpace(obj.ToString()))
            {
                return true;
            }

            return false;
        }

        public static bool IsNullOrEmptyOruUndefinedOrWhiteSpace(string str)
        {
            if (String.IsNullOrWhiteSpace(str) || str.Trim() == "undefined")
            {
                return true;
            }

            return false;
        }

      
        /// <summary>
        /// 自定义bool 转换  只有值为1或true时才返回真否则返回假
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBoolean(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return false; ;
            }

            if (String.IsNullOrWhiteSpace(obj.ToString()))
            {
                return false;
            }

            if (obj.ToString() == "1" || obj.ToString().ToLower() == "true")
            {
                return true;
            }

            return false;
        }

        public static T CloneProperties<T>(object src) where T : new()
        {
            if (src == null)
            {
                return default(T);
            }

            T dest = new T();

            PropertyInfo[] destPis = typeof(T).GetProperties();
            foreach (PropertyInfo destPi in destPis)
            {
                PropertyInfo srcPi = src.GetType().GetProperty(destPi.Name);
                if (srcPi != null)
                {
                    object value = srcPi.GetValue(src, null);
                    if (value != null)
                    {
                        destPi.SetValue(dest, value, null);
                    }
                }
            }
            return dest;
        }


        /// <summary>
        /// 克隆对象属性
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        public static void CloneProperties(object src, object dest)
        {
            if (src == null || dest == null)
            {
                return;
            }

            PropertyInfo[] destPis = dest.GetType().GetProperties();
            foreach (PropertyInfo destPi in destPis)
            {
                PropertyInfo srcPi = src.GetType().GetProperty(destPi.Name);
                if (srcPi != null)
                {
                    object value = srcPi.GetValue(src, null);
                    if (value != null)
                    {
                        destPi.SetValue(dest, value, null);
                    }
                }
            }

        }


        public static object ConvertValue(object src, Type type)
        {
            if (src != null && src != DBNull.Value && src.GetType() == type)
            {
                return src;
            }

            if (type.IsValueType == false && (src == null || src == DBNull.Value))
            {
                return null;
            }

            if (type.FullName.Contains("System.Nullable") && (src == null || src == DBNull.Value || src.ToString().Trim() == ""))
            {
                return null;
            }

            if (src != null && src != DBNull.Value && type.IsValueType && src.ToString().Trim() == "")
            {
                return Activator.CreateInstance(type);
            }


            if (type.FullName.Contains("System.Nullable"))
            {
                type = type.GetGenericArguments()[0];
            }

            if (type == typeof(bool))
            {
                return ToBoolean(src);
            }
            else
            {
                return Convert.ChangeType(src, type);
            }

        }


        /// <summary>
        /// 将DataTable转换成实体  如果dt为null将返回null 如果为0行将返回new List<T>()
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DataTableToModel<T>( DataTable dt) where T : new()
        {
            if (dt == null)
            {
                return null;
            }

            if (dt.Rows.Count == 0)
            {
                return new List<T>();
            }

            List<T> list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T model = new T();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    if (dt.Columns.Contains(info.Name))
                    {
                        if (info.CanWrite)
                        {
                            if (dr[info.Name] != DBNull.Value)
                            {
                                info.SetValue(model, ConvertValue(dr[info.Name], info.PropertyType), null);
                            }
                        }
                    }
                }
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 将DataTable转换成单个实体  如果datatable为空或0行 都返回null
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>方远均 2015-4-6</returns>
        public static T DataTableToSingleModel<T>( DataTable dt) where T : new()
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return default(T);
            }
            else
            {
                return DataTableToModel<T>(dt)[0];
            }
        }

       
        /// <summary>
        /// 克隆IEnumerable 类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static IEnumerable<T> CloneProperties<T>( IEnumerable<object> srcList) where T : new()
        {
            if (srcList == null)
            {
                return null;
            }

            List<T> list = new List<T>();
            foreach (object src in srcList)
            {
                T dest = new T();

                PropertyInfo[] destPis = typeof(T).GetProperties();
                foreach (PropertyInfo destPi in destPis)
                {
                    PropertyInfo srcPi = src.GetType().GetProperty(destPi.Name);
                    if (srcPi != null)
                    {
                        object value = srcPi.GetValue(src, null);
                        if (value != null)
                        {
                            destPi.SetValue(dest, value, null);
                        }
                    }
                }

                list.Add(dest);
            }

            return list;
        }


        public static void UpdateModel( object obj, NameValueCollection collection)
        {
            string[] keys = collection.AllKeys;

            PropertyInfo[] pis = obj.GetType().GetProperties();

            foreach (PropertyInfo pi in pis)
            {
                string name = pi.Name;
                if (keys.Contains(name))
                {
                    string value = collection[name];
                    if (value == "true,false")
                    {
                        pi.SetValue(obj, true, null);
                    }
                    else
                    {
                        pi.SetValue(obj, ConvertValue(value, pi.PropertyType), null);
                    }
                }
            }
        }
    }
}
