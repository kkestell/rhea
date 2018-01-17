namespace Rhea.Ast.Nodes
{
    public abstract class Scope
    {
        public Scope Parent { get; set; }

        public abstract VariableDeclaration FindDeclaration(string name);

        public virtual FunctionDefinition FindFunction(string name)
        {
            return Parent.FindFunction(name);
        }
    }
}