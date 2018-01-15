using System.Collections.Generic;
using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    internal class ArgumentListBuilder : RheaBaseListener
    {
        public Block Scope { get; set; }

        public List<Expression> Arguments { get; set; }

        public ArgumentListBuilder(Block scope)
        {
            Scope = scope;
        }

        public override void EnterArgumentList(RheaParser.ArgumentListContext context)
        {
            Arguments = new List<Expression>();
        }

        public override void EnterArgument(RheaParser.ArgumentContext context)
        {
            Arguments.Add(new ExpressionBuilder(Scope).Visit(context.expression()));
        }
    }
}