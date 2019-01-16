using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;


namespace Common.Helper
{
    /// <summary>
    /// 通用数据访问类
    /// </summary>
    public class SqlHelper
    {

        #region 数据源：SqlServer数据库

        //数据库连接字符串
        public static string connString { get; set; }

        /// <summary>
        /// 返回单个值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = commandType;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        conn.Open();
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                throw new Exception(ex.Message);
            }
        }

        #region 增删改
        /// <summary>
        /// 执行增删改，返回受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int Execute(string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = commandType;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SqlDataReader
        /// <summary>
        /// 执行查询，返回SqlDataReader对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = commandType;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 执行查询，获取List<T>集合
        /// </summary>
        /// <typeparam name="T">entity</typeparam>
        /// <param name="reader">data reader</param>
        /// <returns>entity</returns>
        public static List<T> GetAllFromReader<T>(string sql, CommandType commandType, params SqlParameter[] parameters) where T : new()
        {
            SqlDataReader reader = ExecuteReader(sql, commandType, parameters);
            List<T> list = new List<T>();
            using (reader)
            {
                while (reader.Read())
                {
                    T model = ReaderToEntity<T>(reader);
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 执行查询，获取T单个对象
        /// </summary>
        /// <typeparam name="T">entity</typeparam>
        /// <param name="reader">data reader</param>
        /// <returns>entity</returns>
        public static T GetFromReader<T>(string sql, CommandType commandType, params SqlParameter[] parameters) where T : new()
        {
            SqlDataReader reader = ExecuteReader(sql, commandType, parameters);
            T model = new T();
            using (reader)
            {
                while (reader.Read())
                {
                    model = ReaderToEntity<T>(reader);
                }
            }
            return model;
        }
        /// <summary>
        /// Reader To Entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private static T ReaderToEntity<T>(SqlDataReader data) where T : new()
        {
            T val = new T();
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                //字段属性（字段名、字段类型）
                string propertyName = propertyInfo.Name;
                string propertyType = propertyInfo.PropertyType.ToString().Trim().ToLower();
                //赋值
                var obj = new object();
                obj = data[propertyName];
                if (obj != DBNull.Value && obj != null)
                {
                    propertyInfo.SetValue(val, obj, null);
                }
            }
            return val;
        }
        #endregion

        #region DataTable

        /// <summary>
        /// 执行查询，返回DataTable对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connString))
                {
                    DataTable dt = new DataTable();
                    adapter.SelectCommand.CommandType = commandType;
                    if (parameters != null)
                    {
                        adapter.SelectCommand.Parameters.AddRange(parameters);
                    }
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 执行查询，获取数据集List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static List<T> GetAll<T>(string sql, CommandType commandType, params SqlParameter[] parameters) where T : new()
        {
            try
            {
                DataTable dt = ExecuteDataTable(sql, commandType, parameters);
                List<T> list = new List<T>();
                foreach (DataRow row in dt.Rows)
                {
                    T model = DataTableToEntity<T>(row, dt.Columns);
                    list.Add(model);
                }

                return list;
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 执行查询，获取单个对象T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T Get<T>(string sql, CommandType commandType, params SqlParameter[] parameters) where T : new()
        {
            try
            {
                DataTable dt = ExecuteDataTable(sql, commandType, parameters);
                T model = new T();
                foreach (DataRow row in dt.Rows)
                {
                    model = DataTableToEntity<T>(row, dt.Columns);
                }

                return model;
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// DataTable To Entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private static T DataTableToEntity<T>(DataRow data, DataColumnCollection columns) where T : new()
        {
            T model = new T();
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                //字段属性
                string propName = propertyInfo.Name;
                string propType = propertyInfo.PropertyType.Name.Trim().ToLower();
                //赋值
                if (columns.Contains(propName))
                {
                    object val = data[propName];
                    if (val != DBNull.Value && val != null)
                    {
                        propertyInfo.SetValue(model, val, null);
                    }
                }
            }

            return model;
        }
        #endregion

        #endregion


    }
}

