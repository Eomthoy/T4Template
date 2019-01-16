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
    [Table("Student")]
    public class Student
    {
        #region Property
        /// <summary>
        /// Id
        /// </summary>
        [Description("Id")]
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [Description("Name")]
        public string Name { get; set; }
        /// <summary>
        /// Gender
        /// </summary>
        [Description("Gender")]
        public bool? Gender { get; set; }
        /// <summary>
        /// ClassId
        /// </summary>
        [Description("ClassId")]
        public int? ClassId { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        [Description("CreateTime")]
        public DateTime? CreateTime { get; set; }
        #endregion
    }
}
