using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhea.Ast.Nodes
{
    public class Function : IScope
    {
        public string Name { get; set; }

        public Type Type { get; set; }

        public IEnumerable<FunctionParameter> Parameters { get; set; }

        public Block Block { get; set; }

        public Module Module { get; set; }

        public IScope ParentScope { get; set; }

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

        public Function FindFunction(string name)
        {
            return Module.FindFunction(name);
        }

        public Struct FindStruct(string name)
        {
            return Module.FindStruct(name);
        }

        public VariableDeclaration FindDeclaration(string name)
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