
namespace AmazonWork.Utility
{
    public class StringUtility
    {
        public static string RemoveFirstSubstringFromString(string actualString, string substring)
        {
            var startingIndex = actualString.IndexOf(substring);
            return actualString.Remove(startingIndex, substring.Length);
        }
    }
}
