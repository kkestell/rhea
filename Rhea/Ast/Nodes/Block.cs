using System.Collections.Generic;
using System.Linq;

namespace Rhea.Ast.Nodes
{
    public class Block : Scope
    {
        public Block Scope { get; set; }

        public IEnumerable<Statement> Statements { get; set; }

        public override string ToString()
        {
            var statements = string.Join("\n", Statements.Select(e => e.ToString()));
            return $"{{\n{statements}\n}}";
        }

        public override VariableDeclaration FindDeclaration(string name)
        {
            var declaration = Statements
                .OfType<VariableDeclaration>()
                .FirstOrDefault(d => d.Name == name);

            if (declaration != null)
                return declaration;

            return Parent.FindDeclaration(name);
        }
    }
}