namespace AOC
{
    public class TestResult<T>
    {
        protected TestResult() { }

        public string Name { get; protected set; }
        public T Expected { get; protected set; }
        public T Actual { get; protected set; }

        public bool? Succes => this.Expected?.Equals(this.Actual);

        public override string ToString()
        {
            string state;
            if (!this.Succes.HasValue)
                state = "No result";
            else
                state = this.Succes.Value ? "Success" : "Failure";

            return $"Test result {this.Name}: {state}, expected '{this.Expected}', actual '{this.Actual}'";
        }

        public static TestResult<T> Create(string name, T expected, T actual)
        {
            return new TestResult<T>()
            {
                Name = name,
                Expected = expected,
                Actual = actual
            };
        }
    }
}