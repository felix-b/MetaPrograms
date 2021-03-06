﻿using System;
using System.Linq;
using MetaPrograms;
using MetaPrograms.Members;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MetaPrograms.CSharp.Reader
{
    public class ClassReader : IPhasedTypeReader
    {
        private readonly TypeReaderMechanism _mechanism;

        public ClassReader(TypeReaderMechanism mechanism)
        {
            _mechanism = mechanism;
            _mechanism.MemberBuilder.TypeKind = TypeMemberKind.Class;
        }

        public void RegisterProxy()
        {
            _mechanism.RegisterTemporaryProxy();
        }

        public void ReadName()
        {
            _mechanism.ReadName();
        }

        public void ReadGenerics()
        {
            _mechanism.ReadGenerics();
        }

        public void ReadAncestors()
        {
            _mechanism.ReadContainingType();
            _mechanism.ReadBaseType();
            _mechanism.ReadBaseInterfaces();
        }

        public void ReadMemberDeclarations()
        {
            _mechanism.ReadMemberDeclarations();
        }

        public void ReadAttributes()
        {
            _mechanism.ReadAttributes();
        }

        public void ReadMemberImplementations()
        {
            _mechanism.ReadMemberImplementations();
        }

        public void RegisterReal()
        {
            _mechanism.RegisterFinalType();
        }

        public override string ToString()
        {
            return TypeSymbol.ToString();
        }

        public INamedTypeSymbol TypeSymbol => _mechanism.Symbol;
        public TypeMember TypeMember => _mechanism.CurrentMember;
    }
}
