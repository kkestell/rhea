using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Ast.Nodes
{
    class TypeNode : Node
    {
        public string Value { get; set; }
        public bool Pointer { get; set; }

        public TypeNode(string type)
        {
            if (type.StartsWith("^"))
            {
                Value = type.TrimStart('^');
                Pointer = true;
            }
            else
            {
                Value = type;
            }
        }

        public override string ToString()
        {
            if (Value == "string")
                return "char*";

            if (Pointer)
                return $"{Value}*";

            return Value;
        }
    }
}
