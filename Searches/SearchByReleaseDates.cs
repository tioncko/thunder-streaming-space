using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thunder_streaming_space.Authentication;
using thunder_streaming_space.Database;
using thunder_streaming_space.JsonObjects;
using static thunder_streaming_space.Deserializers.ItemMovies;

namespace thunder_streaming_space.Searches
{
    internal class SearchByReleaseDates : Search
    {
        public override void Execute(Client cli, JsonMovies json, DataSet ds)
        {
            base.Execute(cli, json, ds);
            ReleaseDB(ds);

            List<Movies> listMovies = ListMovies(cli, json, ds);
            
            ShowMenu(this.GetType().Name);
            var date = listMovies
                .Select(x => new { x.ReleaseDate })
                .Where(x => x.ReleaseDate != "").ToList()
                .Select(x => x.ReleaseDate!.Substring(0, x.ReleaseDate.IndexOf("-")))
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .ToList();

            int key = 1;
            Dictionary<int, string> typeDate = new Dictionary<int, string>();
            foreach (var x in date)
            {
                typeDate.Add(key, x.Key!);
                Console.WriteLine($"Ano de lançamento: {x.Key} | Quantidade: {x.Count()}");
                key++;
            }

            string exit = "";
            do {
                Console.Write("\nVerificar filmes de um ano específico? [S/N]: ");
                string answer = Console.ReadLine()!;

               if (answer.ToUpper().Equals("s".ToUpper()))
                {
                    Console.WriteLine();
                    bool valid = false;

                    Console.WriteLine("## Anos\n");
                    foreach (var j in typeDate)
                    {
                        Console.WriteLine($"{j.Key} - {j.Value}");
                    }

                    int cnt = 0;
                    while (!valid)
                    {
                        if (cnt > 0) Console.Write("Ano não encontrado. Informar o código do ano para pesquisa: ");
                        else Console.Write("\nInformar o código do ano para pesquisa: ");
                        int type = int.Parse(Console.ReadLine()!);

                        if (typeDate.ContainsKey(type))
                        {
                            var itm = listMovies.Select(item => item)
                                                .Where(gr => gr.ReleaseDate!.Contains(typeDate[type]))
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
                            Console.Write("\nDeseja pesquisar outro ano? [S/N]: ");
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
