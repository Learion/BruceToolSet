using System;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DBUtility
{
    public abstract class SqlHelper
    {

        //执行单条插入语句，并返回id，不需要返回id的用ExceuteNonQuery执行。
        public static int ExecuteInsert(string connectionString,string sql, MySqlParameter[] parameters)
        {
            //Debug.WriteLine(sql);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"select LAST_INSERT_ID()";
                    int value = Int32.Parse(cmd.ExecuteScalar().ToString());
                    return value;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public static int ExecuteInsert(string connectionString,string sql)
        {
            return ExecuteInsert(connectionString, sql, null);
        }

        //执行带参数的sql语句,返回影响的记录数（insert,update,delete)
        public static int ExecuteNonQuery(string connectionString, string sql, MySqlParameter[] parameters)
        {
            //Debug.WriteLine(sql);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        //执行不带参数的sql语句，返回影响的记录数
        public static int ExecuteNonQuery(string connectionString,string sql)
        {
            return ExecuteNonQuery(connectionString,sql, null);
        }

        //执行单条语句返回第一行第一列,可以用来返回count(*)
        public static int ExecuteScalar(string connectionString, string sql, MySqlParameter[] parameters)
        {
            //Debug.WriteLine(sql);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    int value = Int32.Parse(cmd.ExecuteScalar().ToString());
                    return value;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public static int ExecuteScalar(string connectionString,string sql)
        {
            return ExecuteScalar(connectionString,sql, null);
        }

        //执行事务
        public static void ExecuteTrans(string connectionString, List<string> sqlList, List<MySqlParameter[]> paraList)
        {
            //Debug.WriteLine(sql);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                MySqlTransaction transaction = null;
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    cmd.Transaction = transaction;

                    for (int i = 0; i < sqlList.Count; i++)
                    {
                        cmd.CommandText = sqlList[i];
                        if (paraList != null && paraList[i] != null)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(paraList[i]);
                        }
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();

                }
                catch (Exception e)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch
                    {

                    }
                    throw e;
                }

            }
        }
        public static void ExecuteTrans(string connectionString,List<string> sqlList)
        {
            ExecuteTrans(connectionString,sqlList, null);
        }

        //执行查询语句，返回dataset
        public static DataSet ExecuteQuery(string connectionString, string sql, MySqlParameter[] parameters)
        {
            //Debug.WriteLine(sql);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();

                    MySqlDataAdapter da = new MySqlDataAdapter(sql, connection);
                    if (parameters != null) da.SelectCommand.Parameters.AddRange(parameters);
                    da.Fill(ds, "ds");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return ds;
            }
        }
        public static DataSet ExecuteQuery(string connectionString,string sql)
        {
            return ExecuteQuery(connectionString,sql, null);
        }      
    }
}
