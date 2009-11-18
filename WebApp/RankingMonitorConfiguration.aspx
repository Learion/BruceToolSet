<%@ Page Title="" Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true"
    CodeBehind="RankingMonitorConfiguration.aspx.cs" Inherits="SEOToolSet.WebApp.RankingMonitorConfiguration" %>

<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<%@ Register Src="PageTitle.ascx" TagName="PageTitle" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">
    <uc1:IncludeFile ID="IncludeFile10" FilePath="~/scripts/Controls/R3M.Combobox/cbx.css"
        TypeOfFile="Css" runat="server" />
    <uc1:IncludeFile ID="IncludeFile1" FilePath="~/scripts/Controls/R3M.Combobox/combobox.js"
        TypeOfFile="Javascript" runat="server" />
    <uc1:IncludeFile ID="IncludeFile2" FilePath="~/scripts/plugins/jQuery.CheckedTree/checktree.css"
        TypeOfFile="Css" runat="server" />
    <uc1:IncludeFile ID="IncludeFile3" FilePath="~/scripts/plugins/jQuery.CheckedTree/jquery.checktree.js"
        TypeOfFile="Javascript" runat="server" />
    <uc1:IncludeFile ID="IncludeFile4" FilePath="~/scripts/Controllers/CheckedTreeHelper.js"
        TypeOfFile="Javascript" runat="server" />
    <uc1:IncludeFile ID="IncludeFile7" FilePath="~/scripts/Controllers/Pages/PageBase.js"
        TypeOfFile="Javascript" runat="server" />
    <uc1:IncludeFile ID="IncludeFile6" FilePath="~/scripts/Controllers/Pages/RankingMonitorConfiguration.js"
        TypeOfFile="Javascript" runat="server" />
    <uc1:IncludeFile ID="IncludeFile5" FilePath="~/css/RankingMonitorConfiguration.css"
        TypeOfFile="Css" runat="server" />
    <uc2:PageTitle ID="PageTitle1" RenderRoundPanelStyles="false" runat="server" meta:resourcekey="PageTitle1" />

    <script language="javascript" type="text/javascript">
        var rmc = null;
        var messageBeforeLeaving = '<%= String.Format("{0} {1} ", GetLocalResourceObject("ChangesNotSaved"), GetGlobalResourceObject("DialogMessages","AreYouSureWantToLeave")) %>';
        $(document).ready(function() {
            rmc = new $.SEOToolSet.RankingMonitorConfiguration({
                RankingMonitorHandler: '<%= ResolveClientUrl("Handler/RankingMonitorHelper.ashx") %>',
                ProjectHandler: '<%= ResolveClientUrl("Handler/ProjectHelper.ashx") %>',
                MessageBeforeLeaving: '<%= GetLocalResourceObject("ChangesNotSaved") %>'
            });
            rmc.addEventListener('Error', function(args) {
                $.showBigDialog($.stringFormat('<p>{0}</p>', args.errorMessage), {
                    icon: $.MessageIco.Warning,
                    title: 'UI Error'
                });
            });
            rmc.addEventListener('CancellingConfiguration', function() {
            $.showBigDialog('<p><%= GetLocalResourceObject("CancelConfirmation") %><p>',
                    {
                        title: $.getResourceString('Warning', 'Warning'),
                        icon: $.MessageIco.Warning,
                        buttons: [
                            {
                                text: $.getResourceString('DiscardChanges', 'Discard Changes'),
                                command: 'DiscardChanges'
}],
                        showOk: false,
                        showCancel: true,
                        defaultButtonsBefore: false,
                        onCommand: function(args) {
                            if (args.command == 'DiscardChanges') {
                                rmc.reloadCurrenProject();
                            }
                        }
                    });
            });
            rmc.addEventListener('KeywordsNotSpecified', function(a) {
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
            rmc.addEventListener('NoSearchEnginesSelected', function() {
                $.showBigDialog('<p>' + $.stringFormat($.getResourceString('NoItemSelected', 'You must select at least one search engine.'), $.getResourceString('SearchEngine', 'Search Engine')) + '</p>',
                    {
                        title: $.getResourceString('InputRequired', 'Input Required'),
                        icon: $.MessageIco.Info,
                        showCancel: false
                    });
            });
            rmc.addEventListener('NoKeywordsSelected', function() {
                $.showBigDialog('<p><%= GetGlobalResourceObject("DialogMessages", "NoKeywordSelectedToMonitor") %></p>',
                    {
                        title: $.getResourceString('InputRequired', 'Input Required'),
                        icon: $.MessageIco.Info,
                        showCancel: false
                    });
            });
            rmc.addEventListener('ChangingProjectWithChangesNotSaved', function(a) {
                $.showBigDialog('<p>' + messageBeforeLeaving + '</p>',
                    {
                        title: $.getResourceString("Warning", "Warning"),
                        icon: $.MessageIco.Warning,
                        buttons: [
                            {
                                text: $.getResourceString("DiscardChanges", "Discard Changes"),
                                command: 'Discard'
                            },
                            {
                                text: $.getResourceString("SaveChanges", "Save Changes"),
                                command: 'Save'
}],
                        showOk: false,
                        showCancel: true,
                        defaultButtonsBefore: false,
                        onCommand: function(args) {
                            if (args.command == 'Save') {
                                args.doOnDialogClose = function() {
                                    rmc.doSave(function() {
                                        $.CurrentPage.cbxProjects.setValueTo(a.idProject, true);
                                    });
                                }
                            }
                            else if (args.command == 'Discard') {
                                $.CurrentPage.cbxProjects.setValueTo(a.idProject, true);
                            }
                        }
                    });
            });
            rmc.addEventListener('DiscardingWithChangesNotSaved', function() {
                $.showBigDialog('<p><%= GetGlobalResourceObject("DialogMessages", "RestaureDefaultWithUnsavedChanges") %></p>',
                    {
                        title: $.getResourceString("Warning", "Warning"),
                        icon: $.MessageIco.Warning,
                        buttons: [
                            {
                                text: $.getResourceString("Restore", "Restore Defaults"),
                                command: 'Restore'
                            }
],
                        showOk: false,
                        showCancel: true,
                        defaultButtonsBefore: false,
                        onCommand: function(args) {
                            if (args.command == 'Restore') {
                                rmc.restoreDefaults();
                            }
                        }
                    });
            });
            rmc.addEventListener('NoActiveProjects', function(a) {
                $('div.ActionButton ul li, #MonitorFrequency').hide('fast');
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
            rmc.init();
            var opts = {
                title: '<%= GetGlobalResourceObject("DialogMessages","ConfirmExit") %>',
                txtMessage: messageBeforeLeaving,
                stayText: '<%= GetGlobalResourceObject("DialogMessages","StayOnPageButtonText") %>',
                leaveText: '<%= GetGlobalResourceObject("DialogMessages","LeavePageButtonText") %>',
                beforeShowConfirmation: function(args) {
                    if (!rmc.hasDataPendingToSave()) args.cancel = true;
                }

            };
            $.confirmNavigateAway(opts);
        });
    </script>

    <div style="min-height: 800px;">
        <div id="MonitorFrequency">
            <cc1:RoundPanel ID="RoundPanel1" runat="server">
                <div id="MonitorFrequencySection">
                    <div class="Legend">
                        <h2>
                            <asp:Literal ID="MonitorFrequencyTitle" runat="server" meta:resourcekey="MonitorFrequencyTitle"></asp:Literal>
                        </h2>
                    </div>
                    <div class="MonitorFrequencyWrapper Section">
                        <div class="FormLabel">
                            <asp:Literal ID="RunTheMonitorFrequencyLabel" runat="server" meta:resourcekey="RunTheMonitorFrequencyLabel"></asp:Literal>
                        </div>
                        <select class="FormCbx" style="width: 160px" id="dropdown" name="dropdown">
                        </select>
                    </div>
                    <div class="HeadingDates" style="display: none">
                        <p class="heading">
                            <asp:Literal ID="LastRankingMonitorRunDateLabel" runat="server" meta:resourcekey="LastRankingMonitorRunDateLabel"></asp:Literal><br />
                            <asp:Literal ID="NextScheduledRunDateLabel" runat="server" meta:resourcekey="NextScheduledRunDateLabel"></asp:Literal>
                        </p>
                    </div>
                </div>
            </cc1:RoundPanel>
        </div>
        <div id="MonitorKeywordLists" style="display: none">
            <cc1:RoundPanel ID="RoundPanel2" runat="server">
                <div id="DefaultKeywordsSection">
                    <div class="Legend">
                        <h2>
                            <asp:Literal ID="DefaultKeywordsTitle" runat="server" meta:resourcekey="DefaultKeywordsTitle"></asp:Literal>
                        </h2>
                    </div>
                    <div class="KeywordsSection Section">
                        <div>
                            <div class="RightLabel">
                                <!-- TODO: display a message when there are changes not saved -->
                                <a href="<%= ResolveClientUrl("Project.aspx?idProject=") %>" class="small" id="ManageKeywordsCommand">
                                    <asp:Literal ID="ManageKeywordsCommandText" runat="server" Text="<%$ Resources:CommonTerms, ManageKeywords %>"></asp:Literal>
                                </a>
                            </div>
                            <div>
                                <span><a href="#" id="CheckAllKeywordsListCommand" class="small" style="margin-right: 0px ! important;">
                                    <asp:Literal ID="CheckAllKeywordsListCommandtext" runat="server" Text="<%$ Resources:CommonTerms, CheckAllWithoutBrackets %>"></asp:Literal>
                                </a></span>|<span><a href="#" id="UncheckAllKeywordsListCommand" class="small">
                                    <asp:Literal ID="UncheckAllKeywordsListCommandtext" runat="server" Text="<%$ Resources:CommonTerms, UncheckAllWithoutBrackets %>"></asp:Literal>
                                </a></span>
                            </div>
                        </div>
                        <cc1:RoundPanel ID="RoundPanel5" runat="server" CssIdClassName="round_ctr BlackBorders">
                            <div id='TreeFromArray'>
                            </div>
                        </cc1:RoundPanel>
                    </div>
                </div>
            </cc1:RoundPanel>
        </div>
        <div id="MonitorProxies" style="display: none">
            <cc1:RoundPanel ID="RoundPanel3" runat="server">
                <div id="ProxyServersSection">
                    <div class="Legend">
                        <h2>
                            <asp:Literal ID="ProxyServersTitle" runat="server" meta:resourcekey="ProxyServersTitle"></asp:Literal>
                        </h2>
                    </div>
                    <p class="heading">
                        <asp:Literal ID="ProxyServerOverview" runat="server" meta:resourcekey="ProxyServerOverview"></asp:Literal>
                    </p>
                    <ul id="ProxiesList" class="UlList Section" />
                </div>
            </cc1:RoundPanel>
        </div>
        <div id="MonitorSearchEnginesCountries" style="display: none">
            <cc1:RoundPanel ID="RoundPanel4" runat="server">
                <div id="SearchEnginesSection">
                    <div class="Legend">
                        <h2>
                            <asp:Literal ID="SearchEnginesTitle" runat="server" meta:resourcekey="SearchEnginesTitle"></asp:Literal>
                        </h2>
                    </div>
                    <div class="Selected">
                        <span class="strong" id="selectedSearchEngines"></span><a href="javascript:void(0);"
                            class="small" id="UncheckAllSearchEnginesCommand" style="padding-left: 5px;">
                            <asp:Literal ID="UncheckAllSearchEnginesCommandText" runat="server" Text="<%$ Resources:CommonTerms, UncheckAllWithoutBrackets %>"></asp:Literal>
                        </a>
                    </div>
                    <div class="Filter">
                        <span class="FormLabel">
                            <asp:Literal ID="FilterByCountryLabel" runat="server" meta:resourcekey="FilterByCountryLabel"></asp:Literal></span>
                        <input id="FilterByCountryTextBox" class="FormText" style="width: 200px" />
                        <br />
                        <a href="javascript:void(0);" id="ShowAllSearchEngines" class="small">
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:CommonTerms, ShowAll %>"></asp:Literal></a>|
                        <a href="javascript:void(0);" id="ShowSelectedSearchEngines" class="small">
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:CommonTerms, ShowSelected %>"></asp:Literal></a>
                    </div>
                    <div class="DoClear">
                    </div>
                </div>
                <div class="Section">
                    <ul id="SearchEnginesList" class="UlList" />
                </div>
            </cc1:RoundPanel>
        </div>
        <div class="ActionButton CenterWrapper">
            <ul>
                <li>
                    <cc1:HyperLinkRound ID="HyperLinkSave" runat="server" Text="<%$ Resources:CommonTerms, Save %>"
                        meta:resourcekey="SaveCommand"></cc1:HyperLinkRound>
                </li>
                <li>
                    <cc1:HyperLinkRound ID="HyperLinkRestoreDefaults" runat="server" Text="<%$ Resources:CommonTerms, RestoreDefaults %>"
                        meta:resourcekey="CancelCommand"></cc1:HyperLinkRound>
                </li>
                <li>
                    <cc1:HyperLinkRound ID="HyperLinkCancel" runat="server" Text="<%$ Resources:CommonTerms, Cancel %>"
                        meta:resourcekey="RestoreCommand"></cc1:HyperLinkRound>
                </li>
            </ul>
        </div>
        <div style="height: 40px;">
            <div id="feedbackMsg" class="green" style="display: none">
                <asp:Literal ID="SavedMessage" runat="server" meta:resourcekey="SavedMessage"></asp:Literal>
            </div>
        </div>
    </div>
</asp:Content>
