using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC.Y2020
{
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

        private static readonly Regex splitInner = new Regex(@"\s+", RegexOptions.Compiled);
        public static Passport Parse(string input)
        {
            return splitInner.Split(input)
                .Aggregate(new Passport(), (p, i) =>
                {
                    var item = i.Split(":");
                    p.Add(item[0], item[1]);
                    return p;
                });
        }
    }
}