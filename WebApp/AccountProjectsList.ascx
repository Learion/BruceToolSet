<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="AccountProjectsList.ascx.cs"
    Inherits="SEOToolSet.WebApp.AccountProjectsList" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>

<script type="text/javascript">
    $(function() {
        var btnAdd = $.byId("<%= LinkButtonAddProject.ClientID %>");
        var btnDelete = $.byId("<%= LinkButtonDelete.ClientID %> ");
        btnAdd.click(function() {
            $.showPopUp('<%= AddProject.ClientID %>', {
                width: 400,
                height: 230,
                onShow: function(panel) {
                   panel
                    .find('a.button.Save')
                    .click(function() {                                                
                        if (Page_ClientValidate && Page_ClientValidate("AddProject")) { $(this).disable(true,{ disableClass : 'disabled' }); return true; }
                    });
                }
            });
            return false;
        });
        btnDelete.click(
                function() {
                    if ($("#<%=RoundPanelProjectsSection.ClientID %> span.Chk input[checked]").length == 0) {
                        $.showMessage($.getResourceString('AtLeastOneProjectSelected','Please select at least one Project'), {icon: $.MessageIco.Warning});                
                        return false;
                    }
                    $.showConfirm('<%= GetGlobalResourceObject("CommonTerms","DeleteItemConfirm") %>'.replace('{0}','<%= GetGlobalResourceObject("CommonTerms","TheSelectedProjects") %>'), {
                        onOkClicked: function() {
                            //__doPostBack('ctl00$contentArea$LinkButtonDelete', '');
                            <%= Page.ClientScript.GetPostBackEventReference(LinkButtonDelete,"") %>
                            
                        },
                        onCancelClicked: function() {

                        }
                    });
                    return false;
                });
    });
</script>

<div id="AddProject" class="DivForDialog" runat="server">
    <div class="wrapper-popUp">
        <asp:FormView ID="formViewProject" runat="server" DataSourceID="odsProject" DataKeyNames="Id"
            DefaultMode="Insert">
            <InsertItemTemplate>
                <div class="FormPanel">
                    <div class="Legend">
                        <h2>
                            <asp:Literal ID="NewProjectLiteral" Text="<%$ Resources:CommonTerms, NewProject %>"
                                runat="server"></asp:Literal>
                        </h2>
                    </div>
                    <div class="FormCSS">
                        <div class="Field OneLine">
                            <label>
                                <asp:Literal ID="NameLiteral" runat="server" Text="<%$ Resources:CommonTerms, Name %>"></asp:Literal>
                            </label>
                            <asp:TextBox ID="NameTextBox" Width="200px" runat="server" CssClass="FormText" Text='<%# Bind("Name") %>' />
                            <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddProject" ControlToValidate="NameTextBox"
                                ID="RequiredFieldValidator2" Text="*" runat="server" ErrorMessage="<%$ Resources:CommonTerms, ProjectNameRequired %>"></asp:RequiredFieldValidator>
                        </div>
                        <div class="Field OneLine">
                            <label>
                                <asp:Literal ID="DomainLiteral" Text="<%$ Resources:CommonTerms, Domain %>" runat="server"></asp:Literal>
                            </label>
                            <asp:TextBox ID="DomainTextBox" Width="200px" runat="server" CssClass="FormText"
                                Text='<%# Bind("Domain") %>' />
                            <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddProject" ControlToValidate="DomainTextBox"
                                ID="RequiredFieldValidator1" Text="*" runat="server" ErrorMessage="<%$ Resources:CommonTerms, DomainRequired %>"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ID="DomainRegexValidation" runat="server" ControlToValidate="DomainTextBox"
                                ErrorMessage="<%$ Resources:CommonTerms, DomainNotValid %>" ValidationExpression="(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w-\./?%&=]*)?"
                                ValidationGroup="AddProject">*</asp:RegularExpressionValidator>
                        </div>
                        <div class="Field OneLine">
                            <label>
                                <asp:Literal ID="Literal2" Text="<%$ Resources:CommonTerms, ClientName %>" runat="server"></asp:Literal>
                            </label>
                            <asp:TextBox ID="ClientNameTextBox" Width="200px" runat="server" CssClass="FormText"
                                Text='<%# Bind("ClientName") %>' />
                        </div>
                        <div class="Field OneLine">
                            <label>
                                <asp:Literal ID="Literal3" Text="<%$ Resources:CommonTerms, ContactName %>" runat="server"></asp:Literal>
                            </label>
                            <asp:TextBox ID="ContactNameTextBox" Width="200px" runat="server" CssClass="FormText"
                                Text='<%# Bind("ContactName") %>' />
                        </div>
                        <div class="Field OneLine">
                            <label>
                                <asp:Literal ID="Literal4" Text="<%$ Resources:CommonTerms, ContactEmail %>" runat="server"></asp:Literal>
                            </label>
                            <asp:TextBox ID="ContactEmailTextBox" Width="200px" runat="server" CssClass="FormText"
                                Text='<%# Bind("ContactEmail") %>' />
                        </div>
                        <div class="Field OneLine">
                            <label>
                                <asp:Literal ID="Literal5" Text="<%$ Resources:CommonTerms, ContactPhone %>" runat="server"></asp:Literal>
                            </label>
                            <asp:TextBox ID="ContactPhoneTextBox" Width="200px" runat="server" CssClass="FormText"
                                Text='<%# Bind("ContactPhone") %>' />
                        </div>
                    </div>
                </div>
                <div class="DoClear">
                </div>
                <div class="CenterWrapper">
                    <ul>
                        <li>
                            <%--<asp:LinkButton ValidationGroup="AddProject" CssClass="LinkCommandRound" ID="ButtonSave"
                                runat="server" CausesValidation="True" CommandName="Insert">
                                <span>
                                    <asp:Literal ID="Literal6" Text="<%$ Resources:CommonTerms, Save %>" runat="server"></asp:Literal>
                                </span>
                            </asp:LinkButton>--%>
                            <cc1:LinkButtonRound ValidationGroup="AddProject" ID="ButtonSave" runat="server" CssClass="button Save"
                                CausesValidation="True" CommandName="Insert" Text="<%$ Resources:CommonTerms, Save %>"></cc1:LinkButtonRound>
                        </li>
                        <li>
                            <%--<a href='###' class="ClosePopUp LinkCommandRound" id="ButtonCancel" runat="server">
                            <span>
                                <asp:Literal ID="Literal7" Text="<%$ Resources:CommonTerms, Cancel %>" runat="server"></asp:Literal></span>
                            </a>--%>
                            <cc1:HyperLinkRound CssClass="button ClosePopUp" runat="server" Text="<%$ Resources:CommonTerms, Cancel %>"
                                ID="ButtonCancel" NavigateUrl="###"></cc1:HyperLinkRound>
                        </li>
                    </ul>
                    <div class="DoClear">
                    </div>
                </div>
            </InsertItemTemplate>
        </asp:FormView>
    </div>
</div>
<cc1:RoundPanel ID="RoundPanelProjectsSection" runat="server">
    <div id="ProjectsSection">
        <asp:PlaceHolder ID="AccountProjectsTitle" runat="server" Visible="true">
            <div class="Legend">
                <h2>
                    <asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:CommonTerms, AccountProjects %>"></asp:Localize>
                </h2>
            </div>
        </asp:PlaceHolder>
        <div class="GridView xTable">
            <cc1:CustomRepeater ID="crAccountProjects" OnItemCommand="crAccountProjects_OnItemCommand"
                DataSourceID="odsAccountProject" runat="server">
                <HeaderTemplate>
                    <table cellspacing="0" border="0" style="border-collapse: collapse;">
                        <thead>
                            <tr class="RowHeader">
                                <th scope="col" style="width: 50px;">
                                    <asp:CheckBox CssClass="ChkAll" ID="chkAll" runat="server" />
                                </th>
                                <th style="width: 250px">
                                    <span>
                                        <asp:Literal ID="Literal3" Text="<%$ Resources:CommonTerms, Project %>" runat="server"></asp:Literal></span>
                                </th>
                                <th>
                                    <asp:Localize ID="LocalizeLastMonitorRun" Text='<%$ Resources:CommonTerms, LastMonitorRun %>'
                                        runat="server"></asp:Localize>
                                </th>
                                <th>
                                    <asp:Localize ID="LocalizeTop10" Text='<%$ Resources:CommonTerms, TopTenPercent %>'
                                        runat="server"></asp:Localize>
                                </th>
                                <th>
                                    <asp:Literal ID="Literal8" Text="<%$ Resources:CommonTerms, Keywords%>" runat="server"></asp:Literal></span>
                                </th>
                                <th>
                                    <asp:Literal ID="Literal9" Text="<%$ Resources:CommonTerms, Ranking %>" runat="server"></asp:Literal></span>
                                </th>
                                <th style="width: 110px">
                                    <asp:Literal ID="Literal10" Text="<%$ Resources:CommonTerms, Reports %>" runat="server"></asp:Literal></span>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="RowItem">
                        <td>
                            <asp:CheckBox CssClass="Chk" ID="chk" runat="server" />
                            <asp:HiddenField ID="hiddenFieldIdProject" Value='<%# Eval("Id") %>' runat="server" />
                        </td>
                        <td>
                            <a id="linkProjectEdit" class="LinkLabel" runat="server" href='<%# Eval("Id","Project.aspx?IdProject={0}") %>'>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ClientName") %>'></asp:Label>
                            </a>
                            <p>
                                <asp:Label runat="server" ID="domain" Text='<%# Eval("Domain") %>'></asp:Label>
                            </p>
                            <div class="CenterWrapper" style="width: 80px">
                                <%--<a id="linkProjectEdit2" class="LinkCommandRound Little" href='<%# Eval("Id","Project.aspx?IdProject={0}") %>'
                                    runat="server"><span>
                                        <asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:CommonTerms, Edit %>"></asp:Localize>
                                    </span></a>--%>
                                <cc1:HyperLinkRound Width="80px" CssClass="button" ID="HyperLinkRound1" runat="server"
                                    Text="<%$ Resources:CommonTerms, Edit %>" href='<%# Eval("Id","Project.aspx?IdProject={0}") %>'></cc1:HyperLinkRound>
                            </div>
                        </td>
                        <td>
                            <div class="Resalted">
                                <p>
                                    2006/15/10</p>
                                <p>
                                    2008/05/03</p>
                            </div>
                        </td>
                        <td>
                            <p>
                                12.0%</p>
                            <p>
                                13.9%</p>
                        </td>
                        <td>
                            <div class="Resalted">
                                <p>
                                    25</p>
                            </div>
                        </td>
                        <td>
                            <img src="images/ReportChart5.png" />
                        </td>
                        <td>
                            <div class="CenterWrapper">
                                <%-- <asp:LinkButton ID="linkLaunchReports" runat="server" CssClass="LinkCommandRound Little"
                                    CausesValidation="False" CommandArgument='<%# Eval("Id") %>' CommandName="Launch">
                                    <span>
                                        <asp:Literal EnableViewState="true" ID="Localize1" runat="server" Text='<%# GetGlobalResourceObject("CommonTerms","Launch") %>'></asp:Literal>
                                    </span>
                                </asp:LinkButton>--%>
                                <cc1:LinkButtonRound CommandArgument='<%# Eval("Id") %>' CommandName="Launch" CausesValidation="false"
                                    CssClass="button" ID="LinkButtonRound1" Text='<%$ Resources:CommonTerms, Launch %>'
                                    runat="server"></cc1:LinkButtonRound>
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="AlternatingRowItem">
                        <td>
                            <asp:CheckBox CssClass="Chk" ID="chk" runat="server" />
                            <asp:HiddenField ID="hiddenFieldIdProject" Value='<%# Eval("Id") %>' runat="server" />
                        </td>
                        <td>
                            <a id="linkProjectEdit" class="LinkLabel" runat="server" href='<%# Eval("Id","Project.aspx?IdProject={0}") %>'>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ClientName") %>'></asp:Label>
                            </a>
                            <p>
                                <asp:Label runat="server" ID="domain" Text='<%# Eval("Domain") %>'></asp:Label></p>
                            <div class="CenterWrapper" style="width: 80px">
                                <%--<a id="linkProjectEdit2" class="LinkCommandRound Little" href='<%# Eval("Id","Project.aspx?IdProject={0}") %>'
                                    runat="server"><span>
                                        <asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:CommonTerms, Edit %>"></asp:Localize>
                                    </span></a>--%>
                                <cc1:HyperLinkRound Width="80px" CssClass="button" ID="HyperLinkRound1" runat="server"
                                    Text="<%$ Resources:CommonTerms, Edit %>" href='<%# Eval("Id","Project.aspx?IdProject={0}") %>'></cc1:HyperLinkRound>
                            </div>
                        </td>
                        <td>
                            <div class="Resalted">
                                <p>
                                    2006/15/10</p>
                                <p>
                                    2008/05/03</p>
                            </div>
                        </td>
                        <td>
                            <p>
                                12.0%</p>
                            <p>
                                13.9%</p>
                        </td>
                        <td>
                            <div class="Resalted">
                                <p>
                                    25</p>
                            </div>
                        </td>
                        <td>
                            <img src="images/ReportChart4.png" />
                        </td>
                        <td>
                            <div class="CenterWrapper" style="width: 80px">
                                <%-- <asp:LinkButton ID="linkLaunchReports" runat="server" CssClass="LinkCommandRound Little"
                                    CausesValidation="False" CommandArgument='<%# Eval("Id") %>' CommandName="Launch">
                                    <span>
                                        <asp:Literal EnableViewState="true" ID="Localize1" runat="server" Text='<%# GetGlobalResourceObject("CommonTerms","Launch") %>'></asp:Literal>
                                    </span>
                                </asp:LinkButton>--%>
                                <cc1:LinkButtonRound CommandArgument='<%# Eval("Id") %>' CommandName="Launch" CausesValidation="false"
                                    CssClass="button" ID="LinkButtonRound1" Text='<%$ Resources:CommonTerms, Launch %>'
                                    runat="server"></cc1:LinkButtonRound>
                            </div>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <EmptyDataTemplate>
                    <tr class="AlternatingRowItem">
                        <td colspan="7">
                            <p class="EmptyMessage">
                                <asp:Literal ID="lit" runat="server" Text='<%$ Resources:CommonTerms, NoProjectsFound %>'></asp:Literal></p>
                        </td>
                    </tr>
                </EmptyDataTemplate>
                <FooterTemplate>
                    </tbody> </table>
                </FooterTemplate>
            </cc1:CustomRepeater>
        </div>
        <div class="CenterWrapper" id='test'>
            <ul>
                <li>
                    <%--<asp:LinkButton ID="LinkButtonAddProject" CssClass="LinkCommandRound" runat="server">
                        <span>
                            <asp:Literal runat="server" ID="lit01" Text='<%$ Resources:CommonTerms, Add %>'></asp:Literal></span></asp:LinkButton>--%>
                    <cc1:HyperLinkRound ID="LinkButtonAddProject" runat="server" Text='<%$ Resources:CommonTerms, Add %>'></cc1:HyperLinkRound>
                </li>
                <li>
                    <%--<asp:LinkButton ID="LinkButtonDelete" CssClass="LinkCommandRound" runat="server"
                        OnClick="LinkButtonDelete_Click">
                        <span>
                            <asp:Literal runat="server" ID="Literal1" Text='<%$ Resources:CommonTerms, Delete %>'></asp:Literal></span></asp:LinkButton>--%>
                    <cc1:LinkButtonRound ID="LinkButtonDelete" runat="server" Text='<%$ Resources:CommonTerms, Delete %>'
                        OnClick="LinkButtonDelete_Click"></cc1:LinkButtonRound>
                </li>
            </ul>
            <div class="DoClear">
            </div>
        </div>
    </div>
</cc1:RoundPanel>
<asp:ObjectDataSource ID="odsAccountProject" runat="server" OnSelecting="odsAccountProject_Selecting"
    SelectMethod="GetProjectsByAccount" TypeName="SEOToolSet.Providers.ProjectManager"
    DeleteMethod="DeleteProject">
    <DeleteParameters>
        <asp:Parameter Name="id" Type="Int32" />
    </DeleteParameters>
    <SelectParameters>
        <asp:Parameter Name="account" Type="Object" />
        <asp:Parameter DefaultValue="false" Name="includeInactive" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource TypeName="SEOToolSet.Providers.ProjectManager" SelectMethod="GetProjectById"
    ID="odsProject" runat="server" InsertMethod="CreateProject" OnInserting="odsProject_OnInserting"
    OnInserted="odsProject_Inserted">
    <SelectParameters>
        <asp:Parameter Name="id" Type="Int32" />
    </SelectParameters>
    <InsertParameters>
        <asp:Parameter Direction="Output" Name="id" Type="Int32" />
        <asp:Parameter Name="name" Type="String" />
        <asp:Parameter Name="domain" Type="String" />
        <asp:Parameter Name="clientName" Type="String" />
        <asp:Parameter Name="contactEmail" Type="String" />
        <asp:Parameter Name="contactName" Type="String" />
        <asp:Parameter Name="contactPhone" Type="String" />
        <asp:Parameter Name="account" Type="Object" />
    </InsertParameters>
</asp:ObjectDataSource>
