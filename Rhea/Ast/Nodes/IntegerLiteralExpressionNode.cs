namespace Rhea.Ast.Nodes
{
    class IntegerLiteralExpressionNode : LiteralExpressionNode
    {
        public int Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
