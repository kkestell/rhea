using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class Addition : InfixExpression
    {
        public override string ToString()
        {
            return $"({Left} + {Right})";
        }
    }
}