<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashboardTitle.ascx.cs"
    Inherits="SEOToolSet.WebApp.DashboardTitle" %>
<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<div>
    <table width="100%">
        <tr>
            <td>
                Project Dashboard
            </td>
            <td style="text-align: right">
                Help & Documentation
                <img alt="Help & Documentation" src="" />
            </td>
        </tr>
        <tr>
            <td>
                View projects below click the linked items to access reports and manage project data
            </td>
            <td style="text-align: right">
                Show:<asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</div>
