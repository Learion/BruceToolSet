<%@ Page Language="C#" MasterPageFile="~/PageBase.Master" AutoEventWireup="true" CodeBehind="ProjectDashboard.aspx.cs" 
Inherits="SEOToolSet.WebApp.ProjectDashboard1" Title="Project Dashboard" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register Src="PageTitle.ascx" TagName="PageTitle" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<%@ Register Src="ManageUsersControl.ascx" TagName="ManageUsersControl" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">
    
    <uc1:IncludeFile ID="IncludingTreeStylesheet" FilePath="~/scripts/Controls/Tree/Tree.css"
        TypeOfFile="CSS" runat="server" />
    <uc1:IncludeFile ID="IncludingTreeJavascript" FilePath="~/scripts/Controls/Tree/jquery.simple.tree.js"
        TypeOfFile="Javascript" runat="server" />
    <uc1:IncludeFile ID="IncludeFile1" FilePath="~/css/Project.css" TypeOfFile="CSS"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile2" FilePath="~/scripts/Controllers/TreeHelper.js"
        TypeOfFile="Javascript" runat="server" />
    <uc1:IncludeFile ID="IncludeFile3" FilePath="~/scripts/jQuery.UI/themes/flora/flora.tabs.css"
        TypeOfFile="Css" runat="server" />
    <%--<uc1:PageTitle ID="PageTitle1" runat="server" PanelContainerVisible="false" meta:resourcekey="EditProjectPageTitle" />--%>
    <div>
        <div class="center">
<div style="float:left; padding-top:15PX;">
<h1>Project Dashboard</h1>
<div class="center_text">
   <p style="font-size: 0.8em;
	font-weight:600;">The View projects below and click the linked items to access 
       reports and manage project data</p>   </div>
</div>
<div class="center_right">
<div class="center_right_top"><a href="" target="_blank">Help &amp; Documentation<img 
        src="images/w.gif" alt="help" style="width: 18px; height: 16px" /></a></div>
<div class="center_right_bottom">show:  <%-- <select name="select">
  </select>--%>
  <asp:DropDownList ID="drpShow" runat="server" AutoPostBack="true">
    <asp:ListItem Value="False" Selected="True">Active</asp:ListItem>
    <asp:ListItem Value="True">Inactive</asp:ListItem>
    <asp:ListItem Value="">All</asp:ListItem>
    </asp:DropDownList>
</div>
</div>
<div class="clear"></div>
<div class="center_content">
<div class="center_content_top"></div>
<div class="center_content_center">
<div class="center_content_title">Account:<span> 
    <asp:Label ID="labAccount" runat="server" Text="">Jhon Smith</asp:Label></span></div>
<div class="center_content_left"><span>Account Subscription Level : </span><asp:Label ID="labSubscriptionLevel" runat="server" Text=""></asp:Label>
<div class="center_content_left_text"><span>Account Administrator(s) : </span><asp:Label ID="labAdinistrator" runat="server" Text="">John 
    Smith,Johnny lin</asp:Label></div>
</div>
<div class="center_content_right">
<div class="center_content_right_text"> <span>Your Projects :</span> <asp:Label ID="labProjects" runat="server" Text=""></asp:Label></div>
<div class="center_content_right_text"><span>Account Project : 
    <asp:Label ID="labAccountProj" runat="server"></asp:Label>
    </span>
    
</div>
</div>
<div class="clear"></div>
<div style="padding-top:5px;">
<table border="0" cellpadding="0" cellspacing="0" >
  <tr>
    <td align="left" style="background-image:url(images/table_headerbg.gif); height:38px; background-repeat:repeat-x; padding-left:10px; text-align:left; line-height:38px; font-weight:bold;">
        Project</td>
     <td style="background-image:url(images/table_headerbg.gif); background-repeat:repeat-x;  padding-left:5px; text-align:center; line-height:38px; font-weight:bold;">
         Keywords</td>
      <td style="background-image:url(images/table_headerbg.gif); background-repeat:repeat-x;  padding-left:5px; text-align:center; line-height:38px; font-weight:bold;">
          Statistics</td>
       <td style="background-image:url(images/table_headerbg.gif); background-repeat:repeat-x;  padding-left:5px; text-align:center; line-height:38px; font-weight:bold;">
           Last Monitor Runs</td>
        <td style="background-image:url(images/table_headerbg.gif); background-repeat:repeat-x;  padding-left:5px; text-align:center; line-height:38px; font-weight:bold;">
            Ranking</td> 
  </tr>

  <tr runat="server" id="displyTable">
    <td align="center" bgcolor="#e1e8f2">&nbsp;
    <asp:Label ID="labName" runat="server" Text=""></asp:Label>
    <br />
    <asp:Label ID="labdom" runat="server" Text=""></asp:Label>
    <br />
    <asp:Label ID="labConnect" runat="server" Text=""></asp:Label>
    <br /><br />
        Status:
    <span style="font-weight:bold; color:#e18331">
    <asp:Label ID="labActiveDate" runat="server" Text="">Active</asp:Label>
    </span>
    </td>
    <td align="center" bgcolor="#e1e8f2">Keywords:
    <span style="font-weight:bold;">
    <asp:Label ID="labKeywords" runat="server" Text=""></asp:Label>
    </span>
    <br />
        Keyword Lists:
    <span style="font-weight:bold;">
    <asp:Label ID="labKeyList" runat="server" Text=""></asp:Label>
    </span>
    </td>
    <td align="center" bgcolor="#e1e8f2">
        PageRank:
    <span style="font-weight:bold;">
    <asp:Label ID="labPageRank" runat="server" Text="4"></asp:Label>
    </span><br />
        Inbound Links:
    <span style="font-weight:bold;">
    <asp:Label ID="labLinks" runat="server" Text="13"></asp:Label>
    </span><br />
        Pages Indexed:
    <span style="font-weight:bold;">
    <asp:Label ID="labIndexed" runat="server" Text="168"></asp:Label>
    </span><br />
        (as of &nbsp;
    <asp:Label ID="labAs" runat="server" Text="08/03/2009"></asp:Label>
        )
    </td>
    <td align="center" bgcolor="#e1e8f2">
    <asp:Label ID="labRuns1" runat="server" Text="08/05/2009 Top10:10%"></asp:Label>
    <br />
    <asp:Label ID="labRuns2" runat="server" Text="07/25/2009 Top10:10%"></asp:Label>
    <br />
    <asp:Label ID="labRuns3" runat="server" Text="Next scheduled run N/A"></asp:Label>
    </td>
    <td align="center" bgcolor="#e1e8f2"><asp:Image ID="ImgRanking" runat="server" ImageUrl="~/images/ReportChart5.png"/></td>
  </tr>
<tr runat="server" id="displayNo" bgcolor="#e1e8f2"><td colspan="5"> &nbsp;</td></tr>
</table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="ObjectDataSource1">
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="Id" 
                DataNavigateUrlFormatString="ManageProject.aspx?Mode=Edit&amp;IdProject={0}" 
                DataTextField="Name" HeaderText="Project" />
            <asp:BoundField DataField="KeywordList" HeaderText="Keywords" 
                SortExpression="KeywordList" />
            <asp:TemplateField HeaderText="Statistics"></asp:TemplateField>
            <asp:BoundField DataField="RankingMonitorRun" HeaderText="Last Monitor Runs" 
                SortExpression="RankingMonitorRun" />
            <asp:TemplateField HeaderText="Ranking"></asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        onselecting="ObjectDataSource1_Selecting" SelectMethod="GetProjectsForUser" 
        TypeName="SEOToolSet.Providers.ProjectManager">
        <SelectParameters>
            <asp:Parameter Name="username" Type="String" />
            <asp:ControlParameter ControlID="drpShow" Name="includeInactive" 
                PropertyName="SelectedValue" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
</div>
<div class="center_content_bottom"></div>
</div>

</div>
</div>
    
</asp:Content>
