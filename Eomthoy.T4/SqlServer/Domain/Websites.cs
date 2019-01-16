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
    [Table("Websites")]
    public class Websites
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
        /// Url
        /// </summary>
        [Description("Url")]
        public string Url { get; set; }
        /// <summary>
        /// Alexa
        /// </summary>
        [Description("Alexa")]
        public int? Alexa { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        [Description("Country")]
        public string Country { get; set; }
        #endregion
    }
}
