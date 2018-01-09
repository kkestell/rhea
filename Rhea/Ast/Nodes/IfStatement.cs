namespace Rhea.Ast.Nodes
{
    class IfStatement : Statement
    {
        public Expression Expression { get; set; }
        public Block Block { get; set; }

        public override string ToString()
        {
            return $"if({Expression})\n{Block}";
        }
    }
}
