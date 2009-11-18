//using System;
//using System.Collections.Generic;
//using System.Text;
//using MySql.Data.MySqlClient;
//using System.Data;

//namespace SEOToolSet.Common
//{
//    public abstract class SqlHelper
//    {

//        public static int ExecuteInsert(string connectionString, string sql, MySqlParameter[] parameters)
//        {
//            //Debug.WriteLine(sql);
//            using (MySqlConnection connection = new MySqlConnection(connectionString))
//            {
//                MySqlCommand cmd = new MySqlCommand(sql, connection);
//                try
//                {
//                    connection.Open();
//                    if (parameters != null) cmd.Parameters.AddRange(parameters);
//                    cmd.ExecuteNonQuery();
//                    cmd.CommandText = @"select LAST_INSERT_ID()";
//                    int value = Int32.Parse(cmd.ExecuteScalar().ToString());
//                    return value;
//                }
//                catch (Exception e)
//                {
//                    //log
//                    throw e;
//                }
//            }
//        }
//        public static int ExecuteInsert(string connectionString, string sql)
//        {
//            return ExecuteInsert(connectionString, sql, null);
//        }

//        public static int ExecuteNonQuery(string connectionString, string sql, MySqlParameter[] parameters)
//        {
//            //Debug.WriteLine(sql);
//            using (MySqlConnection connection = new MySqlConnection(connectionString))
//            {
//                MySqlCommand cmd = new MySqlCommand(sql, connection);
//                try
//                {
//                    connection.Open();
//                    if (parameters != null) cmd.Parameters.AddRange(parameters);
//                    int rows = cmd.ExecuteNonQuery();
//                    return rows;
//                }
//                catch (Exception e)
//                {
//                    //log
//                    throw e;
//                }
//            }
//        }

//        public static int ExecuteNonQuery(string connectionString, string sql)
//        {
//            return ExecuteNonQuery(connectionString, sql, null);
//        }


//        public static int ExecuteScalar(string connectionString, string sql, MySqlParameter[] parameters)
//        {
//            //Debug.WriteLine(sql);
//            using (MySqlConnection connection = new MySqlConnection(connectionString))
//            {
//                MySqlCommand cmd = new MySqlCommand(sql, connection);
//                try
//                {
//                    connection.Open();
//                    if (parameters != null) cmd.Parameters.AddRange(parameters);
//                    int value = Int32.Parse(cmd.ExecuteScalar().ToString());
//                    return value;
//                }
//                catch (Exception e)
//                {
//                    //log
//                    throw e;
//                }
//            }
//        }
//        public static int ExecuteScalar(string connectionString, string sql)
//        {
//            return ExecuteScalar(connectionString, sql, null);
//        }


//        public static void ExecuteTrans(string connectionString, List<string> sqlList, List<MySqlParameter[]> paraList)
//        {
//            //Debug.WriteLine(sql);
//            using (MySqlConnection connection = new MySqlConnection(connectionString))
//            {
//                MySqlCommand cmd = new MySqlCommand();
//                MySqlTransaction transaction = null;
//                cmd.Connection = connection;
//                try
//                {
//                    connection.Open();
//                    transaction = connection.BeginTransaction();
//                    cmd.Transaction = transaction;

//                    for (int i = 0; i < sqlList.Count; i++)
//                    {
//                        cmd.CommandText = sqlList[i];
//                        if (paraList != null && paraList[i] != null)
//                        {
//                            cmd.Parameters.Clear();
//                            cmd.Parameters.AddRange(paraList[i]);
//                        }
//                        cmd.ExecuteNonQuery();
//                    }
//                    //log
//                    transaction.Commit();

//                }
//                catch (Exception e)
//                {
//                    try
//                    {
//                        transaction.Rollback();
//                    }
//                    catch
//                    {

//                    }
//                    //log
//                    throw e;
//                }

//            }
//        }
//        public static void ExecuteTrans(string connectionString, List<string> sqlList)
//        {
//            ExecuteTrans(connectionString, sqlList, null);
//        }


//        public static DataSet ExecuteQuery(string connectionString, string sql, MySqlParameter[] parameters)
//        {
//            //Debug.WriteLine(sql);
//            using (MySqlConnection connection = new MySqlConnection(connectionString))
//            {
//                DataSet ds = new DataSet();
//                try
//                {
//                    connection.Open();

//                    MySqlDataAdapter da = new MySqlDataAdapter(sql, connection);
//                    if (parameters != null) da.SelectCommand.Parameters.AddRange(parameters);
//                    da.Fill(ds, "ds");
//                }
//                catch (Exception ex)
//                {
//                    //log
//                    throw ex;
//                }
//                //log
//                return ds;
//            }
//        }
//        public static DataSet ExecuteQuery(string connectionString, string sql)
//        {
//            return ExecuteQuery(connectionString, sql, null);
//        }
//    }
//}
