using System;
using System.Collections.Generic;
using System.Web;
using Serializer;

namespace SEOToolSet.WebApp.Controls.CostConversionUtil
{
    public class Currency
    {
        static List<Currency> _currencies;

        public String Code { get; set; }
        public String CurrencyName { get; set; }
        public String CurrencySymbol { get; set; }

        private static void ensureCurrenciesAreLoaded()
        {
            if (_currencies == null)
            {
                ObjectXmlSerializer.Load(HttpContext.Current.Server.MapPath("~/App_Data/currencies.xml"),
                                         out _currencies);
            }
        }
        public static IList<Currency> GetWorldCurrencies()
        {
            
            ensureCurrenciesAreLoaded();

            return _currencies;

        }

        public static Currency FindCurrencyByCode(String code){

            ensureCurrenciesAreLoaded();

            return _currencies != null ? _currencies.Find(c => c.Code == code) : null;
        }

    }


}
