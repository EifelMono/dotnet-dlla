#define Test
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
#if Test
            args = new string[] { typeof(Program).Assembly.Location};
#endif
            var version = typeof(Program).Assembly.GetCustomAttributes<AssemblyInformationalVersionAttribute>().FirstOrDefault()?.InformationalVersion ?? "";
            Console.WriteLine($"dlla(ttributs) version {version}");
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
#if Test
            Console.ReadLine();
#endif
        }
    }
}
