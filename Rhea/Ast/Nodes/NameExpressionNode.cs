namespace Rhea.Ast.Nodes
{
    class NameExpressionNode : ExpressionNode
    {
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
