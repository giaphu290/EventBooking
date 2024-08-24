using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Domain.Private
{
    public static class PasswordGenerator
    {
        private static readonly Random Random = new();
        private static string? Uppercase;
        private static string? Lowercase;
        private static string? Digits;
        public static void Initialize(IConfiguration configuration)
        {
            Uppercase = configuration["PasswordSettings:UppercaseChars"] ?? "";
            Lowercase = configuration["PasswordSettings:LowercaseChars"] ?? "";
            Digits = configuration["PasswordSettings:DigitChars"] ?? "";
        }

        public static string Generate(int length)
        {
            if (length < 6) throw new ArgumentException("Password length should be at least 6 characters.");

            var allChars = Uppercase + Lowercase + Digits;
            var password = new StringBuilder();

            // Check password format
            password.Append(Uppercase[Random.Next(Uppercase.Length)]);
            password.Append(Lowercase[Random.Next(Lowercase.Length)]);
            password.Append(Digits[Random.Next(Digits.Length)]);

            // Fill the password length with random characters
            for (int i = 3; i < length; i++)
            {
                password.Append(allChars[Random.Next(allChars.Length)]);
            }

            // Randomize password
            return new string(password.ToString().ToCharArray().OrderBy(c => Random.Next()).ToArray());
        }
    }
}
