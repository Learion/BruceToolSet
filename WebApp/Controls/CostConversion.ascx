<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CostConversion.ascx.cs"
    Inherits="SEOToolSet.WebApp.Controls.CostConversion" %>
<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<div class="ConversionControl">
    <div class="ctnr">
        <asp:HiddenField ID="FromCurrency" Value="USD" runat="server" />        
        <asp:HiddenField ID="FromCurrencyValue" Value="10" runat="server" />
        <p>
            <asp:Label ID="_labelSeePrice" runat="server" meta:resourceKey="SeePriceInOtherCurrency"></asp:Label></p>
        <p>
            <%--<asp:DropDownList ID="_dropDownListCurrencies" DataSourceID="_currenciesObjectDataSource"
                DataTextField="CurrencyName" DataValueField="Code" runat="server" AppendDataBoundItems="true">
                <asp:ListItem meta:resourceKey="ChooseCurrency" Text="Choose Currency" Value="-1"></asp:ListItem>
            </asp:DropDownList>--%>
            <select id="<%=ClientID %>_dropDownListCurrencies" class="ddl-currencies">
                <cc1:CustomRepeater ID="CustomRepeater1" runat="server" DataSourceID="_currenciesObjectDataSource">
                    <HeaderTemplate>
                        <option value='-1'><%# GetLocalResourceObject("ChooseCurrency") %></option>                        
                    </HeaderTemplate>
                    <ItemTemplate>
                    <option value='<%#Eval("Code") %>' currency_symbol='<%# Eval("CurrencySymbol") %>'><%# Eval("CurrencyName") %></option>
                    </ItemTemplate>
                </cc1:CustomRepeater>    
            </select>
            
        </p>
        <div class="ConvertionResult" runat="server" id="_panelResults">
            <p class="Result">
                <asp:Label CssClass="CurrencySymbol" runat="server" ID="_currencySymbol"></asp:Label>&nbsp;
                <asp:Label CssClass="CurrencyAmount" runat="server" ID="_currencyAmount"></asp:Label>
            </p>
            <p>
                <asp:Label CssClass="AproximatelyCost" meta:resourceKey="ApproximateMontlyCost" runat="server" ID="_aproxCost"></asp:Label>
            </p>
        </div>
    </div>
</div>
<asp:ObjectDataSource ID="_currenciesObjectDataSource" runat="server" SelectMethod="GetWorldCurrencies"
    TypeName="SEOToolSet.WebApp.Controls.CostConversionUtil.Currency"></asp:ObjectDataSource>
    
 <script type="text/javascript">
 
    var ddlId = "#<%=ClientID %>_dropDownListCurrencies";
    
    var _converter = {
        req : null,
        showValueInOtherMoney : function() {
            var currentAmount = $('#<%=FromCurrencyValue.ClientID %>').val();
            var currentCurrencyCode = $('#<%=FromCurrency.ClientID %>').val();
            var destinationCurrencyCode = $('#<%=ClientID %>_dropDownListCurrencies').val();
            //using yql we retrieve the exchange rate between two currencies.
            var url = 'http://query.yahooapis.com/v1/public/yql?q=select * from html where url%3D"http%3A%2F%2Ffinance.yahoo.com%2Fq%3Fs%3D{CURCODE}{DESTCODE}" and xpath%3D\'%2F%2F*[%40id%3D"yfs_l10_{cur_code}{dest_code}%3Dx"]\' %0A&format=json'.replace('{CURCODE}',currentCurrencyCode).replace('{DESTCODE}', destinationCurrencyCode).replace('{cur_code}', currentCurrencyCode.toLowerCase()).replace('{dest_code}', destinationCurrencyCode.toLowerCase());
            //console.log(decodeURI(url));
          
            if($('#<%= _panelResults.ClientID %>').is(':visible')) {
                $('#<%= _panelResults.ClientID %>').fadeOut();
            }
            this.req = $.getJsonResponse(url, function (data) { 
            var tx = -1;
                try {
                    if (data.query.count > 0) {
                        tx = data.query.results.span[0].content;
                        tx = parseFloat(tx);
                        var symbol = $($(ddlId).getSelectedItem()).attr('currency_symbol');
                        var amount = tx * currentAmount;
                        amount = $.roundNumber(amount,4);
                                                
                        $('#<%= _currencySymbol.ClientID%>').text(symbol);
                        $('#<%= _currencyAmount.ClientID %>').text(amount);
                        
                        $('#<%= _panelResults.ClientID %>').fadeIn();
                        
                    }
                    else {
                        console.log('No results');
                    }
                }
                catch(e) {
                    
                }
            } , function (error) { console.log(error); }, 3000);
                                    
        },
        setFromValue : function(v) {
            $('#<%=FromCurrencyValue.ClientID %>').val(v);
        }                             
    };
    
    <%=ClientID %>_converter = _converter;            
               
    $(function () {                        
        ddl = $(ddlId);
        ddl.change(function () {            
             <%=ClientID %>_converter.showValueInOtherMoney();
        });
    });
 </script>
    
