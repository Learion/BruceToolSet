<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderPage.ascx.cs"
    Inherits="SEOToolSet.WebApp.HeaderPage" %>
<%@ Import Namespace="SEOToolSet.WebApp.Helper" %>
<%@ Register TagPrefix="user" TagName="MainMenu" Src="MainMenu.ascx" %>
<%@ Register TagPrefix="user" TagName="SubMenu" Src="SubMenu.ascx" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc3" %>
<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<uc3:IncludeFile ID="IncludeFile2" FilePath="~/scripts/Controls/R3M.Combobox/cbx.css"
    TypeOfFile="Css" runat="server" />
<uc3:IncludeFile ID="IncludeFile4" FilePath="~/scripts/plugins/jquery.scrollTo.js"
    TypeOfFile="Javascript" runat="server" />
<uc3:IncludeFile ID="IncludeFile1" FilePath="~/scripts/Controls/R3M.Combobox/combobox.js"
    TypeOfFile="Javascript" runat="server" />
<uc3:IncludeFile ID="IncludeFile3" FilePath="~/css/Header.css" TypeOfFile="Css" runat="server" />

<script language="javascript" type="text/javascript">

    $.Namespace('$.CurrentPage');    
    $.CurrentPage.cbxProjects = null;        

    $(function() {
    
        var excess1 = $.browser.msie ? (($.browser.version.indexOf('6') > -1) ? 50 : 35) : 30;        
        $('div.QuickLinks').width($('ul.MainPanel').width() + excess1);

        
//        var cbxLanguages = new $.R3M.Combobox('#<%//= DropDownLanguages.ClientID %>', { width: 145, extraCssClass: 'Languages'  });
//        cbxLanguages.addEventListener("Changed", function(args) {
//            var val = args.select.val();
//            if (val != 'choose') {
//                $.showMessage('<%= Resources.JavascriptMessages.PageReloading %>' , 
//                    { 
//                        onShow:function() {
//                            setTimeout(function() {
//                                document.cookie = "CurrentCulture=" + val;
//                                document.location.reload();
//                            }, 500);
//                    }
//                });
//            }
//        });
        
        var checkProjectNameLength = function (cbxProjects) {
            var cbxValueCtnr = $(cbxProjects.getCbxValueContainer()).find('.CbxValueContent')            
            if (cbxProjects.getDropDown().val()  == '-1')  {                
                cbxValueCtnr.html('<%= Resources.CommonTerms.NoProjectsActive %>')                
            }
            else {                
                var text = cbxValueCtnr.text();
                cbxValueCtnr.text($.ellipsis(text, {max: 30}));
                cbxValueCtnr.attr('title',text);
           }
        }
        
        $.CurrentPage.cbxLanguages = cbxLanguages;
        var cbxProjects = $.CurrentPage.cbxProjects = new $.R3M.Combobox('#<%= DropDownCurrentDomain.ClientID %>', { width: 250, extraCssClass: 'ProjectCbx', dropDownListHeight: 250, optionsAlignmentHorizontal : 'RIGHT'});
        
        checkProjectNameLength(cbxProjects);        
                
        cbxProjects.addEventListener("Changed", function(args) {             
            if (args.select.val() != '-1')                 
            {	                                                                            
                checkProjectNameLength(cbxProjects);
                <%= AjaxCallback1.ClientMethodName %>("changeCurrentDomain", args.select.val());                               
            }  
        });
        
        cbxProjects.addEventListener('Changing', function(args) {               
            if (args.itemJNode.attr('cbx_value') == '-1') {                    
                location.href = '<%= ResolveClientUrl("~/Home.aspx") %>';                
                args.cancel = true;
                return false; // prevent the other listeners to be rised.
            }
        }); 
        var excess1 = $.browser.msie ? (($.browser.version.indexOf('6') > -1) ? 50 : 25) : 30;        
        $('div.QuickLinks').width($('ul.MainPanel').width() + excess1);                                                                                                     
    });
</script>

<script type="text/javascript">
      function <%= ClientID %>_Callback(result, context) {
          if (result != 'true') {
             $.showMessage('<%= Resources.JavascriptMessages.ProjectCouldNotBeChanged %>');            
          }
      }
      function <%= ClientID %>_Error(message) {
         $.showMessage('[Error] : ' + message);            
      }
</script>

<cc1:AjaxCallback ID="AjaxCallback1" NotifyErrors="true" OnScriptCallback="AjaxCallback1_ScriptCallback"
    runat="server" UseParentClientIdAsPrefix="true" OnClientCallback="Callback" OnClientError="Error" />
<div class="Header">
    <div class="Logo">
        &nbsp;
    </div>
    <div class="DoClear">
        &nbsp;</div>
    <div class="Menu">
        <%--<user:MainMenu ID="ucMainMenu" runat="server"></user:MainMenu>--%>
        <div class="DoClear">
            &nbsp;</div>
        <%--<user:SubMenu ID="ucSubMenu" runat="server"></user:SubMenu>--%>
    </div>
    <div class="QuickLinks">
        <cc1:RoundPanel ID="RoundPanel1" NotRenderStyles="true" runat="server" DiscardTop="true"
            CssIdClassName="round_ctr ublock">
            <ul class="MainPanel">
                <li>
                    <asp:LoginView ID="LoginView1" runat="server">
                        <AnonymousTemplate>
                            <asp:Label CssClass="SpanFirst" ID="WelcomeAnonymUser" runat="server" Text="<%$ Resources:CommonTerms, WelcomeAnonymous %>"></asp:Label>
                        </AnonymousTemplate>
                        <LoggedInTemplate>
                            <asp:LinkButton ID="lnkUserAcount" runat="server" PostBackUrl="UserProfile.aspx"
                                CausesValidation="False" CssClass="External">
                                <asp:LoginName ID="WelcomeUser" runat="server" FormatString="<%$ Resources:CommonTerms, Welcome %>" />
                            </asp:LinkButton>
                        </LoggedInTemplate>
                    </asp:LoginView>
                </li>
                <li class="Separator">| </li>
                <li style="margin-top: 0px;">
                    <asp:LoginView ID="LoginView2" runat="server">
                        <LoggedInTemplate>
                            <a href="AccountProfile.aspx">
                                <asp:Label ID="userAccount" runat="server" Text="<%$ Resources:CommonTerms, Account %>"></asp:Label>
                            </a>
                            <li class="Separator">| </li>
                        </LoggedInTemplate>
                    </asp:LoginView>
                </li>
                <li><a href="UserSupport.aspx">
                    <asp:Label ID="userSupport" runat="server" Text="<%$ Resources:CommonTerms, Support %>"></asp:Label></a>
                </li>
                <li class="Separator">| </li>
                <li>
                    <asp:LoginView ID="LoginView3" runat="server">
                        <AnonymousTemplate>
                            <a href="LoginPage.aspx">
                                <asp:Label ID="Label1" runat="server" Text='<%$ Resources:CommonTerms, SignIn %>'></asp:Label></a>
                        </AnonymousTemplate>
                        <LoggedInTemplate>
                            <asp:LoginStatus OnLoggedOut="LoginStatus_OnLoggedOut" ID="StatusUser" runat="server"
                                CssClass="External" LoginText="<%$ Resources:CommonTerms, SignIn %>" LogoutText="<%$ Resources:CommonTerms, SignOut %>" />
                        </LoggedInTemplate>
                    </asp:LoginView>
                </li>
                <%--<li class="Separator">| </li>
                <li>
                    <asp:DropDownList CssClass="FormCbx" ID="DropDownLanguages" runat="server">
                        <asp:ListItem Text="<%$ Resources:CommonTerms, ChooseLanguage %>" Value="choose" />
                        <asp:ListItem Text="English(US)" Value="en-US" />
                        <asp:ListItem Text="Espa&ntilde;ol(Peru)" Value="es-PE" />
                        <asp:ListItem Text="Japanese (Japan)" Value="ja-JP" />
                    </asp:DropDownList>
                </li>--%>
            </ul>
            <div class="DoClear">
                &nbsp;</div>
        </cc1:RoundPanel>
    </div>
    <% if (Page.User.Identity.IsAuthenticated)
       { %>
    <div class="DomainSelectionTool">
        <label for="<%= ClientID %>_DropDownCurrentDomain">
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:CommonTerms, Project %>"></asp:Literal>:
        </label>
        <div id="projectSelect">
            <ul>
                <li>
                    <asp:DropDownList ID="DropDownCurrentDomain" runat="server" DataSourceID="odsProject"
                        DataTextField="Name" DataValueField="Id" OnDataBound="DropDownCurrentDomain_DataBound">
                    </asp:DropDownList>
                </li>
            </ul>
            <div class="DoClear">
            </div>
        </div>
    </div>
    <% } %>
</div>
<asp:ObjectDataSource ID="odsProject" runat="server" SelectMethod="GetProjectsForUser"
    TypeName="SEOToolSet.Providers.ProjectManager" OnSelecting="odsProject_Selecting">
    <SelectParameters>
        <asp:Parameter Name="username" Type="String" />
        <asp:Parameter DefaultValue="false" Name="includeInactive" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>
