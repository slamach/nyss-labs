namespace ParserApp.Models
{
    public class UpdateResult
    {
        public long Id { get; set; }
        public string Field { get; set; }
        public object PreviousValue { get; set; }
        public object CurrentValue { get; set; }

        public UpdateResult(
            long id,
            string field,
            object previousValue,
            object currentValue)
        {
            Id = id;
            Field = field;
            PreviousValue = previousValue;
            CurrentValue = currentValue;
        }
    }
}
