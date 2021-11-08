using System;
using System.Collections.Generic;
using System.Text;

namespace Commerce.Core
{
    public class Store : BaseEntity
    {
        /// <summary>
        /// Gets or sets the store name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the store URL
        /// </summary>
        public string Url { get; set; }
    }
}
