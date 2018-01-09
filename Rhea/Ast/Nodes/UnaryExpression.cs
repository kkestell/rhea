﻿using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class UnaryExpression : Expression
    {
        public Expression Expression { get; set; }

        public override string ToString()
        {
            return $"({Expression})";
        }
    }
}