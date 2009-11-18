<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_ViewMessage"
         ClassName="CtlViewMessage" Codebehind="ViewMessage.ascx.cs" %>

<div class="message" runat="server" id="controlDiv">
    <div class="messageHeader">
        <span class="messageSubject" id="messageTitle" runat="server"></span>
        <div class="messageAuthor">
            <a class="user" id="linkAuthor" runat="server">by <span id="lblAuthor" runat="server"></span></a>
        </div>
        <div class="messageDate">
            Date: <span id="lblDate" runat="server"></span>
        </div>
        <div class="messageAttach" id="sectionAttachment" runat="server">
            <a class="attachment" id="linkAttach" runat="server"></a>
        </div>
    </div>
    <div class="messageBody" runat="server" id="sectionBody">
    </div>
    <div class="messageActions">
        <asp:PlaceHolder ID="sectionNew" runat="server">
            <asp:LinkButton CssClass="mailreply" 
                ID="linkNew" runat="server" OnClick="MessageNew_Click" >Reply</asp:LinkButton>
        </asp:PlaceHolder>
            
        <asp:PlaceHolder ID="sectionDelete" runat="server">
            <asp:LinkButton CssClass="deleteitem" OnClientClick="return confirm('Are you sure to delete the message?');"
                ID="linkDelete" runat="server" OnClick="MessageDelete_Click" >Delete</asp:LinkButton>
        </asp:PlaceHolder>
    </div>
</div>
