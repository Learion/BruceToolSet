<%@ Page Title="" Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true"
    CodeBehind="RankingMonitorRun.aspx.cs" Inherits="SEOToolSet.WebApp.RankingMonitorRun" %>

<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<%@ Register Src="PageTitle.ascx" TagName="PageTitle" TagPrefix="uc2" %>
<%@ Register Src="~/ArrayRenderer.ascx" TagName="ArrayRenderer" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">
    <uc1:IncludeFile ID="IncludeFile2" FilePath="~/scripts/plugins/jQuery.CheckedTree/checktree.css"
        TypeOfFile="Css" runat="server" />
    <uc1:IncludeFile ID="IncludeFile3" FilePath="~/scripts/plugins/jQuery.CheckedTree/jquery.checktree.js"
        TypeOfFile="Javascript" runat="server" />
    <uc1:IncludeFile ID="IncludeFile4" FilePath="~/scripts/Controllers/CheckedTreeHelper.js"
        TypeOfFile="Javascript" runat="server" />
    <uc1:IncludeFile ID="IncludeFile5" FilePath="~/css/RankingMonitorConfiguration.css"
        TypeOfFile="Css" runat="server" />
    <uc1:IncludeFile ID="IncludeFile1" FilePath="~/css/RankingMonitorRun.css" TypeOfFile="Css"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile6" FilePath="~/scripts/Controls/R3M.ProgressBar/jProgressBar.js"
        TypeOfFile="Javascript" runat="server" />
    <uc1:IncludeFile ID="IncludeFile7" FilePath="~/scripts/Controls/R3M.ProgressBar/ProgressBar.css"
        TypeOfFile="Css" runat="server" />
    <uc1:IncludeFile ID="IncludeFile9" FilePath="~/scripts/Controllers/Pages/PageBase.js"
        TypeOfFile="Javascript" runat="server" />
    <uc1:IncludeFile ID="IncludeFile8" FilePath="~/scripts/Controllers/Pages/RankingMonitorRun.js"
        TypeOfFile="Javascript" runat="server" />

    <script language="javascript" type="text/javascript">
        var rmr = null;
        var assistanceMessage = '<p><%= GetGlobalResourceObject("DialogMessages", "AssistanceMessage") %></p>';
        $(document).ready(function() {
            rmr = new $.SEOToolSet.RankingMonitorRun({
                ProjectHandler: '<%= ResolveClientUrl("Handler/ProjectHelper.ashx") %>',
                RankingMonitorHandler: 'http://65.124.219.30:8000',
                MonitorConflictTextsObject: { text: '<p><%= GetLocalResourceObject("MonitorConflict") %></p>',
                    title: '<%= GetLocalResourceObject("MonitorConflictTitle") %>',
                    continueRunButton: '<%= GetLocalResourceObject("ContinueRun") %>',
                    startNewRunButton: '<%= GetLocalResourceObject("StartNewRun") %>'
                },
                ProfileErrorMessage: '<%= GetGlobalResourceObject("DialogMessages", "ProfileServiceFailed") %>',
                ProcessingText: '<%= GetGlobalResourceObject("CommonTerms", "Processing") %>'.replace(/\s/g, '&nbsp;'),
                RunMonitorCommandText: '<%= GetLocalResourceObject("RunMonitorCommand") %>'.replace(/\s/g, '&nbsp;'),
                ActivityTemplate: "<%= GetLocalResourceObject("SearchesForResultsField") %>"
            });

            rmr.addEventListener('Error', function(args) {
                if (args.errorCode == 'GeneralAjaxError') {
                    $.showBigDialog('<p><%= GetLocalResourceObject("ClientConnectionIssue") %></p>' + assistanceMessage,
                        {
                            title: '<%= Resources.DialogMessages.ClientConnectionIssueTitle %>',
                            icon: $.MessageIco.Info,
                            buttons: [
                                {
                                    text: '<%= GetLocalResourceObject("RedisplayPageCommand") %>',
                                    command: 'RedisplayPage'
}],
                            showCancel: false,
                            defaultButtonsBefore: false,
                            onCommand: function(args) {
                                if (args.command == 'RedisplayPage') {
                                    $.hideOverlay(function(){
                                        rmr.continueRun();
                                    });
                                }
                                else {
                                    $("#RankingResultSection").slideUp("fast");
                                    rmr.finishRankingMonitorRun();
                                }
                            }
                        });
                }
                else {
                    $.showBigDialog($.stringFormat('<p><%= Resources.DialogMessages.GeneralError %></p><p>{0}</p>', args.errorMessage), {
                        icon: $.MessageIco.Error,
                        title: 'UI Error'
                    });
                }
            });
            rmr.addEventListener('RankingMonitorFailed', function(args) {
                $.showBigDialog($.stringFormat('<p><%= GetLocalResourceObject("RankingMonitorFailed") %></p>', args.errorMessage ? args.errorMessage : '') + assistanceMessage,
                    {
                        title: '<%= GetLocalResourceObject("RankingMonitorFailedTitle") %>',
                        icon: $.MessageIco.Error,
                        buttons: [
                        {
                            text: '<%= GetGlobalResourceObject("DialogMessages", "Retry") %>',
                            command: 'Retry'
}],
                            showCancel: false,
                            defaultButtonsBefore: false,
                            onCommand: function(args) {
                                if (args.command == 'Retry') {
                                    $.hideOverlay(function(){
                                        rmr.continueRun();
                                    });
                                }
                            }
                        });
            });
            rmr.addEventListener('KeywordsNotSpecified', function(a) {
                $.showBigDialog('<p><%= GetGlobalResourceObject("DialogMessages", "KeywordsNotSpecified") %></p>',
                    {
                        title: '<%= GetGlobalResourceObject("DialogMessages", "KeywordsNotSpecifiedTitle") %>',
                        icon: $.MessageIco.Info,
                        buttons: [
                            {
                                text: '<%= GetGlobalResourceObject("CommonTerms","Back") %>',
                                command: 'Back'
                            },
                            {
                                text: '<%= GetGlobalResourceObject("DialogMessages", "ManageKeywords") %>',
                                command: 'ManageKeywords'
                            }],
                        showOk: false,
                        showCancel: false,
                        onCommand: function(args) {
                            if (args.command == 'ManageKeywords') {
                                location.href = $("#ManageKeywordsCommand").attr("href");
                            }
                            else if (args.command == 'Back') {
                                    history.go(-1);
                            }
                        }
                    });
            });

            rmr.addEventListener('NoActiveProjects', function(a) {
                $.showBigDialog('<p><%= GetGlobalResourceObject("DialogMessages", "NoProjectActive") %></p>',
                    {
                        title: '<%= GetGlobalResourceObject("DialogMessages","ProjectSelectionError") %>',
                        icon: $.MessageIco.Info,
                        showCancel: false,
                        buttons: [
                            {
                                text: '<%= GetGlobalResourceObject("CommonTerms","Back") %>',
                                command: 'Back'
                            },
                            {
                                text: '<%= GetGlobalResourceObject("DialogMessages","AddNewProject") %>',
                                command: 'AddNew'
                            }],
                        onCommand: function(args) {
                            if (args.command == 'AddNew') {
                                location.href = '<%= ResolveClientUrl("~/Home.aspx") %>';
                            }
                            else if (args.command == 'Back') {
                                if (a.idPreviousProject > 0 && a.idPreviousProject != $.CurrentPage.cbxProjects.getDropDown().val()) {
                                    $.CurrentPage.cbxProjects.setValueTo(a.idPreviousProject, true);
                                }
                                else {
                                    history.go(-1);
                                }
                            }
                        }
                    });
            });
            rmr.init();
        });
    </script>

    <uc2:PageTitle ID="PageTitle" RenderRoundPanelStyles="false" runat="server" PanelContainerVisible="true"
        meta:resourcekey="PageTitle" />
    <div id="KeywordsSection">
        <cc1:RoundPanel ID="KeywordsPanel" runat="server" DiscardBottom="False" DiscardTop="False"
            NotRenderStyles="False">
            <div id="DefaultKeywordsSection" class="Section">
                <div class="Legend">
                    <h2>
                        <asp:Literal ID="DefaultKeywordsTitle" runat="server" meta:resourcekey="KeywordsToMonitor">
                        </asp:Literal>
                    </h2>
                </div>
                <div id="InLineMessage">
                </div>
                <div class="KeywordsSectionWrapper">
                    <div class="KeywordsSection">
                        <div class="keywordsCommands">
                            <div class="RightLabel">
                                <a href="<%= ResolveClientUrl("Project.aspx?idProject=") %>" class="small" id="ManageKeywordsCommand">
                                    <asp:Literal ID="ManageKeywordsCommandText" runat="server" Text="<%$ Resources:CommonTerms, ManageKeywords %>">
                                    </asp:Literal></a>
                            </div>
                            <div>
                                <a href="##" id="CheckAllKeywordsListCommand" class="small" style="margin: 0">
                                    <asp:Literal ID="CheckAllKeywordsListCommandtext" runat="server" Text="<%$ Resources:CommonTerms, CheckAllWithoutBrackets %>"></asp:Literal></a>&nbsp;|&nbsp;<a
                                        href="##" id="UncheckAllKeywordsListCommand" class="small" style="margin: 0"><asp:Literal
                                            ID="UncheckAllKeywordsListCommandtext" runat="server" Text="<%$ Resources:CommonTerms, UncheckAllWithoutBrackets %>"></asp:Literal>
                                    </a>
                            </div>
                        </div>
                        <cc1:RoundPanel ID="RoundPanel5" runat="server" CssIdClassName="round_ctr BlackBorders">
                            <div id='TreeFromArray'>
                            </div>
                        </cc1:RoundPanel>
                    </div>
                    <div class="CenterWrapper ActionButton ">
                        <cc1:HyperLinkRound ID="RunMonitorCommand" runat="server" Text="[RUN_MONITOR]">
                        </cc1:HyperLinkRound>
                    </div>
                </div>
            </div>
        </cc1:RoundPanel>
    </div>
    <div id="RankingResultSection" class="Section" style="display: none;">
        <cc1:RoundPanel ID="RankingPanel" runat="server" DiscardBottom="False" DiscardTop="False"
            NotRenderStyles="False">
            <div class="Legend">
                <h2>
                    <asp:Literal ID="Literal1" runat="server" meta:resourcekey="KeywordRankingPanelTitle">
                    </asp:Literal>
                </h2>
            </div>
            <div class="ProgressSection">            
                <div class="LoadingAnimationLogo"></div>
                <div id='ProgressBarHere' style="width: 300px;">
                </div>
                <cc1:HyperLinkRound ID="HyperLinkCancel" Text="<%$ Resources:CommonTerms, Cancel %>"
                    runat="server">
                </cc1:HyperLinkRound>
            </div>
            <div class="DoClear">
            </div>
            <div id="RankingMonitorCompleted" style="display: none;">
                <p id="MonitoredCompetitors" class="heading">
                    <asp:Literal ID="MonitoredCompetitorsText" runat="server" meta:resourcekey="MonitoredCompetitors">
                    </asp:Literal>
                </p>
                <a href="<%= ResolveClientUrl("RankingMonitorReports.aspx") %>" class="small">
                    <asp:Literal ID="ViewFullReportCommandText" runat="server" meta:resourcekey="ViewFullReportCommand">
                    </asp:Literal>
                </a>
            </div>
            <div class="DoClear">
            </div>
            <div id="SummaryTable">
                <div class="xTable">
                    <uc3:ArrayRenderer ID="ArrayRenderer1" runat="server" HeaderCssClass="RowHeader"
                        ItemCssClass="AlternatingRowItem" AlternatingCssClass="RowItem">
                        <HeaderTemplate>
                            <table>
                                <tbody>
                                    <tr class="[CLASS_NAME]">
                                        <th class="Sortable" field_name='Keyword' data_type="String" title="<%= GetLocalResourceObject("KeywordHeadingTooltip") %>"
                                            style="text-align: left">
                                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:CommonTerms, Keywords %>">
                                            </asp:Literal>
                                        </th>
                                        <th class="Sortable" field_name='Pages' data_type="Number" title="<%= GetLocalResourceObject("PagesHeadingTooltip") %>">
                                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:CommonTerms, Pages %>">
                                            </asp:Literal>
                                        </th>
                                        <th class="Sortable" field_name='Engines' data_type="Number" title="<%= GetLocalResourceObject("EnginesHeadingTooltip") %>">
                                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:CommonTerms, Engines %>">
                                            </asp:Literal>
                                        </th>
                                        <th class="Sortable" field_name='DailySearches' data_type="Number" title="<%= GetLocalResourceObject("DailySearchesHeadingTooltip") %>">
                                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:CommonTerms, Activity %>">
                                            </asp:Literal>
                                        </th>
                                        <th class="Sortable" field_name='CPC' data_type="Number" title="<%= GetLocalResourceObject("CPCHeadingTooltip") %>">
                                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:CommonTerms, CPC %>">
                                            </asp:Literal>
                                        </th>
                                        <th class="Sortable" field_name='AllInTitle' data_type="Number" title="<%= GetLocalResourceObject("AllInTitleHeadingTooltip") %>">
                                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:CommonTerms, AllInTitle %>">
                                            </asp:Literal>
                                        </th>
                                        <th class="Sortable" field_name='AliasDomains' data_type="Number" title="<%= GetLocalResourceObject("AliasDomainsHeadingTooltip") %>">
                                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:CommonTerms, AliasDomains %>">
                                            </asp:Literal>
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table>
                                <tbody>
                                    <tr class='clickable [CLASS_NAME]'>
                                        <td style="text-align: left" class='{StatusClass}'>
                                            {Keyword}
                                        </td>
                                        <td>
                                            {Pages|default:'-'}
                                        </td>
                                        <td>
                                            {Engines|default:'-'}
                                        </td>
                                        <td>
                                            {Activity}
                                        </td>
                                        <td>
                                            {CPC|default:'-'|numberFormat:'2'|format:'$ [0]'}
                                        </td>
                                        <td>
                                            {AllInTitle|default:'-'}
                                        </td>
                                        <td>
                                            {AliasDomains|default:'-'}
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ItemTemplate>
                    </uc3:ArrayRenderer>
                </div>
                <div id="FootNote">
                    <p>
                        <asp:Literal ID="Literal13" meta:resourcekey="SummaryFootNote" runat="server"></asp:Literal></p>
                </div>
            </div>
            <div class="DoClear">
            </div>
            <div id="RankingTotal" style="display: none;">                
                <h3>
                    <asp:Literal ID="Literal9" runat="server" meta:resourcekey="YourTotals">
                    </asp:Literal>
                </h3>
                <div title="<%= GetLocalResourceObject("PageRankTooltip") %>">
                    <cc1:RoundPanel ID="RoundPanel1" runat="server" CssIdClassName="round_ctr BlackBorders">
                        <span class="Big">[PAGERANK]</span> <span class="Big Blue">
                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:CommonTerms, PageRank %>">
                            </asp:Literal>
                        </span>
                    </cc1:RoundPanel>
                </div>
                <div title="<%= GetLocalResourceObject("InboundTooltip") %>">
                    <cc1:RoundPanel ID="RoundPanel2" runat="server" CssIdClassName="round_ctr BlackBorders">
                        <span class="Big">[INBOUND_LINKS]</span> <span class="Big Blue">
                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:CommonTerms, InboundLinks %>">
                            </asp:Literal>
                        </span>
                    </cc1:RoundPanel>
                </div>
                <div title="<%= GetLocalResourceObject("TotalpagesIndexedTooltip") %>">
                    <cc1:RoundPanel ID="RoundPanel3" runat="server" CssIdClassName="round_ctr BlackBorders">
                        <span class="Big">[PAGES_INDEXED]</span> <span class="Big Blue">
                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:CommonTerms, PagesIndexed %>">
                            </asp:Literal>
                        </span>
                    </cc1:RoundPanel>
                </div>
                <div class="DoClear">
                    &nbsp;</div>
            </div>
        </cc1:RoundPanel>
    </div>
</asp:Content>
