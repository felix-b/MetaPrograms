﻿using System;
using System.Collections.Generic;
using System.Text;
using MetaPrograms.CodeModel.Imperative.Members;

namespace Example.HyperappAdapter.Metadata
{
    public partial class HyperappPageMetadata
    {
        public Dictionary<TypeMember, LocalVariable> ServiceVarByType { get; } = new Dictionary<TypeMember, LocalVariable>();
    }
}
