using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class Subtraction : InfixExpression
    {
        public override string ToString()
        {
            return $"({Left} - {Right})";
        }
    }
}