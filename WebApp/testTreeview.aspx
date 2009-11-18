<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testTreeview.aspx.cs" Inherits="SEOToolSet.WebApp.testTreeview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">

　　body{

　　font: 10pt 宋体,sans-serif; color: navy; }

　　.branch{

　　cursor: pointer;

　　cursor: hand;

　　display: block; }

　　.leaf{

　　display: none;

　　margin-left: 16px; }

　　a{ text-decoration: none; }

　　a:hover{ text-decoration: underline; }

　　</style>
<script language="JavaScript" type="text/javascript">

　　var openImg = new ..Asset not found..;

　　openImg.src = "open.gif";

　　var closedImg = new ..Asset not found..;

　　closedImg.src = "closed.gif";

　　function showBranch(branch){

　　var objBranch = document.getElementById(branch).style;

　　if (objBranch.display=="block")

　　objBranch.display="none";

　　else

　　objBranch.display="block";

　　swapFolder('I' + branch);

　　}

　　function swapFolder(img){

　　objImg = document.getElementById(img);

　　if (objImg.src.indexOf('closed.gif')>-1)

　　objImg.src = openImg.src;

　　else

　　objImg.src = closedImg.src;

　　}
　　
　　------
　　function tree(){

　　this.branches = new Array();

　　this.add = addBranch;

　　this.write = writeTree;

　　}

-------
function addBranch(branch){

　　this.branches[this.branches.length] = branch;

　　}

　　function writeTree(){

　　var treeString = '';

　　var numBranches = this.branches.length;

　　for (var i=0;i treeString += this.branches[i].write();

　　document.write(treeString);

　　}


　　------

　　</script>



</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TreeView ID="TreeView1" runat="server">
            <Nodes>
                <asp:TreeNode Text="新建节点1" Value="新建节点1">
                    <asp:TreeNode Text="新建节点11" Value="新建节点11"></asp:TreeNode>
                    <asp:TreeNode Text="新建节点22" Value="新建节点22"></asp:TreeNode>
                    <asp:TreeNode Text="新建节点33" Value="新建节点33"></asp:TreeNode>
                    <asp:TreeNode Text="新建节点44" Value="新建节点44"></asp:TreeNode>
                </asp:TreeNode>
            </Nodes>
        </asp:TreeView>
    </div>
    </form>
</body>
</html>
