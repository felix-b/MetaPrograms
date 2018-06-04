﻿using System;
using MetaPrograms.CodeModel.Imperative.Expressions;
using MetaPrograms.CodeModel.Imperative.Statements;

// ReSharper disable InconsistentNaming

namespace MetaPrograms.CodeModel.Imperative.Fluent
{
    public class FluentIf
    {
        private readonly IfStatement _statement;

        public FluentIf(AbstractExpression condition)
        {
            var block = BlockContext.GetBlockOrThrow();

            _statement = new IfStatement(block.PopExpression(condition), thenBlock: null, elseBlock: null);
            block.AppendStatement(_statement);
        }

        public FluentElse THEN(Action body)
        {
            var container = new BlockContainer();

            using (CodeGeneratorContext.GetContextOrThrow().PushState(container))
            {
                body?.Invoke();
            }

            return new FluentElse(BlockContext.Replace(_statement, _statement.WithThenBlock(container.Block)));
        }
    }
}