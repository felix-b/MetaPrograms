﻿using System.Reflection;

namespace MetaPrograms.CodeModel.Imperative.Members
{
    public class PropertyMember : AbstractMember
    {
        public PropertyMember()
        {
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public PropertyMember(TypeMember declaringType, MemberVisibility visibility, MemberModifier modifier, TypeMember type, string name)
            : base(declaringType, visibility, modifier, name)
        {
            this.PropertyType = type;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public override void AcceptVisitor(MemberVisitor visitor)
        {
            base.AcceptVisitor(visitor);

            visitor.VisitProperty(this);

            if (this.Getter != null)
            {
                this.Getter.AcceptVisitor(visitor);
            }

            if (this.Setter != null)
            {
                this.Setter.AcceptVisitor(visitor);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------

        public TypeMember PropertyType { get; set; }
        public MethodMember Getter { get; set; }
        public MethodMember Setter { get; set; }
        public PropertyInfo PropertyBinding { get; set; }
    }
}
