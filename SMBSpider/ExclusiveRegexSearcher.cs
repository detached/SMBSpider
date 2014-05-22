//-----------------------------------------------------------------------
// <copyright file="ExclusiveRegexSearcher.cs" company="Karlsruhe Institute of Technology">
//     Copyright (c) KIT. All rights reserved.
// </copyright>
// <author>Simon Weis</author>
//-----------------------------------------------------------------------

namespace SMBSpider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Excludes all findings if they don't match the regex.
    /// </summary>
    public class ExclusiveRegexSearcher : RegexSearcher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExclusiveRegexSearcher"/> class.
        /// </summary>
        /// <param name="regex">The regex.</param>
        public ExclusiveRegexSearcher(string regex) : base(regex) 
        {
        }

        /// <summary>
        /// Applies the filter on the data set.
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        /// <returns>
        /// A filtered dataset.
        /// </returns>
        protected override IEnumerable<string> ApplyFilter(string[] dataSet)
        {
            return from data in dataSet.AsParallel()
                   where !validator.IsMatch(data)
                   select data;
        }
    }
}
