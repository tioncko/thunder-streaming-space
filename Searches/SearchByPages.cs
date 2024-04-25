using System;
using thunder_streaming_space.Authentication;
using thunder_streaming_space.Database;
using thunder_streaming_space.JsonObjects;
using thunder_streaming_space.Menu;
using static thunder_streaming_space.Deserializers.ItemMovies;

namespace thunder_streaming_space.Searches
{
    internal class SearchByPages : Search
    {
        public override void Execute(Client cli, JsonMovies json, DataSet ds)
        {
            base.Execute(cli, json, ds);
            ReleaseDB(ds);

            int pass = 0, stamp = 0;
            bool valid = false;
                        
            List<Movies> listMovies = ListMovies(cli, json, ds);
            List<string> pages = new List<string>();
            listMovies.Select(x => x.Page!).Distinct().ToList().ForEach(x => pages.Add(x));

            string numpage = "";
            foreach (var item in pages) numpage += item + ", ";

            ShowMenu(this.GetType().Name);
            do
            {
                if (pass == 0) Console.Write($"Digite a página que deseja visualizar [págs. {numpage.Substring(0, numpage.Length - 2)}]: ");
                if (pass == 1) Console.Write($"Digite outra página que deseja visualizar [págs. {numpage.Substring(0, numpage.Length - 2)}]: ");
                string page = Console.ReadLine()!;
                
                if (pages.Contains(page))
                {
                    foreach (var itm in listMovies.Where(x => x.Page == page))
                    {
                        Console.WriteLine(String.Empty.PadLeft(30, '*'));
                        Console.WriteLine(itm);
                    }
                    Console.WriteLine(String.Empty.PadLeft(30, '*'));
                    stamp = 0;

                    Console.Write("\nAcessar outra página? [S/N]: ");
                    string answer = Console.ReadLine()!;

                    if (answer.ToUpper().Equals("s".ToUpper())) pass = 1;
                    if (answer.ToUpper().Equals("n".ToUpper()))
                    {
                        valid = true;
                        Console.Write("Pressione qualquer tecla para voltar ao menu principal ");
                        Console.ReadKey();
                        Start.Running(cli, ds);
                    }
                }

                if (!pages.Contains(page))
                {
                    Console.WriteLine("Página não encontrada");
                    stamp++;

                    if (stamp < 3)
                    {
                        pass = 2;
                        Console.WriteLine($"Digite uma página válida [págs. {numpage.Substring(0, numpage.Length - 2)}]: ");
                    }
                    else
                    {
                        valid = true;
                        Console.Write("Pressione qualquer tecla para voltar ao menu principal ");
                        Console.ReadKey();
                        Start.Running(cli, ds);
                    }
                }
            } while (!valid);
        }
    }
}
