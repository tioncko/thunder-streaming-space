using thunder_streaming_space.Authentication;
using thunder_streaming_space.Database;
using thunder_streaming_space.JsonObjects;
using thunder_streaming_space.Menu;
using static thunder_streaming_space.Deserializers.ItemMovies;

namespace thunder_streaming_space.Searches
{
    internal class SearchById : Search
    {
        public override void Execute(Client cli, JsonMovies json, DataSet ds)
        {
            base.Execute(cli, json, ds);
            ShowMenu(this.GetType().Name);

            try
            {
                Console.Write("# Informe o id do filme: ");
                string movieId = Console.ReadLine()!;
                bool valid = false;
                int error = 0, count = 0;


                while (!valid)
                {
                    if (error >= 4) {
                        Console.WriteLine("\nNúmero de tentativas excedido.\nPressione qualquer tecla para voltar ao menu principal.");
                        Console.ReadKey();
                        valid = true;
                        Start.Running(cli, ds);
                    }

                    if (count > 0) {
                        Console.Write("\nDeseja pesquisar outro filme pelo seu código? [S/N]: ");
                        string answer = Console.ReadLine()!;
                        string exit = "";

                        do {
                            if (answer.ToUpper().Equals("s".ToUpper())) {
                                Console.Write("## Informe o id do filme: ");
                                movieId = Console.ReadLine()!;
                                exit = "OK";
                            }
                            if (answer.ToUpper().Equals("n".ToUpper())) {
                                Console.Write("Pressione qualquer tecla para voltar ao menu principal ");
                                Console.ReadKey();

                                exit = "OK";
                                valid = true;
                                Start.Running(cli, ds);
                            }
                        } while (exit != "OK");
                    }

                    if (int.TryParse(movieId, out int id) && (int.Parse(movieId) != 0)) {
                        
                        Movies movie = json.GetMovieById(cli, int.Parse(movieId));
                        if (movie.Id != null)
                        {
                            Console.WriteLine("******************************************************************");
                            Console.WriteLine(movie);//1252455));
                            count++;
                        }
                        else
                        {
                            error++;
                            count = 0;
                            if (error < 4) { 
                                Console.Write("Id inválido. Por favor, digite um id válido: ");
                                movieId = Console.ReadLine()!;
                            }
                        }
                        if (movie != null) Console.WriteLine("******************************************************************");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error message: {e.Message}");
            }
        }
    }
}
