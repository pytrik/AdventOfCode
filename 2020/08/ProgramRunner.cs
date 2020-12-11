using System;
using System.Collections.Generic;

namespace AOC.Y2020
{
    public class ProgramRunner
    {
        public ProgramRunner(Instruction[] program)
        {
            this.Program = program ?? throw new ArgumentNullException(nameof(program));
            this.Reset();
        }

        public Instruction[] Program { get; }
        public long Accumulator { get; set; }
        public long Cursor { get; set; }
        public HashSet<long> History { get; private set; }

        public void Reset()
        {
            this.Accumulator = 0;
            this.Cursor = 0;
            this.History = new HashSet<long>();
        }

        public void Single()
        {
            var state = this.Cursor;
            if (this.History.Contains(this.Cursor))
            {
                var ex = new ProgramHasInfiniteLoopException($"Detected an infinite loop, the position {this.Cursor} has already been visited, accumulator had value {this.Accumulator}");
                ex.AccumulatorValue = this.Accumulator;
                throw ex;
            }
            this.History.Add(state);

            var instruction = this.Program[this.Cursor];
            instruction.Operation.ApplyTo(this, instruction.Argument);
        }

        public long All()
        {
            while (this.Cursor >= 0 && this.Cursor < Program.Length)
            {
                if (this.Cursor == this.Program.Length)
                    break;
                this.Single();
            }
            return this.Accumulator;
        }
    }
}
