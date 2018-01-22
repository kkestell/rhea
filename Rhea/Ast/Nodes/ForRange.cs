namespace Rhea.Ast.Nodes
{
    public class ForRange : Statement, IStatementWithBlock, IScope
    {
        public IScope ParentScope { get; set; }

        public Block Block { get; set; }

        public VariableDeclaration Iterator { get; set; }

        public Range Range { get; set; }

        public VariableDeclaration FindDeclaration(string name)
        {
            if (Iterator.Name == name)
                return Iterator;

            return ParentScope.FindDeclaration(name);
        }

        public Function FindFunction(string name)
        {
            return ParentScope.FindFunction(name);
        }

        public Struct FindStruct(string name)
        {
            return ParentScope.FindStruct(name);
        }

        public override string ToString()
        {
            return
                $"for ({Iterator.Type} {Iterator.Name} = {Range.Start}; {Iterator.Name} < {Range.End}; {Iterator.Name}++) {Block}";
        }
    }
}