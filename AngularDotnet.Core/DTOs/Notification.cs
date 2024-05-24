namespace AngularDotnet.Core.DTOs
{
    public record Notification
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
