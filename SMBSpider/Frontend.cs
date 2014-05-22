//-----------------------------------------------------------------------
// <copyright file="Frontend.cs" company="Karlsruhe Institute of Technology">
//     Copyright (c) KIT. All rights reserved.
// </copyright>
// <author>Simon Weis</author>
//-----------------------------------------------------------------------

namespace SMBSpider
{
    using System;

    /// <summary>
    /// The command line frontend.
    /// </summary>
    public class Frontend
    {
        /// <summary>
        /// The main entry point for the SMBSpider application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("NAME");
                Console.WriteLine("\t SMBSpider - Windows share network scanner\n");
                Console.WriteLine("SYNOPSIS");
                Console.WriteLine("\tSMBSpider MODE [OPTION]\n");
                Console.WriteLine("DESCRIPTION");
                Console.WriteLine("\t The SMBSpider search in a netwok for public windows shares.");
                Console.WriteLine("\t The program can be used to list or copy the discovered files.");
                Console.WriteLine("MODE");
                Console.WriteLine("\tip IP");
                Console.WriteLine("\t\tScan a single IP Address.");
                Console.WriteLine("\trange RANGE ");
                Console.WriteLine("\t\tScan the IP RANGE. Define the RANGE in CIDR notation.\n");
                Console.WriteLine("OPTION");
                Console.WriteLine("\t-o DIRECTORY");
                Console.WriteLine("\t\tWrite the results to the given DIRECTORY.");
                Console.WriteLine("\t-w FILE");
                Console.WriteLine("\t\tWrite a list of all results to the FILE.");
                Console.WriteLine("\t-if REGEX");
                Console.WriteLine("\t\tDefines a inclusive REGEX filter. Only results matching this pattern will be included.");
                Console.WriteLine("\t-ef REGEX");
                Console.WriteLine("\t\tDefines a exclusive REGEX filter. Only results don't matching this pattern will be included.");
                Console.WriteLine("\t-v");
                Console.WriteLine("\t\tEnables verbose mode.");
                return;
            }

            try
            {
                SmbScanner scanner = SmbScannerFactory.CreateFromArguments(args);
                scanner.Scan();
                scanner.Done();
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Concat("Error: ", e.ToString()));
            }
        }
    }
}
