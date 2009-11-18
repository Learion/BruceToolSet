<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="SEOToolSet.WebApp.HomePage" 
    MaintainScrollPositionOnPostback="false" MasterPageFile="~/PageBase.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">
Project Dashboard
view projects below and click the linked items to access reports and manage project data
help & Documentation
Show:<asp:DropDownList ID="DropDownList1" onchange="up()"
        runat="server">
        <asp:ListItem>kkkkk</asp:ListItem>
        <asp:ListItem>2222</asp:ListItem>
        
        <asp:ListItem>kkk3333kk</asp:ListItem>
    </asp:DropDownList>
    <select id="Select1" style="text-decoration: underline">
        <option  style="text-decoration: underline">ccccccc</option>
        <option><hr /></option>
        <option>ffffffff</option>
    </select>
    
    
<SELECT style="
     FONT-SIZE: 9pt; FONT-FAMILY: Verdana,Arial">
<OPTION>11111</OPTION><OPTION>22222</OPTION><OPTION>33333</OPTION>
<OPTGROUP  LABEL="-----------------" style="text-decoration: underline; color: #008000"></OPTGROUP>
<OPTION>11111</OPTION><OPTION>22222</OPTION><OPTION>33333</OPTION>

</SELECT>
    
    
    <script type="text/javascript">
    function up()
    {
        var drp2 = document.getElementById("ctl00_contentArea_DropDownList1");
        var drptext=drp2.value;
        for(var i=0;i<=drp2.options.length-1;i++)
        {
            //alert(drp2.options[i].text);
            if(drptext==drp2.options[i].text)
            {
             var   nies   =   document.createElement("OPTGROUP")   ;
             nies.label   =   "-----------------";
             nies.setAttribute("style","text-decoration: underline; color: #008000");
             drp2.options.add(nies)  
                drp2.options[i].setAttribute("style","text-decoration: underline");
                drp2.insertBefore(nies,drp2.childNodes[2]);
            }
        }
    }
    </script>
</asp:Content>
