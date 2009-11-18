using System.Configuration.Provider;
using SEOToolSet.Entities;

namespace SEOToolSet.Providers
{
    ///<summary>
    ///Provider to handle the <c>PromoCode</c> related operations
    ///</summary>
    public abstract class PromoCodeProviderBase : ProviderBase
    {
        ///<summary>
        ///Find the promotion by its code
        ///</summary>
        ///<param name="promoCode"></param>
        ///<returns></returns>
        public abstract PromoCode FindByCode(string promoCode);

        ///<summary>
        ///If the promotion has times of use, then it is updated
        ///</summary>
        ///<param name="promoCode"></param>
        public abstract void Consume(string promoCode);
    }
}
