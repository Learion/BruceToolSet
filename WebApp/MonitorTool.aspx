<%@ Page Title="" Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true"
    CodeBehind="MonitorTool.aspx.cs" Inherits="SEOToolSet.WebApp.MonitorTool" %>

<%@ Register src="PageTitle.ascx" tagname="PageTitle" tagprefix="uc1" %>

<%@ Register src="Controls/IncludeSilverlight.ascx" tagname="IncludeSilverlight" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">    

    <uc1:PageTitle ID="PageTitle1" PageTitleText="Ranking Report" PageDescription="Monitor your keywords across major search engines." runat="server" />
    
    <script type="text/javascript">
        function xHeight(h) {
            var id = "<%= SilverlightReportMonitor.ClientID %>_SilverLightHost";
            if (!$.browser.msie) {
                $.byId(id).css('height', h);
                return;
            }
            
            console.log(h);
            $.byId(id).animate( { height : h }, 500 );
        }
    </script>
    
    <uc2:IncludeSilverlight  IsWindowless="true" Background="transparent" HostStyle="height:100px" ID="SilverlightReportMonitor" runat="server" Source="~/ClientBin/SEOToolSet.Silverlight.Reports.xap" Width="100%" Height="100%" SilverlightVersion="2.0"  />
                                                                                                                                                                                
    

</asp:Content>
