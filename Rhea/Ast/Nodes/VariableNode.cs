using Rhea.Ast.Nodes;

namespace Rhea.Ast
{
    class VariableNode : AtomNode
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}