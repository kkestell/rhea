using System.Collections.Generic;
using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class ArgumentListBuilder : RheaBaseListener
    {
        public List<Expression> Arguments { get; set; }

        public override void EnterArgumentList(RheaParser.ArgumentListContext context)
        {
            Arguments = new List<Expression>();
        }

        public override void EnterArgument(RheaParser.ArgumentContext context)
        {
            Arguments.Add(new ExpressionBuilder().Visit(context.expression()));
        }
    }
}
