using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using NHibernateDataStore.Common;
using WebDemo.code;
using WebSite.CustomEntities.AddressBook;
using WebSite.CustomEntities.AddressBook.Domain;

public partial class CustomEntities_AddressBook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadList();
        }
    }

    protected void listRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            string id = (string)e.CommandArgument;

            DeleteContact(id);

            LoadList();
        }
    }

    protected void btAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Contact contact = new Contact(txtDisplayName.Text);
            contact.Address = txtAddress.Text;
            contact.FirstName = txtFirstName.Text;
            contact.LastName = txtLastName.Text;
            contact.Telephone1 = txtTelephone.Text;

            AddContact(contact);

            txtDisplayName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtTelephone.Text = string.Empty;

            LoadList();
        }
        catch (Exception ex)
        {
            ((IErrorMessage)Master).SetError(GetType(), ex);
        }
    }

    private void LoadList()
    {
        listRepeater.DataSource = GetContacts();
        listRepeater.DataBind();
    }


    private void AddContact(Contact contact)
    {


        //ContactDataStore.Create(WebApp.GetSession("DefaultDB"));

        ConnectionParameters parameters = ConfigurationHelper.Create("DefaultDB");

        using (TransactionScope transaction = new TransactionScope(parameters))
        {
            ContactDataStore dataStore = new ContactDataStore(transaction);

            dataStore.Insert(contact);

            transaction.Commit();
        }
    }

    private IList<Contact> GetContacts()
    {
        ConnectionParameters parameters = ConfigurationHelper.Create("DefaultDB");

        using (TransactionScope transaction = new TransactionScope(parameters))
        {
            ContactDataStore dataStore = new ContactDataStore(transaction);

            return dataStore.FinAll();
        }
    }

    private void DeleteContact(string id)
    {
        ConnectionParameters parameters = ConfigurationHelper.Create("DefaultDB");

        using (TransactionScope transaction = new TransactionScope(parameters))
        {
            ContactDataStore dataStore = new ContactDataStore(transaction);

            dataStore.Delete(id);

            transaction.Commit();
        }
    }
}
