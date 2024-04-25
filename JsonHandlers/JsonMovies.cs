using System.Text.Json;
using thunder_streaming_space.Authentication;
using thunder_streaming_space.Database;
using static thunder_streaming_space.Deserializers.ItemMovies;
using static thunder_streaming_space.Lists.ListMovies;

namespace thunder_streaming_space.JsonObjects
{
    internal class JsonMovies
    {
        private DataSet DS;

        public JsonMovies(DataSet banco) { 
            this.DS = banco; 
        }

        /*
         * Retorna a quantidade de paginas de filmes disponiveis na API
         */
        public int CountPageFilms(Client cli)
        {
            string route = "/3/movie/changes?page=1?";
            Titles json = JsonSerializer.Deserialize<Titles>(cli.GetBearerToken(route))!;
            return (int)json.TotalPages!;
        }

        /*
         * Retorna todos os IDs de filmes disponiveis na API
         */
        public List<Code> ListAllFilms(Client cli)
        {
            string route;
            int ct = CountPageFilms(cli);
            List<Code> list = new List<Code>();

            for (int i = 1; i <= ct; i++)
            {
                route = $"/3/movie/changes?page={i}?";
                Titles json = JsonSerializer.Deserialize<Titles>(cli.GetBearerToken(route))!;
                //list.AddRange(json.Results!);
                foreach (Code c in json.Results!)
                {
                    list.Add(c);
                }
            }
            return list;
        }

        /*
         * Retorna todos os IDs de filmes disponiveis por página na API
         */
        public List<Code> ListIdFilmsByPage(Client cli, int page)
        {
            List<Code> list = new List<Code>();
            string route = $"/3/movie/changes?page={page}?";
            Titles json = JsonSerializer.Deserialize<Titles>(cli.GetBearerToken(route))!;

            foreach (Code id in json.Results!)
            {
                list.Add(id);
            }
            return list;
        }

        /*
         * Retorna todos os filmes exisentes na API (todas as páginas ou por página individual)
         */
        public List<Movies> GetMovies(Client cli, string param)
        {
            string route;
            List<Movies> movies = new List<Movies>();
            List<Code> moviesId = (param.Equals("all".ToUpper()) || param.Equals("all".ToLower())) ? ListAllFilms(cli) 
                                : (int.TryParse(param, out int pageId)) ? ListIdFilmsByPage(cli, int.Parse(param)) : new List<Code>();

            int c = 0;
            Console.WriteLine($"\nPágina {param}\n");

            try { 
                foreach (Code id in moviesId)
                {
                    route = $"/3/movie/{id.Id}";
                    string bearer = cli.GetBearerToken(route);

                    if (!bearer.Substring(0, 3).Equals("404"))
                    {
                        c++;
                        if (c % 25 == 0 && c != moviesId.Count) Console.WriteLine($"{c}% dos registros foram identificados.");

                        Movies json = JsonSerializer.Deserialize<Movies>(bearer)!;
                        json.Page = param;
                        movies.Add(json);
                        //DS.Insert(json);
                    }
                    
                }
                if (c == moviesId.Count) Console.WriteLine($"{c}% dos registros foram identificados.");
                else Console.WriteLine($"{c}% dos registros foram identificados.");
                Console.Clear();
            } catch (Exception e) { Console.WriteLine(e.Message); }
            
            return movies;
            //return DS.Select();
        }

        public void SaveInDB(Movies movie) {
            if (DS.Select().Count == 0) DS.Insert(movie);
        }

        /*
         * Retorna um filme pelo seu id
         */
        public Movies GetMovieById(Client cli, int id) { 
        
            string route = $"/3/movie/{id}";
            string bearer = cli.GetBearerToken(route);
            Movies json = new Movies();

            if (!bearer.Substring(0, 3).Equals("404"))
            {
                json = JsonSerializer.Deserialize<Movies>(bearer)!;
            }
            return json;
        }
    }
}


