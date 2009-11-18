<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="SubMenu.ascx.cs" Inherits="SEOToolSet.WebApp.SubMenu" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<div class="SubMenu">
    <uc1:IncludeFile ID="IncludeFile2" TypeOfFile="Javascript" FilePath="~/scripts/Controls/R3M.JMenu/JMenu.js"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile5" TypeOfFile="CSS" FilePath="~/scripts/Controls/R3M.JMenu/JMenuBasic.css"
        runat="server" />        
    <asp:Literal ID="LiteralMenu" runat="server"></asp:Literal>            
    
</div>
