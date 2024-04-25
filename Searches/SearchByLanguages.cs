using thunder_streaming_space.Authentication;
using thunder_streaming_space.Database;
using thunder_streaming_space.JsonObjects;
using static thunder_streaming_space.Deserializers.ItemMovies;
namespace thunder_streaming_space.Searches
{
    internal class SearchByLanguages : Search
    {
        public override void Execute(Client cli, JsonMovies json, DataSet ds)
        {
            base.Execute(cli, json, ds);

            ReleaseDB(ds);
            List<Movies> listMovies = ListMovies(cli, json, ds);

            ShowMenu(this.GetType().Name);
            var result = listMovies.SelectMany(x => x.SpokenLanguages!).ToList()
                                 .Select(x => new { x.Name })
                                 .GroupBy(x => x.Name)
                                 .OrderBy(x => x.Key)
                                 .ToList();

            int key = 1;
            Dictionary<int, string> typeLanguage = new Dictionary<int, string>();
            foreach (var x in result) {
                typeLanguage.Add(key, x.Key!);
                Console.WriteLine($"Idioma: {x.Key} | Quantidade: {x.Count()}");
                key++;
            }

            string exit = "";
            do
            {
                Console.Write("\nVerificar filmes de um idioma específico? [S/N]: ");
                string answer = Console.ReadLine()!;

                if (answer.ToUpper().Equals("s".ToUpper()))
                {
                    Console.WriteLine();
                    bool valid = false;

                    Console.WriteLine("## Gêneros\n");
                    foreach (var j in typeLanguage)
                    {
                        Console.WriteLine($"{j.Key} - {j.Value}");
                    }

                    int cnt = 0;
                    while (!valid)
                    {
                        if (cnt > 0) Console.Write("Idioma não encontrado. Informar o código do idioma para pesquisa: ");
                        else Console.Write("\nInformar o código do idioma para pesquisa: ");
                        int type = int.Parse(Console.ReadLine()!);

                        if (typeLanguage.ContainsKey(type))
                        {
                            var itm = listMovies.Select(item => item)
                                                .Where(gr => gr.SpokenLanguages!.Any(x => x.Name == typeLanguage[type]))
                                                .ToList();
                            Console.WriteLine();
                            foreach (var item in itm)
                            {
                                Console.WriteLine("******************************************************************");
                                ShowMovies(item);
                            }
                            Console.WriteLine("******************************************************************");
                            cnt = 0;
                        }
                        else cnt++;

                        if (cnt == 0)
                        {
                            Console.Write("\nDeseja pesquisar outro idioma? [S/N]: ");
                            string ans = Console.ReadLine()!;

                            if (ans.ToUpper().Equals("s".ToUpper())) valid = false;
                            if (ans.ToUpper().Equals("n".ToUpper()))
                            {
                                valid = true;
                                exit = "OK";
                            }
                        }
                    }
                } if (answer.ToUpper().Equals("n".ToUpper())) exit = "OK";
            } while (exit != "OK");

            MovieDetails(cli, json, ds);
        }
    }
}
