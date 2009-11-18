using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace MySqlDataStore.Common
{
    public abstract class EntityDataStoreBase<TId> : IDao<TId>
    {
        protected EntityDataStoreBase(IDbConnection connection)
        {
            Connection = connection;
        }

        #region IDao<T,TId> 成员
        public IDbConnection Connection
        {
            get;
            private set;
        }

        public DataTable Find(string commandText, IDataParameter[] dataParmas)
        {
            DataTable dt = new DataTable();
            MySqlConnection conn = (MySqlConnection)Connection;
            MySqlDataAdapter dtd = new MySqlDataAdapter(commandText, conn);
            foreach (MySqlParameter para in dataParmas)
            {
                dtd.SelectCommand.Parameters.Add(para);
            }
            dtd.Fill(dt);
            return dt;
        }

        public DataTable Find(string commandText, IDataParameter[] dataParmas, IPagingInfo pagingInfo)
        {
            throw new NotImplementedException();
        }

        public DataTable FindAll()
        {
            return new DataTable();
        }

        public IDataReader FindByKey(string commandText, TId id, bool shouldLock)
        {
            throw new NotImplementedException();
        }

        public IDataReader FindByKey(string commandText, TId id)
        {
            IDataReader dr = null;
            MySqlConnection conn = (MySqlConnection)Connection;
            try
            {
                MySqlCommand cmd = new MySqlCommand(commandText, conn);
                cmd.Parameters.AddWithValue("?id", id);
                OpenConnection(cmd.Connection);
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                CloseConnection(conn);
                throw;
            }
            return dr;
        }

        public IDataReader FindUnique(string commandText, IDataParameter[] dataParmas)
        {
            IDataReader dr = null;
            MySqlConnection conn = (MySqlConnection)Connection;
            try
            {
                MySqlCommand cmd = new MySqlCommand(commandText, conn);
                foreach (MySqlParameter para in dataParmas)
                {
                    cmd.Parameters.Add(para);
                }
                OpenConnection(cmd.Connection);
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                CloseConnection(conn);
                throw;
            }
            return dr;
        }

        private void OpenConnection(MySqlConnection conn)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }

        private static void CloseConnection(MySqlConnection conn)
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        public int ExecuteCommand(string commandText, IDataParameter[] dataParmas)
        {
            int count = 0;
            MySqlConnection conn = (MySqlConnection)Connection;
            try
            {
                MySqlCommand cmd = new MySqlCommand(commandText, conn);
                foreach (MySqlParameter para in dataParmas)
                {
                    cmd.Parameters.Add(para);
                }
                OpenConnection(cmd.Connection);
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection(conn);
            }
            return count;
        }

        public IDataReader InsertOrUpdateCopy(string commandText, IDataParameter[] dataParmas)
        {
            throw new NotImplementedException();
        }

        public int Attach(string commandText)
        {
            throw new NotImplementedException();
        }

        public object Count(string commandText)
        {
            object value = null;
            MySqlConnection conn = (MySqlConnection)Connection;

            try
            {
                MySqlCommand cmd = new MySqlCommand(commandText, conn);
                OpenConnection(cmd.Connection);
                value = cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection(conn);
            }

            return value;
        }

        public DataTable Find(string commandText)
        {
            DataTable dt = new DataTable();
            MySqlConnection conn = (MySqlConnection)Connection;
            MySqlDataAdapter dtd = new MySqlDataAdapter(commandText, conn);
            dtd.Fill(dt);
            return dt;
        }

        #endregion
    }
}
