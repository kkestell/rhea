namespace Rhea.Ast.Nodes
{
    public interface IScope
    {
        IScope Parent { get; set; }

        VariableDeclaration FindDeclaration(string name);

        FunctionDefinition FindFunction(string name);
    }
}