using System;
using System.Threading;
using System.Web;
using SEOToolSet.Common;
using SEOToolSet.Entities;
using SEOToolSet.Entities.Wrappers;
using SEOToolSet.Providers;

namespace SEOToolSet.WebApp.Handler.Async
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    public class RunMonitor : IHttpAsyncHandler
    {
        static readonly DevelopMentor.ThreadPool ThreadPool;
        private readonly object _syncHelper = new object();

        static RunMonitor()
        {
            // Issue: the first request to hit this handler boostraps the
            //        new appdomain & authentication scheme; the result of which
            //        is then used to initialize the thread pool.  This means the
            //        thread identity of the initial threads (2 in the code below)
            //        is equivalent to the thread identity used for this first request.
            //        Similarly, any dynamically added threads will inherit the
            //        identity of the thread that happens to trigger the new thread
            //        being added to the pool.
            //
            ThreadPool = new DevelopMentor.ThreadPool(2, 5, "RunMonitor Pool");
            
               
            
            ThreadPool.Start();
        }

        // IHttpHandler
        //
        public bool IsReusable { get { return (true); } }

        public void ProcessRequest(HttpContext ctx)
        {
            //ctx.Response.Output.Write("*** Should not see this ***<br/>");
        }

        // IHttpAsyncHandler
        //
        public IAsyncResult BeginProcessRequest(HttpContext ctx, AsyncCallback cb, object extraData)
        {
            //BreakIfRequested();

            ctx.Response.Buffer = false;

           /* ctx.Response.Output.Write("IHttpAsyncHandler.BeginProcessRequest called on thread {0}<br/>",
                                       Thread.CurrentThread.ManagedThreadId);

            ctx.Response.Output.Write("HttpContext.Current = {0}<br/>", (HttpContext.Current == null ? "n/a" : HttpContext.Current.ToString()));
            * */

            var reqState = new AsyncRequestState(ctx, cb, extraData);
            ThreadPool.PostRequest(new DevelopMentor.WorkRequestDelegate(ProcessRequest), reqState);
            return (reqState);
        }

        public void EndProcessRequest(IAsyncResult ar)
        {
            var reqState = (AsyncRequestState)ar;

            /*reqState._ctx.Response.Output.Write("IHttpAsyncHandler.EndProcessRequest called on thread {0}<br/>",
                                                Thread.CurrentThread.ManagedThreadId);*/
        }

        void ProcessRequest(object state, DateTime requestTime)
        {
            var reqState = (AsyncRequestState)state;
            var ctx = reqState._ctx;

            //ctx.Response.Output.Write("Handler.ProcessRequest called on thread {0}<br/>",
            //                           Thread.CurrentThread.ManagedThreadId);
            //ctx.Response.Output.Write("HttpContext.Current = {0}<br/>", (HttpContext.Current == null ? "n/a" : HttpContext.Current.ToString()));
            //ctx.Response.Output.Write("IHttpHandler.ProcessRequest HttpContext principal: {0}<br/>",
            //                           ctx.User.Identity.Name);
            //ctx.Response.Output.Write("IHttpHandler.ProcessRequest thread principal: {0}<br/>",
            //                           Thread.CurrentPrincipal.Identity.Name);
            //ctx.Response.Output.Write("Handler.ProcessRequest started sleeping at {0}<br/>", DateTime.Now);

            //for (var n = 0; n < 5; n++)
            //{
            //    Thread.Sleep(1000);
            //    ctx.Response.Output.Write("Handler.ProcessRequest still sleeping at {0}<br/>", DateTime.Now);
            //}

            //ctx.Response.Output.Write("Handler.ProcessRequest finished sleeping at {0}<br/>", DateTime.Now);


            var idRankingMonitorRun = ctx.Request.QueryString["idRankingMonitorRun"];
            if (idRankingMonitorRun != null)
            {
                var rand = new Random((int)(DateTime.Now.Ticks - int.MaxValue));
                //NOTE: replace this section with real data retrieved from web service
                var keywordAnalysisToUpdate = RankingMonitorManager.GetKeywordAnalysis(int.Parse(idRankingMonitorRun));
                var rankingMonitorRunAnalyzed =
                    RankingMonitorManager.GetRankingMonitorRun(int.Parse(idRankingMonitorRun));
                var searchEnginesCountriesTotal =
                    rankingMonitorRunAnalyzed.Project.RankingMonitorConfiguration.MonitorSearchEngineCountry.Count;

                foreach (var keywordAnalysis in keywordAnalysisToUpdate)
                {
                    if (keywordAnalysis.Status != "P")
                    {
                        continue;
                    }
                    lock (_syncHelper)
                    {
                        var keywordAnalysisCheck = RankingMonitorManager.GetSingleKeywordAnalysis(keywordAnalysis.Id);
                        if (keywordAnalysisCheck.Status == "C")
                        {
                            AssignValues(keywordAnalysis, keywordAnalysisCheck.GoogleResults, keywordAnalysisCheck.Pages,
                                         keywordAnalysisCheck.Results, keywordAnalysisCheck.AliasDomains,
                                         keywordAnalysisCheck.AllInTitle, keywordAnalysisCheck.CPC,
                                         keywordAnalysisCheck.DailySearches, keywordAnalysisCheck.Engines);
                        }
                        else
                        {
                            if (rand.Next(100) > 5)
                            {
                                AssignValues(keywordAnalysis, rand.Next(1000, 1000000000), rand.Next(1, 20),
                                             rand.Next(400, 500000000), rand.Next(15, 2000), rand.Next(15, 2400),
                                             rand.NextDouble() * 10 + 0.5, rand.Next(10, 20000),
                                             rand.Next(1, searchEnginesCountriesTotal));
                                keywordAnalysis.Status = "C";
                            }
                            else
                            {
                                keywordAnalysis.Status = "F";
                            }
                            RankingMonitorManager.UpdateKeywordAnalysis(keywordAnalysis);
                        }
                        //NOTE: This value could be changed in order to simulate a heavy process
                        Thread.Sleep(2000);

                    }
                }



            }
            // This triggers the asp.net plumbing to call our
            // IHttpAsyncHandler.EndProcessRequest method.
            //
            reqState.CompleteRequest();
            reqState._cb(reqState);
        }
        private void AssignValues(AbstractKeywordAnalysis keywordAnalysis, int? googleResults, int? pages, int? results, int? aliasDomains, int? allInTitle, double? cpc, int? dailySearches, int? engines)
        {
            keywordAnalysis.AliasDomains = aliasDomains;
            keywordAnalysis.AllInTitle = allInTitle;
            keywordAnalysis.CPC = cpc;
            keywordAnalysis.DailySearches = dailySearches;
            keywordAnalysis.GoogleResults = googleResults;
            keywordAnalysis.Results = results;
            keywordAnalysis.Pages = pages;
            keywordAnalysis.Engines = engines;
        }

        class AsyncRequestState : IAsyncResult
        {
            public AsyncRequestState(HttpContext ctx, AsyncCallback cb, object extraData)
            {
                _ctx = ctx;
                _cb = cb;
                _extraData = extraData;
            }

            internal readonly HttpContext _ctx;
            internal readonly AsyncCallback _cb;
            private readonly object _extraData;
            private bool _isCompleted;
            private ManualResetEvent _callCompleteEvent;

            internal void CompleteRequest()
            {
                _isCompleted = true;

                lock (this)
                {
                    if (_callCompleteEvent != null)
                    {
                        _callCompleteEvent.Set();
                    }
                }
            }

            // IAsyncResult
            //
            public object AsyncState { get { return (_extraData); } }
            public bool CompletedSynchronously { get { return (false); } }

            public bool IsCompleted { get { return (_isCompleted); } }

            public WaitHandle AsyncWaitHandle
            {
                get
                {
                    lock (this)
                    {
                        if (_callCompleteEvent == null)
                        {
                            _callCompleteEvent = new ManualResetEvent(false);
                        }

                        return (_callCompleteEvent);
                    }
                }
            }
        }
    }

}
