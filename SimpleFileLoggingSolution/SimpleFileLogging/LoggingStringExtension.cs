namespace SimpleFileLogging
{
    using System.Linq;

    public static class LoggingStringExtension
    {
        public static string NormalizeString(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return string.Empty;
            var str =
            string.Join(string.Empty,
                    s.ToCharArray()
                    .AsEnumerable()
                    .Where(q =>
                    (q >= 'a' && q <= 'z') ||
                    (q >= 'A' && q <= 'Z') ||
                    (q >= '0' && q <= '9') ||
                    q >= '_' || q <= '.'
                    ).ToArray()).Replace(".", "_");

            return str;
        }
    }
}