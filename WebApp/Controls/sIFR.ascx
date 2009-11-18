<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="sIFR.ascx.cs" Inherits="SEOToolSet.WebApp.Controls.sIFR" %>
<%@ Register Src="IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>

<uc1:IncludeFile ID="IncludeFile1" FilePath="~/scripts/Controls/sIFR/jquery.flash.js" TypeOfFile="Javascript" runat="server" />
<uc1:IncludeFile ID="IncludeFile2" FilePath="~/scripts/Controls/sIFR/jquery.sifr.js" TypeOfFile="Javascript" runat="server" />


<% if (!String.IsNullOrEmpty(Selector) && (!String.IsNullOrEmpty(SwfFontToRender)))
   { %>

<script type="text/javascript">

    $(function() {
        
        $('<%= Selector %>').sifr({ font: '<%= ResolveClientUrl(SwfFontToRender) %>' });
    });

</script>

<% } %>
