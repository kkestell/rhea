using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class Variable : Atom
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}