using System.Collections.Generic;

namespace Rhea.Ast.Nodes
{
    class FunctionCall : Expression
    {
        public string Name { get; set; }

        public IEnumerable<Expression> Arguments { get; set; }

        public override string ToString()
        {
            return $"{Name}({string.Join(", ", Arguments)})";
        }
    }
}
