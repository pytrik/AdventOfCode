using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC
{
    public enum SplitInput
    {
        ByNewLine,
        ByEmptyLine
    }

    public abstract class Day<Tin, Tout> : IDay
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
        public Tin ParsedInput { get; protected set; }
        public Tout ExpectedTest1Result { get; set; }
        public Tout ExpectedTest2Result { get; set; }

        public IEnumerable<TestResult<Tout>> RunTests()
        {
            var result = new List<TestResult<Tout>>();

            this.SetInputFrom("input_test1");
            var resultPart1 = this.TryRunPart(1);
            result.Add(TestResult<Tout>.Create("Part 1", this.ExpectedTest1Result, resultPart1));

            this.SetInputFrom("input_test2");
            var resultPart2 = this.TryRunPart(2);
            result.Add(TestResult<Tout>.Create("Part 2", this.ExpectedTest2Result, resultPart2));

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

        IEnumerable<object> IDay.RunTests()
        {
            return this.RunTests();
        }

        IEnumerable<object> IDay.Run()
        {
            return this.Run();
        }

        private Tout TryRunPart(int partNr)
        {
            try
            {
                if (this.ParsedInput == null)
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

        protected virtual Tin ParseInput(IEnumerable<string> raw)
        {
            var converter = StringConverter.ToType<Tin>();
            return converter(raw);
        }

        protected void WriteOutputFile(string file, string data)
        {
            var path = $"./{this.Year}/{this.DayName}/{file}";
            File.WriteAllText(path, data);
        }

        protected string[] GetInputFileLines(string file)
        {
            var path = $"./{this.Year}/{this.DayName}/{file}";
            if (File.Exists(path))
            {
                return File.ReadAllLines(path);
            }

            return null;
        }

        protected SplitInput InputFormat { get; set; } = SplitInput.ByNewLine;
        protected void SetInputFrom(string file)
        {
            var lines = this.GetInputFileLines(file);
            IEnumerable<string> raw = null;
            if (lines != null)
            {
                if (this.InputFormat == SplitInput.ByNewLine)
                    raw = lines;
                else if (this.InputFormat == SplitInput.ByEmptyLine)
                    raw = lines
                        .Aggregate(new List<List<string>>() { new List<string>() }, (list, line) =>
                        {
                            if (string.IsNullOrWhiteSpace(line))
                                list.Add(new List<string>());
                            else
                                list.Last().Add(line);
                            return list;
                        })
                        .Select(lines => string.Join(Environment.NewLine, lines));

                raw = raw.Where(line => !string.IsNullOrEmpty(line));
                this.ParsedInput = this.ParseInput(raw);
            }
        }

        public override string ToString()
        {
            return $"AOC {this.Year} day {this.DayName}";
        }
    }
}
