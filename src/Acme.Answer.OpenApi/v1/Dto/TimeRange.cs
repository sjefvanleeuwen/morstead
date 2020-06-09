using System;

namespace Acme.Answer.OpenApi.v1.Features.Feature1
{
    /// <summary>
    /// Time range, indicated by start and end date.
    /// </summary>
    public class TimeRange
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
        /// <summary>
        /// Gets a value indicating whether this time range has an end.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has end; otherwise, <c>false</c>.
        /// </value>
        /*
        public bool HasEnd { get; }
        /// <summary>
        /// Gets a value indicating whether this time range has a start.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has start; otherwise, <c>false</c>.
        /// </value>
        public bool HasStart { get; }
        /// <summary>
        /// Gets a value indicating whether this time range is a moment.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is moment; otherwise, <c>false</c>.
        /// </value>
        public bool IsMoment { get; }
        /// <summary>
        /// Gets a value indicating whether this timerange is anytime.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is anytime; otherwise, <c>false</c>.
        /// </value>
        public bool IsAnytime { get; }
        /// <summary>
        /// Gets a value indicating whether this time range is read only.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly { get; }
        /// <summary>
        /// The timespan / duration of the timerange.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public TimeSpan Duration { get; set; }
        */
    }
}