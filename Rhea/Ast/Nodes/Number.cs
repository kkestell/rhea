using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class Number : Atom
    {
        public double Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}