using Practical_Assignment.Entities;
using Practical_Assignment.Interface;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Practical_Assignment.Repository
{
    public class PhoneRepo : IPhone
    {
        public PhoneRepo()
        {

        }
        private static readonly Dictionary<string, string> NumberMap = new Dictionary<string, string>
    {
        {"ZERO", "0"}, {"ONE", "1"}, {"TWO", "2"}, {"THREE", "3"}, {"FOUR", "4"},
        {"FIVE", "5"}, {"SIX", "6"}, {"SEVEN", "7"}, {"EIGHT", "8"}, {"NINE", "9"},
        {"शून्य", "0"}, {"एक", "1"}, {"दो", "2"}, {"तीन", "3"}, {"चार", "4"},
        {"पांच", "5"}, {"छह", "6"}, {"सात", "7"}, {"आठ", "8"}, {"नौ", "9"}
    };
        private static string NormalizeInput(string input)
        {
            foreach (var pair in NumberMap)
            {
                input = Regex.Replace(input, pair.Key, pair.Value, RegexOptions.IgnoreCase);
            }
            return Regex.Replace(input, @"\s+", "");
        }
        //private static bool ContainsPhoneNumber(string input)
        //{
        //    string normalizedInput = NormalizeInput(input);
        //    string pattern = @"(\+?\d{1,4}[-.\s]?)?(\(?\d{1,4}\)?[-.\s]?)?(\d{1,4}[-.\s]?){2,4}\d{1,4}";
        //    return Regex.IsMatch(normalizedInput, pattern);
        //}

        public Dictionary<string, string> DetectPhone(string input)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(input))
            {
                string normalizedInput = NormalizeInput(input);
                string pattern = @"(\+?\d{1,4}[-.\s]?)?(\(?\d{1,4}\)?[-.\s]?)?(\d{1,4}[-.\s]?){2,4}\d{1,4}";

                MatchCollection matches = Regex.Matches(normalizedInput, pattern);

                foreach (Match match in matches)
                {
                    string phoneNumber = match.Value;
                    string format = DetermineFormat(phoneNumber);
                    result.Add(phoneNumber, format);
                }
            }

            return result;
        }
        private static string DetermineFormat(string phoneNumber)
        {
            if (phoneNumber.Length == 10)
            {
                return "10-digit";
            }
            else if (phoneNumber.StartsWith("+"))
            {
                return "Country code";
            }
            else if (phoneNumber.Contains("(") && phoneNumber.Contains(")"))
            {
                return "With parentheses";
            }
            else
            {
                return "Other format";
            }
        }
    }
}
