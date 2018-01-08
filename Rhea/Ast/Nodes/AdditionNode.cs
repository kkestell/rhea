using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class AdditionNode : InfixExpressionNode
    {
        public override string ToString()
        {
            return $"({Left} + {Right})";
        }
    }
}