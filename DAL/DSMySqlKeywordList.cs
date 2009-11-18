using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SEOToolSet.Entities;
using MySqlDataStore.Common;
using MySql.Data.MySqlClient;

namespace SEOToolSet.DAL
{
    public class DSMySqlKeywordList : EntityDataStoreBase<Int32>
    {
        public DSMySqlKeywordList(IDbConnection connection)
            : base(connection)
        {
        }
        public static DSMySqlKeywordList Create(String conStr)
        {
            return new DSMySqlKeywordList(new MySqlConnection(conStr));
        }
        public IList<KeywordList> FindByIdProject(int idProject)
        {
            StringBuilder commandText = new StringBuilder();
            commandText.Append("select KeywordList.* from KeywordList where IdProject=?IdProject");
            MySqlParameter[] parameters = new MySqlParameter[1];
            parameters[0] = new MySqlParameter("?IdProject", idProject);
            return GetObjects(commandText, parameters);
        }

        public void DeleteIncludingChilds(int id)
        {

        }
        private IList<KeywordList> GetObjects(StringBuilder commandText, MySqlParameter[] parameters)
        {
            List<KeywordList> list = new List<KeywordList>();
            DataTable dt = Find(commandText.ToString(), parameters);
            foreach (DataRow dr in dt.Rows)
            {
                KeywordList obj = new KeywordList();
                obj.Id = Convert.ToInt32(dr["Id"]);
                if (!DBNull.Value.Equals(dr["Name"]))
                {
                    obj.Name = dr["Name"].ToString();
                }
                if (!DBNull.Value.Equals(dr["Enabled"]))
                {
                    obj.Enabled = Convert.ToBoolean(dr["Enabled"]);
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
            public static String Enabled = "Enabled";
            public static String Id = "Id";
            public static String Keyword = "Keyword";
            public static String Name = "Name";
            public static String Project = "Project";
        }

        #endregion
    }
}
