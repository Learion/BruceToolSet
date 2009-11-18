using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Reflection;

namespace SEOToolSet.Providers
{
    public  class Common
    {
        public static bool JudgmentDataSet(DataSet ds)
        {
            if (null != ds && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        public static string RetrunStrTrim(object obj)
        {
            return obj.ToString().Trim();
        }

        public static string config()
        {
            return ConfigurationManager.ConnectionStrings["mySqlConn"].ToString();
        }
        public static List<T> DataTableToObject<T>(DataTable dt) where T : new()
        {

            List<PropertyInfo> prlist = new List<PropertyInfo>();
            Type t = typeof(T);
          
            Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });
             
            List<T> oblist = new List<T>();
            foreach (DataRow row in dt.Rows)
            { 
                T ob = new T(); 
                prlist.ForEach(p => { if (row[p.Name] != DBNull.Value) p.SetValue(ob, row[p.Name], null); });
                
                oblist.Add(ob);
            }
            return oblist;
        }

        
    }
}
