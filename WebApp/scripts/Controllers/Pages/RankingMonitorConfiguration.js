
$.SEOToolSet.RankingMonitorConfiguration = function(options) {
    var selectedSearchEngines;
    var lastRunDateSectionIni;
    var maximumNumberOfProxies = 4; //TODO: this must be replaced by real data (retrieved from the server, specifically subscription level)
    var maximumNumberOfSearchEngines = 5; //TODO: this must be replaced by real data (retrieved from the server, specifically subscription level)
    var proxies;
    var groupedSearchEngines;
    var keywordLists;
    var manageLinkUrlBase;
    var chkTree;
    var cbx;
    var nextRunDateBase;
    var lastDate;
    var first = true;
    var defaultConfig = false;
    var sw = null;
    var me = this; //closure
    var cbxProjectSelector = $.CurrentPage.cbxProjects;
    var opts =
        {
            RankingMonitorHandler: null,
            ProjectHandler: null,
            MessageBeforeLeaving: 'Are you sure you want to navigate away from this page?\nYour changes have not been saved for this page. Do you want to save your changes?'
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
            return;
        }
        _loadControls();

        //Bind to the ProjectChanged Event in the Base Class
        me.addEventListener('ProjectChanged', function(args) {
            var idProject = args.IdProject;
            if (idProject == -1) return;
            _projectChanged(idProject, function() { _enableIfChangesHappen(); });
        });

        me.raiseProjectChanged();
        _bindEvents();
    };

    me.restoreDefaults = function() {
        //frequency two weeks
        cbx.setValueTo(2);
        //uncheck all the proxies
        $.each(proxies, function(i, val) {
            val.Checked = false;
        });
        _renderProxies(proxies);
        $('#ProxiesList li.choices').useMaximumWidth(true);
        //check all the keyword lists
        chkTree.checkAll();
        //the default should have Google, Yahoo and Live for the United States checked.
        $.each(groupedSearchEngines, function(i, country) {
            $.each(country.SearchEnginesByCountry, function(i, searchEngine) {
                if (country.Id == 221 && (searchEngine.SearchEngine.Id == 1 || searchEngine.SearchEngine.Id == 2 || searchEngine.SearchEngine.Id == 3)) {
                    searchEngine.Checked = true;
                }
                else {
                    searchEngine.Checked = false;
                }
            });
        });
        if (defaultConfig != true && sw) sw.doCheckChanges();
        _renderSearchEngines(groupedSearchEngines);
        _showSelectedSearchEngines(groupedSearchEngines);
        $('#SearchEnginesList li').useMaximumWidth(true);
    };

    me.doSave = function(callback) {
        var idKeywordLists = [];
        var idProxies = [];
        var idSearchEngineCountries = [];
        var idFrequency = parseInt(cbx.getSelectedValue(), 10);
        defaultConfig = false;
        $.each(proxies, function(i, val) {
            if (val.Checked === true) {
                idProxies.push(val.Id);
            }
        });
        $.each(groupedSearchEngines, function(i, val) {
            $.each(val.SearchEnginesByCountry, function(i, val) {
                if (val.Checked === true) {
                    idSearchEngineCountries.push(val.Id);
                }
            });
        });
        if (idSearchEngineCountries.length < 1) {
            me.dispatchEvent(
                    {
                        type: "NoSearchEnginesSelected"
                    });
            return false;
        }
        idKeywordLists = chkTree.getSelected();
        if (idKeywordLists.length < 1) {
            me.dispatchEvent(
                    {
                        type: "NoKeywordsSelected"
                    });
            return false;
        }
        $.post(opts.RankingMonitorHandler,
            {
                action: "UpdateRankingMonitorConfiguration",
                idProject: cbxProjectSelector.getSelectedValue(),
                idFrequency: idFrequency,
                idKeywordLists: idKeywordLists,
                idProxies: idProxies,
                idSearchEngineCountries: idSearchEngineCountries
            }, function(data) {
                if (data.Result === true) {
                    if (typeof (callback) == 'function') {
                        callback();
                    }
                }
            }, "json");
    };

    me.reloadCurrenProject = function() {
        _loadProjectConfiguration(cbxProjectSelector.getSelectedValue(), function() {
            _enableIfChangesHappen();
        });
    }

    me.hasDataPendingToSave = function() {
        return !$("#ctl00_contentArea_HyperLinkCancel").hasClass("disabled");
    };

    function _loadControls() {
        chkTree = new $.R3M.Helpers.CheckedTree('#TreeFromArray');
        lastRunDateSectionIni = $("#MonitorFrequencySection p.heading").html();
        cbx = new $.R3M.Combobox('#dropdown',
        {
            width: 185
        });
        cbx.setTextField('{Name}').setValueField('{Id}').setItemTemplate("<div cbx_value='[VALUE]' title='[TEXT]' ><span>[TEXT]</span></div>");

        manageLinkUrlBase = $("#ManageKeywordsCommand").attr("href");
        $("#ctl00_contentArea_HyperLinkCancel").click(function(e) {
            if (defaultConfig == true) {
                location.reload(false);
            }
            else {
                me.dispatchEvent(
                    {
                        type: "CancellingConfiguration"
                    });
            }
            return false;
        });
        $("#ProxyServersSection p.heading").html($("#ProxyServersSection p.heading").html().replace(/\[MAXIMUM_PROXIES\]/g, maximumNumberOfProxies));
    }

    function _projectChanged(idProjectSelected, callback) {
        $("#MonitorKeywordLists, #MonitorProxies, #MonitorSearchEnginesCountries").hide();
        $("#ManageKeywordsCommand").attr("href", manageLinkUrlBase + idProjectSelected);
        $("#ctl00_contentArea_HyperLinkSave, #ctl00_contentArea_HyperLinkCancel").addClass('disabled');
        defaultConfig = false;
        _loadProjectConfiguration(idProjectSelected, callback);
    }

    function _loadProjectConfiguration(idProjectSelected, callback) {
        var interval = null;
        var projectConfig = null;
        keywordLists = null;
        proxies = null;
        groupedSearchEngines = null;

        //Loading the profile data
        $.post(opts.RankingMonitorHandler,
        {
            action: 'GetRankingMonitorConfiguration',
            idProject: idProjectSelected
        }, function(config) {
            // Start the loading of data from server -------------------------------------------------------
            $.post(opts.RankingMonitorHandler, {
                action: 'GetRankingMonitorConfigurationStructure',
                idProject: idProjectSelected
            }, function(structure) {
                keywordLists = structure.KeywordLists;
                chkTree.setDataSource(keywordLists);
                if (keywordLists.length < 1) {
                    try {
                        me.dispatchEvent(
                        {
                            type: 'KeywordsNotSpecified',
                            idPreviousProject: me.parent.getPreviousIdProject.call(me)
                        });
                    } catch (ex) {
                        console.log('Error: ' + ex.message);
                    }
                    return;
                }
                chkTree.render();
                var idKeywordLists = [];
                $.each(config.MonitorKeywordList, function(i, val) {
                    idKeywordLists.push(val.KeywordList.IdKeywordList);
                });
                if (config && config.MonitorKeywordList && config.MonitorKeywordList.length > 0) {
                    chkTree.setCheckedItems(idKeywordLists);
                }
                else {
                    chkTree.checkAll(false);
                }
                $("#MonitorKeywordLists").show();
                proxies = structure.ProxyServers;
                if (config && config.MonitorProxyServer.length > 0) {
                    $.each(proxies, function(i, val) {
                        $.each(config.MonitorProxyServer, function(i, profileProxy) {
                            if (val.Id == profileProxy.ProxyServer.Id) val.Checked = true;
                        });
                    });
                }
                _renderProxies(proxies);
                $("#MonitorProxies").show();
                $('#ProxiesList li.choices').useMaximumWidth(true);

                groupedSearchEngines = _groupSearchEnginesByCountry(structure.SearchEngineCountry);
                var idSearchEnginesCountries = [];
                if (config !== null && config.MonitorSearchEngineCountry !== null && config.MonitorSearchEngineCountry.length > 0) {
                    $.each(config.MonitorSearchEngineCountry, function(i, val) {
                        idSearchEnginesCountries.push(val.SearchEngineCountry.Id);
                    });
                }
                else {
                    //the default should have Google, Yahoo and Live for the United States checked. 
                    $.each(groupedSearchEngines, function(i, country) {
                        if (country.Id == 221) {
                            $.each(country.SearchEnginesByCountry, function(i, searchEngine) {
                                if (searchEngine.SearchEngine.Id == 1 || searchEngine.SearchEngine.Id == 2 || searchEngine.SearchEngine.Id == 3) {
                                    idSearchEnginesCountries.push(searchEngine.Id);
                                }
                            });
                        }
                    });
                }
                $.each(groupedSearchEngines, function(i, country) {
                    $.each(country.SearchEnginesByCountry, function(i, searchEngine) {
                        $.each(idSearchEnginesCountries, function(i, profileSearchEngineId) {
                            if (searchEngine.Id == profileSearchEngineId) searchEngine.Checked = true;
                        });
                    });
                });
                _renderSearchEngines(groupedSearchEngines);
                _showSelectedSearchEngines(groupedSearchEngines);
                $("#MonitorSearchEnginesCountries").show();
                $('#SearchEnginesList li').useMaximumWidth(true);

                $.each(structure.Frequency, function(i, val) {
                    val.Name = $.getResourceString("Frequency" + val.Name, val.Name);
                });
                cbx.setDataSource(structure.Frequency).dataBind();
                if (config && config.Frequency) {
                    cbx.setValueTo(config.Frequency.Id);
                }
                else {
                    cbx.setValueTo(2, false);
                    defaultConfig = true;
                }

                var lastRun = structure.LastRun;
                var wasNeverRun = lastRun == null;
                $("#MonitorFrequencySection p.heading").html(lastRunDateSectionIni.replace(/\[LAST_DATE\]/g, wasNeverRun ? 'N/A' : lastDate = lastRun.LastDate).replace(/\[LAST_TIME\]/g, wasNeverRun ? '' : lastRun.LastTime));
                nextRunDateBase = $("#MonitorFrequencySection p.heading").html();
                if (wasNeverRun) lastDate = "";
                _frequencyChanged(cbx.getSelectedValue, lastDate);

                if (callback) callback();

                $('div.HeadingDates').show();

                //end loading data from server -----------------------------------------------------------------------
            }, "json");
        }, "json");
    }

    function _enableIfChangesHappen() {
        if (!first) {
            if (sw) {
                sw.acceptChanges();
                if (defaultConfig == true) {
                    $("#ctl00_contentArea_HyperLinkSave").removeClass('disabled');
                    return;
                }
                sw.doCheckChanges();
            }
            return;
        }
        if (defaultConfig == true) {
            $("#ctl00_contentArea_HyperLinkSave").removeClass('disabled');
            return;
        }
        sw = $("#ctl00_contentArea_HyperLinkSave, #ctl00_contentArea_HyperLinkCancel").enableIfChangeHappens(".Section",
        {
            disableClass: 'disabled',
            onCalculate: function(args) {
                args.extraValue = cbx.getSelectedValue() + chkTree.getSelected().join(',');
            }
        });
        sw.addEventListener('StateChanged', function(args) {
            if (defaultConfig == true) {
                return;
            }
            window.onbeforeunload = _showMessageBeforeLeave;
        });
        sw.addEventListener('ReturnedToInitialState', function() {
            window.onbeforeunload = null;
        });
        cbx.addEventListener('Changed', function() {
            if (defaultConfig != true && sw) sw.doCheckChanges();
        });
        chkTree.addEventListener('CheckedStateChanged', function() {
            if (defaultConfig != true && sw) sw.doCheckChanges();
        });
        sw.acceptChanges();
        sw.doCheckChanges();
        first = false;
    }

    function _bindEvents() {
        cbxProjectSelector.addEventListener('Changing', function(args) {
            if (me.hasDataPendingToSave()) {
                args.cancel = true;
                me.dispatchEvent(
                    {
                        type: "ChangingProjectWithChangesNotSaved",
                        idProject: args.itemJNode.attr('cbx_value')
                    });
                return false;
            }
        });

        cbx.addEventListener('Changed', function(args) {
            _frequencyChanged(args.newValue, lastDate);
            args.cancel = true;
            //return false; // prevent the other listeners to be raised.
        });

        $('.ProxyCheck').live('click', function(e) {
            var id = $(this).val();
            var selectedProxies = $("#ProxiesList input:checked").length;
            var proxy = $.arrayFind(proxies, function(val) {
                return val.Id == id
            });
            proxy.Checked = selectedProxies > maximumNumberOfProxies ? false : $(this).attr('checked');
            _renderProxies(proxies);
            $('#ProxiesList li.choices').useMaximumWidth(true);
            if (defaultConfig != true && sw) sw.doCheckChanges();
        });

        $("#CheckAllKeywordsListCommand").click(function(e) {
            chkTree.checkAll();
            return false;
        });
        $("#UncheckAllKeywordsListCommand").click(function(e) {
            chkTree.checkNone();
            return false;
        });

        $("#UncheckAllSearchEnginesCommand").click(function(e) {
            $("#SearchEnginesList input:checked").removeAttr("checked");
            $.each(groupedSearchEngines, function(i, val) {
                $.each(val.SearchEnginesByCountry, function(i, val) {
                    val.Checked = false;
                });
            });
            _showSelectedSearchEngines(groupedSearchEngines);
            if (defaultConfig != true && sw) sw.doCheckChanges();
            return false;
        });

        $("#ShowSelectedSearchEngines").click(function(e) {
            var temporarySearchEngines = $.arrayFilter(groupedSearchEngines, function(ele) {
                if (typeof ele.hasSelected == 'undefined') {
                    ele.hasSelected = function() {
                        var selectedSearchEngines = 0;
                        $.each(this.SearchEnginesByCountry, function(i, val) {
                            if (val.Checked) selectedSearchEngines++;
                        });
                        return selectedSearchEngines > 0;
                    }
                }
                return ele.hasSelected();
            });
            _renderSearchEngines(temporarySearchEngines);
            $('#SearchEnginesList li').useMaximumWidth(true);
            return false;
        });


        $("#FilterByCountryTextBox").blur(function() {
            $(this).val("");
        });

        $("#ShowAllSearchEngines").click(function(e) {
            _renderSearchEngines(groupedSearchEngines);
            $('#SearchEnginesList li').useMaximumWidth(true);
            return false;
        });

        $("#ctl00_contentArea_HyperLinkSave").click(function() {
            me.doSave(function() {
                _enableIfChangesHappen();
                $("#feedbackMsg").fadeIn(500, function() {
                    var ele = $(this);
                    setTimeout(function() {
                        ele.fadeOut("slow");
                    }, 2000);
                });
            });
            return false;
        });

        $("#SearchEnginesList input:checkbox").live('click', function(e) {
            var id = $(this).val();
            var countryId = $(this).attr('country');
            var country = $.arrayFind(groupedSearchEngines, function(val) {
                return val.Id == countryId;
            });
            if (country == null)
                return;
            var searchEngine = $.arrayFind(country.SearchEnginesByCountry, function(val) {
                return val.Id == id;
            });
            searchEngine.Checked = $(this).attr('checked');
            _showSelectedSearchEngines(groupedSearchEngines);
            if (defaultConfig != true && sw) sw.doCheckChanges();
        });

        $("#ctl00_contentArea_HyperLinkRestoreDefaults").click(function() {
            if (defaultConfig != true && me.hasDataPendingToSave()) {
                me.dispatchEvent(
                    {
                        type: "DiscardingWithChangesNotSaved"
                    });
            }
            else {
                me.restoreDefaults();
            }
            return false;
        });

        $("#SearchEnginesList input:checked .disabled").live("click", function() {
            $(this).removeAttr('checked');
        });

        //    $("#SearchEnginesList :checkbox").live("click", function() {
        //        var act = $(this).attr('checked');
        //        if (act) {
        //            if (selectedSearchEngines >= maximumNumberOfSearchEngines) {
        //                $(this).attr('checked', false);
        //                //$("#SearchEnginesList ~(input[checked])").addClass("disabled");
        //                return;
        //            }
        //            selectedSearchEngines++;
        //        }
        //        else {
        //            selectedSearchEngines--;
        //            if (selectedSearchEngines < maximumNumberOfSearchEngines) {
        //                console.log("searchengines: " + selectedSearchEngines);
        //                //_renderSearchEngines(groupedSearchEngines);
        //            }
        //            else {
        //                return;
        //            }
        //        }
        //        var idCountry = $(this).attr("country");
        //        var id = $(this).val();
        //        var country = $.arrayFind(groupedSearchEngines, function(country) {
        //            return country.Id == idCountry;
        //        });
        //        country = country || { SearchEnginesByCountry: [] };
        //        var currentSearchEngine = $.arrayFind(country.SearchEnginesByCountry, function(se) {
        //            return se.Id == id;
        //        });
        //        currentSearchEngine.Checked = selectedSearchEngines > maximumNumberOfSearchEngines ? false : $(this).attr('checked');

        //        return;      
        //    });

        $("#FilterByCountryTextBox").keyup(function(e) {
            var q = $(this).val().toUpperCase();
            var interval = null;
            clearTimeout(interval);
            interval = setTimeout(function() {
                var temporarySearchEngines = $.arrayFilter(groupedSearchEngines, function(val) {
                    return (val.Name.toUpperCase().indexOf(q) == 0)
                });
                _renderSearchEngines(temporarySearchEngines);
                $('#SearchEnginesList li').useMaximumWidth(true);
            }, 100);
        });
    }

    function _frequencyChanged(idFrequency, lastRunDate) {
        $.post(opts.RankingMonitorHandler,
        {
            action: 'GetNextScheduledRunDate',
            lastRankingMonitorRunDate: lastRunDate,
            idFrequency: idFrequency
        }, function(data) {
            $("#MonitorFrequencySection p.heading").html(nextRunDateBase.replace(/\[NEXT_DATE\]/g, data.NextRun));
        }, 'json');
    }

    function _renderProxies(proxies) {
        var proxyTemplate = "<input type='checkbox' id='proxies_[ID_PROXY]' value='[ID_PROXY]' [CHECKED] class='ProxyCheck [DISABLED]'/><span class='ImgWrapper'><img src='[FLAG_COUNTRY]' height='24' width='30' class='[DISABLED] BigFlag' /></span><label for='proxies_[ID_PROXY]'>[CITY], [COUNTRY]</label>";
        var proxiesList = $("#ProxiesList").empty();
        var selectedProxies = $.arrayFilter(proxies, function(proxy) {
            proxy.Checked = proxy.Checked || false;
            return proxy.Checked == true;
        }).length;
        proxies.sort(function(a, b) {
            if (a.Importance < b.Importance)
                return 1;
            if (a.Importance > b.Importance)
                return -1;
            return 0;
        });
        $.each(proxies, function(i, val) {
            var proxyTpl = "<li class='choices' >{0}</li>";
            val.Disabled = false;
            if (selectedProxies >= maximumNumberOfProxies) val.Disabled = !val.Checked;
            var proxyHtml = proxyTemplate
            .replace(/\[CITY\]/g, val.City)
            .replace(/\[COUNTRY\]/g, val.Country.Name)
            .replace(/\[FLAG_COUNTRY\]/g, val.Country.FlagUrl)
            .replace(/\[ID_PROXY\]/g, val.Id)
            .replace(/\[CHECKED\]/g, val.Checked ? "checked='checked'" : "")
            .replace(/\[DISABLED\]/g, val.Disabled ? "disabled" : "");

            var proxyTag = $.stringFormat(proxyTpl, proxyHtml);
            proxiesList.append(proxyTag);
        });
    }

    /*
    * This function return a JSON array with the countries and inside them, the search engines by countries
    */
    function _groupSearchEnginesByCountry(datasource) {
        var countries = [];
        if (!datasource)
            return null;
        //Order by country
        datasource.sort(function(a, b) {
            if (a.Country.SearchEngineImportance == b.Country.SearchEngineImportance) {
                if (a.Country.Name > b.Country.Name)
                    return 1;
                else if (a.Country.Name < b.Country.Name)
                    return -1;
                else
                    return 0;
            }
            else if (a.Country.SearchEngineImportance > b.Country.SearchEngineImportance)
                return -1
            else if (a.Country.SearchEngineImportance < b.Country.SearchEngineImportance)
                return 1
        });
        //Group by country
        $.each(datasource, function(i, val) {
            var countriesLength = countries.length;
            var searchEngineByCountry =
            {
                Id: val.Id,
                SearchEngine: val.SearchEngine,
                Url: val.Url
            };
            var country = val.Country;
            var isNewCountry = countriesLength == 0 || country.Id != countries[countriesLength - 1].Id;
            if (isNewCountry)
                country.SearchEnginesByCountry = [];
            else
                country = countries[countriesLength - 1];

            country.SearchEnginesByCountry.push(searchEngineByCountry);
            if (isNewCountry) countries.push(country);
        });
        return countries;
    }

    function _renderSearchEngines(datasource) {
        var searchEnginesList = $("#SearchEnginesList").empty();
        selectedSearchEngines = 0;
        $.each(groupedSearchEngines, function(i, ele) {
            $.each(ele.SearchEnginesByCountry, function(i, val) {
                val.Checked = val.Checked || false;
                if (val.Checked) selectedSearchEngines++;
            });
        });
        //first level: countries
        $.each(datasource, function(i, val) {
            var searchEngines1stLevelTemplate = "<li><div><img src='[FLAG_COUNTRY]' class='Country'/><span class='Name'>[COUNTRY_NAME]</span></div><ul class='Engines'>";
            var firstLevel = searchEngines1stLevelTemplate.replace(/\[FLAG_COUNTRY\]/g, val.FlagUrl).replace(/\[COUNTRY_NAME\]/g, val.Name);
            var secondLevel = "";
            var countryId = val.Id;
            //second level: search engines
            $.each(val.SearchEnginesByCountry, function(i, val) {
                var searchEngines2ndLevelTemplate = "<li><input type='checkbox' value=[ID_SEARCHENGINE] id='searchEngines_[ID_SEARCHENGINE]' [CHECKED] country='[COUNTRY_ID]' [DISABLED] />" +
                "<img src='[LOGO_SEARCHENGINE]' title='[URL]' class='[DISABLED] Engine' /><label for='searchEngines_[ID_SEARCHENGINE]' title='[URL]'>[SEARCHENGINE_NAME]</label></li>";
                val.Disabled = false;
                if (selectedSearchEngines >= maximumNumberOfSearchEngines)
                    val.Disabled = !val.Checked
                secondLevel += searchEngines2ndLevelTemplate
                .replace(/\[ID_SEARCHENGINE\]/g, val.Id)
                .replace(/\[LOGO_SEARCHENGINE\]/g, val.SearchEngine.SearchEngineUrl)
                .replace(/\[SEARCHENGINE_NAME\]/g, val.SearchEngine.SearchEngineName)
                .replace(/\[CHECKED\]/g, val.Checked ? "checked='checked'" : "")
                .replace(/\[COUNTRY_ID\]/g, countryId).replace(/\[URL\]/g, val.Url)
                .replace(/\[DISABLED\]/g, "");
                //TODO: Replace the disabled token with this, in order to disabling the excess of search engines
                //.replace(/\[DISABLED\]/g, val.Disabled ? "disabled" : ""));
            });
            firstLevel += secondLevel + "</ul></li>";
            searchEnginesList.append(firstLevel);
        });
    }

    function _showSelectedSearchEngines(datasource) {
        var checkedSearchEngines = 0;
        $.each(datasource, function(i, val) {
            $.each(val.SearchEnginesByCountry, function(i, val) {
                if (val.Checked == true) checkedSearchEngines++;
            });
        });
        $("#selectedSearchEngines").html($.stringFormat($.getResourceString("ItemsSelected", "{0} selected"), checkedSearchEngines));
    }

    function _showMessageBeforeLeave(e) {
        /*
        $.showBigDialog('<p>Your changes have not been saved for this project’s Configure Ranking Monitor. Do you want to save your changes?</p>',
        {
        title: 'Warning',
        icon: $.MessageIco.Warning,
        buttons: [
        {
        text: 'Save Changes',
        command: 'SaveChanges'
        },
        {
        text: 'Discard Changes',
        command: 'DiscardChanges'
        }],
        showOk: false,
        showCancel: true,
        onCommand: function(args)
        {
        if (args.command == 'SaveChanges')
        {
        me.doSave();
        }
        }
                 
        });
        return false;
        */
        var msg = opts.MessageBeforeLeaving;
        if (e) {
            e.returnValue = msg;
            return false;
        }
        else {
            return msg;
        }
    }

};

$.SEOToolSet.RankingMonitorConfiguration.inherits($.SEOToolSet.PageBase);
