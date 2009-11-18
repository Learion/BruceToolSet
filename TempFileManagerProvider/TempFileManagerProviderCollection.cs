#region Using Directives

using System;
using System.Configuration.Provider;

#endregion

namespace SEOToolSet.TempFileManagerProvider
{
    public class TempFileManagerProviderCollection : ProviderCollection
    {
        public new TempFileManagerProviderBase this[string name]
        {
            get { return (TempFileManagerProviderBase) base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider", "The provider parameter cannot be null.");

            if (!(provider is TempFileManagerProviderBase))
                throw new ArgumentException("The provider parameter must be of type TempFileManagerProviderBase.",
                                            "provider");

            base.Add(provider);
        }


        public void CopyTo(TempFileManagerProviderBase[] array, int index)
        {
            base.CopyTo(array, index);
        }
    }
}