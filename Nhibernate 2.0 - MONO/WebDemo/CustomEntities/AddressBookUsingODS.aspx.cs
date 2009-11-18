using System;
using System.Collections.Generic;
using System.Web;

using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernateDataStore.Common;
using NHibernateDataStore.Transaction;

namespace WebDemo.CustomEntities
{
    public partial class AddressBookUsingODS : R3M.Integration.Ninject.Web.WebPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }
      
        protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            e.NewValues["telephone"] = ((TextBox)FormView1.FindControl("Telephone1TextBox")).Text;
        }

        protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            GridView2.DataBind();
        }

        protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            e.Values["telephone"] = ((TextBox)FormView1.FindControl("Telephone1TextBox")).Text;
        }

        protected void odsContactCreate_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {

        }

        protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            // WebSite.CustomEntities.AddressBook.ContactODS.CommitTransaction();
            GridView2.DataBind();
        }

        protected void odsContactCreate_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            //WebSite.CustomEntities.AddressBook.ContactODS.BeginTransaction();

        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            e.NewValues["telephone"] = ((TextBox)GridView2.Rows[e.RowIndex].FindControl("TextBox1")).Text;
        }
    
        protected void InsertButton_Click(object sender, EventArgs e)
        {
            try
            {
                ConfigurationHelper.BeginTransaction("DefaultDB");
                
                FormView1.InsertItem(true);

                ConfigurationHelper.CommitTransaction("DefaultDB");
            }
            catch(Exception)
            {
                ConfigurationHelper.RollbackTransaction("DefaultDB");
            }
        }

        protected void FormView1_DataBinding(object sender, EventArgs e)
        {

        }

        

    }
}
