using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class Division : InfixExpression
    {
        public override string ToString()
        {
            return $"({Left} / {Right})";
        }
    }
}