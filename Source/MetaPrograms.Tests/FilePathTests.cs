using NUnit.Framework;
using Shouldly;

namespace MetaPrograms.Tests
{
    public class FilePathTests
    {
        [TestCase("aaa/bbb/ccc", new[] {"aaa", "bbb"}, "ccc")]
        [TestCase("/aaa/bbb/ccc", new[] {"/aaa", "bbb"}, "ccc")]
        [TestCase(@"aaa\bbb\ccc", new[] {"aaa", "bbb"}, "ccc")]
        [TestCase(@"c:\aaa\bbb\ccc", new[] {"c:", "aaa", "bbb"}, "ccc")]
        public void CanParse(string path, string[] expectedSubFolder, string expectedFileName)
        {
            var actual = FilePath.Parse(path);
            
            actual.SubFolder.ShouldBe(expectedSubFolder);
            actual.FileName.ShouldBe(expectedFileName);
        }
    }
}
