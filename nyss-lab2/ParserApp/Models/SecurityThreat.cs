namespace ParserApp.Models
{
    public class SecurityThreat
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public bool ConfViolated { get; set; }
        public bool IntegViolated { get; set; }
        public bool AccessViolated { get; set; }

        public SecurityThreat(
            long id,
            string name,
            string description,
            string source,
            string target,
            bool confViolated,
            bool integViolated,
            bool accessViolated)
        {
            Id = id;
            Name = name;
            Description = description;
            Source = source;
            Target = target;
            ConfViolated = confViolated;
            IntegViolated = integViolated;
            AccessViolated = accessViolated;
        }
    }
}
