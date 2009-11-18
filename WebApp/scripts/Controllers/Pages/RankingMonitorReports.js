$.Namespace('$.SEOToolSet');
$.Namespace('$.CurrentPage');

$.SEOToolSet.RankingMonitorReport = function(options) {

    var me = this; //closure
    var opts = {
        RankingMonitorHandler: null,
        ProjectProfileHandler: null,
        RetrieveFileHandler: null,
        cbxProxyFilterTooltip: 'Select proxy server data to show in the Report Detail table, from the proxy server locations used for the Ranking Monitor report(s)',
        cbxSearchEngineFilterTooltip: 'Select the search engines to show in the Report Detail table, from the search engine data gathered in the Ranking Monitor(s)'
    };

    $.extend(opts, options);


    //dropdownlists in the Report and Engine Selection tool
    var _ddlReportToView = null;
    var _ddlReportToCompare = null;
    var _ddlAddEngine = null;

    //the set of SearchEngines/Country that are shown in the Engine Selector tool
    var _enginesToDisplay = [];

    //Set of engines per each report (To View and To Compare)
    var _enginesInReportToView = [];
    var _enginesInReportToCompare = [];

    //Set of uniques engines without repetitions    
    var _uniqueEnginesInReports = [];

    //dropdownlists in the Detailed Report Section
    var _ddlProxyFilter = null;
    var _ddlEnginesFilter = null;

    //flags to control when all the spots in the Engine Selector are Empty or Filled
    var _allSpotsFilled = true;
    var _allSpotsEmpty = false;

    //Manager to help in the creation of the ArrayRenderer Objects
    var _renderManagerEngines = new ArrayRendererManager();
    var _reportRenderers = new ArrayRendererManager();

    //find the renderers inside the those selectors
    _renderManagerEngines.findArrayRenderers($('#SearchEnginesList'));
    _reportRenderers.findArrayRenderers($('.ReportView'));

    var _originalRowTemplate = _reportRenderers.getArrayRendererByIndex(1).getRowTemplate();
    var _originalHeaderTemplate = _reportRenderers.getArrayRendererByIndex(1).getHeaderTemplate();

    //Hidden fields used to transfer the data to the page to create the CSV Report.
    var _cvsHiddenId = null;
    var _fileNameHiddenId = null;

    //the column keyNames and Header Names for the Dynamic Columns
    var _extraColumnsProxies = [];
    var _extraColumnsEngines = [];
    var _extraColumnsProxiesEngines = [];

    //Call the constructor of the Base
    me.parent.constructor.call(me);

    /**
    * Init the Job of the Class
    */
    me.init = function() {
        //call the method from the base class
        me.parent.init.call(me);

        //If no project is selected, dispatch the Event in the Base Class
        if (!me.CheckValidProject()) {
            return;
        }

        $('.RankingReportsWrapper').show();
        _initUserInterfaceControls();


        //Bind to the ProjectChanged Event in the Base Class         				
        me.addEventListener('ProjectChanged', function() {
            _loadInitialData();
            $(".ReportView").hide();
        });
        _loadInitialData();
    };

    /**
    * Set the Id of the hidden field used to store the CSV data created in the client UI
    * @param {String} id
    */
    me.setCSVHiddenID = function(id) {
        _cvsHiddenId = id;
    };

    /**
    * Set the ID of the hidden field used to store the Proposed FileName of the file
    * @param {String} clientId
    */
    me.setFileNameHiddenId = function(clientId) {
        _fileNameHiddenId = clientId;
    };

    /**
    * the Search Engine is present in the Report To View
    * @param {Object} se the search Engine to search in the array
    */
    function _isInReportToView(se) {
        return $.arrayContains(_enginesInReportToView, se, function(a) {
            return a.Id == se.Id;
        });
    }

    /**
    * the Searcch Engine is present in the Report To Compare
    * @param {Object} se
    */
    function _isInReportToCompare(se) {
        return $.arrayContains(_enginesInReportToCompare, se, function(a) {
            return a.Id == se.Id;
        });
    }
    /**
    * return always an array with only 6 engines
    * @param {Object} engines
    */
    function _fixSearchEngines(engines) {
        /**
        * function to return an element from the array "arr" in the position "ix"
        * avoiding to raise the exception
        * @param {Object} arr
        * @param {Object} ix
        */
        function __getItemFromArray(arr, ix) {
            try {
                return arr[ix];
            }
            catch (e) {
                return null;
            }
        }

        //flags used to control wheter
        var allSelected = true;
        var allEmpty = true;

        var searchEngines = [];
        for (var ix = 0; ix < 6; ix++) {
            var item = {};
            var se = __getItemFromArray(engines, ix);
            item.IsPresent = se ? "" : "HideIt";
            if (se) {
                allEmpty = false;
                item.SearchEngineLogoUrl = se.UrlLogo;
                item.SearchEngineName = se.SearchEngineName;
                item.SearchEngineUrl = se.SearchEngineUrl;
                item.BlueClass = _isInReportToView(se) ? "" : "HideIt";
                item.GreenClass = _isInReportToCompare(se) ? "" : "HideIt";
                item.Id = se.Id;
            }
            else {
                allSelected = false;
            }
            searchEngines.push(item);
        }

        _allSpotsFilled = allSelected;
        _allSpotsEmpty = allEmpty;

        return searchEngines;
    }

    function _initUserInterfaceControls() {

        _ddlProxyFilter = new $.R3M.Combobox("#CbxProxyServers", {
            width: 300,
            extraCssClass: "ProxyFilter"
        });

        _ddlProxyFilter.setTextField('{City}').setValueField("{Id|default:'0'}").setItemTemplate("<div cbx_value='[VALUE]' title='" + opts.cbxProxyFilterTooltip +
        "' >	<div class='Proxy Combo'><img class='Flag {FlagClass}' src='{Country.FlagUrl}' alt='{City} - {Country.Name}' title='{City} - {Country.Name}' /><span class='Proxy'> {City} <label class='{FlagClass}'>-</label> {Country.Name} </span> <div class='DoClear'></div> </div> </div>")

        _ddlEnginesFilter = new $.R3M.Combobox("#CbxSearchEnginesFilter", {
            width: 300,
            extraCssClass: "AddEngineCbx"
        });

        _ddlEnginesFilter.setTextField('{SearchEngineName}').setValueField('{Id}').setItemTemplate("<div cbx_value='[VALUE]' title='" + opts.cbxSearchEngineFilterTooltip + "' ><div class='SearchEngine Combo'><img class='DynamicImage Logo {ExtraClass|default:\' \'}' src='{SearchEngineLogoUrl}' alt='{SearchEngineName}' title='{SearchEngineName}' /> <span class='Engine'>{SearchEngineName} ({SearchEngineUrl})</span> <span class='BlueDot {BlueClass}'> &#x2022;</span> <span class='GreenDot {GreenClass}'>&#x2022;</span></div> </div>");

        $(".ReportView").hide();

        _ddlReportToView = new $.R3M.Combobox('#ReportToviewDropDown', {
            width: 190,
            extraCssClass: 'Reports'
        });
        _ddlReportToCompare = new $.R3M.Combobox('#ReportToCompare', {
            width: 190,
            extraCssClass: 'Reports'
        });
        _ddlAddEngine = new $.R3M.Combobox('#AddEngineDropDown', {
            width: 200,
            extraCssClass: 'AddEngineCbx',
            dropDownListHeight: 250
        });


        _bindAddEngineEvent();

        _bindViewReportEvent();

        _bindExportButtonsEvents();
    }


    function _getFileNameForReport() {
        var tpl = 'Ranking Monitor {Project} {DateSingle}{DateComparison}';
        tpl = tpl.replace('{Project}', me.getCurrentProjectName());
        tpl = tpl.replace('{DateSingle}', _ddlReportToView.getSelectedItemText().replace(/\//g,'-').replace(/\:/g,'.'));
        var isSingle = _ddlReportToCompare.getSelectedValue() == "-1";
        tpl = tpl.replace('{DateComparison}', isSingle? '':' - ' + _ddlReportToCompare.getSelectedItemText().replace(/\//g,'-').replace(/\:/g,'.'));
        return tpl;
    }
    function _getTitleForExport() {
        
        var tpl = 'SEOToolSet - {ReportType} ' + opts.ExportedReportTitle + ' {DateRanking} {DateComparison}';
        var isSingle = _ddlReportToCompare.getSelectedValue() == "-1";
        tpl = tpl.replace('{ReportType}', isSingle ? opts.ExportTypeSingle : opts.ExportTypeComparison);
        tpl = tpl.replace('{DateRanking}', _ddlReportToView.getSelectedItemText());
        tpl = tpl.replace('{DateComparison}', isSingle ? "" : " - " + _ddlReportToCompare.getSelectedItemText());        
        return tpl;

    }

    function _getFirsCSVRow() {
        var tpl = '"' + opts.ExportedTitleString + '","{TITLE}","' + opts.ExportedDateString + '","{TODAY}",{ReportsDate},"' + opts.ExportedTypeString + '","{Type}"';
        var isSingle = _ddlReportToCompare.getSelectedValue() == "-1";
        var ReportDateTpl = '"{ReportTitle} ","{ReportDate}"';
        var reports = ReportDateTpl.replace('{ReportTitle}', opts.ExportedPrimaryReportString).replace('{ReportDate}', _ddlReportToView.getSelectedItemText());
        if (!isSingle) {
            reports += "," + ReportDateTpl.replace('{ReportTitle}', opts.ExportedComparisonReportString).replace('{ReportDate}', _ddlReportToCompare.getSelectedItemText());
        }
        var exportType = $('#radioCurrentView').attr('checked') ? opts.ExportedCurrentViewString : opts.ExportedFullDataString;
        var today = new Date();
        return tpl.replace('{TITLE}', opts.ExportedReportTitleFor + ' ' + me.getCurrentProjectName()).replace('{TODAY}', today.toString()).replace('{ReportsDate}', reports).replace('{Type}', exportType);
    }

    function _bindExportButtonsEvents() {
        $('a.button.PrintReport').click(function() {
            var w = window.open('FriendlyPrint.aspx', '_blank', '');
            var t = setInterval(function() {
                try {
                    if (w && w.window && w.window.setContent) {
                        clearInterval(t);
                        w.window.setContent($('#DeepAnalisis').html().replace(/\s+</gim, '<').replace(/>\s+/gim, '>'), _getTitleForExport());
                    }
                }
                catch (e) {
                    console.log(e.message);
                }
            }, 500);
            return false;
        });

        $('a.button.PrintToCSV').click(function() {
            var action = $(this).attr('href').replace('javascript:', '');            
            $.byId(_fileNameHiddenId).val(_getFileNameForReport());
            if ($('#radioCurrentView').attr('checked')) {
                $.byId(_cvsHiddenId).val(_getFirsCSVRow() + '\n' + $('#DeepAnalisis table').toCSV());
            }
            else {
                action = new Function(action);
                _retrieveDetailedReport({
                    idRankingMonitorRunToView: _ddlReportToView.getSelectedValue(),
                    idRankingMonitorRunToCompare: _ddlReportToCompare.getSelectedValue(),
                    idSearchEngineFilter: _ddlEnginesFilter.getDropDown()[0].options[0].value, //all
                    idProxyServerFilter: _ddlProxyFilter.getDropDown()[0].options[_ddlProxyFilter.getDropDown()[0].options.length - 1].value // 0 ==> None (Primary Server) 
                }, function(data) {
                    var template = '<table>{0}{1}</table>';
                    var headerTemplate = _getHeaderTemplateAccordingToFilters(true);

                    var arrayRenderer = new $.R3M.ArrayRenderer({ encodeBlanks: false });
                    arrayRenderer.setItemTemplate(_getRowTemplateAccordingToFilters(true))
                    arrayRenderer.setDataSource(data);

                    var rows = arrayRenderer.render();

                    var table = $.stringFormat(template, headerTemplate, rows);

                    $.byId(_cvsHiddenId).val(_getFirsCSVRow() + '\n' + $(table).toCSV());

                    //__doPostBack('ctl00$contentArea$LinkButtonRound1','');
                    action();


                });

                return false;
            }
        });

        $('a.button.PrintToPDF').click(function() {
            /*$.byId(_fileNameHiddenId).val(_getTitleForExport().replace(/:|\//g, ""));            
            $.byId(_cvsHiddenId).val(_getTitleForExport() + '\n' + $('#DeepAnalisis table').toCSV({ separator: '|' }));*/
            var fName = _getFileNameForReport();
            $.post(opts.RankingMonitorHandler, {
                action: 'GetMonitorReportPDF',
                ReportData: _getTitleForExport() + '\n' +
                $('#DeepAnalisis table').toCSV({
                    separator: '|',
                    noInclude: '.CSVOnly',
                    wrapperCharacter: ''
                }),
                FileName: fName
            }, function(data) {
                window.open(opts.RetrieveFileHandler + encodeURIComponent(fName) + ".aspx?action=GetPDFReport&pdfName=" + encodeURIComponent(fName) + "&fileId=" + data.fileId, '_blank', '');
            }, 'json');

            return false;
        });
    }

    function _bindViewReportEvent() {
        $('.button.ViewReport').click(function() {
            $('.CollapsablePanel').find('.Legend').trigger('click');
            $.byId(opts.RunReportUI).blockContent();
            _storeEnginesInProfile();
            _displaySummaryReport(_ddlReportToView.getSelectedValue(), _ddlReportToCompare.getSelectedValue(), _ddlReportToView.getSelectedItemText(), _ddlReportToCompare.getSelectedItemText());
            _displayDetailedReport(_ddlReportToView.getSelectedValue(), _ddlReportToCompare.getSelectedValue(), function() {
                $.byId(opts.RunReportUI).unblockContent();
            });
        });
    }

    function _displayDetailedReport(IdReportToView, IdReportToCompare, callbackFinish) {
        _loadProxies(IdReportToView, IdReportToCompare, function(proxies) {
            function _fixProxies(arr) {
                var ids = [];
                $.each(arr, function(i, val) {
                    ids.push(val.Id);
                    if (val.Id === 0) {
                        val.Country.Name = opts.NoProxyString;
                        val.City = '';
                        val.FlagClass = 'HideAndCollapse';
                    }
                });

                if (ids.length <= 1) {
                    ids.push(-1);
                }
                return ids.join(',');
            }

            var allProxyIds = _fixProxies(proxies);

            proxies.push({
                Id: allProxyIds,
                Country: {
                    Name: opts.AllProxiesString
                },
                FlagClass: 'HideAndCollapse',
                City: ''
            });

            _ddlProxyFilter.setDataSource(proxies).dataBind();

            _ddlProxyFilter.removeEventsOfType('Changed');
            _ddlProxyFilter.addEventListener('Changed', function() {
                _checkRulesFromProxiesFilter();
                _doRetrieveDetailedReport(IdReportToView, IdReportToCompare);
            });

            var arr = _getEnginesForFilter();

            var engines = _getAllSearchEnginesInPanel(arr);


            arr.unshift({
                SearchEngineUrl: opts.AllEnginesString,
                SearchEngineName: '',
                Id: engines.join(","),
                BlueClass: 'HideIt',
                GreenClass: 'HideIt',
                ExtraClass: 'HideAndCollapse'
            });

            _ddlEnginesFilter.setDataSource(arr).dataBind();

            _ddlEnginesFilter.removeEventsOfType('Changed');
            _ddlEnginesFilter.addEventListener('Changed', function() {
                _checkRulesFromEngineFilter();
                _doRetrieveDetailedReport(IdReportToView, IdReportToCompare);
            });

            _doRetrieveDetailedReport(IdReportToView, IdReportToCompare, callbackFinish);

        });

    }

    function _checkRulesFromProxiesFilter() {
        if (_ddlProxyFilter.getSelectedValue().indexOf(',') > -1) {
            if (_ddlEnginesFilter.getSelectedValue().indexOf(',') > -1) {
                _ddlEnginesFilter.setValueTo(_ddlEnginesFilter.getDropDown()[0].options[1].value); //No Proxy
            }
        }
    }

    function _checkRulesFromEngineFilter() {
        if (_ddlEnginesFilter.getSelectedValue().indexOf(',') > -1) {
            if (_ddlProxyFilter.getSelectedValue().indexOf(',') > -1) {
                _ddlProxyFilter.setValueTo('0'); //No Proxy
            }
        }
    }

    function _doRetrieveDetailedReport(IdReportToView, IdReportToCompare, callbackFinish) {
        _retrieveDetailedReport({
            "idRankingMonitorRunToView": IdReportToView,
            "idRankingMonitorRunToCompare": IdReportToCompare,
            idSearchEngineFilter: _ddlEnginesFilter.getSelectedValue(), //all
            idProxyServerFilter: _ddlProxyFilter.getSelectedValue()
        }, function(initialData) {
            $(".ReportView").show(); //If the Report is hide, then show it
            var renderer = _reportRenderers.getArrayRendererByIndex(1);

            renderer.setHeaderTemplate(_getHeaderTemplateAccordingToFilters());
            renderer.setRowTemplate(_getRowTemplateAccordingToFilters());
            renderer.onRowsFinishedRendered = function(args) {
                args.rows = "<tbody>" + args.rows + "</tbody>";
                args.header = '<thead>' + args.header + '</thead>';
            }

            renderer.setDataSource(initialData);
            renderer.orderBy('Activity', 'DESC', 'Number');
            renderer.dataBind();



            if (callbackFinish)
                callbackFinish();
        });
    }

    function _getRowTemplateAccordingToFilters(enginesAsHeaders) {

        var extra = '';
        var extraTDTemplate = "<td class='[ClassToUse]' > {[key]|default:'-'} </td>";

        if (enginesAsHeaders) {
            if (_ddlReportToCompare.getSelectedValue() != '-1') {
                $.each(_extraColumnsProxiesEngines, function(i, val) {
                    /* extra += extraTDTemplate
                    .replace('[key]', val.key || "")
                    .replace('[ClassToUse]', 'NotCSV');*/
                    extra += extraTDTemplate.replace('[key]', val.key + '_ds_01' || "").replace('[ClassToUse]', 'CSVOnly');
                    extra += extraTDTemplate.replace('[key]', val.key + '_ds_02' || "").replace('[ClassToUse]', 'CSVOnly');

                });
            }
            else {
                $.each(_extraColumnsProxiesEngines, function(i, val) {
                    extra += extraTDTemplate.replace('[key]', val.key || "").replace('[ClassToUse]', '');
                });
            }
        }
        else {
            var selVal = _ddlReportToCompare.getSelectedValue();
            var extraClass = selVal != "-1" ? 'NotCSV' : '';
            if (_ddlProxyFilter.getSelectedValue().indexOf(',') < 0) { //all engines and No Proxy or at least one proxy selected
                $.each(_extraColumnsEngines, function(i, val) {
                    extra += extraTDTemplate.replace('[key]', val.key || "").replace('[ClassToUse]', extraClass);
                    if (selVal != "-1") {
                        extra += extraTDTemplate.replace('[key]', val.key + '_ds_01' || "").replace('[ClassToUse]', 'CSVOnly');
                        extra += extraTDTemplate.replace('[key]', val.key + '_ds_02' || "").replace('[ClassToUse]', 'CSVOnly');
                    }
                });
            }
            else {
                $.each(_extraColumnsProxies, function(i, val) {
                    extra += extraTDTemplate.replace('[key]', val.key || "").replace('[ClassToUse]', extraClass);
                    if (selVal != "-1") {
                        extra += extraTDTemplate.replace('[key]', val.key + '_ds_01' || "").replace('[ClassToUse]', 'CSVOnly');
                        extra += extraTDTemplate.replace('[key]', val.key + '_ds_02' || "").replace('[ClassToUse]', 'CSVOnly');
                    }
                });
            }
        }
        
        //console.log('row templates ' + _originalRowTemplate.replace(/<td>\s*\[EXTRA_COLUMNS\]\s*<\/td>/gi, extra));

        return _originalRowTemplate.replace(/<td>\s*\[EXTRA_COLUMNS\]\s*<\/td>/gi, extra);
    }
    function _getHeaderTemplateAccordingToFilters(enginesAsHeaders) {
        var extra = '';
        var extraHDTemplate = '<th class="Sortable [ClassToUse]" field_name="[key]" data_type="Number"><span>[header] <span class="CSVOnly">&nbsp;-&nbsp;</span> <br />[subHeader]</span></th>';

        if (enginesAsHeaders) {
            if (_ddlReportToCompare.getSelectedValue() != '-1') {
                $.each(_extraColumnsProxiesEngines, function(i, val) {
                    /*extra += extraHDTemplate
                    .replace('[key]', val.key + '_ds_01' || "")
                    .replace('[header]', val.header || "")
                    .replace('[subHeader]', val.subHeader || "")
                    .replace('[ClassToUse]','NotCSV');*/
                    extra += extraHDTemplate.replace('[key]', val.key + '_ds_01' || "").replace('[header]', val.header || "").replace('[subHeader]', val.subHeader + ' - Primary' || "").replace('[ClassToUse]', 'CSVOnly');

                    extra += extraHDTemplate.replace('[key]', val.key + '_ds_02' || "").replace('[header]', val.header || "").replace('[subHeader]', val.subHeader + ' - Comparison' || "").replace('[ClassToUse]', 'CSVOnly');

                });
            }
            else {
                $.each(_extraColumnsProxiesEngines, function(i, val) {
                    extra += extraHDTemplate.replace('[key]', val.key || "").replace('[header]', val.header || "").replace('[subHeader]', val.subHeader || "").replace('[ClassToUse]', '');
                });

            }
        }
        else {

            var selVal = _ddlReportToCompare.getSelectedValue() != "-1";
            var extraClass = selVal ? 'NotCSV' : '';
            if (_ddlProxyFilter.getSelectedValue().indexOf(',') < 0) { //all engines and No Proxy or at Least One Proxy Selected
                $.each(_extraColumnsEngines, function(i, val) {
                    extra += extraHDTemplate.replace('[key]', (selVal ? val.key + '_ds_01' : val.key) || "").replace('[header]', val.header || "").replace('[subHeader]', val.subHeader || "").replace('[ClassToUse]', extraClass);

                    if (selVal) {
                        extra += extraHDTemplate.replace('[key]', val.key + '_ds_01' || "").replace('[header]', val.header || "").replace('[subHeader]', val.subHeader + ' - Primary' || "").replace('[ClassToUse]', 'CSVOnly');
                        extra += extraHDTemplate.replace('[key]', val.key + '_ds_02' || "").replace('[header]', val.header || "").replace('[subHeader]', val.subHeader + ' - Comparison' || "").replace('[ClassToUse]', 'CSVOnly');
                    }
                });
            }
            else {
                $.each(_extraColumnsProxies, function(i, val) {
                    extra += extraHDTemplate.replace('[key]', (selVal ? val.key + '_ds_01' : val.key) || "").replace('[header]', val.header || "").replace('[subHeader]', val.subHeader || "").replace('[ClassToUse]', extraClass);

                    if (selVal) {
                        extra += extraHDTemplate.replace('[key]', val.key + '_ds_01' || "").replace('[header]', val.header || "").replace('[subHeader]', val.subHeader + ' - Primary' || "").replace('[ClassToUse]', 'CSVOnly');
                        extra += extraHDTemplate.replace('[key]', val.key + '_ds_02' || "").replace('[header]', val.header || "").replace('[subHeader]', val.subHeader + ' - Comparison' || "").replace('[ClassToUse]', 'CSVOnly');
                    }
                });
            }
        }

        return _originalHeaderTemplate.replace(/<th>\s*\[EXTRA_HEADERS\]\s*<\/th>/gi, extra);
    }

    function _loadProxies(IdReportToView, IdReportToCompare, callback) {
        $.post(opts.RankingMonitorHandler, {
            action: "GetProxiesInRankingMonitorReportRun",
            "idRankingMonitorRunToView": IdReportToView,
            "idRankingMonitorRunToCompare": IdReportToCompare
        }, function(data) {
            callback(data || []);
        }, "json");
    }

    function _getEnginesForFilter() {
        var arr = $.map(_fixSearchEngines(_enginesToDisplay), function(val, i) {
            return (val.BlueClass === '' || val.GreenClass === '') ? val : null;
        });

        return arr;
    }

    function _getAllSearchEnginesInPanel(arr) {
        var IdSearchEngines = [];
        $.each(arr, function(i, val) {
            IdSearchEngines.push(val.Id);
        });

        if (IdSearchEngines.length <= 1) {
            IdSearchEngines.push(-1)
        }

        return IdSearchEngines;
    }

    function _retrieveDetailedReport(options, callback) {
        var _opts = {
            action: 'GetDetailedRankingMonitorReportRun',
            idRankingMonitorRunToView: -1,
            idRankingMonitorRunToCompare: -1,
            idSearchEngineFilter: _ddlEnginesFilter.getSelectedValue(), //all
            idProxyServerFilter: 0 // 0 ==> None (Primary Server) 
        };
        $.extend(_opts, options);
        $.post(opts.RankingMonitorHandler, _opts, function(data) {

            _extraColumnsProxies = [];
            _extraColumnsEngines = [];
            _extraColumnsProxiesEngines = [];

            _processData(data.keywordAnalisis);
            _processData(data.keywordAnalisisToCompare)
            _extraColumnsEngines = $.arrayUnique(_extraColumnsEngines, function(a, b) {
                return a.key === b.key;
            });
            _extraColumnsProxies = $.arrayUnique(_extraColumnsProxies, function(a, b) {
                return a.key === b.key;
            });

            _extraColumnsProxiesEngines = $.arrayUnique(_extraColumnsProxiesEngines, function(a, b) {
                return a.key === b.key;
            });

            var comparisonDataSet = _createComparisonDataSet(data.keywordAnalisis, data.keywordAnalisisToCompare);
            callback(comparisonDataSet);
        }, "json");

    }
    function cleanKeywordsToRender(data) {
        $.each(data, function(i, val) {
            if (val && val.Status) {
                val.StatusClass = (val.Status == "F") ? "ResultError" : "SuccessClass";
            }
            else {
                console.log("There wasn't a value/status for the keyword");
            }
        });
    }
    function _createComparisonDataSet(ds01, ds02) {
        cleanKeywordsToRender(ds01);
        if (!ds02 || !ds02.length)
            return ds01;
        cleanKeywordsToRender(ds02);
        $.each(ds01, function(i, val) {
            var keyToCompare = $.arrayFind(ds02, function(ele) {
                return ele.Keyword == val.Keyword
            });
            $.each(val.EnginesPerProxyResults || [], function(ix, item) {
                keyToCompare = keyToCompare ||
                {};
                var proxyKey = "proxy_" + item.IdProxy;
                var engineKey = "engine_" + item.IdSearchEngineCountry;
                var proxy_engine_key = engineKey + '_' + proxyKey;

                val[proxyKey + '_ds_01'] = item.Pages;
                val[proxyKey + '_ds_02'] = keyToCompare[proxyKey];

                val[engineKey + '_ds_01'] = item.Pages
                val[engineKey + '_ds_02'] = keyToCompare[engineKey];

                val[proxy_engine_key + '_ds_01'] = item.Pages;
                val[proxy_engine_key + '_ds_02'] = keyToCompare[proxy_engine_key];

                var value01 = parseInt(val[proxy_engine_key + '_ds_01']);
                var value02 = parseInt(val[proxy_engine_key + '_ds_02']);

                var difference = "-";
                if (!isNaN(value01) && !isNaN(value02)) {
                    difference = value01 - value02;
                }

                var decrease = "";

                if (difference > 0) {
                    decrease = 'Increase';
                    difference = '&#x25B2;' + Math.abs(difference);
                }
                if (difference < 0) {
                    decrease = 'Decrease';
                    difference = '&#x25BC;' + Math.abs(difference);
                }

                var value = $.stringFormat('<span class="PrimaryToken">{0}</span> / <span class="ComparisonToken">{1}</span> / <span class="{2}" >{3}</span>', val[proxy_engine_key + '_ds_01'] || '-', val[proxy_engine_key + '_ds_02'] || '-', decrease, difference);

                val[engineKey] = value;
                val[proxy_engine_key] = value;
                val[proxyKey] = value;
            });
        });

        $.each(ds02, function(i, val) {
            var keyToCompare = $.arrayFind(ds01, function(ele) {
                return ele.Keyword == val.Keyword
            });
            $.each(val.EnginesPerProxyResults || [], function(ix, item) {              
                keyToCompare =  keyToCompare || {};
                
                var proxyKey = "proxy_" + item.IdProxy;
                var engineKey = "engine_" + item.IdSearchEngineCountry;
                var proxy_engine_key = engineKey + '_' + proxyKey;

                val[proxyKey + '_ds_02'] = item.Pages;
                val[proxyKey + '_ds_01'] = keyToCompare[proxyKey];

                val[engineKey + '_ds_02'] = item.Pages
                val[engineKey + '_ds_01'] = keyToCompare[engineKey];

                val[proxy_engine_key + '_ds_02'] = item.Pages;
                val[proxy_engine_key + '_ds_01'] = keyToCompare[proxy_engine_key];

                var value01 = parseInt(val[proxy_engine_key + '_ds_01']);
                var value02 = parseInt(val[proxy_engine_key + '_ds_02']);

                var difference = "-";
                if (!isNaN(value01) && !isNaN(value02)) {
                    difference = value01 - value02;
                }

                var decrease = "";

                if (difference > 0) {
                    decrease = 'Increase';
                    difference = '&#x25B2;' + Math.abs(difference);
                }
                if (difference < 0) {
                    decrease = 'Decrease';
                    difference = '&#x25BC;' + Math.abs(difference);
                }

                var value = $.stringFormat('<span class="PrimaryToken">{0}</span> / <span class="ComparisonToken">{1}</span> / <span class="{2}" >{3}</span>', val[proxy_engine_key + '_ds_01'] || '-', val[proxy_engine_key + '_ds_02'] || '-', decrease, difference);                                
                
                val[engineKey] = value;
                val[proxy_engine_key] = value;
                val[proxyKey] = value;
            });
        });
        var ret = $.arrayMergeUnique(ds01, ds02, function(a, b) { if (a.Keyword == b.Keyword) { _copyValuesIfNotExists(a,b); return true; } return false; });        
        return ret;
    }
    
    function _copyValuesIfNotExists(a, b) {
        
        for (var key in b) {
            if (a[key] == null) {
                a[key] = b[key];
            }
        }        
    }
    function _processData(arr) {
        $.each(arr, function(i, val) {
            $.each(val.EnginesPerProxyResults || [], function(ix, item) {
                var proxyKey = "proxy_" + item.IdProxy;
                var engineKey = "engine_" + item.IdSearchEngineCountry;
                var proxy_engine_key = engineKey + '_' + proxyKey;
                if (!val[proxyKey])
                    val[proxyKey] = 0;
                val[proxyKey] = item.Pages;

                if (!val[engineKey])
                    val[engineKey] = 0;
                val[engineKey] = item.Pages;

                val[proxy_engine_key] = item.Pages;

                _extraColumnsProxies.push({
                    key: proxyKey,
                    header: item.ProxyCity,
                    subHeader: item.ProxyCountry
                });
                _extraColumnsEngines.push({
                    key: engineKey,
                    header: item.SearchEngineName,
                    subHeader: item.SearchEngineUrl
                });
                _extraColumnsProxiesEngines.push({
                    key: proxy_engine_key,
                    header: item.SearchEngineName + '<span class="CSVOnly">&nbsp;-&nbsp;</span> ' + item.SearchEngineUrl,
                    subHeader: item.ProxyCity + '<span class="CSVOnly">&nbsp;-&nbsp;</span> ' + item.ProxyCountry
                })
            });
        });
    }

    function _displaySummaryReport(IdReportToView, IdReportToCompare, ReportToViewDate, ReportToCompareDate) {
        _retrieveSummaryReport(function(data) {
            $(".ReportView").show().find('#ReportName').html(me.getCurrentProjectName());

            $('.ReportToView .ReportDate').html(ReportToViewDate);

            if (_ddlReportToCompare.getSelectedValue() != "-1") {
                $('.ReportToCompare').show();
                $('.ReportToCompare .ReportDate').html(ReportToCompareDate);
            }
            else {
                $('.ReportToCompare').hide();
            }

            var renderer = _reportRenderers.getArrayRendererByIndex(0);

            renderer.setDataSource(data.data);

            renderer.onRowsFinishedRendered = function(o) {
                o.rows += $.stringFormat('<tr class="totals clickable selected" image_src="{3}" > <td> <span class="TotalTitle">' +
                $.getResourceString('Totals', 'Totals') +
                '</span></td> <td> {0} </td> <td> {1} </td> <td><div class="ArrowContainer"> {2} <div class="SummaryArrow"></div></div> </td> <td class="MergeCell" > </td> </tr>', data.total.PageRank, data.total.InboundLinks, data.total.PagesIndexed, data.total.ResumeImage);
            };

            renderer.onRendered = function() {
                $('td.MergeCell').mergeCells();

                $('#ImageReport').attr('src', $('.ReportSummary tr:last').attr('image_src'));

                $('.ReportSummary tr.clickable').css('cursor', 'pointer').click(function() {
                    $('#ImageReport').attr('src', $(this).attr('image_src'));
                    $('.ReportSummary tr').removeClass('selected');
                    $(this).addClass('selected');
                });

            };

            renderer.dataBind();





        });
    }

    function _retrieveSummaryReport(callback) {
        var arr = _getEnginesForFilter();
        var engines = _getAllSearchEnginesInPanel(arr);

        $.post(opts.RankingMonitorHandler, {
            action: 'GetReportSummaryForRankingMonitorRun',
            idRankingMonitorRunToView: _ddlReportToView.getSelectedValue(),
            idRankingMonitorRunToCompare: _ddlReportToCompare.getSelectedValue(),
            engines: engines.join(',')
        }, function(data) {
            if (callback) {
                callback(data);
            }
        }, "json");
    }

    function _storeEnginesInProfile(callback) {
        $.post(opts.ProjectProfileHandler, {
            action: "SetRankingMonitorReportDefaultEngines",
            idProject: me.getCurrentIdProject() || -1,
            RankingMonitorReportDefaultEngines: JSON.stringify(_enginesToDisplay)
        }, function() {
            if (callback) {
                callback();
            }
        }, "json");
    }

    function _bindAddEngineEvent() {
        $('a.button.AddEngine').click(function() {
            var engine = $.arrayFind(_uniqueEnginesInReports, function(val) {
                return val.Id == _ddlAddEngine.getSelectedValue();
            });
            if (engine) {
                _enginesToDisplay.push(engine);
                _renderSearchEnginesPanel();
                _setDataToAddEngineDropDown();
            }
            return false;
        });
    }

    function _emptyEngineDropDown() {
        //console.log('empty -> ' + _ddlAddEngine.getVisibleItemsCount());
        return _ddlAddEngine.getVisibleItemsCount() == 0;
    }

    function _getReportsForProject(IdProjectSelected, callback) {
        $.post(opts.RankingMonitorHandler, {
            idProject: IdProjectSelected,
            action: 'GetRankingMonitorRuns'
        }, function(data) {
            callback(data);
        }, "json");


    }

    function _loadInitialData() {


        _ddlReportToCompare.removeEventsOfType('Changed');
        _ddlReportToView.removeEventsOfType('Changed');
        var IdProjectSelected = me.getCurrentIdProject();
        if (!IdProjectSelected || IdProjectSelected == '-1') {
            console.log('IdSelectedProject Not Valid');
            return;
        }

        //FirstTime Load the Engines Stored in user Profile
        _loadEnginesFromProfile(function() {
            //Now Retrieve the Reports Available for the Project With the given Id                     
            _getReportsForProject(IdProjectSelected, function(data) {

                if (!data || !data.length) {
                    $.byId(opts.RunReportUI).blockContent();
                    me.dispatchEvent({
                        type: 'NoReportsFound',
                        idPreviousProject: me.parent.getPreviousIdProject.call(me)
                    });
                    return;
                }
                $.byId(opts.RunReportUI).unblockContent();

                _ddlReportToView.setDataSource(data).setTextField('{ExecutionDate}').setValueField('{Id}').dataBind();

                var newArr = [{
                    ExecutionDate: '[' + opts.NoneSelectedString + ']',
                    Id: '-1'
}].concat(data);

                    _ddlReportToCompare.setDataSource(newArr).setTextField('{ExecutionDate}').setValueField('{Id}').dataBind();

                    //Set Listeners so we can react to a project Change.
                    _ddlReportToView.addEventListener('Changed', function(args) {
                        _initEnginesPanelAndAddEngineDropDown();
                    });

                    _ddlReportToCompare.addEventListener('Changed', function(args) {
                        _initEnginesPanelAndAddEngineDropDown();
                    });


                    _initEnginesPanelAndAddEngineDropDown();

                });


            });
        }

        function _initEnginesPanelAndAddEngineDropDown() {

            var IdSelectedReport = _ddlReportToView.getSelectedValue();
            var IdSelectedReportToCompare = _ddlReportToCompare.getSelectedValue();

            _getEnginesInReports(IdSelectedReport, IdSelectedReportToCompare, function() {
                _renderSearchEnginesPanel();
                _setDataToAddEngineDropDown();
            });

            $('.ReportView').hide();
        }

        function _setDataToAddEngineDropDown() {
            var data = $.map(_uniqueEnginesInReports, function(val, i) {
                if ($.arrayContains(_enginesToDisplay, val, function(a, b) {
                    return a.Id == b.Id;
                })) {
                    return null;
                }
                return val;
            });
            _ddlAddEngine.setDataSource(data).setTextField('{SearchEngineName}').setValueField('{Id}').setItemTemplate("<div cbx_value='[VALUE]' title='[TEXT]' ><div class='SearchEngine Combo'><img class='DynamicImage Logo' image_src='{UrlLogo}' alt='{SearchEngineName}' title='{SearchEngineName}' /> <span class='Engine'>{SearchEngineName} ({SearchEngineUrl})</span> <span class='BlueDot {BlueClass}'> &#x2022;</span> <span class='GreenDot {GreenClass}'>&#x2022;</span></div> </div>");
            _ddlAddEngine.addEventListener('ItemRendering', function(evt) {
                evt.args.row.BlueClass = _isInReportToView(evt.args.row) ? "" : "HideIt";
                evt.args.row.GreenClass = _isInReportToCompare(evt.args.row) ? "" : "HideIt";
            });
            _ddlAddEngine.addEventListener('ItemsRendered', function(evt) {
                if (_emptyEngineDropDown()) {
                    $('a.button.AddEngine').disable(true, {
                        disableClass: 'disabled'
                    });
                }
            });
            _ddlAddEngine.dataBind();

            $('.SearchEngine.Combo img.DynamicImage').each(function() {
                $(this).attr('src', $(this).attr('image_src'));
            });

        }

        function _getEnginesInReports(IdSelectedReport, IdSelectedReportToCompare, callback) {
            _enginesInReportToView = [];
            _enginesInReportToCompare = [];

            $.post(opts.RankingMonitorHandler, {
                action: "GetSearchEnginesCountriesByRankingMonitorRun",
                idRankingMonitorRun: IdSelectedReport,
                idRankingMonitorRunToCompare: IdSelectedReportToCompare
            }, function(data) {
                _enginesInReportToView = data.enginesInReportToView || [];
                _enginesInReportToCompare = data.enginesInReportToCompare || [];

                _uniqueEnginesInReports = $.arrayMergeUnique(_enginesInReportToView, _enginesInReportToCompare, function(eleA, eleB) {
                    return eleA.Id == eleB.Id;
                }) ||
            [];
                callback();
            }, "json");
        }

        function _loadEnginesFromProfile(callback) {
            $.post(opts.ProjectProfileHandler, {
                action: "GetRankingMonitorReportDefaultEngines",
                idProject: me.getCurrentIdProject() || -1
            }, function(data) {
                _enginesToDisplay = data;
                callback();
            }, "json");
        }


        function _bindCloseEventOnEngines() {
            $('#SearchEnginesList a.xClose').unbind('click').bind('click', function() {
                var Id = $(this).attr('attr_id');

                var item = $.arrayFind(_enginesToDisplay, function(val) {
                    return val.Id == Id;
                });

                if (item) {
                    $(this).parent().parent().fadeOut(500, function() {
                        $.arrayRemove(_enginesToDisplay, item, function(a, b) {
                            return a.Id == b.Id;
                        });
                        _renderSearchEnginesPanel();
                        _setDataToAddEngineDropDown();
                    });


                }

                return false;
            });
        }

        function _setButtonStates() {
            if (_allSpotsFilled) {
                $('a.button.AddEngine').disable(true, {
                    disableClass: 'disabled'
                });
            }
            else {
                $('a.button.AddEngine').disable(false);
            }

            if (_allSpotsEmpty) {
                $('.button.ViewReport').disable(true, {
                    disableClass: 'disabled'
                });
            }
            else {
                $('.button.ViewReport').disable(false);
            }
        }


        function _renderSearchEnginesPanel() {
            var _renderer = _renderManagerEngines.getArrayRendererByIndex(0);
            _renderer.setDataSource(_fixSearchEngines(_enginesToDisplay));
            _renderer.dataBind();

            _bindCloseEventOnEngines();

            _setButtonStates();
        }

    };

    $.SEOToolSet.RankingMonitorReport.inherits($.SEOToolSet.PageBase);
