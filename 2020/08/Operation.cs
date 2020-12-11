using System;

namespace AOC.Y2020
{
    public static class Operations
    {
        public static Accumulate Accumulate { get; } = new();
        public static Jump Jump { get; } = new();
        public static NoOperation NoOperation { get; } = new();

        public static IOperation FromString(string op)
        {
            if (op == "acc")
                return Accumulate;
            else if (op == "jmp")
                return Jump;
            else if (op == "nop")
                return NoOperation;
            else
                throw new ArgumentException($"No operation '{op}' exists", nameof(op));
        }
    }

    public interface IOperation
    {
        public void ApplyTo(ProgramRunner runner, int argument);
    }

    public class Accumulate : IOperation
    {
        public void ApplyTo(ProgramRunner runner, int argument)
        {
            runner.Accumulator += argument;
            runner.Cursor += 1;
        }

        public override string ToString() => "Accumulate";
    }

    public class Jump : IOperation
    {
        public void ApplyTo(ProgramRunner runner, int argument)
        {
            runner.Cursor += argument;
        }

        public override string ToString() => "Jump";
    }

    public class NoOperation : IOperation
    {
        public void ApplyTo(ProgramRunner runner, int argument)
        {
            runner.Cursor += 1;
        }

        public override string ToString() => "No Operation";
    }
}