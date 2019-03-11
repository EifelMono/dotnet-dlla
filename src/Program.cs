#define Test
using EifelMono.Fluent;
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

            var fileName = Path.GetFullPath(args[0]);
            var assembly = AssemblyDefinition.ReadAssembly(fileName);
            Console.WriteLine();
            Console.WriteLine(fileName);
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
