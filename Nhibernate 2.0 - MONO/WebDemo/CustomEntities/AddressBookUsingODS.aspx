<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="True" Inherits="WebDemo.CustomEntities.AddressBookUsingODS"
    Title="Address Book Using ODS" CodeBehind="AddressBookUsingODS.aspx.cs" %>

<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">
    <asp:FormView ID="FormView1" CssClass="FormView2" DefaultMode="Insert" runat="server"
        DataSourceID="odsContactCreate" OnItemUpdating="FormView1_ItemUpdating" OnItemUpdated="FormView1_ItemUpdated"
        OnItemInserting="FormView1_ItemInserting" DataKeyNames="Id" OnItemInserted="FormView1_ItemInserted"
        OnDataBinding="FormView1_DataBinding">
        <InsertItemTemplate>
            <label>
                <span>DisplayName:</span>
                <asp:TextBox ID="DisplayNameTextBox" runat="server" Text='<%# Bind("DisplayName") %>' />
            </label>
            <label>
                <span>FirstName:</span>
                <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>' />
            </label>
            <label>
                <span>LastName:</span>
                <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>' />
            </label>
            <label>
                <span>Telephone1:</span>
                <asp:TextBox ID="Telephone1TextBox" runat="server" Text='<%# Eval("Telephone1") %>' />
            </label>
            <label>
                <span>Note:</span>
                <asp:TextBox ID="NoteTextBox" runat="server" Text='<%# Bind("Note") %>' />
            </label>
            <label>
                <span>Address: </span>
                <asp:TextBox ID="AddressTextBox" runat="server" Text='<%# Bind("Address") %>' />
            </label>
        </InsertItemTemplate>
    </asp:FormView>
    <div class="Toolbar">
        <ul>
            <li>
                <asp:LinkButton CssClass="LinkButton Save" ID="InsertButton" runat="server" CausesValidation="True"
                    OnClick="InsertButton_Click"><span>Insert</span></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton CssClass="LinkButton Cancel" ID="InsertCancelButton" runat="server"
                    CausesValidation="False" CommandName="Cancel"><span>Cancel</span></asp:LinkButton>
            </li>
        </ul>
    </div>
    <div class="ClearIt">
    </div>
    <cc1:ObjectDataSource ID="odsContactCreate" runat="server" DeleteMethod="DeleteContact"
        InsertMethod="CreateContact" SelectMethod="GetById" TypeName="WebSite.CustomEntities.AddressBook.ContactODS"
        UpdateMethod="UpdateContact" OnInserted="odsContactCreate_Inserted" OnInserting="odsContactCreate_Inserting">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="Id" Type="String" />
            <asp:Parameter Name="displayName" Type="String" />
            <asp:Parameter Name="address" Type="String" />
            <asp:Parameter Name="firstName" Type="String" />
            <asp:Parameter Name="lastName" Type="String" />
            <asp:Parameter Name="telephone" Type="String" />
            <asp:Parameter Name="note" Type="String" />
        </UpdateParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="825aa23051a54ba0be14d018b8e908f5" Name="Id"
                QueryStringField="IdContact" Type="String" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="displayName" Type="String" />
            <asp:Parameter Name="address" Type="String" />
            <asp:Parameter Name="firstName" Type="String" />
            <asp:Parameter Name="lastName" Type="String" />
            <asp:Parameter Name="telephone" Type="String" />
            <asp:Parameter Name="note" Type="String" />
            <asp:Parameter Direction="Output" Name="Id" Type="String" />
        </InsertParameters>
    </cc1:ObjectDataSource>
    <asp:GridView CssClass="datatable" ID="GridView2" runat="server" DataSourceID="odsContacts2"
        AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="Id" OnRowUpdating="GridView2_RowUpdating">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
            <asp:BoundField DataField="DisplayName" HeaderText="DisplayName" SortExpression="DisplayName" />
            <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
            <asp:TemplateField HeaderText="Telephone1" SortExpression="Telephone1">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("Telephone1") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Telephone1") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Note" HeaderText="Note" SortExpression="Note" />
            <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
    </asp:GridView>
    <cc1:ObjectDataSource ID="odsContacts2" TypeName="WebSite.CustomEntities.AddressBook.ContactODS"
        runat="server" OldValuesParameterFormatString="{0}" SelectMethod="GetAll" DeleteMethod="DeleteContact"
        InsertMethod="CreateContact" UpdateMethod="UpdateContact">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="Id" Type="String" />
            <asp:Parameter Name="displayName" Type="String" />
            <asp:Parameter Name="address" Type="String" />
            <asp:Parameter Name="firstName" Type="String" />
            <asp:Parameter Name="lastName" Type="String" />
            <asp:Parameter Name="telephone" Type="String" />
            <asp:Parameter Name="note" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="displayName" Type="String" />
            <asp:Parameter Name="address" Type="String" />
            <asp:Parameter Name="firstName" Type="String" />
            <asp:Parameter Name="lastName" Type="String" />
            <asp:Parameter Name="telephone" Type="String" />
            <asp:Parameter Name="note" Type="String" />
            <asp:Parameter Direction="Output" Name="Id" Type="String" />
        </InsertParameters>
    </cc1:ObjectDataSource>
</asp:Content>
