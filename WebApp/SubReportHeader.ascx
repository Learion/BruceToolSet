<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="SubReportHeader.ascx.cs"
    Inherits="SEOToolSet.WebApp.SubReportHeader" %>
<%@ Register TagPrefix="user" TagName="SubReportSelector" Src="SubReportSelector.ascx" %>
<div class="ToolBar">
    <div class="ToolBarItem">
        <h2 class='title'>
            <asp:Label ID="lblSubReportTitle" runat="server"></asp:Label>
        </h2>
    </div>
    <div class="ToolBarItem Right" style="padding-top:15px;">
        <a class="MiniCommand" id="collapseAll" href="#" ><span>
            <asp:Label ID="collapseAllCommand" runat="server" 
            Text="<%$ Resources:CommonTerms, CollapseAll %>" ></asp:Label></span></a>
        <span class="MiniCommandText">| </span><a class="MiniCommand" id="expandAll" href="#">
            <span>
                <asp:Label ID="expandAllCommand" runat="server" 
            Text="<%$ Resources:CommonTerms, ExpandAll %>" ></asp:Label></span></a>
    </div>
    <div class="DoClear">
    </div>
</div>
