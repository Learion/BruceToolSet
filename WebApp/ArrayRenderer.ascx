<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ArrayRenderer.ascx.cs"
    Inherits="SEOToolSet.WebApp.ArrayRenderer" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<uc1:IncludeFile ID="IncludeFile2" FilePath="~/scripts/Controllers/ArrayRenderers.js"
    TypeOfFile="Javascript" runat="server" />
<div style="display: none;" id='<%=ClientID %>' class="ArrayRenderer" header_class='<%= HeaderCssClass %>'
    container_tag_name='<% = ContainerTagName %>' row_class='<%= ItemCssClass %>'
    renderer_type='<%= TypeOfRenderer.ToString()  %>' alternating_row_class='<%= AlternatingCssClass %>'
    header_path='<%= HeaderPath %>' item_path='<%= ItemPath %>'>
    <div class="xHeader">
        <asp:PlaceHolder ID="HeaderTemplatePlaceHolder" runat="server"></asp:PlaceHolder>
    </div>
    <div class='xItemTemplate'>
        <asp:PlaceHolder ID="ItemTemplatePlaceHolder" runat="server"></asp:PlaceHolder>
    </div>
</div>
