﻿using System.Collections.Generic;
using MetaPrograms.Members;

namespace MetaPrograms.Expressions
{
    public class BinaryExpression : AbstractExpression
    {
        public override void AcceptVisitor(StatementVisitor visitor)
        {
            visitor.VisitBinaryExpression(this);

            if (Left != null)
            {
                Left.AcceptVisitor(visitor);
            }

            if (Right != null)
            {
                Right.AcceptVisitor(visitor);
            }
        }

        public override AbstractExpression AcceptRewriter(StatementRewriter rewriter)
        {
            return rewriter.RewriteBinaryExpression(this);
        }

        public AbstractExpression Left { get; set; }
        public BinaryOperator Operator { get; set; }
        public AbstractExpression Right { get; set; }
    }
}
