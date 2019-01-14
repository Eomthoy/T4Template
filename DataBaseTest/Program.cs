using Core.Common.Helper;
using Core.Common.Basics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;

namespace DataBaseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = @"Server=1TVFGO8OATR3Y7G\SQLEXPRESS;DataBase=MyDB;User Id=sa;Password=123;";
            string sql = "select * from sys.tables";
            SqlHelper.connString = connString;
            IList<Tables> tables = SqlHelper.GetAll<Tables>(sql, CommandType.Text);
            foreach (Tables item in tables)
            {
                Console.WriteLine(item.Name + "  " + item.object_id);
            }

            Console.ReadKey();
        }

        public static DataTable GetResult(string connString, string sql)
        {
            SqlHelper.connString = connString;
            DataTable dataTable = SqlHelper.ExecuteDataTable(sql, CommandType.Text);
            return dataTable;
        }

        public List<Columns> GetColumns(int object_id)
        {
            string sql = "SELECT [columns].[name], [descriptions].[value] as [Description] FROM sys.tables as [tables] INNER JOIN sys.columns as [columns] on[columns].[object_id] =[tables].[object_id] LEFT JOIN sys.extended_properties[descriptions] on[descriptions].[major_id] =[tables].[object_id] and[descriptions].[minor_id] =[columns].[column_id] where[tables].[object_id] = 277576027";
            List<Columns> columns = SqlHelper.GetAll<Columns>(sql, CommandType.Text);
            return columns;
        }
    }
}
