<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="AboutDialog.ascx.cs"
    Inherits="AboutDialog" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<%@ Register TagPrefix="user" TagName="Header" Src="Header.ascx" %>
<%@ Register Src="Controls/Popup.ascx" TagName="Popup" TagPrefix="uc2" %>
<%@ Register Src="ArrayRenderer.ascx" TagName="ArrayRenderer" TagPrefix="uc3" %>
<uc2:Popup ID="PopUp1" runat="server" />

<script type="text/javascript">
    $(document).ready(function() {
        var renderer = null;
        var CurrentAboutRequest = null;
        var RequestMade = null;
        var eleNotify = '#divRenderer div.NotifyMessage';

        function callbackHandler(data) {
            if (renderer && data) {
                renderer.setDataSource([data.Version]);
                renderer.dataBind(true);
                RequestMade = true;
            }
            $.hideInLineMessage($(eleNotify));
        }

        function errorHandler(xhr, status, error) {
            $.showInlineMessage($(eleNotify), $.getResourceString('ErrorFound', 'Errors were found'), { sticky: true });
        }


        function aboutDialogOnShow() {
            if (RequestMade) return;
            //Create Renderer
            if (!renderer) {
                var arrManager = new ArrayRendererManager();
                
                arrManager.findArrayRenderers($('div.divAboutSpacer #divRenderer'));
                renderer = arrManager.getArrayRendererByIndex(0);
            }
            $.showInlineMessage($(eleNotify), $.getResourceString('RetrievingData', 'Retrieving data'), { sticky: true, highlightColor: "#FFCC33" });
            CurrentAboutRequest = $.getJsonResponse('<%= SEOToolSet.Common.WebHelper.GetAbsolutePath("Handler/GetVersion.ashx?jsoncallback=?") %>', callbackHandler, errorHandler, 20000);
        }
        
        function aboutDialogOnHide() {
            if (CurrentAboutRequest) CurrentAboutRequest.abort();
        }

        $('#hyperlinkApplicationVersion').click(function() {
            var aboutSeoToolSetTitle = $.getResourceString('AboutTitle', 'About SEOToolSet');
            $.showPopUp('divAbout',
                    { width: 380,
                        height: 205,
                        title: aboutSeoToolSetTitle,
                        onShow: aboutDialogOnShow,
                        onHide: aboutDialogOnHide
                    });
            return false;
        });
    });
</script>

<div id="divAbout" style="display: none;">
    <div class="divAboutSpacer">
        <div id="divRenderer">
            <div class="NotifyMessage">
            </div>
            <uc3:ArrayRenderer ID="ArrayRenderer1" TypeOfRenderer="Free" runat="server">
                <HeaderTemplate>
                    <h1 class="Logo">
                        <asp:Label ID="ApplicationName" runat="server" Text="<%$ Resources:CommonTerms, ApplicationName %>"></asp:Label>
                    </h1>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="AboutDivTable">
                        <table width="100%">
                            <tr class="RowItem">
                                <td style="width: 250px;" class="LabelTD">
                                    <asp:Label ID="ApplicationWebVersion" runat="server" meta:resourcekey="ApplicationWebVersion"></asp:Label>
                                </td>
                                <td style="width: 100px;" class="VersionTD">
                                    {SEOToolSetWebAppVersion}
                                </td>
                            </tr>
                            <tr class="AlternatingRowItem">
                                <td class="LabelTD">
                                    <asp:Label ID="ReportServicesVersion" runat="server" meta:resourcekey="ReportServicesVersion"></asp:Label>
                                </td>
                                <td class="VersionTD">
                                    {ReportServicesWebAppVersion.ReportServicesWebAppVersion}
                                </td>
                            </tr>
                            <tr class="RowItem">
                                <td class="LabelTD">
                                    <asp:Label ID="ReportFacadeVersion" runat="server" meta:resourcekey="ReportFacadeVersion"></asp:Label>
                                </td>
                                <td class="VersionTD">
                                    {ReportServicesWebAppVersion.ReportFacadeVersion}
                                </td>
                            </tr>
                            <tr class="AlternatingRowItem">
                                <td class="LabelTD">
                                    <asp:Label ID="TemporalFileManagerServiceVersion" runat="server" meta:resourcekey="TemporalFileManagerServiceVersion"></asp:Label>
                                </td>
                                <td class="VersionTD">
                                    {TempFileManagerServiceVersion}
                                </td>
                            </tr>
                            <tr class="RowItem">
                                <td class="LabelTD">
                                    {TempFileManagerProviderName}
                                </td>
                                <td class="VersionTD">
                                    {TempFileManagerProviderVersion}
                                </td>
                            </tr>
                        </table>
                    </div>
                </ItemTemplate>
            </uc3:ArrayRenderer>
        </div>
    </div>
</div>
