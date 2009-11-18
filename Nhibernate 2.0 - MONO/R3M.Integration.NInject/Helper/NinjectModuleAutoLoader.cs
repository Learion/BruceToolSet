using System;
using System.Collections.Generic;
using System.Reflection;

using Ninject.Core;
using R3M.Integration.Ninject.Helper;


namespace R3M.Integration.Ninject.Helper
{
    public class NinjectModuleAutoLoader : INinjectModuleAutoLoader
    {
        private readonly IKernel kernel;


        public NinjectModuleAutoLoader(IKernel kernel)
        {
            this.kernel = kernel;
        }


        public void InitAndLoadModulesForLoadedAssemblies()
        {
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                LoadModulesFromAssembly(a);
            }


            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
        }


        private void LoadModulesFromAssembly(Assembly assembly)
        {
            foreach (var modules in FindAllModulesToLoad(assembly))
            {
                kernel.Load(modules);
            }
        }


        private static IEnumerable<IModule[]> FindAllModulesToLoad(Assembly assembly)
        {
            foreach (var t in assembly.GetTypes())
            {
                if ((t.IsAbstract || !t.IsPublic) || t.GetInterface(typeof (INinjectModulesToLoad).FullName) == null)
                    continue;

                var modulesToLoad = (INinjectModulesToLoad) Activator.CreateInstance(t);
                yield return modulesToLoad.RequiredModules;
            }
        }


        private void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            LoadModulesFromAssembly(args.LoadedAssembly);
        }
    }
}