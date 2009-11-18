using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.IO;
using NHibernate.AdoNet.Util;
using NHibernate.Dialect;
using NHibernate.Util;
using NHibernateDataStore.Common;

namespace SchemaExporter
{
    public static class SqlExecutor
    {


        public static void CreateSchemaFromEntitiesAssembly(string connectionName, string fileNameToExportSql, bool executeSql)
        {
            var cnp =
                NHibernateConfigurationManager.ConfigurationHelper.Create(connectionName);

            var cfg = cnp.CreateNHibernateConfiguration(ConfigurationFlags.Settings | ConfigurationFlags.MappingsToExport);

            var ddlExport = new NHibernate.Tool.hbm2ddl.SchemaExport(cfg);

            var exportFile = !string.IsNullOrEmpty(fileNameToExportSql);

            if (exportFile)
            {
                ddlExport.SetOutputFile(fileNameToExportSql);
            }
            ddlExport.Create(exportFile, executeSql);
            /*
            var ddlupadte = new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg);

            ddlupadte.Execute(true, true);*/
        }

        public static void UpdateSchemaFromEntitiesAssembly(string connectionName, string fileNameToExportSql, bool executeSql)
        {
            var cnp =
                NHibernateConfigurationManager.ConfigurationHelper.Create(connectionName);

            var cfg = cnp.CreateNHibernateConfiguration(ConfigurationFlags.Settings | ConfigurationFlags.MappingsToExport);

            var ddlUpdate = new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg);


            var exportFile = !string.IsNullOrEmpty(fileNameToExportSql);

            if (exportFile)
            {
                var queries = string.Empty;

                ddlUpdate.Execute(delegate(string s) { queries = string.Format("{0};{1}", s, Environment.NewLine); }, executeSql);

                if (!String.IsNullOrEmpty(queries))
                    File.WriteAllText(fileNameToExportSql, queries);
            }

            else
            {
                ddlUpdate.Execute(false, true);
            }

            if (ddlUpdate.Exceptions.Count > 0)
            {
                throw ddlUpdate.Exceptions[0];
            }
        }

        public static void ValidateSchemaFromEntitiesAssembly(string connectionName)
        {
            var cnp =
                NHibernateConfigurationManager.ConfigurationHelper.Create(connectionName);

            var cfg = cnp.CreateNHibernateConfiguration(ConfigurationFlags.Settings | ConfigurationFlags.MappingsToExport);

            var ddlValidate = new NHibernate.Tool.hbm2ddl.SchemaValidator(cfg);

            ddlValidate.Validate();


        }

        public delegate bool OnTransaction(string s);

        public static void ExecuteInTransaction(OnTransaction onTransaction, string connectionName)
        {
            using (var t = new TransactionScope(connectionName))
            {
                if (onTransaction != null)
                    if (onTransaction(connectionName))
                        t.Commit();
            }
        }

        public static IEnumerable<string> GetSqlDropSchemaFromEntitiesAssembly(string connectionName)
        {
            var cnp =
              NHibernateConfigurationManager.ConfigurationHelper.Create(connectionName);

            var cfg = cnp.CreateNHibernateConfiguration(ConfigurationFlags.Settings | ConfigurationFlags.MappingsToExport);

            var dialect = Dialect.GetDialect(cfg.Properties);

            var sqlDrop = cfg.GenerateDropSchemaScript(dialect);

            var formatter = (PropertiesHelper.GetBoolean(NHibernate.Cfg.Environment.FormatSql, cfg.Properties, true) ? FormatStyle.Ddl : FormatStyle.None).Formatter;


            foreach (var s in sqlDrop)
            {
                yield return String.Format("{0};{1}", formatter.Format(s), Environment.NewLine);
            }



        }

        public static IEnumerable<string> GetSqlCreateSchemaFromEntitiesAssembly(string connectionName)
        {
            var cnp =
              NHibernateConfigurationManager.ConfigurationHelper.Create(connectionName);

            var cfg = cnp.CreateNHibernateConfiguration(ConfigurationFlags.Settings | ConfigurationFlags.MappingsToExport);

            var dialect = Dialect.GetDialect(cfg.Properties);

            var sqlCreate = cfg.GenerateSchemaCreationScript(dialect);

            var formatter = (PropertiesHelper.GetBoolean(NHibernate.Cfg.Environment.FormatSql, cfg.Properties, true) ? FormatStyle.Ddl : FormatStyle.None).Formatter;

            foreach (var s in sqlCreate)
            {
                yield return String.Format("{0};{1}", formatter.Format(s), Environment.NewLine);

            }

        }

        public static void ExecuteSqlScript(string script, string connectionName, bool throwExceptions)
        {
            var cfgHelper =
               NHibernateConfigurationManager.ConfigurationHelper;

            var session = cfgHelper.GetCurrentSession(connectionName);
            using (var tran = new TransactionScope(connectionName))
            {
                try
                {
                    var cmd = session.Connection.CreateCommand();
                    cmd.CommandText = script;
                    session.Transaction.Enlist(cmd);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LoggerFacade.Log.LogException(typeof(SqlExecutor), ex);
                    if (throwExceptions)
                        throw;
                }

                tran.Commit();
            }
        }

        public static void ExecuteSqlScript(IEnumerable<string> script, string connectionName, bool throwExceptions)
        {
            var cfgHelper =
               NHibernateConfigurationManager.ConfigurationHelper;

            var session = cfgHelper.GetCurrentSession(connectionName);
            using (var tran = new TransactionScope(connectionName))
            {
                foreach (var s in script)
                {
                    try
                    {
                        var cmd = session.Connection.CreateCommand();
                        cmd.CommandText = s;
                        session.Transaction.Enlist(cmd);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LoggerFacade.Log.LogException(typeof(SqlExecutor), ex);
                        if (throwExceptions)
                            throw;
                    }
                }


                tran.Commit();
            }
        }

        public static void ExecuteSqlScriptWithoutTransaction(string script, string connectionName, bool throwExceptions)
        {
            var cfgHelper =
           NHibernateConfigurationManager.ConfigurationHelper;

            var session = cfgHelper.GetCurrentSession(connectionName);

            try
            {
                var cmd = session.Connection.CreateCommand();
                cmd.CommandText = script;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LoggerFacade.Log.LogException(typeof(SqlExecutor), ex);
                if (throwExceptions)
                    throw;
            }
        }

        public static void ExecuteSqlScript(string script, string connectionName)
        {
            ExecuteSqlScript(script, connectionName, true);
        }

        public static string GetCurrentDb(string connectionName)
        {
            return NHibernateConfigurationManager.ConfigurationHelper.GetDataBaseFromConnection(
                connectionName);

        }
    }

}
