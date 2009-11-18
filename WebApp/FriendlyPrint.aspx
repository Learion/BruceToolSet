<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FriendlyPrint.aspx.cs"
    Inherits="SEOToolSet.WebApp.FriendlyPrint" %>

<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Friendly Print</title>
    <style type="text/css">
        body, html { background:none; background:#fff !important;}
        h1 { font-size:200% !important; font-weight:bold !important; margin:20px; }
    </style>
</head>

<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="literalOutput" runat="server"></asp:Literal>
        <uc1:IncludeFile ID="IncludeFile2" FilePath="~/css/reset-fonts.css" TypeOfFile="CSS"
            runat="server" />
        <uc1:IncludeFile ID="IncludeFile1" TypeOfFile="CSS" FilePath="~/css/app.css" runat="server" />
        <uc1:IncludeFile ID="IncludeFile6" TypeOfFile="Javascript" FilePath="~/scripts/jquery.js"
            runat="server" />
        <uc1:IncludeFile ID="IncludeFile3" TypeOfFile="CSS" FilePath="~/css/print.css" MediaCssAttribute="print"
            runat="server" />

        <script type="text/javascript">
            function setContent(ctn, title) {
                document.title = title;
                $('#tit').text(title);
                $('#ctn').html(ctn);
                window.print();
            }
        </script>
        <div class="_hd">
            <h1 id='tit'></h1>        
        </div>
        <div id="ctn" class='xTable'>
            
        </div>

    </div>
    </form>
</body>
</html>
