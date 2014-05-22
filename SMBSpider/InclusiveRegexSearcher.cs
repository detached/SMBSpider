//-----------------------------------------------------------------------
// <copyright file="InclusiveRegexSearcher.cs" company="Karlsruhe Institute of Technology">
//     Copyright (c) KIT. All rights reserved.
// </copyright>
// <author>Simon Weis</author>
//-----------------------------------------------------------------------

namespace SMBSpider
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Only includes the findings if they match the pattern.
    /// </summary>
    public class InclusiveRegexSearcher : RegexSearcher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InclusiveRegexSearcher"/> class.
        /// </summary>
        /// <param name="regex">The regex.</param>
        public InclusiveRegexSearcher(string regex) : base(regex)
        {
        }

        /// <summary>
        /// Applies the inclusive filter.
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        /// <returns> A filtered dataset.</returns>
        protected override IEnumerable<string> ApplyFilter(string[] dataSet) 
        {
            return from data in dataSet.AsParallel()
                    where validator.IsMatch(data)
                    select data;
        }
    }
}
