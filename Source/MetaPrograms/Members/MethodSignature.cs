﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;

namespace MetaPrograms.Members
{
    public class MethodSignature
    {
        public bool IsAsync { get; set; }
        public MethodParameter ReturnValue { get; set; }
        public List<MethodParameter> Parameters { get; set; } = new List<MethodParameter>();

        public bool IsVoid => (ReturnValue == null);
        public TypeMember ReturnType => (IsVoid ? TypeMember.Void : ReturnValue.Type);
    }
}
