namespace Rhea.Ast.Nodes
{
    public class If : Statement, IStatementWithBlock
    {
        public Expression Expression { get; set; }

        public Block Block { get; set; }

        public override string ToString()
        {
            return $"if({Expression})\n{Block}";
        }
    }
}