using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.ReadyGo3.Gadget.TableManager.Providers
{
    public class MariaDbDatabaseProvider : MySqlDatabaseProvider
    {
    }
    public class MySqlDatabaseProvider : DatabaseProvider
    {
        public override string GetTryCreateTable(Type type, bool withIndex = true)
        {
            var ti = TableInfo.FromType(type);
            var sql = "CREATE TABLE IF NOT EXISTS " + GetTableName(ti) + "(\r\n";
            foreach (var item in ti.Columns) {
                sql += "    " + CreateColumn(ti, item) + ",\r\n";
            }
            if (withIndex) {
                foreach (var item in ti.Indexs) {
                    var txt = "i_" + string.Join("_", item).Replace(" ", "_").Replace("[", "").Replace("]", "");
                    var columns = BuildColumns(item);
                    sql += "    INDEX " + txt + "(" + columns + "),\r\n";
                }
                foreach (var item in ti.Uniques) {
                    var txt = "u_" + string.Join("_", item).Replace(" ", "_").Replace("[", "").Replace("]", "");
                    var columns = BuildColumns(item);
                    sql += "    UNIQUE INDEX " + txt + " ( " + columns + "),\r\n";
                }
            }
            sql = sql.Substring(0, sql.Length - 3);
            sql += "\r\n);";
            return sql;
        }

        public override string GetCreateTable(Type type, bool withIndex = true)
        {
            var ti = TableInfo.FromType(type);
            var sql = "CREATE TABLE " + GetTableName(ti) + "(\r\n";
            foreach (var item in ti.Columns) {
                sql += "    " + CreateColumn(ti, item) + ",\r\n";
            }
            if (withIndex) {
                foreach (var item in ti.Indexs) {
                    var txt = "i_" + string.Join("_", item).Replace(" ", "_").Replace("[", "").Replace("]", "");
                    var columns = BuildColumns(item);
                    sql += "    INDEX " + txt + "(" + columns + "),\r\n";
                }
                foreach (var item in ti.Uniques) {
                    var txt = "u_" + string.Join("_", item).Replace(" ", "_").Replace("[", "").Replace("]", "");
                    var columns = BuildColumns(item);
                    sql += "    UNIQUE INDEX " + txt + " ( " + columns + "),\r\n";
                }
            }
            sql = sql.Substring(0, sql.Length - 3);
            sql += "\r\n);";
            return sql;
        }

        public override string GetCreateIndex(Type type)
        {
            string sql = "";
            var ti = TableInfo.FromType(type);
            foreach (var item in ti.Indexs) {
                var txt = "i_" + string.Join("_", item).Replace(" ", "_").Replace("[", "").Replace("]", "");
                var columns = BuildColumns(item);
                sql += $"ALTER TABLE {GetTableName(ti)} ADD INDEX {txt} ({columns});\r\n";
            }
            foreach (var item in ti.Uniques) {
                var txt = "u_" + string.Join("_", item).Replace(" ", "_").Replace("[", "").Replace("]", "");
                var columns = BuildColumns(item);
                sql += $"ALTER TABLE {GetTableName(ti)} ADD UNIQUE INDEX {txt} ({columns});\r\n";
            }
            return sql;
        }
        private string BuildColumns(List<string> columnList)
        {
            var columns = "";
            foreach (var col in columnList) {
                columns += $"`{col}`,";
            }
            return columns.Replace("[", "`").Replace("]", "`").Replace("``", "`").Trim(',');
        }


        public override string GetDropTable(Type type)
        {
            var ti = TableInfo.FromType(type);
            return "DROP TABLE IF EXISTS " + GetTableName(ti) + ";";
        }

        public override string GetTruncateTable(Type type)
        {
            var ti = TableInfo.FromType(type);
            return "TRUNCATE TABLE " + GetTableName(ti) + ";";
        }

        public string CreateColumn(TableInfo ti, ColumnInfo ci)
        {
            var type = ci.PropertyType;
            var isRequired = ci.Required;
            if (type.IsEnum) return CreateField(ti, ci, "int", ci.FieldLength, true);
            if (type == typeof(string)) return CreateField(ti, ci, ci.IsText ? "Text" : "varchar", ci.IsText ? "" : (string.IsNullOrEmpty(ci.FieldLength) ? "4000" : ci.FieldLength), isRequired);
            if (type == typeof(Byte[]) || type == typeof(SByte[])) {
                if (int.TryParse(ci.FieldLength, out int fieldLength)) {
                    if (fieldLength <= 255) {
                        return CreateField(ti, ci, "tinyblob", "", isRequired);
                    } else if (fieldLength <= 65535) {
                        return CreateField(ti, ci, "blob", "", isRequired);
                    } else if (fieldLength <= 16777215) {
                        return CreateField(ti, ci, "mediumblob", "", isRequired);
                    } else {
                        return CreateField(ti, ci, "longBlob", "", isRequired);
                    }
                } else {
                    return CreateField(ti, ci, "tinyblob", "", isRequired);
                }
            }
            if (type == typeof(AnsiString)) return CreateField(ti, ci, "varchar", ci.FieldLength, isRequired);

            //var isRequired = ColumnType.IsNullType(type) == false;
            //if (isRequired == false) type = ColumnType.GetBaseType(type);

            if (type == typeof(bool)) return CreateField(ti, ci, "tinyint", "1", isRequired);
            if (type == typeof(byte)) return CreateField(ti, ci, "tinyint", "1", isRequired);
            if (type == typeof(char)) return CreateField(ti, ci, "char", "1", isRequired);

            if (type == typeof(UInt16)) return CreateField(ti, ci, "UNSIGNED smallint", ci.FieldLength, isRequired);
            if (type == typeof(UInt32)) return CreateField(ti, ci, "UNSIGNED int", ci.FieldLength, isRequired);
            if (type == typeof(UInt64)) return CreateField(ti, ci, "UNSIGNED bigint", ci.FieldLength, isRequired);
            if (type == typeof(Int16)) return CreateField(ti, ci, "smallint", ci.FieldLength, isRequired);
            if (type == typeof(Int32)) return CreateField(ti, ci, "int", ci.FieldLength, isRequired);
            if (type == typeof(Int64)) return CreateField(ti, ci, "bigint", ci.FieldLength, isRequired);
            if (type == typeof(Single)) return CreateField(ti, ci, "FLOAT", ci.FieldLength, isRequired);
            if (type == typeof(double)) return CreateField(ti, ci, "DOUBLE", ci.FieldLength, isRequired);
            if (type == typeof(decimal)) return CreateField(ti, ci, "decimal", ci.FieldLength, isRequired);
            if (type == typeof(DateTime)) return CreateField(ti, ci, "dateTime", ci.FieldLength, isRequired);
            if (type == typeof(TimeSpan)) return CreateField(ti, ci, "time", ci.FieldLength, isRequired);
            if (type == typeof(DateTimeOffset)) return CreateField(ti, ci, "dateTime", ci.FieldLength, isRequired);



            if (type == typeof(Guid)) return CreateField(ti, ci, "char", "40", isRequired);

            throw new Exception("");
        }

        private string CreateField(TableInfo ti, ColumnInfo ci, string fieldType, string length, bool isRequired)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("`" + ci.ColumnName + "`");
            sb.AppendFormat(" {0}", fieldType);
            if (string.IsNullOrEmpty(length) == false) {
                sb.AppendFormat("({0})", length);
            }
            if (isRequired) {
                sb.Append(" NOT");
            }
            sb.Append(" NULL");
            if (string.IsNullOrEmpty(ci.DefaultValue) == false) {
                sb.AppendFormat(" DEFAULT {0} ", ci.DefaultValue);
            }
            if (ti.PrimaryKey == ci.ColumnName) {
                sb.Append(" PRIMARY KEY");
                if (ti.AutoIncrement) {
                    sb.Append(" AUTO_INCREMENT");
                }
            }
            if (string.IsNullOrEmpty(ci.Comment) == false) {
                sb.AppendFormat(" COMMENT '{0}'", ci.Comment.Replace("'", @"\'"));
            }
            return sb.ToString();
        }

        public override string GetTableName(string databaseName, string schemaName, string tableName)
        {
            if (string.IsNullOrEmpty(databaseName) == false) {
                return $"`{databaseName}`.`{tableName}`";
            }
            if (string.IsNullOrEmpty(schemaName) == false) {
                return $"`{schemaName}`.`{tableName}`";
            }
            return $"`{tableName}`";
        }
    }
}
