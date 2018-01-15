namespace Rhea.Ast.Nodes
{
    public abstract class Statement : Node
    {
        public Block Scope { get; set; }
    }
}