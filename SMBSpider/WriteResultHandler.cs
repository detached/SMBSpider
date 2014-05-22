//-----------------------------------------------------------------------
// <copyright file="WriteResultHandler.cs" company="Karlsruhe Institute of Technology">
//     Copyright (c) KIT. All rights reserved.
// </copyright>
// <author>Simon Weis</author>
//-----------------------------------------------------------------------

namespace SMBSpider
{
    using System;
    using System.IO;

    /// <summary>
    /// This handler writes all results to the disk.
    /// </summary>
    public class WriteResultHandler : IHandleResult
    {
        /// <summary>
        /// The file stream.
        /// </summary>
        private StreamWriter destination;

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteResultHandler"/> class.
        /// </summary>
        /// <param name="dir">The destination.</param>
        public WriteResultHandler(string dir)
        {
            this.destination = new StreamWriter(File.OpenWrite(dir));
        }

        /// <summary>
        /// Write the result to a file.
        /// </summary>
        /// <param name="result">The result is a directory or a file on a remote system.</param>
        public void HandleResult(string result)
        {
            this.destination.WriteLine(result);
            this.destination.Flush();
        }

        /// <summary>
        /// Führt anwendungsspezifische Aufgaben durch, die mit der Freigabe, der Zurückgabe oder dem Zurücksetzen von nicht verwalteten Ressourcen zusammenhängen.
        /// </summary>
        public void Dispose()
        {
            this.destination.Close();
            this.destination.Dispose();
        }
    }
}
