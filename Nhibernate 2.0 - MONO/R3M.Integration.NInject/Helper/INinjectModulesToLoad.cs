using Ninject.Core;

namespace R3M.Integration.Ninject.Helper
{
    public interface INinjectModulesToLoad
    {
        IModule[] RequiredModules { get; }
    }
}