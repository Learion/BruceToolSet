/**
 * Initialize a Page to show Reports based on the checks that the user selects.
 */
function InitializeReportPage(pageSelectionDivId, subReportContainerId, reportServicesUrl, deleteHandlerUrl, uploadHandlerUrl, inlineMessageDiv, _performPreClickJobs)
{
	
    //to allow the execution of some async tasks. 
    //If you want to perform some Ajax job before the click Event on the Run Reports do its job use this function
    _performPreClickJobs = _performPreClickJobs || function (callback, entries, rm) {
        if (typeof(callback) == 'function') callback();
    };
    
    var rm = ReportManager.GetInstance();    
    rm.handlerUrl = reportServicesUrl;    
	rm.setAsyncManager(new $.AsyncManager.Manager());	
    rm.findReportsInPage();
	
	var ps = new PageSelectionTool(pageSelectionDivId);
    
    ps.SetDeletionHandler(deleteHandlerUrl);
    
	
    rm.onAsyncError = function(event, request, settings)
    {
        $.showInlineMessage($.byId(inlineMessageDiv), $.getResourceString('ErrorInLine', 'Oops, Error while trying to receive the report data.'), 
        {
           sticky: true
        });
    };	
    
	rm.onAsyncWorkComplete = function () {
		ps.setRunAction();
		//Expands all sub-reports that are active when the ajax is done.
		// AGL 7/14/09
		rm.doExpandAll();
	};
	

    var fsm = FileSelectorManager.GetInstance();
    
    fsm.setUploadUrl(uploadHandlerUrl);
    
    ps.setFileSelectorManager(fsm);
    
    ps.renderChecks(rm.reports);
    ps.onExpandCollapseClick = function(e)
    {
        if (e && e.actionToDo) 
        {
            if (e.actionToDo == 'Collapse') 
            {
                rm.doCollapseAll();
            }
            else 
            {
                rm.doExpandAll();
            }
        }
    };
    
    ps.onCheckClicked = function(reportId, display)
    {
        rm.notifyClickToReport(reportId, display);
    };
    
    ps.onRunClicked = function(entries)
    {
        _performPreClickJobs(function() {
		    $.hideInLineMessage($.byId(inlineMessageDiv));
		    ps.setAbortAction();
            $.byId(subReportContainerId).show();
            rm.runReports(entries);
        }, entries, rm);
    };
    
    ps.onAbort = function()
    {
        rm.abortReports();
        $.byId(subReportContainerId).hide();
		ps.setRunAction();
    };    
}


/**
 * @author rriojas
 */
/** *********************************************************************************************************************
 * FileSelectorManager is class that will hold a list of UI Widgets that allow the user to enter
 * an url entry or to select a file to upload it has internally an linked list to be able to call
 * an event when the last file to upload has been processed
 */
function FileSelectorManager()
{

    //Constructor - Begin
    //closure
    var _me = this;
    //pointer to the first FileSelector node
    var _first = null;
    //the url of the Handler to process uploads 
    //this is override in the Page that call the FileSelector Manager
    var _fileUploadUrl = "../../../Handler/Upload.ashx";
    
    var _swfUrl = "scripts/Controls/SwfUpload/swfupload.swf";
    var _maxFileSize = '2048';
    var _fileTypes = "*.html;*.htm;*.txt";
    var _fileTypesDescription = "html files, text files";
    
    _me.onEnterPresedInLast = function() {};
    //Constructor - End
    
    
    _me.setUploadUrl = function(value)
    {
        _fileUploadUrl = value;
    };
    
    /**
     * Set Max File Size
     * @param {Object} val
     */
    _me.setMaxSize = function(val)
    {
        _maxFileSize = val;
    };
    /**
     * get Max Size
     */
    _me.getMaxSize = function()
    {
        return _maxFileSize;
    };
    
    
    function _checkEnter(fSelector) {
        if (fSelector.next) {
            fSelector.next.setFocus();
        }
        else {
            _me.onEnterPresedInLast();
        }
    }
    
    /**
     * Create FileSelectors finding them in the Page and register them in the FileSelectors
     */
    _me.findFileSelectorsInPage = function()
    {
        var previous = null;
        $('.FileSelector').each(function(i)
        {
            var fs = new FileSelector(this.id, _fileUploadUrl, _swfUrl, _maxFileSize, _fileTypes, _fileTypesDescription);
            fs.onEnterPressed = _checkEnter;
            if (_first === null) 
            {
                _first = fs;
            }
            if (previous) 
            {
                previous.next = fs;
            }
            previous = fs;
        });
    };
    /**
     * Process the Uploads if any is required
     * @param {Function} callback a function to call when all the files has been uploaded
     */
    _me.processUploads = function(callback)
    {
        var node = _first;
        node.uploadFile(callback);
    };
    /**
     * Cancel the Pending Uploads
     */
    _me.cancelUploads = function()
    {
        try 
        {
            var node = _first;
            while (node) 
            {
                node.stopUpload();
                node = node.next;
            }
        } 
        catch (ex) 
        {
            alert("[Error] : " + ex.message);
            console.log("[Error] : " + ex.message);
        }
    };
	
	_me.blockUI = function () {
		try 
        {
            var node = _first;
            while (node) 
            {
                node.blockUI();
                node = node.next;
            }
        } 
        catch (ex) 
        {
            alert("[Error] : " + ex.message);
            console.log("[Error] : " + ex.message);
        }
	};
	
	_me.unBlockUI = function () {
		try 
        {
            var node = _first;
            while (node) 
            {
                node.unBlockUI();
                node = node.next;
            }
        } 
        catch (ex) 
        {
            alert("[Error] : " + ex.message);
            console.log("[Error] : " + ex.message);
        }
	};
    
    /**
     * set the focus to the first FileSelector's edit button
     */
    _me.setFocusToTheFirstElement = function()
    {
        _first.setFocus();
    };
    /**
     * retrieve an historical list of files that has been uploaded
     */
    _me.getFilesUploaded = function()
    {
        var arr = [];
        var node = _first;
        while (node) 
        {
            var temparr = node.getFilesUploaded();
            if (temparr) 
            {
                arr = arr.concat(temparr);
            }
            node = node.next;
        }
        return arr;
    };
    
    /**
     * return all the FileEntries to be processed
     */
    _me.getFileEntries = function()
    {
        var arr = [];
        var node = _first;
        while (node) 
        {
            if (node.hasFile()) 
            {
                arr.push(node.getFileEntry());
            }
            node = node.next;
        }
        return arr;
    };
    /**
     * is there at least one URL or file to upload?
     */
    _me.atLeastOneUrlToProcess = function()
    {
        var node = _first;
        while (node) 
        {
            if (node.hasFile()) 
            { return true; }
            node = node.next;
        }
        return false;
    };
    
    /**
    * is there at least one file to be uploaded?
    */
    _me.atLeastOneFileToUpload = function () {
        var node = _first;
        while (node) 
        {
            if (node.hasToUpload()) 
            { return true; }
            node = node.next;
        }
        return false;        
    }
	
	
    
}

/**
 * FileSelectorManager Static Instance
 */
FileSelectorManager._Instance = null;

/**
 * Retrieves the Instance for the FileSelectorManager Class
 */
FileSelectorManager.GetInstance = function()
{
    if (FileSelectorManager._Instance === null) 
    {
        FileSelectorManager._Instance = new FileSelectorManager();
    }
    return FileSelectorManager._Instance;
};


/**
 * FileSelector is a Widget that allows to the user to enter a url or to choose a file in the pc to upload.
 * @param {String} str_id
 */
function FileSelector(str_id, uploadUrl, swfUrl, maxFileSize, fileTypes, fileTypeDescription)
{

    //Constructor - Begin
    
    var _me = this;
    var _div = $.byId(str_id);
    
    var _txtURLFile = _div.findById('txtUrlFile');
    //var _lnkEditUrl = _div.findById('lnkEditUrl');
    var _lnkUploadFile = _div.findById('lnkUploadFile');	
    
    var _EditUrlDialog = null;
    var _hasToUpload = false;
    var _innerUploadHandler = null;
    
    _me.onEnterPressed = function (fs) {};
    
    var _fileEntry = 
        {
            Id: -1,
            Url: null,
            DisplayUrl: null
        };



    console.log('title of the _lnkUploadFile ' + _lnkUploadFile.attr('title'));
    	
    var _uploadedFiles = [];
    
    if (_lnkUploadFile.length > 0) 
    {
        var _swfu = new SWFUpload(
            {
                upload_url: uploadUrl, // Relative to the SWF file
                file_size_limit: maxFileSize,
                file_types: fileTypes,
                file_types_description: fileTypeDescription,
                file_upload_limit: "0", //0 = unlimited
                file_queue_limit: "2", //2 files maximum in queue      
                button_placeholder_id : _div.findById("uploadFile").get(0).id,
				
				button_width: 100,
				button_height: 30,
				button_text : '<span class="label">' + _lnkUploadFile.attr('title') + '</span>',
				button_text_style: '.label { font-family:"Lucida sans", Arial, Helvetica, sans-serif; font-size:13px; color: #0C5EAC; text-align:center; } ',
				button_text_top_padding: 2,
				button_text_left_padding: 0,
				button_window_mode: SWFUpload.WINDOW_MODE.TRANSPARENT,
				button_cursor: SWFUpload.CURSOR.HAND,
				
                //Handlers - Begin
                file_queue_error_handler: _fileQueueError,
                file_queued_handler: _fileQueued,
                file_dialog_complete_handler: _fileDialogComplete,
                upload_start_handler: _uploadStart,
                upload_progress_handler: _uploadProgress,
                upload_error_handler: _uploadError,
                upload_success_handler: _uploadSuccess,
                upload_complete_handler: _uploadComplete,
                //Handlers - End
                
                flash_url: swfUrl // Relative to this file       
            });
    }
    _initializeUI();
    
    //Constructor - End	
    
    //Private Methods - Begin
    /**
     * raised when the FileDialog is closed
     * @param {Number} numFilesSelected 	number of files selected
     * @param {Number} numFilesQueued 		number of files put to queue
     */
    function _fileDialogComplete(numFilesSelected, numFilesQueued)
    {
        //console.log($.stringFormat('file Dialog Complete Event: numFilesSelected : {0}, numFilesQueued : {1}', numFilesSelected, numFilesQueued));
    }
    
    /**
     * raised when a file has been successfuly uploaded
     * @param {Object} file 		the file object
     * @param {Object} serverData	the response from the server
     */
    function _uploadSuccess(file, serverData)
    {
        try 
        {
            //console.log('Server Data : ' + serverData);
            
            if (_fileEntry.Id == file.id) 
            {
            
                var strOutput = serverData || "";
                
                var tokens = strOutput.split('|');
                
                if (tokens.length !== 2) 
                {                throw (
                    {
                        message: "no tokens have been found after a successful upload"
                    }); }
                _fileEntry.Url = tokens[1];
                //TODO: Find a better way to avoid the user try to upload and run the reports more than 10 times				
                if (_uploadedFiles.length < 10) 
                {
                    _uploadedFiles.push(tokens[0]);
                }
            }
            
            if (typeof(_innerUploadHandler) == 'function') 
            {
                _innerUploadHandler();
            }
            
        } 
        catch (ex) 
        {
            console.log("[Exception] : " + ex.message);
            alert('Error Uploading File : ' + ex.message);
        }
    }
    /**
     * raised when all uploads has been completed
     * @param {Object} file the file object
     */
    function _uploadComplete(file)
    {
        try 
        {
            if (_swfu.getStats().files_queued === 0) 
            {
                _hasToUpload = false;
            }
        } 
        catch (ex) 
        {
            console.log("[Exception] : " + ex.message);
            alert('Error Uploading File : ' + ex.message);
        }
        
    }
    
    function _uploadError(file, errorCode, message)
    {
        switch (errorCode)
        {
            case SWFUpload.UPLOAD_ERROR.HTTP_ERROR:
                console.log("Upload Error: " + message);
                console.log("Error Code: HTTP Error, File name: " + file.name + ", Message: " + message);
                alert("Error Code: HTTP Error, File name: " + file.name + ", Message: " + message);
                break;
            case SWFUpload.UPLOAD_ERROR.MISSING_UPLOAD_URL:
                console.log("Configuration Error");
                console.log("Error Code: No backend file, File name: " + file.name + ", Message: " + message);
                alert("Error Code: No backend file, File name: " + file.name + ", Message: " + message);
                break;
                
            case SWFUpload.UPLOAD_ERROR.UPLOAD_FAILED:
                console.log("Upload Failed.");
                console.log("Error Code: Upload Failed, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                alert("Error Code: Upload Failed, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                break;
            case SWFUpload.UPLOAD_ERROR.IO_ERROR:
                console.log("Server (IO) Error");
                console.log("Error Code: IO Error, File name: " + file.name + ", Message: " + message);
                alert("Error Code: IO Error, File name: " + file.name + ", Message: " + message);
                break;
            case SWFUpload.UPLOAD_ERROR.SECURITY_ERROR:
                console.log("Security Error");
                console.log("Error Code: Security Error, File name: " + file.name + ", Message: " + message);
                alert("Error Code: Security Error, File name: " + file.name + ", Message: " + message);
                break;
            case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:
                console.log("Upload limit exceeded.");
                console.log("Error Code: Upload Limit Exceeded, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                alert("Error Code: Upload Limit Exceeded, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                break;
            case SWFUpload.UPLOAD_ERROR.SPECIFIED_FILE_ID_NOT_FOUND:
                console.log("File not found.");
                console.log("Error Code: The file was not found, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                alert("Error Code: The file was not found, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                break;
            case SWFUpload.UPLOAD_ERROR.FILE_VALIDATION_FAILED:
                console.log("Failed Validation.  Upload skipped.");
                console.log("Error Code: File Validation Failed, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                alert("Error Code: File Validation Failed, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                break;
            case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
                console.log("Upload Cancelled");
                //alert("Cancelled Here");
                break;
            case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
                console.log("Stopped");
                break;
            default:
                console.log("Unhandled Error: " + error_code);
                console.log("Error Code: " + errorCode + ", File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                alert("Error Code: " + errorCode + ", File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
                break;
        }
    }
    /**
     * raised when the file is about to be uploaded
     * @param {Object} file the file object reference
     */
    function _uploadStart(file)
    {
        //console.log('start uploading file : file.id -> ' + file.id);
    }
    /**
     * raised while the file is being uploaded
     * @param {Object} file 		the file object
     * @param {Object} bytesLoaded	current bytes loaded
     * @param {Object} bytesTotal	bytes total
     */
    function _uploadProgress(file, bytesLoaded, bytesTotal)
    {
        try 
        {
            var percent = Math.ceil((bytesLoaded / bytesTotal) * 100);
            //console.log("Uploading file.id -> " + percent);
        } 
        catch (ex) 
        {
            alert(ex.message);
        }
    }
    
    
    
    /**
     * raises when a file has benn selected using the OpenFileDialog
     * @param {Object} file the file object reference
     */
    function _fileQueued(file)
    {
        try 
        {
        
            if (_fileEntry.Id != -1) 
            {
                //only one file in queue!!!
                _swfu.cancelUpload(_fileEntry.Id);
            }
            
            //console.log('set to File ' + file.name)
            _setToFile(file);
        } 
        catch (ex) 
        {
            alert(ex.message);
        }
        
    }
    
    /**
     * raised when an error happens trying to queue the file
     * @param {Object} file 		the file object reference
     * @param {Object} errorCode	the error code
     * @param {Object} message		the message with the error description
     */
    function _fileQueueError(file, errorCode, message)
    {
        try 
        {
            var errorName = "";
            switch (errorCode)
            {
                case SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED:
                    errorName = "too many files in queue";
                    break;
                case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
                    errorName = "the file has zero byte length.";
                    break;
                case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
                    errorName = "the file is too big";
                    break;
                case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
                    errorName = "invalid file type";
                    
                    break;
                default:
                    $.showMessage(message, 
                        {
                            icon: $.MessageIco.Alert
                        });
                    break;
            }
            
            if (errorName !== "") 
            {
                //alert(errorName);
                $.showMessage(errorName);
                return;
            }
            
        } 
        catch (ex) 
        {
            console.log('[Error] : ' + ex.message);
            alert(ex.message);
        }
    }
    
    
    /**
     * set the file Entry to a file
     * @param {Object} file object reference
     */
    function _setToFile(file)
    {
		
        if (_fileEntry.Id != -1) 
        {
            //only one file in queue!!!		
            _swfu.cancelUpload(_fileEntry.Id);
        }
        
        _fileEntry.Id = file.id;
        _fileEntry.Url = null; //this url will be fulfilled with the url from the uploadCompleted event
        _fileEntry.DisplayUrl = file.name;
        
        _txtURLFile.get(0).value = file.name;
        
        _hasToUpload = true;        
		/*
		_div.find('span.MessageIndicator.selected').removeClass('selected');
		_div.find('span.MessageIndicator.FileUpload').addClass('selected');*/
    }
    
    /**
     * set file Entry to an URL
     * @param {Object} value
     */
    function _setToInternetUrl(value)
    {
        if (_fileEntry.Id != -1) 
        {
            //only one file in queue!!!
            _swfu.cancelUpload(_fileEntry.Id);
        }
        _txtURLFile.get(0).value = value;
        _hasToUpload = false;
        _fileEntry.Id = -1;
        _fileEntry.Url = (value !== '') ? value : null;
        _fileEntry.DisplayUrl = (value !== '') ? value : null;
		
		/*_div.find('span.MessageIndicator.selected').removeClass('selected');
		_div.find('span.MessageIndicator.Url').addClass('selected');*/
    }
    
    /**
     * Initializes the UI components
     */
    function _initializeUI()
    {
		var validateIt = function (callback) {								
				//_setToInternetUrl(_txtURLFile.val());
				var url = _txtURLFile.val();        
		        if (url.search('http://|https://') !== 0 && (url !== '')) 
		        {
		            url = "http://" + url;
		        }
		        if (_validateUrl(url) || url === '') 
		        {
		            _setToInternetUrl(url);
		            if (typeof(callback) == 'function' ) callback();		            
		        }
		        else 
		        {
		            var uriNotValidMessage = $.getResourceString('UriNotValid', 'The entry is not a valid. Please enter a valid URL to proceed');
		            $.showMessage(uriNotValidMessage, 
		                {
		                    icon: $.MessageIco.Alert,
		                    onHide: function()
		                    {
		                        _txtURLFile.select().focus();
		                    }
		                });
		        }
		        //return false;
			};
        _txtURLFile
			.change(validateIt)
			.enterPressed(function (e,args) { validateIt(function () { _me.onEnterPressed(_me); }); return false; });
        
		_txtURLFile.val("");

		//to avoid the default work of the upload Link
		_lnkUploadFile.click(function () { return false;  } );
        
    }
    
    /**
     * raised when the OkButton of the Dialog has been Clicked
     * @param {Object} e the Event object
     */
    function _onOkClick(e)
    {
        _EditUrlDialog.getTxtUrl().blur();
        var url = _EditUrlDialog.getTxtUrlValue();        
        if (url.search('http://|https://') !== 0 && (url !== '')) 
        {
            url = "http://" + url;
        }
        if (_validateUrl(url) || _EditUrlDialog.getTxtUrlValue() === '') 
        {
            _setToInternetUrl(url);
            
            _EditUrlDialog.hideDialog();
        }
        else 
        {
            var uriNotValidMessage = $.getResourceString('UriNotValid', 'The entry is not a valid. Please enter a valid URL to proceed');
            $.showMessage(uriNotValidMessage, 
                {
                    icon: $.MessageIco.Alert,
                    onHide: function()
                    {
                        _EditUrlDialog.getTxtUrl().select().focus();
                    }
                });
        }
        return false;
    }
    
    /**
     * function to validate an URL
     * @param {Object} url the url to be validated
     */
    function _validateUrl(url)
    {
        var v = new RegExp();
        v.compile("^[A-Za-z]+://[A-Za-z0-9-_]+\\.[A-Za-z0-9-_%&\?\/.=]+$");
        return v.test(url);
    }
    
    /**
     * raised when the Dialog is shown
     */
    function _onShowDialog()
    {
        _EditUrlDialog.setTxtUrlValue(_txtURLFile.get(0).value);
    }
    
    /**
     * start the upload process
     * @param {Object} callback the function to call when the upload process has been completed
     */
    function _doUpload(callback)
    {
        _innerUploadHandler = callback;
        _swfu.startUpload();
    }
    
    
    //Private Methods - End
    
    //Public Methods - Begin
    
	
	_me.blockUI = function () {
		_txtURLFile.attr({ disabled: "disabled" });
		_lnkUploadFile.find('object').hide();
		_lnkUploadFile.append('<span class="label disabled">' + _lnkUploadFile.attr('title') + '</span>');
	};
	
	_me.unBlockUI = function () {
		_txtURLFile.attr({ disabled: "" });
		_lnkUploadFile.find('object').show();
		_lnkUploadFile.find('span.label').remove();
	};
	
    /**
     * Cancel the Current Upload and requeue the file to upload
     */
    _me.stopUpload = function()
    {
        _swfu.stopUpload();
    };
    
    /**
     * has a file or url selected?
     */
    _me.hasFile = function()
    { 
        
        
        //test to see if this works. It does! TODO: NEED TO FIX!!!!!!
        if(_txtURLFile.get(0) == undefined) {
            return true;
        }
        else {        
            return _txtURLFile.get(0).value !== '' && _fileEntry.DisplayUrl !== ''; 
        }
    };
    
    /**
    * has to upload?
    */
    _me.hasToUpload = function () {
        return _hasToUpload
    };
    
    /**
     * retrieve current file entry
     */
    _me.getFileEntry = function()
    { return _fileEntry; };
    
    /**
     * start the file upload process and start the next node file upload process if next exists
     * @param {Object} callback the function to call when the fileUpload has been completed
     */
    _me.uploadFile = function(callback)
    {
        var innerCallback = function()
        {
            if (_me.next) 
            {
                _me.next.uploadFile(callback);
            }
            else 
            {
                callback();
            }
        };
        if (_hasToUpload) 
        {
            _doUpload(innerCallback);
        }
        else 
        {
            innerCallback();
        }
    };
    
    /**
     * set the focus in EditURL
     */
    
	_me.setFocus = function()
    {
        //_lnkEditUrl.focus();
		console.log('TODO: Set focus on the txt_entry');
    };
	
    /**
     * retrieve the list of historical uploaded files
     */
    _me.getFilesUploaded = function()
    { return _uploadedFiles; };
    
    //Public Methods - End

}

/**
 * PageSelectionTool represents the Main UI for the Selection of the page and the Reports to be run
 * @param {Object} str_id the id of the div that will contain the ui
 */
function PageSelectionTool(str_id)
{
    var _me = this; //closure	    
    var pstObj = $.byId(str_id);
    
    if (!pstObj.length) 
    { return; }
    
    var _lnkRun = pstObj.find('#lnkRun');
    var _objChecks = {}; //Asociative Array	
    var str_ulForChecks = str_id + ' #SubReports ul'; //ul element to hold the checkboxes    	    
    var _checkNone = pstObj.find('#checkNone');
	var _chekAll = pstObj.find("#checkAll");
    
    
	
	var _clickAction;		
    
    //The Server Side ashx used to clean the files uploaded
    _deletionHandler = null;//'Handler/CleanFiles.ashx';
    _me.SetDeletionHandler = function(val)
    {
        if (val) 
        {
            _deletionHandler = val;
            //set a handler to try to delete the temp files.
            $(window).bind('unload', _clearTemporaryFiles);
        }
    };            
    
    // Public Events - Begin
    _me.onCheckClicked = null;
    _me.onRunClicked = null;
    _me.onAbort = null;
    _me.FileSelectorManager = null;
    
    //Public Events - End                
    
    
    //Private Methods - Begin
    /**
     * Clean the temporary uploaded files
     */
    function _clearTemporaryFiles()
    {
        var str_files = "";
        var entries = _me.getFilesUploaded();
        for (var ix = 0; ix < entries.length; ix++) 
        {
            if (entries[ix]) 
            {
                try 
                {
                
                    str_files += entries[ix] + '|';
                } 
                catch (ex) 
                {
                    console.log("[Error]" + ex.message);
                }
            }
        }
        
        //console.log("str_files = " + str_files);
        
        if (str_files !== "") 
        {
            try 
            {
                $.post(_deletionHandler, 
                    {
                        action: 'DeleteTempFiles',
                        FileNames: str_files
                    });
            } 
            catch (ex) 
            {
                console.log('[Error] : Error trying to delete temp files' + ex.message);
            }
        }
    }
    
    /**
     * Check at least one check checked
     */
    function _atLeastOneChecked()
    {
        var oneChecked = false;
        $.byId(str_ulForChecks).find('input[type=checkbox]').each(function(i)
        {
            if (this.checked) 
            {
                oneChecked = true;
                return false;
            }
        });
        return oneChecked;
    }
    /**
     * checks if there is at least one URL to process
     */
    function _urlSelected()
    { 
        return _me.FileSelectorManager.atLeastOneUrlToProcess(); 
    }
    /**
     * abort the upload process
     * @param {Object} e
     */
    function _abortUploads(e)
    {
        if (confirm('Abort Uploads?')) 
        {
            _me.FileSelectorManager.cancelUploads();
            $.hideMessage();
        }
        return false;
    }
    function _setupAbortUploads(panel)
    {
        panel.find('a.abortx').click(_abortUploads);
        _me.FileSelectorManager.processUploads(_allFilesUpload);
    }
    
    function _abortReports(e)
    {
        if (confirm($.getResourceString('AbortReportsLoadingQuestion', 'Abort Reports loading?'))) 
        {
            //console.log('reports aborted!!!!');
            if (typeof(_me.onAbort) == 'function') 
            {
                _me.onAbort();
            }
        }
        return false;
    }
    
    function _allFilesUpload()
    {
        $.hideMessage();
        
        if (typeof(_me.onRunClicked) == 'function') 
        {
            _me.onRunClicked(_me.getUrls());
        }
        //console.log('begin the ReportManager Works');
    }
	
	function _doActionRun(e) {
		if (!_urlSelected()) 
        {
            $.showMessage($.getResourceString('UrlNotEntered', 'Please enter a url or select a file to upload'), 
                {
                    icon: $.MessageIco.Alert
                });
            return false;
        }
        if (!_atLeastOneChecked()) 
        {
            $.showMessage($.getResourceString('NoReportSelected', 'Please choose at least one report'), 
                {
                    icon: $.MessageIco.Alert
                });
            return false;
        }
        
        //all fine, process the uploads first if ther is any
        if (_me.FileSelectorManager.atLeastOneFileToUpload()) {
            $.showMessage('<table style="width:100%"><tr><td style="width:70%">' + $.getResourceString('ProcessingUploads', 'Processing Uploads') + '...</td><td valign="top"><a class="LinkCommandRound ClosePopUp abortx" href="javascript:void(0)"><span>' + $.getResourceString('Cancel', 'Cancel') + '</span></a></td></tr></table>', 
            {
                closeable: false,
                icon: $.MessageIco.Info,
                useEscToHide: false,
                onShow: _setupAbortUploads
            });    
        }
        else {
            _allFilesUpload();
        }
        
	}
	
	function _doActionAbort(e) {
		_abortReports(e);
	}
    function _doRun(e)
    {
		switch(_clickAction) {
			case "Run" : _doActionRun(e);
			break;
			case "Abort" : _doActionAbort(e);
			break;
			default: console.log('Not valid Action');
			break;
		}        
        return false;
    }               
    
  /*
  _me.evaluateCheckState = function(changeState)
    {
        if (_checkUncheck.initialState == 'CheckNone') 
        {
            _doCheckNone(changeState);
            _checkUncheck.initialState = 'CheckAll';
        }
        else 
        {
            _doCheckAll(changeState);
            _checkUncheck.initialState = 'CheckNone';
        }
        
    };
*/
    function _doCheckAll()
    {     
        for (var key in _objChecks) 
        {
        	_objChecks[key].attr("checked", true);
            _me.raiseClickOn(_objChecks[key].get(0).id);
		}
        
    }
    
    function _doCheckNone()
    {        
        for (var key in _objChecks) 
        {
        	_objChecks[key].attr("checked", false);
            _me.raiseClickOn(_objChecks[key].get(0).id);
        }        
    }
    
    //Private Methods - End    
    _me.setFileSelectorManager = function(fsm)
    {
        _me.FileSelectorManager = fsm;
        _me.FileSelectorManager.findFileSelectorsInPage();
        _me.FileSelectorManager.setFocusToTheFirstElement();
        _me.FileSelectorManager.onEnterPresedInLast = function() { _lnkRun.trigger('click'); };
    };
	
	
	_me.setAbortAction = function () {
		_clickAction = "Abort";
		var text = _lnkRun.find('span').attr('state02_text');
		_lnkRun.find('span').html(text);		
		if (_me.FileSelectorManager) 
		{
			_me.FileSelectorManager.blockUI();
		}
	};
	
	_me.setRunAction = function () {
		_clickAction = "Run";
		var text = _lnkRun.find('span').attr('state01_text');
		_lnkRun.find('span').html(text);	
		 if (_me.FileSelectorManager) 
		 {
		 	_me.FileSelectorManager.unBlockUI();
		 }
	};
	
    _me.renderChecks = function(Reports)
    {
        for (var key in Reports) 
        {
            var rep = Reports[key];
            if (!_objChecks[key]) 
            {
                $.byId(str_ulForChecks).append("<li><label for='chk_" + rep.ReportId + "'><input checked='checked' report_id='" + rep.ReportId + "' class='ReportTrigger' type='checkbox' id='chk_" + rep.ReportId + "' name='checkbox' value='checkbox' /><span>" + rep.ReportCheckName + "</span></label></li>");
                _objChecks[key] = $.byId("chk_" + rep.ReportId).bind('click', _me.handleCheckClick);
            }
            
        }
    };
    
/*
    _me.checkAllOrNoneSelected = function()
    {
        var AllSelected = true;
        var NoneSelected = false;
        for (var key in _objChecks) 
        {
            if (!_objChecks[key].attr('checked')) 
            {
                AllSelected = false;
                break;
            }
        }
        if (!AllSelected) 
        {
            NoneSelected = true;
            for (key in _objChecks) 
            {
                if (_objChecks[key].attr('checked')) 
                {
                    NoneSelected = false;
                    break;
                }
            }
        }
        
        if (AllSelected || NoneSelected) 
        {
            if (AllSelected) 
            {
                _checkUncheck.initialState = 'CheckAll';
            }
            if (NoneSelected) 
            {
                _checkUncheck.initialState = 'CheckNone';
            }
            _me.evaluateCheckState();
        }
        
    };

*/    _me.handleCheckClick = function(e)
    {
        if (typeof(_me.onCheckClicked && typeof(_me.onCheckClicked) == 'function')) 
        {
            var target = $.byId(e.target.id);
            //_me.checkAllOrNoneSelected();
            _me.onCheckClicked(target.attr('report_id'), target.attr('checked'));
        }
    };
    
    _me.raiseClickOn = function(str_id)
    {
        if (typeof(_me.onCheckClicked && typeof(_me.onCheckClicked) == 'function')) 
        {
            var target = $.byId(str_id);
            _me.onCheckClicked(target.attr('report_id'), target.attr('checked'));
        }
        
    };
    
    
    _me.getFilesUploaded = function()
    { return _me.FileSelectorManager.getFilesUploaded(); };
    _me.getUrls = function()
    { return _me.FileSelectorManager.getFileEntries(); };
    
    
	function _initializeUI()
    {
        _lnkRun.bind('click', _doRun);
		_chekAll.click(function (e) {
			_doCheckAll();
			return false;	
		});
		
        _checkNone.click(function(e)
        {         
			_doCheckNone();
            return false;
        });
        
        var _expandAll = $('#expandAll');
        var _collpaseAll = $('#collapseAll');
        
        _expandAll.click(function()
        {
            if (typeof _me.onExpandCollapseClick == 'function') 
            {
                _me.onExpandCollapseClick(
                    {
                        actionToDo: 'Expand'
                    });
            }
            return false;
        });
        
        _collpaseAll.click(function()
        {
            if (typeof _me.onExpandCollapseClick == 'function') 
            {
                _me.onExpandCollapseClick(
                    {
                        actionToDo: 'Collapse'
                    });
            }
            return false;
        });
		_me.setRunAction();
    }
    
    _initializeUI();
}

/**
 * Control and manage the Reports in the Page
 */
function ReportManager()
{
    var _me = this; //closure
    _me.reports = {};
	
	_me.AsyncManager = null;
	
    _me.seviceUrlPattern = "{0}?URIPage={1}&KindOfReport={2}&jsoncallback=?";
  	_me.setAsyncManager = function (aManager) {
		_me.AsyncManager = aManager;
	};
	
	
	_me.onAsyncWorkComplete = null;
	
    _me.doCollapseAll = function()
    {
        //_ExpandCollapse.html("<span>[ Expand All ]</span>")
        for (var reportId in _me.reports) 
        {
            var report = _me.reports[reportId];
            if (report) 
            {
                report.collapse();
            }
        }
    };
    
    _me.doExpandAll = function()
    {
        //_ExpandCollapse.html("<span>[ Collapse All ]</span>")
        for (var reportId in _me.reports) 
        {
            var report = _me.reports[reportId];
            if (report) 
            {
                report.expand();
            }
        }
    };
    
    _me.handlerUrl = null;//'Handler/GetReport.ashx';
    _me.notifyClickToReport = function(reportId, display)
    {
        _me.reports[reportId].setVisible(display);
    };
    
    
    _me.registerReport = function(ReportObject)
    {
        var reportId = ReportObject.ReportId;
        _me.reports[reportId] = ReportObject;
      
    };
    
    _me.findReportsInPage = function()
    {
        $('.Report').each(function(i)
        {
            var report = new Report(this.id, new ArrayRendererManager());
            _me.registerReport(report);
        });
    };
    _me.abortReports = function()
    {
        if (_me.AsyncManager) {
			_me.AsyncManager.stopTasks();
		}
    };	
    _me.runReports = function(entries)
    {					
        var urls = entries[0].Url; //get the url of the first entry		
      	var reportArr = [];
		
		_me.AsyncManager.onAsyncWorkComplete = function () {
			if (typeof _me.onAsyncWorkComplete == 'function') 
			{
				_me.onAsyncWorkComplete();
			}
		};

		$.each(_me.reports, function (key, val) {
			reportArr.push(val);
		});
		
		reportArr.sort(function(a, b)
        {
        	if (a.isVisible() < b.isVisible()) 
            { return 1; }
            if (a.isVisible() > b.isVisible()) 
            { return -1; }
            return 0;
        });
		
		
		var onSuccess = function (o, report) {
			if (typeof(window[report.onDataReceived]) == 'function') 
	        {
	            window[report.onDataReceived](report, o.result);
	        }
	        report.ReportLoaded = true;
	        report.hideLoading();			
			console.log('global OnSuccess');			
		};
		
		var errorNotified = false;
		
		var onError = function (o, report) {
			if (!errorNotified) {
				if (typeof _me.onAsyncError == 'function' && report.isVisible()) {					
					_me.onAsyncError();
					errorNotified = true;
				}
			}
			report.notifyError(o.result.status || o.result.error);		
		};
		
		var onAbort = function (report) {
			try {
				report.abort();
			}
			catch(e) {
			    console.log('ERROR on abort');
			}
		};
		
		var onBeforeCall = function (evt) {
            try {
					report = evt.report;
                    if (typeof(window[report.onBeforeAjaxCall]) == 'function') 
	                {
	                    window[report.onBeforeAjaxCall](evt);
	                }	              
            }
            catch(e) {
                console.log('ERROR on beforeCall');
            }		    
		}
		
		_me.AsyncManager.clearTasks();
		
		$.each(reportArr, function (ix, val) {

			var _reportObject = val;	
								
			_reportObject.setLoading();			
			
			var task = new $.AsyncManager.AsyncTask();
			
			var evt = { report : _reportObject, ReportUrl : _reportObject.ReportUrl };			
			
			onBeforeCall(evt);			
			
			task.serviceUrl = evt.ReportUrl || _me.seviceUrlPattern.replace('{0}', _me.handlerUrl).replace('{1}', encodeURI(urls)).replace('{2}', _reportObject.ReportIdentifier);			
			
			task.addEventListener('onSuccess', $.createDelegateExpanded(_me, onSuccess, _reportObject) );
			task.addEventListener('onError', $.createDelegateExpanded(_me, onError, _reportObject) );
			task.addEventListener('onAbort', $.createDelegateExpanded(_me, onAbort, _reportObject) );							        			
			
			_me.AsyncManager.addTask(task);
		} );
		
       _me.AsyncManager.runTasks(); 
        
    };
}

ReportManager._Instance = null;

ReportManager.GetInstance = function()
{
    if (ReportManager._Instance === null) 
    {
        ReportManager._Instance = new ReportManager();
    }
    return ReportManager._Instance;
};


var ReportState = 
    {
        Collapse: "Collapse",
        UnCollapse: "UnCollapse"
    };

function Report(str_id, rendererManager)
{
    var _me = this;
    _me.ReportLoaded = false;
    _me.handlerURL = null;
    
    _me.ReportId = str_id;
    _me.ReportDiv = $.byId(str_id);
    
    _me.ReportName = _me.ReportDiv.attr('report_name');
    _me.ReportCheckName = _me.ReportDiv.attr('check_name') || _me.ReportName;
    _me.ReportType = _me.ReportDiv.attr('report_type');
    _me.ReportIdentifier = _me.ReportDiv.attr('report_id');
    _me.TableArea = _me.ReportDiv.find('.TableArea');
    _me.onDataReceived = _me.ReportDiv.attr('on_data_received');
    _me.ReportUrl = _me.ReportDiv.attr('report_url');
    _me.onBeforeAjaxCall = _me.ReportDiv.attr('on_before_ajax_call');
    _me.ArrayRenderersManager = rendererManager;
    _me.ArrayRenderersManager.findArrayRenderers(_me.ReportDiv);       
    
    _me._loading = false;
    _me.onAbort = null;
    
    var _interval = null;
    
    var _req = null;
    var _h1Message = _me.ReportDiv.find('span.NotifyMessage');
    
    
    function _hideStatusMessage()
    {
        _h1Message.slideUp(300);
    }
    
    function _doCollapseExpand()
    {
        if (_me._loading) 
        { return false; }
        if (_me.state == ReportState.Collapse) 
        {
            if (!_me.ReportLoaded) 
            {
                _me.showMessage($.getResourceString('RunReportsFirst', 'Run Reports first!!'));
                return false;
            }
            else 
            {
                _me.setState(ReportState.UnCollapse, true);
            }
        }
        else 
        {
            _me.setState(ReportState.Collapse, true);
        }
    }
    
    
    _me.notifyError = function(msg)
    {
		_me.ReportDiv.addClass("Error");
        _me.hideLoading();
        _me.showMessage( "[" + $.getResourceString('Error', 'Error') + "] : " + msg, true);
        _me.ReportLoaded = false;
    };
    
    _me.getArrayRendererByIndex = function(index)
    {
        if (_me.ArrayRenderersManager) 
        { return _me.ArrayRenderersManager.getArrayRendererByIndex(index); }
        return null;
    };
    
    _me.showMessage = function(msg, sticky)
    {
        window.clearInterval(_interval);
        _h1Message.html(msg).slideDown(300);//show("blind", { direction: "vetical" }, 6000);		
        if (!sticky) {
			_interval = window.setTimeout(_hideStatusMessage, '3000');
		} 
    };
    
    _me.abort = function()
    {
        try 
        {
            _me.hideLoading();
            _me.showMessage('ABORTED');            
            
        } 
        catch (ex) 
        {
            alert(ex.message());
        }
    };
    
    _me.collapse = function()
    {
        if (_me._loading) 
        { return false; }
        _me.setState(ReportState.Collapse, true);
    };
    
    _me.expand = function()
    {
        if (_me._loading) 
        { return false; }
        if (_me.state == ReportState.Collapse) 
        {
            if (!_me.ReportLoaded) 
            {
                _me.showMessage($.getResourceString('RunReportsFirst', 'Run Reports first!!'));
                return false;
            }
            else 
            {
                _me.setState(ReportState.UnCollapse, true);
            }
        }
    };
    
    _me.setLoading = function()
    {		
        _me._loading = true;
		_me.ReportDiv.removeClass("Error");
        _me.ReportDiv.find('h2:first').hide();
        _me.ReportDiv.find('div.LoadingMark').show();
        _me.setState(ReportState.Collapse, true);
		_hideStatusMessage();
    };
    _me.hideLoading = function()
    {
        _me._loading = false;
        _me.ReportDiv.find('h2:first').show();
        _me.ReportDiv.find('div.LoadingMark').hide();
    };
    
    
    _me.setState = function(str_State, useAnimate)
    {
        if (_me.state == str_State) 
        { return; }
        switch (str_State)
        {
            case ReportState.Collapse:
                _me.ReportDiv.find('.ReportHeader').attr('show_tooltip', 'true');
                _me.ReportDiv.removeClass('UnCollapsed');
                
                if (!useAnimate) 
                {
                    _me.TableArea.css("display", "none");
                }
                else 
                {
                    _me.TableArea.hide("blind", 
                        {
                            direction: "vertical"
                        }, 500);
                }
                
                break;
            case ReportState.UnCollapse:
                _me.ReportDiv.addClass('UnCollapsed');
                _me.ReportDiv.find('.ReportHeader').attr('show_tooltip', 'false');
                _me.ReportDiv.removeClass('Highlight');
                
                if (!useAnimate) 
                {
                    _me.TableArea.css("display", "block");
                }
                else 
                {
                    _me.TableArea.show("blind", 
                        {
                            direction: "vertical"
                        }, 500);
                }
                
                break;
        }
        _me.state = str_State;
    };
    _me.highlight = function()
    {
        if (this.state == ReportState.Collapse) 
        {			
            this.ReportDiv.addClass('Highlight');
        }
    };
    _me.unHighlight = function()
    {
        if (this.state == ReportState.Collapse) 
        {
            this.ReportDiv.removeClass('Highlight');
        }
    };
    
    _me.isVisible = function()
    {
        return this.ReportDiv.is(':visible');
    };
    
    _me.setVisible = function(b_visible)
    {
        if (!b_visible) 
        {
            this.ReportDiv.hide("blind", 
                {
                    direction: "vertical"
                }, 500);
        }
        else 
        {
            this.ReportDiv.show("blind", 
                {
                    direction: "vertical"
                }, 500);
        }
        
    };
	
	function _initializeUI()
    {
        _me.setState(ReportState.Collapse);
        _me.ReportDiv.find('.ReportHeader').css('cursor', 'pointer').attr('show_tooltip', 'true').bind('mouseover', function()
        {
            _me.highlight();
        }).bind('mouseout', function(e)
        {
            _me.unHighlight();
        }).click(function(e)
        {
            _doCollapseExpand();
            return false;
        });
        
        //Initialize Tooltips
        _me.ReportDiv.find('.ReportHeader').brTip(
        {
                toShow: 500,
                toHide: 0,
                fadeIn: 'fast',
                fadeOut: 'fast',
                showTitle: false
        });
    }
	
	 _initializeUI();
	 
}



