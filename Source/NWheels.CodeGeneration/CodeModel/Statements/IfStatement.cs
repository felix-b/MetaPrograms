﻿using NWheels.CodeGeneration.CodeModel.Expressions;

namespace NWheels.CodeGeneration.CodeModel.Statements
{
    public class IfStatement : AbstractStatement
    {
        public IfStatement()
        {
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public override void AcceptVisitor(StatementVisitor visitor)
        {
            visitor.VisitIfStatement(this);

            if (Condition != null)
            {
                Condition.AcceptVisitor(visitor);
            }

            if (ThenBlock != null)
            {
                ThenBlock.AcceptVisitor(visitor);
            }

            if (ElseBlock != null)
            {
                ElseBlock.AcceptVisitor(visitor);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public AbstractExpression Condition { get; set; }
        public BlockStatement ThenBlock { get; set; }
        public BlockStatement ElseBlock { get; set; }
    }
}
