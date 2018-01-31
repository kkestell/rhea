using System.Linq;

using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
	class ModuleBuilder : RheaBaseVisitor<Module>
	{
		Module parentModule;

		public ModuleBuilder(Module parentModule)
		{
			this.parentModule = parentModule;
		}

		public override Module VisitModule(RheaParser.ModuleContext context)
		{
			var newModule = new Module("main")
			{
				Context = context,
				ParentModule = parentModule
			};

			newModule.Functions = context._functions.Select(f => MakeFunction(newModule, f));
			newModule.ExternFunctions = context._externFunctions.Select(f => MakeExternFunction(newModule, f));
			newModule.Structs = context._structs.Select(MakeStruct);

			return newModule;
		}

		static Function MakeExternFunction(Module module, RheaParser.ExternFunctionDeclarationContext context)
		{
			var name = context.functionDeclaration().name().GetText();
			var type = new Type(context.functionDeclaration().type().GetText());

			var newFunction = new ExternFunction
			{
				Name = name,
				Type = type,
				Module = module,
				Parameters = context.functionDeclaration()._parameters.Select(p => new FunctionParameter
					{
						Context = p,
						Name = p.name().GetText(),
						Type = new Type(p.type().GetText())
					})
			};

			return newFunction;
		}

		static Function MakeFunction(Module module, RheaParser.FunctionContext context)
		{
			var newFunction = new Function
			{
				Name = context.functionDeclaration().name().GetText(),
				Type = new Type(context.functionDeclaration().type().GetText()),
				Module = module,
				Parameters = context.functionDeclaration()._parameters.Select(
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

		static Struct MakeStruct(RheaParser.StructContext context)
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