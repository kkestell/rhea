namespace Rhea.Ast.Nodes
{
    public class Variable : Atom
    {
        public string Name { get; set; }

        public override Type InferredType => Scope.FindDeclaration(Name).Type;

        public override string ToString()
        {
            return Name;
        }
    }
}