<%@ Page Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true" CodeBehind="Dummy.aspx.cs"
    Inherits="SEOToolSet.WebApp.Dummy" %>

<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>

<%@ Register TagPrefix="user" TagName="PageTitle" Src="PageTitle.ascx" %>
<%@ Register Src="Controls/CostConversion.ascx" TagName="CostConversion" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">    
    <user:PageTitle PageTitleText="Dummy" PageDescription="<%$ Resources:CommonTerms, SiteUnderConstruction %>"
        ID="WebUserControl0" runat="server"></user:PageTitle>
        
    
    <uc1:CostConversion ID="CostConversion1" runat="server" />
    <div class="FormCSS">
        <div class="Field">
            <label>Amount of Money to convert in US$</label>
            <input id="originalAmount" type="text" class="FormText" />            
        </div>
        <div class="Field"><label>&nbsp;</label></div>
        <div class="Field">
            <cc1:HyperLinkRound ID="hlnkDoCalculate" CssClass="button ClickConvert" Text="Convert" runat="server"></cc1:HyperLinkRound>
        </div>
        
        <script type="text/javascript">
            $(function() {
            $('.ClickConvert').click(function() { 
                var c = <%=CostConversion1.ClientID %>_converter;
                if (!c) return;
                    c.setFromValue($('#originalAmount').val());
                    c.showValueInOtherMoney();
                });
            });
        </script>
    </div>
</asp:Content>
