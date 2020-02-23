using System;
using System.Data;
using System.Reflection;
using Hangfire.PostgreSql;
using Hangfire.Storage;
using Hangfire.Tags.Storage;
using Npgsql;
namespace Hangfire.Tags.PostgreSql
{
    internal class PostgreSqlTagsTransaction : ITagsTransaction
    {
        private readonly PostgreSqlStorageOptions _options;
        private readonly IWriteOnlyTransaction _transaction;

        private static Type _type;
        private static MethodInfo _queueCommand;
        private static Type _sqlCommandBatchParameter;
        private static ConstructorInfo _sqlCommandBatchParameterCtor;
        private static PropertyInfo _sqlCommandBatchParameterValue;

        private static MethodInfo _addCommand;

        public PostgreSqlTagsTransaction(PostgreSqlStorageOptions options, IWriteOnlyTransaction transaction)
        {
            if (transaction.GetType().Name != "PostgreSqlWriteOnlyTransaction")
                throw new ArgumentException("The transaction is not an SQL transaction", nameof(transaction));

            _options = options;
            _transaction = transaction;

            // Dirty, but lazy...we would like to execute these commands in the same transaction, so we're resorting to reflection for now

            // Other transaction type, clear cached methods
            if (_type != transaction.GetType())
            {
                _queueCommand = null;
                _addCommand = null;

                _type = transaction.GetType();
            }

            if (_queueCommand == null)
                _queueCommand = transaction.GetType().GetTypeInfo().GetMethod(nameof(QueueCommand),
                    BindingFlags.NonPublic | BindingFlags.Instance);

            if (_queueCommand == null && _addCommand == null)
                throw new ArgumentException("The functions QueueCommand and AddCommand cannot be found.");
        }
        
        private void QueueCommand(string commandText, params NpgsqlParameter[] parameters)
        {
            _queueCommand.Invoke(_transaction, new object[] { commandText, parameters });
        }

        private void AddCommand(string commandText, string key, params NpgsqlParameter[] parameters)
        {
            
               var batchParams = Array.CreateInstance(_sqlCommandBatchParameter, parameters.Length);
            for (var i = 0; i < parameters.Length; i++)
            {
                var p = parameters[i];
                var param = _sqlCommandBatchParameterCtor.Invoke(new object[] { p.ParameterName, p.DbType, null });
                _sqlCommandBatchParameterValue.SetValue(param, p.Value);
                batchParams.SetValue(param, i);
            }
            var setCommands = _transaction.GetType().GetTypeInfo().GetField("_setCommands", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(_transaction);
            _addCommand.Invoke(_transaction, new object[] { setCommands, key, commandText, batchParams });
        }

        public void ExpireSetValue(string key, string value, TimeSpan expireIn)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            var sql = $@"UPDATE ""{_options.SchemaName}"".set SET expireat = @expireAt WHERE key = @key";

            QueueCommand(sql,
                    new NpgsqlParameter("@key", key),
                    new NpgsqlParameter("@expireAt", DateTime.UtcNow.Add(expireIn)));

            //var sql = $@"UPDATE ""{_options.SchemaName}"".""set"" SET ""expireat"" = @expireAt WHERE ""key"" = @key and ""value"" = @value";
            //if (_queueCommand == null)
            //{
            //    AddCommand(sql,
            //        key,
            //        new NpgsqlParameter("@key", key),
            //        new NpgsqlParameter("@value", value),
            //        new NpgsqlParameter("@expireAt", DateTime.UtcNow.Add(expireIn)));
            //}
            //else
            //{
            //    QueueCommand(sql,
            //        new NpgsqlParameter("@key", key),
            //        new NpgsqlParameter("@value", value),
            //        new NpgsqlParameter("@expireAt", DateTime.UtcNow.Add(expireIn)));
            //}

        }

        public void PersistSetValue(string key, string value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            var query = $@"
";
            if (_queueCommand == null)
            {
            }
            else
            {
                //QueueCommand(query);
            }
        }
    }
}
