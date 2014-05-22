//-----------------------------------------------------------------------
// <copyright file="IHandleResult.cs" company="Karlsruhe Institute of Technology">
//     Copyright (c) KIT. All rights reserved.
// </copyright>
// <author>Simon Weis</author>
//-----------------------------------------------------------------------

using System;
namespace SMBSpider
{
    /// <summary>
    /// Interface for SmbScanner to access instances that can handle results.
    /// </summary>
    public interface IHandleResult: IDisposable
    {
        /// <summary>
        /// Handles the result.
        /// </summary>
        /// <param name="result">The result is a directory or a file on a remote system.</param>
        void HandleResult(string result);
    }
}
