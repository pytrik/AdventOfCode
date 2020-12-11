using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        public Tin[] ParsedInput { get; protected set; }
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

        private Tout TryRunPart(int partNr)
        {
            try
            {
                if (this.ParsedInput == null || this.ParsedInput.Length == 0)
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

        protected virtual void SetParsedInput()
        {
            var converter = TypeConverter<Tin>();
            this.ParsedInput = this.RawInput.Select(converter).ToArray();
        }

        protected void WriteOutputFile(string file, string data)
        {
            var path = $"./{this.Year}/{this.DayName}/{file}";
            File.WriteAllText(path, data);
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
            if (content != null)
            {
                this.RawInput = split.Split(content)
                    .Where(line => !string.IsNullOrEmpty(line));
                SetParsedInput();
            }
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
            else
            {
                var type = typeof(T);
                var converterMethod = type.GetMethods(BindingFlags.Static | BindingFlags.Public)
                    .Where(method => method.Name == "Parse")
                    .SingleOrDefault(method =>
                    {
                        var parameters = method.GetParameters();
                        return parameters.Length == 1 && parameters.First().ParameterType == typeof(string);
                    });

                if (converterMethod == null)
                    throw new NoConverterFoundException($"Could not find a public static method on type '{type}' with name 'Parse' that accepts exactly 1 argument of type string");

                return (value) => (T)converterMethod.Invoke(null, new[] { value });
            }
        }

        public override string ToString()
        {
            return $"AOC {this.Year} day {this.DayName}";
        }
    }
}
