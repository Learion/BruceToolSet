#region Using Directives

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

#endregion

namespace R3M.Controls
{
    [ToolboxData("<{0}:CustomRepeater runat=server></{0}:CustomRepeater>")]
    public class CustomRepeater : Repeater
    {
        private Boolean _EmptyTemplateRendered;

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Browsable(false)]
        public ITemplate EmptyDataTemplate { get; set; }


        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            if (Items.Count != 0 || _EmptyTemplateRendered) return;
            _EmptyTemplateRendered = true;
            if (EmptyDataTemplate != null) EmptyDataTemplate.InstantiateIn(this);
        }


        protected override void OnItemCreated(RepeaterItemEventArgs e)
        {
            var shouldRenderEmptyTemplate = Items == null || Items.Count == 0;

            if (e.Item.ItemType != ListItemType.Footer) return;
            if (shouldRenderEmptyTemplate)
            {
                _EmptyTemplateRendered = true;
                if (EmptyDataTemplate != null) EmptyDataTemplate.InstantiateIn(this);
            }
            base.OnItemCreated(e);
        }
    }
}