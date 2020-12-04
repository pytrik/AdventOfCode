using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AOC.Y2020
{
    public abstract class PassportField
    {
        protected PassportField(string key, string name, bool isRequired = true)
        {
            this.Key = key ?? throw new ArgumentNullException(nameof(key));
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            IsRequired = isRequired;
        }

        public string Key { get; }
        public string Name { get; }
        public bool IsRequired { get; }
        public abstract bool IsValid(string input);

        public bool IsValid(Passport passport)
        {
            return passport.ContainsKey(this.Key) &&
                   this.IsValid(passport[this.Key]);
        }

        public static bool InputIsIntegerBetween(string input, int lower, int upper)
        {
            int value;
            if (!int.TryParse(input, out value))
                return false;
            return value >= lower && value <= upper;
        }
    }

    public class PassportBirthYear : PassportField
    {
        public PassportBirthYear() : base("byr", "Birth Year") { }

        public override bool IsValid(string input)
        {
            return PassportField.InputIsIntegerBetween(input, 1920, 2002);
        }
    }

    public class PassportIssueYear : PassportField
    {
        public PassportIssueYear() : base("iyr", "Issue Year") { }

        public override bool IsValid(string input)
        {
            return PassportField.InputIsIntegerBetween(input, 2010, 2020);
        }
    }

    public class PassportExpirationYear : PassportField
    {
        public PassportExpirationYear() : base("eyr", "Expiration Year") { }

        public override bool IsValid(string input)
        {
            return PassportField.InputIsIntegerBetween(input, 2020, 2030);
        }
    }

    public class PassportHeight : PassportField
    {
        public PassportHeight() : base("hgt", "Height") { }

        private static readonly Regex height = new Regex(@"(?<value>\d+)(?<unit>(?:cm|in))", RegexOptions.Compiled);
        public override bool IsValid(string input)
        {
            var groups = height.Match(input).Groups;
            if (groups["unit"].Value == "cm")
                return InputIsIntegerBetween(groups["value"].Value, 150, 193);
            else if (groups["unit"].Value == "in")
                return InputIsIntegerBetween(groups["value"].Value, 59, 76);
            return false;
        }
    }

    public class PassportHairColor : PassportField
    {
        public PassportHairColor() : base("hcl", "Hair Color") { }

        private static readonly Regex hairColor = new Regex(@"#[0-9a-f]{6}", RegexOptions.Compiled);
        public override bool IsValid(string input)
        {
            return hairColor.IsMatch(input);
        }
    }

    public class PassportEyeColor : PassportField
    {
        public PassportEyeColor() : base("ecl", "Eye Color") { }

        private static readonly HashSet<string> eyeColors = new() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
        public override bool IsValid(string input)
        {
            return eyeColors.Contains(input);
        }
    }

    public class PassportID : PassportField
    {
        public PassportID() : base("pid", "Passport ID") { }

        private static readonly Regex passportId = new Regex(@"[0-9]{9}", RegexOptions.Compiled);
        public override bool IsValid(string input)
        {
            return passportId.IsMatch(input);
        }
    }

    public class PassportCountryID : PassportField
    {
        public PassportCountryID() : base("cid", "Country ID", false) { }

        public override bool IsValid(string input)
        {
            return true;
        }
    }
}