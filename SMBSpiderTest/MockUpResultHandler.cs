//-----------------------------------------------------------------------
// <copyright file="MockUpResultHandler.cs" company="Karlsruhe Institute of Technology">
//     Copyright (c) KIT. All rights reserved.
// </copyright>
// <author>Simon Weis</author>
//-----------------------------------------------------------------------

namespace SMBSpiderTest
{
    using System.Collections.Generic;
    using SMBSpider;

    /// <summary>
    /// This handler writes the results to a list.
    /// </summary>
    public class MockUpResultHandler : IHandleResult
    {
        /// <summary>
        /// Determine if this instance was disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// containes the results.
        /// </summary>
        private List<string> resultList = new List<string>();

        /// <summary>
        /// Writes the result to a lists.
        /// </summary>
        /// <param name="result">The result is a directory or a file on a remote system.</param>
        public void HandleResult(string result)
        {
            this.resultList.Add(result);
        }

        /// <summary>
        /// Gets the results.
        /// </summary>
        /// <returns>All results.</returns>
        internal string[] GetResults()
        {
            return this.resultList.ToArray();
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
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.resultList = null;
                }

                this.disposed = true;
            }
        }
    }
}
