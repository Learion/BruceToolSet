<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_ViewNewsItem" Codebehind="ViewNewsItem.ascx.cs" %>

<div runat="server" id="controlDiv">
    <div class="newsitem">
        <div class="newsitemHeader">
            <div class="newsitemTitle" id="lblTitle" runat="server"></div>
        </div>
        <div class="newsitemProperties">
            <div class="newsitemAuthor">
                <a class="user" id="linkAuthor" runat="server">by <span id="lblAuthor" runat="server"></span></a>
            </div>
            <div class="newsitemDate">
                Date: <span id="lblDate" runat="server"></span>
            </div>
            <div class="newsitemUrl">
                Link: <a class="urlitem" id="linkUrl" runat="server"></a>
            </div>
        </div>
        <div class="newsitemActions" id="sectionActions" runat="server">
            <asp:LinkButton CssClass="deleteitem" OnClientClick="return confirm('Are you sure to delete the news?');"
                ID="linkDelete" runat="server" OnClick="MessageDelete_Click" >Delete</asp:LinkButton>
            <a id="linkEdit" runat="server" class="edititem">Edit</a>
        </div>
        
        <div class="newsitemDescription" runat="server" id="sectionDescription">
        </div>
    </div>
</div>