using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using MySql.Data.MySqlClient;

namespace MySqlDataStore.Common
{
    public interface IDao<IdT>
    {
        IDbConnection Connection { get; }
        DataTable Find(string commandText, IDataParameter[] dataParmas);
        DataTable Find(string commandText, IDataParameter[] dataParmas, IPagingInfo pagingInfo);
        DataTable FindAll();

        IDataReader FindByKey(string commandText, IdT id, bool shouldLock);
        IDataReader FindByKey(string commandText, IdT id);
        IDataReader FindUnique(string commandText, IDataParameter[] dataParmas);

        int ExecuteCommand(string commandText, IDataParameter[] dataParmas);
        IDataReader InsertOrUpdateCopy(string commandText, IDataParameter[] dataParmas);

        int Attach(string commandText);
        object Count(string commandText);

        DataTable Find(string commandText);
    }
}
