using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEOToolSet.Common;

namespace SEOToolSet.WebApp
{
    public partial class ArrayRenderer : UserControl
    {
        public String ItemPath { get; set; }
        public String ContainerTagName { get; set; }
        public String HeaderPath { get; set; }

        #region Properties

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate HeaderTemplate { get; set; }

        public ArrayRendererType TypeOfRenderer { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate ItemTemplate { get; set; }

        [DefaultSettingValue("RowHeader")]
        public String HeaderCssClass { get; set; }


        [DefaultSettingValue("RowItem")]
        public String ItemCssClass { get; set; }

        [DefaultSettingValue("AlternatingRowItem")]
        public String AlternatingCssClass { get; set; }

        #endregion

        protected void Page_Load(Object sender, EventArgs e)
        {

        }

        protected void Page_Init()
        {
            //EnsureChildControls();
            ItemTemplatePlaceHolder.Controls.Clear();
            if (ItemTemplate != null)
            {
                ItemTemplate.InstantiateIn(ItemTemplatePlaceHolder);
            }

            HeaderTemplatePlaceHolder.Controls.Clear();

            if (HeaderTemplate != null)
            {
                HeaderTemplate.InstantiateIn(HeaderTemplatePlaceHolder);
            }
        }

        protected override void EnsureChildControls()
        {
            if (ItemTemplatePlaceHolder == null) ItemTemplatePlaceHolder = new PlaceHolder();
            if (HeaderTemplatePlaceHolder == null) HeaderTemplatePlaceHolder = new PlaceHolder();
            ;
        }
    }
}