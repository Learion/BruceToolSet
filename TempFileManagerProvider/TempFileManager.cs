#region Using Directives

using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Globalization;
using System.Timers;
using System.Web.Configuration;
using LoggerFacade;

#endregion

namespace SEOToolSet.TempFileManagerProvider
{
    public static class TempFileManager
    {
        private static readonly TempFileManagerProviderBase defaultProvider;

        private static readonly TempFileManagerProviderCollection providerCollection =
            new TempFileManagerProviderCollection();

        private static readonly Timer providerTimer = new Timer();

        static TempFileManager()
        {
            var qc = ConfigurationManager.GetSection("TempFileManagerProvider") as TempFileManagerProviderConfiguration;

            if (qc == null || qc.DefaultProvider == null || qc.Providers == null || qc.Providers.Count < 1)
                throw new ProviderException("You must specify a valid default provider for TempFileManagerProvider.");


            //Instantiate the providers
            ProvidersHelper.InstantiateProviders(qc.Providers, providerCollection, typeof (TempFileManagerProviderBase));
            providerCollection.SetReadOnly();
            defaultProvider = providerCollection[qc.DefaultProvider];

            if (defaultProvider != null && defaultProvider.AutoStartPurge && defaultProvider.MaxAgeOfFilesInMinutes > 0)
            {
                try
                {
                    //Create the Timer
                    Log.Debug(typeof (TempFileManager),
                              "Trying to create the Peridic Task to delete Temporary Files");
                    providerTimer.Elapsed += providerTimer_Elapsed;
                    providerTimer.Interval = defaultProvider.MaxAgeOfFilesInMinutes*60*1000;
                    providerTimer.Start();
                }
                catch (Exception ex)
                {
                    throw new ProviderCannotStartThePeriodicPurgeTaskException(
                        "The Periodic Task could not be started.", ex);
                }
                return;
            }
            var defaultProviderProp = qc.ElementInformation.Properties["defaultProvider"];
            if (defaultProviderProp != null)
            {
                throw new ConfigurationErrorsException(
                    "You must specify a default provider for the TempFileManagerProvider.",
                    defaultProviderProp.Source,
                    defaultProviderProp.LineNumber);
            }
        }

        public static TempFileManagerProviderBase Provider
        {
            get { return defaultProvider; }
        }

        public static TempFileManagerProviderCollection Providers
        {
            get { return providerCollection; }
        }

        #region ProviderMethods

        public static string SaveFile(byte[] binaryData)
        {
            if (Provider.CanWrite())
            {
                return Provider.SaveFile(binaryData);
            }
            throw new ProviderHasNotReadWritePermissionException("The repository is not writeable");
        }

        public static byte[] GetFileById(string fileId)
        {
            return Provider.GetFileById(fileId);
        }

        public static void DeleteFile(string fileId)
        {
            Provider.DeleteFile(fileId);
        }

        public static void PurgeOldFiles(TimeSpan maxAge)
        {
            if (maxAge.Minutes > 0)
            {
                Provider.PurgeOldFiles(maxAge);
            }
            //No Purge Work, the maxAge.Minutes must be greater than one Minute
        }

        public static bool CanWrite()
        {
            return Provider.CanWrite();
        }

        #endregion

        private static void providerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Log.Debug(typeof (TempFileManager),
                      String.Format(CultureInfo.InvariantCulture, "PeriodicTask Called at : {0}, TimeElapsed : {1}",
                                    DateTime.Now.ToString("MM/dd/yyyy - HH:mm:ss", CultureInfo.InvariantCulture),
                                    Provider.MaxAgeOfFilesInMinutes));
            var maxAgeOfFilesInTimeSpan = new TimeSpan(0, Provider.MaxAgeOfFilesInMinutes, 0);
            Provider.PurgeOldFiles(maxAgeOfFilesInTimeSpan);
        }

        public static void StopTimer()
        {
            if (providerTimer != null)
            {
                providerTimer.Stop();
            }
        }
    }
}