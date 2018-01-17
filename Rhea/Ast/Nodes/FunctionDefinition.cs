using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhea.Ast.Nodes
{
    public class FunctionDefinition : Scope
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public List<FunctionParameter> Parameters { get; set; } = new List<FunctionParameter>();
        public Block Block { get; set; }
        public Program Program { get; set; }

        public string Declaration
        {
            get
            {
                var argList = string.Join(", ", Parameters.Select(p => p.ToString()));
                return $"{Type} {Name}({argList});";
            }
        }

        public override string ToString()
        {
            var argList = string.Join(", ", Parameters.Select(a => a.ToString()));
            return $"{Type} {Name}({argList}) {Block}";
        }

        public override FunctionDefinition FindFunction(string name)
        {
            return Program.FindFunction(name);
        }

        public override VariableDeclaration FindDeclaration(string name)
        {
            var parameter = Parameters.SingleOrDefault(p => p.Name == name);

            if (parameter != null)
                return new VariableDeclaration
                {
                    Name = parameter.Name,
                    Type = parameter.Type
                };

            throw new Exception($"Can't find declaration for {name}");
        }
    }
}