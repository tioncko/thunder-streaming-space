using thunder_streaming_space.Authentication;
using thunder_streaming_space.Database;
using thunder_streaming_space.JsonObjects;
using thunder_streaming_space.Menu;
using static thunder_streaming_space.Deserializers.ItemMovies;
using static thunder_streaming_space.Lists.ListMovies;

namespace thunder_streaming_space.Searches
{
    internal class Search
    {
        public void ShowMenu(string title)
        {
            int len = title.Length;
            Console.WriteLine(String.Empty.PadLeft(len, '*'));
            Console.WriteLine(title);
            Console.WriteLine(String.Empty.PadLeft(len, '*') + "\n");
        }
        //virtual method is an abstract method that is declared in a base class and can be overridden in derived class
        public virtual void Execute(Client cli, JsonMovies json, DataSet ds) {
            Console.Clear();
        }

        public bool MovieDetails(Client cli, JsonMovies json, DataSet ds)
        {
            string exit = "";
            do
            {
                Console.Write("\nVer detalhes de um filme em específico? [S/N]: ");
                string answer = Console.ReadLine()!;
                if (answer.ToUpper().Equals("s".ToUpper()))
                {
                    SearchById sbi = new SearchById();
                    sbi.Execute(cli, json, ds);
                    exit = "OK";
                }
                if (answer.ToUpper().Equals("n".ToUpper()))
                {
                    Console.Write("Pressione qualquer tecla para voltar ao menu principal ");
                    Console.ReadKey();

                    exit = "OK";
                    Start.Running(cli, ds);
                }
            } while (exit != "OK");
            return true;
        }

        public List<Movies> ListMovies(Client cli, JsonMovies json, DataSet ds) {

            List<Movies> listMovies;
            if (ds.Select().Count == 0)
            {   
                Console.WriteLine("Digite as páginas que deseja visualizar as informações desejadas");
                Console.WriteLine($"=> Paginas disponíveis: {json.CountPageFilms(cli)}");

                Console.Write(" - Primeira página: ");
                string stPage = Console.ReadLine()!;
                Console.Write(" - Última página: ");
                string ltPage = Console.ReadLine()!;

                Console.WriteLine("Iniciando coleta dos dados...");
                stPage = ((int.Parse(stPage) <= 0) ? "1" : stPage);
                ltPage = ((int.Parse(ltPage) < (int.Parse(stPage))) ? stPage : ltPage);

                Console.Clear();
                listMovies = new List<Movies>();
                for (int i = int.Parse(stPage); i <= int.Parse(ltPage); i++)
                {
                    foreach (var item in json.GetMovies(cli, Convert.ToString(i)))
                    {
                        listMovies.Add(item);
                    }
                }

                Console.Write("\nDeseja salvar os filmes no banco de dados? [S/N]: ");
                string answer = Console.ReadLine()!;
                if (answer.ToUpper().Equals("s".ToUpper()))
                {
                    foreach (var item in listMovies)
                    {
                        ds.Insert(item);
                    }
                    Console.WriteLine("\nFilmes salvos no banco de dados.\n");
                    listMovies = ds.Select();
                }
            }
            else
            {
                listMovies = ds.Select();
            }
            return listMovies;
        }

        public void ShowMovies(Movies movie)
        {
            Console.WriteLine(
                $"Id: {((movie.Id == 0) ? "No data" : movie.Id)}" +
                $"\nTitle: {((movie.Title == "") ? "No data" : movie.Title)}" +
                $"\nProduction Company: {((movie.ProductionCompanies!.Count == 0) ? "No data" : String.Join(", ", movie.ProductionCompanies!.Select(x => x.Name).ToList()))}" +
                $"\nGenre: {((movie.Genres!.Count == 0) ? "No data" : String.Join(", ", movie.Genres!.Select(x => x.Name).ToList()))}" +
                $"\nRelease Date: {((movie.ReleaseDate == "") ? "No data" : movie.ReleaseDate)}");
        }

        public void ReleaseDB(DataSet ds)
        {
            if (ds.Select().Count > 0)
            {
                Console.Write("\nInformar novos dados para o banco? [S/N]: ");
                string answer = Console.ReadLine()!;

                if (answer.ToUpper().Equals("s".ToUpper()))
                {
                    ds.Delete();
                }
            }
        }
    }
}
