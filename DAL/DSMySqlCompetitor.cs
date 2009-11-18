using System;
using System.Collections.Generic;
using System.Text;
using MySqlDataStore.Common;
using System.Data;
using SEOToolSet.Entities;
using MySql.Data.MySqlClient;

namespace SEOToolSet.DAL
{
    public class DSMySqlCompetitor : EntityDataStoreBase<Int32>
    {
        public DSMySqlCompetitor(IDbConnection connection)
            : base(connection)
        {
        }
        public static DSMySqlCompetitor Create(String conStr)
        {
            return new DSMySqlCompetitor(new MySqlConnection(conStr));
        }

        public IList<Competitor> FindByIdProject(int idProject)
        {
            StringBuilder commandText = new StringBuilder();
            commandText.Append("select Competitor.* from Competitor where IdProject=?IdProject");
            MySqlParameter[] parameters = new MySqlParameter[1];
            parameters[0] = new MySqlParameter("?IdProject", idProject);
            return GetObjects(commandText, parameters);
        }

        private IList<Competitor> GetObjects(StringBuilder commandText, MySqlParameter[] parameters)
        {
            List<Competitor> list = new List<Competitor>();
            DataTable dt = Find(commandText.ToString(), parameters);
            foreach (DataRow dr in dt.Rows)
            {
                Competitor obj = new Competitor();
                obj.Id = Convert.ToInt32(dr["Id"]);
                if (!DBNull.Value.Equals(dr["Name"]))
                {
                    obj.Name = dr["Name"].ToString();
                }
                if (!DBNull.Value.Equals(dr["Url"]))
                {
                    obj.Url = dr["Url"].ToString();
                }
                if (!DBNull.Value.Equals(dr["Description"]))
                {
                    obj.Description = dr["Description"].ToString();
                }
                obj.Project = new Project { Id = Convert.ToInt32(dr["IdProject"]) };
                list.Add(obj);
            }
            return list;
        }

        #region Columns Metadata

        public static class Columns
        {
            //public static String Calificacion = "Calificacion";
            public static String Description = "Description";
            public static String Id = "Id";
            public static String Name = "Name";
            public static String Project = "Project";
            public static String Url = "Url";
        }

        #endregion
    }
}
