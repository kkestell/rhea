using System;

namespace Rhea.Ast.Nodes
{
    public class Variable : Atom
    {
        public string Name { get; set; }

        public override Type InferredType
        {
            get
            {
                var variableDeclaration = Scope.FindDeclaration(Name);

                if(variableDeclaration == null)
                    throw new Exception($"Can't find declaration for {Name}");

                return variableDeclaration.Type;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}