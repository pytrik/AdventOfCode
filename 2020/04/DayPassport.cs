using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC.Y2020
{
    public abstract class DayPassport : AOC.Y2020.Day<Passport, int>
    {
        public DayPassport(string day) : base(day)
        {
            this.split = new Regex(@"(?:\r\n){2}", RegexOptions.Compiled);
        }

        private static readonly Regex splitInner = new Regex(@"\s+", RegexOptions.Compiled);
        protected override bool TryParseInput()
        {
            this.ParsedInput = this.RawInput
                .Select(p => splitInner
                    .Split(p).Aggregate(new Passport(), (p, i) =>
                    {
                        var item = i.Split(":");
                        p.Add(item[0], item[1]);
                        return p;
                    }))
                .ToArray();

            return true;
        }
    }

    public class Passport : Dictionary<string, string>
    {
        private Dictionary<string, Func<string, bool>> required = new()
        {
            { "byr", (string input) => InputIsIntegerBetween(input, 1920, 2002) },
            { "ecl", IsValidEyeColor },
            { "eyr", (string input) => InputIsIntegerBetween(input, 2020, 2030) },
            { "hcl", IsValidHairColor },
            { "hgt", IsValidHeight },
            { "iyr", (string input) => InputIsIntegerBetween(input, 2010, 2020) },
            { "pid", IsValidPassportID }
        };

        public bool HasRequiredFields()
        {
            return required.All(req => this.ContainsKey(req.Key));
        }

        public bool HasValidValues()
        {
            return this.HasRequiredFields() &&
                   required.All(req => req.Value(this[req.Key]));
        }

        private static readonly Regex height = new Regex(@"(?<value>\d+)(?<unit>(?:cm|in))", RegexOptions.Compiled);
        private static bool IsValidHeight(string input)
        {
            var groups = height.Match(input).Groups;
            if (groups["unit"].Value == "cm")
                return InputIsIntegerBetween(groups["value"].Value, 150, 193);
            else if (groups["unit"].Value == "in")
                return InputIsIntegerBetween(groups["value"].Value, 59, 76);
            return false;
        }

        private static readonly Regex hairColor = new Regex(@"#[0-9a-f]{6}", RegexOptions.Compiled);
        private static bool IsValidHairColor(string input)
        {
            return hairColor.IsMatch(input);
        }

        private static readonly HashSet<string> eyeColors = new() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
        private static bool IsValidEyeColor(string input)
        {
            return eyeColors.Contains(input);
        }

        private static readonly Regex passportId = new Regex(@"[0-9]{9}", RegexOptions.Compiled);
        private static bool IsValidPassportID(string input)
        {
            return passportId.IsMatch(input);
        }

        private static bool InputIsIntegerBetween(string input, int lower, int upper)
        {
            int value;
            if (!int.TryParse(input, out value))
                return false;
            return value >= lower && value <= upper;
        }

        public override string ToString()
        {
            return $"Passport, fields {this.HasRequiredFields()}, values {this.ToCSV()}";
        }

        public string ToCSV()
        {
            var values = new List<string>() { this.HasValidValues().ToString() };
            foreach (var key in required.Select(r => r.Key))
            {
                values.Add(this.ContainsKey(key) ? this[key] : "");
            }
            return string.Join(",", values);
        }
    }
}