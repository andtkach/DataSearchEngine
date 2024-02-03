namespace ProcessEngine.API.Utils
{
    public static class EmailSplitter
    {
        public static string SplitEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return string.Empty;

            var str = email
                .Replace("@", " ")
                .Replace("-", " ")
                .Replace("_", " ")
                .Replace(".", " ");
            return str;
        }
    }
}
