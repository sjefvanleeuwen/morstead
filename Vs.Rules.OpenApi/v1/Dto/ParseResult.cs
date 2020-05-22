using System.Collections.Generic;
using Vs.Rules.OpenApi.v1.Features.discipl.Dto.Exceptions;

namespace Vs.Rules.OpenApi.v1.Dto
{
    public class ParseResult
    {
        /// <summary>
        /// Gets or sets the formatting exceptions.
        /// </summary>
        /// <value>
        /// The formatting exceptions.
        /// </value>
        public List<FormattingException> FormattingExceptions { get; set; }
    }
}
