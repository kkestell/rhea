using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class DivisionNode : InfixExpressionNode
    {
        public override string ToString()
        {
            return $"({Left} / {Right})";
        }
    }
}