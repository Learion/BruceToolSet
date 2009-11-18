using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using SEOToolSet.Common;

namespace SEOToolSet.WebApp.Controls
{
    public partial class MonthYearPicker : UserControl
    {
        #region Properties
        public int Years { get; set; }
        public string StartMonthYear
        {
            get
            {
                return ViewState["StartMonthYear"].ToString();
            }
            set
            {
                ViewState["StartMonthYear"] = value;
            }
        }
        public DateTime? DateSelected
        {
            get
            {
                return string.IsNullOrEmpty(MonthYearSelected.Value) ? null : WebHelper.ParseDateWithMonthAndYear(MonthYearSelected.Value);
            }
            set
            {
                if (value == null) return;
                MonthYearSelected.Value = value.Value.ToString("MM/yyyy");
            }
        }
        public string DateSelectedText
        {
            get
            {
                return DateSelected.HasValue ? DateSelected.Value.ToString("MM/yyyy") : string.Empty;
            }
            set
            {
                DateTime date;
                if (!DateTime.TryParse(value, out date)) return;
                DateSelected = date;
            }
        }
        #endregion

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var monthOfYearTemplate = "<option value='{0}/{1}'>{2}</option>";
            var yearGroupTemplate = "<optgroup label='{0}'>";
            var monthsOfYear = new StringBuilder();
            var onLoadClientScript = new StringBuilder();
            onLoadClientScript.AppendFormat(@"$.onDomReady(function(){{");
            onLoadClientScript.AppendFormat(@"  $.getScript('{0}', function() {{", ResolveClientUrl("~/scripts/Controls/MonthPicker/selectToUISlider.js"));
            onLoadClientScript.AppendFormat(@"        var monthYearSelected = $('#{0}');", MonthYearSelected.ClientID);
            onLoadClientScript.AppendFormat(@"        $('#{0}')", ClientID);
            onLoadClientScript.AppendFormat(@"			.selectToUISlider({{");
            onLoadClientScript.AppendFormat(@"			    labels: '{0}',", Years);
            onLoadClientScript.AppendFormat(@"			    monthYearSubscriber: $('#{0}')", MonthYearSelected.ClientID);
            onLoadClientScript.AppendFormat(@"			}});");
            onLoadClientScript.AppendFormat(@"        $('#{0}').focus(", ClientID);
            onLoadClientScript.AppendFormat(@"				function() {{");
            onLoadClientScript.AppendFormat(@"				    $('#{0} ~div.ui-slider, #{1}').show('slow');", ClientID, HideCommand.ClientID);
            onLoadClientScript.AppendFormat(@"				}}");
            onLoadClientScript.AppendFormat(@"			);");
            onLoadClientScript.AppendFormat(@"        $('#{0} ~div.ui-slider').css('display', 'none');", ClientID);
            onLoadClientScript.AppendFormat(@"        $('#{0}').click(function(e) {{", HideCommand.ClientID);
            onLoadClientScript.AppendFormat(@"            $('#{0} ~div.ui-slider, #{1}').hide('slow');", ClientID, HideCommand.ClientID);
            onLoadClientScript.AppendFormat(@"        }});");
            onLoadClientScript.AppendFormat(@"    }});");
            onLoadClientScript.AppendFormat(@"}});");
            int startYear, startMonth;
            if (StartMonthYear == null)
            {
                startYear = DateTime.Now.Year;
                startMonth = DateTime.Now.Month;
            }
            else
            {
                if (!int.TryParse(StartMonthYear.Substring(3), out startYear))
                    startYear = DateTime.Now.Year;
                if (!int.TryParse(StartMonthYear.Substring(0, 2), out startMonth))
                    startMonth = DateTime.Now.Month;
            }
            //insert months of year inside combo
            for (var i = 0; i < Years; i++)
            {
                monthsOfYear.AppendFormat(yearGroupTemplate, startYear + i);
                for (var j = 0; j < 12; j++)
                {
                    if (i == 0 && j + 1 < startMonth) continue;
                    monthsOfYear.AppendFormat(monthOfYearTemplate, (j + 1).ToString().PadLeft(2, '0'), startYear + i,
                                              GetGlobalResourceObject("Months", Enum.GetName(typeof(Months), j)) + " " + (startYear + i).ToString().Substring(2));
                }
                monthsOfYear.AppendLine("</optgroup>");
            }
            MonthYearSelectionContent.Text = monthsOfYear.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), ClientID, onLoadClientScript.ToString(), true);
        }

    }

    internal enum Months
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
}