using System.Collections.Generic;
using System.Linq;

namespace Rhea.Ast.Nodes
{
    class Block : Node
    {
        public IEnumerable<Statement> Statements { get; set; }

        public override string ToString()
        {
            var statements = string.Join("\n", Statements.Select(e => e.ToString()));
            return $"{{\n{statements}\n}}";
        }
    }
}