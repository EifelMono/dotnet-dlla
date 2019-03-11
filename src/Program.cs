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
            var version = typeof(Program).Assembly.GetCustomAttributes<AssemblyInformationalVersionAttribute>().FirstOrDefault()?.InformationalVersion ?? "";
            Console.WriteLine($"Dll attribute");
            Console.WriteLine($"dlla version {version}");
            if (args.Length == 0)
            {
                Console.WriteLine("Add a dll file to get attributes about this file");
                return;
            }
            var fileName = Path.GetFullPath(args[0]);
            var assembly = AssemblyDefinition.ReadAssembly(fileName);
   
            Console.WriteLine(fileName);
            Console.WriteLine(assembly.FullName);
            foreach (var a in assembly.CustomAttributes)
            {
                var name = a.AttributeType.FullName.Split('.').Last().Replace("Attribute", "");
                var value = "";
                if (a.HasConstructorArguments)
                    value = $"{a.ConstructorArguments[0].Value}";
                if (!string.IsNullOrEmpty(value))
                    Console.WriteLine($"{name}{Environment.NewLine}  {value}");
            }
        }
    }
}
