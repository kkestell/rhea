using System.Linq;
using Antlr4.Runtime.Misc;
using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    internal class FunctionBuilder : RheaBaseListener
    {
        public Nodes.Program program;

        public FunctionBuilder(Nodes.Program program)
        {
            this.program = program;
        }

        public FunctionDefinition Function { get; private set; }

        public override void EnterFunction([NotNull] RheaParser.FunctionContext context)
        {
            var name = context.name().GetText();
            var type = context.type().GetText();

            var block = new Block();

            var statements = context.block()._statements.Select(s => new StatementBuilder(block).Visit(s));

            block.Statements = statements;

            Function = new FunctionDefinition
            {
                Name = name,
                Type = new Type(type),
                Block = block,
                Program = program
            };

            Function.Block.Parent = Function;
        }

        public override void EnterParameter([NotNull] RheaParser.ParameterContext context)
        {
            var arg = new FunctionParameter
            {
                Name = context.name().GetText(),
                Type = new Type(context.type().GetText())
            };

            Function.Parameters.Add(arg);
        }
    }
}