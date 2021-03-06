﻿using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace MetaPrograms.CSharp.Reader
{
    public interface IWorkspaceLoader
    {
        Workspace LoadWorkspace(IEnumerable<string> projectFilePaths);
    }
}