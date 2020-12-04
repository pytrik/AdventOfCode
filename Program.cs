using System;
using System.Linq;

namespace AOC.Run
{
    public class Program
    {
        static void Main(string[] args)
        {
            var day = new AOC.Y2020.Day04();
            Console.WriteLine(day);
            foreach (var testResult in day.RunTests())
                Console.WriteLine(testResult);
            foreach (var result in day.Run())
                Console.WriteLine(result);
        }
    }
}
