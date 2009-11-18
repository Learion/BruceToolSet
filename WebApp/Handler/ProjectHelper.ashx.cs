using System;
using System.Collections.Generic;
using System.Security;
using System.Web;
using LoggerFacade;
using SEOToolSet.Common;
using SEOToolSet.Entities.Wrappers;
using SEOToolSet.Providers;

namespace SEOToolSet.WebApp.Handler
{
    /// <summary>
    /// Helps to handle the complex data for the account project 
    /// </summary>
    public class ProjectHelper : IHttpHandler
    {
        private const ObjectSerializerType SERIALIZERTYPE = ObjectSerializerType.Object;

        #region IHttpHandler Members

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            var result = true;
            var tempResult = string.Empty;
            try
            {
                var action = context.Request.Form["action"];
                var project = ProjectManager.GetProjectById(int.Parse(context.Request.Form["idProject"]));

                if (!context.User.Identity.IsAuthenticated)
                    throw new SecurityException("User Not Authenticated");

                if (action == null)
                    throw new ApplicationException("The action was not provided");

                int id;
                string name;
                switch (action)
                {
                    case "getProjectInfo":
                        id = int.Parse(context.Request.Form["idProject"]);
                        ProjectWrapper projectInfo = ProjectManager.GetProjectById(id);
                        if(projectInfo==null)
                        {
                            result = false;
                            break;
                        }
                        tempResult = string.Format(@", ""project"": {0}", SerializeHelper.GetJsonResult(projectInfo, SERIALIZERTYPE));
                        break;
                    case "getKeywordsListsFromProject":
                        id = int.Parse(context.Request.Form["idProject"]);
                        var keywords = ProjectManager.GetKeywordLists(id);
                        if (keywords == null)
                        {
                            result = false;
                            break;
                        }
                        var keywordsWrapper = new List<KeywordListWrapper>();
                        foreach (var keyword in keywords)
                            keywordsWrapper.Add(keyword);
                        tempResult = string.Format(@", ""keywordLists"": {0}",
                                                   SerializeHelper.GetJsonResult(keywordsWrapper, SERIALIZERTYPE));
                        break;
                    case "createKeywordList":
                        name = context.Request.Form["name"];
                        ProjectManager.CreateKeywordList(out id,
                                                         name,
                                                         project);
                        tempResult = setTempResult(id);
                        break;
                    case "updateKeywordList":
                        id = int.Parse(context.Request.Form["idKeywordList"]);
                        name = context.Request.Form["name"];
                        ProjectManager.UpdateKeywordList(id, name, null);
                        break;
                    case "deleteKeywordList":
                        id = int.Parse(context.Request.Form["idKeywordList"]);
                        ProjectManager.DeleteKeywordList(id);
                        break;
                    case "createKeyword":
                        name = context.Request["keyword"];
                        var keywordList =
                            ProjectManager.GetKeywordListById(int.Parse(context.Request.Form["idKeywordList"]));
                        ProjectManager.CreateKeyword(out id, name, keywordList);
                        tempResult = setTempResult(id);
                        break;
                    case "updateKeyword":
                        id = int.Parse(context.Request.Form["idKeyword"]);
                        name = context.Request.Form["keyword"];
                        ProjectManager.UpdateKeyword(id, name);
                        break;
                    case "deleteKeyword":

                        id = int.Parse(context.Request.Form["idKeyword"]);
                        ProjectManager.DeleteKeyword(id);
                        break;
                    case "createCompetitor":
                        name = context.Request.Form["name"];
                        ProjectManager.AddCompetitor(out id,
                                                     name,
                                                     null, null,
                                                     project);
                        tempResult = setTempResult(id);
                        break;
                    case "updateCompetitor":
                        id = int.Parse(context.Request.Form["idCompetitor"]);
                        name = context.Request.Form["name"];
                        ProjectManager.UpdateCompetitor(id,
                                                        name,
                                                        null, null);
                        break;
                    case "deleteCompetitor":
                        id = int.Parse(context.Request.Form["idCompetitor"]);
                        ProjectManager.DeleteCompetitor(id);
                        break;
                    default:
                        throw new Exception(string.Format("The {0} action does not exist", action));
                }
            }
            catch (Exception ex)
            {
                Log.LogException(GetType(), ex);
                result = false;
                tempResult = string.Empty;
            }
            context.Response.Write(string.Format(@"{{""result"":""{0}""{1}}}", result, tempResult));
        }

        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

        private static string setTempResult(int id)
        {

            if (id > 0)
                return @",""id"":""" + id + @"""";
            return string.Empty;
        }
    }
}