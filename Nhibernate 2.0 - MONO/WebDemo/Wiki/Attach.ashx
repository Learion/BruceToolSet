<%@ WebHandler Language="C#" Class="Attach" %>

using System;
using System.Web;

public class Attach : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string articleName = context.Request["article"];
        string attachName = context.Request["attach"];
        string mode = context.Request["mode"];

        Eucalypto.Wiki.Article article = Eucalypto.Wiki.WikiManager.GetArticleByName(articleName, true);

        if (Eucalypto.SecurityHelper.CanRead(context.User, article.Category, null) == false)
            throw new Eucalypto.InvalidPermissionException("Reading file for category " + article.Category.Name);

        Eucalypto.Wiki.FileAttachment attachment = Eucalypto.Wiki.WikiManager.GetFileAttachmentByName(article, attachName, true);

        if (mode == "download")
        {
            context.Response.ContentType = "application/x-download";
            context.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + attachment.Name + "\"");
        }
        else
        {
            context.Response.ContentType = attachment.ContentType;
            context.Response.AddHeader("Content-Disposition", "filename=\"" + attachment.Name + "\"");
        }

        context.Response.OutputStream.Write(attachment.ContentData, 0, attachment.ContentData.Length);
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }

}