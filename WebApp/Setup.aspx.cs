using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEOToolSet.Providers;
using SEOToolSet.Entities;

namespace SEOToolSet.WebApp
{
    public partial class Setup : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			/**Commented out so we can run Setup on the dev machine - AGL
            if (Request.IsLocal == false)
                throw new ApplicationException("Run Setup is only Allowed from Local Connections");
			**/
            if (IsPostBack) return;
            LoadConnections();
            lblNotification.Text = String.Empty;
        }

        private void LoadConnections()
        {
            _dropDownListConnections.Items.Clear();
            foreach (ConnectionStringSettings connection in ConfigurationManager.ConnectionStrings)
            {
                var item = new ListItem { Text = connection.Name, Value = connection.Name };

                _dropDownListConnections.Items.Add(item);
            }
        }

        protected void LinkLoadInitialData_Click(object sender, EventArgs e)
        {
            try
            {
                LoadInitialData();
                lblNotification.CssClass = "";
                lblNotification.Text = "The Schema has Been Created and the initial Data has also been loaded.";
                _loginLink.Visible = true;
            }
            catch (Exception ex)
            {
                lblNotification.CssClass = "Error";

                lblNotification.Text = string.Format("Initial Data Load Error: {0}", ex.Message);
            }

        }

        private void LoadInitialData()
        {
            CreateSchema();
            LoadInitialDataFromSql();
            LoadSubscriptionLevels();
            LoadBaseAccount();
        }

        private static void LoadSubscriptionLevels()
        {
            var liteId = SubscriptionManager.CreateSubscriptionLevel("Lite", SEORolesManager.GetRoleById(11), 9.95);
            var proId = SubscriptionManager.CreateSubscriptionLevel("Professional", SEORolesManager.GetRoleById(10), 14.95);
            var enterpriseId = SubscriptionManager.CreateSubscriptionLevel("Enterprise", SEORolesManager.GetRoleById(9), 19.95);
            var partnerId = SubscriptionManager.CreateSubscriptionLevel("Partner", SEORolesManager.GetRoleById(8), 0);
            var maxNumberOfUsersPropId = SubscriptionManager.CreateSubscriptionProperty("MaxNumberOfUsers");
            var maxNumberOfDomainUsersPropId = SubscriptionManager.CreateSubscriptionProperty("MaxNumberOfDomainUsers");
            var maxNumberOfProjectsId = SubscriptionManager.CreateSubscriptionProperty("MaxNumberOfProjects");

            SubscriptionManager.CreateSubscriptionDetails(liteId, new Dictionary<int, string>
                                                                      {
                                                                          {maxNumberOfUsersPropId, 2.ToString()},
                                                                          {maxNumberOfDomainUsersPropId, 2.ToString()},
                                                                          {maxNumberOfProjectsId, 2.ToString()}
                                                                      });
            SubscriptionManager.CreateSubscriptionDetails(proId, new Dictionary<int, string>
                                                                      {
                                                                          {maxNumberOfUsersPropId, 3.ToString()},
                                                                          {maxNumberOfDomainUsersPropId, 7.ToString()},
                                                                          {maxNumberOfProjectsId, 4.ToString()}
                                                                      });
            SubscriptionManager.CreateSubscriptionDetails(enterpriseId, new Dictionary<int, string>
                                                                      {
                                                                          {maxNumberOfUsersPropId, 5.ToString()},
                                                                          {maxNumberOfDomainUsersPropId, 10.ToString()},
                                                                          {maxNumberOfProjectsId, 10.ToString()}
                                                                      });
            SubscriptionManager.CreateSubscriptionDetails(partnerId, new Dictionary<int, string>
                                                                      {
                                                                          {maxNumberOfUsersPropId, 5.ToString()},
                                                                          {maxNumberOfDomainUsersPropId, 7.ToString()},
                                                                          {maxNumberOfProjectsId, 5.ToString()}
                                                                      });

        }

        private void CreateSchema()
        {
            var filename = GetFilename("Create");

            var beforeCreateTableScript = File.ReadAllText(Server.MapPath("~/SQLData/BeforeTableCreation.sql.txt"));

            if (!String.IsNullOrEmpty(beforeCreateTableScript))
                SchemaExporter.SqlExecutor.ExecuteSqlScriptWithoutTransaction(beforeCreateTableScript.Replace("{DATABASE_NAME}", SchemaExporter.SqlExecutor.GetCurrentDb(_dropDownListConnections.SelectedValue)), _dropDownListConnections.SelectedValue, true);

            SchemaExporter.SqlExecutor.CreateSchemaFromEntitiesAssembly(_dropDownListConnections.SelectedValue, filename, true);

            var createViewScript = File.ReadAllText(Server.MapPath("~/SQLData/ViewsDDL.sql.txt"));

            if (!String.IsNullOrEmpty(createViewScript))
                SchemaExporter.SqlExecutor.ExecuteSqlScript(
                    createViewScript,
                    _dropDownListConnections.SelectedValue);



        }

        private string GetFilename(string prefix)
        {
            var filename = String.Empty;
            if (_checkboxExportSql.Checked)
            {
                filename = Server.MapPath(String.Format("~/App_Data/OutputSQL{1}Instructions_Scripts{0}.sql", DateTime.Now.Ticks, prefix));
            }
            return filename;
        }

        private void LoadInitialDataFromSql()
        {
            SchemaExporter.SqlExecutor
                .ExecuteInTransaction(delegate(string connectionString)
                                          {
                                              var sqlInstructions =
                                                  File.ReadAllText(Server.MapPath("~/SQLData/LoadInitialData.sql.txt"));

                                              //sqlInstructions = sqlInstructions.Replace("{DATABASE_NAME}",SchemaExporter.SqlExecutor.GetCurrentDb(connectionString));

                                              SchemaExporter.SqlExecutor.ExecuteSqlScript(sqlInstructions,
                                                                                          connectionString);
                                              return true;
                                          }, _dropDownListConnections.SelectedItem.Value);
        }



        private static void LoadBaseAccount()
        {
            if (AccountManager.ExistsAccount("Bruce Clay, Inc")) return;
            var account = new Account { Name = AccountManager.TopLevelAccountName, CompanyName = "Bruce Clay, Inc. Internet Business Consultants", Enabled = true, SubscriptionLevel = SubscriptionManager.GetSubscriptionLevelByName("Partner") };
            var user = new SEOToolsetUser { Login = "bruceclayadmin", Enabled = true, Email = "bruce@bruceclay.com", Password = "roca", PasswordAnswer = "roca", FirstName = "Admin", LastName = "Admin" };

            AccountManager.CreateAccountAndUser(account, user);

            user = new SEOToolsetUser
                       {
                           Login = "bruceclayclient",
                           Enabled = true,
                           Email = "bruceclient@bruceclay.com",
                           Password = "roca",
                           PasswordAnswer = "roca",
                           Account = account,
                           FirstName = "CLient",
                           LastName = "Client Last Name"
                       };
            MembershipCreateStatus status;
            SEOMembershipManager.CreateUser(user, out status);

            if (status != MembershipCreateStatus.Success)
            {
                throw new Exception("User Cannot be created");
            }
            
            SEORolesManager.AddUserToRole(user.Login, "Client");

            user = new SEOToolsetUser
            {
                Login = "pguerrero",
                Enabled = true,
                Email = "pguerrero@bruceclay.com",
                Password = "roca",
                PasswordAnswer = "roca",
                Account = account,
                FirstName = "Paolo",
                LastName = "Guerrero"
            };
            SEOMembershipManager.CreateUser(user, out status);

            if (status != MembershipCreateStatus.Success)
            {
                throw new Exception("User Cannot be created");
            }

            SEORolesManager.AddUserToRole(user.Login, "Client");

            int idProject;
            ProjectManager.CreateProject(out idProject, "BruceClay Project", "http://www.bruceclay.com", "BruceClay, Inc.", "contact@gmail.com", "contactName", "contactPhoneToValidate","",account);
            int idCompetitor;

            var project = ProjectManager.GetProjectById(idProject);

            int idProjectUser;
            ProjectManager.AddUserToProject(out idProjectUser, "bruceclayadmin", "ProjectAdministrator", project);
            ProjectManager.AddUserToProject(out idProjectUser, "bruceclayclient", "Member", project);

            ProjectManager.AddCompetitor(out idCompetitor, "http://www.SEOMagics.com", "http://www.SEOMagics.com", "Competitor Description 1", project);
            ProjectManager.AddCompetitor(out idCompetitor, "http://www.SEOEspecialist.com.pe", "http://www.SEOEspecialist", "Competitor Description 2", project);
            ProjectManager.AddCompetitor(out idCompetitor, "http://www.SEOExperts.com.ar", "http://www.SEOExperts.com.ar", "Competitor Description 3", project);
            ProjectManager.AddCompetitor(out idCompetitor, "http://www.onlineSEO.com.br", "http://www.onlineSEO.com.br", "Competitor Description 4", project);


            int idKeywordList;

            int idKeyword;

            ProjectManager.CreateKeywordList(out idKeywordList, "K List 01", project);
            var keyList = ProjectManager.GetKeywordListById(idKeywordList);
            ProjectManager.CreateKeyword(out idKeyword, "K 1", keyList);
            ProjectManager.CreateKeyword(out idKeyword, "k 2", keyList);
            ProjectManager.CreateKeyword(out idKeyword, "K 3", keyList);

            ProjectManager.CreateKeywordList(out idKeywordList, "K List 02", project);
            keyList = ProjectManager.GetKeywordListById(idKeywordList);
            ProjectManager.CreateKeyword(out idKeyword, "K 2", keyList);
            ProjectManager.CreateKeyword(out idKeyword, "K 3", keyList);
            ProjectManager.CreateKeyword(out idKeyword, "K 4", keyList);

            ProjectManager.CreateKeywordList(out idKeywordList, "Kanji keywords", project);
            keyList = ProjectManager.GetKeywordListById(idKeywordList);
            ProjectManager.CreateKeyword(out idKeyword, @"出典: フリー百科事典『ウィキペディア", keyList);
            ProjectManager.CreateKeyword(out idKeyword, @"コミュニティ・ポータル", keyList);
            ProjectManager.CreateKeyword(out idKeyword, @"報道", keyList);


        }

        protected void LinkButtonValidate_Click(object sender, EventArgs e)
        {
            _loginLink.Visible = false;
            try
            {
                SchemaExporter.SqlExecutor.ValidateSchemaFromEntitiesAssembly(
                    _dropDownListConnections.SelectedItem.Value);
                //asume the Schema is Valid
                lblNotification.CssClass = "";
                lblNotification.Text = "Validation Successful. It appears you have the last version of the Schema";



            }
            catch (Exception ex)
            {
                lblNotification.CssClass = "Error";
                lblNotification.Text = string.Format("Validation Error: {0}", ex.Message);
            }



        }

        protected void LinkButtonUpdate_Click(object sender, EventArgs e)
        {
            _loginLink.Visible = false;
            try
            {
                SchemaExporter.SqlExecutor.UpdateSchemaFromEntitiesAssembly(
                    _dropDownListConnections.SelectedItem.Value, GetFilename("Update"), true);
                //asume the Schema is Valid
                lblNotification.CssClass = "";
                lblNotification.Text = "Update Successful. Now you have the last Schema";



            }
            catch (Exception ex)
            {
                lblNotification.CssClass = "Error";
                lblNotification.Text = string.Format("Update Error: {0}", ex.Message);
            }
        }
    }
}