using Common.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eomthoy.T4.DataBaseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlHelper.connString = @"Server=1TVFGO8OATR3Y7G\SQLEXPRESS;DataBase=MyDB;User Id=sa;Password=123;";
            string sql = "select * from sys.tables where name like '%%' ";
            List<Tables> tables = SqlHelper.GetAll<Tables>(sql, CommandType.Text);
            string newSql = "SELECT [columns].*, [descriptions].[value] as [description], [type].[name] as [type] FROM sys.tables as [tables] INNER JOIN sys.columns as [columns] on [columns].[object_id]=[tables].[object_id] LEFT JOIN sys.extended_properties [descriptions] on [descriptions].[major_id]=[tables].[object_id] and [descriptions].[minor_id]=[columns].[column_id] LEFT JOIN sys.types as [type] on [type].[user_type_id]=[columns].[user_type_id]";
            List<Columns> columns = SqlHelper.GetAll<Columns>(newSql, CommandType.Text);
            foreach (Tables table in tables)
            {
                table.Columns = columns.Where(x => x.object_id == table.object_id).ToList();
                MapType(table.Columns);
            }

            Console.ReadKey();
        }
        /// <summary>
        /// 映射字段类型
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <param name="isNullable">是否可空</param>
        /// <param name="max_length">长度</param>
        /// <returns></returns>
        public static void MapType(IList<Columns> list)
        {
            foreach (Columns model in list)
            {
                switch (model.type)
                {
                    case "int":
                    case "tinyint":
                    case "smallint": model.type = "int"; break;
                    case "bit": model.type = "bool"; break;
                    case "smalldatetime":
                    case "datetime": model.type = "DateTime"; break;
                    case "float": model.type = "double"; break;
                    case "numeric":
                    case "smallmoney":
                    case "decimal": model.type = "decimal"; break;
                    case "real":
                    case "money": model.type = "float"; break;
                    case "xml":
                    case "ntext":
                    case "varchar":
                    case "nvarchar":
                    case "text": model.type = (model.max_length == 1) ? "char" : "string"; break;
                    case "bigint": model.type = "long"; break;
                    case "binary":
                    case "varbinary":
                    case "timestamp": model.type = "byte[]"; break;
                    case "char":
                    case "nchar": model.type = "char"; break;
                    case "sql_variant": model.type = "object"; break;
                    case "uniqueidentifier": model.type = "Guid"; break;
                    default: model.type = "string"; break;
                }
                //可空值类型判断
                model.type = (model.type != "string" && model.type != "byte[]" && model.type != "object" && model.is_nullable) ? model.type + "?" : model.type;
            }
        }
    }
}
