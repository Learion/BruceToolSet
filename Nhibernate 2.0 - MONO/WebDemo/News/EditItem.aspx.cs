using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WebDemo.code;

public partial class News_EditItem : System.Web.UI.Page
{
    private string ItemId
    {
        get { return Request["item"]; }
    }

    private string CategoryName
    {
        get { return Request["category"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            //Edit
            if (ItemId != null && ItemId.Length > 0)
            {
                Eucalypto.News.Item item = Eucalypto.News.NewsManager.GetItem(ItemId);

                txtDescription.Text = item.Description;
                txtTitle.Text = item.Title;
                txtOwner.Text = item.Owner;
                txtAuthor.Text = item.Author;
                txtURL.Text = item.URL;
                txtURLName.Text = item.URLName;
                txtNewsDate.Text = Utilities.FormatDate(item.NewsDate);

                txtAuthor.Enabled = true;
            }
            //New
            else if (CategoryName != null && CategoryName.Length > 0)
            {

                txtNewsDate.Text = Utilities.FormatDate(DateTime.Today);

                txtAuthor.Enabled = false;
                txtAuthor.Text = Page.User.Identity.Name;
                txtOwner.Text = Page.User.Identity.Name;
            }
            else
                throw new ApplicationException("Invalid parameters");
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            //Edit
            if (ItemId != null && ItemId.Length > 0)
            {
                Eucalypto.News.Item item = Eucalypto.News.NewsManager.GetItem(ItemId);

                //Check permissions
                if (Eucalypto.SecurityHelper.CanEdit(Page.User, item.Category, item) == false)
                    throw new Eucalypto.InvalidPermissionException("edit news item");

                item.Description = txtDescription.Text;
                item.Title = txtTitle.Text;
                item.Author = txtAuthor.Text;
                item.URL = txtURL.Text;
                item.URLName = txtURLName.Text;
                item.NewsDate = Utilities.ParseDate(txtNewsDate.Text);

                Eucalypto.News.NewsManager.UpdateItem(item);

                Navigation.News_ViewCategory(item.Category.Name).Redirect(this);
            }
            //New
            else if (CategoryName != null && CategoryName.Length > 0)
            {
                Eucalypto.News.Category category = Eucalypto.News.NewsManager.GetCategoryByName(CategoryName, true);

                //Check permissions
                if (Eucalypto.SecurityHelper.CanInsert(Page.User, category) == false)
                    throw new Eucalypto.InvalidPermissionException("insert news item");

                Eucalypto.News.NewsManager.CreateItem(category, Page.User.Identity.Name, txtTitle.Text, 
                                                                txtDescription.Text, txtURL.Text, 
                                                                txtURLName.Text,
                                                                Utilities.ParseDate(txtNewsDate.Text));

                Navigation.News_ViewCategory(CategoryName).Redirect(this);
            }
            else
                throw new ApplicationException("Invalid parameters");

        }
        catch (Exception ex)
        {
            ((IErrorMessage)Master).SetError(GetType(), ex);
        }
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        //Edit
        if (ItemId != null && ItemId.Length > 0)
        {
            Eucalypto.News.Item item = Eucalypto.News.NewsManager.GetItem(ItemId);
            Navigation.News_ViewCategory(item.Category.Name).Redirect(this);
        }
        //New
        else if (CategoryName != null && CategoryName.Length > 0)
        {
            Navigation.News_ViewCategory(CategoryName).Redirect(this);
        }
        else
            throw new ApplicationException("Invalid parameters");
    }
}
