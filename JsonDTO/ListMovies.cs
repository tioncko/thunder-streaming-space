using System.Text.Json.Serialization;

namespace thunder_streaming_space.Lists
{
    internal class ListMovies
    {
        internal class Code
        {
            [JsonPropertyName("id")]
            public int? Id { get; set; }
        }

        internal class Titles
        {
            [JsonPropertyName("results")]
            public List<Code>? Results { get; set; }

            [JsonPropertyName("page")]
            public int? Page { get; set; }

            [JsonPropertyName("total_pages")]
            public int? TotalPages { get; set; }
        }
    }
}
