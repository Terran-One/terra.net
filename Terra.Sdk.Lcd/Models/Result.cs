namespace Terra.Sdk.Lcd.Models
{
    public readonly struct Result<T> where T: class
    {
        public Result(T value, Pagination pagination)
        {
            Value = value;
            Pagination = pagination;
            Error = null;
        }

        public Result(string error)
        {
            Value = null;
            Pagination = null;
            Error = error;
        }

        /// <summary>
        /// Model value, or null if there was an error.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Pagination details for retrieving the next page.
        /// </summary>
        public Pagination Pagination { get; }

        /// <summary>
        /// If an error has occurred whilst retrieving the model,
        /// the state will be stored here.
        /// </summary>
        public string Error { get; }
    }
}
