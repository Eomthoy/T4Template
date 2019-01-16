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
    [Table("Class")]
    public class Class
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
        #endregion
    }
}
