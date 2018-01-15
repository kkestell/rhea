namespace Rhea.Ast.Nodes
{
    public class VariableInitialization : VariableDeclaration
    {
        public Expression Expression { get; set; }

        public override string ToString()
        {
            return $"{Type} {Name} = {Expression};";
        }
    }
}