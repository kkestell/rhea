using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class NumberNode : AtomNode
    {
        public double Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}