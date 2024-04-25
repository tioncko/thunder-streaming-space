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
    internal class SearchByProductionCompanies : Search
    {
        public override void Execute(Client cli, JsonMovies json, DataSet ds)
        {
            base.Execute(cli, json, ds);
            ReleaseDB(ds);
            List<Movies> listMovies = ListMovies(cli, json, ds);

            ShowMenu(this.GetType().Name);
            var result = listMovies.SelectMany(item => item.ProductionCompanies!).ToList()
                                   .Select(x => new { x.Name })
                                   .GroupBy(x => x.Name)
                                   .OrderBy(x => x.Key)
                                   .ToList();
            int key = 1;
            Dictionary<int, string> typeProductionCompanies = new Dictionary<int, string>();
            foreach (var x in result)
            {
                typeProductionCompanies.Add(key, x.Key!);
                Console.WriteLine($"Produtora: {x.Key} | Quantidade: {x.Count()}");
                key++;
            }

            string exit = "";
            do
            {
                Console.Write("\nVerificar filmes de uma produtora específica? [S/N]: ");
                string answer = Console.ReadLine()!;

                if (answer.ToUpper().Equals("s".ToUpper()))
                {
                    Console.WriteLine();
                    bool valid = false;

                    Console.WriteLine("## Produtoras\n");
                    foreach (var j in typeProductionCompanies)
                    {
                        Console.WriteLine($"{j.Key} - {j.Value}");
                    }

                    int cnt = 0;
                    while (!valid)
                    {
                        if (cnt > 0) Console.Write("Produtora não encontrada. Informar o código da produtora para pesquisa: ");
                        else Console.Write("\nInformar o código da produtora para pesquisa: ");
                        int type = int.Parse(Console.ReadLine()!);

                        if (typeProductionCompanies.ContainsKey(type))
                        {
                            var itm = listMovies.Select(item => item)
                                                .Where(gr => gr.ProductionCompanies!.Any(x => x.Name == typeProductionCompanies[type]))
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
                            Console.Write("\nDeseja pesquisar outra produtora? [S/N]: ");
                            string ans = Console.ReadLine()!;

                            if (ans.ToUpper().Equals("s".ToUpper())) valid = false;
                            if (ans.ToUpper().Equals("n".ToUpper()))
                            {
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
