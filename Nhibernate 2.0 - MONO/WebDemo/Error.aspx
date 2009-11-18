<%@ Page Language="C#" AutoEventWireup="true" Inherits="Error" Codebehind="Error.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Error</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p class="errorBox">
                <span runat="server" id="lbError"></span>
            </p>
            <p>
                The server encountered an error. Click <a href="javascript:history.go(-1)">Back</a> to try the operation again.<br />
                If the problem persist contact system administrator or try again later.
            </p>
        </div>
    </form>
</body>
</html>
