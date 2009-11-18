<%@ Page Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true" CodeBehind="SiteChecker.aspx.cs"
    Inherits="SEOToolSet.WebApp.SiteChecker" Title="Site Checker" %>

<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register TagPrefix="user" TagName="PageTitle" Src="PageTitle.ascx" %>
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
    <user:PageTitle PageTitleText="Site Checker" PageDescription="Analyze headers of the Server"
        ID="PageHeading" runat="server" meta:resourcekey="PageHeading"></user:PageTitle>
    <!-- Main application scripts -->
    <uc5:ReportPageConfigurator InitReportsUrl="ReportServicesURL" ID="ReportPageConfigurator1" runat="server" />
    <cc1:RoundPanel ID="RoundPanel1" runat="server">
        <div id="pInfo">
            <div id="PageSelector">
                <user:FileSelector ID="WebUserControl01" HideUploadChoice="true" runat="server">
                </user:FileSelector>
            </div>
            <user:SubReportSelector ID="WebUserControl02" runat="server"></user:SubReportSelector>
        </div>
    </cc1:RoundPanel>
    <div id="ReportsSection">

        <script type="text/javascript">
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
            function beforeCallReportUrl(evt) {
                var page_url = $("input[id*='txtUrlFile']:first").val();
                var cpId = $.CurrentPage.cbxProjects.getDropDown().val();
                var lang = $.CurrentPage.cbxLanguages.getDropDown().val();
                if (lang == 'choose') {
                    lang = 'en_US';
                }
                //evt.ReportUrl = evt.report.ReportUrl + '?pid=' + $.SinglePage.IdProcess + '&project=' + cpId + '&lang=' + lang );
                //evt.ReportUrl = $.stringFormat('{0}?pid={1}&project={2}&lang={3}&jsonp=?', evt.report.ReportUrl, $.SinglePage.IdProcess, cpId, lang);                
                evt.ReportUrl = $.stringFormat('{0}?url={1}&lang={2}&jsonp=?', evt.report.ReportUrl, page_url, lang);
            }                                                    
            
         
        </script>

        <div id="SubReportContainer" style="display: none;">
            <user:SubReportHeader TitleText="Site Checker Reports" ID="SiteCheckerReportsHeader"
                runat="server" meta:resourcekey="SiteCheckerReportsHeader"></user:SubReportHeader>
            <!-- Reports Section -->
            <user:AsyncReport ReportTitle='Site Info' ReportUrl='http://bclaydev.bruceclay.com/services/sitechecker.py/GetSiteInfo' 
                OnBeforeAjaxCall="beforeCallReportUrl" CheckName='Site Info' 
                ReportTooltip='Provides information on how the site will be indexed.' ID="AsyncReport1"
                ReportType="8" OnClientDataReceived="AsyncReport_dataReceived" runat="server" 
                ReportIdentifier="SiteInfoReporter" meta:resourcekey="SiteInfoReport">
                <ItemTemplate>
                    <div class='xTable'>
                        <uc4:ArrayRenderer ID="ArrayRenderer1" runat="server" HeaderCssClass="RowHeader"
                            ItemCssClass="RowItem" AlternatingCssClass="AlternatingRowItem">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tbody>
                                        <tr class='[CLASS_NAME]'>
                                            <td class="Left Keyword" style="width: 200px;">
                                                {CheckedCriterion}
                                            </td>
                                            <td style="text-align: left">
                                                {Description}
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ItemTemplate>
                        </uc4:ArrayRenderer>
                    </div>
                </ItemTemplate>
            </user:AsyncReport>
            <user:AsyncReport ReportTitle='Header Info' ReportUrl='http://bclaydev.bruceclay.com/services/sitechecker.py/GetHeaderInfo' 
                OnBeforeAjaxCall="beforeCallReportUrl" CheckName='Header Info' 
                ReportTooltip='Displays the response header information' ID="AsyncReport2"
                ReportType="9" OnClientDataReceived="AsyncReport_dataReceived" runat="server" 
                ReportIdentifier="HeaderInfoReporter" meta:resourcekey="HeaderInfoReport">
                <ItemTemplate>
                    <div class='xTable'>
                        <uc4:ArrayRenderer ID="ArrayRenderer1" runat="server" HeaderCssClass="RowHeader"
                            ItemCssClass="RowItem" AlternatingCssClass="AlternatingRowItem">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tbody>
                                        <tr class='[CLASS_NAME]'>
                                            <td class="Left Keyword" style="width: 200px;">
                                                {CheckedCriterion}
                                            </td>
                                            <td style="text-align: left">
                                                {Description}
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ItemTemplate>
                        </uc4:ArrayRenderer>
                    </div>
                </ItemTemplate>
            </user:AsyncReport>
            <user:AsyncReport ReportTitle='Cloak Check' CheckName='Cloak Check Info' ReportUrl='http://bclaydev.bruceclay.com/services/sitechecker.py/GetCloakingCheck'
                OnBeforeAjaxCall="beforeCallReportUrl" ReportTooltip='Checks and displays results for user-agent based cloaking.'
                ReportType="10" ReportIdentifier="CloakCheckInfoReporter" OnClientDataReceived='AsyncReport_dataReceived' ID="AsyncReport5"
                runat="server" meta:resourcekey="CloakCheckInfoReport">
                <ItemTemplate>
                    <div class='xTable'>
                        <uc4:ArrayRenderer ID="ArrayRenderer2" runat="server" HeaderCssClass="RowHeader"
                            ItemCssClass="RowItem" AlternatingCssClass="AlternatingRowItem">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tbody>
                                        <tr class='[CLASS_NAME]'>
                                            <td style="text-align: left; width: 200px;">
                                                <img class='DynamicImage' image_src='css/Assets/{CheckedCriterion}-favicon.png' />
                                                <!-- {CheckedCriterion} -->
                                            </td>
                                            <td style="text-align: left;">
                                                {Description}
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ItemTemplate>
                        </uc4:ArrayRenderer>
                    </div>
                    <div class="DoClear">
                    </div>
                </ItemTemplate>
            </user:AsyncReport>
        </div>
    </div>
</asp:Content>
