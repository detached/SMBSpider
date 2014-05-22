//-----------------------------------------------------------------------
// <copyright file="SmbScannerFactory.cs" company="Karlsruhe Institute of Technology">
//     Copyright (c) KIT. All rights reserved.
// </copyright>
// <author>Simon Weis</author>
//-----------------------------------------------------------------------

namespace SMBSpider
{
    using System;
    using System.Linq;
    using System.Net;
    using LukeSkywalker.IPNetwork;

    /// <summary>
    /// Creates smbScanner.
    /// </summary>
    public class SmbScannerFactory
    {
        /// <summary>
        /// Creates a smbScanner from arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>A new instance of smbScanner.</returns>
        public static SmbScanner CreateFromArguments(string[] arguments)
        {
            if (arguments.Length <= 1)
            {
                throw new ArgumentException("Argument is missing!");
            }

            IPAddress[] addresses = null;

            if (arguments[0].Equals("ip"))
            {
                IPAddress address;
                if (!IPAddress.TryParse(arguments[1], out address))
                {
                    throw new ArgumentException("IP is invalid!");
                }

                addresses = new IPAddress[] { address };
            }
            else
            {
                if (arguments[0].Equals("range"))
                {
                    IPNetwork network;
                    if (!IPNetwork.TryParse(arguments[1], out network))
                    {
                        throw new ArgumentException("IP range is invalid!");
                    }

                    IPAddressCollection addressCollection = IPNetwork.ListIPAddress(network);
                    
                    // Remove own ips.
                    var hostIps = from ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList select ip.ToString().ToLower();
                    addresses = (from ip in addressCollection where !hostIps.Contains(ip.ToString().ToLower()) select ip).ToArray();
                }
                else
                {
                    throw new ArgumentException("Wrong parameter. See help!");
                }
            }

            SmbScanner scanner = new SmbScanner(new ListResultHandler(), addresses);

            for (int i = 2; i < arguments.Length; i++)
            {
                if (arguments[i].Equals("-v"))
                {
                    Config.VERBOSE = true;
                    Console.WriteLine("Verbose mode activated.");
                    continue;
                }

                if (i < arguments.Length - 1)
                {
                    if (string.IsNullOrEmpty(arguments[i + 1]))
                    {
                        continue;
                    }

                    switch (arguments[i])
                    {
                        case "-o":
                            ResultHandlerComposition multiHandler = new ResultHandlerComposition();
                            multiHandler.Components.Add(scanner.ResultHandler);
                            multiHandler.Components.Add(new CopyResultHandler(arguments[i + 1]));
                            scanner.ResultHandler = multiHandler;
                            i++;
                            break;
                        case "-w":
                            ResultHandlerComposition multiHandler2 = new ResultHandlerComposition();
                            multiHandler2.Components.Add(scanner.ResultHandler);
                            multiHandler2.Components.Add(new WriteResultHandler(arguments[i + 1]));
                            scanner.ResultHandler = multiHandler2;
                            i++;
                            break;
                        case "-if":
                            scanner.Searcher = new InclusiveRegexSearcher(arguments[i + 1]);
                            i++;
                            break;
                        case "-ef":
                            scanner.Searcher = new ExclusiveRegexSearcher(arguments[i + 1]);
                            i++;
                            break;
                        default:
                            Console.WriteLine("Don't understand: {0}", arguments[i]);
                            break;
                    }
                }
            }

            return scanner;
        }
    }
}
