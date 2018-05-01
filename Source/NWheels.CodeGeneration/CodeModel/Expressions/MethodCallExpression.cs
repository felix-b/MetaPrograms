﻿using System.Collections.Generic;
using NWheels.CodeGeneration.CodeModel.Members;

namespace NWheels.CodeGeneration.CodeModel.Expressions
{
    public class MethodCallExpression : AbstractExpression
    {
        public MethodCallExpression()
        {
            this.Arguments = new List<Argument>();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public override void AcceptVisitor(StatementVisitor visitor)
        {
            visitor.VisitMethodCallExpression(this);

            if (Target != null)
            {
                Target.AcceptVisitor(visitor);
            }

            foreach (var argument in Arguments)
            {
                argument.Expression.AcceptVisitor(visitor);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public AbstractExpression Target { get; set; }
        public MethodMember Method { get; set; }
        public string MethodName { get; set; }
        public List<Argument> Arguments { get; }
        public bool IsAsyncAwait { get; set; }
    }
}

