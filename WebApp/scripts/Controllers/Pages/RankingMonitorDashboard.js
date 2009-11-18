
$.SEOToolSet.RankingMonitorDashboard = function(options) {
    var summaryHtmlConst;
    var summaryHtml;
    var summary;
    var me = this;
    var opts =
        {
            RankingMonitorHandler: null,
            ProjectHandler: null,
            ChartTooltip: '',
            NoRankingMonitorRun: ''
        };

    $.extend(opts, options);

    //Call the constructor of the Base
    me.parent.constructor.call(me);

    /**
    * Init the Job of the Class
    */
    me.init = function() {
        summary = $("#SummarySection");
        summaryHtml = summary.html();
        summaryHtmlConst = summaryHtml;

        //call the method from the base class
        me.parent.init.call(me);

        //If no project is selected, dispatch the Event in the Base Class
        if (!me.CheckValidProject()) {
            return;
        }

        //Bind to the ProjectChanged Event in the Base Class         				
        me.addEventListener('ProjectChanged', function(args) {
            projectChanged(args.IdProject);
        });
        projectChanged(me.getCurrentIdProject());
    };

    function projectChanged(idProjectSelected) {
        //Clearing the page
        $("#SummaryMediaWrapper").hide();
        $("#Summary").slideUp();
        summaryHtml = summaryHtmlConst;
        $.post(opts.RankingMonitorHandler, { action: 'GetLastRankingMonitorRunDate', idProject: idProjectSelected },
                    function(data) {
                        var lastRun = data.LastRun;
                        if (lastRun == 'N/A') {                            
                            summary.html(opts.NoRankingMonitorRun.replace(/\[PROJECT_NAME\]/, me.getCurrentProjectName()));
                        }
                        else {
                            summaryHtml = summaryHtml.replace(/\[LAST_RUN_DATE\]/, lastRun.LastDate);
                            summaryHtml = summaryHtml.replace(/\[LAST_RUN_TIME\]/, lastRun.LastTime);
                            summary.html(summaryHtml);
                            summaryHtml = summaryHtml.replace(/\[PROJECT_NAME\]/, me.getCurrentProjectName());
                            summary.html(summaryHtml);
                        }
                        $("#SummarySection").show();
                        $("#Summary").slideDown();
                    }, 'json');

        $.post(opts.RankingMonitorHandler, { action: 'GetTopKeywords', idProject: idProjectSelected, quantity: 5 },
                    function(data) {
                        var topKeywordsList = $("#TopKeywordsContainer ol").empty();
                        if (data && data.length > 0) {
                            $.each(data, function(i, val) {
                                var keywordListItem = $("<li />");
                                keywordListItem.text(val);
                                topKeywordsList.append(keywordListItem);
                            });
                        }
                    }, 'json');
        $.post(opts.RankingMonitorHandler, { action: 'GetRankingChartUri', idProject: idProjectSelected },
                    function(data) {
                        var rankingChartSection = $("#RankingChartContainer");
                        $("#RankingChartContainer img").remove();
                        if (data && data.ChartUrl != "N/A") {
                            var chartImg = $("<img />");
                            chartImg.attr("src", data.ChartUrl);
                            chartImg.attr("title", opts.ChartTooltip);
                            rankingChartSection.append(chartImg);
                            $("#SummaryMediaWrapper").show();
                        }
                    }, 'json');
    }
};

$.SEOToolSet.RankingMonitorDashboard.inherits($.SEOToolSet.PageBase);