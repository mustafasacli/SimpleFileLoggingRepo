////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Extensions\LoggingStringExtension.cs
//
// summary:	Implements the logging string extension class
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace SimpleFileLogging
{
    using System.Linq;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A logging string extension. </summary>
    ///
    /// <remarks>   Msacli, 29.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class LoggingStringExtension
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A string extension method that normalize string. </summary>
        ///
        /// <remarks>   Msacli, 29.04.2019. </remarks>
        ///
        /// <param name="s">    The s to act on. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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