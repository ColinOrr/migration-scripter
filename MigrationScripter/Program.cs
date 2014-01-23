using MigrationScripter.Properties;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations.Design;
using System.IO;
using System.Linq;

namespace MigrationScripter
{
    class Program
    {
        static dynamic ParseArguments(string[] args)
        {
            var parameters = new
            {
                assembly   = args.FindParameter(),
                config     = args.FindParameter("config"),
                source     = args.FindParameter("source"),
                target     = args.FindParameter("target"),
                connection = args.FindParameter("connection") ?? args.FindParameter(),
                output     = args.FindParameter("out")
            };

            //  Required Parameters
            if (parameters.assembly == null || parameters.config == null) 
            {
                Console.WriteLine(Resources.Usage);
                return null;
            }

            return parameters;
        }

        static void Main(string[] args)
        {
            //  Parse arguments
            var parameters = ParseArguments(args);
            if (parameters == null) return;

            //  Generate the script
            var connection = new DbConnectionInfo(parameters.connection);

            var tool = new ToolingFacade(
                assemblyName:          parameters.assembly,
                configurationTypeName: null,
                workingDirectory:      null,
                configurationFilePath: parameters.config,
                dataDirectory:         null,
                connectionStringInfo:  connection
            );

            var script = tool.ScriptUpdate(parameters.source, parameters.target, force: true);

            //  Output the script
            if (parameters.output == null) Console.WriteLine(script);
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(parameters.output));
                File.WriteAllText(parameters.output, script);
            }
        }
    }
        
    static class Extensions
    {
        /// <summary>
        /// Finds a parameter within a string array.
        /// </summary>
        public static string FindParameter(this string[] args, string name = null)
        {
            //  Find unnamed parameters
            if (name == null)
            {
                return args.FirstOrDefault(a => !a.StartsWith("-"));
            }

            //  Find named parameters
            var selector = "-" + name + "=";
            foreach (var arg in args)
            {
                if (arg.StartsWith(selector)) return arg.Replace(selector, "");
            }

            //  Give up
            return null;
        }
    }
}
