using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace Rhea
{
    internal abstract class Node
    {
    }

    internal class ExpressionNode : Node
    {
        public string Text { get; set; }
    }

    internal class BlockNode : Node
    {
        public List<ExpressionNode> Expressions { get; set; } = new List<ExpressionNode>();

        public override string ToString()
        {
            return "{}";
        }
    }

    internal class FunctionArgumentNode : Node
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return $"{Type} {Name}";
        }
    }

    internal class FunctionNode : Node
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<FunctionArgumentNode> Arguments { get; set; } = new List<FunctionArgumentNode>();
        public BlockNode Block { get; set; }

        public override string ToString()
        {
            var argList = string.Join(", ", Arguments.Select(a => a.ToString()));
            return $"{Type} {Name}({argList}) {Block}";
        }
    }

    internal class ProgramNode : Node
    {
        public List<FunctionNode> Functions { get; set; } = new List<FunctionNode>();

        public override string ToString()
        {
            return string.Join("\n\n", Functions.Select(f => f.ToString()));
        }
    }

    internal class BlockNodeBuilder : RheaBaseListener
    {
        public BlockNode Block { get; private set; }

        public override void EnterBlock([NotNull] RheaParser.BlockContext context)
        {
            Block = new BlockNode();
        }
    }

    internal class FunctionNodeBuilder : RheaBaseListener
    {
        public FunctionNode Function { get; private set; }

        public override void EnterFunction([NotNull] RheaParser.FunctionContext context)
        {
            Function = new FunctionNode
            {
                Name = context.name().GetText(),
                Type = context.type().GetText()
            };
        }

        public override void EnterArgument([NotNull] RheaParser.ArgumentContext context)
        {
            var arg = new FunctionArgumentNode
            {
                Name = context.name().GetText(),
                Type = context.type().GetText()
            };

            Function.Arguments.Add(arg);
        }

        public override void EnterBlock([NotNull] RheaParser.BlockContext context)
        {
            var builder = new BlockNodeBuilder();
            ParseTreeWalker.Default.Walk(builder, context);

            Function.Block = builder.Block;
        }
    }

    internal class AstBuilder : RheaBaseListener
    {
        public ProgramNode Program { get; } = new ProgramNode();

        public override void EnterFunction([NotNull] RheaParser.FunctionContext context)
        {
            var builder = new FunctionNodeBuilder();
            ParseTreeWalker.Default.Walk(builder, context);

            Program.Functions.Add(builder.Function);
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var src = File.ReadAllText(args[0]);
            var input = new AntlrInputStream(src);

            var lexer = new RheaLexer(input);
            var tokens = new CommonTokenStream(lexer);

            var parser = new RheaParser(tokens);
            var tree = parser.program();

            var astBuilder = new AstBuilder();
            ParseTreeWalker.Default.Walk(astBuilder, tree);

            var program = astBuilder.Program.ToString();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}