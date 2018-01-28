using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Clong.BaseEngine.TApplication.Test.GeodatabaseTest
{
    public class SdeWorkspaceFactoryTest
    {
        //测试版本库的
        public void Test()
        {
            var server = "192.168.3.100";
            var instance = "192.168.3.110/orcl";
            var user = "geodata";
            var pwd = "geodata";
            var version =  "geodata.DEFAULT";
            OpenSdeWorkspace(server, user, pwd, instance, version);

        }

        /// <summary>
        /// 打开一个SDE数据库连接。
        /// </summary>
        /// <param name="serverName">服务器名。</param>
        /// <param name="user">用户名。</param>
        /// <param name="password">密码。</param>
        /// <param name="instance">实例名:如5151。</param>
        /// <param name="versionName">版本名称。</param>
        /// <returns>工作空间。</returns>
        public static IWorkspace OpenSdeWorkspace(string serverName, string user, string password, string instance, string versionName)
        {
            IPropertySet propertySet = new PropertySetClass();
            propertySet.SetProperty("SERVER", serverName);
            propertySet.SetProperty("USER", user);
            propertySet.SetProperty("INSTANCE", instance);
            propertySet.SetProperty("PASSWORD", password);
            propertySet.SetProperty("VERSION", versionName);
            return OpenSdeWorkspace(propertySet);
        }

        /// <summary>
        /// 打开一个SDE数据连接。
        /// </summary>
        /// <param name="propertySet">连接信息。</param>
        /// <returns>工作空间。</returns>
        public static IWorkspace OpenSdeWorkspace(IPropertySet propertySet)
        {
            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
            IWorkspaceFactory2 workspaceFactory = (IWorkspaceFactory2)Activator.CreateInstance(factoryType);
            return workspaceFactory.Open(propertySet, 0);
        }
    }
}
