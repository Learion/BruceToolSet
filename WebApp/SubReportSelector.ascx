<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="SubReportSelector.ascx.cs"
    Inherits="SEOToolSet.WebApp.SubReportSelector" %>
<div id="SubReportSelector">
    <div class="ToolBar">
        <div class="ToolBarItem">
            <p class="LabelItem">
                <asp:Label ID="showReportsCommand" runat="server" Text="<%$ Resources:CommonTerms, ShowReports %>"></asp:Label></p>
        </div>
        <div class="ToolBarItem">
            <a class="MiniCommand" id="checkNone" href="#"><span>
                <asp:Label ID="checkNoneCommand" runat="server" Text="<%$ Resources:CommonTerms, CheckNone %>"></asp:Label></span></a>
            <span>|</span> <a class="MiniCommand" id="checkAll" href="#"><span>
                <asp:Label ID="checkAllCommand" runat="server" Text="<%$ Resources:CommonTerms, CheckAll %>"></asp:Label></span></a>
        </div>
        <div class="DoClear">
        </div>
    </div>
    <div id="SubReports">
        <ul>
            <li>&nbsp; </li>
        </ul>
    </div>
    <div class="DoClear">
    </div>
    <a class="LinkCommandRound Big" href="javascript:void(0);" id="lnkRun">
        <asp:Label ID="runCommand" runat="server" Text="<%$ Resources:CommonTerms, Run %>"></asp:Label></a>
</div>
<div id="ErrorInLineMessage">
</div>
<script type="text/javascript">
    $(function () {
        $('.ButtonSelector').each(function() { $(this).append($('#lnkRun'))});
     });
</script>