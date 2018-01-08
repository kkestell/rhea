using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class MultiplicationNode : InfixExpressionNode
    {
        public override string ToString()
        {
            return $"({Left} * {Right})";
        }
    }
}