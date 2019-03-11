#define Test
using EifelMono.Fluent;
using EifelMono.Fluent.IO;
using Mono.Cecil;
using System;
using System.IO;
using System.Linq;
using System.Reflection;


namespace dotnet_dlla
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            args = new string[] { typeof(Program).Assembly.Location};
#endif
            Console.WriteLine($"dlla(ttributs) version {fluent.App.Version}");
            if (args.Length == 0)
            {
                Console.WriteLine("Add a dll file to get attributes about this file");
                return;
            }

            var dllaFile = new FilePath(args[0]);
            if (!dllaFile.Exists)
            {
                Console.WriteLine($"{dllaFile.FileName} does not exist");
                Console.WriteLine($"{dllaFile} does not exist");
                return;
            }
            var assembly = AssemblyDefinition.ReadAssembly(dllaFile);
            Console.WriteLine();
            Console.WriteLine(dllaFile);
            Console.WriteLine(assembly.FullName);
            Console.WriteLine();

            var items = assembly.CustomAttributes.Select(a => new DllAttributeItem(a));
            foreach (var a in items)
            {
                if (a.UseFull && a.HasValue)
                    Console.WriteLine($"{a.ShortName} = {a.Value}");
            }
#if DEBUG
            Console.WriteLine("...");
            Console.ReadLine();
#endif
        }
    }
}
