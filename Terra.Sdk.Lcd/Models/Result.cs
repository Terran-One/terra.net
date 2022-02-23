using Terra.Sdk.Lcd.Dto;

namespace Terra.Sdk.Lcd.Models
{
    public struct Result<T> where T: class
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

        public T Value { get; }
        public Pagination Pagination { get; }
        public string Error { get; }
    }
}