using System;
using System.Collections.Generic;

namespace SEOToolSet.WebApp
{
    public partial class MailTest : System.Web.UI.Page
    {

        protected void SubmitLinkButton_Click(object sender, EventArgs e)
        {
            var from = MailerFacade.GetSenderFromWebConfig();
            var addresses = MailTextBox.Text;
            if (!MailerFacade.AtLeastOneEmailIsValid(ref addresses)) return;
            try
            {
                if (!string.IsNullOrEmpty(from))
                {
                    var parameters = new Dictionary<string, string>
                                         {
                                             {"BODYTEXT",ContentTextBox.Text},
                                             {"ITEM1", "1st element"},
                                             {"ITEM2", "2nd element"},
                                             {"REASON", SubjectTextBox.Text}
                                         };
                    MailerFacade.SendEmailUsingMultiPart(MailTextBox.Text, from, Server.MapPath("~\\App_Data\\HtmlMailTemplate_Test.html"), Server.MapPath("~\\App_Data\\PlainMailTemplate_Test.xml"), parameters);
                    ResultLiteral.Text =
                        string.Format("<div style='height: 40px;'>The mail was successfuly sent to {0}.</div>",
                                      MailTextBox.Text);
                }
            }
            catch (Exception ex)
            {
                LoggerFacade.Log.Error(GetType(), string.Format("Error: {0}", ex), ex);
                ResultLiteral.Text = string.Format("There was an error sending the mail. The code for the error is {0}, and the details:\n{1}", ex, ex.Message);
            }
        }

    }
}
