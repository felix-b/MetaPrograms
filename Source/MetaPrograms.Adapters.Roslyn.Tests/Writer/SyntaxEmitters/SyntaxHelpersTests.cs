﻿using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using NWheels.Compilation.Adapters.Roslyn.SyntaxEmitters;
using MetaPrograms.CodeModel.Imperative.Expressions;
using MetaPrograms.CodeModel.Imperative.Members;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NWheels.Compilation.Adapters.Roslyn.UnitTests.SyntaxEmitters
{
    public class SyntaxHelpersTests : SyntaxEmittingTestBase
    {
        public static IEnumerable<object[]> TestCases_TestGetTypeNameSyntax = new object[][] {
            #region Test cases
            new object[] {
                "NS1.C1",
                new TypeMember("NS1", MemberVisibility.Public, TypeMemberKind.Class, "C1")
            },
            new object[] {
                "NS1.C1<NS2.C2>",
                new TypeMember("NS1", MemberVisibility.Public, TypeMemberKind.Class, "C1",
                    new TypeMember("NS2", MemberVisibility.Public, TypeMemberKind.Class, "C2")
                )
            },
            new object[] {
                "NS1.C1<NS2.C2, NS3.C3<NS4.C4>>",
                new TypeMember("NS1", MemberVisibility.Public, TypeMemberKind.Class, "C1",
                    new TypeMember("NS2", MemberVisibility.Public, TypeMemberKind.Class, "C2"),
                    new TypeMember("NS3", MemberVisibility.Public, TypeMemberKind.Class, "C3",
                        new TypeMember("NS4", MemberVisibility.Public, TypeMemberKind.Class, "C4")))
            },
            new object[] {
                "System.DateTime",
                new TypeMember(typeof(DateTime))
            },
            new object[] {
                "System.Collections.Generic.List<System.DateTime>",
                new TypeMember(typeof(List<DateTime>))
            },
            new object[] {
                "System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<string>>",
                new TypeMember(typeof(Dictionary<int, List<string>>))
            },
            new object[] {
                "NWheels.Compilation.Adapters.Roslyn.UnitTests.SyntaxEmitters.SyntaxHelpersTests.TestNestedType",
                new TypeMember(typeof(TestNestedType))
            },
            new object[] {
                "int[]",
                new TypeMember(typeof(int[]))
            },
            new object[] {
                "NWheels.Compilation.Adapters.Roslyn.UnitTests.SyntaxEmitters.SyntaxHelpersTests.TestNestedType[]",
                new TypeMember(typeof(TestNestedType[]))
            },
            new object[] {
                "NWheels.Compilation.Adapters.Roslyn.UnitTests.SyntaxEmitters.SyntaxHelpersTests.TestNestedType[]",
                new TypeMember(typeof(TestNestedType[]))
            },
            #endregion
        };

        [Theory]
        [MemberData(nameof(TestCases_TestGetTypeNameSyntax))]
        public void TestGetTypeNameSyntax(string expectedCode, TypeMember type)
        {
            //-- act

            var actualSyntax = SyntaxHelpers.GetTypeNameSyntax(type);

            //-- assert

            actualSyntax.Should().BeEquivalentToCode(expectedCode);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public static IEnumerable<object[]> TestCases_TestGetTypeNameSyntax_BuiltinTypes = new object[][] {
            #region Test cases
            new object[] { "bool", new TypeMember(typeof(bool)) },
            new object[] { "byte", new TypeMember(typeof(byte)) },
            new object[] { "sbyte", new TypeMember(typeof(sbyte)) },
            new object[] { "short", new TypeMember(typeof(short)) },
            new object[] { "ushort", new TypeMember(typeof(ushort)) },
            new object[] { "int", new TypeMember(typeof(int)) },
            new object[] { "uint", new TypeMember(typeof(uint)) },
            new object[] { "long", new TypeMember(typeof(long)) },
            new object[] { "ulong", new TypeMember(typeof(ulong)) },
            new object[] { "double", new TypeMember(typeof(double)) },
            new object[] { "float", new TypeMember(typeof(float)) },
            new object[] { "decimal", new TypeMember(typeof(decimal)) },
            new object[] { "string", new TypeMember(typeof(string)) },
            new object[] { "char", new TypeMember(typeof(char)) },
            new object[] { "object", new TypeMember(typeof(object)) },
            #endregion
        };

        [Theory]
        [MemberData(nameof(TestCases_TestGetTypeNameSyntax_BuiltinTypes))]
        public void TestGetTypeNameSyntax_BuiltinTypes(string expectedCode, TypeMember type)
        {
            //-- act

            var actualSyntax = SyntaxHelpers.GetTypeNameSyntax(type);

            //-- assert

            actualSyntax.Should().BeEquivalentToCode(expectedCode);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public static IEnumerable<object[]> TestCases_TestGetTypeNameSyntax_NullableTypes = new object[][] {
            #region Test cases
            new object[] { "bool ?", new TypeMember(typeof(bool?)) },
            new object[] { "System.DateTime?", new TypeMember(typeof(DateTime?)) },
            #endregion
        };

        [Theory]
        [MemberData(nameof(TestCases_TestGetTypeNameSyntax_NullableTypes))]
        public void TestGetTypeNameSyntax_NullableTypes(string expectedCode, TypeMember type)
        {
            //-- act

            var actualSyntax = SyntaxHelpers.GetTypeNameSyntax(type);

            //-- assert

            actualSyntax.Should().BeEqualToCode(expectedCode);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public static IEnumerable<object[]> TestCases_TestGetTypeNameSyntax_ByRefTypes = new object[][] {
            #region Test cases
            new object[] { "bool", typeof(bool).MakeByRefType() },
            new object[] { "System.DateTime", typeof(DateTime).MakeByRefType() },
            #endregion
        };

        [Theory]
        [MemberData(nameof(TestCases_TestGetTypeNameSyntax_ByRefTypes))]
        public void TestGetTypeNameSyntax_ByRefTypes(string expectedCode, Type clrType)
        {
            //-- arrange

            TypeMember typeMember = clrType;

            //-- act

            var actualSyntax = SyntaxHelpers.GetTypeNameSyntax(typeMember);

            //-- assert

            actualSyntax.Should().BeEquivalentToCode(expectedCode);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public static IEnumerable<object[]> TestCases_TestGetTypeNameSyntax_GenericTypes = new object[][] {
            #region Test cases
            new object[] {
                "System.IEquatable<int>",
                new TypeMember(typeof(IEquatable<int>))
            },
            new object[] {
                "System.Action<int, string, decimal>",
                new TypeMember(typeof(Action<int, string, decimal>))
            },
            new object[] {
                "System.Collections.Generic.Dictionary<System.DateTime, System.Collections.Generic.HashSet<string>>",
                new TypeMember(typeof(Dictionary<DateTime, HashSet<string>>))
            },
            #endregion
        };

        [Theory]
        [MemberData(nameof(TestCases_TestGetTypeNameSyntax_GenericTypes))]
        public void TestGetTypeNameSyntax_GenericTypes(string expectedCode, TypeMember type)
        {
            //-- act

            var actualSyntax = SyntaxHelpers.GetTypeNameSyntax(type);

            //-- assert

            actualSyntax.Should().BeEqualToCode(expectedCode);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public static IEnumerable<object[]> TestCases_TestGetTypeNameSyntax_OmitNamespaceIfImported = new object[][] {
            #region Test cases
            new object[] {
                "DateTime",
                new Func<TypeMember>(() => {
                    var type = new TypeMember(typeof(DateTime));
                    type.SafeBackendTag().IsNamespaceImported = true;
                    return type;
                })
            },
            new object[] {
                "IEquatable<System.TimeSpan>",
                new Func<TypeMember>(() => {
                    var type = new TypeMember(typeof(IEquatable<TimeSpan>));
                    type.GenericTypeDefinition.SafeBackendTag().IsNamespaceImported = true;
                    return type;
                })
            },
            new object[] {
                "Dictionary<string, TimeSpan>",
                new Func<TypeMember>(() => {
                    var type = new TypeMember(typeof(Dictionary<string, TimeSpan>));
                    type.GenericTypeDefinition.SafeBackendTag().IsNamespaceImported = true;
                    type.GenericTypeArguments[1].SafeBackendTag().IsNamespaceImported = true;
                    return type;
                })
            },
            new object[] {
                "System.Collections.Generic.Dictionary<string, TimeSpan>",
                new Func<TypeMember>(() => {
                    var type = new TypeMember(typeof(Dictionary<string, TimeSpan>));
                    type.GenericTypeDefinition.SafeBackendTag().IsNamespaceImported = false;
                    type.GenericTypeArguments[1].SafeBackendTag().IsNamespaceImported = true;
                    return type;
                })
            },
            new object[] {
                "MyClassOne",
                new Func<TypeMember>(() => {
                    var type = new TypeMember("My.NS1", MemberVisibility.Public, TypeMemberKind.Class, "MyClassOne");
                    type.SafeBackendTag().IsNamespaceImported = true;
                    return type;
                })
            },
            new object[] {
                "MyClassOne<MyClassTwo, My.NS3.MyClassThree>",
                new Func<TypeMember>(() => {
                    var type = new TypeMember("My.NS1", MemberVisibility.Public, TypeMemberKind.Class, "MyClassOne",
                        new TypeMember("My.NS2", MemberVisibility.Public, TypeMemberKind.Class, "MyClassTwo"),
                        new TypeMember("My.NS3", MemberVisibility.Public, TypeMemberKind.Class, "MyClassThree")
                    );
                    type.SafeBackendTag().IsNamespaceImported = true;
                    type.GenericTypeArguments[0].SafeBackendTag().IsNamespaceImported = true;
                    type.GenericTypeArguments[1].SafeBackendTag().IsNamespaceImported = false;
                    return type;
                })
            },
            new object[] {
                "My.NS1.MyClassOne<MyClassTwo, MyClassThree>",
                new Func<TypeMember>(() => {
                    var type = new TypeMember("My.NS1", MemberVisibility.Public, TypeMemberKind.Class, "MyClassOne",
                        new TypeMember("My.NS2", MemberVisibility.Public, TypeMemberKind.Class, "MyClassTwo"),
                        new TypeMember("My.NS3", MemberVisibility.Public, TypeMemberKind.Class, "MyClassThree")
                    );
                    type.SafeBackendTag().IsNamespaceImported = false;
                    type.GenericTypeArguments[0].SafeBackendTag().IsNamespaceImported = true;
                    type.GenericTypeArguments[1].SafeBackendTag().IsNamespaceImported = true;
                    return type;
                })
            },
            #endregion
        };

        [Theory]
        [MemberData(nameof(TestCases_TestGetTypeNameSyntax_OmitNamespaceIfImported))]
        public void TestGetTypeNameSyntax_OmitNamespaceIfImported(string expectedCode, Func<TypeMember> typeFactory)
        {
            //-- arrange

            var type = typeFactory();

            //-- act

            var actualSyntax = SyntaxHelpers.GetTypeNameSyntax(type);

            //-- assert

            actualSyntax.Should().BeEqualToCode(expectedCode);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public static IEnumerable<object[]> TestCases_TestGetLiteralSyntax = new object[][] {
            #region Test cases
            new object[] { 123 , "123" },
            new object[] { 1234.56m, "1234.56M" },
            new object[] { "ABC" , "\"ABC\"" },
            new object[] { 'a' , "'a'" },
            new object[] { null, "null", },
            new object[] { typeof(string), "typeof(string)" },
            new object[] { new TypeMember(typeof(string)), "typeof(string)" },
            new object[] { typeof(Dictionary<int, string>), "typeof(System.Collections.Generic.Dictionary<int, string>)" },
            new object[] { new TypeMember(typeof(Dictionary<int, string>)), "typeof(System.Collections.Generic.Dictionary<int, string>)" },
            new object[] { new ConstantExpression() { Value = 123 }, "123" },
            new object[] { new ConstantExpression() { Value = null }, "null"  }
            #endregion
        };

        [Theory]
        [MemberData(nameof(TestCases_TestGetLiteralSyntax))]
        public void TestGetLiteralSyntax(object value, string expectedCode)
        {
            //-- act

            var actualSyntax = SyntaxHelpers.GetLiteralSyntax(value);

            //-- assert

            actualSyntax.Should().BeEquivalentToCode(expectedCode);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public static IEnumerable<object[]> TestCases_TestGetLegalCSharpIdentifier = new object[][] {
            #region Test cases
            new object[] { "abcDef" , "abcDef" },
            new object[] { "abc1Def2" , "abc1Def2" },
            new object[] { "1abc2Def" , "_abc2Def" },
            new object[] { "abc_def" , "abc_def" },
            new object[] { "abc@def" , "abc_def" },
            new object[] { "abc:def" , "abc_def" },
            new object[] { "abc.def" , "abc_def" },
            new object[] { "abc!def" , "abc_def" },
            new object[] { "abc/def" , "abc_def" },
            new object[] { "abc,def" , "abc_def" },
            new object[] { "abc@def.ghi" , "abc_def_ghi" },
            new object[] { "" , "" },
            new object[] { null , null },
            #endregion
        };

        [Theory]
        [MemberData(nameof(TestCases_TestGetLegalCSharpIdentifier))]
        public void TestGetLegalCSharpIdentifier(string proposedName, string expectedName)
        {
            //-- act

            var actualName = SyntaxHelpers.GetValidCSharpIdentifier(proposedName);

            //-- assert

            actualName.Should().Be(expectedName);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public class TestNestedType {  }
    }
}
