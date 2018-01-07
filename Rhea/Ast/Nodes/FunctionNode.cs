using System.Collections.Generic;
using System.Linq;

namespace Rhea.Ast.Nodes
{
    class FunctionNode : Node
    {
        public string Name { get; set; }
        public TypeNode Type { get; set; }
        public List<FunctionArgumentNode> Arguments { get; set; } = new List<FunctionArgumentNode>();
        public BlockNode Block { get; set; }

        public override string ToString()
        {
            var argList = string.Join(", ", Arguments.Select(a => a.ToString()));
            return $"{Type} {Name}({argList}) {Block}";
        }
    }
}