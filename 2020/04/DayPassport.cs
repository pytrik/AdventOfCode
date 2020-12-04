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
        private static List<PassportField> fields = new()
        {
            new PassportBirthYear(),
            new PassportIssueYear(),
            new PassportExpirationYear(),
            new PassportHeight(),
            new PassportHairColor(),
            new PassportEyeColor(),
            new PassportID(),
            new PassportCountryID()
        };

        public bool HasRequiredFields()
        {
            return fields
                .Where(field => field.IsRequired)
                .All(field => this.ContainsKey(field.Key));
        }

        public bool HasValidFields()
        {
            return fields.All(field => field.IsValid(this));
        }

        public override string ToString()
        {
            return $"Passport, fields {this.HasRequiredFields()}, values {this.ToCSV()}";
        }

        private static string BoolAsString(bool input) => input ? "WAAR" : "ONWAAR";
        public string ToCSV()
        {
            var values = new List<string>() {
                BoolAsString(this.HasRequiredFields()),
                BoolAsString(this.HasValidFields())
            };
            foreach (var field in fields)
                values.AddRange(new[] { this.ContainsKey(field.Key) ? this[field.Key] : "", BoolAsString(field.IsValid(this)) });
            return string.Join(",", values);
        }
    }
}