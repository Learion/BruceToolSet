<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageUsersControl.ascx.cs"
    Inherits="SEOToolSet.WebApp.ManageUsersControl" %>
<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<div id="panelUsers" class="Simple" runat="server">
    <cc1:CustomRepeater DataSourceID="odsProjectUsers" ID="CustomRepeaterProjectUsers"
        OnItemCommand="CustomRepeaterProjectUsers_OnItemCommand" runat="server" OnItemDataBound="CustomRepeaterProjectUsers_ItemDataBound">
        <HeaderTemplate>
            <table>
                <thead>
                    <tr class="RowHeader">
                        <th style="width: 380px">
                            <asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:CommonTerms, Name %>"></asp:Localize>
                        </th>
                        <th>
                            <asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:CommonTerms, Email %>"></asp:Localize>
                        </th>
                        <th>
                            <asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:CommonTerms, ProjectRole %>"></asp:Localize>
                        </th>
                        <th class="AdvancedCol">
                            <asp:Localize ID="Localize4" runat="server" meta:resourcekey="MonitorEmails"></asp:Localize>
                        </th>
                        <th class="AdvancedCol">
                            <asp:Localize ID="Localize5" runat="server" meta:resourcekey="AccountUser"></asp:Localize>
                        </th>
                        <th>
                            <asp:Localize ID="Localize6" runat="server" meta:resourcekey="Action"></asp:Localize>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="AlternatingRowItem">
                        <td class="TdLeft">
                            <ul>
                                <li>
                                    <asp:LinkButton OnClick="lnkAddProjectUser_OnClick" ID="LnkAddProjectUser" CssClass="LinkLabel Add"
                                        runat="server">
                                        <asp:Label ID="Label1" runat="server" meta:resourcekey="AddProjectUser"></asp:Label>
                                    </asp:LinkButton>
                                    <asp:Panel ID="panel1" Visible="false" runat="server">
                                        <asp:DropDownList Width="250px" AppendDataBoundItems="True" ID="DropDownListUsersAvailable"
                                            DataSourceID="odsUsersNotInProject" runat="server" DataTextField="Login" DataValueField="Id">
                                            <asp:ListItem Value="-1" Text='<%$ Resources:CommonTerms, Choose %>'></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="LnkAdd" CssClass="MiniCommand" OnClick="lnkAdd_OnClick" runat="server">                                            
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LnkCancelAdd" CssClass="MiniCommand" OnClick="lnkCancelAdd_OnClick"
                                            CausesValidation="False" runat="server">
                                            <span>[
                                                <asp:Literal ID="Localize8" runat="server" Text="<%$ Resources:CommonTerms, Cancel %>"></asp:Literal>
                                                ]</span>
                                        </asp:LinkButton>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="DropDownListUsersAvailable"
                                            InitialValue="-1" runat="server" Text="<%$ Resources:CommonTerms, ChooseAnItem %>"
                                            ErrorMessage="<%$ Resources:CommonTerms, ChooseAnItem %>"></asp:RequiredFieldValidator>
                                    </asp:Panel>
                                </li>
                            </ul>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td class="AdvancedCol">
                        </td>
                        <td class="AdvancedCol">
                        </td>
                        <td>
                        </td>
                    </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="RowItem">
                <td class="TdLeft">
                    <a class="LinkLabel" href='<%# Eval("SEOToolsetUser.Id","UserProfile.aspx?IdUser={0}") %>'>
                        <span>
                            <%# Eval("SEOToolsetUser.Login")%></span> </a>
                </td>
                <td>
                    <span>
                        <%# Eval("SEOToolsetUser.Email")%></span>
                </td>
                <td>
                    <span>
                        <%# Eval("ProjectRole.Name")%></span>
                </td>
                <td class="AdvancedCol">
                    <asp:CheckBox ID="chkMonitorEmails" runat="server" Checked='<%# Eval("MonitorEmails") ?? false %>' />
                </td>
                <td class="AdvancedCol">
                    <span>
                        <%#  ((Int32)Eval("Project.Account.Id")) == ((Int32) Eval("SEOToolsetUser.Account.Id")) ? "Yes" : "No" %></span>
                </td>
                <td style="width: 160px">
                    <div class="CenterWrapper NoForce" style="width: 80px">
                        <cc1:LinkButtonRound ID="LinkButtonRemove" meta:resourcekey="DeleteUsersFromProjectsCommand" CssClass="button CommandDelete"
                            CommandName="DoDelete" CommandArgument='<%# Eval("Id") %>' item_name='<%# Eval("SEOToolsetUser.Login") %>'
                            runat="server"></cc1:LinkButtonRound>
                        <%--<asp:LinkButton meta:resourcekey="DeleteUsersFromProjectsCommand" ID="LinkButtonRemove"
                            confirm_message='Delete Projects?' item_name='<%# Eval("SEOToolsetUser.Login") %>'
                            CssClass="LinkCommandRound Little CommandDelete" CommandName="DoDelete" CommandArgument='<%# Eval("Id") %>'
                            runat="server">                            
                                <span>
                                <%# GetDeleteButtonText() %>
                            </span>
                            
                        </asp:LinkButton>--%>
                    </div>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="AlternatingRowItem">
                <td class="TdLeft">
                    <a class="LinkLabel" href='<%# Eval("SEOToolsetUser.Id","UserProfile.aspx?IdUser={0}") %>'>
                        <span>
                            <%# Eval("SEOToolsetUser.Login")%></span> </a>
                </td>
                <td>
                    <span>
                        <%# Eval("SEOToolsetUser.Email")%></span>
                </td>
                <td>
                    <span>
                        <%# Eval("ProjectRole.Name")%></span>
                </td>
                <td class="AdvancedCol">
                    <asp:CheckBox ID="chkMonitorEmails" runat="server" Checked='<%# Eval("MonitorEmails") ?? false %>' />
                </td>
                <td class="AdvancedCol">
                    <span>
                        <%#  ((Int32)Eval("Project.Account.Id")) == ((Int32) Eval("SEOToolsetUser.Account.Id")) ? "Yes" : "No" %></span>
                </td>
                <td style="width: 160px">
                    <div class="CenterWrapper NoForce" style="width: 80px">
                        <cc1:LinkButtonRound ID="LinkButtonRemove" meta:resourcekey="DeleteUsersFromProjectsCommand" CssClass="button CommandDelete"
                            CommandName="DoDelete" CommandArgument='<%# Eval("Id") %>' item_name='<%# Eval("SEOToolsetUser.Login") %>'
                            runat="server"></cc1:LinkButtonRound>
                        <%-- <asp:LinkButton meta:resourcekey="DeleteUsersFromProjectsCommand" ID="LinkButtonRemove"
                            item_name='<%# String.Format("\"{0}\"",Eval("SEOToolsetUser.Login")) %>' CssClass="LinkCommandRound Little CommandDelete"
                            CommandName="DoDelete" CommandArgument='<%# Eval("Id") %>' runat="server">
                            <span>
                                <%# GetDeleteButtonText() %>
                            </span>
                        </asp:LinkButton>--%>
                    </div>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EmptyDataTemplate>
            <tr class="RowItem">
                <td>
                    <span>--</span>
                </td>
                <td>
                    <span>--</span>
                </td>
                <td>
                    <span>--</span>
                </td>
                <td class="AdvancedCol">
                    <span>--</span>
                </td>
                <td class="AdvancedCol">
                    <span>--</span>
                </td>
                <td>
                    <span>--</span>
                </td>
            </tr>
        </EmptyDataTemplate>
        <FooterTemplate>
            </tbody> </table>
        </FooterTemplate>
    </cc1:CustomRepeater>
</div>
<asp:ObjectDataSource ID="odsUsersNotInProject" runat="server" OnSelecting="OnSelecting"
    SelectMethod="GetUsersNotInProject" TypeName="SEOToolSet.Providers.ProjectManager">
    <SelectParameters>
        <asp:ControlParameter ControlID="__Page" Name="idProject" PropertyName="IdProject"
            Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsProjectUsers" TypeName="SEOToolSet.Providers.ProjectManager"
    SelectMethod="GetUsersInProject" runat="server">
    <SelectParameters>
        <asp:ControlParameter ControlID="__Page" Name="idProject" PropertyName="IdProject"
            Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<%--Note: Some values are setted using the code behind file--%>
<cc1:ConfirmMessageExt ID="ConfirmMessageExt1" useItemNameInElement="true" runat="server"
    CancelButtonText="Cancel" ConfirmMessage="Remove Users from the Project?" ConfirmTitle="Confirmation Required"
    useConfirmMessageFromElement="true" OkButtonText="Ok" Selector='a.CommandDelete' />
