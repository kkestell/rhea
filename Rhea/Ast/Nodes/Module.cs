using System.Collections.Generic;
using System.Linq;

namespace Rhea.Ast.Nodes
{
    using System.Text.RegularExpressions;

    public class Module : Node
    {
        public string Name { get; }

        public IEnumerable<Function> Functions { get; set; }

        public IEnumerable<Struct> Structs { get; set; }

        public Module(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            var standardIncludes = new[]
            {
                "stdbool",
                "stdint",
                "stdio",
                "stdlib"
            };

            var standardIncludeStatements = string.Join(
                "\n",
                standardIncludes.Select(i => $"#include <{i}.h>"));

            var vendorIncludes = new[]
            {
                "gc"
            };

            var vendorIncludeStatements = string.Join(
                "\n",
                vendorIncludes.Select(i => $"#include \"{i}.h\""));

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

            var runtime = @"typedef struct {
  char* data;
  size_t len;
} string;

string* str(char* val)
{
  string* s = (string*)GC_MALLOC_ATOMIC(sizeof(string));
  s->len = strlen(val);
  s->data = (char*)GC_MALLOC_ATOMIC(sizeof(char) * s->len);
  strcpy(s->data, val);
  return s;
}";

            var program = $"{standardIncludeStatements}\n\n{vendorIncludeStatements}\n\n{runtime}\n\n{structs}\n\n{forwardDeclarations}\n\n{functionDefinitions}";

            program = Regex.Replace(program, @"\n{2,}", "\n\n");

            return program;
        }

        public Function FindFunction(string name)
        {
            return Functions.SingleOrDefault(f => f.Name == name);
        }

        public Struct FindStruct(string name)
        {
            return Structs.FirstOrDefault(s => s.Name == name);
        }
    }
}