namespace Rhea.Ast.Nodes
{
    class StringLiteralNode : LiteralExpressionNode
    {
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
