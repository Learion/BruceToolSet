<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportPageConfigurator.ascx.cs"
    Inherits="SEOToolSet.WebApp.ReportPageConfigurator" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<uc1:IncludeFile ID="IncludeFile1" FilePath="~/scripts/Controls/SwfUpload/swfupload.js"
    TypeOfFile="Javascript" runat="server" />
<uc1:IncludeFile ID="IncludeFile2" FilePath="~/scripts/Controllers/ArrayRenderers.js"
    TypeOfFile="Javascript" runat="server" />
<uc1:IncludeFile ID="IncludeFile3" FilePath="~/scripts/Controllers/AsyncManager.js"
    TypeOfFile="Javascript" runat="server" />
<uc1:IncludeFile ID="IncludeFile5" FilePath="~/scripts/Controllers/Reports.js" TypeOfFile="Javascript"
    runat="server" />

<script type="text/javascript">                       
        $(document).ready(function()
        {                        
            var reportServicesUrl = '<%= ConfigurationManager.AppSettings[InitReportsUrl] %>';
            var deleteHandlerUrl = '<%= SEOToolSet.Common.WebHelper.GetAbsolutePath("Handler/CleanFiles.ashx") %>';
            var uploadHandlerUrl = '<%= SEOToolSet.Common.WebHelper.GetAbsolutePath("Handler/Upload.ashx") %>';
            var pageSelectionDivId = 'pInfo';
            var subReportContainerId = 'SubReportContainer';
            var inlineMessageDiv = 'ErrorInLineMessage';
            var preClickJobs = window['<%= onPreClickJobs %>'];
            
            try {  
                InitializeReportPage(pageSelectionDivId,subReportContainerId,reportServicesUrl,deleteHandlerUrl,uploadHandlerUrl, inlineMessageDiv, preClickJobs);                                  
            }
            catch(e) {
                console.log(e.message);
            }
        });
</script>

