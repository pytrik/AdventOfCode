namespace AOC
{
    public class RunResult<T>
    {
        protected RunResult() { }

        public string Name { get; protected set; }
        public T Result { get; protected set; }

        public override string ToString()
        {
            return $"Run result {this.Name}: {this.Result}";
        }

        public static RunResult<T> Create(string name, T result)
        {
            return new RunResult<T>()
            {
                Name = name,
                Result = result
            };
        }
    }
}