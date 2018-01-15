using System;

namespace Rhea.Ast.Nodes
{
    public abstract class Expression : Statement
    {
        public abstract Type InferredType { get; }
    }
}