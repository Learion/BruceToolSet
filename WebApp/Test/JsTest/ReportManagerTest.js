/**
 * @author rriojas
 */
function ReportManagerTest(){
    Test.call(this);
    this.instance = null;
    this.reportId = null;
}

ReportManagerTest.prototype = new Test();

ReportManagerTest.prototype.setup = function(){
    this.reportId = "ctl00_contentArea_AsyncReport1";
    //create in page the HTML Needed to some methods of the ReportManager to Work accurately
    $('<div id="helperContet_ReportManagerTest"></div>').appendTo(document.body);
    var reportHTML = "<div id='" + this.reportId + "' class='Report' report_type='1' check_name='Keyword List' on_data_received='AsyncReport_dataReceived' report_name='Untitled Report'><div class='PanelRounded'> <div class='ReportHeader' title='Breaks down the number of times each ToolSet Keyword was found on the page.'> <h1>ToolSet Keywords<span></span></h1> <div class='LoadingMark' style='display: none;'> <h1>Loading...</h1> </div> <a class='ReportCollapseTrigger' href='javascript:void(0);'><span>Collapse</span></a> </div> <div class='TableArea'> <div class='xTable'> <div id='ctl00_contentArea_AsyncReport1_ArrayRenderer1' class='ArrayRenderer' header_class='RowHeader' row_class='RowItem' renderer_type='Grid' alternating_row_class='AlternatingRowItem' on_databinding=''> <div class='xHeader'> <table> <tbody> <tr class='[CLASS_NAME]'> <th> Keyword </th> <th> Used </th> </tr> </tbody> </table> </div> <div class='xItemTemplate'> <table> <tbody> <tr class='[CLASS_NAME]'> <td class='Left Keyword'> {Name} </td> <td> {Used} </td> </tr> </tbody> </table> </div> </div> </div> </div> </div></div>";
    $('#helperContet_ReportManagerTest').html(reportHTML);
}

ReportManagerTest.prototype.testGetInstance = function(){
    this.instance = ReportManager.GetInstance();
    $.assertNotNull('TestGetInstance', this.instance);
}

ReportManagerTest.prototype.testFindReportsInPage = function(){
    this.instance.findReportsInPage();
    var count = 0;
    for (var key in this.instance.reports) {
        count++;
    }
    $.assertEquals("Just One Report", 1, count);
}

ReportManagerTest.prototype.tearDown = function(){
    $('#helperContet_ReportManagerTest').remove();
    ReportManager._Instance = null;
}

function PageSelectionToolTest(){
    Test.call(this);
    this.ps = null;
    this.reportsToRenderCounter = 0;
}

PageSelectionToolTest.prototype = new Test();

PageSelectionToolTest.prototype.setup = function(){
    var reportSelectorHtml;
    $('<div id="helperContext_PageSelectionToolTest"></div>').appendTo(document.body);
    reportSelectorHtml = "<div id='pInfo' class='PanelRounded'>" +
    " <div id='PageSelector'>" +
    " <div class='FileSelector' id='ctl00_contentArea_WebUserControl01'>" +
    " <table>" +
    " <tbody><tr>" +
    " <td class='LabelTD Red' style='width: 80px;'>" +
    " Analyze Page:" +
    " </td>" +
    " <td style='width: 210px;'>" +
    " <input type='text' class='FormText' style='width: 200px;' id='ctl00_contentArea_WebUserControl01_txtUrlFile' name='ctl00$contentArea$WebUserControl01$txtUrlFile' disabled=''/>" +
    " </td>" +
    " <td style='width: 80px;'>" +
    " <a title='Edit URL' class='LinkCommandRound' id='ctl00_contentArea_WebUserControl01_lnkEditUrl' href='#'><span>Edit URL</span></a>" +
    " </td>" +
    " <td style='width: 20px;'>" +
    " or" +
    " </td>" +
    " <td style=''>" +
    " <a title='Edit URL' class='LinkCommandRound' id='ctl00_contentArea_WebUserControl01_lnkUploadFile' href='#'><span>Upload File</span></a>" +
    " </td>" +
    " </tr>" +
    " </tbody></table>" +
    " <div style='display: none;' id='ctl00_contentArea_WebUserControl01_EditUrlDialog'>" +
    " <div class='DialogBody'>" +
    " <h1>" +
    " URL to Analyze" +
    " </h1>" +
    " <input type='text' class='FormText' style='width: 325px;' id='ctl00_contentArea_WebUserControl01_EditUrlDialog_txtUrl' name='ctl00$contentArea$WebUserControl01$EditUrlDialog$txtUrl'/>" +
    " <div class='DialogButtons'>" +
    " <ul>" +
    " <li><a title='Ok' href='#' class='LinkCommandRound CancelButton ClosePopUp'>Cancel</a></li>" +
    " <li><a title='Ok' class='LinkCommandRound OkButton' id='ctl00_contentArea_WebUserControl01_EditUrlDialog_lnkOkDialog' href='#'>Ok</a></li>" +
    " </ul>" +
    " </div>" +
    " </div>" +
    "</div>" +
    "</div>" +
    " </div>" +
    " <div id='SubReportSelector'>" +
    " <p>" +
    " Show Reports" +
    " </p>" +
    " <div id='SubReports'><ul>" +
    " </ul></div>" +
    " <div class='DoClear'>" +
    " </div>" +
    " <a title='Run' id='lnkRun' href='javascript:void(0);' class='LinkCommandRound Big'><span>" +
    " Run</span></a>" +
    " </div>" +
    " </div>" +
    " <div class='ReportsSection'>" +
    " </div>";
    $("#helperContext_PageSelectionToolTest").html(reportSelectorHtml);
    this.ps = new PageSelectionTool("pInfo");
    this.reportsToRenderCounter = 8;
    var reportsToRenderText = "";
    for (var i = 0; i < this.reportsToRenderCounter; i++) {
        reportsToRenderText += "<div id='IdReport" + i + "' class='Report' report_name='Test report ' " + i + "></div>";
    }
    $(".ReportsSection").html(reportsToRenderText);
    console.log('setup for PageSelectionToolTest');
}

PageSelectionToolTest.prototype.successfulSetFileSelectorManager = function(){
    //We send the file selector as file selector parameter
    this.ps.setFileSelectorManager(FileSelectorManager.GetInstance());
    //Assert there is no file selector registered in the page selection tool
    $.assertNotNull('The file selector not was registered', this.ps.FileSelectorManager);
}

PageSelectionToolTest.prototype.nullFileSelectorInSetFileSelectorManager = function(){
    var wasAnException = false;
    try {
        //We send a null value as file selector parameter
        this.ps.setFileSelectorManager(null);
    } 
    catch (ex) {
        wasAnException = true;
    }
    $.assertTrue("An exception was expected", wasAnException);
}

PageSelectionToolTest.prototype.successfulRenderChecks = function(){
    ReportManager.GetInstance().findReportsInPage();
    //We send the orden to render the checks according to the reports declared
    this.ps.renderChecks(ReportManager.GetInstance().reports);
    //Assert there are as many reports as were declared in the page (we declare 4)
    $.assertEquals(this.reportsToRenderCounter,  $(".ReportTrigger").length);
}

PageSelectionToolTest.prototype.nullReportParameterRenderChecks = function(){
    //Let's clear the reports and subreports section
	$(".ReportsSection").empty();
	$("#SubReports ul").empty();
	//Then, we search for the reports in the page
	ReportManager.GetInstance().findReportsInPage();
	//Do the assert
    $.assertEquals(0,  $(".ReportTrigger").length);
}

PageSelectionToolTest.prototype.tearDown = function(){
    console.log('teardown for PageSelectionToolTest');
    $('#helperContext_PageSelectionToolTest').remove();
    ReportManager._Instance = null;
}

function FileSelectorManagerTest(){
    Test.call();
}

FileSelectorManagerTest.prototype = new Test();

FileSelectorManagerTest.prototype.setup = function(){
    console.log('setup for FileSelectorManagerTest');
}

FileSelectorManagerTest.prototype.notNullGetInstance = function(){
    $.assertNotNull('TestGetInstance', FileSelectorManager.GetInstance());
}

FileSelectorManagerTest.prototype.tearDown = function(){
    console.log('teardown for FileSelectorManagerTest');
}
