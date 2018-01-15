﻿namespace Rhea.Ast.Nodes
{
    public class UnaryNegation : UnaryExpression
    {
        public override Type InferredType => Expression.InferredType;

        public override string ToString()
        {
            return $"(-{Expression})";
        }
    }
}