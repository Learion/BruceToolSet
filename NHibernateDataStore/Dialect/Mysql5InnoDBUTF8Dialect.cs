using System.Data;
using NHibernate.SqlCommand;

namespace NHibernateDataStore.Dialect
{
    public class Mysql5InnoDbutf8Dialect : NHibernate.Dialect.MySQLDialect
    {
        public Mysql5InnoDbutf8Dialect()
        {
            //Fix for mysql Length
            RegisterColumnType(DbType.String, 1024, "VARCHAR($l)");
        }

        public override string TableTypeString
        {
            get
            {
                return " ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE utf8_general_ci";
            }
        }

        public override bool SupportsVariableLimit
        {
            get
            {
                //note: why false?
                return false;
            }
        }

        public override bool SupportsSubSelects
        {
            get
            {
                //subquery in mysql? yes! From 4.1!
                //http://dev.mysql.com/doc/refman/5.1/en/subqueries.html
                return true;
            }
        }


        public override string GetTableComment(string comment)
        {
            return TableTypeString;
        }

        public override SqlString GetLimitString(SqlString querySqlString, int offset, int limit)
        {
            var pagingBuilder = new SqlStringBuilder();

            pagingBuilder.Add(querySqlString);
            pagingBuilder.Add(" limit ");
            if (offset > 0)
            {
                pagingBuilder.Add(offset.ToString());
                pagingBuilder.Add(", ");
            }

            pagingBuilder.Add(limit.ToString());

            return pagingBuilder.ToSqlString();
        }

        public override string SelectGUIDString
        {
            get
            {
                return "select uuid()";
            }
        }
    }
}
