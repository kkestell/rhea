using System;
using System.Diagnostics;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Rhea.Ast;

namespace Rhea
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();

            var inputFilename = args[0];
            var outputFilename = $"{Path.GetFileNameWithoutExtension(inputFilename)}.c";

            var src = File.ReadAllText(inputFilename);
            var input = new AntlrInputStream(src);

            var lexer = new RheaLexer(input);
            var tokens = new CommonTokenStream(lexer);

            var parser = new RheaParser(tokens);
            var tree = parser.program();

            var astBuilder = new AstBuilder();
            ParseTreeWalker.Default.Walk(astBuilder, tree);

            var program = astBuilder.ToString();

            File.WriteAllText(outputFilename, program);

            Console.WriteLine(program);

            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}