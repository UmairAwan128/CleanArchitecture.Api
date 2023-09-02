namespace CleanArchitecture.Application.Common.Extensions
{
    using BCrypt = BCrypt.Net.BCrypt;

    public static class EncryptionHelper
    {
        public static string WithBCrypt(this string text)
        {
            var result = BCrypt.HashPassword(text);
            return result;
        }

        public static bool VerifyWithBCrypt(this string hashedPassword, string plainText)
        {
            var result = BCrypt.Verify(plainText, hashedPassword);
            return result;
        }
    }
}
