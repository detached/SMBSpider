//-----------------------------------------------------------------------
// <copyright file="RegexSearcher.cs" company="Karlsruhe Institute of Technology">
//     Copyright (c) KIT. All rights reserved.
// </copyright>
// <author>Simon Weis</author>
//-----------------------------------------------------------------------

namespace SMBSpider
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The base class for regex directory and file searchers.
    /// </summary>
    public abstract class RegexSearcher : ISearchDirsAndFiles
    {
        /// <summary>
        /// The Regex filter
        /// </summary>
        protected Regex validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegexSearcher"/> class.
        /// </summary>
        /// <param name="regex">The regex.</param>
        public RegexSearcher(string regex) 
        {
            this.validator = new Regex(regex);
        }

        /// <summary>
        /// Gets all files from a directory. Filtered by the search pattern.
        /// </summary>
        /// <param name="dir">The path to search in.</param>
        /// <returns>
        /// A filtered list.
        /// </returns>
        public IEnumerable<string> GetFiles(string dir)
        {
            return this.ApplyFilter(Directory.GetFiles(dir));
        }

        /// <summary>
        /// Gets all directories from a directoy. Filtered by the search pattern.
        /// </summary>
        /// <param name="dir">The path to search in.</param>
        /// <returns>
        /// A filtered list.
        /// </returns>
        public IEnumerable<string> GetDirectories(string dir)
        {
            return this.ApplyFilter(Directory.GetDirectories(dir));
        }

        /// <summary>
        /// Applies the filter on the data set.
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        /// <returns>
        /// A filtered dataset.
        /// </returns>
        protected abstract IEnumerable<string> ApplyFilter(string[] dataSet);
    }
}
