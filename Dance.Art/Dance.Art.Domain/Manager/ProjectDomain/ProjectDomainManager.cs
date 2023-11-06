using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 项目领域管理器
    /// </summary>
    [DanceSingleton(typeof(IProjectDomainManager))]
    public class ProjectDomainManager : IProjectDomainManager
    {
        /// <summary>
        /// 加载插件
        /// </summary>
        /// <param name="assemblyPrefix">程序集前缀</param>
        public void LoadPlugin(string assemblyPrefix)
        {
            List<string> files = new();
            List<Assembly> assemblies = new();

            files.AddRange(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Where(p => Path.GetFileName(p).StartsWith(assemblyPrefix)));

            assemblies.AddRange(files.Select(p => Assembly.Load(AssemblyName.GetAssemblyName(p))));

            LoadAssembly(assemblies.ToArray());
        }

        /// <summary>
        /// 加载程序集
        /// </summary>
        /// <param name="assemblies">程序集</param>
        private static void LoadAssembly(params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    if (type.IsAbstract || !type.IsClass || string.IsNullOrWhiteSpace(type.FullName))
                        continue;

                    if (!type.IsAssignableTo(typeof(IProjectDomainBuilder)))
                        continue;

                    if (type.Assembly.CreateInstance(type.FullName) is not IProjectDomainBuilder builder)
                        continue;

                    ArtDomain.Current.ProjectDomainBuilders.Add(builder);
                }
            }
        }
    }
}
