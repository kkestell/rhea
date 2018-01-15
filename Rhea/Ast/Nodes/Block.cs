using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhea.Ast.Nodes
{
    public class Block : Node
    {
        public Block Scope { get; set; }

        public IEnumerable<Statement> Statements { get; set; }

        public override string ToString()
        {
            var statements = string.Join("\n", Statements.Select(e => e.ToString()));
            return $"{{\n{statements}\n}}";
        }

        public VariableDeclaration FindDeclaration(string name)
        {
            return FindDeclaration(name, this);
        }

        VariableDeclaration FindDeclaration(string name, Block scope)
        {
            var declaration = scope
                .Statements
                .OfType<VariableDeclaration>()
                .FirstOrDefault(d => d.Name == name);

            if (declaration != null)
                return declaration;

            if (scope.Scope == null)
                throw new Exception($"Declaration for {name} not found");

            return FindDeclaration(name, scope.Scope);
        }
    }
}