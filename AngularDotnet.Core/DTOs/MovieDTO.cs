namespace AngularDotnet.Core.DTOs
{
    public record MovieDTO
    {
        public string MovieName { get; set; }
        public string IMDBId { get; set; }
        public string Poster { get; set; }
        public string Year { get; set; }
        public Guid Id { get; set; }

    }
}
