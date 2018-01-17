namespace Rhea.Ast.Nodes
{
    public class False : Atom
    {
        public override Type InferredType => new Type("bool");

        public override string ToString()
        {
            return "false";
        }
    }
}
