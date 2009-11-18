<%@ WebHandler Language="C#" Class="Attach" %>

using System;
using System.Web;

public class Attach : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string id = context.Request["id"];
        string mode = context.Request["mode"];

        Eucalypto.Forum.Message msg = Eucalypto.Forum.ForumManager.GetMessage(id);

        if (msg.Attachment == null)
            throw new Eucalypto.FileAttachNotFoundException(id);

        if (Eucalypto.SecurityHelper.CanRead(context.User, msg.Topic.Category, null) == false)
            throw new Eucalypto.InvalidPermissionException("Reading file " + msg.Attachment.Name);

        if (mode == "download")
        {
            context.Response.ContentType = "application/x-download";
            context.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + msg.Attachment.Name + "\"");
        }
        else
        {
            context.Response.ContentType = msg.Attachment.ContentType;
            context.Response.AddHeader("Content-Disposition", "filename=\"" + msg.Attachment.Name + "\"");
        }

        context.Response.OutputStream.Write(msg.Attachment.ContentData, 0, msg.Attachment.ContentData.Length);
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}