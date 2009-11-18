
using Ninject.Core;
using Ninject.Framework.Web;

using R3M.Integration.Ninject.Helper;

namespace R3M.Integration.Ninject.Web
{
    public class GlobalApp : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            IKernel kernel = new StandardKernel();

            INinjectModuleAutoLoader moduleAutoLoader = new NinjectModuleAutoLoader(kernel);
            moduleAutoLoader.InitAndLoadModulesForLoadedAssemblies();

            KernelLocator.Kernel = kernel;

            return kernel;  
        }
    }
}
