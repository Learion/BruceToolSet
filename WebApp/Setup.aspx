<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Setup.aspx.cs" Inherits="SEOToolSet.WebApp.Setup" %>

<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<%@ Register Src="Controls/Popup.ascx" TagName="Popup" TagPrefix="uc2" %>
<%@ Register Src="Controls/sIFR.ascx" TagName="sIFR" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SEOToolSet - Setup</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
</head>
<body>
    <form id="aspnetForm" class="MainForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc1:IncludeFile ID="IncludeFile1" FilePath="~/css/reset-fonts.css" TypeOfFile="CSS"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile4" FilePath="~/css/Setup.css" TypeOfFile="CSS" runat="server" />
    <uc1:IncludeFile ID="IncludeFile3" TypeOfFile="Javascript" FilePath="~/scripts/jquery.js"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile7" TypeOfFile="Javascript" FilePath="~/Handler/RetrieveResource.ashx"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile2" TypeOfFile="Javascript" FilePath="~/scripts/common.js"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile6" TypeOfFile="CSS" FilePath="~/scripts/Controls/R3M.RoundPanel/round_ctr.css"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile5" TypeOfFile="Javascript" FilePath="~/scripts/Controls/R3M.RoundPanel/RoundPanel.js"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile56" FilePath="~/scripts/Controls/R3M.Combobox/cbx.css"
        TypeOfFile="Css" runat="server" />
    <uc1:IncludeFile ID="IncludeFile8" FilePath="~/scripts/Controls/R3M.RoundButton/RoundButton.css"
        TypeOfFile="Css" runat="server" />
    <uc1:IncludeFile ID="IncludeFile57" FilePath="~/scripts/plugins/jquery.scrollTo.js"
        TypeOfFile="Javascript" runat="server" />
    <uc1:IncludeFile ID="IncludeFile58" FilePath="~/scripts/Controls/R3M.Combobox/combobox.js"
        TypeOfFile="Javascript" runat="server" />

    <script type="text/javascript">
        $(function() {
            new $.R3M.Combobox('#<%= _dropDownListConnections.ClientID %>');
        });
        $.onDomReady(function() {
            $('.Toolbar').fitToChildrenWidth();
            $('.Toolbar ul li a').click(function() {
                $('.Toolbar ul li a').each(function(i, val) {
                    $(this).disable(true);
                    $('.FormCSS').blockContent();
                });
            });

            $('.FormCSS').unblockContent();
        });
    </script>

    <uc3:sIFR ID="sIFR1" runat="server" Selector=".hd h1" SwfFontToRender="~/scripts/Controls/sIFR/fonts/font_ps.swf" />
    <div class="mc_outer">
        <ul class="DividerStructure">
            <li class="Column InstallGraphPane">
                <cc1:RoundPanel CssClass="PannelWrapper" ID="RoundPanel5" runat="server">
                    <div class="ImageSetup">
                    </div>
                </cc1:RoundPanel>
            </li>
            <li class="Column MainColumn">
                <div class="bd">
                    <cc1:RoundPanel CssClass="PannelWrapper" ID="RoundPanel1" runat="server">
                        <div class="hd">
                            <h1>
                                SEOToolSet - Setup
                            </h1>
                        </div>
                        <div class="DoClear">
                            &nbsp;</div>
                        <div class="Legend">
                            <h2>
                                Creates the Schema and Load Initial Data Process
                            </h2>
                        </div>
                        <div class="SectionContent">
                            <p>
                                Welcome to the SEOToolSet Setup page. This page has been created to help you in
                                the process of install a new fresh instance of the SEOToolSet Application.
                            </p>
                            <div class="Note-Important">
                                <h2>
                                    Please Remember</h2>
                                <p>
                                    As any other installation process, if you have already a version of the SEOToolSet
                                    application running, <span class="resalted">please make a backup of the files</span>
                                    of the application and a <span class="resalted">backupd of the Database too.</span>
                                </p>
                            </div>
                            <p>
                                First you need to:
                            </p>
                            <ul>
                                <li>Create a db schema with any name you desire (seodb is highly recommended) in your
                                    mysql db instance. Make sure you have set the <span class="resalted">default charset
                                        to UTF-8 and the collation to utf8_general_ci.</span> </li>
                                <li>Create a uses (SEOUser i.e.) and grant permissions to the new created database.
                                </li>
                                <li>Open your web.config and search for the <span class="resalted">connectionstrings</span>
                                    section choose one connection string and changes the information accordingly to
                                    your connection information.</li>
                                <li>Select the options below</li>
                            </ul>
                            <div class="FormCSS">
                                <div class="Field First">
                                    <span class="label">Please Select the Connection String to Use: </span>
                                    <asp:DropDownList Width="150px" ID="_dropDownListConnections" runat="server">
                                    </asp:DropDownList>
                                    <div class="Hint">
                                        If no connection strings are found please check your web.config file</div>
                                </div>
                                <div class="Field">
                                    <span class="label">Export the Generated DDL Instructions </span><span></span>
                                    <asp:CheckBox ID="_checkboxExportSql" runat="server" />
                                    <div class="Hint">
                                        Check this option if you want to export the Sql Create/Update Instructions. (It's
                                        useful for debugging purposes). The Generated file will be found inside App_Data
                                        with a name like this:<span class="resalted">OutputSQLCreateInstructions_Scripts633777734558750000.sql</span>
                                    </div>
                                </div>
                                <div class="DoClear">
                                </div>
                            </div>
                            <p>
                                Please click on the "Create Schema and Load Initial Data" Button below<uc2:Popup
                                    ID="Popup1" runat="server" />
                            </p>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="Toolbar">
                                        <ul>
                                            <li>
                                                <asp:LinkButton ID="LinkLoadInitialData" class="LinkButton Install" OnClick="LinkLoadInitialData_Click"
                                                    runat="server"><span>Create Schema and Load Initial Data</span></asp:LinkButton></li>
                                            <li>
                                                <asp:LinkButton ID="LinkButtonValidate" class="LinkButton Validate" OnClick="LinkButtonValidate_Click"
                                                    runat="server"><span>Validate Schema</span></asp:LinkButton></li>
                                            <li>
                                                <asp:LinkButton ID="LinkButtonUpdate" class="LinkButton Update" OnClick="LinkButtonUpdate_Click"
                                                    runat="server"><span>Update Schema</span></asp:LinkButton></li>
                                        </ul>
                                        <div class="DoClear">
                                        </div>
                                    </div>
                                    <div class="Notification">
                                        <p>
                                            <asp:Label ID="lblNotification" runat="server" Text="Label"></asp:Label>
                                        </p>
                                        <p>
                                            <a href="~/LoginPage.aspx" visible="false" id="_loginLink" runat="server"><span>Continue
                                                to the Login Page </span></a>
                                        </p>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                                <ProgressTemplate>
                                    <div class="Notification Loading">
                                        <p>
                                            <span class="LoadingIco"></span>Please Wait while the Scripts are being Executed</p>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </cc1:RoundPanel>
                </div>
            </li>
        </ul>
    </div>
    <cc1:ConfirmMessageExt ID="_confirmMessageExt1" Selector="a.LinkButton.Install" runat="server"
        ConfirmMessage='Are you sure you want to create the DB Schema? This action will <span class="resalted">destroy all the tables and data</span> already present in the current DB Schema' />
    </form>
</body>
</html>
