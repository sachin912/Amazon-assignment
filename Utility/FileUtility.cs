using System.IO;

namespace AmazonWork.Utility
{
    public  class FileUtility
    {
        public static string GetSourceDirectoryPath()
        {
            var actualPath = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string extraStringInFilePath = "file:///";
            var directoryPath = StringUtility.RemoveFirstSubstringFromString(actualPath, extraStringInFilePath);
            string parentPath = Directory.GetParent(directoryPath).Parent.Parent.Parent.FullName;
            return parentPath;
        }
    }
}
