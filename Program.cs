using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AOC.Run
{
    public class Program
    {
        static void Main(string[] args)
        {
            var days = GetDays("AOC.Y2020");
            var execute = new List<Type>();
            if (args.Length == 0)
                execute.Add(days.Last().Value);
            else if (args[0] == "all")
                execute.AddRange(days.Values);
            else
            {
                int dayNr;
                var exists = int.TryParse(args[0], out dayNr);
                exists &= days.ContainsKey(dayNr);
                if (!exists)
                {
                    PrintUsage();
                    return;
                }
                execute.Add(days[dayNr]);
            }

            foreach (var day in execute)
            {
                var instance = (IDay)Activator.CreateInstance(day);
                PrintResults(instance);
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine(@"First argument should be unspecified, literal 'all' or number of an implemented day");
        }

        private static Regex number = new Regex(@"(?<nr>\d+)", RegexOptions.Compiled);
        private static Dictionary<int, Type> GetDays(string ns)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => String.Equals(t.Namespace, ns, StringComparison.Ordinal))
                .Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(IDay)))
                .Where(t => number.IsMatch(t.Name));
            return types
                .ToDictionary(
                    t => int.Parse(number.Match(t.Name).Groups["nr"].Value),
                    t => t);
        }

        private static void PrintResults(IDay day)
        {
            Console.WriteLine(day);
            foreach (var testResult in day.RunTests())
                Console.WriteLine(testResult);
            foreach (var result in day.Run())
                Console.WriteLine(result);
        }
    }
}
