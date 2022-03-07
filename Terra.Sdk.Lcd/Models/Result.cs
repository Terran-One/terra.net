namespace Terra.Sdk.Lcd.Models
{
    public class Result<TEntity>
    {
        /// <summary>
        /// Returned entity.
        /// </summary>
        public TEntity Value { get; set; }

        /// <summary>
        /// If an error has occurred whilst retrieving the model,
        /// the state will be stored here.
        /// </summary>
        public Error Error { get; set; }
    }
}
