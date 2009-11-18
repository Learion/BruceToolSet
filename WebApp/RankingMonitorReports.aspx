<%@ Page Title="" Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true"
    CodeBehind="RankingMonitorReports.aspx.cs" Inherits="SEOToolSet.WebApp.RankingMonitorReports"
    meta:resourcekey="PageResource1" %>

<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register Src="PageTitle.ascx" TagName="PageTitle" TagPrefix="uc1" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc2" %>
<%@ Register Src="~/ArrayRenderer.ascx" TagName="ArrayRenderer" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">
    <uc2:IncludeFile ID="IncludeFile11" runat="server" FilePath="~/css/RankingMonitorReports.css"
        TypeOfFile="Css" />
    <uc1:PageTitle ID="PageTitle1" meta:resourceKey="PageTitle1" PageTitleText="View Ranking Monitor Reports"
        PageDescription="View the data from a Ranking Monitor, or compare two Ranking Monitor reports from different dates"
        RenderRoundPanelStyles="false" runat="server" />
    <uc2:IncludeFile ID="IncludeFile2" FilePath="~/scripts/Controls/R3M.Combobox/cbx.css"
        TypeOfFile="Css" runat="server" />
    <uc2:IncludeFile ID="IncludeFile4" FilePath="~/scripts/plugins/jquery.scrollTo.js"
        TypeOfFile="Javascript" runat="server" />
    <uc2:IncludeFile ID="IncludeFile1" FilePath="~/scripts/Controls/R3M.Combobox/combobox.js"
        TypeOfFile="Javascript" runat="server" />
    <uc2:IncludeFile ID="IncludeFile7" FilePath="~/scripts/Controllers/Pages/PageBase.js"
        TypeOfFile="Javascript" runat="server" />
    <uc2:IncludeFile ID="IncludeFile3" FilePath="~/scripts/Controllers/Pages/RankingMonitorReports.js?x"
        TypeOfFile="Javascript" runat="server" />

    <script type="text/javascript">

        //Ranking Monitor Report Page
        var rmp = null;

        $(function() {
            rmp = new $.SEOToolSet.RankingMonitorReport({ RankingMonitorHandler: '<%= ResolveClientUrl("~/Handler/RankingMonitorHelper.ashx") %>',
                ProjectProfileHandler: '<%= ResolveClientUrl("~/Handler/ProjectProfileHelper.ashx") %>',
                RetrieveFileHandler: '<%= ResolveClientUrl("~/Handler/Report/") %>',
                cbxProxyFilterTooltip: '<%= GetLocalResourceObject("CbxProxyFilterTooltip") %>',
                cbxSearchEngineFilterTooltip: '<%=GetLocalResourceObject("CbxSearchEngineFilterTooltip") %>',
                ExportedReportTitleFor: '<%=GetLocalResourceObject("ExportedReportTitleFor") %>',
                ExportedTitleString: '<%=GetLocalResourceObject("ExportedTitleString") %>',
                ExportedDateString: '<%=GetLocalResourceObject("ExportedDateString") %>',
                ExportedPrimaryReportString: '<%=GetLocalResourceObject("ExportedPrimaryReportString") %>',
                ExportedComparisonReportString: '<%=GetLocalResourceObject("ExportedComparisonReportString") %>',
                ExportedTypeString: '<%=GetLocalResourceObject("ExportedTypeString") %>',
                ExportedReportTitle: '<%=GetLocalResourceObject("ExportedReportTitle") %>',
                ExportTypeSingle: '<%=GetLocalResourceObject("ExportTypeSingle") %>',
                ExportTypeComparison: '<%=GetLocalResourceObject("ExportTypeComparison") %>',
                ExportedCurrentViewString: '<%= GetLocalResourceObject("LocalizeCurrentView.Text") %>',
                ExportedFullDataString: '<%= GetLocalResourceObject("LocalizeFullData.Text") %>',
                NoneSelectedString: '<%= GetLocalResourceObject("NoneSelected") %>',
                NoProxyString: '<%= GetLocalResourceObject("NoProxy") %>',
                AllProxiesString: '<%= GetLocalResourceObject("AllProxies") %>',
                AllEnginesString: '<%= GetLocalResourceObject("AllEngines") %>',
                RunReportUI: '<%= RoundPanel1.ClientID %>'
            });

            rmp.addEventListener('Error', function(args) {
                $.showBigDialog($.stringFormat('<p>{0}</p>', args.errorMessage), {
                    icon: $.MessageIco.Warning,
                    title: 'UI Error'
                });
            });

            rmp.addEventListener('NoReportsFound', function(a) {
                $.showBigDialog('<p><%= GetGlobalResourceObject("DialogMessages","RankingReportsNotFound") %></p>' +
                    '<p><%= GetGlobalResourceObject("DialogMessages", "AssistanceMessage") %></p>',
                        {
                            title: '<%= GetGlobalResourceObject("DialogMessages", "DataNotFoundTitle") %>',
                            icon: $.MessageIco.Warning,
                            buttons: [  { text: '<%= Resources.CommonTerms.Back %>', command: 'Back' },
                                        { text: '<%= Resources.DialogMessages.ShowDashboard %>', command: 'ShowDashBoard' }
                                      ],
                            showCancel: false,
                            showOk: false,
                            onCommand: function(args) {
                                if (args.command == 'ShowDashBoard') {
                                    location.href = '<%= ResolveClientUrl("~/RankingMonitorDashboard.aspx") %>';
                                }
                                else if (args.command == 'Back') {
                                    if (a.idPreviousProject && a.idPreviousProject > 0 && a.idPreviousProject != $.CurrentPage.cbxProjects.getDropDown().val()) {
                                        
                                        $.CurrentPage.cbxProjects.setValueTo(a.idPreviousProject, true);
                                    }
                                    else {
                                        history.go(-1);
                                    }
                                }
                            }

                        });
            });
            rmp.addEventListener('NoActiveProjects', function(a) {
                $.showBigDialog('<p><%= GetGlobalResourceObject("DialogMessages", "NoProjectActive") %></p>',
                {
                    title: '<%= GetGlobalResourceObject("DialogMessages","ProjectSelectionError") %>',
                    icon: $.MessageIco.Info,
                    showCancel: false,
                    showOk : false,
                    buttons: [
                                {text: '<%= Resources.CommonTerms.Back %>', command: 'Back'} ,
                                {text: '<%= GetGlobalResourceObject("DialogMessages","AddNewProject") %>',command: 'AddNew'}
                              ],
                    onCommand: function(args) {
                        if (args.command == 'AddNew') {
                            location.href = '<%= ResolveClientUrl("~/RankingMonitorDashboard.aspx") %>';
                        }
                        if (args.command == 'Back') {                                                        
                                history.go(-1);
                            
                        }
                    }
                });
            });

            rmp.setCSVHiddenID('<%=CSVData.ClientID %>');
            rmp.setFileNameHiddenId('<%=FileNameHidden.ClientID %>');
            rmp.init();
            
            //$('.CollapsablePanel').collapsePanel({ headerElement:'.Legend', contentElement: '.CollapsibleArea' })                        
            $('.CollapsablePanel').collapsePanel();
            
            
        });
    </script>

    <div class="RankingReportsWrapper">
        <div class="UIWrapper">
            <cc1:RoundPanel ID="RoundPanel1" runat="server">
                <div class="CollapsablePanel">
                    <div class="Legend">
                        <h2>
                            <asp:Localize ID="LocalizeReportSelection" meta:resourceKey="LocalizeReportSelection"
                                runat="server" Text="Report Selection"></asp:Localize></h2>
                        <a class="CollapseTrigger" href="#"><span>Collapse</span></a>
                    </div>
                    <div class="CollapsibleArea">
                        <div class="ReportUI">
                            <ul class="ReportSelector">
                                <li class="Selector"><span class="BlueDot">&#x2022;</span>
                                    <label>
                                        <asp:Localize ID="LocalizeReportToView" runat="server" Text="Report to View:" meta:resourceKey="LocalizeReportToView"></asp:Localize></label>
                                    <div class="SelectWrapper">
                                        <select id="ReportToviewDropDown">
                                        </select>
                                    </div>
                                </li>
                                <li class="Selector"><span class="GreenDot">&#x2022;</span>
                                    <label>
                                        <asp:Localize meta:resourceKey="LocalizeReportToCompare" ID="LocalizeReportToCompare"
                                            runat="server" Text="Report to Compare (optional):"></asp:Localize>
                                    </label>
                                    <div class="SelectWrapper">
                                        <select id="ReportToCompare">
                                        </select>
                                    </div>
                                </li>
                            </ul>
                            <div class="SearchEnginesSelector">
                                <label>
                                    <asp:Localize ID="LocalizeSearchEnginesToDisplay" meta:resourceKey="LocalizeSearchEnginesToDisplay"
                                        runat="server" Text="Seach Engines to Display"></asp:Localize></label>
                                <div id="SearchEnginesList">
                                    <uc3:ArrayRenderer ID="SearchEnginesRenderer" ItemPath="ul" runat="server" TypeOfRenderer="Free"
                                        ContainerTagName="ul">
                                        <ItemTemplate>
                                            <ul>
                                                <li>
                                                    <div class="SearchEngine">
                                                        <div class="{IsPresent}">
                                                            <img class='DynamicImage Logo' image_src="{SearchEngineLogoUrl|default:''}" alt='{SearchEngineName}'
                                                                title='{SearchEngineName}' />
                                                            <span class="Engine">{SearchEngineName} ({SearchEngineUrl})</span> <span class="BlueDot {BlueClass}">
                                                                &#x2022;</span> <span class="GreenDot {GreenClass}">&#x2022;</span> <a href='##'
                                                                    class='xClose' title='<%= GetGlobalResourceObject("CommonTerms","RemoveItem") %>'
                                                                    attr_id='{Id}'><span>x</span></a>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </ItemTemplate>
                                    </uc3:ArrayRenderer>
                                </div>
                                <ul class="SearchEngines">
                                </ul>
                                <div class="DoClear">
                                </div>
                                <div class="Line">
                                </div>
                                <div id="AddSearchEnginePanel">
                                    <label>
                                        <asp:Localize ID="LocalizeSearchEngineToAdd" meta:resourceKey="LocalizeSearchEngineToAdd"
                                            runat="server" Text="Search Engine to Add:"></asp:Localize></label>
                                    <div class="SelectWrapper AddEngine">
                                        <select id="AddEngineDropDown">
                                        </select>
                                        <cc1:HyperLinkRound meta:resourceKey="AddEngine" CssClass="button AddEngine" ID="addEngineLink"
                                            Text="Add Engine" runat="server"></cc1:HyperLinkRound>
                                        <div class="DoClear">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="DoClear">
                            </div>
                        </div>
                        <cc1:HyperLinkRound Width="180px" ID="ViewReportButton" meta:resourceKey="ViewReportButton"
                            CssClass="button ViewReport" Text="View Report" runat="server"></cc1:HyperLinkRound>
                        <div class="DoClear">
                        </div>
                    </div>
                </div>
            </cc1:RoundPanel>
        </div>
        <div class="ReportView">
            <cc1:RoundPanel ID="RoundPanel3" runat="server">
                <div class="Legend">
                    <h2>
                        <%=GetLocalResourceObject("ReportSummaryTitle") %>&nbsp;<span id="ReportName">Report
                            Name</span></h2>
                </div>
                <div class="ReportSummary">
                    <div class="xTable">
                        <uc3:ArrayRenderer ID="ArrayRenderer1" runat="server" HeaderCssClass="RowHeader"
                            ItemCssClass="AlternatingRowItem" AlternatingCssClass="RowItem">
                            <HeaderTemplate>
                                <table>
                                    <tbody>
                                        <tr class="[CLASS_NAME]">
                                            <th class="Sortable" field_name='SearchEngineName' data_type="String" title="Search engines monitored in the Ranking Monitor report selected as the Report to View">
                                                <%= GetGlobalResourceObject("CommonTerms","SearchEngine") %>
                                            </th>
                                            <th class="Sortable" field_name='PageRank' data_type="Number" title="PageRank value for the home page of the project’s domain (PageRank is Google’s calculation on a scale of 1 to 10 of the page’s relative importance on the Web, based on linking.)">
                                                <%= GetGlobalResourceObject("CommonTerms","PageRank") %>
                                            </th>
                                            <th class="Sortable" field_name='InboundLinks' data_type="Number" title="Number of links coming to the project domain and its alias domains, as reported by the search engine">
                                                <%= GetGlobalResourceObject("CommonTerms","InboundLinks") %>
                                            </th>
                                            <th class="Sortable" field_name='PagesIndexed' data_type="Number" title="Number of project Web pages in the search engine’s index">
                                                <%= GetGlobalResourceObject("CommonTerms","PagesIndexed") %>
                                            </th>
                                            <th title="Chart showing how well the project’s Web pages ranked in the highlighted search engine. Bars represent the percentage of keywords that yielded results in the top 10, 20, 30, 40 or 50. When the Totals row is highlighted, the chart shows the highest rankings across all the search engines queried. Blue bars reflect data from the primary Report to View, and green bars show data from the Report to Compare (optional)">
                                                <%= GetGlobalResourceObject("CommonTerms","KeywordRankingDistribution") %>
                                            </th>
                                        </tr>
                                    </tbody>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tbody>
                                        <tr class='clickable [CLASS_NAME]' image_src='{ResumeImage}'>
                                            <td class="SearchEngineColumn">
                                                <img class='DynamicImage' image_src='{SearchEngineLogo}' alt='{SearchEngineName}'
                                                    title='{SearchEngineName}' />
                                                <div class="SearchEngineLabel">
                                                    {SearchEngineUrl}</div>
                                            </td>
                                            <td>
                                                {PageRank}
                                            </td>
                                            <td>
                                                {InboundLinks}
                                            </td>
                                            <td>
                                                <div class="ArrowContainer">
                                                    {PagesIndexed}
                                                    <div class="SummaryArrow">
                                                    </div>
                                                </div>
                                            </td>
                                            <td class="MergeCell">
                                                <img id='ImageReport' title='Chart showing how well the project’s Web pages ranked in the highlighted search engine. Bars represent the percentage of keywords that yielded results in the top 10, 20, 30, 40 or 50. When the Totals row is highlighted, the chart shows the highest rankings across all the search engines queried. Blue bars reflect data from the primary Report to View, and green bars show data from the Report to Compare (optional)'
                                                    alt='Chart showing how well the project’s Web pages ranked in the highlighted search engine. Bars represent the percentage of keywords that yielded results in the top 10, 20, 30, 40 or 50. When the Totals row is highlighted, the chart shows the highest rankings across all the search engines queried. Blue bars reflect data from the primary Report to View, and green bars show data from the Report to Compare (optional)' />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ItemTemplate>
                        </uc3:ArrayRenderer>
                        <div class="ReportLabels">
                            <ul>
                                <li class="ReportToView"><span class="BlueDot">&#x2022;</span><span class="ReportDate"></span>
                                </li>
                                <li class="ReportToCompare"><span class="GreenDot">&#x2022;</span><span class="ReportDate"></span>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </cc1:RoundPanel>
            <cc1:RoundPanel ID="RoundPanelReportDetail" runat="server">
                <div class="Legend">
                    <h2>
                        <asp:Localize ID="LocalizeReportDetail" meta:resourceKey="LocalizeReportDetail" Text="Report Detail"
                            runat="server"></asp:Localize></h2>
                    <div class="CbxDiv">
                        <ul>
                            <li>
                                <label>
                                    <%= GetGlobalResourceObject("CommonTerms","SearchEngines") %>:
                                </label>
                            </li>
                            <li>
                                <select id="CbxSearchEnginesFilter">
                                </select>
                            </li>
                            <li>
                                <label>
                                    <%= GetGlobalResourceObject("CommonTerms","ProxyServers") %>:</label></li>
                            <li>
                                <select id="CbxProxyServers">
                                </select>
                            </li>
                        </ul>
                        <div class="DoClear">
                        </div>
                    </div>
                    <div id="DeepAnalisis" class="xTable">
                        <uc3:ArrayRenderer ID="ArrayRenderer2" runat="server" HeaderCssClass="RowHeader"
                            ItemCssClass="AlternatingRowItem" AlternatingCssClass="RowItem">
                            <HeaderTemplate>
                                <table>
                                    <tbody>
                                        <tr class='[CLASS_NAME]'>
                                            <th class='Sortable' field_name='Keyword' data_type='String' title='<%= GetLocalResourceObject("KeywordColumnTooltip") %>'>
                                                <%= GetGlobalResourceObject("CommonTerms","keywords") %>
                                            </th>
                                            <th class='Sortable' field_name='Activity' data_type='Number' title='<%= GetLocalResourceObject("ActivityColumnTooltip") %>'>
                                                <%= GetGlobalResourceObject("CommonTerms","Activity") %>
                                            </th>
                                            <th class='Sortable' field_name='GoogleResults' data_type='Number' title='<%= GetLocalResourceObject("GoogleResultsColumnTooltip") %>'>
                                                <%= GetGlobalResourceObject("CommonTerms","GoogleResults") %>
                                            </th>
                                            <th class='Sortable' field_name='CPC' data_type='Number' title='<%= GetLocalResourceObject("CPCColumnTooltip") %>'>
                                                <%= GetGlobalResourceObject("CommonTerms","CPC") %>
                                            </th>
                                            <th class='Sortable' field_name='AllInTitle' data_type='Number' title='<%= GetLocalResourceObject("AllInTitleColumnTooltip") %>'>
                                                <%= GetGlobalResourceObject("CommonTerms", "AllInTitle")%>
                                            </th>
                                            <th>
                                                [EXTRA_HEADERS]
                                            </th>
                                        </tr>
                                    </tbody>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tbody>
                                        <tr class='[CLASS_NAME]'>
                                            <td title='<%= GetLocalResourceObject("KeywordColumnTooltip") %>' class='Keyword Left {StatusClass}'>
                                                {Keyword}
                                            </td>
                                            <td title='<%= GetLocalResourceObject("ActivityColumnTooltip") %>'>
                                                {Activity|default:'-'|numberReadable:','}
                                            </td>
                                            <td title='<%= GetLocalResourceObject("GoogleResultsColumnTooltip") %>'>
                                                {GoogleResults|default:'-'|numberReadable:','}
                                            </td>
                                            <td title='<%= GetLocalResourceObject("CPCColumnTooltip") %>'>
                                                {CPC|numberFormat:'2'|default:'-'|format:'$ [0]'}
                                            </td>
                                            <td title='<%= GetLocalResourceObject("AllInTitleColumnTooltip") %>'>
                                                {AllInTitle|default:'-'|numberReadable:','}
                                            </td>
                                            <td>
                                                [EXTRA_COLUMNS]
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ItemTemplate>
                        </uc3:ArrayRenderer>
                    </div>
                    <p>
                        <asp:Localize ID="LocalizeDetailedReportLegend" meta:resourceKey='LocalizeDetailedReportLegend'
                            Text='NR = Not ranked in the top 50 search results. Dash = No data. Red keywords = Search query failed'
                            runat="server"></asp:Localize>
                    </p>
                    <div class="PrintButtons">
                        <ul>
                            <li>
                                <cc1:HyperLinkRound meta:resourceKey="PrintReport" CssClass="button PrintReport"
                                    ID="HyperLinkPrintReport" Text="Print Report" ToolTip='Print the Report Detail table using your printer'
                                    runat="server"></cc1:HyperLinkRound></li>
                            <li>
                                <cc1:LinkButtonRound meta:resourceKey="PrintToPDF" CssClass="button PrintToPDF" ID="LinkButtonPrintToPDF"
                                    ToolTip='Create a PDF file of the current Report Detail table that can be saved'
                                    Text="Print to PDF" runat="server"></cc1:LinkButtonRound>
                                <%--<label for="chkUseSiberix">
                                    Use Siberix
                                    <input id="chkUseSiberix" type="checkbox" /></label>--%>
                            </li>
                            <li class="last">
                                <ul class="RadioSelectors">
                                    <li>
                                        <input id="radioCurrentView" value="CurrentView" checked="checked" type="radio" name="ExportType" /></li>
                                    <li>
                                        <label for="radioCurrentView">
                                            <asp:Localize ID="LocalizeCurrentView" meta:resourceKey="LocalizeCurrentView" Text="Current View"
                                                runat="server"></asp:Localize>
                                        </label>
                                    </li>
                                    <li>
                                        <input id="radioFullData" value="FullData" type="radio" name="ExportType" /></li>
                                    <li>
                                        <label for="radioFullData">
                                            <asp:Localize ID="LocalizeFullData" meta:resourceKey="LocalizeFullData" Text="Full Data"
                                                runat="server"></asp:Localize>
                                        </label>
                                    </li>
                                    <li>
                                        <cc1:LinkButtonRound meta:resourceKey="ExportToCSV" Text="Export to CSV" OnClick="ExportToCSV"
                                            ToolTip="Export the data to a comma-separated value (.CSV) file that can be opened with any report tool such as Microsoft Excel, Crystal Reports, etc. Choose Full Data to export all data gathered by the Ranking Monitor, or choose Current View to export only the data columns currently showing in the Report Detail table"
                                            ID="LinkButtonRound1" CssClass="button PrintToCSV" runat="server"></cc1:LinkButtonRound>
                                    </li>
                                    <li>
                                        <cc1:LinkButtonRound meta:resourceKey="ExportToExcel" Text="Export to CSV For Excel"
                                            OnClick="ExportToCSVForExcel" ToolTip="Export the data to a comma-separated value (.CSV) file that can be opened with any report tool such as Microsoft Excel, Crystal Reports, etc. Choose Full Data to export all data gathered by the Ranking Monitor, or choose Current View to export only the data columns currently showing in the Report Detail table"
                                            ID="LinkButtonRound2" CssClass="button PrintToCSV" runat="server"></cc1:LinkButtonRound>
                                    </li>
                                </ul>
                            </li>
                            <asp:HiddenField ID="CSVData" runat="server" Value="" />
                            <asp:HiddenField ID="FileNameHidden" runat="server" Value="" />
                        </ul>
                        <div class="DoClear">
                        </div>
                    </div>
                </div>
            </cc1:RoundPanel>
        </div>
    </div>
</asp:Content>
