namespace thunder_streaming_space.Properties
{
    internal class API
    {
        public static string? Endpoint { get; set; }
        public static string? ApiKey { get; set; }
        public static string? Token { get; set; }
        public static string? TokenType { get; set; }
    }

    internal partial class ReturnParam
    {
        public int? Id { get; set; } 
        public string? Token { get; set; }
        public string? TokenType { get; set; }
    }
}
