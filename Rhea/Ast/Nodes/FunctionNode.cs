using System.Collections.Generic;
using System.Linq;

namespace Rhea.Ast.Nodes
{
    public class FunctionNode : Node
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public List<FunctionParameter> Parameters { get; set; } = new List<FunctionParameter>();
        public Block Block { get; set; }

        public string Declaration
        {
            get
            {
                var argList = string.Join(", ", Parameters.Select(a => a.ToString()));
                return $"{Type} {Name}({argList});";
            }
        }

        public override string ToString()
        {
            var argList = string.Join(", ", Parameters.Select(a => a.ToString()));
            return $"{Type} {Name}({argList}) {Block}";
        }
    }
}