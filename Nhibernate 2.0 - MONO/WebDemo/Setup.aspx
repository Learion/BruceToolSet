<%@ Page Language="C#" AutoEventWireup="true" Inherits="Setup" Codebehind="Setup.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Eucalypto database setup</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Setup</h1>
            <p>SECURITY NOTE: This page is accesible only from the server computer, but it is recommended to remove this page on a production enviroment.</p>

            <strong><span id="lblStatus" runat="server"></span></strong>
            
            <h2>Database configuration</h2>
            <p>Use this setup to create the required database schema.
            Select the connection string:</p>
            <asp:DropDownList ID="cmbConnections" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbConnections_SelectedIndexChanged"></asp:DropDownList>
            
            <p>Schema sections:</p>
            <asp:CheckBoxList ID="list" runat="server">
            </asp:CheckBoxList>

            <p>Use the 'Check status' button to check the status of the database tables. Use the 'Create schema' button to regenerate the selected tables.</p>

            <p>
                <asp:Button ID="btCheckStatus" runat="server" Text="Check status" OnClick="btCheckStatus_Click" />
                <asp:Button ID="btCreate" runat="server" Text="Create schema" OnClick="btCreate_Click" 
                   OnClientClick="return confirm('Are you sure to generate the schema? WARNING: If a table already exists its content will be deleted.');" />
            </p>               
            
            <h2>Administrator user</h2>
            <p>Click 'Create admin user' to create the administrator user with the following configuration:</p>
            <table>
                <tr>
                    <td>User: </td>
                    <td><asp:TextBox ID="txtAdminUser" runat="server" Text="admin"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Password:</td>
                    <td><asp:TextBox ID="txtAdminPassword" runat="server" TextMode="Password"></asp:TextBox> (required)</td>
                </tr>
                <tr>
                    <td>EMail:</td>
                    <td><asp:TextBox ID="txtAdminEMail" runat="server"></asp:TextBox> (required if you have configured requiresUniqueEmail=true)</td>
                </tr>
                <tr>
                    <td>Role:</td>
                    <td><asp:TextBox ID="txtAdminRole" runat="server" Text="administrators"></asp:TextBox></td>
                </tr>                
            </table>
            <p>
                <asp:Button ID="btCreateAdmin" runat="server" Text="Create admin user" OnClick="btCreateAdmin_Click" />
            </p>       
            
            <h2>Smoke test</h2>
            <p>You can use any of these tests to see if the application works correctly (configuration, database, ...).</p>
            <p>IMPORTANT: Usually these tests must be executed only on a clean database. If a test fail any data used during the test is not removed. I suggest to recreate the database structure at the end of the tests.</p>
            <asp:CheckBoxList ID="listTest" runat="server">
            </asp:CheckBoxList>
            
            <p>
                <asp:Button ID="btRunTest" runat="server" Text="Smoke test!" OnClick="btRunTest_Click" />
            </p>
            <asp:BulletedList ID="testResult" runat="server">
            </asp:BulletedList>
            
            
            
            <h2>Performance test</h2>
            <p>You can use any of these tests to see the performance of the application.</p>
            <p>IMPORTANT: These tests must be executed only on a test database. Entities used during the test are not removed. I suggest to recreate the database structure at the end of the tests.</p> 
            <p>IMPORTANT: Each of these tests can require several minutes to complete. Please be patient.</p> 
            <p>
                Performance results: <span id="lblPerfResult" runat="server"></span>
            </p>
            <table>
                <thead>
                    <tr>
                        <th>Description</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Forum: Insert 20 topics each with 20 messages.</td>
                        <td>
                            <asp:Button ID="btForumPerfTest" runat="server" Text="Execute" OnClick="btForumPerfTest_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
                       
        </div>
    </form>
</body>
</html>
