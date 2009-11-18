<%@ Page Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true" CodeBehind="Pixelsilk.aspx.cs"
    Inherits="SEOToolSet.WebApp.Pixelsilk" Title="Pixelsilk" %>

<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register Src="PageTitle.ascx" TagName="PageTitle" TagPrefix="uc6" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<%@ Register Src="Controls/Popup.ascx" TagName="Popup" TagPrefix="uc2" %>
<%@ Register Src="Controls/ToolTip.ascx" TagName="ToolTip" TagPrefix="uc3" %>
<%@ Register TagPrefix="user" TagName="FileSelector" Src="FileSelector.ascx" %>
<%@ Register TagPrefix="user" TagName="AsyncReport" Src="Controls/AsyncReport.ascx" %>
<%@ Register Src="ArrayRenderer.ascx" TagName="ArrayRenderer" TagPrefix="uc4" %>
<%@ Register TagPrefix="user" TagName="WordPhrases" Src="WordPhrases.ascx" %>
<%@ Register TagPrefix="user" TagName="SubReportSelector" Src="SubReportSelector.ascx" %>
<%@ Register TagPrefix="user" TagName="SubReportHeader" Src="SubReportHeader.ascx" %>
<%@ Register Src="ReportPageConfigurator.ascx" TagName="ReportPageConfigurator" TagPrefix="uc5" %>
<asp:Content ID="Content2" ContentPlaceHolderID="contentArea" runat="server">
    <uc2:Popup ID="PopUp1" runat="server" />
    <uc3:ToolTip ID="ToolTip1" runat="server" />
    <!-- Control Used for the Page Title -->
    
    <uc6:PageTitle ID="PageTitle1" PageTitleText="Pixelsilk Integration" 
        PageDescription="Get Recommended Keyword Configurations and push them to Pixelsilk"
        runat="server" RenderRoundPanelStyles="false" />
        
    <!-- Main application scripts -->
    <uc5:ReportPageConfigurator ID="ReportPageConfigurator1" runat="server" />
    <asp:ObjectDataSource ID="odsKeywordList" TypeName="SEOToolSet.Providers.ProjectManager"
        SelectMethod="GetKeywordLists" runat="server">
        <SelectParameters>
            <asp:ControlParameter ControlID="__Page" Name="idProject" PropertyName="IdProject"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <cc1:RoundPanel ID="RoundPanel1" runat="server">
        <div id="pInfo">
            <div id="PageSelector">
                <div id='<%= ClientID %>' class='FileSelector'>
                    <div class="FormRow">
                        <div class="FormSelector">
                            <label class="LabelItem" for="kwdSelector">
                                <asp:Literal ID="ltl" Text='Select a Keyword:' runat="server" meta:resourcekey="KeywordSelction"></asp:Literal>
                            </label>
                            <select class="FormText" id="kwdSelector" name="kwdSelector">
                                <option value=""></option>
                                <cc1:CustomRepeater id="kwdOptGroupRepeater" DataSourceID='odsKeywordList' runat='server'>
                                    <ItemTemplate>
                                        <optgroup label='<%# Eval("Name") %>'></optgroup>
                                        <cc1:CustomRepeater ID="kwdOptionRepeater" DataSource='<%# Eval("Keyword") %>' runat='server'>
                                            <ItemTemplate>
                                                <option value='<%# Eval("Keyword") %>'><%# Eval("Keyword") %></option>
                                            </ItemTemplate>
                                        </cc1:CustomRepeater>
                                    </ItemTemplate>
                                </cc1:CustomRepeater>
                            </select>
                            <input type="hidden" class="FormText" id="txtUrlFile" name="txtUrlFile" runat="server" />
                            <br />
                            <div class="DoClear">
                            </div>
                            <label class="LabelItem" for="pKey">
                                <asp:Literal ID="ltl1" Text="Pixelsilk Key:" runat='server'></asp:Literal>
                            </label>
                            <input type="text" class="FormText" id="pKey" name="pKey" />
                        </div>
                        
                        <div class="ButtonSelector">
                            
                        </div>
                        <div class="DoClear">
                        </div>
                    </div>
                </div>
            </div>
            <user:SubReportSelector ID="WebUserControl02" runat="server"></user:SubReportSelector>
        </div>
    </cc1:RoundPanel>
    <div id="ReportsSection">

        <script type="text/javascript">
            $(document).ready(function() {
                $("#kwdSelector").change(function() {
                    $("input[id*='txtUrlFile']:first").val($(this).val());
                });
                
            });
            function AsyncReport_dataReceived (report, data) {                                                
                 try   {
                    //getArrayRendererByIndex gets the ArrayRenderer inside the Report that has the index given
                    //could be retrieved by name too, but its easier to retrieve them using only the index
                    //the order of the renderers should be managed carefully, if the designer moves the arrayrenderer, the index may change.
                    var renderer = report.getArrayRendererByIndex(0);                                                           
                    
                    //adds the dataSource for this rendererer 
                    //the renderer expects an array so in the cases where the object retrieved is not an array it may be converted first to an array
                    renderer.setDataSource(data.Criteria);
                    
                    //Populate the renderer templates
                    renderer.dataBind();
                }
                catch(ex) {
                    console.log(ex.message);
                }
            }
            function WordPhrases_dataReceived(report, data) {
                try {
                    //Report WordPhrases     
                    //Here could be up to 4 reports in this page                    
                    for (var ix = 0; ix < 4; ix++) {
                        //var rendererCount = report.getArrayRendererByIndex(ix * 2 );
                        var rendererPhrases = report.getArrayRendererByIndex(ix); // * 2) + 1);                        
                        if (!data.Phrases[ix]) {
                            //not data for that kind of report
                            report.ReportDiv.find('.Phrases:eq(' + ix + ')').hide();
                            continue;
                        }
                        if (rendererPhrases) //make sure the Renderers exist in the page  
                        {
                            rendererPhrases.setDataSource(data.Phrases[ix].Keywords);
                            rendererPhrases.onDataBound = $.createDelegateExpanded(rendererPhrases, function(e, index) {
                                try {
                                    if (!e || !e.renderedHTML || !data.Phrases[index].Count)
                                    { return; }
                                    var tokkenRegex = /\{-.+?-\}/g;
                                    var tokens = e.renderedHTML.match(tokkenRegex) || [];

                                    for (var j = 0; j < tokens.length; j++) {
                                        var valueForCell = rendererPhrases.getMatchkey(data.Phrases[index].Count, tokens[j].replace(/-/g, ''));
                                        e.renderedHTML = e.renderedHTML.replace(tokens[j], valueForCell);
                                    }
                                }
                                catch (ex) {
                                    console.log("[Error] : " + ex);
                                }

                            }, ix);
                            //Default Sort order - AGL 7/14/09
                            rendererPhrases.orderBy('AllWords.Counter', 'DESC', 'Number');
                            rendererPhrases.dataBind();
                        }
                    }
                    //Add the DESC class to each word phrases table by default - AGL 7/15/09
                    $("div[id*='ArrayRenderer9']").each(function() {
                            $(this).find("th[field_name*=AllWords]").addClass("DESC");
                    });
                    $("#lnkRun2").show('slow');
                }
                catch (ex) {
                    console.log(ex.message);
                }
            }
            function beforeCallReportUrl(evt) {
                $("#lnkRun2").hide();
                //var page_url = $("input[id*='txtUrlFile']:first").val();
                var kwd = encodeURI($("select[id*='kwdSelector']:first").val());
                //var cpId = $.CurrentPage.cbxProjects.getDropDown().val();
                //var lang = $.CurrentPage.cbxLanguages.getDropDown().val();
                //if (lang == 'choose') {
                //    lang = 'en_US';
                //}
                //evt.ReportUrl = evt.report.ReportUrl + '?pid=' + $.SinglePage.IdProcess + '&project=' + cpId + '&lang=' + lang );
                //evt.ReportUrl = $.stringFormat('{0}?pid={1}&project={2}&lang={3}&jsonp=?', evt.report.ReportUrl, $.SinglePage.IdProcess, cpId, lang);                
                evt.ReportUrl = $.stringFormat('{0}?keyword={1}&jsonp=?', evt.report.ReportUrl, kwd);
            }                                                    
            
         
        </script>

        <div id="SubReportContainer" style="display: none;">
            <!-- <user:SubReportHeader TitleText="Site Checker Reports" ID="SiteCheckerReportsHeader"
                runat="server" meta:resourcekey="SiteCheckerReportsHeader"></user:SubReportHeader> -->
            <!-- Reports Section -->
            <user:AsyncReport ReportUrl="http://bclaydev.bruceclay.com/services/pixelsilk.py/GetRecommendedValues"  
                OnBeforeAjaxCall="beforeCallReportUrl"   ReportTitle='Word Phrases' CheckName='Recommended Configuration' 
                ReportTooltip='Provides a detailed breakdown of where keywords appear on the page.'
                ReportType="4" ReportIdentifier="WordPhrasesReporter" OnClientDataReceived='WordPhrases_dataReceived'
                ID="AsyncReport4" runat="server" meta:resourcekey="WordPhrasesReport">
                <ItemTemplate>
                    <user:WordPhrases TitleText="Recommended Phrases" ID="WebUserControl02" runat="server">
                    </user:WordPhrases>
                </ItemTemplate>
            </user:AsyncReport>
            <a class="LinkCommandRound Big" href="javascript:void(0);" id="lnkRun2">
                <asp:Label ID="runCommand1" runat="server" Text="Push to Pixelsilk"></asp:Label>
            </a>
        </div>
    </div>
</asp:Content>
