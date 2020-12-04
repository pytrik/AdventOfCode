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

        public string ToCSV()
        {
            var values = new List<string>() {
                this.HasRequiredFields().ToString(),
                this.HasValidFields().ToString()
            };
            foreach (var key in fields.Select(f => f.Key))
            {
                values.Add(this.ContainsKey(key) ? this[key] : "");
            }
            return string.Join(",", values);
        }
    }
}