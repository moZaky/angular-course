namespace AngularDotnet.Core.Entities
{
    public class Movies
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string MovieName { get; set; }
        public string IMDBId { get; set; }
        public string Poster { get; set; }
        public string Year { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;
    }
}
