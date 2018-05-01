﻿using System.Collections.Generic;
using NWheels.CodeGeneration.CodeModel.Expressions;

namespace NWheels.CodeGeneration.CodeModel.Statements
{
    public class ForStatement : AbstractStatement
    {
        public ForStatement()
        {
            this.Initializers = new List<AbstractStatement>();
            this.Iterators = new List<AbstractStatement>();
            this.Body = new BlockStatement();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public override void AcceptVisitor(StatementVisitor visitor)
        {
            visitor.VisitForStatement(this);

            foreach (var initializer in Initializers)
            {
                initializer.AcceptVisitor(visitor);
            }

            if (Condition != null)
            {
                Condition.AcceptVisitor(visitor);
            }

            foreach (var iterator in Iterators)
            {
                iterator.AcceptVisitor(visitor);
            }

            Body.AcceptVisitor(visitor);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public List<AbstractStatement> Initializers { get; }
        public AbstractExpression Condition { get; set; }
        public List<AbstractStatement> Iterators { get; }
        public BlockStatement Body { get; }
    }
}
