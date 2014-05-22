//-----------------------------------------------------------------------
// <copyright file="SmbScanner.cs" company="Karlsruhe Institute of Technology">
//     Copyright (c) KIT. All rights reserved.
// </copyright>
// <author>Simon Weis</author>
//-----------------------------------------------------------------------

namespace SMBSpider
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The Windows Share Scanner. Uses NetApi32.dll for locating shares.
    /// </summary>
    public class SmbScanner
    {
        /// <summary>
        /// Representation of netapi32 success error code.
        /// </summary>
        private const uint SUCCESS = 0;

        /// <summary>
        /// The netshareenum function is allowed to accocate the required amount of memory.
        /// </summary>
        private const uint MAXPREFERREDLENGTH = 0xFFFFFFFF;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmbScanner"/> class.
        /// </summary>
        /// <param name="resultHandler">The result handler.</param>
        /// <param name="addresses">The addresses to scan.</param>
        public SmbScanner(IHandleResult resultHandler, IPAddress[] addresses)
        {
            this.ResultHandler = resultHandler;
            this.Addresses = addresses;
            this.Searcher = new DefaultDirectoryAndFileSearcher();
        }

        /// <summary>
        /// Gets or sets the result handler.
        /// </summary>
        public IHandleResult ResultHandler { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        public IPAddress[] Addresses { get; set; }

        /// <summary>
        /// Gets or sets the directory and file searcher.
        /// </summary>
        /// <value>
        /// The directory and file searcher.
        /// </value>
        public ISearchDirsAndFiles Searcher { get; set; }

        /// <summary>
        /// Scans this instance.
        /// </summary>
        public void Scan()
        {
            int structSize = Marshal.SizeOf(typeof(SHARE_INFO_1));

            if (Config.VERBOSE)
            {
                Console.WriteLine("Going to scan {0} ip/s", this.Addresses.Length);
            }

            Parallel.ForEach(
                this.Addresses,
                ip =>
            {
                int entriesRead = 0;
                int totalEnties = 0;
                int resumeHandle = 0;
                IntPtr bufPtr = IntPtr.Zero;
                var server = new StringBuilder();

                server.Clear();
                server.Append(ip.ToString());

                int ret = NativeMethods.NetShareEnum(server, 1, ref bufPtr, MAXPREFERREDLENGTH, ref entriesRead, ref totalEnties, ref resumeHandle);
                if (ret == SUCCESS)
                {
                    if (Config.VERBOSE)
                    {
                        Console.WriteLine("{0} - Found: {1}, Thread {2}", server.ToString(), entriesRead, Thread.CurrentThread.ManagedThreadId);
                    }

                    IntPtr currentPtr = bufPtr;
                    for (int i = 0; i < entriesRead; i++)
                    {
                        SHARE_INFO_1 share = (SHARE_INFO_1)Marshal.PtrToStructure(currentPtr, typeof(SHARE_INFO_1));
                        CallHandlerforAllFilesAndDirectories(string.Format("\\\\{0}\\{1}\\", server.ToString(), share.Netname));
                        currentPtr = new IntPtr(currentPtr.ToInt32() + structSize);
                    }

                    NativeMethods.NetApiBufferFree(ref bufPtr);
                }
                else 
                {
                    if (Config.VERBOSE)
                    {
                        switch (ret)
                        {
                            case 5: Console.WriteLine("{0} - Access is denied.", server.ToString()); break;
                            case 53: Console.WriteLine("{0} - The network path was not found.", server.ToString()); break;
                            default: Console.WriteLine("{0} - System Error: {1}", server.ToString(), ret); break;
                        }
                        
                    }
                }
            });
        }

        /// <summary>
        /// Dones this instance.
        /// </summary>
        public void Done()
        {
            this.ResultHandler.Dispose();
        }

        /// <summary>
        /// Gets all files and directories recursive.
        /// Catches UnauthorizedAccessExeption.
        /// </summary>
        /// <param name="root">The root directory.</param>
        private void CallHandlerforAllFilesAndDirectories(string root)
        {
            try
            {
                foreach (string file in this.Searcher.GetFiles(root))
                {
                    this.ResultHandler.HandleResult(file);
                }

                this.ResultHandler.HandleResult(root);
                foreach (string dir in this.Searcher.GetDirectories(root))
                {
                    this.CallHandlerforAllFilesAndDirectories(dir);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("{0} - Unauthorized Access.", root);
            }
            catch (IOException e) 
            {
                Console.Write("{0} - {1}", root, e.Message.ToString());
            }
        }

        /// <summary>
        /// Contains information about the shared resource,
        /// including the name and type of the resource, and a comment associated with the resource.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct SHARE_INFO_1
        {
            /// <summary>
            /// A Unicode string specifying the share name of a resource.
            /// </summary>
            public string Netname;

            /// <summary>
            /// A combination of values that specify the type of the shared resource.
            /// </summary>
            public uint Type;

            /// <summary>
            /// A Unicode string specifying an optional comment about the shared resource.
            /// </summary>
            public string Remark;
            
            /// <summary>
            /// Initializes a new instance of the <see cref="SHARE_INFO_1"/> struct.
            /// </summary>
            /// <param name="sharename">The sharename.</param>
            /// <param name="sharetype">The sharetype.</param>
            /// <param name="remark">The remark.</param>
            public SHARE_INFO_1(string sharename, uint sharetype, string remark)
            {
                this.Netname = sharename;
                this.Remark = remark;
                this.Type = sharetype;
            }
        }

        /// <summary>
        /// Class for the native methods.
        /// </summary>
        internal static class NativeMethods
        {
            [DllImport("Netapi32.dll", CharSet = CharSet.Unicode)]
            internal static extern int NetShareEnum(
                StringBuilder serverName,
                int level,
                ref IntPtr bufPtr,
                uint prefmaxlen,
                ref int entriesread,
                ref int totalentries,
                ref int resume_handle);

            [DllImport("Netapi32.dll", CharSet = CharSet.Unicode)]
            internal static extern long NetApiBufferFree(
                ref IntPtr buffer);
        }
    }
}
