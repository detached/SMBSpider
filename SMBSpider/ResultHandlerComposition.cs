//-----------------------------------------------------------------------
// <copyright file="ResultHandlerComposition.cs" company="Karlsruhe Institute of Technology">
//     Copyright (c) KIT. All rights reserved.
// </copyright>
// <author>Simon Weis</author>
//-----------------------------------------------------------------------

namespace SMBSpider
{
    using System.Collections.Generic;

    /// <summary>
    /// A composition of multiple IHandleResult objects.
    /// </summary>
    public class ResultHandlerComposition : IHandleResult
    {
        /// <summary>
        /// Determine if this instance was disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultHandlerComposition"/> class.
        /// </summary>
        public ResultHandlerComposition()
        {
            this.Components = new List<IHandleResult>();
        }

        /// <summary>
        /// Gets the components.
        /// </summary>
        public List<IHandleResult> Components { get; private set; }

        /// <summary>
        /// Calls the IHandleResult.HandleResult(string) method for all IHandleResults.
        /// </summary>
        /// <param name="result">The result is a directory or a file on a remote system.</param>
        public void HandleResult(string result)
        {
            foreach (IHandleResult handler in this.Components)
            {
                handler.HandleResult(result);
            }
        }

        /// <summary>
        /// Führt anwendungsspezifische Aufgaben durch, die mit der Freigabe, der Zurückgabe oder dem Zurücksetzen von nicht verwalteten Ressourcen zusammenhängen.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                foreach (IHandleResult handler in this.Components)
                {
                    handler.Dispose();
                }

                this.disposed = true;
            }
        }
    }
}
