using System.Linq;
using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class ModuleBuilder : RheaBaseVisitor<Module>
    {
        public override Module VisitModule(RheaParser.ModuleContext context)
        {
            var newModule = new Module("main")
            {
                Context = context
            };

            newModule.Functions = context._functions.Select(f => MakeFunction(newModule, f));
            newModule.Structs = context._structs.Select(s => MakeStruct(newModule, s));

            return newModule;
        }

        static Function MakeFunction(Module module, RheaParser.FunctionContext context)
        {
            var newFunction = new Function
            {
                Name = context.name().GetText(),
                Type = new Type(context.type().GetText()),
                Module = module,
                Parameters = context._parameters.Select(
                    p => new FunctionParameter
                    {
                        Context = p,
                        Name = p.name().GetText(),
                        Type = new Type(p.type().GetText())
                    })
            };

            var block = new Block
            {
                ParentScope = newFunction
            };

            var statements = context.block()._statements.Select(s => new StatementBuilder(block, block).Visit(s));

            block.Statements = statements;

            newFunction.Block = block;

            return newFunction;
        }

        static Struct MakeStruct(Module module, RheaParser.StructContext context)
        {
            return new Struct
            {
                Context = context,
                Name = context.structName.Text,
                Members = context._members.Select(m => new Member
                {
                    Name = m.memberName.Text,
                    Type = new Type(m.type().GetText())
                })
            };
        }
    }
}