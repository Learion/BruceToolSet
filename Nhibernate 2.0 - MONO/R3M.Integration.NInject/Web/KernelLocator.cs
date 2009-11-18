using Ninject.Core;

namespace R3M.Integration.Ninject.Web
{
    public class KernelLocator
    {
        private static IKernel _kernel;
        /// <summary>
        /// Null at the begin
        /// </summary>
        public static IKernel Kernel
        {
            get
            {
                return _kernel;
            }
            set
            {
                if (_kernel != null) return;
                _kernel = value;
            }

        }
    }
         
}