using System;
using System.Data;
using System.Threading.Tasks;

#if NET40 || NET45
using System.Data.SqlClient;
#endif

#if !NET40

using SqlCommand = System.Data.Common.DbCommand;


namespace ToolGood.ReadyGo3.PetaPoco.Core
{
    partial class DatabaseProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="cmd"></param>
        /// <param name="PrimaryKeyName"></param>
        /// <returns></returns>
        public virtual async Task<object> ExecuteInsert_Async(Database db, SqlCommand cmd, string PrimaryKeyName)
        {
            cmd.CommandText += ";\nSELECT @@IDENTITY AS NewID;";
            return await db.ExecuteScalarHelper_Async(cmd);
        }
    }
}

namespace ToolGood.ReadyGo3.PetaPoco.Providers
{
    partial class FirebirdDbDatabaseProvider
    {
        public override Task<object> ExecuteInsert_Async(Database database, SqlCommand cmd, string primaryKeyName)
        {
            cmd.CommandText = cmd.CommandText.TrimEnd();

            if (cmd.CommandText.EndsWith(";"))
                cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 1);

            cmd.CommandText += " RETURNING " + EscapeSqlIdentifier(primaryKeyName) + ";";
            return database.ExecuteScalarHelper_Async(cmd);
        }
    }
    partial class MsAccessDbDatabaseProvider
    {
        public override async Task<object> ExecuteInsert_Async(Database database, SqlCommand cmd, string primaryKeyName)
        {
            await database.ExecuteNonQueryHelper_Async(cmd);
            cmd.CommandText = "SELECT @@IDENTITY AS NewID;";
            return await database.ExecuteScalarHelper_Async(cmd);
        }
    }
    partial class MySqlDatabaseProvider
    {

    }
    partial class OracleDatabaseProvider
    {

        public override async Task<object> ExecuteInsert_Async(Database database, SqlCommand cmd, string primaryKeyName)
        {
            if (primaryKeyName != null) {
                cmd.CommandText += $" returning {EscapeSqlIdentifier(primaryKeyName)} into :newid";
                var param = cmd.CreateParameter();
                param.ParameterName = ":newid";
                param.Value = DBNull.Value;
                param.Direction = ParameterDirection.ReturnValue;
                param.DbType = DbType.Int64;
                cmd.Parameters.Add(param);
                await database.ExecuteNonQueryHelper_Async(cmd);
                return param.Value;
            } else {
                await database.ExecuteNonQueryHelper_Async(cmd);
                return -1;
            }
        }
    }

    partial class PostgreSQLDatabaseProvider
    {
        public override async Task<object> ExecuteInsert_Async(Database database, SqlCommand cmd, string primaryKeyName)
        {
            if (primaryKeyName != null) {
                cmd.CommandText += $"returning {EscapeSqlIdentifier(primaryKeyName)} as NewID";
                return await database.ExecuteScalarHelper_Async(cmd);
            } else {
                await database.ExecuteNonQueryHelper_Async(cmd);
                return -1;
            }
        }
    }
    partial class SQLiteDatabaseProvider
    {
        public override async Task<object> ExecuteInsert_Async(Database database, SqlCommand cmd, string primaryKeyName)
        {
            if (primaryKeyName != null) {
                cmd.CommandText += ";\nSELECT last_insert_rowid();";
                return await database.ExecuteScalarHelper_Async(cmd);
            } else {
                await database.ExecuteNonQueryHelper_Async(cmd);
                return -1;
            }
        }
    }
    partial class SqlServer2012DatabaseProvider
    {

    }
    partial class SqlServerCEDatabaseProviders
    {

    }
    partial class SqlServerDatabaseProvider
    {
        public override Task<object> ExecuteInsert_Async(Database database, SqlCommand cmd, string primaryKeyName)
        {
            return database.ExecuteScalarHelper_Async(cmd);
        }
    }

}
#endif