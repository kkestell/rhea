using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class SubtractionNode : InfixExpressionNode
    {
        public override string ToString()
        {
            return $"({Left} - {Right})";
        }
    }
}