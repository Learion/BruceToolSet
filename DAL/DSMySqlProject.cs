using System;
using System.Collections.Generic;
using System.Text;
using MySqlDataStore.Common;
using System.Data;
using MySql.Data.MySqlClient;
using SEOToolSet.Entities;

namespace SEOToolSet.DAL
{
    public class DSMySqlProject : EntityDataStoreBase<Int32>
    {
        public DSMySqlProject(IDbConnection connection)
            : base(connection)
        {
        }

        public static DSMySqlProject Create(String conStr)
        {
            return new DSMySqlProject(new MySqlConnection(conStr));
        }

        public IList<Project> FindByAccount(Account account, bool? inactive)
        {
            return null;
        }
        public Project FindByKey(Int32 id)
        {
            IDataReader dr = FindByKey("select Project.* from Project where id=?id", id);
            return GetObject(dr);
        }

        public int Insert(Project obj)
        {
            string commandText = "insert into project (Domain,ClientName,ContactEmail,ContactName,ContactPhone,CreatedDate,CreatedBy,Enabled,Name,IdAccount) values(?Domain,?ClientName,?ContactEmail,?ContactName,?ContactPhone,?CreatedDate,?CreatedBy,?Enabled,?Name,?IdAccount)";
            MySqlParameter[] parameters = new MySqlParameter[10];
            parameters[0] = new MySqlParameter("?Domain", obj.Domain);
            parameters[1] = new MySqlParameter("?ClientName", obj.ClientName);
            parameters[2] = new MySqlParameter("?ContactEmail", obj.ContactEmail);
            parameters[3] = new MySqlParameter("?ContactName", obj.ContactName);
            parameters[4] = new MySqlParameter("?ContactPhone", obj.ContactPhone);
            parameters[5] = new MySqlParameter("?CreatedDate", DateTime.Now);
            parameters[6] = new MySqlParameter("?CreatedBy", obj.CreatedBy);
            parameters[7] = new MySqlParameter("?Enabled", 1);
            parameters[8] = new MySqlParameter("?Name", obj.Name);
            parameters[9] = new MySqlParameter("?IdAccount", obj.Account.Id);
            int count = ExecuteCommand(commandText, parameters);
            obj.Id = Convert.ToInt32(Count("select max(id) from project"));

            return count;
        }

        public int Update(Project obj)
        {
            string commandText = "update project set Domain=?Domain,ClientName=?ClientName,ContactEmail=?ContactEmail,ContactName=?ContactName,ContactPhone=?ContactPhone,UpdatedDate=?UpdatedDate,UpdatedBy=?UpdatedBy,Name=?Name where id=?id";
            MySqlParameter[] parameters = new MySqlParameter[9];
            parameters[0] = new MySqlParameter("?Domain", obj.Domain);
            parameters[1] = new MySqlParameter("?ClientName", obj.ClientName);
            parameters[2] = new MySqlParameter("?ContactEmail", obj.ContactEmail);
            parameters[3] = new MySqlParameter("?ContactName", obj.ContactName);
            parameters[4] = new MySqlParameter("?ContactPhone", obj.ContactPhone);
            parameters[5] = new MySqlParameter("?UpdatedDate", DateTime.Now);
            parameters[6] = new MySqlParameter("?UpdatedBy", obj.UpdatedBy);
            parameters[7] = new MySqlParameter("?Name", obj.Name);
            parameters[8] = new MySqlParameter("?id", obj.Id);
            int count = ExecuteCommand(commandText, parameters);
            return count;
        }

        public IList<Project> FindByUser(string userName, bool? includeInactive)
        {
            StringBuilder commandText = new StringBuilder();
            commandText.Append("select Project.* from Project where IdAccount in(select IdAccount from seotoolsetuser where login=?username)");
            if (includeInactive.HasValue)
            {
                if (!includeInactive.Value)
                {
                    commandText.Append(" and Enabled=1");
                }
                else
                {
                    commandText.Append(" and Enabled=0");
                }
            }
            MySqlParameter[] parameters = new MySqlParameter[1];
            parameters[0] = new MySqlParameter("?username", userName);
            return GetObjects(commandText, parameters);
        }

        private IList<Project> GetObjects(StringBuilder commandText, MySqlParameter[] parameters)
        {
            List<Project> pjList = new List<Project>();
            DataTable dt = Find(commandText.ToString(), parameters);
            foreach (DataRow dr in dt.Rows)
            {
                Project pj = new Project();
                pj.Id = Convert.ToInt32(dr["Id"]);
                if (!DBNull.Value.Equals(dr["Domain"]))
                {
                    pj.Domain = dr["Domain"].ToString();
                }
                if (!DBNull.Value.Equals(dr["ClientName"]))
                {
                    pj.ClientName = dr["ClientName"].ToString();
                }
                if (!DBNull.Value.Equals(dr["ContactEmail"]))
                {
                    pj.ContactEmail = dr["ContactEmail"].ToString();
                }
                if (!DBNull.Value.Equals(dr["ContactName"]))
                {
                    pj.ContactName = dr["ContactName"].ToString();
                }
                if (!DBNull.Value.Equals(dr["ContactPhone"]))
                {
                    pj.ContactPhone = dr["ContactPhone"].ToString();
                }
                if (!DBNull.Value.Equals(dr["CreatedDate"]))
                {
                    pj.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                }
                if (!DBNull.Value.Equals(dr["UpdatedDate"]))
                {
                    pj.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                }
                if (!DBNull.Value.Equals(dr["CreatedBy"]))
                {
                    pj.CreatedBy = dr["CreatedBy"].ToString();
                }
                if (!DBNull.Value.Equals(dr["UpdatedBy"]))
                {
                    pj.UpdatedBy = dr["UpdatedBy"].ToString();
                }
                if (!DBNull.Value.Equals(dr["Enabled"]))
                {
                    pj.Enabled = Convert.ToBoolean(dr["Enabled"]);
                }
                if (!DBNull.Value.Equals(dr["Name"]))
                {
                    pj.Name = dr["Name"].ToString();
                }
                pj.Account = new Account { Id = Convert.ToInt32(dr["IdAccount"]) };

                pjList.Add(pj);
            }
            return pjList;
        }

        public IList<Project> FindByUserAndAccount(string userName, Account account, bool? includeInactive)
        {
            StringBuilder commandText = new StringBuilder();
            commandText.Append("select Project.* from Project where IdAccount=?accountId");
            if (includeInactive == null || !includeInactive.Value)
            {
                commandText.Append(" and Enabled=1");
            }
            MySqlParameter[] parameters = new MySqlParameter[1];
            parameters[0] = new MySqlParameter("?accountId", account.Id);
            return GetObjects(commandText, parameters);
        }

        public void SetDisable(int id)
        {
            var ele = FindByKey(id);
            if (ele == null) throw new Exception("The record is removed or not exists");
            string commandText = "update project set Enabled=0 where id=?id";
            MySqlParameter[] parameters = new MySqlParameter[1];
            parameters[0] = new MySqlParameter("?id", id);
            ExecuteCommand(commandText, parameters);
        }

        public Project FindProjectByAccountAndName(Account account, Project project)
        {
            MySqlParameter[] parameters = new MySqlParameter[3];
            parameters[0] = new MySqlParameter("?IdAccount", account.Id);
            parameters[1] = new MySqlParameter("?Name", project.Name);
            parameters[2] = new MySqlParameter("?Id", project.Id);
            IDataReader dr = FindUnique("select Project.* from Project where Enabled=1 and  IdAccount=?IdAccount and Name=?Name and Id<>?Id", parameters);
            return GetObject(dr);
        }

        private static Project GetObject(IDataReader dr)
        {
            Project pj = null;
            if (dr != null && dr.Read())
            {
                pj = new Project();
                pj.Id = Convert.ToInt32(dr["Id"]);
                if (!DBNull.Value.Equals(dr["Domain"]))
                {
                    pj.Domain = dr["Domain"].ToString();
                }
                if (!DBNull.Value.Equals(dr["ClientName"]))
                {
                    pj.ClientName = dr["ClientName"].ToString();
                }
                if (!DBNull.Value.Equals(dr["ContactEmail"]))
                {
                    pj.ContactEmail = dr["ContactEmail"].ToString();
                }
                if (!DBNull.Value.Equals(dr["ContactName"]))
                {
                    pj.ContactName = dr["ContactName"].ToString();
                }
                if (!DBNull.Value.Equals(dr["ContactPhone"]))
                {
                    pj.ContactPhone = dr["ContactPhone"].ToString();
                }
                if (!DBNull.Value.Equals(dr["CreatedDate"]))
                {
                    pj.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                }
                if (!DBNull.Value.Equals(dr["UpdatedDate"]))
                {
                    pj.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                }
                if (!DBNull.Value.Equals(dr["CreatedBy"]))
                {
                    pj.CreatedBy = dr["CreatedBy"].ToString();
                }
                if (!DBNull.Value.Equals(dr["UpdatedBy"]))
                {
                    pj.UpdatedBy = dr["UpdatedBy"].ToString();
                }
                if (!DBNull.Value.Equals(dr["Enabled"]))
                {
                    pj.Enabled = Convert.ToBoolean(dr["Enabled"]);
                }
                if (!DBNull.Value.Equals(dr["Name"]))
                {
                    pj.Name = dr["Name"].ToString();
                }
                pj.Account = new Account { Id = Convert.ToInt32(dr["IdAccount"]) };

            }
            if (dr != null)
            {
                dr.Close();
            }
            return pj;
        }

        #region Columns Metadata

        public static class Columns
        {
            //public static String Calificacion = "Calificacion";
            public static String Account = "Account";
            public static String ClientName = "ClientName";
            public static String Competitor = "Competitor";
            public static String ContactEmail = "ContactEmail";
            public static String ContactName = "ContactName";
            public static String ContactPhone = "ContactPhone";
            public static String CreatedBy = "CreatedBy";
            public static String CreatedDate = "CreatedDate";
            public static String Domain = "Domain";
            public static String Enabled = "Enabled";
            public static String Id = "Id";
            public static String KeywordList = "KeywordList";
            public static String Name = "Name";
            public static String ProjectUser = "ProjectUser";
            public static String UpdatedBy = "UpdatedBy";
            public static String UpdatedDate = "UpdatedDate";
        }

        #endregion

    }
}
