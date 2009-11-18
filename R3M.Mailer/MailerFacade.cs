using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Xml.XPath;
using R3M.Mailer;

public class MailerFacade
{
    // Methods
    public static bool AtLeastOneEmailIsValid(ref string addressesText)
    {

        var addressArray = Regex.Replace(addressesText, @"\s", string.Empty).Split(new[] { ',', ';' });
        var auxAddressesText = string.Empty;
        var hasAddress = false;
        foreach (var address in addressArray)
        {
            if (!IsValidEmail(address)) continue;
            if (hasAddress)
                auxAddressesText = auxAddressesText + ",";
            auxAddressesText = auxAddressesText + address;
            hasAddress = true;
        }
        if (string.IsNullOrEmpty(auxAddressesText))
        {
            addressesText = null;
            return false;
        }
        addressesText = auxAddressesText;
        return true;
    }

    private static MailMessage CreateMailMessageFromTemplate(IXPathNavigable template, string To, string From, Dictionary<string, string> parameters, bool IsBodyHtml)
    {
        var navigator = template.CreateNavigator();
        var source = (string)navigator.Evaluate("string(template/body)");
        var subject = (string)navigator.Evaluate("string(template/subject)");
        if (!IsValidEmail(From))
        {
            return null;
        }
        var message = new MailMessage(From, To)
        {
            IsBodyHtml = IsBodyHtml,
            BodyEncoding = Encoding.UTF8,
            SubjectEncoding = Encoding.UTF8
        };

        if (!string.IsNullOrEmpty(source))
            message.Body = ReplaceParameters(source, parameters);
        if (!string.IsNullOrEmpty(subject))
            message.Subject = ReplaceParameters(subject, parameters);
        return message;
    }

    public static bool IsValidEmail(string email)
    {
        var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        return regex.IsMatch(email);
    }

    public static void SendEmail(MailMessage mail)
    {
        var client = Settings.Default.UseNetworkCredentials
            ? new SmtpClient() :
            new SmtpClient(Settings.Default.SMTPHost, Settings.Default.SMTPPort) { Credentials = new NetworkCredential(Settings.Default.UserName, Settings.Default.Password) };
        client.Send(mail);
    }

    public static void SendEmail(string To, string From, string Subject, string Body)
    {
        SendEmail(To, From, Subject, Body, null);
    }

    public static void SendEmail(string To, string From, string Subject, string Body, string AttachmentPath)
    {
        if (!AreValidMailAddresses(ref To, From)) return;
        var mail = new MailMessage(From, To, Subject, Body)
                               {
                                   IsBodyHtml = true,
                                   Priority = MailPriority.Normal
                               };
        if (!string.IsNullOrEmpty(AttachmentPath))
        {
            mail.Attachments.Add(new Attachment(AttachmentPath, "application/octet-stream"));
        }
        SendEmail(mail);
    }

    public static void SendEmail(string To, string From, string Subject, string Body, byte[] AttachmentBytes, string AttachmentName)
    {
        if (!AreValidMailAddresses(ref To, From)) return;
        var mail = new MailMessage(From, To, Subject, Body)
                       {
                           IsBodyHtml = true,
                           Priority = MailPriority.Normal
                       };
        mail.Attachments.Add(new Attachment(new MemoryStream(AttachmentBytes), AttachmentName));
        SendEmail(mail);
    }

    public static void SendHtmlEmailUsingTemplate(string To, string From, string MailTemplatePath, Dictionary<string, string> parameters)
    {
        if (!AreValidMailAddresses(ref To, From)) return;
        var template = new XPathDocument(MailTemplatePath);
        var mail = CreateMailMessageFromTemplate(template, To, From, parameters, true);
        SendEmail(mail);
    }

    /// <summary>
    /// Send an email using to templates, one for HTML and one for plain Text
    /// </summary>
    /// <param name="To"></param>
    /// <param name="From"></param>
    /// <param name="MailHtmlTemplatePath">path to the html template</param>
    /// <param name="MailPlainTextTemplatePath">path to the plain text template</param>
    /// <param name="parameters"></param>
    public static void SendEmailUsingMultiPart(string To, string From, string MailHtmlTemplatePath, string MailPlainTextTemplatePath, Dictionary<string, string> parameters)
    {
        if (!AreValidMailAddresses(ref To, From)) return;
        var htmlBody = ReplaceParameters(File.ReadAllText(MailHtmlTemplatePath), parameters);
        var baseMail = CreateMailMessageFromTemplate(new XPathDocument(MailPlainTextTemplatePath), To, From, parameters, false);
        var htmlContent = AlternateView.CreateAlternateViewFromString(htmlBody,
                                                                   new System.Net.Mime.ContentType("text/html"));
        baseMail.AlternateViews.Add(htmlContent);
        SendEmail(baseMail);
    }

    private static string ReplaceParameters(string source, Dictionary<string, string> parameters)
    {
        var str = source;
        foreach (var str2 in parameters.Keys)
        {
            str = str.Replace(string.Format(@"[{0}]", str2), parameters[str2]);
        }
        return str;
    }

    private static bool AreValidMailAddresses(ref string To, string From)
    {
        if (AtLeastOneEmailIsValid(ref To) && IsValidEmail(From))
            return true;
        throw new ApplicationException("Not valid email(s). Check the email account(s) that you provide.");
    }

    public static string GetSenderFromWebConfig()
    {
        var config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        var settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
        return settings == null ? null : settings.Smtp.From;
    }
}
