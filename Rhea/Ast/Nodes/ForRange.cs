namespace Rhea.Ast.Nodes
{
    public class ForRange : Statement, IStatementWithBlock, IScope
    {
        public IScope Parent { get; set; }

        public Block Block { get; set; }

        public VariableDeclaration Iterator { get; set; }

        public Range Range { get; set; }

        public VariableDeclaration FindDeclaration(string name)
        {
            if (Iterator.Name == name)
                return Iterator;

            return Parent.FindDeclaration(name);
        }

        public FunctionDefinition FindFunction(string name)
        {
            return Parent.FindFunction(name);
        }

        public override string ToString()
        {
            return
                $"for({Iterator.Type} {Iterator.Name} = {Range.Start}; {Iterator.Name} < {Range.End}; {Iterator.Name}++)\n{Block}";
        }
    }
}