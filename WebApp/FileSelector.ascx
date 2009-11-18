<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="FileSelector.ascx.cs"
    Inherits="SEOToolSet.WebApp.FileSelector" %>
<div id='<%= ClientID %>' class='FileSelector'>
    <div class="FormRow">
        <div class="FormSelector">
            <label class="LabelItem" for="<%=ClientID %>_txtUrlFile">
                <asp:Literal ID="ltl" Text='<%$ Resources:CommonTerms, AnalyzePage %>' runat="server"></asp:Literal>
            </label>
            <input type="text" class="FormText" id="txtUrlFile" name="txtUrlFile" runat="server" />
        </div>
        <div class="ButtonSelector">
            
        </div>
        <div class="DoClear">
        </div>
    </div>
    <div id="rowIndicators" class="FormRow" runat="server">        
        <div class="ModeIndicators">
            <span class="Item" id="tdLnk" runat="server">
            <a id='lnkUploadFile' href='#' class="LinkUpload"
                title='<%$ Resources:CommonTerms, UploadFile %>' runat="server">
                <asp:Label ID="uploadFile" runat="server"></asp:Label>
            </a></span>
        </div>
    </div>
</div>
