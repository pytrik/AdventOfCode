using System.Collections.Generic;

namespace AOC
{
    interface IDay
    {
        IEnumerable<object> RunTests();
        IEnumerable<object> Run();
    }
}