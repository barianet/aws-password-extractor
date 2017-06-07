﻿// ----------------------------------------------------------------------------
// <copyright file="Program.cs" company="MTCS (Matt Middleton)">
// Copyright (c) Meridian Technology Consulting Services (Matt Middleton).
// All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------

namespace Meridian.AwsPasswordExtractor.Console
{
    using System;
    using CommandLine;
    using Meridian.AwsPasswordExtractor.Logic.Definitions;
    using StructureMap;

    /// <summary>
    /// Contains the main entry point for the application,
    /// <see cref="Program.Main(string[])" />. 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">
        /// Any arguments passed to the executable upon calling.
        /// </param>
        private static void Main(string[] args)
        {
            ParserResult<Options> parseResult =
                Parser.Default.ParseArguments<Options>(args);

            // TODO: Create an option/switch "quiet", which only shows Errors
            //       and Fatals.
            //       Otherwise, it shows everything.
            parseResult.WithParsed(CommandLineArgumentsParsed);
        }

        /// <summary>
        /// The method triggered when the command line arguments are parsed
        /// with success.
        /// </summary>
        /// <param name="options">
        /// An instance of <see cref="Options" /> containing the parsed command
        /// line arguments.
        /// </param>
        private static void CommandLineArgumentsParsed(Options options)
        {
            // Create our StructureMap registry and...
            Registry registry = new Registry();
            Container container = new Container(registry);

            // Get an instance.
            IOutputFileGenerator outputFileGenerator =
                container.GetInstance<IOutputFileGenerator>();

            outputFileGenerator.CreateOutputFile(
                new Tuple<string, string>(
                    options.AccessKeyId,
                    options.SecretAccessKey),
                options.AwsRegion,
                options.PasswordEncryptionKeyFile,
                options.RoleArn,
                options.OutputFile);
        }
    }
}