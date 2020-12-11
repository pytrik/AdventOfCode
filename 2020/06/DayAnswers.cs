using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC.Y2020
{
    public class Answers : Dictionary<char, int>
    {
        public int NumberOfPeople { get; set; }
        private static readonly Regex splitInner = new Regex(@"\s+", RegexOptions.Compiled);
        public static Answers Parse(string input)
        {
            var rows = splitInner.Split(input);
            return rows
                .Aggregate(new Answers() { NumberOfPeople = rows.Count() }, (answers, row) =>
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