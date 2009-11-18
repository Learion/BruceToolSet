<%@ Page Title="" Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true"
    CodeBehind="ManageAccountUsers.aspx.cs" Inherits="SEOToolSet.WebApp.ManageAccountUsers" %>

<%@ Import Namespace="SEOToolSet.Providers" %>
<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register Src="PageTitle.ascx" TagName="PageTitle" TagPrefix="uc1" %>
<%@ Register Src="~/ArrayRenderer.ascx" TagName="ArrayRenderer" TagPrefix="uc2" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">
    <uc3:IncludeFile ID="IncludeFile11" runat="server" TypeOfFile="Css" FilePath="~/css/ManageAccountUsers.css" />
    <uc1:PageTitle PanelContainerVisible="true" ID="PageTitle1" runat="server" RenderRoundPanelStyles="false"
        meta:resourcekey="PageTitle" />
    <div class="ColumnUsersDetail">
        <cc1:RoundPanel ID="_roundPanelUsersDetail" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="IdUserSelected" runat="server" />
                    <asp:LinkButton ID="LinkButtonRefreshUserPanel" CssClass="Invisible" OnClick="RefreshFormView"
                        runat="server">Refresh</asp:LinkButton>
                    <asp:FormView ID="FormView1" runat="server" DataSourceID="_odsSEOMemebershipUser"
                        OnItemInserting="FormView1_OnItemInserting" OnDataBound="Formview1_OnDataBound"
                        OnItemUpdating="FormView1_OnItemUpdating">
                        <InsertItemTemplate>
                            <div class="FormPanel">
                                <div class="Legend">
                                    <h2>
                                        <asp:Localize ID="Localize43" runat="server" Text="Add New User"></asp:Localize></h2>
                                </div>
                                <div class="FormCSS">
                                    <div id="AjaxValidationSummary" style="margin-bottom: 5px;">
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="Localize44" runat="server" Text="Login ID"></asp:Localize>
                                            *</label><asp:RequiredFieldValidator CssClass="Validator" ID="LoginRequired" runat="server"
                                                ErrorMessage="Login ID is required" Display="Dynamic" Text="Login ID is required"
                                                ControlToValidate="LoginTextBox" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                                        <asp:TextBox CssClass="FormText Required" Width="200px" ID="LoginTextBox" runat="server"
                                            Text='<%# Bind("Login") %>' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="UserRoleLabel" runat="server" Text="<%$ Resources:CommonTerms, UserRole %>"></asp:Localize>
                                            *
                                        </label>
                                        <asp:RequiredFieldValidator CssClass="Validator" ID="ListRolesRequired" runat="server"
                                            ErrorMessage="<%$ Resources:CommonTerms, RoleRequired %>" Display="Dynamic" Text="<%$ Resources:CommonTerms, RoleRequired %>"
                                            ControlToValidate="DropDownListRoles" InitialValue="-1" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="DropDownListRoles" Width="200px" runat="server" DataSourceID="odsRoles"
                                            DataTextField="Name" DataValueField="Id" AppendDataBoundItems="True" CssClass="FormCbx Required">
                                            <asp:ListItem Value="-1" Text="<%$ Resources:CommonTerms, Choose %>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="FirstNameLabel" runat="server" Text="<%$ Resources:CommonTerms, FirstName %>"></asp:Localize>
                                            *
                                        </label>
                                        <asp:RequiredFieldValidator CssClass="Validator" ID="FirstNameRequired" runat="server"
                                            ErrorMessage="First Name is required" Display="Dynamic" Text="First Name is required"
                                            ControlToValidate="FirstNameTextBox" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                                        <asp:TextBox CssClass="FormText Required" Width="200px" ID="FirstNameTextBox" runat="server"
                                            Text='<%# Bind("FirstName") %>' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="LastNameLabel" runat="server" Text="<%$ Resources:CommonTerms, LastName %>"></asp:Localize>
                                            *
                                        </label>
                                        <asp:RequiredFieldValidator CssClass="Validator" ID="LastNameRequired" runat="server"
                                            ErrorMessage="Last Name (surname) is required" Display="Dynamic" Text="Last Name (surname) is required"
                                            ControlToValidate="LastNameTextBox" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                                        <asp:TextBox CssClass="FormText Required" Width="200px" ID="LastNameTextBox" runat="server"
                                            Text='<%# Bind("LastName") %>' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="EmailLabel" runat="server" Text="<%$ Resources:CommonTerms, Email %>"></asp:Localize>
                                            *
                                        </label>
                                        <asp:RequiredFieldValidator CssClass="Validator" ID="EmailRequired" runat="server"
                                            ErrorMessage="<%$ Resources:CommonTerms, EmailRequired %>" Display="Dynamic"
                                            Text="<%$ Resources:CommonTerms, EmailRequired %>" ControlToValidate="EmailTextBox"
                                            ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                                        <asp:TextBox CssClass="FormText Required" Width="200px" ID="EmailTextBox" runat="server"
                                            Text='<%# Bind("Email") %>' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="CountryLabel" runat="server" Text="<%$ Resources:CommonTerms, Country %>"></asp:Localize>
                                        </label>
                                        <asp:DropDownList Width="200px" CssClass="FormDropDown" AppendDataBoundItems="True"
                                            ID="DropDownListCountry" runat="server" DataSourceID="odsCountries" DataTextField="Name"
                                            DataValueField="Id">
                                            <asp:ListItem Value="-1" Text="<%$ Resources:CommonTerms, Choose %>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="AddressLabel" runat="server" Text="<%$ Resources:CommonTerms, Address %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText" Width="200px" ID="Address1TextBox" runat="server"
                                            Text='<%# Bind("Address1") %>' />
                                    </div>
                                    <div class="Field ">
                                        <asp:TextBox CssClass="FormText" Width="200px" ID="Address2TextBox" runat="server"
                                            Text='<%# Bind("Address2") %>' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="CityLabel" runat="server" Text="<%$ Resources:CommonTerms, City %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText" Width="200px" ID="CityTownTextBox" runat="server"
                                            Text='<%# Bind("CityTown") %>' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="StateLabel" runat="server" Text="<%$ Resources:CommonTerms, State %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText" Width="200px" ID="StateTextBox" runat="server" Text='<%# Bind("State") %>' />
                                    </div>
                                    <div class="Field">
                                        <label class="ShortLabel">
                                            <asp:Localize ID="ZipLabel" runat="server" Text="<%$ Resources:CommonTerms, Zip %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText" Width="200px" ID="ZipTextBox" runat="server" Text='<%# Bind("Zip") %>' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="PhoneLabel" runat="server" Text="<%$ Resources:CommonTerms, Telephone %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText" Width="200px" ID="TelephoneTextBox" runat="server"
                                            Text='<%# Bind("Telephone") %>' />
                                    </div>
                                    <div class="DoClear">
                                    </div>
                                </div>
                                <div class="AddUserToolBar">
                                    <ul>
                                        <li>
                                            <cc1:LinkButtonRound Width="100px" ID="LinkButtonAdd" runat="server" CommandName="Insert"
                                                ValidationGroup="AddUser" Text="<%$ Resources:CommonTerms, Save %>"></cc1:LinkButtonRound>
                                        </li>
                                        <li>
                                            <cc1:LinkButtonRound Width="100px" ID="LinkButtonDelete" CssClass="button ClosePopUp"
                                                runat="server" CommandName="Cancel" Text="<%$ Resources:CommonTerms, Cancel %>"></cc1:LinkButtonRound>
                                        </li>
                                    </ul>
                                    <div class="DoClear">
                                    </div>
                                    <p class="RequiedNotice Small">
                                        * Required fields</p>
                                </div>
                            </div>
                        </InsertItemTemplate>
                        <EmptyDataTemplate>
                            <div class="FormPanel">
                                <div class="Legend">
                                    <h2>
                                        <asp:Localize ID="Localize43" runat="server" Text="User Detail"></asp:Localize></h2>
                                </div>
                                <div class="FormCSS">
                                    <div class="Field ">
                                        <div class="CheckWrapper">
                                            <label>
                                                <asp:CheckBox Enabled="false" ID="_chkResetPassword" runat="server" />
                                                Reset Password</label>
                                        </div>
                                    </div>
                                    <div class="Field">
                                        <label>
                                            <asp:Localize ID="Localize44" runat="server" Text="Login ID"></asp:Localize></label><asp:TextBox
                                                CssClass="FormText Disabled" Width="200px" ID="LoginTextBox" runat="server" Text='' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="UserRoleLabel" runat="server" Text="<%$ Resources:CommonTerms, UserRole %>"></asp:Localize>
                                        </label>
                                        <asp:DropDownList Enabled="false" ID="DropDownListRoles" Width="200px" runat="server"
                                            DataTextField="Name" DataValueField="Id" AppendDataBoundItems="True">
                                            <asp:ListItem Value="-1" Text="<%$ Resources:CommonTerms, Choose %>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div style="clear: both">
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="FirstNameLabel" runat="server" Text="<%$ Resources:CommonTerms, FirstName %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText Disabled" Width="200px" ID="FirstNameTextBox" runat="server"
                                            Text='' />
                                    </div>
                                    <div class="Field">
                                        <label>
                                            <asp:Localize ID="LastNameLabel" runat="server" Text="<%$ Resources:CommonTerms, LastName %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText Disabled" Width="200px" ID="LastNameTextBox" runat="server"
                                            Text='' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="EmailLabel" runat="server" Text="<%$ Resources:CommonTerms, Email %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText Disabled" Width="200px" ID="EmailTextBox" runat="server"
                                            Text='' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="CountryLabel" runat="server" Text="<%$ Resources:CommonTerms, Country %>"></asp:Localize>
                                        </label>
                                        <asp:DropDownList Enabled="false" Width="200px" CssClass="FormDropDown" AppendDataBoundItems="True"
                                            ID="DropDownListCountry" runat="server" DataTextField="Name" DataValueField="Id">
                                            <asp:ListItem Value="-1" Text="<%$ Resources:CommonTerms, Choose %>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="AddressLabel" runat="server" Text="<%$ Resources:CommonTerms, Address %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText Disabled" Width="200px" ID="Address1TextBox" runat="server"
                                            Text='' />
                                    </div>
                                    <div class="Field ">
                                        <asp:TextBox CssClass="FormText Disabled" Width="200px" ID="Address2TextBox" runat="server"
                                            Text='' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="CityLabel" runat="server" Text="<%$ Resources:CommonTerms, City %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText Disabled" Width="200px" ID="CityTownTextBox" runat="server"
                                            Text='' />
                                    </div>
                                    <div class="Field">
                                        <label>
                                            <asp:Localize ID="StateLabel" runat="server" Text="<%$ Resources:CommonTerms, State %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText Disabled" Width="200px" ID="StateTextBox" runat="server"
                                            Text='' />
                                    </div>
                                    <div class="Field">
                                        <label class="ShortLabel">
                                            <asp:Localize ID="ZipLabel" runat="server" Text="<%$ Resources:CommonTerms, Zip %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText Disabled" Width="200px" ID="ZipTextBox" runat="server"
                                            Text='' />
                                    </div>
                                    <div class="Field">
                                        <label>
                                            <asp:Localize ID="PhoneLabel" runat="server" Text="<%$ Resources:CommonTerms, Telephone %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText Disabled" Width="200px" ID="TelephoneTextBox" runat="server"
                                            Text='' />
                                    </div>
                                    <div class="DoClear">
                                    </div>
                                </div>
                                <div class="AddUserToolBar">
                                    <ul>
                                        <li>
                                            <cc1:LinkButtonRound Width="100px" ID="LinkButtonAdd" runat="server" CssClass="button disabled"
                                                OnClientClick="return false;" Text="<%$ Resources:CommonTerms, Save %>"></cc1:LinkButtonRound>
                                        </li>
                                        <li>
                                            <cc1:LinkButtonRound Width="100px" ID="LinkButtonDelete" CssClass="button disabled"
                                                runat="server" OnClientClick="return false;" Text="<%$ Resources:CommonTerms, Cancel %>"></cc1:LinkButtonRound>
                                        </li>
                                    </ul>
                                    <div class="DoClear">
                                    </div>
                                </div>
                            </div>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <div class="FormPanel">
                                <div class="Legend">
                                    <h2>
                                        <asp:Localize ID="Localize43" runat="server" Text="Edit User"></asp:Localize></h2>
                                </div>
                                <div class="FormCSS">
                                    <div id="AjaxValidationSummary" style="margin-bottom: 5px;">
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="Localize44" runat="server" Text="Login ID"></asp:Localize>
                                            *</label><asp:RequiredFieldValidator CssClass="Validator" ID="LoginRequired" runat="server"
                                                ErrorMessage="Login ID is required" Display="Dynamic" Text="Login ID is required"
                                                ControlToValidate="LoginTextBox" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                                        <asp:TextBox CssClass="FormText Required" Width="200px" ID="LoginTextBox" runat="server"
                                            Text='<%# Bind("Login") %>' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="UserRoleLabel" runat="server" Text="<%$ Resources:CommonTerms, UserRole %>"></asp:Localize>
                                            *
                                        </label>
                                        <asp:RequiredFieldValidator CssClass="Validator" ID="ListRolesRequired" runat="server"
                                            ErrorMessage="<%$ Resources:CommonTerms, RoleRequired %>" Display="Dynamic" Text="<%$ Resources:CommonTerms, RoleRequired %>"
                                            ControlToValidate="DropDownListRoles" InitialValue="-1" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="DropDownListRoles" Width="200px" runat="server" DataSourceID="odsRoles"
                                            SelectedValue='<%# Eval("UserRole") != null ? Eval("UserRole.Id") : "-1" %>'
                                            DataTextField="Name" DataValueField="Id" AppendDataBoundItems="True" CssClass="FormCbx Required">
                                            <asp:ListItem Value="-1" Text="<%$ Resources:CommonTerms, Choose %>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="FirstNameLabel" runat="server" Text="<%$ Resources:CommonTerms, FirstName %>"></asp:Localize>
                                            *
                                        </label>
                                        <asp:RequiredFieldValidator CssClass="Validator" ID="FirstNameRequired" runat="server"
                                            ErrorMessage="First Name is required" Display="Dynamic" Text="First Name is required"
                                            ControlToValidate="FirstNameTextBox" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                                        <asp:TextBox CssClass="FormText Required" Width="200px" ID="FirstNameTextBox" runat="server"
                                            Text='<%# Bind("FirstName") %>' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="LastNameLabel" runat="server" Text="<%$ Resources:CommonTerms, LastName %>"></asp:Localize>
                                            *
                                        </label>
                                        <asp:RequiredFieldValidator CssClass="Validator" ID="LastNameRequired" runat="server"
                                            ErrorMessage="Last Name (surname) is required" Display="Dynamic" Text="Last Name (surname) is required"
                                            ControlToValidate="LastNameTextBox" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                                        <asp:TextBox CssClass="FormText Required" Width="200px" ID="LastNameTextBox" runat="server"
                                            Text='<%# Bind("LastName") %>' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="EmailLabel" runat="server" Text="<%$ Resources:CommonTerms, Email %>"></asp:Localize>
                                            *
                                        </label>
                                        <asp:RequiredFieldValidator CssClass="Validator" ID="EmailRequired" runat="server"
                                            ErrorMessage="<%$ Resources:CommonTerms, EmailRequired %>" Display="Dynamic"
                                            Text="<%$ Resources:CommonTerms, EmailRequired %>" ControlToValidate="EmailTextBox"
                                            ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                                        <asp:TextBox CssClass="FormText Required" Width="200px" ID="EmailTextBox" runat="server"
                                            Text='<%# Bind("Email") %>' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="CountryLabel" runat="server" Text="<%$ Resources:CommonTerms, Country %>"></asp:Localize>
                                        </label>
                                        <asp:DropDownList Width="200px" CssClass="FormDropDown" AppendDataBoundItems="True"
                                            ID="DropDownListCountry" runat="server" DataSourceID="odsCountries" DataTextField="Name"
                                            SelectedValue='<%# Eval("Country") != null ? Eval("Country.Id") : "-1" %>' DataValueField="Id">
                                            <asp:ListItem Value="-1" Text="<%$ Resources:CommonTerms, Choose %>"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="AddressLabel" runat="server" Text="<%$ Resources:CommonTerms, Address %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText" Width="200px" ID="Address1TextBox" runat="server"
                                            Text='<%# Bind("Address1") %>' />
                                    </div>
                                    <div class="Field ">
                                        <asp:TextBox CssClass="FormText" Width="200px" ID="Address2TextBox" runat="server"
                                            Text='<%# Bind("Address2") %>' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="CityLabel" runat="server" Text="<%$ Resources:CommonTerms, City %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText" Width="200px" ID="CityTownTextBox" runat="server"
                                            Text='<%# Bind("CityTown") %>' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="StateLabel" runat="server" Text="<%$ Resources:CommonTerms, State %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText" Width="200px" ID="StateTextBox" runat="server" Text='<%# Bind("State") %>' />
                                    </div>
                                    <div class="Field">
                                        <label class="ShortLabel">
                                            <asp:Localize ID="ZipLabel" runat="server" Text="<%$ Resources:CommonTerms, Zip %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText" Width="200px" ID="ZipTextBox" runat="server" Text='<%# Bind("Zip") %>' />
                                    </div>
                                    <div class="Field ">
                                        <label>
                                            <asp:Localize ID="PhoneLabel" runat="server" Text="<%$ Resources:CommonTerms, Telephone %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox CssClass="FormText" Width="200px" ID="TelephoneTextBox" runat="server"
                                            Text='<%# Bind("Telephone") %>' />
                                    </div>
                                    <div class="DoClear">
                                    </div>
                                </div>
                                <div class="AddUserToolBar">
                                    <ul>
                                        <li>
                                            <cc1:LinkButtonRound ID="LinkButtonAdd" runat="server" Width="100px" CommandName="Update"
                                                ValidationGroup="AddUser" Text="<%$ Resources:CommonTerms, Save %>"></cc1:LinkButtonRound>
                                        </li>
                                        <li>
                                            <cc1:LinkButtonRound ID="LinkButtonDelete" Width="100px" runat="server" OnClick="Formview_DoCancel"
                                                CausesValidation="false" Text="<%$ Resources:CommonTerms, Cancel %>"></cc1:LinkButtonRound>
                                        </li>
                                    </ul>
                                    <div class="DoClear">
                                    </div>
                                </div>
                            </div>
                        </EditItemTemplate>
                    </asp:FormView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="_linkAddNewUser" />
                </Triggers>
            </asp:UpdatePanel>
        </cc1:RoundPanel>
    </div>
    <div class="ColumnUsers">
        <cc1:RoundPanel ID="AccountUsersRoundPanel" runat="server" UpdateMode="Conditional">
            <div class="FormPanel">
                <div class="Legend">
                    <h2>
                        <asp:Literal ID="Literal1" runat="server" meta:resourcekey="AccountUsers"></asp:Literal></h2>
                    <span>
                        <asp:Literal ID="Literal2" runat="server" meta:resourcekey="AccountUsersDescription"></asp:Literal></span>
                    <asp:LinkButton ID="_linkAddNewUser" runat="server" CssClass="AddNewUser" OnClick="DoAddNewUser">
                        <asp:Label ID="_label" runat="server" meta:resourcekey="AddNewUserCommand"></asp:Label>
                    </asp:LinkButton>
                </div>
            </div>
            <div id="AccountUsersTable" class="xTable">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView OnRowCommand="OnRowCommand" OnRowDataBound="OnRowDataBound" EnableViewState="false"
                            ID="_gridviewUsers" runat="server" DataSourceID="_odsSeoMembershipUsers" DataKeyNames="Id"
                            GridLines="None" AllowSorting="True" RowStyle-CssClass="RowItem clickable" AlternatingRowStyle-CssClass="AlternatingRowItem clickable"
                            HeaderStyle-CssClass="RowHeader" AutoGenerateColumns="False">
                            <RowStyle CssClass="RowItem clickable" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderStyle CssClass="Sortable" />
                                    <HeaderTemplate>
                                        <div class='SortIndicator <%# EvalSortBy("FirstName") %>'>
                                        </div>
                                        <asp:LinkButton ID="_linkHeaderName" CommandName="CustomSort" CommandArgument="FirstName"
                                            runat="server">
                                            <asp:Label ID="_nameHeadingLiteral1" meta:resourcekey="Name" runat="server"></asp:Label></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemStyle CssClass="Left" />
                                    <ItemTemplate>
                                        <%# string.Concat(Eval("FirstName"), " ", Eval("LastName"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle CssClass='Sortable' />
                                    <HeaderTemplate>
                                        <div class='SortIndicator <%# EvalSortBy("Login") %>'>
                                        </div>
                                        <asp:LinkButton ID="_linkHeaderLoginId" CommandName="CustomSort" CommandArgument="Login"
                                            runat="server">
                                            <asp:Label ID="_nameHeadingLiteral2" meta:resourcekey="LoginId" runat="server"></asp:Label></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Login") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle CssClass="Sortable" />
                                    <HeaderTemplate>
                                        <div class='SortIndicator <%# EvalSortBy("Email") %>'>
                                        </div>
                                        <asp:LinkButton ID="_linkHeaderEmail" CommandName="CustomSort" CommandArgument="Email"
                                            runat="server">
                                            <asp:Label ID="_nameHeadingLiteral3" meta:resourcekey="EmailAddress" runat="server"></asp:Label></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" CssClass="ResaltedBlue Email" runat="server" NavigateUrl='<%# "mailto:" + Eval("Email") %>'><%# Eval("Email") %></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle CssClass="Sortable" />
                                    <HeaderTemplate>
                                        <div class='SortIndicator <%# EvalSortBy("UserRole.Name") %>'>
                                        </div>
                                        <asp:LinkButton ID="_linkHeaderUserRole" CommandName="CustomSort" CommandArgument="UserRole.Name"
                                            runat="server">
                                            <asp:Label ID="_nameHeadingLiteral4" meta:resourcekey="UserRole" runat="server"></asp:Label></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# GetUserRoleName(Eval("UserRole"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <div class="SortIndicator">
                                        </div>
                                        <asp:HyperLink ID="HyperLink2" runat="server">
                                            <asp:Label ID="_nameHeadingLiteral5" meta:resourceKey="ProjectsInvolving" runat="server"></asp:Label>
                                        </asp:HyperLink>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%--<asp:LinkButton CssClass="SelectButton" ID="_lnkSelectItem" runat="server" CausesValidation="False"
                                            CommandName="CustomSelect" CommandArgument='<%# Eval("Id") %>' Text="Select"></asp:LinkButton>--%>
                                        <span class="ResaltedBlue">
                                            <%# GetProjectsInvolved(Eval("Login").ToString(), Eval("Account"))%></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="RowHeader" />
                            <AlternatingRowStyle CssClass="AlternatingRowItem clickable" />
                        </asp:GridView>
                        <cc1:CollectionPager StyleSheet="scripts/Controls/R3M.CollectionPager/Pager.css"
                            NumberOfItemsPerPage="5" ID="CollectionPager1" CssStyle="Control-Pager" runat="server">
                        </cc1:CollectionPager>
                        <asp:LinkButton ID="LinkButtonRefresh" CssClass="Invisible" OnClick="RefreshGrid" runat="server">Refresh</asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </div>
            <div id="AccountUsersNumbersContainer">
                <div class="TotalUsersSection">
                    <ul>
                        <li><span class="PremiumSpotsAvailable"><span class="strong">
                            <asp:Literal ID="Literal7" runat="server" meta:resourcekey="PremiumUserRolesAvailable"></asp:Literal>
                            :</span>&nbsp;<span><%= CurrentAccount !=null? SEOMembershipManager.GetPremiumUsersAvailable(CurrentAccount) : 0 %>
                            </span></span></li>
                        <li><span class="strong">
                            <asp:Literal ID="Literal4" runat="server" meta:resourcekey="TotalUsers"></asp:Literal>
                            : </span><span title='<%= GetLocalResourceObject("PremiumUsersTooltip") %>'>
                                <%= CurrentAccount != null? SEOMembershipManager.GetUsersCountOtherThanReadOnly(CurrentAccount): 0 %>
                                Premium </span>&nbsp;/&nbsp; <span title='<%= GetLocalResourceObject("ReadOnlyUsersTooltip") %>'>
                                    <%= CurrentAccount != null? SEOMembershipManager.GetReadOnlyUsers(CurrentAccount): 0 %>
                                    Courtesy </span></li>
                    </ul>
                </div>
            </div>
        </cc1:RoundPanel>
    </div>

    <script type="text/javascript">
        $.onDomReady(function() {
            $('th.Sortable').hover(function() { $(this).addClass('Over'); }, function() { $(this).removeClass('Over'); })
            .click(function(e) {            
                var jEle = $(this).find('a');
                if (jEle.length == 0 || !jEle.attr("href")) return;
                if (jEle.attr("href").indexOf('javascript:' > -1)) {
                    var fn = new Function(jEle.attr('href').replace('javascript:', ''));
                    fn.call(window);
                }                                                                
            });

            $('.xTable tr.RowItem,.xTable tr.AlternatingRowItem').click(function(e) {
                if ($(e.target).is('.Email')) return false;
				//remove the Selected class from previous row selected if any
				$('.xTable tr.Selected').removeClass('Selected');
				//add the class Selected to the current class
				$(this).addClass('Selected');
				
                var jEle = $(this);
                var user_id = jEle.attr('user_id');                
                if (user_id != null && user_id != '') {
                    $('#<%= IdUserSelected.ClientID %>').val(user_id);                    
                    <%= ClientScript.GetPostBackEventReference(LinkButtonRefreshUserPanel,String.Empty) %>
                }
            });
            
            //Fix tab sequence inside the roundPanelUsersDetail 
            $('#<%= _roundPanelUsersDetail.ClientID %> input, #<%= _roundPanelUsersDetail.ClientID %> select').fixTabIndex();
            
            
            
        });
		
		function refreshGrid() {	
		    __doPostBack('ctl00$contentArea$LinkButtonRefresh','');
		}
    </script>

    <asp:ObjectDataSource ID="odsRoles" runat="server" SelectMethod="GetUserRoles" TypeName="SEOToolSet.Providers.SEORolesManager">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource TypeName="SEOToolSet.Providers.SEOMembershipManager" ID="odsCountries"
        runat="server" SelectMethod="GetCountries"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="_odsSEOMemebershipUser" runat="server" TypeName="SEOToolSet.Providers.SEOMembershipManager"
        OnSelecting="OnSelecting" SelectMethod="GetUserById" DeleteMethod="DeleteUser"
        OnInserted="_odsSEOMembership_OnInserted" InsertMethod="CreateUser" UpdateMethod="UpdateUser">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="id" Type="Int32" />
            <asp:Parameter Name="firstName" Type="String" />
            <asp:Parameter Name="lastName" Type="String" />
            <asp:Parameter Name="email" Type="String" />
            <asp:Parameter Name="address1" Type="String" />
            <asp:Parameter Name="address2" Type="String" />
            <asp:Parameter Name="cityTown" Type="String" />
            <asp:Parameter Name="state" Type="String" />
            <asp:Parameter Name="zip" Type="String" />
            <asp:Parameter Name="telephone" Type="String" />
            <asp:Parameter Name="login" Type="String" />
            <asp:Parameter Name="enabled" Type="Boolean" />
            <asp:Parameter Name="account" Type="Object" />
            <asp:Parameter Name="country" Type="Object" />
            <asp:Parameter Name="userRole" Type="Object" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="IdUserSelected" Name="id" PropertyName="Value" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Direction="Output" Name="id" Type="Int32" />
            <asp:Parameter Name="firstName" Type="String" />
            <asp:Parameter Name="lastName" Type="String" />
            <asp:Parameter Name="email" Type="String" />
            <asp:Parameter Name="address1" Type="String" />
            <asp:Parameter Name="address2" Type="String" />
            <asp:Parameter Name="cityTown" Type="String" />
            <asp:Parameter Name="state" Type="String" />
            <asp:Parameter Name="zip" Type="String" />
            <asp:Parameter Name="telephone" Type="String" />
            <asp:Parameter Name="login" Type="String" />
            <asp:Parameter Name="account" Type="Object" />
            <asp:Parameter Name="country" Type="Object" />
            <asp:Parameter Name="userRole" Type="Object" />
            <asp:Parameter Direction="Output" Name="status" Type="Object" />
            <asp:Parameter Direction="Output" Name="password" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <%--  GetUsersFromAccountSortBy --%>
    <asp:ObjectDataSource ID="_odsSeoMembershipUsers" OnSelected="UsersSelected" runat="server"
        SelectMethod="GetUsersFromAccountWithSortAndPaging" TypeName="SEOToolSet.Providers.SEOMembershipManager"
        OnSelecting="odsSeoMembershipUsers_Selecting">
        <SelectParameters>
            <asp:Parameter Name="account" Type="Object" />
            <asp:Parameter DefaultValue="false" Name="includeInactive" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="asc" Type="Boolean" />
            <asp:Parameter DefaultValue="FirstName" Name="fieldName" Type="String" />
            <asp:Parameter DefaultValue="" Direction="Output" Name="count" Type="Int32" />
            <asp:ControlParameter ControlID="CollectionPager1" Name="pageSize" PropertyName="NumberOfItemsPerPage"
                Type="Int32" />
            <asp:ControlParameter ControlID="CollectionPager1" Name="currentPageNumber" PropertyName="CurrentPageIndex"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
