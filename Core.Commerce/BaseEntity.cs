using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Commerce.Core
{
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity id
        /// </summary>
        [Key]
        public Guid Guid { get; set; }
    }
}
