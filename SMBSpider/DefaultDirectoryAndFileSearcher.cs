//-----------------------------------------------------------------------
// <copyright file="DefaultDirectoryAndFileSearcher.cs" company="Karlsruhe Institute of Technology">
//     Copyright (c) KIT. All rights reserved.
// </copyright>
// <author>Simon Weis</author>
//-----------------------------------------------------------------------

namespace SMBSpider
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// A default implementation.
    /// </summary>
    public class DefaultDirectoryAndFileSearcher : ISearchDirsAndFiles
    {
        /// <summary>
        /// Search for Files.
        /// </summary>
        /// <param name="dir">The directory.</param>
        /// <returns>
        /// A list of files.
        /// </returns>
        public IEnumerable<string> GetFiles(string dir)
        {
            return Directory.GetFiles(dir);
        }

        /// <summary>
        /// Search for Directories.
        /// </summary>
        /// <param name="dir">The directory.</param>
        /// <returns>
        /// a list of directories.
        /// </returns>
        public IEnumerable<string> GetDirectories(string dir)
        {
            return Directory.GetDirectories(dir);
        }
    }
}
