<%@ Page Title="" Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true"
    CodeBehind="RankingMonitorDashboard.aspx.cs" Inherits="SEOToolSet.WebApp.RankingMonitorDashboard" %>

<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register Src="PageTitle.ascx" TagName="PageTitle" TagPrefix="uc1" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">
    <uc2:IncludeFile ID="IncludeFile11" runat="server" FilePath="~/css/RankingMonitorDashboard.css"
        TypeOfFile="Css" />
    <uc2:IncludeFile ID="IncludeFile7" FilePath="~/scripts/Controllers/Pages/PageBase.js"
        TypeOfFile="Javascript" runat="server" />
    <uc2:IncludeFile ID="IncludeFile2" runat="server" FilePath="~/scripts/Controllers/Pages/RankingMonitorDashboard.js"
        TypeOfFile="Javascript" />

    <script language="javascript" type="text/javascript">
        var dashboard = null;
        $(document).ready(function() {

            dashboard = new $.SEOToolSet.RankingMonitorDashboard({
                RankingMonitorHandler: '<%= ResolveClientUrl("Handler/RankingMonitorHelper.ashx") %>',
                ProjectHandler: '<%= ResolveClientUrl("Handler/ProjectHelper.ashx") %>',
                ChartTooltip: '<%= GetLocalResourceObject("ChartToolTip") %>',
                NoRankingMonitorRun: '<p class="strong"><%= GetLocalResourceObject("NoRankingMonitorRun") %><br><%= GetLocalResourceObject("NoRankingMonitorRunComplement") %></p>'
            });

            dashboard.addEventListener('Error', function(args) {
                $.showBigDialog($.stringFormat('<p>{0}</p>', args.errorMessage), {
                    icon: $.MessageIco.Warning,
                    title: 'UI Error'
                });
            });

            dashboard.addEventListener('NoActiveProjects', function(a) {
                $.showBigDialog('<p><%= GetGlobalResourceObject("DialogMessages", "NoProjectActive") %></p>',
                {
                    title: '<%= GetGlobalResourceObject("DialogMessages","ProjectSelectionError") %>',
                    icon: $.MessageIco.Warning,
                    showOk: false,
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
            dashboard.init();
        });

    </script>

    <uc1:PageTitle ID="PageTitle1" RenderRoundPanelStyles="false" runat="server" meta:resourcekey="PageTitle1" />
    <cc1:RoundPanel ID="ShortcutRoundPanel" runat="server">
        <ul class="Toolbar">
            <li><a class='btnWithIco Run large' href="RankingMonitorRun.aspx" id="RunRankingMonitorLink">
                <span>
                    <asp:Literal ID="RunRankingMonitorTextLink" runat="server" meta:resourcekey="RunRankingMonitorTextLink"></asp:Literal>
                </span></a></li>
            <li><a class="btnWithIco Config large" href="RankingMonitorConfiguration.aspx" id="ConfigRankingMonitorLink">
                <span>
                    <asp:Literal ID="ConfigureRankingMonitorTextLink" runat="server" meta:resourcekey="ConfigureRankingMonitorTextLink"></asp:Literal>
                </span></a></li>
        </ul>
        <div class="DoClear">
        </div>
    </cc1:RoundPanel>
    <div id="Summary" style="display: none">
        <cc1:RoundPanel ID="SummaryRoundPanel" runat="server">
            <div style="text-align: center; display: none;" id="SummarySection">
                <p>
                    <asp:Literal ID="LastRankingMonitorRunText" runat="server" meta:resourcekey="LastRankingMonitorRunText"></asp:Literal>
                </p>
                <br />
                <a href="RankingMonitorReports.aspx">
                    <asp:Literal ID="RunReportLinkText" runat="server" meta:resourcekey="RunReportLinkText"></asp:Literal>
                </a>
            </div>
            <div id="SummaryMediaWrapper">
                <div id="RankingChartContainer" class="Summary">
                    <h3>
                        <asp:Literal ID="RankingChartTitle" runat="server" meta:resourcekey="RankingChartTitle"></asp:Literal>
                    </h3>
                </div>
                <div id="TopKeywordsContainer" class="Summary">
                    <h3>
                        <asp:Literal ID="TopKeywordsTitle" runat="server" meta:resourcekey="TopKeywordsTitle"></asp:Literal>
                    </h3>
                    <cc1:RoundPanel ID="RoundPanel1" CssIdClassName="round_ctr BlackBorders" runat="server">
                        <ol class="Keywords" />
                    </cc1:RoundPanel>
                </div>
            </div>
        </cc1:RoundPanel>
    </div>
</asp:Content>
