using System;
using System.ComponentModel;
using System.Web.UI;

namespace SEOToolSet.WebApp.Controls
{
    public partial class AsyncReport : UserControl
    {
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Browsable(false)]
        public ITemplate ItemTemplate { get; set; }

        private String _checkName;
        public String CheckName
        {
            get
            {
                return String.IsNullOrEmpty(_checkName) ? ReportTitle : _checkName;
            }
            set
            {
                _checkName = value;
            }
        }

        public String ReportTitle { get; set; }

        public String ReportTooltip { get; set; }

        public Int32 ReportType { get; set; }

        public String ReportIdentifier { get; set; }

        public String OnClientDataReceived { get; set; }

        private String _reportName;
        
        public string ReportUrl { get; set;}

        public String ReportName
        {
            get
            {
                return String.IsNullOrEmpty(_reportName) ? "Untitled Report" : _reportName;
            }
            set
            {
                _reportName = value;
            }
        }



        protected void Page_Init()
        {
            if (ItemTemplatePlaceHolder == null) return;
            ItemTemplatePlaceHolder.Controls.Clear();
            if (ItemTemplate != null)
            {
                ItemTemplate.InstantiateIn(ItemTemplatePlaceHolder);
            }
        }

        private Control ItemTemplatePlaceHolder
        {
            get { return ItemTemplatePlaceHolder1; }
        }

        public string OnBeforeAjaxCall { get; set; }

        protected void Page_Load(Object sender, EventArgs e)
        {
        }
    }
}