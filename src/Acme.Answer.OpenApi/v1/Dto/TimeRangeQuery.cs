using System;

namespace Acme.Answer.OpenApi.v1.Features.Feature1
{
    /// <summary>
    /// Time range, indicated by start and end date.
    /// </summary>
    public class TimeRangeQuery
    {
        /// <summary>
        /// Start date
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime Start { get; set; }
        /// <summary>
        /// End date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime End { get; set; }
    }
}