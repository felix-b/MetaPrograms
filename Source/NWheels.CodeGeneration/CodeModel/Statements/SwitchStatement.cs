﻿using System.Collections.Generic;
using NWheels.CodeGeneration.CodeModel.Expressions;
using NWheels.CodeGeneration.CodeModel.Members;

namespace NWheels.CodeGeneration.CodeModel.Statements
{
    public class SwitchStatement : AbstractStatement
    {
        public SwitchStatement()
        {
            this.CaseBlocks = new List<SwitchCaseBlock>();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public override void AcceptVisitor(StatementVisitor visitor)
        {
            visitor.VisitSwitchStatement(this);

            foreach (var caseBlock in this.CaseBlocks)
            {

            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public List<SwitchCaseBlock> CaseBlocks { get; }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------

    public class SwitchCaseBlock
    {
        public SwitchCaseBlock()
        {
            this.Body = new BlockStatement();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public void AcceptVisitor(StatementVisitor visitor)
        {
            if (ConstantMatch != null)
            {
                ConstantMatch.AcceptVisitor(visitor);
            }

            if (PatternMatchType != null)
            {
                visitor.VisitReferenceToTypeMember(PatternMatchType);
            }

            if (PatternMatchCondition != null)
            {
                PatternMatchCondition.AcceptVisitor(visitor);
            }

            if (Body != null)
            {
                Body.AcceptVisitor(visitor);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public AbstractExpression ConstantMatch { get; set; }
        public TypeMember PatternMatchType { get; set; }
        public AbstractExpression PatternMatchCondition { get; set; }
        public BlockStatement Body { get; }        
    }
}
