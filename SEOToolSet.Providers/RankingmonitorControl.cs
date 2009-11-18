using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using SEOToolSet.Common;

namespace SEOToolSet.Providers
{
    public class RankingmonitorControl
    {
        public static string  GetStatistics(string IdProject)
        {
            string EndDate = string.Empty;
            StringBuilder strSb = new StringBuilder();
            DataTable tb=GetStatisticsInfor(IdProject,ref EndDate);
            if (null != tb)
            {
                strSb.AppendFormat("{0}#{1}#{2}#{3}",
                    tb.Rows[0]["PageRank"].ToString().Trim(),
                    tb.Rows[0]["Inboundlinks"].ToString().Trim(),
                    tb.Rows[0]["PagesIndexed"].ToString().Trim(),
                    string.Format("{0:dd/MM/yyyy}", EndDate)
                );

            }

            return strSb.ToString();

        }

        //public static string GetMonitor(string IdProject)
        //{

        //}
       
        private static DataTable GetStatisticsInfor(string idProject,ref string enddate)
        {
            string str = string.Format("select id,enddate from rankingmonitorrun where status='C' and idproject={0} order by enddate desc limit 0,1", idProject);
            DataSet ds = SqlHelper.ExecuteQuery(Common.config(), str);
            if (null != ds && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string sql = string.Format("select SUM(pagerank) as PageRank,sum(inboundlinks) as Inboundlinks,sum(pagesindexed) as PagesIndexed  from rankingmonitordeeprun where IdRankingMonitorRun={0} and status='C'", ds.Tables[0].Rows[0]["id"].ToString());
                DataSet dst = SqlHelper.ExecuteQuery(Common.config(), sql);
                if (null != dst && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                {
                    enddate = ds.Tables[0].Rows[0]["enddate"].ToString();
                    return dst.Tables[0];
                }
                else
                    return null;
            }
            else
                return null;
            
        }
    }
}
