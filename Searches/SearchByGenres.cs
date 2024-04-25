using thunder_streaming_space.Authentication;
using thunder_streaming_space.Database;
using thunder_streaming_space.JsonObjects;
using static thunder_streaming_space.Deserializers.ItemMovies;

namespace thunder_streaming_space.Searches
{
    internal class SearchByGenres : Search
    {
        public override void Execute(Client cli, JsonMovies json, DataSet ds)
        {
            base.Execute(cli, json, ds);
            ReleaseDB(ds);
            List<Movies> listMovies = ListMovies(cli, json, ds);

            //SelectMany is the same as flatMap in Java
            ShowMenu(this.GetType().Name);
            var result = listMovies.SelectMany(item => item.Genres!.Select(x => new { x.Name })).ToList()
                                   .GroupBy(x => x.Name)
                                   .OrderBy(x => x.Key)
                                   .ToList();
            //result.ForEach(x => Console.WriteLine($"Gênero: {x.Key} | Quantidade: {x.Count()}"));
            int key = 1;
            Dictionary<int, string> typeGenre = new Dictionary<int, string>();
            foreach (var x in result) {
                typeGenre.Add(key, x.Key!);
                Console.WriteLine($"Gênero: {x.Key} | Quantidade: {x.Count()}");
                key++;
            }

            string exit = "";
            do {
                Console.Write("\nVerificar filmes de um gênero específico? [S/N]: ");
                string answer = Console.ReadLine()!;

                if (answer.ToUpper().Equals("s".ToUpper()))
                {
                    Console.WriteLine();
                    bool valid = false;

                    Console.WriteLine("## Gêneros\n");
                    foreach (var j in typeGenre)
                    {
                        Console.WriteLine($"{j.Key} - {j.Value}");
                    }

                    int cnt = 0;
                    while (!valid) {
                        if (cnt > 0) Console.Write("Gênero não encontrado. Informar o código do gênero para pesquisa: ");
                        else Console.Write("\nInformar o código do gênero para pesquisa: ");
                        int type = int.Parse(Console.ReadLine()!);

                        if (typeGenre.ContainsKey(type)) {
                            var itm = listMovies.Select(item => item)
                                                .Where(gr => gr.Genres!.Any(x => x.Name == typeGenre[type]))
                                                .ToList();
                            Console.WriteLine();
                            foreach (var item in itm) {
                                Console.WriteLine("******************************************************************");
                                ShowMovies(item);
                            }
                            Console.WriteLine("******************************************************************");
                            cnt = 0;
                        }
                        else cnt++;

                        if (cnt == 0) {
                            Console.Write("\nDeseja pesquisar outro gênero? [S/N]: ");
                            string ans = Console.ReadLine()!;

                            if (ans.ToUpper().Equals("s".ToUpper())) valid = false;
                            if (ans.ToUpper().Equals("n".ToUpper())) {
                                valid = true;
                                exit = "OK";
                            }
                        }
                    }
                }
                if (answer.ToUpper().Equals("n".ToUpper())) exit = "OK";
            } while (exit != "OK");

            MovieDetails(cli, json, ds);
        }
    }
}

#region Old
//Console.Write("Digite até qual página que deseja visualizar {Pág. 1 até 'X paginas' ou 'all' para todos os filmes}: ");
//string lastpage = Console.ReadLine()!;

//    = new List<Movies>();
//for (int i = 1; i <= int.Parse(lastpage); i++)
//{
//    foreach (var item in json.GetMovies(cli, Convert.ToString(i)))
//    {
//        listMovies.Add(item);
//    }
//}

#endregion