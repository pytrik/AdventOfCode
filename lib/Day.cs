using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC
{
    public abstract class Day<Tin, Tout>
    {
        protected Day(string year, string day)
        {
            if (string.IsNullOrEmpty(year))
            {
                throw new ArgumentException($"'{nameof(year)}' cannot be null or empty", nameof(year));
            }

            if (string.IsNullOrEmpty(day))
            {
                throw new ArgumentException($"'{nameof(day)}' cannot be null or empty", nameof(day));
            }

            this.Year = year;
            this.DayName = day;
        }

        public string Year { get; }
        public string DayName { get; }
        public IEnumerable<string> RawInput { get; protected set; }
        public bool HasParsedInput { get; protected set; }
        public Tin[] ParsedInput { get; protected set; }
        public Tout ExpectedTest1Result { get; set; }
        public Tout ExpectedTest2Result { get; set; }

        public IEnumerable<TestResult<Tout>> RunTests()
        {
            var result = new List<TestResult<Tout>>();

            this.SetInputFrom("input_test1");
            if (this.HasParsedInput)
            {
                var resultPart1 = this.TryRunPart(1);
                result.Add(TestResult<Tout>.Create("Part 1", this.ExpectedTest1Result, resultPart1));
            }

            this.SetInputFrom("input_test2");
            if (this.HasParsedInput)
            {
                var resultPart2 = this.TryRunPart(2);
                result.Add(TestResult<Tout>.Create("Part 2", this.ExpectedTest2Result, resultPart2));
            }

            return result;
        }

        public IEnumerable<RunResult<Tout>> Run()
        {
            this.SetInputFrom("input");
            return new List<RunResult<Tout>>() {
                RunResult<Tout>.Create("Part 1", this.TryRunPart(1)),
                RunResult<Tout>.Create("Part 2", this.TryRunPart(2))
            };
        }

        private Tout TryRunPart(int partNr)
        {
            try
            {
                if (!this.HasParsedInput)
                    throw new NoParsedInputSetException($"No input was set for run of {this} part {partNr}");

                if (partNr == 1)
                    return this.RunPart1();
                else if (partNr == 2)
                    return this.RunPart2();
            }
            catch (NotImplementedException) { }

            return default(Tout);
        }

        protected abstract Tout RunPart1();
        protected abstract Tout RunPart2();

        protected virtual bool TryParseInput()
        {
            var converter = TypeConverter<Tin>();
            if (converter != null)
            {
                this.ParsedInput = this.RawInput.Select(converter).ToArray();
                return true;
            }
            return false;
        }

        protected string GetInputFileContent(string file)
        {
            var path = $"./{this.Year}/{this.DayName}/{file}";
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }

            return null;
        }

        protected Regex split = new Regex(@"(?:\r?\n|\r\n?)", RegexOptions.Compiled);
        protected virtual void SetInputFrom(string file)
        {
            var content = GetInputFileContent(file);
            var success = false;
            if (content != null)
            {
                this.RawInput = split.Split(content)
                    .Where(line => !string.IsNullOrEmpty(line));
                success = TryParseInput();
            }

            this.HasParsedInput = success;
        }

        protected bool IsValueType<T>() => typeof(T).IsValueType;
        protected Func<object, T> TypeConverter<T>()
        {
            if (Nullable.GetUnderlyingType(typeof(T)) != null)
            {
                System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
                return (value) => (T)converter.ConvertFrom(value);
            }
            else if (this.IsValueType<T>())
            {
                return (value) => (T)Convert.ChangeType(value, typeof(T));
            }

            return null;
        }

        public override string ToString()
        {
            return $"AOC {this.Year} day {this.DayName}";
        }
    }
}
