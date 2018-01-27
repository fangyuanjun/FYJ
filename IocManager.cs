using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYJ
{
    public class IocManager
    {
        private static Dictionary<string, object> instanceCache = new Dictionary<string, object>();

        /// <summary>
        /// 获取一个实例
        /// </summary>
        public static T GetInstance<T>() where T : class
        {
            string fullName = typeof(T).FullName;
            if (instanceCache[fullName] == null)
            {
                throw new Exception("没有创建" + fullName + "类型,需要先调用PutInstance方法");
            }

            return instanceCache[fullName] as T;
        }

        public static object GetInstance(string key)
        {
            if (instanceCache[key] == null)
            {
                throw new Exception("没有Put" + key + ",需要先调用PutInstance方法");
            }

            return instanceCache[key];
        }

        public static void PutInstance(string key, object obj)
        {
            instanceCache[key] = obj;
        }

        public static void PutInstance<T>(params object[] args) where T : class
        {
            T o;
            if (args == null || args.Length == 0)
            {
                o = (T)Activator.CreateInstance(typeof(T));
            }
            else
            {
                o = (T)Activator.CreateInstance(typeof(T), args);
            }

            if (o == null)
            {
                throw new Exception("创建类型" + typeof(T).FullName + "失败");
            }

            instanceCache[typeof(T).FullName] = o;
        }
    }
}
