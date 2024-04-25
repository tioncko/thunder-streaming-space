using System.Text.Json.Serialization;

namespace thunder_streaming_space.Deserializers
{
    internal class ItemMovies
    {
        internal class Genres
        {
            //https"://api.themoviedb.org/3/genre/movie/list
            [JsonPropertyName("name")]
            public string? Name { get; set; }
        }

        internal class FilmProductionCompanies
        {
            [JsonPropertyName("name")]
            public string? Name { get; set; }

            [JsonPropertyName("origin_country")]
            public string? Country { get; set; }
        }

        internal class Languages
        {
            [JsonPropertyName("english_name")]
            public string? Name { get; set; }
        }

        internal class Movies
        {
            [JsonPropertyName("genres")]
            public List<Genres>? Genres { get; set; }

            [JsonPropertyName("id")]
            public int? Id { get; set; }

            [JsonPropertyName("original_language")]
            public string? OriginalLanguage { get; set; }

            [JsonPropertyName("overview")]
            public string? Overview { get; set; }

            [JsonPropertyName("production_companies")]
            public List<FilmProductionCompanies>? ProductionCompanies { get; set; }

            [JsonPropertyName("release_date")]
            public string? ReleaseDate { get; set; }

            [JsonPropertyName("runtime")]
            public int? Runtime { get; set; }

            [JsonPropertyName("spoken_languages")]
            public List<Languages>? SpokenLanguages { get; set; }

            [JsonPropertyName("title")]
            public string? Title { get; set; }
            
            public string? Page { get; set; }

            public override string ToString()
            {
                return
                   $"Id: {((Id == 0) ? "No data" : Id)}" +
                   $"\nTitle: {((Title == "") ? "No data" : Title)}" +
                   $"\nOriginal Language: {((OriginalLanguage == "") ? "No data" : OriginalLanguage)}" +
                   $"\nOverview: {((Overview == "") ? "No data" : Overview)}" +
                   $"\nRelease Date: {((ReleaseDate == "") ? "No data" : ReleaseDate)}" +
                   $"\nRuntime: {((Runtime == 0) ? "No data" : Runtime + " min.")}" +
                   $"\nGenre: {((Genres!.Count == 0) ? "No data" : String.Join(", ", Genres!.Select(x => x.Name).ToList()))}" +
                   $"\nProduction Company: {((ProductionCompanies!.Count == 0) ? "No data" : String.Join(", ", ProductionCompanies!.Select(x => x.Name).ToList()))}" +
                   $"\nSpoken Languages: {((SpokenLanguages!.Count == 0) ? "No data" : String.Join(", ", SpokenLanguages!.Select(x => x.Name).ToList()))}"; 
                   //$"\nPage: {((Page == "") ? "No data" : Page)}";
            }            
        }
    }
}