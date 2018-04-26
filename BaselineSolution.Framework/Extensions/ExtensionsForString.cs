namespace BaselineSolution.Framework.Extensions
{
    public static class ExtensionsForString
    {
        public static string NullIfEmpty(this string content)
        {
            if (content == string.Empty)
                return null;
            return content;
        }

        public static bool IsNullOrEmpty(this string content)
        {
            return string.IsNullOrWhiteSpace(content);
        }
    }
}
