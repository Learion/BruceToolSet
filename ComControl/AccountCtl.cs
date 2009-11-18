using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SEOToolSet.Model;
using SEOToolSet;

namespace SEOToolSet.ComControl
{
    public class AccountCtl
    {
        private string strConnection;
        public AccountCtl(string strCon)
        {
            strConnection = strCon;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Accoun ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteQuery(strConnection,strSql.ToString());
        }

        public Account GetModelByID(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Accoun where id=");
            strSql.Append(ID);
            DataSet ds = SqlHelper.ExecuteQuery(strConnection, strSql.ToString());
            if (null != ds && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Model.Account account = new Account();
                account.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString().Trim());
                account.Name = ds.Tables[0].Rows[0]["Name"].ToString().Trim();
                if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MaxNumberOfUser"].ToString().Trim()))
                    account.MaxNumberOfUser = int.Parse(ds.Tables[0].Rows[0]["MaxNumberOfUser"].ToString().Trim());
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MaxNumberOfDomainUser"].ToString().Trim()))
                    account.MaxNumberOfDomainUser = int.Parse(ds.Tables[0].Rows[0]["MaxNumberOfDomainUser"].ToString().Trim());
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MaxNumberOfProjects"].ToString().Trim()))
                    account.MaxNumberOfProjects = int.Parse(ds.Tables[0].Rows[0]["MaxNumberOfProjects"].ToString().Trim());
                account.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString().Trim();
                account.CompanyAddress1 = ds.Tables[0].Rows[0]["CompanyAddress1"].ToString().Trim();
                account.CompanyAddress2 = ds.Tables[0].Rows[0]["CompanyAddress2"].ToString().Trim();
                account.CompanyCity = ds.Tables[0].Rows[0]["CompanyCity"].ToString().Trim();
                account.CompanyZip = ds.Tables[0].Rows[0]["CompanyZip"].ToString().Trim();
                account.CreditCardNumber = ds.Tables[0].Rows[0]["CreditCardNumber"].ToString().Trim();
                account.CreditCardCvs = ds.Tables[0].Rows[0]["CreditCardCvs"].ToString().Trim();
                account.CreditCardAddress1 = ds.Tables[0].Rows[0]["CreditCardAddress1"].ToString().Trim();
                account.CreditCardAddress2 = ds.Tables[0].Rows[0]["CreditCardAddress2"].ToString().Trim();
                account.CreditCardCity = ds.Tables[0].Rows[0]["CreditCardCity"].ToString().Trim();
                account.CreditCardZip = ds.Tables[0].Rows[0]["CreditCardZip"].ToString().Trim();
                account.RecurringBill =
                    string.IsNullOrEmpty(ds.Tables[0].Rows[0]["RecurringBill"].ToString().Trim()) ? false : bool.Parse(ds.Tables[0].Rows[0]["RecurringBill"].ToString().Trim());
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CreatedDate"].ToString().Trim()))
                    account.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString().Trim());
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["UpdatedDate"].ToString().Trim()))
                    account.UpdatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString().Trim());

                account.Enabled = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Enabled"].ToString().Trim()) ? false : bool.Parse(ds.Tables[0].Rows[0]["Enabled"].ToString().Trim());
                account.PromoCode = ds.Tables[0].Rows[0]["PromoCode"].ToString().Trim();
                if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CompanyIdCountry"].ToString().Trim()))
                    account.CompanyIdCountry = int.Parse(ds.Tables[0].Rows[0]["CompanyIdCountry"].ToString().Trim());
                account.CompanyPhone = ds.Tables[0].Rows[0]["CompanyPhone"].ToString().Trim();
                account.Enabled = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Enabled"].ToString().Trim()) ? false : bool.Parse(ds.Tables[0].Rows[0]["Enabled"].ToString().Trim());
                account.CreditCardPhone = ds.Tables[0].Rows[0]["CreditCardPhone"].ToString().Trim();
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["LastBillingDate"].ToString().Trim()))
                    account.LastBillingDate = DateTime.Parse(ds.Tables[0].Rows[0]["LastBillingDate"].ToString().Trim());

                return account;
            }
            else
                return null;
        }

        public List<Account> GetModelByUser(string strWhere)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from Accoun");
            if (!string.IsNullOrEmpty(strWhere))
                sb.Append("  where " + strWhere);
            DataSet ds = SqlHelper.ExecuteQuery(strConnection, sb.ToString());
            if (null != ds && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<Account> list = new List<Account>();

                foreach (DataRow rw in ds.Tables[0].Rows)
                {
                    Model.Account account = new Account();
                    account.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString().Trim());
                    account.Name = ds.Tables[0].Rows[0]["Name"].ToString().Trim();
                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MaxNumberOfUser"].ToString().Trim()))
                        account.MaxNumberOfUser = int.Parse(ds.Tables[0].Rows[0]["MaxNumberOfUser"].ToString().Trim());
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MaxNumberOfDomainUser"].ToString().Trim()))
                        account.MaxNumberOfDomainUser = int.Parse(ds.Tables[0].Rows[0]["MaxNumberOfDomainUser"].ToString().Trim());
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MaxNumberOfProjects"].ToString().Trim()))
                        account.MaxNumberOfProjects = int.Parse(ds.Tables[0].Rows[0]["MaxNumberOfProjects"].ToString().Trim());
                    account.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString().Trim();
                    account.CompanyAddress1 = ds.Tables[0].Rows[0]["CompanyAddress1"].ToString().Trim();
                    account.CompanyAddress2 = ds.Tables[0].Rows[0]["CompanyAddress2"].ToString().Trim();
                    account.CompanyCity = ds.Tables[0].Rows[0]["CompanyCity"].ToString().Trim();
                    account.CompanyZip = ds.Tables[0].Rows[0]["CompanyZip"].ToString().Trim();
                    account.CreditCardNumber = ds.Tables[0].Rows[0]["CreditCardNumber"].ToString().Trim();
                    account.CreditCardCvs = ds.Tables[0].Rows[0]["CreditCardCvs"].ToString().Trim();
                    account.CreditCardAddress1 = ds.Tables[0].Rows[0]["CreditCardAddress1"].ToString().Trim();
                    account.CreditCardAddress2 = ds.Tables[0].Rows[0]["CreditCardAddress2"].ToString().Trim();
                    account.CreditCardCity = ds.Tables[0].Rows[0]["CreditCardCity"].ToString().Trim();
                    account.CreditCardZip = ds.Tables[0].Rows[0]["CreditCardZip"].ToString().Trim();
                    account.RecurringBill =
                        string.IsNullOrEmpty(ds.Tables[0].Rows[0]["RecurringBill"].ToString().Trim()) ? false : bool.Parse(ds.Tables[0].Rows[0]["RecurringBill"].ToString().Trim());
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CreatedDate"].ToString().Trim()))
                        account.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString().Trim());
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["UpdatedDate"].ToString().Trim()))
                        account.UpdatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString().Trim());

                    account.Enabled = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Enabled"].ToString().Trim()) ? false : bool.Parse(ds.Tables[0].Rows[0]["Enabled"].ToString().Trim());
                    account.PromoCode = ds.Tables[0].Rows[0]["PromoCode"].ToString().Trim();
                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CompanyIdCountry"].ToString().Trim()))
                        account.CompanyIdCountry = int.Parse(ds.Tables[0].Rows[0]["CompanyIdCountry"].ToString().Trim());
                    account.CompanyPhone = ds.Tables[0].Rows[0]["CompanyPhone"].ToString().Trim();
                    account.Enabled = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Enabled"].ToString().Trim()) ? false : bool.Parse(ds.Tables[0].Rows[0]["Enabled"].ToString().Trim());
                    account.CreditCardPhone = ds.Tables[0].Rows[0]["CreditCardPhone"].ToString().Trim();
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["LastBillingDate"].ToString().Trim()))
                        account.LastBillingDate = DateTime.Parse(ds.Tables[0].Rows[0]["LastBillingDate"].ToString().Trim());

                    list.Add(account);
                }
                return list;
            }
            else
                return null;
        }
    }
}
