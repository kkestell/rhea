using System;
using System.Linq;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Rhea.Ast.Nodes;
using Type = Rhea.Ast.Nodes.Type;

namespace Rhea.Ast
{
    class FunctionBuilder : RheaBaseListener
    {
        public FunctionNode Function { get; private set; }

        public override void EnterFunction([NotNull] RheaParser.FunctionContext context)
        {
            Function = new FunctionNode
            {
                Name = context.name().GetText(),
                Type = new Type(context.type().GetText()),
                Block = new Block
                {
                    Statements = context.block()._statements.Select(s => new StatementBuilder().Visit(s))
                }
            };
        }

        public override void EnterParameter([NotNull] RheaParser.ParameterContext context)
        {
            var arg = new FunctionParameter
            {
                Name = context.name().GetText(),
                Type = context.type().GetText()
            };

            Function.Parameters.Add(arg);
        }
    }
}