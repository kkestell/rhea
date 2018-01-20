using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhea.Ast.Nodes
{
    using System.Text.RegularExpressions;

    public class Program : Node
    {
        public List<FunctionDefinition> Functions { get; set; } = new List<FunctionDefinition>();

        public List<Struct> Structs { get; set; } = new List<Struct>();

        public override string ToString()
        {
            var standardIncludes = new[]
            {
                "stdbool",
                "stdint",
                "stdio",
                "stdlib"
            };

            var includeStatements = string.Join(
                "\n",
                standardIncludes.Select(i => $"#include <{i}.h>"));

            var structs = string.Join(
                "\n",
                Structs
                    .Select(s => s.ToString()));

            var forwardDeclarations = string.Join(
                "\n",
                Functions
                    .Where(f => f.Name != "main")
                    .Select(f => f.Declaration));

            var functionDefinitions = string.Join(
                "\n\n",
                Functions
                    .Select(f => f.ToString()));

            var program = $"{includeStatements}\n\n{structs}\n\n{forwardDeclarations}\n\n{functionDefinitions}";

            program = Regex.Replace(program, @"\n{2,}", "\n\n");

            return program;
        }

        public FunctionDefinition FindFunction(string name)
        {
            var func = Functions.Where(f => f.Name == name).SingleOrDefault();

            if (func == null)
                throw new Exception($"Cannot find function {name}");

            return func;
        }

        public Struct FindStruct(string name)
        {
            return Structs.FirstOrDefault(s => s.Name == name);
        }
    }
}