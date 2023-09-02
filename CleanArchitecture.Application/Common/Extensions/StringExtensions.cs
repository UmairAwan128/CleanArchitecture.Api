using System.Text.RegularExpressions;

namespace CleanArchitecture.Application.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsValidEmailAddress(this string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
    }
}
