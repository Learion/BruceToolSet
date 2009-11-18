﻿#region Using Directives

using System.Diagnostics;
using System.Windows;
using System.Windows.Browser;

#endregion

namespace SEOToolSet.Silverlight.Reports
{
    public partial class App
    {
        public App()
        {
            Startup += Application_Startup;
            //Exit += Application_Exit;
            UnhandledException += Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //e.InitParams[""];
            RootVisual = new Page(e.InitParams["ParentHost"]);
        }


        private static void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (Debugger.IsAttached) return;

            // NOTE: This will allow the application to continue running after an exception has been thrown
            // but not handled. 
            // For production applications this error handling should be replaced with something that will 
            // report the error to the website and stop the application.
            e.Handled = true;

            Deployment.Current.Dispatcher.BeginInvoke(() => ReportErrorToDOM(e));
        }

        private static void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            var errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
            errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

            HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight 2 Application " + errorMsg +
                                 "\");");
        }
    }
}