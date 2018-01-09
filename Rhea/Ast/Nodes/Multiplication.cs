using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class Multiplication : InfixExpression
    {
        public override string ToString()
        {
            return $"({Left} * {Right})";
        }
    }
}