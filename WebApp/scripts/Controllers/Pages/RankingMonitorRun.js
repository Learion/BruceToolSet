
$.SEOToolSet.RankingMonitorRun = function(options) {
    var keywordLists;
    var chkTree;
    var manageLinkUrlBase;
    var pb;
    var rankingResultTitleBase;
    var reportRenderers;
    var profileConfig;
    var rankingTotalBase;
    var competitorsTotalBase;
    var manageLinkUrlBase;
    var runMonitorCommandBase;
    var keywordListProfile;
    var isProcessing;
    var idMonitor;
    var idProjectSelected = -1;
    var cbx = $.CurrentPage.cbxProjects;
    var me = this; //closure
    var opts =
                {
                    RankingMonitorHandler: null,
                    ProjectHandler: null,
                    MonitorConflictTextsObject: { text: null, title: null, continueRunButton: null, startNewRunButton: null },
                    ProfileErrorMessage: '',
                    RunMonitorCommandText: '',
                    ProcessingText: '',
                    ActivityTemplate: ''
                };

    $.extend(opts, options);


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
            $('#KeywordsSection').hide('fast');
            return;
        }

        rankingTotalBase = $("#RankingTotal").html();
        competitorsTotalBase = $("#RankingMonitorCompleted").html();
        runMonitorCommandBase = $("#ctl00_contentArea_RunMonitorCommand").html();
        _changeLabelRunCommand(false);
        reportRenderers = new ArrayRendererManager();
        reportRenderers.findArrayRenderers($('#RankingResultSection'));
        rankingResultTitleBase = $("#RankingResultSection h2").html();
        chkTree = new $.R3M.Helpers.CheckedTree('#TreeFromArray');
        manageLinkUrlBase = $("#ManageKeywordsCommand").attr("href");

        me.addEventListener('ProjectChanged', function(args) {
            idProjectSelected = args.IdProject;
            _projectChanged();
        });

        idProjectSelected = cbx.getDropDown().val();
        _initUserInterfaceControls();
        _projectChanged();
    };

    me.continueRun = function(idMonitorRun) {
        _clearResultsForMonitorResults();
        if (!idMonitorRun) {
            if (idMonitor) {
                idMonitorRun = idMonitor;
            }
            else {
                me.dispatchEvent(
                    {
                        type: 'Error',
                        errorMessage: 'There is not any monitor found',
                        errorCode: 'MonitorRunNotFound'
                    });
            }
        }
        $.post(opts.RankingMonitorHandler,
                {
                    action: "GetKeywordsAnalysisFromRankingMonitorRun",
                    idRankingMonitorRun: idMonitorRun
                }, function(data) {
                    var keywordsTotal = data.length;
                    var renderer = reportRenderers.getArrayRendererByIndex(0);
                    var keywordsProcessed;
                    var keywordsToRender = $.arrayFilter(data, function(ele) {
                        return ele.Status == "C" || ele.Status == "F";
                    });
                    _clearResultsForMonitorResults();
                    keywordsProcessed = keywordsToRender.length;
                    _getRecurrentRequests(idMonitorRun, keywordsTotal, renderer, keywordsProcessed, keywordsToRender);
                }, "json");
    };

    me.finishRankingMonitorRun = function(callback) {
        $("div.ProgressSection").slideUp("slow");
        isProcessing = false;
        _changeLabelRunCommand(false);
        $(".KeywordsSection").unblockContent();
        $("#ctl00_contentArea_RunMonitorCommand").disable(false);
        cbx.enable();
        if (typeof (callback) == 'function')
            callback();
    };

    me.runRankingMonitor = function() {
        _saveProfile();
        $(".KeywordsSection").blockContent();
        cbx.disable();
        $("#ctl00_contentArea_RunMonitorCommand").disable(true,
                        {
                            disableClass: "disabled"
                        });
        $.get(opts.RankingMonitorHandler,
                {
                    action: "IsMonitorRunning",
                    idProject: cbx.getDropDown().val()
                }, function(data) {
                    console.log("IsMonitorRunning: go!");
                    var idMonitor = data.IsAnyRunning;
                    var monitorConflict = opts.MonitorConflictTextsObject;
                    if (idMonitor > 0) {
                        $.showBigDialog(monitorConflict.text,
                                {
                                    title: monitorConflict.title,
                                    icon: $.MessageIco.Warning,
                                    buttons: [
                                        {
                                            text: monitorConflict.startNewRunButton,
                                            command: 'StartNewRun'
                                        },
                                        {
                                            text: monitorConflict.continueRunButton,
                                            command: 'ContinueRun'
}],
                                    showCancel: false,
                                    onCommand: function(args) {
                                        if (args.command == 'Ok') {
                                            $.hideOverlay(function() {
                                                $(".KeywordsSection").unblockContent();
                                                cbx.enable();
                                                $("#ctl00_contentArea_RunMonitorCommand").disable(false);
                                            });
                                        }
                                        else if (args.command == 'ContinueRun') {
                                            me.continueRun(idMonitor);
                                        }
                                        else if (args.command == 'StartNewRun') {
                                            $.post(opts.RankingMonitorHandler,
                                                {
                                                    action: "EndMonitorProcess",
                                                    idRankingMonitorRun: idMonitor
                                                }, function(data) {
                                                    if (data.Result == true) {
                                                        _startNewRun();
                                                    }
                                                    else {
                                                        me.dispatchEvent(
                                                            {
                                                                type: 'Error',
                                                                errorMessage: 'Wrong results from server',
                                                                errorCode: 'WrongResultsMonitorRun'
                                                            });
                                                    }
                                                },
                                            "json");
                                        }
                                    }
                                });
                    }
                    else {
                        console.log("adsStarting new monitor run!");
                        _startNewRun();
                    }
                }, "jsonp");
    }

    function _initUserInterfaceControls() {
        $().ajaxError(function(event, request, settings) {
            me.dispatchEvent(
                        {
                            type: 'Error',
                            errorMessage: 'Error on loading',
                            errorCode: 'GeneralAjaxError'
                        });
        });

        $("#CheckAllKeywordsListCommand").click(function(e) {
            chkTree.checkAll();
        });

        $("#UncheckAllKeywordsListCommand").click(function(e) {
            chkTree.checkNone();
        });

        $("#ctl00_contentArea_RunMonitorCommand").click(me.runRankingMonitor);

        $("#ctl00_contentArea_HyperLinkCancel").click(function() {
            me.finishRankingMonitorRun();
            $.post(opts.RankingMonitorHandler, {
                action: "EndMonitorProcess",
                idRankingMonitorRun: idMonitor
            }, function(data) {
                if (data.Result != true) {
                    me.dispatchEvent(
                            {
                                type: 'Error',
                                errorMessage: 'Wrong results from server',
                                errorCode: 'WrongResultsMonitorRun'
                            });
                }
            }, "json");
        });

        chkTree.addEventListener('CheckedStateChanged', function(args) {
            if (args.checked > 0)
                $("#ctl00_contentArea_RunMonitorCommand").disable(false);
            else
                $("#ctl00_contentArea_RunMonitorCommand").disable(true,
                            {
                                disableClass: "disabled"
                            });
        });

        pb = new $.R3M.ProgressBar('#ProgressBarHere');
    }

    function _projectChanged() {
        $("#ManageKeywordsCommand").attr("href", manageLinkUrlBase + idProjectSelected);
        _loadProfile();
    }

    function _startNewRun() {
        console.log($("#ctl00_userControlHeader_LoginView1_WelcomeUser").html().split(" ")[1]);
        $.get(opts.RankingMonitorHandler,
                {
                    action: "RunRankingMonitor",
                    idProject: cbx.getDropDown().val(),
                    idKeywordLists: chkTree.getSelected(),
                    user: $("#ctl00_userControlHeader_LoginView1_WelcomeUser").html().split(" ")[1]
                }, function(data) {
                    console.log("Inside RunRankingMonitor...");
                    var keywordsTotal = 0;
                    idMonitor = data.IdRankingMonitorRun;
                    _clearResultsForMonitorResults();
                    keywordsTotal = data.KeywordsTotal;
                    _showRankingMonitorReport(idMonitor, keywordsTotal);
                }, "jsonp");
    }

    function _clearResultsForMonitorResults() {
        _changeLabelRunCommand(true);
        $("#RankingMonitorCompleted").slideUp();
        $("#RankingTotal").slideUp();
        $("#SummaryTable").slideUp();
        pb.setValue(0);
        $("div.ProgressSection").slideDown("slow");
        $("#RankingResultSection h2").html(rankingResultTitleBase.replace(/\[PROJECT_NAME\]/, cbx.getSelectedItemText()));
        $("#RankingResultSection").slideDown("slow");
    }

    function _loadProjectProfile() {
        keywordListProfile = Sys.Services.ProfileService.properties.KeywordListRankingMonitorRunPreferences;
        profileConfig = null;
        var posProject = keywordListProfile ?
            _getProjectPositionFromRankingMonitorPreferences(idProjectSelected, keywordListProfile = JSON.parse(keywordListProfile)) : 0;
        if (keywordListProfile && posProject >= 0) {
            profileConfig = keywordListProfile.projects[posProject];
            _renderKeywordLists();
        }
        else {
            console.log("Getting monitor configuration...");
            $.get(opts.RankingMonitorHandler,
                    {
                        action: 'GetRankingMonitorConfiguration',
                        idProject: idProjectSelected
                    }, function(config) {
                        if (config !== null && config.MonitorKeywordList != null && config.MonitorKeywordList.length > 0) {
                            idKeywordLists = [];
                            $.each(config.MonitorKeywordList, function(i, monitorKeywordList) {
                                idKeywordLists.push(monitorKeywordList.KeywordList.IdKeywordList);
                            });
                            profileConfig = { idKeywordLists: idKeywordLists };
                        }
                        _renderKeywordLists();
                    }, 'jsonp');
        }
    }

    function _renderKeywordLists() {
        $.post(opts.ProjectHandler,
                {
                    action: 'getKeywordsListsFromProject',
                    idProject: idProjectSelected
                }, function(data) {
                    keywordLists = data.keywordLists;
                    chkTree.setDataSource(keywordLists);
                    if (keywordLists.length < 1) {
                        $("#ctl00_contentArea_RunMonitorCommand").disable(true,
                        {
                            disableClass: "disabled"
                        });
                        me.dispatchEvent(
                        {
                            type: 'KeywordsNotSpecified',
                            idPreviousProject: me.parent.getPreviousIdProject.call(me)
                        });
                    }
                    else {
                        $("#ctl00_contentArea_RunMonitorCommand").disable(false);
                    }
                    chkTree.render();
                    if (profileConfig && profileConfig.idKeywordLists && profileConfig.idKeywordLists.length > 0) {
                        chkTree.setCheckedItems(profileConfig.idKeywordLists);
                        $(".KeywordsSection").unblockContent();
                        $("#ctl00_contentArea_RunMonitorCommand").disable(false);
                    }
                    else {
                        $(".KeywordsSection").blockContent();
                        $("#ctl00_contentArea_RunMonitorCommand").disable(true,
                        {
                            disableClass: "disabled"
                        });
                    }
                }, 'json');
    }

    function _showRankingMonitorReport(idRankingMonitorRun, keywordsTotal) {
        var renderer = reportRenderers.getArrayRendererByIndex(0);
        var keywordsProcessed = 0;
        var keywordsToRender = [];
        _getRecurrentRequests(idRankingMonitorRun, keywordsTotal, renderer, keywordsProcessed, keywordsToRender);
    }

    function _getRecurrentRequests(idRankingMonitorRun, keywordsTotal, renderer, keywordsProcessed, keywordsToRender) {
        console.log("_getRecurrentRequests");
        var intervalTime = 2000;
        isProcessing = true;
        setTimeout(function() {
            _getResultsFromServer(idRankingMonitorRun, keywordsTotal, renderer, keywordsProcessed, keywordsToRender);
        }, intervalTime);
    }

    function _cleanKeywordsToRender(data) {
        var cRenderer = new $.R3M.ArrayRenderer();
        cRenderer.setItemTemplate(opts.ActivityTemplate);
        $.each(data, function(i, val) {
            if (val) {
                if (val.Status) {
                    val.StatusClass = (val.Status == "F") ? "ResultError" : "SuccessClass";
                }
                if (val.Results || val.DailySearches) {
                    cRenderer.setDataSource([val]);
                    val.Activity = cRenderer.render();
                }
                else {
                    val.Activity = '-';
                }
            }
        });
    }
    function _getResultsFromServer(idRankingMonitorRun, keywordsTotal, renderer, keywordsProcessed, keywordsToRender) {
        console.log("_getResultsFromServer: go!");
        $.get(opts.RankingMonitorHandler,
                    {
                        action: "CheckProgressRankingMonitorRun",
                        idRankingMonitorRun: idRankingMonitorRun
                    }, function(data) {
                        console.log("Got a result for _getResultsFromServer");
                        $("#SummaryTable").slideDown();
                        if (!data || !(data instanceof Array)) {
                            me.finishRankingMonitorRun();
                            me.dispatchEvent({
                                type: 'RankingMonitorFailed'
                            });
                            return;
                        }
                        else {
                            for(var i=0; i<data.length; i++) {
                                console.log(data);
                                keywordsToRender.push(data[i].KeywordAnalyzed);
                                keywordsProcessed++;
                            }
                            var progress = (keywordsProcessed / keywordsTotal) * 100;
                            pb.setValue(progress);
                            _cleanKeywordsToRender(keywordsToRender);
                            console.log(keywordsToRender);
                            renderer.setDataSource(keywordsToRender);
                            renderer.dataBind();
                            if (progress < 100 && isProcessing) {
                                _getRecurrentRequests(idRankingMonitorRun, keywordsTotal, renderer, keywordsProcessed, keywordsToRender);
                            }
                            else if (isProcessing) {
                                me.finishRankingMonitorRun(function() {
                                    $.get(opts.RankingMonitorHandler,
                                        {
                                            action: "CheckLastProgressRankingMonitorRun",
                                            idRankingMonitorRun: idRankingMonitorRun
                                        }, function(data) {
                                            var summary = data.RankingMonitorDeepRunSummary;
                                            $("#RankingTotal").html(rankingTotalBase.replace(/\[PAGERANK\]/, summary.PageRank).replace(/\[INBOUND_LINKS\]/, summary.InboundLinks).replace(/\[PAGES_INDEXED\]/, summary.PagesIndexed));
                                            $("#RankingMonitorCompleted").html(competitorsTotalBase.replace(/\[COMPETITORS\]/, data.Competitors));
                                            $("#RankingMonitorCompleted").slideDown("slow");
                                            $("#RankingTotal").slideDown("slow");
                                        }, "jsonp");
                                });
                            }
                        }
                    }, "jsonp");
    }


    function _changeLabelRunCommand(isRunning) {
        $("#ctl00_contentArea_RunMonitorCommand").disable(isRunning,
                {
                    disableClass: "disabled"
                });
        $("#ctl00_contentArea_RunMonitorCommand").html(runMonitorCommandBase.replace(/\[RUN_MONITOR\]/, isRunning
                    ? opts.ProcessingText
                    : opts.RunMonitorCommandText));
        $(".CenterWrapper").fitToChildrenWidth();
    }

    function _loadProfile() {
        Sys.Services.ProfileService.load(null, _loadProjectProfile, null, null);
    }

    function _saveProfile() {
        var keywordListsProject =
                {
                    idProject: idProjectSelected,
                    idKeywordLists: chkTree.getSelected()
                };
        if (!keywordListProfile || keywordListProfile == '') {
            keywordListProfile = { "projects": [] };
        }
        var pos = _getProjectPositionFromRankingMonitorPreferences(idProjectSelected, keywordListProfile);
        if (pos >= 0) {
            keywordListProfile.projects[pos] = keywordListsProject;
        }
        else {
            keywordListProfile.projects.push(keywordListsProject);
        }
        Sys.Services.ProfileService.properties.KeywordListRankingMonitorRunPreferences = JSON.stringify(keywordListProfile);
        Sys.Services.ProfileService.save(null, null, _onProfileFailed, null);
    }

    function _getProjectPositionFromRankingMonitorPreferences(idProject, rankingMonitorPreferences) {
        for (var i = 0; i < rankingMonitorPreferences.projects.length; i++) {
            if (rankingMonitorPreferences.projects[i].idProject == idProject) {
                return i;
            }
        }
        return -1;
    }

    function _onProfileFailed(error_object, userContext, methodName) {
        var message = $.stringFormat(opts.ProfileErrorMessage, error_object.get_message());
        $.showInlineMessage($.byId('InLineMessage'), message, { sticky: true });
    }

};

$.SEOToolSet.RankingMonitorRun.inherits($.SEOToolSet.PageBase);