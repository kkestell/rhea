using System;

namespace Rhea.Ast.Nodes
{
    public class InfixExpression : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }

        public override Type InferredType
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}