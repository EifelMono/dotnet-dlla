using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;

namespace dotnet_dlla
{
    public class DllAttributeItem
    {
        public DllAttributeItem(CustomAttribute attribute)
            => Attribute = attribute;

        private CustomAttribute _attribute;
        public CustomAttribute Attribute
        {
            get => _attribute;
            set
            {
                _attribute = value;
                Name = _attribute.AttributeType.FullName;
                ShortName = Name.Split('.').Last()
                    .Replace("Attribute", "")
                    .Replace("Assembly", "");
                if (_attribute.HasConstructorArguments)
                    Value = _attribute.ConstructorArguments[0].Value;
            }
        }

        public bool HasValue => Value != null;
        public object Value { get; set; } = null;

        public string Name { get; set; }
        public string ShortName { get; set; }

        public static List<string> NotUseFull = new List<string>
        {
            "CompilationRelaxations",
            "Debuggable"
        };
        public bool UseFull => !NotUseFull.Contains(ShortName);
    }
}
