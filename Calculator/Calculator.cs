using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tdd.Calculator
{
    public class Calculator
    {
        public int AddNumbersFromString(string numberList)
        {
            if (string.IsNullOrEmpty(numberList))
                return 0;

            var delimiters = GetDelimiters(ref numberList);

            var numberStrings = numberList.Split(delimiters.ToArray(), StringSplitOptions.None).ToList();

            CheckAllValuesAreNumeric(numberStrings);
            CheckNumbersContainNegatives(numberStrings);

            return numberStrings.Where(n => Convert.ToInt32(n.ToString()) <= 1000).Sum(n => Convert.ToInt32(n.ToString()));
        }

        public List<string> GetDelimiters(ref string numberList)
        {
            var delimiters = new List<string> { ",", "\n" };

            if (!numberList.StartsWith("//"))
                return delimiters;

            const string delimetersPattern = @"^//((\[.+\])+)\n";
            var delimitersRegex = new Regex(delimetersPattern);

            var delimitersMatches = delimitersRegex.Matches(numberList);

            const string delimeterPattern = @"\[(.+?)\]";

            var delimiterRegex = new Regex(delimeterPattern);

            var delimiterMatches = delimiterRegex.Matches(delimitersMatches[0].Groups[1].Value);

            delimiters.AddRange(from Match delimiterMatch in delimiterMatches select delimiterMatch.Groups[1].Value);

            numberList = delimitersRegex.Replace(numberList, "");

            return delimiters;
        }

        public void CheckAllValuesAreNumeric(List<string> numberStrings)
        {
            var nonNumericValues = numberStrings.Where(n => !int.TryParse(n, out int num));

            if (nonNumericValues.Any())
                throw new FormatException("only numeric values supported");
        }

        public void CheckNumbersContainNegatives(List<string> numberStrings)
        {
            var negativeNumberStrings =
                numberStrings.Where(n => Convert.ToInt32(n.ToString()) < 0).ToList();

            var numberCommaSeparatedString = string.Join(",", negativeNumberStrings);

            if (negativeNumberStrings.Any())
                throw new ArgumentException(string.Format("negatives not allowed {0}", numberCommaSeparatedString));
        }
    }
}
