using System;

namespace Vs.Morstead.Grains.Interfaces.Primitives.Directory
{
    public class DirectoryContentsItem
    {
        /// <summary>
        /// Gets or sets the created date time of the directory item.
        /// </summary>
        /// <value>
        /// The created date time.
        /// </value>
        public DateTime? Created { get; set; }
        /// <summary>
        /// Gets or sets the modified date time of the directory item.
        /// </summary>
        /// <value>
        /// The created date time.
        /// </value>
        public DateTime? Modified { get; set; }
        /// <summary>Gets or sets the meta data.</summary>
        /// <value>The meta data.</value>
        public string MetaData { get; set; }
        /// <summary>
        /// Gets or sets the grain identifier.
        /// </summary>
        /// <value>
        /// The grain identifier.
        /// </value>
        public string GrainId { get; set; }
        /// <summary>
        /// Gets or sets the grain interface type.
        /// </summary>
        /// <value>
        /// The interface.
        /// </value>
        public Type Interface { get; set; }
    }
}
