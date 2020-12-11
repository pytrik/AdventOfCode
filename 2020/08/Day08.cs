using System;
using System.Linq;
using System.Collections.Generic;

namespace AOC.Y2020
{
    public class Day08 : Day<Instruction, int>
    {
        public Day08() : base("08")
        {
            this.ExpectedTest1Result = 5;
            this.ExpectedTest2Result = 8;
        }

        protected override bool TryParseInput()
        {
            this.ParsedInput = this.RawInput.Select(Instruction.FromString).ToArray();
            return true;
        }

        protected override int RunPart1()
        {
            var runner = new ProgramRunner(this.ParsedInput);
            try
            {
                runner.All();
            }
            catch (ProgramHasInfiniteLoopException ex)
            {
                return (int)ex.AccumulatorValue;
            }
            return 0;
        }

        protected override int RunPart2()
        {
            var runner = new ProgramRunner(this.ParsedInput);
            try
            {
                runner.All();
            }
            catch (ProgramHasInfiniteLoopException)
            {
                var fixableInstructions = runner.History
                    .Select(i => ((int index, Instruction instruction))(i, runner.Program[i]))
                    .Where(kvp => kvp.instruction.Operation == Operations.Jump || kvp.instruction.Operation == Operations.NoOperation)
                    .ToArray();

                for (var i = fixableInstructions.Length - 1; i >= 0; i--)
                {
                    var program = (Instruction[])this.ParsedInput.Clone();
                    var instructionToFix = fixableInstructions[i];
                    program[instructionToFix.index] = new Instruction() { Argument = instructionToFix.instruction.Argument };

                    if (program[instructionToFix.index].Operation == Operations.NoOperation)
                        program[instructionToFix.index].Operation = Operations.Jump;
                    else
                        program[instructionToFix.index].Operation = Operations.NoOperation;

                    try
                    {
                        runner = new ProgramRunner(program);
                        return (int)runner.All();
                    }
                    catch (ProgramHasInfiniteLoopException) { }
                }
            }
            return 0;
        }
    }
}