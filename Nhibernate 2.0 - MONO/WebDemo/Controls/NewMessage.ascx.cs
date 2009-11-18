using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Controls_NewMessage : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public bool EnabledAttach
    {
        get { return sectionAttach1.Visible; }
        set
        {
            sectionAttach1.Visible = value;
            sectionAttach2.Visible = value;
        }
    }

    public void SetAcceptedExtensions(string extensions)
    {
        lblAcceptedExtensions.InnerText = extensions;
    }

    public void SetMaxAttachSize(int maxSize)
    {
        lblMaxAttachSize.InnerText = maxSize.ToString();
    }

    public string MessageSubject
    {
        get { return txtSubject.Text; }
        set { txtSubject.Text = value; }
    }

    public string MessageBodyHtml
    {
        get 
        {
            Eucalypto.XHTMLText xhtml = new Eucalypto.XHTMLText();
            xhtml.Load(Eucalypto.XHTMLText.FromPlainText(txtBody.Text, Eucalypto.PlainTextMode.XHtmlConversion));

            return xhtml.RenderHTML(); 
        }
    }

    /// <summary>
    /// Content of the attachment. Null if not specified.
    /// </summary>
    public FileUpload AttachmentFile
    {
        get {return fAttachment;}
    }
}
