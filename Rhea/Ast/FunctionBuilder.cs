using System;
using System.Linq;
using System.Net.Mime;
using Antlr4.Runtime.Misc;
using Rhea.Ast.Nodes;
using Type = Rhea.Ast.Nodes.Type;

namespace Rhea.Ast
{
    internal class FunctionBuilder : RheaBaseListener
    {
        public FunctionNode Function { get; private set; }

        public override void EnterFunction([NotNull] RheaParser.FunctionContext context)
        {
            var name = context.name().GetText();
            var type = context.type().GetText();

            var block = new Block();

            var statements = context.block()._statements.Select(s => new StatementBuilder(block).Visit(s));

            block.Statements = statements;

            Function = new FunctionNode
            {
                Name = name,
                Type = new Type(type),
                Block = block
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