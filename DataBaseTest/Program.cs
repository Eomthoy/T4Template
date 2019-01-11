using Core.Common.Helper;
using Core.Common.Basics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataBaseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = @"Server=1TVFGO8OATR3Y7G\SQLEXPRESS;DataBase=MyDB;User Id=sa;Password=123;";
            string sql = "select * from sys.tables";
            var result = GetResult(connString, sql);
            if (result.IsSuccess.Value)
            {
                Console.WriteLine("表：" + string.Join("、", result.Data.Select(x => x.Name)));
            }
            Console.ReadKey();
        }

        public static AjaxResult<Tables> GetResult(string connString, string sql)
        {
            SqlHelper.connString = connString;
            IList<Tables> tables = SqlHelper.GetAll<Tables>(sql, CommandType.Text);
            return new AjaxResult<Tables>(true, "", tables);
        }
    }
}
