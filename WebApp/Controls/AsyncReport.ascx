<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="AsyncReport.ascx.cs"
    Inherits="SEOToolSet.WebApp.Controls.AsyncReport" %>
<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<div id='<%= ClientID %>' 
    class="Report" 
    report_type='<%= ReportType %>' 
    report_id='<%= ReportIdentifier %>'  
    check_name='<%= CheckName %>'
    on_data_received='<%= OnClientDataReceived %>' 
    report_name='<%= ReportName %>'
    report_url='<%= ReportUrl %>'
    on_before_ajax_call='<%= OnBeforeAjaxCall %>'
    >
    <cc1:RoundPanel ID="RoundPanel1" runat="server">
        <div class='ReportHeader' title='<%= ReportTooltip %>'>
            <h2>
                <%= ReportTitle %>
            </h2>
            <div class="LoadingMark" style="display: none;">
                <h2>
                    <asp:Literal ID="loadingText" runat="server" Text="<%$ Resources:CommonTerms, Loading %>"></asp:Literal></h2>
            </div>
            <a class='ReportCollapseTrigger' href='javascript:void(0);'><span>Collapse</span></a>
            <span class="NotifyMessage"></span>
        </div>
        <div class='TableArea'>
            <asp:PlaceHolder ID="ItemTemplatePlaceHolder1" runat="server"></asp:PlaceHolder>
        </div>
    </cc1:RoundPanel>
</div>
