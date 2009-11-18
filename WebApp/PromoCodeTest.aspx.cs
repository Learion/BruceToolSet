using System;
using SEOToolSet.Providers;

namespace SEOToolSet.WebApp
{
    ///<summary>
    ///Page for testing the <see cref="PromoCodeManager"/> class.
    ///</summary>
    public partial class PromoCodeTest : System.Web.UI.Page
    {
        ///<summary>
        ///Applies to the promotion specified by its code.
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="e"></param>
        protected void ConsumeLinkButton_OnClick(object sender, EventArgs e)
        {
            var promoCode=PromoCodeManager.GetByCode(PromotionCodeTextBox.Text);
            if(promoCode==null)
            {
                ServerMessagesLiteral.Text = "The promo code it seems to be changed before sending to server.";
                return;
            }
            try
            {
                PromoCodeManager.Consume(promoCode.Code);
                //Here we use the promoCode to send its information to Authorize.Net ARB service
                
            }
            catch (ApplicationException ex)
            {
                ServerMessagesLiteral.Text = string.Format("There was an error when trying to apply to the promotion. <br />Error detail: {0}", ex.ToString());
            }
        }
    }
}
