using thunder_streaming_space.Authentication;
using thunder_streaming_space.Database;
using thunder_streaming_space.JsonObjects;
using thunder_streaming_space.Searches;
using static thunder_streaming_space.Deserializers.ItemMovies;

namespace thunder_streaming_space.Menu
{
    internal class SearchByTitles : Search
    {
        public override void Execute(Client cli, JsonMovies json, DataSet ds)
        {
            base.Execute(cli, json, ds);
            ReleaseDB(ds);
            List<Movies> listMovies = ListMovies(cli, json, ds);

            ShowMenu(this.GetType().Name);
            Movies movie = new Movies();
            foreach (var item in listMovies) {
                movie.Id = item.Id;
                movie.Title = item.Title;
                movie.ProductionCompanies = item.ProductionCompanies;
                movie.Genres = item.Genres;
                movie.ReleaseDate = item.ReleaseDate;
                Console.WriteLine("******************************************************************");
                ShowMovies(movie);
            }
            Console.WriteLine("******************************************************************");

            MovieDetails(cli, json, ds);
        }
    }
}

#region Old

/*Console.Write("\nVer detalhes de um filme em específico? [S/N]: ");
//string answer = Console.ReadLine()!;
//string exit = "";

//do
//{
//    if (answer.ToUpper().Equals("s".ToUpper()))
//    {
//        SearchById sbi = new SearchById(); 
//        sbi.Execute(cli, json, ds);
//        exit = "OK";
//    }
//    if (answer.ToUpper().Equals("n".ToUpper()))
//    {
//        Console.Write("Pressione qualquer tecla para voltar ao menu principal ");
//        Console.ReadKey();

//        exit = "OK";
//        Start.Running(cli, ds);
//    }
//} while (exit != "OK");*/
//public virtual void ShowMovies(Movies movie) {
//    Console.WriteLine(
//    $"Title: {movie.Title}" +
//    $"\nProduction Company: {String.Join(", ", movie.ProductionCompanies!.Select(x => x.Name).ToList())}" +
//    $"\nGenre: {String.Join(", ", movie.Genres!.Select(x => x.Name).ToList())}" +
//    $"\nRelease Date: {movie.ReleaseDate}");
//}




//if (ds.Select().Count == 0)
//{
//    Console.WriteLine("Digite as páginas que deseja visualizar");
//    Console.Write("Primeira página: ");
//    string firstpage = Console.ReadLine()!;
//    Console.Write("Última página: ");
//    string lastpage = Console.ReadLine()!;
//}

//if (ds.Select().Count == 0)
//{
//    listMovies = new List<Movies>();
//    for (int i = 1; i <= int.Parse(lastpage); i++)
//    {
//        foreach (var item in json.GetMovies(cli, Convert.ToString(i)))
//        {
//            listMovies.Add(item);
//        }
//    }

//    Console.Write("\nDeseja salvar os filmes no banco de dados? [S/N]: ");
//    string answer = Console.ReadLine()!;
//    if (answer.ToUpper().Equals("s".ToUpper()))
//    {
//        foreach (var item in listMovies)
//        {
//            ds.Insert(item);
//        }
//        listMovies = ds.Select();
//    }
//}
//else
//{
//    listMovies = ds.Select();
//}

///pergunta se o db esta vazio, se sim, trás a lista e pergunta se quer salvar no banco, se sim, salva no banco, se não, pega a lista.

#endregion
