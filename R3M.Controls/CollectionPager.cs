#region Using Directives

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Web.UI;
using R3M.Controls.Properties;

#endregion

namespace R3M.Controls
{
    [DefaultProperty("DefaultPagePattern")]
    [ToolboxData("<{0}:CollectionPager runat=server></{0}:CollectionPager>")]
    public class CollectionPager : Control, IPostBackEventHandler, INamingContainer, IPostBackDataHandler
    {
        public const String ChangePage = "ChangePage";

        private const string ControlHTMLTemplate =
            "<div class='Control-Pager' id='{Control_ID}' {MAIN_CONTROL_PAGER_STYLE} > <div class='Pager-Resume'><span>{STRING_PATTERN_RESUME}</span> </div> <table> <tr> <td class='Pager-Cell' ><a id='{Control_ID}-First' class='Pager-Btn {BtnPageFirstClass}' href=\"javascript:{POST_BACK_FOR_FIRST};\" > <span> First</span> </a> </td> <td class='Pager-Cell'><a id='{Control_ID}-Prev' class='Pager-Btn {BtnPagePrevClass}' href=\"javascript:{POST_BACK_FOR_PREV};\" > <span>Prev</span> </a> </td> <td><span>Page</span> </td> <td class='Pager-Input-Cell' ><input class='txt_class' style='width:25px;' id='{Control_ID}_CurrentPageIndex' name='{InputFieldName}' type='text' onchange=\"javascript:{POST_BACK_INPUT}\" value='{TXT_VALUE}' {DISABLED} /></td> <td><span>Of {TOTAL_PAGE_COUNT}</span> </td> <td class='Pager-Cell'><a id='{Control_ID}-Next' class='Pager-Btn {BtnPageNextClass}' href=\"javascript:{POST_BACK_FOR_NEXT}\" > <span> Next</span> </a></td> <td class='Pager-Cell'><a id='{Control_ID}-Last' class='Pager-Btn {BtnPageLastClass}' href=\"javascript:{POST_BACK_FOR_LAST};\" > <span> Last</span> </a></td> </tr> </table></div>";

        public const String GoToFirst = "GoToFirst";
        public const String GoToLast = "GoToLast";
        public const String GoToNext = "GoToNext";
        public const String GoToPrev = "GoToPrev";


        [Category("Appearance")]
        [DefaultValue(0)]
        public Int32 TotalItemCount
        {
            get { return xDefault(ViewState["TotalItemCount"], 0); }
            set { ViewState["TotalItemCount"] = value; }
        }


        [Category("Appearance")]
        [DefaultValue("")]
        public String CssStyle
        {
            get { return xDefault(ViewState["CssStyle"], String.Empty); }
            set { ViewState["CssStyle"] = value; }
        }


        [Category("Appearance")]
        [DefaultValue(10)]
        public Int32 NumberOfItemsPerPage
        {
            get { return xDefault(ViewState["NumberOfItemsPerPage"], 10); }
            set { ViewState["NumberOfItemsPerPage"] = value; }
        }


        public Int32 DownLimit
        {
            get { return (NumberOfItemsPerPage*CurrentPageIndex) + 1; }
        }


        private string InputFieldName
        {
            get { return UniqueID + "_CurrentIndexPage"; }
        }

        public Int32 TopLimit
        {
            get
            {
                int tLimit = NumberOfItemsPerPage*(CurrentPageIndex + 1);
                return tLimit > TotalItemCount ? TotalItemCount : tLimit;
            }
        }

        public Int32 CurrentPage
        {
            get { return CurrentPageIndex + 1; }
        }

        [Browsable(false)]
        public Int32 CurrentPageIndex
        {
            get { return xDefault(ViewState["CurrentPageIndex"], 0); }
            set
            {
                if (value >= TotalPageCount)
                {
                    ViewState["CurrentPageIndex"] = TotalPageCount - 1;
                }
                else
                {
                    ViewState["CurrentPageIndex"] = value;
                }
            }
        }

        public Int32 TotalPageCount
        {
            get { return (TotalItemCount/NumberOfItemsPerPage) + ((TotalItemCount%NumberOfItemsPerPage > 0) ? 1 : 0); }
        }


        [Category("Appearance")]
        [DefaultValue("Pager.css")]
        public String StyleSheet
        {
            get { return xDefault(ViewState["StyleSheet"], "Pager.css"); }
            set { ViewState["StyleSheet"] = value; }
        }


        [Category("Appearance")]
        [DefaultValue("Page")]
        public String StringPatternForPage
        {
            get { return xDefault(ViewState["StringPatternForPage"], "Page"); }
            set { ViewState["StringPatternForPage"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue("Of {TOTAL_PAGE_COUNT}")]
        [Description("this Property represents the Text Pattern for the \"Of Pages\" Text of the Pager.")]
        public String StringPatternForOfPages
        {
            get { return xDefault(ViewState["StringPatternForOfPages"], "Of {TOTAL_PAGE_COUNT}"); }
            set { ViewState["StringPatternForOfPages"] = value; }
        }


        [Category("Appearance")]
        [DefaultValue("Displaying Items {DOWN_LIMIT} - {TOP_LIMIT} of {TOTAL_ITEM_COUNT}")]
        [Description("this Property represents the Text Pattern for the \"Of Pages\" Text of the Pager.")]
        public String StringPatternForResume
        {
            get
            {
                return xDefault(ViewState["StringPatternForResume"],
                                "Displaying Items {DOWN_LIMIT} - {TOP_LIMIT} of {TOTAL_ITEM_COUNT}");
            }
            set { ViewState["StringPatternForResume"] = value; }
        }


        [Category("Appearance")]
        [DefaultValue("No items to show")]
        [Description("this Property represents the Text Template for the EmptyTemplate.")]
        public String EmptyStringPages
        {
            get { return xDefault(ViewState["EmptyStringPages"], "No items to show"); }
            set { ViewState["EmptyStringPages"] = value; }
        }

        #region IPostBackDataHandler Members

        public virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            var changed = false;


            //Duplicate functionality of textbox since there is no way to call base class
            if (postCollection[InputFieldName] != null)
            {
                var changedPage = postCollection[InputFieldName];

                if (IsNumeric(changedPage))
                {
                    if (CurrentPage.ToString() != changedPage)
                    {
                        changed = true;
                        var changedIntPage = Convert.ToInt32(changedPage) - 1;

                        CurrentPageIndex = (changedIntPage < 0) ? 0 : changedIntPage;
                    }
                }
            }

            return changed;
        }

        public void RaisePostDataChangedEvent()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IPostBackEventHandler Members

        public void RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument == ChangePage)
            {
                //doChangePage(this.CurrentPageIndex);
            }
            if (eventArgument == GoToFirst)
            {
                doChangePage(0);
            }
            if (eventArgument == GoToPrev)
            {
                doChangePage(CurrentPageIndex - 1);
            }
            if (eventArgument == GoToNext)
            {
                doChangePage(CurrentPageIndex + 1);
            }
            if (eventArgument == GoToLast)
            {
                doChangePage(TotalPageCount - 1);
            }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresPostBack(this);
        }

        protected override void OnPreRender(EventArgs e)
        {
            WriteCSSAndJSIncludes();
            base.OnPreRender(e);
        }


        private static Control GetFirstCtrl(string sCtrlType, ControlCollection colControls)
        {
            Control oFirstCtrl = null;

            foreach (Control oCtrl in colControls)
            {
                //Check if control type matches sCtrlType 
                if (oCtrl.GetType().ToString() == sCtrlType)
                {
                    //Make sure the control is visible
                    if (oCtrl.Visible)
                        return oCtrl;
                    continue;
                }


                if (!oCtrl.HasControls()) continue;
                oFirstCtrl = GetFirstCtrl(sCtrlType, oCtrl.Controls);
                if (oFirstCtrl != null)
                    break;
            }

            return oFirstCtrl;
        }


        private bool IsTopControl()
        {
            Control oCtrl = GetFirstCtrl(GetType().ToString(), Page.Controls);
            if (oCtrl == null)
                return false;

            return oCtrl.UniqueID == UniqueID;
        }


        protected override void Render(HtmlTextWriter writer)
        {
            //WriteCSSAndJSIncludes();

            var showFirstPrev = (CurrentPageIndex > 0);
            var btnPageFirstClass = showFirstPrev ? "Page-First" : "Page-First-Disabled";
            var btnPagePrevClass = showFirstPrev ? "Page-Prev" : "Page-Prev-Disabled";

            var showNextLast = ((CurrentPageIndex + 1) < TotalPageCount);
            var btnPageNextClass = showNextLast ? "Page-Next" : "Page-Next-Disabled";
            var btnPageLastClass = showNextLast ? "Page-Last" : "Page-Last-Disabled";


            var htmlToBeWritten = ControlHTMLTemplate.Replace("{Control_ID}", ClientID);
            htmlToBeWritten = htmlToBeWritten.Replace("{InputFieldName}", InputFieldName);

            if (TotalPageCount > 0)
            {
                var resumeTemplate = StringPatternForResume.Replace("{DOWN_LIMIT}", DownLimit.ToString());
                resumeTemplate = resumeTemplate.Replace("{TOP_LIMIT}", TopLimit.ToString());
                resumeTemplate = resumeTemplate.Replace("{TOTAL_ITEM_COUNT}", TotalItemCount.ToString());

                htmlToBeWritten = htmlToBeWritten.Replace("{STRING_PATTERN_RESUME}", resumeTemplate);
            }
            else
            {
                htmlToBeWritten = htmlToBeWritten.Replace("{STRING_PATTERN_RESUME}", EmptyStringPages);
            }

            htmlToBeWritten = htmlToBeWritten.Replace("{TXT_VALUE}", CurrentPage.ToString());

            htmlToBeWritten = htmlToBeWritten.Replace("{TOTAL_PAGE_COUNT}", TotalPageCount.ToString());

            htmlToBeWritten = htmlToBeWritten.Replace("{BtnPageFirstClass}", btnPageFirstClass);
            htmlToBeWritten = htmlToBeWritten.Replace("{BtnPagePrevClass}", btnPagePrevClass);
            htmlToBeWritten = htmlToBeWritten.Replace("{BtnPageNextClass}", btnPageNextClass);
            htmlToBeWritten = htmlToBeWritten.Replace("{BtnPageLastClass}", btnPageLastClass);


            htmlToBeWritten = TotalPageCount > 1
                                  ? htmlToBeWritten.Replace("{DISABLED}", String.Empty)
                                  : htmlToBeWritten.Replace("{DISABLED}", "disabled=\"disabled\"");


            htmlToBeWritten = htmlToBeWritten.Replace("{POST_BACK_INPUT}",
                                                      Page.ClientScript.GetPostBackEventReference(this, ChangePage));
            htmlToBeWritten = htmlToBeWritten.Replace("{POST_BACK_FOR_FIRST}",
                                                      showFirstPrev
                                                          ? Page.ClientScript.GetPostBackEventReference(this, GoToFirst)
                                                          : "void(0)");
            htmlToBeWritten = htmlToBeWritten.Replace("{POST_BACK_FOR_PREV}",
                                                      showFirstPrev
                                                          ? Page.ClientScript.GetPostBackEventReference(this, GoToPrev)
                                                          : "void(0)");
            htmlToBeWritten = htmlToBeWritten.Replace("{POST_BACK_FOR_NEXT}",
                                                      showNextLast
                                                          ? Page.ClientScript.GetPostBackEventReference(this, GoToNext)
                                                          : "void(0)");
            htmlToBeWritten = htmlToBeWritten.Replace("{POST_BACK_FOR_LAST}",
                                                      showNextLast
                                                          ? Page.ClientScript.GetPostBackEventReference(this, GoToLast)
                                                          : "void(0)");

            htmlToBeWritten = htmlToBeWritten.Replace("{MAIN_CONTROL_PAGER_STYLE}", CssStyle);

            writer.Write(htmlToBeWritten);
            //base.Render(writer);
        }

        private void WriteCSSAndJSIncludes()
        {
            if (!IsTopControl()) return;

            var resolvedUrl = StyleSheet ?? Settings.Default.CollectionPagerCss;
            //(ResourcesDir.EndsWith("/") ? ResourcesDir : ResourcesDir + "/") + StyleSheet;

            Common.AddFileToPageHeader(Page, ResolveClientUrl(resolvedUrl), FileType.Css);
            //output.WriteLine("<link href='{0}' type='text/css' rel='stylesheet'>",);
            //output.WriteLine("<script type=\"text/javascript\">function __doKeyPress(e) { try { var xe = new xEvent(e); if (xe.keyCode == 13) { xStopEvent(e);xe.target.blur(); } } catch(ex) { } }</script>");
        }


        /// <summary>
        /// Generic Method to do special Casting to type T 
        /// </summary>
        /// <typeparam name="T">the Expected Type to be returned</typeparam>
        /// <param name="aValue">the Object with the value to be casted</param>
        /// <param name="aDefaultValue">the Default Value if aValue is null</param>
        /// <returns></returns>
        public static T xDefault<T>(Object aValue, T aDefaultValue)
        {
            if (aValue == null) return aDefaultValue;
            return (T) aValue;
        }

        public void doChangePage(int PageIndex)
        {
            CurrentPageIndex = PageIndex;
        }

        // This method returns a true if the passed in string contains only alphanumeric characters
        public static bool IsNumeric(string Text)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return false;
            }
            Text = Text.Trim();
            bool bResult = Regex.IsMatch(Text, @"^\d+$");
            return bResult;
        }

        // Defines the Click event.
        public event EventHandler Click;

        // Invokes delegates registered with the Click event.

        protected virtual void OnClick(EventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }
    }
}