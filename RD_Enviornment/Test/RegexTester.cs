
using System.Text.RegularExpressions;

namespace RD_Enviornment.Test
{
    internal class RegexTester
    {
        private static bool isMatch;
        public RegexTester IsMatch(string s, string p)
        {
            // First check if length between 1 - 20
            // Check if only is lower
            var lowerAndBetweenPattern = "(^[a-z.*]{0,20}$)";

            if (!new Regex(lowerAndBetweenPattern).IsMatch(s))
                return this;
            if (!new Regex(lowerAndBetweenPattern).IsMatch(p))
                return this;

            var pattern = p;
            var regex = new Regex(pattern);

            isMatch = regex.IsMatch(s);

            return this;
        }

        public RegexTester ConsoleThis()
        {
            Console.WriteLine(isMatch);
            return this;
        }
    }
}
