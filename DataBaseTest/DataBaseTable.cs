using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
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
    [Table("Tables")]
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
        [Description("")]
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
        /// 字段类型Id
        /// </summary>
        public int user_type_id { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public int max_length { get; set; }
        /// <summary>
        /// 是否可空
        /// </summary>
        public bool is_nullable { get; set; }
        /// <summary>
        /// 字段描述
        /// </summary>
        public string Description { get; set; }

        public char aaa { get; set; }
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
