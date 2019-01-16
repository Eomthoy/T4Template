using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain
{
    /// <summary>
    /// Create By T4Template
    /// </summary>
    [Table("Apps")]
    public class AppsDto
    {
        #region Property
        /// <summary>
        /// Id
        /// </summary>
        [Description("Id")]
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Description("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        [Description("链接")]
        public string Url { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        [Description("国家")]
        public string Country { get; set; }
        #endregion
    }
}
