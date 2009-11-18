<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ToolTip.ascx.cs" Inherits="SEOToolSet.WebApp.Controls.ToolTip" %>
<%@ Register Src="IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<uc1:IncludeFile ID="IncludeFile1" runat="server" FilePath="~/scripts/Controls/R3M.BrTip/brTip.css"
    TypeOfFile="CSS" />
<uc1:IncludeFile ID="IncludeFile6" runat="server" FilePath="~/scripts/Controls/R3M.BrTip/brTip.src.js"
    TypeOfFile="Javascript" />
<% if (!String.IsNullOrEmpty(Selector))
   { %>
<div class="ControlInitializer">

    <script type="text/javascript">
    var <%= ClientID %>_ensureJustOne = 0;
    function <%= ClientID %>_InitToolTips() {        
        if (<%= ClientID %>_ensureJustOne > 0) return;        
        
        setTimeout(function () {
            var options = {};        
            <%= GetOptionsObj() %>        
            $('<%= Selector %>').attr('show_tooltip','true').brTip(options);
        }, 1000);
        
        <%= ClientID %>_ensureJustOne++;     
    }
    
    $.onDomReady(<%= ClientID %>_InitToolTips);
    
    </script>

</div>
<% } %>
