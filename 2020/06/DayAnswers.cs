using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC.Y2020
{
    public abstract class DayAnswers : AOC.Y2020.Day<Answers, int>
    {
        public DayAnswers(string day) : base(day)
        {
            this.split = new Regex(@"(?:\r\n){2}", RegexOptions.Compiled);
        }

        private static readonly Regex splitInner = new Regex(@"\s+", RegexOptions.Compiled);
        protected override bool TryParseInput()
        {
            this.ParsedInput = this.RawInput.Select(Answers.FromString).ToArray();
            return true;
        }
    }

    public class Answers : Dictionary<char, int>
    {
        public int NumberOfPeople { get; set; }
        private static readonly Regex splitInner = new Regex(@"\s+", RegexOptions.Compiled);
        public static Answers FromString(string input)
        {
            var rows = splitInner.Split(input);
            return rows
                .Aggregate(new Answers() { NumberOfPeople = rows.Count() } , (answers, row) =>
                {
                    foreach (var ch in row)
                        if (answers.ContainsKey(ch))
                            answers[ch]++;
                        else
                            answers.Add(ch, 1);
                    return answers;
                });
        }
    }
}