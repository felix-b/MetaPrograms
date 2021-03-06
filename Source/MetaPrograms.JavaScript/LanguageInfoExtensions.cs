﻿using MetaPrograms;

namespace MetaPrograms.JavaScript
{
    public static class LanguageInfoExtensions
    {
        private static readonly LanguageInfo JavaScriptLanguage = new LanguageInfo(name: "JavaScript", abbreviation: "JS");

        public static LanguageInfo JavaScript(this LanguageInfo.ExtensibleEntries entries) => JavaScriptLanguage;
    }
}
