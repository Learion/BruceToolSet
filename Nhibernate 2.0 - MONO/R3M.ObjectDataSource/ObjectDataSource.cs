using System;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ninject.Core;
using R3M.Integration.Ninject.Web;

namespace R3M.Controls
{
    [ToolboxData("<{0}:ObjectDataSource runat=server></{0}:ObjectDataSource>")]
    public class ObjectDataSource : System.Web.UI.WebControls.ObjectDataSource
    {
        public ObjectDataSource()
        {
            ObjectCreating += ObjectDataSource_ObjectCreating;
        }

        void ObjectDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            if (KernelLocator.Kernel != null)
                e.ObjectInstance = KernelLocator.Kernel.Get(BuildManager.GetType(TypeName, false));
            else
            {
                throw new Exception("Kernel must not be null, Make sure your Global application inherits from GlobalApp located in R3M.Integration.Ninject.Web");
            }
        }
    }
}