//-----------------------------------------------------------------------
// <copyright file="ListResultHandler.cs" company="Karlsruhe Institute of Technology">
//     Copyright (c) KIT. All rights reserved.
// </copyright>
// <author>Simon Weis</author>
//-----------------------------------------------------------------------

namespace SMBSpider
{
    using System;

    /// <summary>
    /// Writes the result to the console.
    /// </summary>
    public class ListResultHandler : IHandleResult
    {
        /// <summary>
        /// Writes the result to the console.
        /// </summary>
        /// <param name="result">The result is a directory or a file on a remote system.</param>
        public void HandleResult(string result)
        {
            Console.WriteLine(result);
        }

        /// <summary>
        /// Führt anwendungsspezifische Aufgaben durch, die mit der Freigabe, der Zurückgabe oder dem Zurücksetzen von nicht verwalteten Ressourcen zusammenhängen.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
