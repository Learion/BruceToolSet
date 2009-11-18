<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OverrideFontSettings.ascx.cs"
    Inherits="SEOToolSet.WebApp.Controls.OverrideFontSettings" %>

<script type="text/javascript">
    $(function() {
        //TODO: Make this a parametrizable selector
        $('#hyperlinkFontSettings').click(function() {
            $.showPopUp('divFontSettings',
                        { width: 480,
                            title: ":."
                        });
            return false;
        });
    });
</script>

<div id="divFontSettings" class="DivForDialog">
    <div class="wrapper-popUp">
        <asp:FormView ID="formViewFontSettings" runat="server" DataSourceID="odsFontSettings"
            DefaultMode="Edit">
            <EditItemTemplate>
                <div class="FormPanel">
                    <div class="FormCSS">
                        <div class="Legend">
                            <h2>
                                <asp:Literal ID="LiteralFontSettings" Text="<%$ Resources:CommonTerms, FontSettings %>"
                                    runat="server"></asp:Literal>
                            </h2>
                        </div>
                        <div class="Field OneLine">
                            <label>
                                Font Family
                            </label>
                            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="odsFontFamilies"
                                DataTextField="Text" DataValueField="Value" SelectedValue='<%# Bind("FontFamily") %>'>
                            </asp:DropDownList>
                        </div>
                        <div class="Field OneLine">
                            <label>
                                Header Font Size
                            </label>
                            <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="odsFontSizes" DataTextField="Text"
                                DataValueField="Value" SelectedValue='<%# Bind("HeaderFontSize") %>'>
                            </asp:DropDownList>
                        </div>
                        <div class="Field OneLine">
                            <label>
                                Body Font Size
                            </label>
                            <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="odsFontSizes" DataTextField="Text"
                                DataValueField="Value" SelectedValue='<%# Bind("BodyFontSize") %>'>
                            </asp:DropDownList>
                        </div>
                        <div class="Field OneLine">
                            <label>
                                Footer Font Size
                            </label>
                            <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="odsFontSizes" DataTextField="Text"
                                DataValueField="Value" SelectedValue='<%# Bind("FooterFontSize") %>'>
                            </asp:DropDownList>
                        </div>
                        <div class="Field OneLine">
                            <label>
                                Use Pixels Units
                            </label>
                            <asp:CheckBox ID="UsePixelsCheckBox" runat="server" Checked='<%# Bind("UsePixels") %>' />
                            <span class="Hint">if not checked, percentage values will be used for the font-size</span>
                        </div>
                        <div class="Field OneLine">
                            <label>
                                Header And Footer always visible
                            </label>
                            <asp:CheckBox ID="KeepHeaderAlwaysVisibleCheckbox" runat="server" Checked='<%# Bind("HeaderFixed") %>' />
                            <span class="Hint">When checked allow the header and footer to keep always visible.</span>
                        </div>
                        <div class="DoClear">
                        </div>
                    </div>
                </div>
                <div class="CenterWrapper">
                    <ul>
                        <li>
                            <asp:LinkButton ValidationGroup="UpdateFontSettings" CssClass="LinkCommandRound"
                                ID="ButtonSave" runat="server" CausesValidation="True" CommandName="Update">
                                <span>
                                    <asp:Literal ID="Literal6" Text="<%$ Resources:CommonTerms, Save %>" runat="server"></asp:Literal>
                                </span>
                            </asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton CssClass="LinkCommandRound" ID="LinkButtonReset" runat="server" OnClick="LinkButtonReset_Click">
                                <span>
                                    <asp:Literal ID="Literal1" Text="<%$ Resources:CommonTerms, Reset %>" runat="server"></asp:Literal></span>
                            </asp:LinkButton>
                        </li>
                        <li><a href='###' class="ClosePopUp LinkCommandRound" id="ButtonCancel" runat="server">
                            <span>
                                <asp:Literal ID="Literal7" Text="<%$ Resources:CommonTerms, Cancel %>" runat="server"></asp:Literal></span></a>
                        </li>
                    </ul>
                    <div class="DoClear">
                    </div>
                </div>
            </EditItemTemplate>
        </asp:FormView>
    </div>
</div>
<asp:ObjectDataSource ID="odsFontSettings" runat="server" SelectMethod="GetFontSettings"
    TypeName="SEOToolSet.WebApp.odsClass.FontsSettingsODS" UpdateMethod="UpdateFontSettings">
    <UpdateParameters>
        <asp:Parameter Name="FontFamily" Type="String" />
        <asp:Parameter Name="HeaderFontSize" Type="Int32" />
        <asp:Parameter Name="BodyFontSize" Type="Int32" />
        <asp:Parameter Name="FooterFontSize" Type="Int32" />
        <asp:Parameter Name="UsePixels" Type="Boolean" />
    </UpdateParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsFontFamilies" runat="server" SelectMethod="GetFontFamilies"
    TypeName="SEOToolSet.WebApp.odsClass.FontsSettingsODS"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsFontSizes" runat="server" SelectMethod="FontSizes" TypeName="SEOToolSet.WebApp.odsClass.FontsSettingsODS">
</asp:ObjectDataSource>
