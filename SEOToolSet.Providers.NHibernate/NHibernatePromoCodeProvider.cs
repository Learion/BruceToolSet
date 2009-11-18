using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using NHibernateDataStore.Common;
using SEOToolSet.DAL;
using SEOToolSet.Entities;

namespace SEOToolSet.Providers.NHibernate
{
    ///<summary>
    ///Implementation of the <see cref="PromoCodeProviderBase"/> for <see cref="NHibernate"/>
    ///</summary>
    public class NHibernatePromoCodeProvider : PromoCodeProviderBase
    {
        private string _connName;
        private string _providerName;

        ///<summary>
        ///
        ///                    Gets the friendly name used to refer to the provider during configuration.
        ///                
        ///</summary>
        ///
        ///<returns>
        ///
        ///                    The friendly name used to refer to the provider during configuration.
        ///                
        ///</returns>
        ///
        public override string Name
        {
            get { return _providerName; }
        }


        ///<summary>
        ///
        ///                    Initializes the provider.
        ///                
        ///</summary>
        ///
        ///<param name="name">
        ///                    The friendly name of the provider.
        ///                </param>
        ///<param name="config">
        ///                    A collection of the name/value pairs representing the
        ///                    provider-specific attributes specified in the configuration
        ///                    for this provider.
        ///                </param>
        ///<exception cref="T:System.ArgumentNullException">
        ///                    The name of the provider is null.
        ///                </exception>
        ///<exception cref="T:System.ArgumentException">
        ///                    The name of the provider has a length of zero.
        ///                </exception>
        ///<exception cref="T:System.InvalidOperationException">
        ///                    An attempt is made to call 
        ///                    <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)" />
        ///                    on a provider after the provider has already been
        ///                    initialized.
        ///                </exception>
        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name.Length == 0)
                name = "ProjectProvider";

            base.Initialize(name, config);

            _providerName = name;

            _connName = ConfigHelper.ExtractConfigValue(config, "connectionStringName", null);

            if (config.Count == 0)
                return;

            // Throw an exception if unrecognized attributes remain
            var attr = config.GetKey(0);
            if (!String.IsNullOrEmpty(attr))
                throw new ProviderException("Unrecognized attribute: " + attr);
        }

        #region Overrides of PromoCodeProviderBase

        ///<summary>
        ///Find the promotion by its code
        ///</summary>
        ///<param name="promoCode"></param>
        ///<returns></returns>
        public override PromoCode FindByCode(string promoCode)
        {
            return DSPromoCode.Create(_connName).FindByCode(promoCode);
        }

        ///<summary>
        ///If the promotion has times of use, then it is updated
        ///</summary>
        ///<param name="promoCode"></param>
        public override void Consume(string promoCode)
        {
            var ds = DSPromoCode.Create(_connName);
            var promo = ds.FindByCode(promoCode);
            if (!promo.TimesUsed.HasValue || !promo.MaxUse.HasValue)
                return;
            if (promo.TimesUsed.Value >= promo.MaxUse.Value)
                throw new Exceptions.ExceededMaximumUsageException();
            promo.TimesUsed++;
            using (var tran = new TransactionScope(_connName))
            {
                ds.Update(promo);
                tran.Commit();
            }
        }

        #endregion
    }
}
