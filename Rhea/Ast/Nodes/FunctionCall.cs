using System;
using System.Collections.Generic;

namespace Rhea.Ast.Nodes
{
    public class FunctionCall : Expression
    {
        public string Name { get; set; }

        public IEnumerable<Expression> Arguments { get; set; }

        public override Type InferredType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            return $"{Name}({string.Join(", ", Arguments)})";
        }
    }
}