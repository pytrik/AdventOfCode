namespace AOC.Y2020
{
    public class Movement
    {
        public char Move { get; init; }
        public int Amount { get; init; }

        public static Movement Parse(string move)
        {
            return new Movement()
            {
                Move = move[0],
                Amount = int.Parse(move.Substring(1))
            };
        }
    }
}