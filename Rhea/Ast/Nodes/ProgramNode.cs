using System.Collections.Generic;
using System.Linq;

namespace Rhea.Ast.Nodes
{
    class ProgramNode : Node
    {
        public List<FunctionNode> Functions { get; set; } = new List<FunctionNode>();

        public override string ToString()
        {
            return string.Join("\n\n", Functions.Select(f => f.ToString()));
        }
    }
}