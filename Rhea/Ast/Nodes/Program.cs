using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhea.Ast.Nodes
{
    public class Program : Node
    {
        public List<FunctionDefinition> Functions { get; set; } = new List<FunctionDefinition>();

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

            var forwardDeclarations = string.Join(
                "\n",
                Functions
                    .Where(f => f.Name != "main")
                    .Select(f => f.Declaration));

            var functionDefinitions = string.Join(
                "\n\n",
                Functions
                    .Select(f => f.ToString()));

            return $"{includeStatements}\n\n{forwardDeclarations}\n\n{functionDefinitions}";
        }

        public FunctionDefinition FindFunction(string name)
        {
            var func = Functions.Where(f => f.Name == name).SingleOrDefault();

            if (func == null)
                throw new Exception($"Cannot find function {name}");

            return func;
        }
    }
}