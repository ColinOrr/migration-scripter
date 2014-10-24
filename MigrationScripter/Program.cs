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
                contextAssembly       = args.FindParameter(),
                migrationsAssembly    = args.FindParameter("migrationsAssembly") ?? args.FindParameter(),
                configurationTypeName = args.FindParameter("configurationTypeName"),
                config                = args.FindParameter("config"),
                source                = args.FindParameter("source"),
                target                = args.FindParameter("target"),
                connection            = args.FindParameter("connection") ?? args.FindParameter(),
                output                = args.FindParameter("out")
            };

            //  Required Parameters
            if (parameters.contextAssembly == null || parameters.config == null) 
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
                migrationsAssemblyName: parameters.migrationsAssembly,
                contextAssemblyName: parameters.contextAssembly,
                configurationTypeName: parameters.configurationTypeName,
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
