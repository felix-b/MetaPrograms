using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MetaPrograms
{
    public class FilePath
    {
        private static readonly char[] DirectorySeparatorChars = new[] {
            '/',
            '\\'
        };
        
        private readonly string[] _subFolder;
        private readonly string _fileName;

        public FilePath(params string[] pathAndName)
        {
            _subFolder = pathAndName.Take(pathAndName.Length - 1).ToArray();
            _fileName = pathAndName.Last();
        }

        public FilePath(IEnumerable<string> subFolder, string fileName)
        {
            _subFolder = subFolder.ToArray();
            _fileName = fileName;
        }

        public FilePath WithBase(string basePath)
        {
            return new FilePath(new[] { basePath }.Concat(_subFolder), _fileName);
        }

        public FilePath Append(params string[] pathAndName)
        {
            return new FilePath(_subFolder.Append(_fileName).Concat(pathAndName).ToArray());
        }

        public FilePath ReplaceFileName(string newFileName)
        {
            return new FilePath(_subFolder, newFileName);
        }

        public FilePath ReplaceSubFolder(IEnumerable<string> newSubFolder)
        {
            return new FilePath(newSubFolder, _fileName);
        }

        public FilePath Up(int count = 1)
        {
            return new FilePath(_subFolder.Take(_subFolder.Length - count + 1).ToArray());
        }

        public FilePath Tail(int count = 1)
        {
            return new FilePath(
                _subFolder
                .Skip(_subFolder.Length - count + 1)
                .Append(_fileName)
                .ToArray());
        }

        public FilePath RelativeTo(FilePath other)
        {
            var thisParts = _subFolder.Append(_fileName).ToArray();    
            var otherParts = other._subFolder.Append(other._fileName).ToArray();
            int commonPrefixPartCount;
            
            for (
                commonPrefixPartCount = 0; 
                commonPrefixPartCount < thisParts.Length && 
                commonPrefixPartCount < otherParts.Length &&
                thisParts[commonPrefixPartCount] == otherParts[commonPrefixPartCount]; 
                commonPrefixPartCount++)
            {
            }

            return (commonPrefixPartCount > 0 
                ? new FilePath(thisParts.Skip(commonPrefixPartCount).ToArray())
                : this);
        }

        public override string ToString()
        {
            return FullPath;
        }

        public IReadOnlyList<string> SubFolder => _subFolder;
        public string FileName => _fileName;
        public string FolderPath => Path.Combine(_subFolder);
        public string FullPath => Path.Combine(FolderPath, _fileName);
        public string NormalizedFolderPath => Normalize(FolderPath);
        public string NormalizedFullPath => Normalize(FullPath);

        public static string Normalize(string path)
        {
            return path?.Replace(Path.DirectorySeparatorChar, '/');
        }

        public static FilePath Parse(string path)
        {
            int initialSlashCount;

            for (
                initialSlashCount = 0; 
                initialSlashCount < path.Length && path[initialSlashCount] == '/'; 
                initialSlashCount++)
            {
            }

            var parts = path
                .Substring(initialSlashCount)
                .Split(DirectorySeparatorChars, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 0)
            {
                parts[0] = path.Substring(0, initialSlashCount + parts[0].Length);
                return new FilePath(parts);
            }

            throw new ArgumentException("Specified string is an empty path", nameof(path));
        }
    }
}
