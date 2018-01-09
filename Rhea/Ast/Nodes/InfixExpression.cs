namespace Rhea.Ast.Nodes
{
    class InfixExpression : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }
    }
}
