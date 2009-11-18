<%@ Page Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true" CodeBehind="SinglePageKda.aspx.cs"
    Inherits="SEOToolSet.WebApp.SinglePageKda" meta:resourcekey="PageResource" %>

<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register TagPrefix="user" TagName="PageTitle" Src="PageTitle.ascx" %>
<%@ Register Src="Controls/ToolTip.ascx" TagName="ToolTip" TagPrefix="uc3" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<%@ Register Src="Controls/Popup.ascx" TagName="Popup" TagPrefix="uc2" %>
<%@ Register TagPrefix="user" TagName="FileSelector" Src="FileSelector.ascx" %>
<%@ Register TagPrefix="user" TagName="AsyncReport" Src="Controls/AsyncReport.ascx" %>
<%@ Register Src="ArrayRenderer.ascx" TagName="ArrayRenderer" TagPrefix="uc4" %>
<%@ Register TagPrefix="user" TagName="WordPhrases" Src="WordPhrases.ascx" %>
<%@ Register TagPrefix="user" TagName="SubReportSelector" Src="SubReportSelector.ascx" %>
<%@ Register TagPrefix="user" TagName="SubReportHeader" Src="SubReportHeader.ascx" %>
<%@ Register Src="ReportPageConfigurator.ascx" TagName="ReportPageConfigurator" TagPrefix="uc5" %>
<asp:Content ID="Content2" ContentPlaceHolderID="contentArea" runat="server">
    <uc2:Popup ID="PopUp1" runat="server" />
    <uc3:ToolTip ID="ToolTip1" runat="server" />
    <!-- Control Used for the Page Title -->
    <user:PageTitle PageTitleText="Single-Page Analyzer" PageDescription="Analyze keywords usage on webpage"
        ID="PageHeading" runat="server" RenderRoundPanelStyles="false" meta:resourcekey="PageHeading"></user:PageTitle>
    <!-- Main application scripts -->
    <uc5:ReportPageConfigurator  InitReportsUrl="SinglePageAnalyzerInitUrl"  onPreClickJobs="singlePageAnalyzerInit" ID="ReportPageConfigurator1" runat="server" />
    <cc1:RoundPanel ID="RoundPanel1" runat="server">
        <div id="pInfo">
            <div id="PageSelector">
                <user:FileSelector ID="WebUserControl01" runat="server"></user:FileSelector>
            </div>
            <user:SubReportSelector ID="WebUserControl02" runat="server"></user:SubReportSelector>
        </div>
    </cc1:RoundPanel>
    <div id="ReportsSection">
        <script type="text/javascript">
            function singlePageAnalyzerInit(callback, entries, rm) {
                $.SinglePage = {};                
                $.SinglePage.IdProcess = -1;
                entries = entries || [];    
                
                if (entries.length > 0) {                            
                    $.getJsonResponse(rm.handlerUrl +'?url=' + encodeURI(entries[0].Url)+'&jsonp=?', function (data) {                    
                        if (data && data.id) {
                            $.SinglePage.IdProcess = data.id;
                            callback();
                        }
                        else{
                            console.log('Error ocurred in the SinglePageAnalyzer');
                        }                                                        
                    }, function(){
                            console.log('Error ocurred in the SinglePageAnalyzer Ajax Call');
                    });
                }
            }
            function AsyncReport_dataReceived(report, data) {
                try {
                    //Reports 1, 2 & 5
                    //getArrayRendererByIndex gets the ArrayRenderer inside the Report that has the index given
                    //could be retrieved by name too, but its easier to retrieve them using only the index
                    //the order of the renderers should be managed carefully, if the designer moves the arrayrenderer, the index may change.
                    var renderer = report.getArrayRendererByIndex(0);

                    //adds the dataSource for this rendererer 
                    //the renderer expects an array so in the cases where the object retrieved is not an array it may be converted first to an array
                    renderer.setDataSource(data.Keywords);

                    //Populate the renderer templates
                    renderer.dataBind();
                }
                catch (ex) {
                    console.log(ex.message);
                }
            }
            function CloakCheck_dataReceived(report, data) {                                                
                 try   {
                    //getArrayRendererByIndex gets the ArrayRenderer inside the Report that has the index given
                    //could be retrieved by name too, but its easier to retrieve them using only the index
                    //the order of the renderers should be managed carefully, if the designer moves the arrayrenderer, the index may change.
                    var renderer = report.getArrayRendererByIndex(0);
                    
                    
                    //adds the dataSource for this rendererer 
                    //the renderer expects an array so in the cases where the object retrieved is not an array it may be converted first to an array
                    renderer.setDataSource(data.Criteria);
                    
                    
                    //Populate the renderer templates
                    renderer.dataBind();
                                        
                }
                catch(ex) {
                    console.log(ex.message);
                }
            }

            function Tag_dataReceived(report, data) {
                try {
                    //Report Tags
                    var renderer = report.getArrayRendererByIndex(0);
                    renderer.setDataSource(data.Tags);
                    renderer.dataBind();
                }
                catch (ex) {
                    console.log(ex.message);
                }
            }

            function WordPhrases_dataReceived(report, data) {
                try {
                    //Report WordPhrases     
                    //Here could be up to 4 reports in this page                    
                    for (var ix = 0; ix < 4; ix++) {
                        //var rendererCount = report.getArrayRendererByIndex(ix * 2 );
                        var rendererPhrases = report.getArrayRendererByIndex(ix); // * 2) + 1);                        
                        if (!data.Phrases[ix]) {
                            //not data for that kind of report
                            report.ReportDiv.find('.Phrases:eq(' + ix + ')').hide();
                            continue;
                        }
                        if (rendererPhrases) //make sure the Renderers exist in the page  
                        {
                            rendererPhrases.setDataSource(data.Phrases[ix].Keywords);
                            rendererPhrases.onDataBound = $.createDelegateExpanded(rendererPhrases, function(e, index) {
                                try {
                                    if (!e || !e.renderedHTML || !data.Phrases[index].Count)
                                    { return; }
                                    var tokkenRegex = /\{-.+?-\}/g;
                                    var tokens = e.renderedHTML.match(tokkenRegex) || [];

                                    for (var j = 0; j < tokens.length; j++) {
                                        var valueForCell = rendererPhrases.getMatchkey(data.Phrases[index].Count, tokens[j].replace(/-/g, ''));
                                        e.renderedHTML = e.renderedHTML.replace(tokens[j], valueForCell);
                                    }
                                }
                                catch (ex) {
                                    console.log("[Error] : " + ex);
                                }

                            }, ix);
                            //Default Sort order - AGL 7/14/09
                            rendererPhrases.orderBy('AllWords.Counter', 'DESC', 'Number');
                            rendererPhrases.dataBind();
                        }
                    }
                    //Add the DESC class to each word phrases table by default - AGL 7/15/09
                    $("div[id*='ArrayRenderer9']").each(function() {
                            $(this).find("th[field_name*=AllWords]").addClass("DESC");
                    });
                }
                catch (ex) {
                    console.log(ex.message);
                }
            }

            function Links_dataReceived(report, data) {
                try {
                    var renderer = report.getArrayRendererByIndex(0);
                    renderer.setDataSource(data.Links);

                    renderer.onRowDataBound = function(e) {
                        //replace the true/false values of the Rel column with a green arrow
                        if (!e)
                        { return; }
                        var img_class = '';
                        if (e.row && e.row.Rel) {
                            img_class = 'LinkImageClass';
                        }
                        e.result = e.result.replace("[img_class]", img_class);
                    };

                    renderer.onDataBound = function(e) {
                        try {
                            if (!e || !e.renderedHTML || !data.SummaryLinks)
                            { return; }
                            e.renderedHTML = e.renderedHTML.replace('[Total]', data.SummaryLinks.Total);
                            e.renderedHTML = e.renderedHTML.replace('[UniqueTargets]', data.SummaryLinks.UniqueTargets);
                        }
                        catch (ex) {
                            console.log("[Error] : " + ex);
                        }
                    };

                    renderer.dataBind();
                }
                catch (ex) {
                    console.log(ex.message);
                }
            }
            
            function kwds_dataReceived(report, data) {
                try {
                    var renderer = report.getArrayRendererByIndex(0);
                    renderer.setDataSource(data.Keywords);
                    renderer.onRowDataBound = function(e) {
                        //get images for true/false
                        if (!e) { return; }
                        var img_toolset = '';
                        var img_page = '';
                        if (e.row && e.row.Toolset) {
                            img_toolset = 'LinkImageClass';
                        }
                        if (e.row && e.row.Page) {
                            img_page = 'LinkImageClass';
                        }
                        e.result = e.result.replace("[img_toolset]",img_toolset);
                        e.result = e.result.replace("[img_page]",img_page);
                    };
                    renderer.dataBind();
                    keywordsImgUrl = data.Chart;
                    keywordsImgLength = data.Keywords.length;
                    //$("div[id*='AsyncReport2'] div[id*='ArrayRenderer2'] table tbody tr.RowItem:first").append('<td rowspan="'+keywordsImgLength+'"><img src="'+keywordsImgUrl+'" /></td>');
                    //$("div[id*='AsyncReport2'] div[id*='ArrayRenderer2'] table tbody tr.RowItem:first").append('<td rowspan="'+data.Keywords.length+'"><img src="http://chart.apis.google.com/chart?chs=400x450&cht=bhs&chd=t:11.7,11.7,10.9,10.2,9.8,9.5,7.2,5.6,5.3,5.3,2.2,4.2,1.1,1.1,3.8&chxt=x,y&chxl=0:||1:|search|engine|search%20engine|site|web|marketing|design|internet|website|internet%20marketing|search%20engine%20marketing|search%20engine%20optimization|search%20engine%20placement|search%20engine%20relationship%20chart|seo&chds=0,15&chco=281299&chbh=a&chxs=0,0000DD,12,0,t|1,0C5EAC,12,-1,t&chf=bg,s,E7EBFF" /></td>');
                    $("#KeywordsChart").html('<img src="'+keywordsImgUrl+'" />');
                    var newwid = parseInt(($("#ReportsSection").width() - 415)*100 / $("#ReportsSection").width());
                    //alert(newwid + 'x' + $("#ReportsSection").height());
                    $("div[id$='AsyncReport2'] div.xTable div[id$='ArrayRenderer2']").css('width',''+newwid+'%');
                    $(window).bind("resize", function() {
                        newwid = parseInt(($("#ReportsSection").width() - 415)*100 / $("#ReportsSection").width());
                        $("div[id$='AsyncReport2'] div.xTable div[id$='ArrayRenderer2']").css('width',''+newwid+'%');
                    });
                } 
                catch (ex) {
                    console.log(ex.message);
                }
            }

            function WordMetrics_dataReceived(report, data) {
                try {
                    //Report Tags
                    var renderer = report.getArrayRendererByIndex(0);
                    var wordCountRenderer = report.getArrayRendererByIndex(1);
                    var arr = new Array();

                    renderer.setDataSource(data.WordMetrics);
                    renderer.dataBind();

                    arr.push(data.UsedWords);

                    wordCountRenderer.setDataSource(arr);
                    wordCountRenderer.dataBind();
                }
                catch (ex) {
                    console.log(ex.message);
                }
            }
            
            function beforeCallReportUrlCloak(evt) {
                evt.ReportUrl = $.stringFormat('{0}?url={1}&jsonp=?', evt.report.ReportUrl, $("input[id*='txtUrlFile']:first").val());
            }
            function beforeCallReportUrl(evt) {
                var cpId = $.CurrentPage.cbxProjects.getDropDown().val();
                var lang = $.CurrentPage.cbxLanguages.getDropDown().val();
                if (lang == 'choose') {
                    lang = 'en_US';
                }
                //evt.ReportUrl = evt.report.ReportUrl + '?pid=' + $.SinglePage.IdProcess + '&project=' + cpId + '&lang=' + lang );
                evt.ReportUrl = $.stringFormat('{0}?pid={1}&project={2}&lang={3}&jsonp=?', evt.report.ReportUrl, $.SinglePage.IdProcess, cpId, lang);                
            }
            
            
        </script>

        <div id="SubReportContainer" style="display: none;">
            <user:SubReportHeader TitleText="Page Report" ID="WebUserControl03" runat="server"
                meta:resourcekey="SinglePageReportsHeader"></user:SubReportHeader>
            <!-- Reports Section -->
            <user:AsyncReport ReportTitle='Cloak Check' CheckName='Cloak Check' ReportUrl='http://bclaydev.bruceclay.com/services/sitechecker.py/GetCloakingCheck'
                OnBeforeAjaxCall="beforeCallReportUrlCloak" ReportTooltip='Checks and displays results for user-agent based cloaking.'
                ReportType="10" ReportIdentifier="CloakCheckInfoReporter" OnClientDataReceived='CloakCheck_dataReceived' ID="AsyncReport1"
                runat="server" meta:resourcekey="CloakCheckInfoReport">
                <ItemTemplate>
                    <div class='xTable'>
                        
                            
                        <uc4:ArrayRenderer TypeOfRenderer="Free" ID="ArrayRenderer3" runat="server" ItemCssClass="RowItem" AlternatingCssClass="RowItem" 
                            HeaderPath="table tbody" ItemPath="table tbody tr" ContainerTagName="table">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td class="[CLASS_NAME]" style="width:14%">
                                                <img class='DynamicImage' image_src='css/Assets/{CheckedCriterion}-favicon.png' alt='{CheckedCriterion}' title='{CheckedCriterion}' />
                                                <br />
                                                {Description}
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ItemTemplate>
                        </uc4:ArrayRenderer>    
                    </div>
                    <div class="DoClear">
                    </div>
                </ItemTemplate>
            </user:AsyncReport>
            <user:AsyncReport ReportUrl="http://bclaydev.bruceclay.com/services/kda.py/GetKeywordUsage"  OnBeforeAjaxCall="beforeCallReportUrl" 
                ReportIdentifier="KeywordsReporter" ReportTitle='Keywords' CheckName='Prominent & Optimized Keywords'
                ReportTooltip='Shows the prominent and optimized keyword usage on this page.' ReportType="6"
                OnClientDataReceived='kwds_dataReceived' ID="AsyncReport2" runat="server" meta:resourcekey="KeywordsReport">
                <ItemTemplate>
                    <div class='xTable'>
                        <div style="float:right;border:1px solid #C2D2E5" id="KeywordsChart"></div>
                        <uc4:ArrayRenderer ID="ArrayRenderer2" runat="server" HeaderCssClass="RowHeader"
                            ItemCssClass="RowItem" AlternatingCssClass="AlternatingRowItem">
                            <HeaderTemplate>
                                <table>
                                    <tbody>
                                        <tr class="[CLASS_NAME]">
                                            <th class="Sortable" field_name='Name' data_type="String" style="text-align:left;">
                                                <asp:Label ID="KeywordsHeading" runat="server" meta:resourcekey="KeywordsHeading"></asp:Label>
                                            </th>
                                            <th class="Sortable" field_name='Toolset' data_type='String' style="width: 60px;">
                                                <asp:Label ID="ToolsetKeywordsHeading" runat="server" meta:resourcekey="ToolsetKeywordsHeading"></asp:Label>
                                            </th>
                                            <th class="Sortable" field_name='Page' data_type='String' style="width: 60px;">
                                                <asp:Label ID="PageKeywordsHeading" runat="server" meta:resourcekey="PageKeywordsHeading"></asp:Label>
                                            </th>
                                            <th class="Sortable" field_name='Activity' data_type='Number' style="width: 100px;">
                                                <asp:Label ID="ActivityKeywordsHeading" runat="server" meta:resourcekey="ActivityKeywordsHeading"></asp:Label>
                                            </th>
                                            <th class="Sortable" field_name='Used' data_type='Number' style="width: 100px;">
                                                <asp:Label ID="UsedKeywordsHeading" runat="server" meta:resourcekey="UsedKeywordsHeading"></asp:Label>
                                            </th>
                                            <!-- <th field_name='Chart' style="width: 400px;>
                                                <asp:Label ID="ChartKeywordsHeading" runat="server" meta:resourcekey="ChartKeywordsHeading"></asp:Label>
                                            </th> -->
                                        </tr>
                                    </tbody>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tbody>
                                        <tr class='[CLASS_NAME]'>
                                            <td class="Left Keyword">
                                                {Name}
                                            </td>
                                            <td>
                                                <div class="[img_toolset]">
                                                </div>
                                            </td>
                                            <td>
                                                <div class="[img_page]">
                                                </div>
                                            </td>
                                            <td>
                                                {Activity}
                                            </td>
                                            <td>
                                                {Used}
                                            </td>
                                            
                                        </tr>
                                    </tbody>
                                </table>
                            </ItemTemplate>
                        </uc4:ArrayRenderer>
                    </div>
                </ItemTemplate>
            </user:AsyncReport>
            <user:AsyncReport ReportUrl="http://bclaydev.bruceclay.com/services/kda.py/GetTagInformation"  OnBeforeAjaxCall="beforeCallReportUrl"   ReportIdentifier="TagInformationReporter" ReportTitle='Tag Information'
                CheckName='Tag information' ReportTooltip='Provides infromation about the HTML tags on the page.'
                ReportType="3" OnClientDataReceived='Tag_dataReceived' ID="AsyncReport3" runat="server"
                meta:resourcekey="TagInformationReport">
                <ItemTemplate>
                    <div class='xTable'>
                        <uc4:ArrayRenderer ID="ArrayRenderer1" runat="server" HeaderCssClass="RowHeader"
                            ItemCssClass="RowItem" AlternatingCssClass="AlternatingRowItem">
                            <HeaderTemplate>
                                <table>
                                    <tbody>
                                        <tr class='[CLASS_NAME]'>
                                            <th style="width: 100px;" class="Sortable" field_name='Name' data_type="String">
                                                <asp:Label ID="TagTagInformationHeading" runat="server" meta:resourcekey="TagTagInformationHeading"></asp:Label>
                                            </th>
                                            <th style="width: 100px;" class="Sortable" field_name='Count' data_type="Number">
                                                <asp:Label ID="CountTagInformationHeading" runat="server" meta:resourcekey="CountTagInformationHeading"></asp:Label>
                                            </th>
                                            <th style="width: 100px;" class="Sortable" field_name='StopWords' data_type="Number">
                                                <asp:Label ID="StopWordsTagInformationHeading" runat="server" meta:resourcekey="StopWordsTagInformationHeading"></asp:Label>
                                            </th>
                                            <th style="width: 100px;" class="Sortable" field_name='UsedWords' data_type="Number">
                                                <asp:Label ID="UsedWordsTagInformationHeading" runat="server" meta:resourcekey="UsedWordsTagInformationHeading"></asp:Label>
                                            </th>
                                            <th style="width: 100px;" class="Sortable" field_name='Length' data_type="Number">
                                                <asp:Label ID="LengthTagInformationHeading" runat="server" meta:resourcekey="LengthTagInformationHeading"></asp:Label>
                                            </th>
                                            <th class="Sortable" field_name='Data.ResultData' data_type="String">
                                                <asp:Label ID="DataTagInformationHeading" runat="server" meta:resourcekey="DataTagInformationHeading"></asp:Label>
                                            </th>
                                        </tr>
                                    </tbody>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tbody>
                                        <tr class='[CLASS_NAME]'>
                                            <td valign="top" class="Keyword">
                                                {Name}
                                            </td>
                                            <td valign="top">
                                                {Count}
                                            </td>
                                            <td valign="top">
                                                {StopWords|default:'-'}
                                            </td>
                                            <td valign="top">
                                                {UsedWords|default:'-'}
                                            </td>
                                            <td valign="top">
                                                {Length|default:'-'}
                                            </td>
                                            <td class="Left" valign="top">
                                                <table>
                                                    <tr>
                                                        <td valign="top">
                                                            {Data.ResultData}
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            {Data.TextData}
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ItemTemplate>
                        </uc4:ArrayRenderer>
                    </div>
                </ItemTemplate>
            </user:AsyncReport>
            <user:AsyncReport ReportUrl="http://bclaydev.bruceclay.com/services/kda.py/GetWordPhrases"  OnBeforeAjaxCall="beforeCallReportUrl"   ReportTitle='Word Phrases' CheckName='Word Phrases' ReportTooltip='Provides a detailed breakdown of where keywords appear on the page.'
                ReportType="4" ReportIdentifier="WordPhrasesReporter" OnClientDataReceived='WordPhrases_dataReceived'
                ID="AsyncReport4" runat="server" meta:resourcekey="WordPhrasesReport">
                <ItemTemplate>
                    <user:WordPhrases TitleText="1 Word Phrases" ID="WebUserControl02" runat="server">
                    </user:WordPhrases>
                    <user:WordPhrases TitleText="2 Word Phrases" ID="WebUserControl03" runat="server">
                    </user:WordPhrases>
                    <user:WordPhrases TitleText="3 Word Phrases" ID="WebUserControl04" runat="server">
                    </user:WordPhrases>
                    <user:WordPhrases TitleText="4 Word Phrases" ID="WebUserControl05" runat="server">
                    </user:WordPhrases>
                </ItemTemplate>
            </user:AsyncReport>
            <user:AsyncReport ReportUrl="http://bclaydev.bruceclay.com/services/kda.py/GetLinearDistribution"  OnBeforeAjaxCall="beforeCallReportUrl" ReportIdentifier="LinearKeywordDistributionReporter" ReportTitle='Linear Keyword Distribution'
                CheckName='Keyword Distribution' ReportTooltip='Provides charts showing the distribution of prominent words on the page.'
                ReportType="5" OnClientDataReceived='AsyncReport_dataReceived' ID="AsyncReport5"
                runat="server" meta:resourcekey="LinearKeywordDistributionReport">
                <ItemTemplate>
                    <div class='xLinearDistribution'>
                        <uc4:ArrayRenderer TypeOfRenderer="Free" ID="ArrayRenderer2" runat="server" HeaderCssClass="RowHeader"
                            ItemCssClass="RowItem" AlternatingCssClass="AlternatingRowItem">
                            <HeaderTemplate>
                                <div class="ButtonDiv Sortable" field_name='Name' data_type='String'>
                                    <asp:Label ID="orderByKeywordCommand" runat="server" Text="Order by Keyword" meta:resourcekey="orderByKeywordCommand"></asp:Label>
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="xLinearContainer">
                                    <h2>
                                        <span>{Name}</span></h2>
                                    <img class='DynamicImage' image_src='{SrcImage}' alt='{Name}' title='{Name}' />
                                </div>
                            </ItemTemplate>
                        </uc4:ArrayRenderer>
                        <div class="DoClear">
                        </div>
                    </div>
                </ItemTemplate>
            </user:AsyncReport>
            <user:AsyncReport ReportUrl="http://bclaydev.bruceclay.com/services/kda.py/GetLinkText"  OnBeforeAjaxCall="beforeCallReportUrl" ReportIdentifier="LinkTextReporter" ReportTitle='Link Text' CheckName='Link Text'
                ReportTooltip='Provides a in depth report for the links on the page.' ReportType="6"
                OnClientDataReceived='Links_dataReceived' ID="AsyncReport6" runat="server" meta:resourcekey="LinkTextReport">
                <ItemTemplate>
                    <div class='xTable'>
                        <uc4:ArrayRenderer ID="ArrayRenderer2" runat="server" HeaderCssClass="RowHeader"
                            ItemCssClass="RowItem" AlternatingCssClass="AlternatingRowItem">
                            <HeaderTemplate>
                                <table>
                                    <tbody>
                                        <tr class="[CLASS_NAME]">
                                            <th class="Sortable" field_name='Order' data_type='Number'>
                                                <asp:Label ID="orderLinkTextHeading" runat="server" meta:resourcekey="orderLinkTextHeading"></asp:Label>
                                            </th>
                                            <th class="Sortable" field_name='LinkedTo' data_type='String' style="width: 300px;">
                                                <asp:Label ID="urlLinkedToLinkTextHeading" runat="server" meta:resourcekey="urlLinkedToLinkTextHeading"></asp:Label>
                                            </th>
                                            <th class="Sortable" field_name='Category' data_type='String' style="width: 60px;">
                                                <asp:Label ID="httpLinkTextHeading" runat="server" meta:resourcekey="httpLinkTextHeading"></asp:Label>
                                            </th>
                                            <th class="Sortable" field_name='Rel' data_type='String' style="width: 80px;">
                                                <asp:Label ID="relNoFollowLinkTextHeading" runat="server" meta:resourcekey="relNoFollowLinkTextHeading"></asp:Label>
                                            </th>
                                            <th class="Sortable" field_name='Title' data_type='String'>
                                                <asp:Label ID="titleTagLinkTextHeading" runat="server" meta:resourcekey="titleTagLinkTextHeading"></asp:Label>
                                            </th>
                                            <th class="Sortable" field_name='Type' data_type='String' style="width: 80px;">
                                                <asp:Label ID="typeLinKTextHeading" runat="server" meta:resourcekey="typeLinkTextHeading"></asp:Label>
                                            </th>
                                            <th class="Sortable" field_name='AnchorText' data_type='String'>
                                                <asp:Label ID="anchorTextLinkTextHeading" runat="server" meta:resourcekey="anchorTextLinkTextHeading"></asp:Label>
                                            </th>
                                            <th class="Sortable" field_name='Error' data_type='String' style="width: 120px;">
                                                <asp:Label ID="errorLinkTextHeading" runat="server" meta:resourcekey="errorLinkTextHeading"></asp:Label>
                                            </th>
                                        </tr>
                                    </tbody>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tbody>
                                        <tr class='[CLASS_NAME]'>
                                            <td>
                                                {Order}
                                            </td>
                                            <td class="Left Keyword" style="width: 250px;">
                                                {LinkedTo}
                                            </td>
                                            <td>
                                                {Category}
                                            </td>
                                            <td>
                                                <div class="[img_class]">
                                                </div>
                                            </td>
                                            <td>
                                                {Title|default:''}
                                            </td>
                                            <td>
                                                {Type}
                                            </td>
                                            <td class="Left">
                                                {AnchorText}
                                            </td>
                                            <td style="width: 100px;">
                                                {Error|default:''}
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ItemTemplate>
                        </uc4:ArrayRenderer>
                    </div>
                </ItemTemplate>
            </user:AsyncReport>
            <user:AsyncReport ReportUrl="http://bclaydev.bruceclay.com/services/kda.py/GetWordMetrics"  OnBeforeAjaxCall="beforeCallReportUrl" ReportIdentifier="WordMetricsReporter" ReportTitle='Word Metrics'
                CheckName='Used Word' ReportTooltip='Provides information on the pages readability and the text being spidered.'
                ReportType="7" OnClientDataReceived='WordMetrics_dataReceived' ID="AsyncReport7"
                runat="server" meta:resourcekey="WordMetricsReport">
                <ItemTemplate>
                    <div class='xTable'>
                        <uc4:ArrayRenderer ID="ArrayRenderer2" runat="server" HeaderCssClass="RowHeader"
                            ItemCssClass="RowItem" AlternatingCssClass="AlternatingRowItem">
                            <HeaderTemplate>
                                <table>
                                    <tbody>
                                        <tr class='[CLASS_NAME]'>
                                            <th style="width: 200px" class="Sortable" field_name='Name' data_type='String'>
                                                <asp:Label ID="NameWordMetricsHeading" runat="server" meta:resourcekey="NameWordMetricsHeading"></asp:Label>
                                            </th>
                                            <th class="Sortable" style="width: 100px" field_name='Value' data_type='Number'>
                                                <asp:Label ID="valueWordMetricsHeading" runat="server" meta:resourcekey="valueWordMetricsHeading"></asp:Label>
                                            </th>
                                            <th class="Sortable" field_name='Description' data_type='String'>
                                                <asp:Label ID="descriptionWordMetricsHeading" runat="server" meta:resourcekey="descriptionWordMetricsHeading"></asp:Label>
                                            </th>
                                        </tr>
                                    </tbody>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tbody>
                                        <tr class='[CLASS_NAME]'>
                                            <td class="Keyword">
                                                {Name}
                                            </td>
                                            <td>
                                                {Value}
                                            </td>
                                            <td class="Left">
                                                {Description}
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ItemTemplate>
                        </uc4:ArrayRenderer>
                        <uc4:ArrayRenderer ID="ArrayRenderer11" runat="server" HeaderCssClass="RowHeader"
                            ItemCssClass="RowItem" AlternatingCssClass="AlternatingRowItem">
                            <ItemTemplate>
                                <table>
                                    <tbody>
                                        <tr class="RowHeader">
                                            <td style="text-align: left">
                                                <asp:Label ID="totalWordsUsedItem" runat="server" meta:resourcekey="totalWordsUsedItem"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                {Words}
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ItemTemplate>
                        </uc4:ArrayRenderer>
                    </div>
                </ItemTemplate>
            </user:AsyncReport>
        </div>
    </div>
</asp:Content>
