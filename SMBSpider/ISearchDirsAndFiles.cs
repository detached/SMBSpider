//-----------------------------------------------------------------------
// <copyright file="ISearchDirsAndFiles.cs" company="Karlsruhe Institute of Technology">
//     Copyright (c) KIT. All rights reserved.
// </copyright>
// <author>Simon Weis</author>
//-----------------------------------------------------------------------

namespace SMBSpider
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides functions to search a directory for files and directories.
    /// </summary>
    public interface ISearchDirsAndFiles
    {
        /// <summary>
        /// Search for Files.
        /// </summary>
        /// <param name="dir">The directory.</param>
        /// <returns>A list of files.</returns>
        IEnumerable<string> GetFiles(string dir);

        /// <summary>
        /// Search for Directories.
        /// </summary>
        /// <param name="dir">The directory.</param>
        /// <returns>a list of directories.</returns>
        IEnumerable<string> GetDirectories(string dir);
    }
}
