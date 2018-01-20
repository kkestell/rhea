using System.Linq;

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    public class ProgramBuilder : RheaBaseListener
    {
        public Nodes.Program Program { get; } = new Nodes.Program();

        public override void EnterFunction([NotNull] RheaParser.FunctionContext context)
        {
            var builder = new FunctionBuilder(this.Program);
            ParseTreeWalker.Default.Walk(builder, context);

            Program.Functions.Add(builder.Function);
        }

        public override void EnterStruct(RheaParser.StructContext context)
        {
            var newStruct = new Struct
            {
                Name = context.structName.Text,
                Members = context._members.Select(m => new Member
                {
                    Name = m.memberName.Text,
                    Type = new Type(m.type().GetText())
                })
            };

            Program.Structs.Add(newStruct);
        }

        public override string ToString()
        {
            return this.Program.ToString();
        }
    }
}