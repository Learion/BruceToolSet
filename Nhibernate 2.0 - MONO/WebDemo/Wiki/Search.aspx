<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" 
        Inherits="Wiki_Search" Title="Search article" ValidateRequest="false" Codebehind="Search.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="SearchResult" Src="~/Controls/SearchResult.ascx" %>
        
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" Runat="Server">

    <h1>Articles Search</h1>

    <h2>Search parameters:</h2>
    <table class="formtable">
        <tr>
            <td><label for="txtSearchFor">Search for:</label></td>
            <td>
                <asp:TextBox ID="txtSearchFor" runat="server" Columns="40"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><label for="txtAuthor">Author:</label></td>
            <td>
                <asp:TextBox ID="txtAuthor" runat="server" Columns="40"></asp:TextBox>
            </td>
        </tr>
    </table>
    
    <h2>Search on these categories:</h2>
    <asp:CheckBoxList ID="listForum" runat="server" DataTextField="Description" DataValueField="Id">
    </asp:CheckBoxList>
    
    <p>
        <asp:Button ID="btSearch" runat="server" Text="Search" OnClick="btSearch_Click" />
    </p>

    <h2>Results:</h2>
    <uc:SearchResult ID="searchResult" runat="server" />

</asp:Content>

