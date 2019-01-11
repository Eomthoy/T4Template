using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Core.Common.Helper;

namespace DataBaseTest
{
    public class DataBaseTable
    {
        public Tables Tables { get; set; }
        public Columns Columns { get; set; }
        public Description Description { get; set; }
    }

    public class Tables
    {
        /// <summary>
        /// 表Id
        /// </summary>
        public int object_id { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 字段
        /// </summary>
        public List<Columns> Columns { get; set; }
    }
    public class Columns
    {
        /// <summary>
        /// 表Id
        /// </summary>
        public int object_id { get; set; }
        /// <summary>
        /// 字段Id
        /// </summary>
        public int column_id { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 字段描述
        /// </summary>
        public Description Description { get; set; }
    }
    public class Description
    {
        /// <summary>
        /// 表Id
        /// </summary>
        public int major_id { get; set; }
        /// <summary>
        /// 字段Id
        /// </summary>
        public int minor_id { get; set; }
        /// <summary>
        /// 字段描述
        /// </summary>
        public string name { get; set; }
    }
}
