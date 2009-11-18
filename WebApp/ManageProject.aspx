<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageProject.aspx.cs"
    Inherits="SEOToolSet.WebApp.ManageProject" MasterPageFile="~/PageBase.Master" %>

<%@ Import Namespace="SEOToolSet.Providers" %>
<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register Src="PageTitle.ascx" TagName="PageTitle" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
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
    <asp:Panel ID="ProjectPanel" runat="server">
        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="scripts/Controllers/ButtonDivHelper.js" />
            </Scripts>
        </asp:ScriptManagerProxy>
        <cc1:ConfirmMessageExt ID="ConfirmMessageExt1" runat="server" CancelButtonText="Cancel"
            ConfirmTitle="Confirmation Delete" OkButtonText="Ok" Selector="a.CommandDelete"
            useConfirmMessageFromElement="True" />
        <cc1:ConfirmMessageExt ID="ConfirmMessageExt2" runat="server" CancelButtonText="Cancel"
            ConfirmTitle="Confirm Exit" OkButtonText="Ok" Selector="a.ClosePopUp" ConfirmMessage="Your changes have not been saved. Are you sure you want to navigate away from this page?"
            useConfirmMessageFromElement="True" />
        <cc1:ConfirmMessageExt ID="ConfirmMessageExt3" runat="server" CancelButtonText="Cancel"
            ConfirmTitle="Confirm Exit" OkButtonText="Ok" Selector="a.CommandAdd" ConfirmMessage="Your changes have not been saved. Are you sure you want to navigate away from this page?"
            useConfirmMessageFromElement="True" />
        <uc1:PageTitle PanelContainerVisible="true" RenderRoundPanelStyles="false" PageDescription="The settings for an individual project can be managed below"
            ID="PageTitle1" runat="server" PageTitleText="<%$ Resources:CommonTerms, ManageProject %>" />
        <div class="DoClear">
        </div>
        <!--Project Information -->
        <cc1:RoundPanel ID="rpProjectManage" runat="server">
            <div class="Legend" style="float: left;">
                <h2>
                    <asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:CommonTerms, ProjectInformation %>"></asp:Localize></h2>
            </div>
            <div id="divProjectnav" style="float: right; color: #06F;" runat="server" visible="false">
                <a id="afirst" href="ManageProject.aspx?mode=add">Add a new project</a> <span id="spanp"
                    style="display: none;">
                    <asp:LinkButton ID="lbtnAddProject" runat="server" CssClass="CommandAdd" OnClick="lbtnAddProject_Click">Add 
                a new project</asp:LinkButton></span> |
                <asp:LinkButton ID="lbtnDelete" runat="server" OnClick="lbtnDelete_Click" CssClass="CommandDelete">Delete 
                this project</asp:LinkButton>
            </div>
            <div class="DoClear">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Block">
                <ContentTemplate>
                    <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick" Enabled="False">
                    </asp:Timer>
                    <asp:FormView ID="fvProject" runat="server" DefaultMode="Edit" Width="100%" DataSourceID="ObjectDataSource1"
                        OnItemInserting="fvProject_ItemInserting" OnItemUpdating="fvProject_ItemUpdating"
                        DataKeyNames="Id" OnItemUpdated="fvProject_ItemUpdated">
                        <EditItemTemplate>
                            <div style="padding-top: 10px;">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td width="28%" height="27" align="right" style="color: #1b6ecc;">
                                            Project Name<sup>*</sup>
                                        </td>
                                        <td width="22%" style="padding-left: 5px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NameTextBox"
                                                Display="Dynamic" ErrorMessage="A project name is required" ValidationGroup="UpdateProject"></asp:RequiredFieldValidator><asp:CustomValidator
                                                    ID="CustomValidator1" runat="server" ControlToValidate="NameTextBox" Display="Dynamic"
                                                    ErrorMessage="Must be unique within the account" OnServerValidate="CustomValidator1_ServerValidate"
                                                    ValidationGroup="UpdateProject"></asp:CustomValidator>
                                            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' ToolTip="The project name identifies the project on reports and throughout the software, and must be unique. Examples: ZZZ Corporation Japan, ABC Company - Foods Division, etc."
                                                CssClass="ProjectManageTextBox" MaxLength="128" onKeyup="SetButtonEnable()" AutoPostBack="True"
                                                OnTextChanged="NameTextBox_TextChanged" />
                                        </td>
                                        <td width="10%" align="right" style="color: #1b6ecc;">
                                            Contact Name<sup>*</sup>
                                        </td>
                                        <td width="40%" style="padding-left: 5px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ContactNameTextBox"
                                                ErrorMessage="Contact Name is required&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                                ValidationGroup="UpdateProject" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="ContactNameTextBox" runat="server" ToolTip="Enter the name of the primary contact person for this project. Example: John Smith."
                                                Text='<%# Bind("ContactName") %>' CssClass="ProjectManageTextBox" MaxLength="128"
                                                onKeyup="SetButtonEnable()" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="27" align="right" style="color: #1b6ecc;">
                                            Company Name<sup>*</sup>
                                        </td>
                                        <td style="padding-left: 5px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ClientNameTextBox"
                                                ErrorMessage="Company Name is required " ValidationGroup="UpdateProject" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="ClientNameTextBox" runat="server" Text='<%# Bind("ClientName") %>'
                                                CssClass="ProjectManageTextBox" ToolTip="Enter the name of the company, organization, person or other entity that this project belongs to."
                                                MaxLength="128" onKeyup="SetButtonEnable()" />
                                        </td>
                                        <td align="right" style="color: #1b6ecc;">
                                            Contact E-mail<sup>*</sup>
                                        </td>
                                        <td style="padding-left: 5px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ContactEmailTextBox"
                                                ErrorMessage="An e-mail address for the contact person is required" ValidationGroup="UpdateProject"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="ContactEmailTextBox"
                                                ErrorMessage="Enter a valid e-mail address&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="UpdateProject"
                                                Display="Dynamic"></asp:RegularExpressionValidator><asp:TextBox ID="ContactEmailTextBox"
                                                    runat="server" Text='<%# Bind("ContactEmail") %>' CssClass="ProjectManageTextBox"
                                                    ToolTip="Enter an e-mail address for the contact person, which may be useful in case of project errors or issues."
                                                    MaxLength="255" onKeyup="SetButtonEnable()" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="27" align="right" style="color: #1b6ecc;">
                                            Domain<sup>*</sup>
                                        </td>
                                        <td style="padding-left: 5px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DomainTextBox"
                                                ErrorMessage="The project’s domain is required " ValidationGroup="UpdateProject"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="DomainTextBox" runat="server" ToolTip="This is the primary Web site domain the project is trying to optimize. Enter only the domain, not a full URL (the &quot;http://&quot; and directory or file names should be omitted). Examples: domain.org; ThisIsMyDomain.co.uk; subdomain.company.com"
                                                Text='<%# Bind("Domain") %>' CssClass="ProjectManageTextBox" onKeyup="SetButtonEnable()" />
                                        </td>
                                        <td align="right" style="color: #1b6ecc;">
                                            Contact Phone
                                        </td>
                                        <td style="padding-left: 5px;">
                                            <asp:TextBox ID="ContactPhoneTextBox" runat="server" Text='<%# Bind("ContactPhone") %>'
                                                BackColor="White" CssClass="ProjectManageTextBox" ToolTip="Please enter a phone number with area code for the contact person. Include country code if outside the U.S."
                                                MaxLength="25" onKeyup="SetButtonEnable()" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="padding-top: 5px;" colspan="4">
                                            <div id="feedbackMsg" class="green">
                                                <asp:Literal ID="lblUpdateResult" runat="server" Text="Project changes have been saved"
                                                    Visible="false"></asp:Literal>
                                            </div>
                                            <div id="divProjectDefault">
                                                <div style="float: left; width: 50%;">
                                                    <cc1:LinkButtonRound Width="100px" ID="LinkButtonAddDisabled" runat="server" Style="margin-left: 340px;"
                                                        CssClass="button disabled" OnClientClick="return false;" Text="<%$ Resources:CommonTerms, Save %>"
                                                        CausesValidation="True" CommandName="Update" ToolTip="Save changes to the project information"
                                                        ImageUrl="images/btn_save.png" ValidationGroup="UpdateProject"></cc1:LinkButtonRound></div>
                                                <div style="float: left;">
                                                    &nbsp;</div>
                                                <div style="float: left;">
                                                    <cc1:LinkButtonRound Width="100px" ID="LinkButtonDeleteDisabled" CssClass="button disabled"
                                                        runat="server" OnClientClick="return false;" Text="<%$ Resources:CommonTerms, Cancel %>"
                                                        ToolTip="Cancel changes to the project information"></cc1:LinkButtonRound></div>
                                            </div>
                                            <div id="divProject" style="display: none;">
                                                <div style="float: left; width: 50%;">
                                                    <cc1:LinkButtonRound Width="100px" ID="LinkButtonAdd" runat="server" Style="margin-left: 340px;"
                                                        Text="<%$ Resources:CommonTerms, Save %>" CausesValidation="True" CommandName="Update"
                                                        ToolTip="Save changes to the project information" ImageUrl="images/btn_save.png"
                                                        ValidationGroup="UpdateProject" OnClientClick="ResetAddProjectlbtn()"></cc1:LinkButtonRound></div>
                                                <div style="float: left;">
                                                    &nbsp;</div>
                                                <div style="float: left;">
                                                    <cc1:LinkButtonRound Width="100px" ID="LinkButtonDelete" CssClass="button ClosePopUp"
                                                        runat="server" Text="<%$ Resources:CommonTerms, Cancel %>" OnClick="LinkButtonDelete_Click"
                                                        ToolTip="Cancel changes to the project information"></cc1:LinkButtonRound></div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <div style="padding-top: 10px;">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td width="28%" height="27" align="right" style="color: #1b6ecc;">
                                            Project Name<sup>*</sup>
                                        </td>
                                        <td width="22%" style="padding-left: 5px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NameTextBox"
                                                ErrorMessage="A project name is required" ValidationGroup="AddProject" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="NameTextBox"
                                                Display="Dynamic" ErrorMessage="Must be unique within the account" OnServerValidate="CustomValidator1_ServerValidate"
                                                ValidationGroup="AddProject"></asp:CustomValidator>
                                            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' ToolTip="The project name identifies the project on reports and throughout the software, and must be unique. Examples: ZZZ Corporation Japan, ABC Company - Foods Division, etc."
                                                CssClass="ProjectManageTextBox" MaxLength="128" onKeyup="SetButtonEnable()" AutoPostBack="True"
                                                OnTextChanged="NameTextBox_TextChanged" />
                                        </td>
                                        <td width="10%" align="right" style="color: #1b6ecc;">
                                            Contact Name<sup>*</sup>
                                        </td>
                                        <td width="40%" style="padding-left: 5px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ContactNameTextBox"
                                                Display="Dynamic" ErrorMessage="Contact Name is required&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                                ValidationGroup="AddProject"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="ContactNameTextBox" runat="server" Text='<%# Bind("ContactName") %>'
                                                CssClass="ProjectManageTextBox" ToolTip="Enter the name of the primary contact person for this project. Example: John Smith."
                                                MaxLength="128" onKeyup="SetButtonEnable()" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="27" align="right" style="color: #1b6ecc;">
                                            Company Name<sup>*</sup>
                                        </td>
                                        <td style="padding-left: 5px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ClientNameTextBox"
                                                Display="Dynamic" ErrorMessage="Company Name is required" ValidationGroup="AddProject"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="ClientNameTextBox" runat="server" Text='<%# Bind("ClientName") %>'
                                                CssClass="ProjectManageTextBox" ToolTip="Enter the name of the company, organization, person or other entity that this project belongs to."
                                                MaxLength="128" onKeyup="SetButtonEnable()" />
                                        </td>
                                        <td align="right" style="color: #1b6ecc;">
                                            Contact E-mail<sup>*</sup>
                                        </td>
                                        <td style="padding-left: 5px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ContactEmailTextBox"
                                                Display="Dynamic" ErrorMessage="An e-mail address for the contact person is required"
                                                ValidationGroup="AddProject"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="ContactEmailTextBox"
                                                Display="Dynamic" ErrorMessage="Enter a valid e-mail address&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="AddProject"></asp:RegularExpressionValidator>
                                            <asp:TextBox ID="ContactEmailTextBox" runat="server" Text='<%# Bind("ContactEmail") %>'
                                                CssClass="ProjectManageTextBox" ToolTip="Enter an e-mail address for the contact person, which may be useful in case of project errors or issues."
                                                MaxLength="255" onKeyup="SetButtonEnable()" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="27" align="right" style="color: #1b6ecc;">
                                            Domain<sup>*</sup>
                                        </td>
                                        <td style="padding-left: 5px;">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DomainTextBox"
                                                ErrorMessage="The project’s domain is required " ValidationGroup="AddProject"
                                                Display="Dynamic"></asp:RequiredFieldValidator><asp:TextBox ID="DomainTextBox" runat="server"
                                                    Text='<%# Bind("Domain") %>' CssClass="ProjectManageTextBox" ToolTip="This is the primary Web site domain the project is trying to optimize. Enter only the domain, not a full URL (the &quot;http://&quot; and directory or file names should be omitted). Examples: domain.org; ThisIsMyDomain.co.uk; subdomain.company.com"
                                                    onKeyup="SetButtonEnable()" />
                                        </td>
                                        <td align="right" style="color: #1b6ecc;">
                                            Contact Phone
                                        </td>
                                        <td style="padding-left: 5px;">
                                            <asp:TextBox ID="ContactPhoneTextBox" runat="server" Text='<%# Bind("ContactPhone") %>'
                                                BackColor="White" CssClass="ProjectManageTextBox" ToolTip="Please enter a phone number with area code for the contact person. Include country code if outside the U.S."
                                                MaxLength="25" onKeyup="SetButtonEnable()" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center" style="padding-top: 5px;">
                                            <div id="divProjectDefault">
                                                <div style="float: left; width: 50%;">
                                                    <cc1:LinkButtonRound Width="100px" ID="LinkButtonAddDisabled" runat="server" Style="margin-left: 340px;"
                                                        CssClass="button disabled" OnClientClick="return false;" Text="<%$ Resources:CommonTerms, Save %>"
                                                        CausesValidation="True" CommandName="Insert" ToolTip="Save changes to the project information"
                                                        ImageUrl="images/btn_save.png" ValidationGroup="AddProject"></cc1:LinkButtonRound></div>
                                                <div style="float: left;">
                                                    &nbsp;</div>
                                                <div style="float: left;">
                                                    <cc1:LinkButtonRound Width="100px" ID="LinkButtonDeleteDisabled" CssClass="button disabled"
                                                        runat="server" OnClientClick="return false;" Text="<%$ Resources:CommonTerms, Cancel %>"
                                                        ToolTip="Cancel changes to the project information"></cc1:LinkButtonRound></div>
                                            </div>
                                            <div id="divProject" style="display: none;">
                                                <div style="float: left; width: 50%;">
                                                    <cc1:LinkButtonRound Width="100px" ID="LinkButtonAdd" runat="server" Style="margin-left: 340px;"
                                                        Text="<%$ Resources:CommonTerms, Save %>" CausesValidation="True" CommandName="Insert"
                                                        ToolTip="Save changes to the project information" ImageUrl="images/btn_save.png"
                                                        ValidationGroup="AddProject"></cc1:LinkButtonRound></div>
                                                <div style="float: left;">
                                                    &nbsp;</div>
                                                <div style="float: left;">
                                                    <cc1:LinkButtonRound Width="100px" ID="LinkButtonDelete" CssClass="button ClosePopUp"
                                                        runat="server" Text="<%$ Resources:CommonTerms, Cancel %>" OnClick="LinkButtonDelete_Click"
                                                        ToolTip="Cancel changes to the project information"></cc1:LinkButtonRound></div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            Id:<asp:Label ID="IdLabel" runat="server" Text='<%# Bind("Id") %>' />
                        </ItemTemplate>
                    </asp:FormView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </cc1:RoundPanel>
        <!--Project Settings -->
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" RenderMode="Block"
            Visible="false">
            <ContentTemplate>
                <cc1:RoundPanel ID="RoundPanel2" runat="server">
                    <div class="FormPanel">
                        <div class="Legend" style="float: left;">
                            <h2>
                                <asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:CommonTerms, ProjectSettings %>"></asp:Localize></h2>
                        </div>
                        <div style="float: right;">
                            <a href="#" id="A1">
                                <%= GetLocalResourceObject("Insertbulkkeywords")%></a>
                        </div>
                        <div style="clear: both;">
                        </div>
                        <div class="FormCSS">
                            <!--Alias Domains-->
                            <div class="TreeHolder1" id="TreeDomainLists">
                                <h3>
                                    <asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:CommonTerms, AliasDomains %>"></asp:Localize>
                                </h3>
                                <ul id="treeDomain" class="simpleTree">
                                    <li class="root" id='Li2'>
                                        <cc1:CustomRepeater ID="DomainsRepeater" DataSourceID="ObjectDataSource1" runat="server">
                                            <HeaderTemplate>
                                                <ul>
                                                    <li class="AddCommandElement CompetitorAdd"><span tabindex='50'>
                                                        <asp:Localize ID="Localize21" runat="server" Text="<%$ Resources:CommonTerms, AddDomain %>"></asp:Localize></span>
                                                    </li>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <li class="Competitor" competitor_id='<%# Eval("Id") %>'><span>
                                                    <%# Eval("Domain") ?? "Unnamed Domain"%></span></li>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </ul>
                                            </FooterTemplate>
                                        </cc1:CustomRepeater>
                                    </li>
                                </ul>
                            </div>
                            <!--Competitors -->
                            <div class="TreeHolder1">
                                <h3>
                                    <asp:Localize ID="Localize19" runat="server" Text="<%$ Resources:CommonTerms, Competitors %>"></asp:Localize>
                                </h3>
                                <ul id="treeCompetitor" class="simpleTree">
                                    <li class="root" id='1'>
                                        <cc1:CustomRepeater ID="CompetitorsRepeater" runat="server" DataSourceID="ObjectDataSource2">
                                            <HeaderTemplate>
                                                <ul>
                                                    <li class="AddCommandElement CompetitorAdd"><span tabindex='50'>
                                                        <asp:Localize ID="Localize21" runat="server" Text="<%$ Resources:CommonTerms, AddCompetitor %>"></asp:Localize></span>
                                                    </li>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <li class="Competitor" competitor_id='<%# Eval("Id") %>'><span>
                                                    <%# Eval("Name") ?? "Unnamed Competitor" %></span></li>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </ul>
                                            </FooterTemplate>
                                        </cc1:CustomRepeater>
                                    </li>
                                </ul>
                            </div>
                            <!--KeywordsLists-->
                            <div class="TreeHolder" id='KeywordsLists'>
                                <div class="HolderTitle">
                                    <h3>
                                        <asp:Localize ID="Localize25" runat="server" Text="<%$ Resources:CommonTerms, KeywordLists %>"></asp:Localize>
                                    </h3>
                                    <div class="DoClear">
                                    </div>
                                </div>
                                <ul id="treeKeywordList" class="simpleTree">
                                    <li class="root" id='Li1'>
                                        <cc1:CustomRepeater ID="CustomRepeaterKeywordList" runat="server" DataSourceID="ObjectDataSource3">
                                            <HeaderTemplate>
                                                <ul>
                                                    <li class="AddCommandElementList KeywordListAdd"><span tabindex='51'>
                                                        <asp:Localize ID="Localize24" runat="server" meta:resourcekey="NewKeywordList"></asp:Localize>
                                                    </span></li>
                                                </ul>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <ul>
                                                    <li class='open KeywordList'><span>
                                                        <%# Eval("Name") ?? "Unnamed KeywordList" %></span>
                                                        <ul>
                                                            <li class='AddCommandElement KeywordAdd' keyword_list_id='<%# Eval("Id") %>'><span
                                                                tabindex='52'>
                                                                <asp:Localize ID="Localize26" runat="server" meta:resourcekey="AddNewKeyword"></asp:Localize>
                                                            </span></li>
                                                            <cc1:CustomRepeater ID="CustomRepeater1" DataSource='<%# Eval("Keyword") %>' runat="server">
                                                                <ItemTemplate>
                                                                    <li class="doc" keyword_id='<%# Eval("Id") %>'><span>
                                                                        <%# Eval("Keyword") ?? "Unnamed Keyword" %>
                                                                    </span></li>
                                                                </ItemTemplate>
                                                            </cc1:CustomRepeater>
                                                        </ul>
                                                    </li>
                                                </ul>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                        </cc1:CustomRepeater>
                                    </li>
                                </ul>
                            </div>
                            <div class="DoClear">
                            </div>
                            <div id='InlineMessage'>
                            </div>
                        </div>
                    </div>
                </cc1:RoundPanel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="DoClear">
        </div>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" InsertMethod="CreateProject"
            SelectMethod="GetProjectById" TypeName="SEOToolSet.Providers.ProjectManager"
            UpdateMethod="UpdateProject" OnSelected="ObjectDataSource1_Selected" OnInserted="ObjectDataSource1_Inserted">
            <UpdateParameters>
                <asp:Parameter Name="id" Type="Int32" />
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="domain" Type="String" />
                <asp:Parameter Name="clientName" Type="String" />
                <asp:Parameter Name="contactEmail" Type="String" />
                <asp:Parameter Name="contactName" Type="String" />
                <asp:Parameter Name="contactPhone" Type="String" />
                <asp:Parameter Name="enabled" Type="Boolean" />
                <asp:Parameter Name="updateBy" Type="String" />
                <asp:Parameter Name="account" Type="Object" />
            </UpdateParameters>
            <SelectParameters>
                <asp:QueryStringParameter Name="id" QueryStringField="IdProject" Type="Int32" />
            </SelectParameters>
            <InsertParameters>
                <asp:Parameter Direction="Output" Name="id" Type="Int32" />
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="domain" Type="String" />
                <asp:Parameter Name="clientName" Type="String" />
                <asp:Parameter Name="contactEmail" Type="String" />
                <asp:Parameter Name="contactName" Type="String" />
                <asp:Parameter Name="contactPhone" Type="String" />
                <asp:Parameter Name="createBy" Type="String" />
                <asp:Parameter Name="account" Type="Object" />
            </InsertParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetCompetitorsByProjectId"
            TypeName="SEOToolSet.Providers.ProjectManager">
            <SelectParameters>
                <asp:ControlParameter ControlID="__Page" Name="idProject" PropertyName="IdProject"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="GetKeywordLists"
            TypeName="SEOToolSet.Providers.ProjectManager">
            <SelectParameters>
                <asp:ControlParameter ControlID="__Page" Name="idProject" PropertyName="IdProject"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </asp:Panel>
</asp:Content>
