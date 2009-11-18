<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IncludeSilverlight.ascx.cs" Inherits="SEOToolSet.WebApp.Controls.IncludeSilverlight" %>


<%@ Register src="IncludeFile.ascx" tagname="IncludeFile" tagprefix="uc1" %>

<uc1:IncludeFile ID="IncludeFile1" runat="server" FilePath="~/scripts/jquery.js" TypeOfFile="Javascript" />
<uc1:IncludeFile ID="IncludeFile2" runat="server" FilePath="~/scripts/common.js" TypeOfFile="Javascript"/>
<uc1:IncludeFile ID="IncludeFile3" runat="server" FilePath="~/scripts/Silverlight.js" TypeOfFile="Javascript"/>

<div id="SilverLightHost" runat="server">
<!-- Silverlight content here -->        
</div>

<script type="text/javascript">

    $.onDomReady(function() {
        var slEvents = {};
        var slProperties = {};

        var width = "<%= Width %>";
        var height = "<%= Height %>";
        var version = "<%= SilverlightVersion %>";
        var background = "<%= Background %>";
        var isWindowless = "<%= IsWindowless.ToString().ToLowerInvariant() %>";

        if (width != "") slProperties.width = width;
        if (height != "") slProperties.height = height;
        if (version != "") slProperties.version = version;
        if (background != "") slProperties.background = background;
        if (isWindowless != "") slProperties.isWindowless = isWindowless;

        Silverlight.createObjectEx({
            source: "<%= ResolveClientUrl(Source) %>",
            parentElement: $.byId('<%= SilverLightHost.ClientID %>').get(0),
            id: "<%= ClientID %>",
            properties: slProperties,
            events: slEvents,
            initParams: '<%= InitParameters %>',
            context: '<%= SilverlightContext %>'
        });

    });    
                                                                       
    </script>           


           

