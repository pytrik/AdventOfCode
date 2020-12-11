namespace AOC.Y2020
{
    public class Instruction
    {
        public IOperation Operation { get; set; }
        public int Argument { get; set; }

        public static Instruction Parse(string input)
        {
            var parts = input.Split(' ');
            return new Instruction()
            {
                Operation = Operations.FromString(parts[0]),
                Argument = int.Parse(parts[1])
            };
        }

        public override string ToString() => $"{this.Operation} {this.Argument}";
    }
}
