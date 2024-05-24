namespace AngularDotnet.Core
{
    public record ConfigOptions
    {
        public const string DefaultAccount = "DefaultAccount";
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string PasswordKey { get; set; }
        public required IEnumerable<string> Roles { get; set; }

    }
}
