<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XiaoLangTest.aspx.cs" Inherits="SEOToolSet.WebApp.XiaoLangTest"
    MasterPageFile="~/PageBase.Master" Title="Test By XL" %>

<%@ Import Namespace="SEOToolSet.Providers" %>
<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register Src="PageTitle.ascx" TagName="PageTitle" TagPrefix="uc1" %>
<%@ Register Src="~/ArrayRenderer.ascx" TagName="ArrayRenderer" TagPrefix="uc2" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">
    <uc3:IncludeFile ID="IncludeFile11" runat="server" TypeOfFile="Css" FilePath="~/css/ManageAccountUsers.css" />
    <cc1:ConfirmMessageExt ID="ConfirmMessageExt1" runat="server" 
        CancelButtonText="cancel" ConfirmMessage="aaaaaaa" ConfirmTitle="bb" 
        OkButtonText="ok" Selector="a.CommandDelete" useConfirmMessageFromElement="True" />
    <uc1:PageTitle PanelContainerVisible="true" RenderRoundPanelStyles="false" PageDescription="The settings for an individual project can be managed below"
        ID="PageTitle1" runat="server" PageTitleText="<%$ Resources:CommonTerms, ManageProject %>" />
    <cc1:RoundPanel ID="rpProjectManage" runat="server">
        <div style="float: left; margin:0;">
            <h2>
                Project Information
            </h2>
        </div>
        <div id="divProject" style="float: right; color: #06F" runat="server" visible="false">
            <a href="ManageProject.aspx?mode=add">Add a new project</a> |
            <asp:LinkButton ID="lbtnDelete" runat="server" OnClick="lbtnDelete_Click" CssClass="CommandDelete">Delete 
            this project</asp:LinkButton>
        </div>
        <asp:FormView ID="fvProject" runat="server" DefaultMode="Insert" Width="100%" DataSourceID="ObjectDataSource1"
            OnItemUpdating="FormView1_ItemUpdating" DataKeyNames="Id">
            <EditItemTemplate>
                <div style="padding-top: 10px;">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td width="30%" height="27" align="right" style="color: #1b6ecc;">
                                Project Name<sup>*</sup>
                            </td>
                            <td width="25%" style="font-weight: bold; padding-left: 5px;">
                                <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' CssClass="ProjectManageTextBox" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NameTextBox"
                                    ErrorMessage="*" ValidationGroup="AddProject"></asp:RequiredFieldValidator>
                            </td>
                            <td width="10%" align="right" style="color: #1b6ecc;">
                                Contact Name<sup>*</sup>
                            </td>
                            <td width="35%" style="padding-left: 5px;">
                                <asp:TextBox ID="ContactNameTextBox" runat="server" Text='<%# Bind("ContactName") %>'
                                    CssClass="ProjectManageTextBox" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ContactNameTextBox"
                                    ErrorMessage="*" ValidationGroup="AddProject"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td height="27" align="right" style="color: #1b6ecc;">
                                Company Name<sup>*</sup>
                            </td>
                            <td style="padding-left: 5px;">
                                <asp:TextBox ID="ClientNameTextBox" runat="server" Text='<%# Bind("ClientName") %>'
                                    CssClass="ProjectManageTextBox" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ClientNameTextBox"
                                    ErrorMessage="*" ValidationGroup="AddProject"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" style="color: #1b6ecc;">
                                Contact E-mail<sup>*</sup>
                            </td>
                            <td style="padding-left: 5px;">
                                <asp:TextBox ID="ContactEmailTextBox" runat="server" Text='<%# Bind("ContactEmail") %>'
                                    CssClass="ProjectManageTextBox" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ContactEmailTextBox"
                                    ErrorMessage="*" ValidationGroup="AddProject">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="ContactEmailTextBox"
                                        ErrorMessage="not available" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="AddProject"></asp:RegularExpressionValidator></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td height="27" align="right" style="color: #1b6ecc;">
                                Domain<sup>*</sup>
                            </td>
                            <td style="font-weight: bold; padding-left: 5px;">
                                <asp:TextBox ID="DomainTextBox" runat="server" Text='<%# Bind("Domain") %>' CssClass="ProjectManageTextBox" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DomainTextBox"
                                    ErrorMessage="*" ValidationGroup="AddProject"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" style="color: #1b6ecc;">
                                Contact Phone
                            </td>
                            <td style="padding-left: 5px;">
                                <asp:TextBox ID="ContactPhoneTextBox" runat="server" Text='<%# Bind("ContactPhone") %>'
                                    BackColor="White" CssClass="ProjectManageTextBox" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="ContactPhoneTextBox"
                                    ErrorMessage="not available" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"
                                    ValidationGroup="AddProject"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="color: #1b6ecc; padding-left: 5px;">
                                &nbsp;
                            </td>
                            <td align="center" style="padding-left: 5px;">
                                <asp:ImageButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                    text="插入" ImageUrl="images/btn_save.png" Width="51" Height="17" ValidationGroup="UpdateProject" />
                                &nbsp;
                            </td>
                            <td align="center" style="color: #1b6ecc;">
                                <input name="imageField2" type="image" src="images/btn_cancel.png" width="60" height="17"
                                    onclick="this.form.reset();return false;">
                            </td>
                            <td style="padding-left: 5px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </EditItemTemplate>
            <InsertItemTemplate>
                <div style="padding-top: 10px;">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td width="30%" height="27" align="right" style="color: #1b6ecc;">
                                Project Name<sup>*</sup>
                            </td>
                            <td width="25%" style="font-weight: bold; padding-left: 5px;">
                                <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' CssClass="ProjectManageTextBox" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NameTextBox"
                                    ErrorMessage="*" ValidationGroup="AddProject"></asp:RequiredFieldValidator>
                            </td>
                            <td width="10%" align="right" style="color: #1b6ecc;">
                                Contact Name<sup>*</sup>
                            </td>
                            <td width="35%" style="padding-left: 5px;">
                                <asp:TextBox ID="ContactNameTextBox" runat="server" Text='<%# Bind("ContactName") %>'
                                    CssClass="ProjectManageTextBox" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ContactNameTextBox"
                                    ErrorMessage="*" ValidationGroup="AddProject"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td height="27" align="right" style="color: #1b6ecc;">
                                Company Name<sup>*</sup>
                            </td>
                            <td style="padding-left: 5px;">
                                <asp:TextBox ID="ClientNameTextBox" runat="server" Text='<%# Bind("ClientName") %>'
                                    CssClass="ProjectManageTextBox" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ClientNameTextBox"
                                    ErrorMessage="*" ValidationGroup="AddProject"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" style="color: #1b6ecc;">
                                Contact E-mail<sup>*</sup>
                            </td>
                            <td style="padding-left: 5px;">
                                <asp:TextBox ID="ContactEmailTextBox" runat="server" Text='<%# Bind("ContactEmail") %>'
                                    CssClass="ProjectManageTextBox" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ContactEmailTextBox"
                                    ErrorMessage="*" ValidationGroup="AddProject"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="ContactEmailTextBox"
                                    ErrorMessage="not available" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ValidationGroup="AddProject"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td height="27" align="right" style="color: #1b6ecc;">
                                Domain<sup>*</sup>
                            </td>
                            <td style="font-weight: bold; padding-left: 5px;">
                                <asp:TextBox ID="DomainTextBox" runat="server" Text='<%# Bind("Domain") %>' CssClass="ProjectManageTextBox" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DomainTextBox"
                                    ErrorMessage="*" ValidationGroup="AddProject"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" style="color: #1b6ecc;">
                                Contact Phone
                            </td>
                            <td style="padding-left: 5px;">
                                <asp:TextBox ID="ContactPhoneTextBox" runat="server" Text='<%# Bind("ContactPhone") %>'
                                    BackColor="White" CssClass="ProjectManageTextBox" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="ContactPhoneTextBox"
                                    ErrorMessage="not available" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"
                                    ValidationGroup="AddProject"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="color: #1b6ecc; padding-left: 5px;">
                                &nbsp;
                            </td>
                            <td align="center" style="padding-left: 5px;">
                                <asp:ImageButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    text="插入" ImageUrl="images/btn_save.png" Width="51" Height="17" ValidationGroup="AddProject" />
                                &nbsp;
                            </td>
                            <td align="center" style="color: #1b6ecc;">
                                <input name="imageField2" type="image" src="images/btn_cancel.png" width="60" height="17"
                                    onclick="this.form.reset();return false;">
                            </td>
                            <td style="padding-left: 5px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </InsertItemTemplate>
            <ItemTemplate>
                Id:
                <asp:Label ID="IdLabel" runat="server" Text='<%# Bind("Id") %>' />
            </ItemTemplate>
        </asp:FormView>
    </cc1:RoundPanel>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetProjectById"
        TypeName="SEOToolSet.Providers.ProjectManager" UpdateMethod="UpdateProject">
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
    </asp:ObjectDataSource>
</asp:Content>
