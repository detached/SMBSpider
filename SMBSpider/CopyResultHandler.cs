//-----------------------------------------------------------------------
// <copyright file="CopyResultHandler.cs" company="Karlsruhe Institute of Technology">
//     Copyright (c) KIT. All rights reserved.
// </copyright>
// <author>Simon Weis</author>
//-----------------------------------------------------------------------

namespace SMBSpider
{
    using System.IO;

    /// <summary>
    /// Copies all results to a defined destination.
    /// </summary>
    public class CopyResultHandler : IHandleResult
    {
        /// <summary>
        /// Determine if this instance was disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Maximal storage size in Bytes.
        /// </summary>
        private long storageSize = 0;

        /// <summary>
        /// Copied bytes.
        /// </summary>
        private long copiedBytes = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="CopyResultHandler"/> class.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        public CopyResultHandler(string destinationPath)
        {
            this.DestinationPath = destinationPath;
            if (!Directory.Exists(this.DestinationPath))
            {
                Directory.CreateDirectory(this.DestinationPath);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CopyResultHandler"/> class.
        /// </summary>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="storageSize">The maximum size of the storage.</param>
        public CopyResultHandler(string destinationPath, long storageSize)
            : this(destinationPath)
        {
            this.storageSize = storageSize;
        }

        /// <summary>
        /// Gets the destination path.
        /// </summary>
        public string DestinationPath { get; private set; }

        /// <summary>
        /// Handles the result.
        /// </summary>
        /// <param name="result">The result.</param>
        public void HandleResult(string result)
        {
            if (result.Contains(@"..\"))
            {
                return;
            }

            FileInfo sourceFile = new FileInfo(result);

            string localPath;

            if (result.StartsWith(@"\\"))
            {
                localPath = Path.Combine(this.DestinationPath, result.Remove(0, 2));
            }
            else
            {
                localPath = Path.Combine(this.DestinationPath, result);
            }

            if ((sourceFile.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }
            }
            else
            {
                string basePath = Path.GetDirectoryName(localPath);
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }

                if (!File.Exists(localPath))
                {
                    if (sourceFile.Length + copiedBytes <= storageSize)
                    {
                        sourceFile.CopyTo(localPath, false);
                        copiedBytes += sourceFile.Length;
                    }
                }
            }
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
                    this.copiedBytes = 0;
                    this.storageSize = 0;
                    this.DestinationPath = null;
                }

                this.disposed = true;
            }
        }
    }
}
