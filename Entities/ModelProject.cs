using System;
using System.Collections.Generic;
using System.Text;

namespace SEOToolSet.Entities
{
    public class ModelProject
    {
        #region Model
        private int _id;
        private string _domain;
        private string _clientname;
        private string _contactemail;
        private string _contactname;
        private string _contactphone;
        private DateTime _createddate;
        private DateTime _updateddate;
        private string _createdby;
        private string _updatedby;
        private int _enabled;
        private string _name;
        private int _idaccount;
        private string _monitorupdatedby;
        private DateTime _monitorupdateddate;
        private int _idmonitorfrequency;
        public ModelAccount Account;

        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
         
        public string Domain
        {
            set { _domain = value; }
            get { return _domain; }
        }
        
        public string ClientName
        {
            set { _clientname = value; }
            get { return _clientname; }
        }
        
        public string ContactEmail
        {
            set { _contactemail = value; }
            get { return _contactemail; }
        }
        
        public string ContactName
        {
            set { _contactname = value; }
            get { return _contactname; }
        }
           
          
           
        public string ContactPhone
        {
            set { _contactphone = value; }
            get { return _contactphone; }
        }
           
          
           
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
           
          
           
        public DateTime UpdatedDate
        {
            set { _updateddate = value; }
            get { return _updateddate; }
        }
           
          
           
        public string CreatedBy
        {
            set { _createdby = value; }
            get { return _createdby; }
        }
           
          
           
        public string UpdatedBy
        {
            set { _updatedby = value; }
            get { return _updatedby; }
        }
           
          
           
        public int Enabled
        {
            set { _enabled = value; }
            get { return _enabled; }
        }
           
          
           
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
           
          
           
        public int IdAccount
        {
            set { _idaccount = value; }
            get { return _idaccount; }
        }
           
          
           
        public string MonitorUpdatedBy
        {
            set { _monitorupdatedby = value; }
            get { return _monitorupdatedby; }
        }
           
          
           
        public DateTime MonitorUpdatedDate
        {
            set { _monitorupdateddate = value; }
            get { return _monitorupdateddate; }
        }
           
          
           
        public int IdMonitorFrequency
        {
            set { _idmonitorfrequency = value; }
            get { return _idmonitorfrequency; }
        }
        #endregion Model
    }
}
