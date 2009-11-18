using System;

using System.Configuration;
using System.Collections.Generic;

using System.Web.Security;

using System.Web.UI.WebControls;
using Eucalypto.Common;
using NHibernateDataStore.Common;
using NHibernateDataStore.SchemaGenerator;


public partial class Setup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.IsLocal == false)
            throw new Eucalypto.InvalidPermissionException("run setup");

        if (!IsPostBack)
        {
            LoadConnections();
            LoadListAdvanced(false);

            LoadTests();
        }
    }

    private void LoadConnections()
    {
        cmbConnections.Items.Clear();
        foreach (ConnectionStringSettings connection in ConfigurationManager.ConnectionStrings)
        {
            ListItem item = new ListItem();
            item.Text = connection.Name;
            item.Value = connection.Name;

            cmbConnections.Items.Add(item);
        }
    }

    private void LoadListAdvanced(bool checkStatus)
    {
        list.Items.Clear();

        var connection = ConfigurationHelper.Create(cmbConnections.SelectedValue);

        if (connection != null)
        {
            var generator = new GenericGenerator(connection);

            foreach (string section in generator.GetSchemaCategories())
            {
                ListItem item = new ListItem();
                item.Text = section.ToString();
                if (checkStatus)
                    item.Text += " - " + generator.GetStatus(section).ToString();
                item.Value = section;

                list.Items.Add(item);
            }
        }
    }

    protected void btCheckStatus_Click(object sender, EventArgs e)
    {
        LoadListAdvanced(true);
    }

    protected void btCreate_Click(object sender, EventArgs e)
    {
        var connection = ConfigurationHelper.Create(cmbConnections.SelectedValue);

        var generator = new GenericGenerator(connection);

        foreach (ListItem item in list.Items)
        {
            if (item.Selected)
            {
                generator.CreateSchemaTable(item.Value);
            }
        }

        LoadListAdvanced(true);

        lblStatus.InnerText = "Schema created!";
    }

    protected void cmbConnections_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadListAdvanced(false);
    }

    protected void btCreateAdmin_Click(object sender, EventArgs e)
    {
        Roles.CreateRole(txtAdminRole.Text);

        string pwd = txtAdminPassword.Text;

        Membership.CreateUser(txtAdminUser.Text, pwd, txtAdminEMail.Text);

        Roles.AddUserToRole(txtAdminUser.Text, txtAdminRole.Text);

        lblStatus.InnerText = "User created!";
    }


    protected void btRunTest_Click(object sender, EventArgs e)
    {
        testResult.Items.Clear();

        foreach (ListItem item in listTest.Items)
        {
            var elapsed = new System.Diagnostics.Stopwatch();
            elapsed.Start();

            if (item.Selected)
            {
                try
                {
                    if (item.Value == TEST_USER)
                        TestUser();
                    else if (item.Value == TEST_FORUM)
                        TestForum();
                    else if (item.Value == TEST_WIKI)
                        TestWiki();
                    else if (item.Value == TEST_NEWS)
                        TestNews();
                    else
                        continue;

                    elapsed.Stop();

                    testResult.Items.Add(item.Value + " OK (" + elapsed.ElapsedMilliseconds + " ms)");
                }
                catch (Exception ex)
                {
                    testResult.Items.Add(item.Value + " FAILED! - " + ex.Message);
                }
            }
        }

        lblStatus.InnerText = "Test completed!";
    }

    #region Tests

    private const string TEST_USER = "Users and roles";
    private const string TEST_FORUM = "Forums";
    private const string TEST_WIKI = "Wiki";
    private const string TEST_NEWS = "News";

    private void LoadTests()
    {
        listTest.Items.Clear();

        listTest.Items.Add(TEST_USER);
        listTest.Items.Add(TEST_FORUM);
        listTest.Items.Add(TEST_WIKI);
        listTest.Items.Add(TEST_NEWS);
    }

    private void TestUser()
    {
        MembershipCreateStatus status;
        MembershipUser user = Membership.CreateUser("test", "testpwd", "test@test.test", "question", "answer", true, out status);

        if (Membership.ValidateUser("test", "testpwd") == false)
            throw new ApplicationException("Password not valid");

        user.ChangePasswordQuestionAndAnswer("testpwd", "question2", "answer2");
        Membership.UpdateUser(user);

        bool found = false;
        MembershipUserCollection list = Membership.FindUsersByName("test");
        foreach (MembershipUser listItem in list)
        {
            if (listItem.UserName == "test")
                found = true;
        }

        if (found == false)
            throw new ApplicationException("User created not found");


        Membership.DeleteUser("test", true);
    }

    private void TestForum()
    {
        //Create category
        Eucalypto.Forum.Category category = Eucalypto.Forum.ForumManager.CreateCategory("test", "test");

        //Create a topic (with a message)
        Eucalypto.Forum.Topic topic;
        Eucalypto.Forum.Message message;
        Eucalypto.Forum.ForumManager.CreateTopic(category, "user1", "Title 1",
                                        "<p>Body text</p>", null, out topic, out message);

        //Create an answer (message)




        Eucalypto.Forum.Message message1 = Eucalypto.Forum.ForumManager.CreateMessage(topic, message.Id,
                                                            "user1", "RE: Title 1", "<p>Response</p>", null);

        //Create a second answer (message)
        Eucalypto.Forum.Message message2 = Eucalypto.Forum.ForumManager.CreateMessage(topic, message1.Id,
                                                            "user1", "RE: Title 1", "<p>Response</p>", null);

        //Create a second topic
        Eucalypto.Forum.Topic topic2;
        Eucalypto.Forum.Message message3;
        Eucalypto.Forum.ForumManager.CreateTopic(category, "user2", "Title 2", "<p>Body text</p>",
                                                                    null, out topic2, out message3);

        //Find the previous messages (filtering the owner field)
        PagingInfo paging = new PagingInfo(10, 0);
        Eucalypto.Forum.ForumManager.FindMessages(Filter.MatchOne(category.Name),
                                                    null,
                                                    Filter.MatchOne("user1"),
                                                    null,
                                                    null, null,
                                                    paging);
        if (paging.RowCount != 3)
            throw new ApplicationException("Forum messages for author not found");

        //Find the previous messages (filtering by a field)
        Eucalypto.Forum.ForumManager.FindMessages(Filter.MatchOne(category.Name),
                                                    Filter.ContainsOne("RE:"),
                                                    null, null, null, null, paging);
        if (paging.RowCount != 2)
            throw new ApplicationException("Forum messages not found");

        //Delete a topic
        Eucalypto.Forum.ForumManager.DeleteTopic(topic);

        //Create another topic
        Eucalypto.Forum.Topic topic3;
        Eucalypto.Forum.Message message4;
        Eucalypto.Forum.ForumManager.CreateTopic(category, "test", "Title 3", "<p>Body text</p>", null, out topic3, out message4);

        //Delete the category
        Eucalypto.Forum.ForumManager.DeleteCategory(category);
    }

    private void TestWiki()
    {
        //Create a category
        Eucalypto.Wiki.Category category = Eucalypto.Wiki.WikiManager.CreateCategory("test", "test");

        //Create an article
        Eucalypto.Wiki.Article article = Eucalypto.Wiki.WikiManager.CreateArticle(category, "owner1",
                                        "article1", "Title 1", "Description", "<p>body 1</p>");

        //Create a second article
        Eucalypto.Wiki.Article article2 = Eucalypto.Wiki.WikiManager.CreateArticle(category, "owner2",
                                        "article2", "Title 2", "Description",
                                        "<p>bla bla bla search1 bla bla bla</p>");

        //Update an article
        article.Body = "<p>body 1 v2</p>";
        string myTag = Guid.NewGuid().ToString();
        article.Tag = myTag;
        Eucalypto.Wiki.WikiManager.UpdateArticle(article, true);

        //Read the first version of the article
        Eucalypto.Wiki.ArticleBase articleV1 = Eucalypto.Wiki.WikiManager.GetArticleByVersion(article, 1);
        if (articleV1.Body != "<p>body 1</p>")
            throw new ApplicationException("Version not correctly created");

        //Find an article (filtering using the search string)
        PagingInfo paging = new PagingInfo(10, 0);
        IList<Eucalypto.Wiki.Article> articles = Eucalypto.Wiki.WikiManager.FindArticles(Filter.MatchOne(category.Name),
                                            Filter.ContainsAll("search1"),
                                            null, null, null, null, null,
                                            Eucalypto.Wiki.ArticleStatus.All,
                                            paging);
        if (paging.RowCount != 1)
            throw new ApplicationException("Article not found");

        //Find another article (filtering using the Tag)
        articles = Eucalypto.Wiki.WikiManager.FindArticles(null, null, null, null,
                                            Filter.MatchOne(myTag), null, null, Eucalypto.Wiki.ArticleStatus.All, paging);
        if (articles.Count != 1)
            throw new ApplicationException("Article not found");

        //Delete an article
        Eucalypto.Wiki.WikiManager.DeleteArticle(article);

        //Delete the category
        Eucalypto.Wiki.WikiManager.DeleteCategory(category);
    }

    private void TestNews()
    {
        //Create a category
        Eucalypto.News.Category category = Eucalypto.News.NewsManager.CreateCategory("test", "test");

        //Create an item
        Eucalypto.News.Item item = Eucalypto.News.NewsManager.CreateItem(category, "owner1",
                                        "Title 1", "Description 1", "http://www.devage.com/", "DevAge home page", new DateTime(2006, 1, 3));
        //Create a second item
        Eucalypto.News.Item item2 = Eucalypto.News.NewsManager.CreateItem(category, "owner1",
                                        "Title 2", "Description 2", "http://www.devage.com/", "DevAge home page", new DateTime(2006, 10, 3));

        //Update an item
        item.Description = "Description modified";
        Eucalypto.News.NewsManager.UpdateItem(item);

        //Find the previous items
        PagingInfo paging = new PagingInfo(10, 0);
        Eucalypto.News.NewsManager.FindItems(Filter.MatchOne(category.Name),
                                            null, null, null,
                                            paging);
        if (paging.RowCount != 2)
            throw new ApplicationException("News not found");

        //Delete an item
        Eucalypto.News.NewsManager.DeleteItem(item2);

        //Delete the entire category
        Eucalypto.News.NewsManager.DeleteCategory(category);
    }



    protected void btForumPerfTest_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Stopwatch elapsed = new System.Diagnostics.Stopwatch();
        elapsed.Start();


        Eucalypto.Forum.Category category = Eucalypto.Forum.ForumManager.GetCategoryByName("performance", false);
        if (category == null)
            category = Eucalypto.Forum.ForumManager.CreateCategory("performance", "Performance test");

        for (int t = 0; t < 20; t++)
        {
            string title = "Performance test " + DateTime.Now.ToString() + " (" + t + ")";

            Eucalypto.Forum.Topic topic;
            Eucalypto.Forum.Message message;
            Eucalypto.Forum.ForumManager.CreateTopic(category, "TestUser1", title,
                                            "<p>Topic</p>", null, out topic, out message);

            for (int m = 0; m < 20; m++)
            {
                Eucalypto.Attachment.FileInfo attach = new Eucalypto.Attachment.FileInfo("attach.txt", "text/plain", System.Text.Encoding.UTF8.GetBytes("test attach string " + m.ToString()));
                Eucalypto.Forum.Message message1 = Eucalypto.Forum.ForumManager.CreateMessage(topic, message.Id,
                                                                    "TestUser2", "RE: " + title, "<p>Response + " + m.ToString() + "</p>", attach);
            }
        }


        elapsed.Stop();
        lblPerfResult.InnerText = elapsed.Elapsed.ToString();
    }

    #endregion
}
