using Ninject.Framework.Web;
using Ninject.Core;



namespace R3M.Integration.Ninject.Web
{
    public class WebPageBase : PageBase
    {
        [Inject]
        public IKernel Kernel;

    }
}
