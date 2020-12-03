using System.Linq;
using System.Text.RegularExpressions;

namespace AOC.Y2020
{
    public abstract class DayPassword : AOC.Y2020.Day<PasswordCheck, int>
    {
        public DayPassword(string day) : base(day)
        {
        }

        protected override bool TryParseInput()
        {
            this.ParsedInput = this.RawInput.Select(PasswordCheckParser.Parse);
            return true;
        }
    }

    public class PasswordCheck
    {
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }
        public char RequiredCharacter { get; set; }
        public string Password { get; set; }

        public bool IsValidSledRental()
        {
            var charCount = this.Password
                .Count(ch => ch == this.RequiredCharacter);
            return charCount >= this.FirstNumber && charCount <= this.SecondNumber;
        }

        public bool IsValidTobogganCorporate()
        {
            return new[] { this.Password[FirstNumber - 1], this.Password[SecondNumber - 1] }
                .Count(ch => ch == RequiredCharacter) == 1;
        }
    }

    public class PasswordCheckParser
    {
        private static readonly Regex regex = new Regex(@"(?<start>[0-9]+)-(?<end>[0-9]+)\s*(?<char>\w):\s*(?<password>\w+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static PasswordCheck Parse(string input)
        {
            var groups = regex.Match(input).Groups;
            return new PasswordCheck()
            {
                FirstNumber = int.Parse(groups["start"].Value),
                SecondNumber = int.Parse(groups["end"].Value),
                RequiredCharacter = groups["char"].Value[0],
                Password = groups["password"].Value
            };
        }
    }
}