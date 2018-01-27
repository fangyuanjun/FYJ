using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Xml;

namespace FYJ
{
    /// <summary>
    /// 创建对象工厂
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// 方远均-20141111
    public class IocFactory<T> where T : class
    {
        /// <summary>
        /// 获取一个实例
        /// </summary>
        public static T Instance
        {
            get
            {
                string iocFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/Ioc.xml");
                ObjectCache cache = MemoryCache.Default;
                string fullName = typeof(T).FullName;
                if (cache[fullName] == null)
                {
                    CacheItemPolicy policy = new CacheItemPolicy();

                    List<string> filePaths = new List<string>();
                    filePaths.Add(iocFilePath);

                    policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));

                    T instace = Create();
                    if (instace == null)
                    {
                        throw new Exception("没有找到类型" + fullName + "的配置,请检查ioc.xml");
                    }
                    cache.Set(fullName, instace, policy);
                }

                return cache[fullName] as T;
            }
        }

        public static T Create(params object[] args)
        {
            string iocFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/Ioc.xml");
            string fullName = typeof(T).AssemblyQualifiedName;
            XmlDocument doc = new XmlDocument();
            doc.Load(iocFilePath);
            foreach (XmlNode node in doc.GetElementsByTagName("Service"))
            {
                Type nametype = Type.GetType(node.Attributes["name"].InnerText);
                if (nametype == null)
                {
                    throw new Exception("获取类型" + node.Attributes["name"].InnerText + "失败");
                }

                if (nametype.AssemblyQualifiedName == fullName)
                {
                    Type instanceType = Type.GetType(node.Attributes["type"].InnerText);
                    if (instanceType == null)
                    {
                        throw new Exception("创建" + node.Attributes["type"].InnerText + "失败");
                    }

                    T o;
                    if (args == null || args.Length == 0)
                    {
                        o = (T)Activator.CreateInstance(instanceType);
                    }
                    else
                    {
                        o = (T)Activator.CreateInstance(instanceType, args);
                    }

                    if (o == null)
                    {
                        throw new Exception("创建类型" + instanceType + "失败");
                    }
                    return o;

                }

            }

            return default(T);
        }
    }
}

