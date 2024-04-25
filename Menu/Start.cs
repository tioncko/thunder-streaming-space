using thunder_streaming_space.Authentication;
using thunder_streaming_space.Database;
using thunder_streaming_space.JsonObjects;
using thunder_streaming_space.Searches;

namespace thunder_streaming_space.Menu
{
    internal class Start
    {
        private static Dictionary<int, Search> Menu = new Dictionary<int, Search>();

        public static void Running(Client cli, DataSet ds)
        {
            Console.Clear();

            JsonMovies json = new JsonMovies(ds);
            int choice = FirstPage();
            bool valid = false;

            while (!valid)
            {
                if (choice == 0)
                {
                    Finish.Close(cli, ds);
                    valid = true;
                }

                if (Menu.ContainsKey(choice))
                {
                    Search search = Menu![choice];
                    search.Execute(cli, json, ds);
                    valid = true; 
                }
                else {
                    Console.WriteLine("Opção inválida");
                    choice = FirstPage();
                }
            }
        }

        private static int FirstPage()
        {
            Menu.Clear();

            Menu.Add(1, new SearchByTitles()); 
            Menu.Add(2, new SearchByGenres()); 
            Menu.Add(3, new SearchByInitials()); 
            Menu.Add(4, new SearchByLanguages()); 
            Menu.Add(5, new SearchByPages()); 
            Menu.Add(6, new SearchByProductionCompanies());
            Menu.Add(7, new SearchByReleaseDates());
            Menu.Add(8, new SearchById()); 

            ShowLogo();

            Console.WriteLine("\n## Digite uma opção\n" +
                "\n1 - Pesquisar por título" +
                "\n2 - Pesquisar por gênero" +
                "\n3 - Pesquisar pelas iniciais" +
                "\n4 - Pesquisar pelo idioma" +
                "\n5 - Pesquisar por páginas" +
                "\n6 - Pesquisar por produtoras" +
                "\n7 - Pesquisar por data de lançamento" +
                "\n8 - Pesquisar por id" +
                "\n0 - Sair");

            string option;// = Console.ReadLine()!;
            Console.Write($"\n=> Id menu: "); 
            option = Console.ReadLine()!;

            return int.Parse(option); ;
        }

        private static void ShowLogo() {
            Console.WriteLine("\r" +
                "\n██████████████████████████████████████████████████████████████████████████████████████████████████████████████████████\r\n█░░░░░░░░░░░░░░█░░░░░░██░░░░░░█░░░░░░██░░░░░░█░░░░░░██████████░░░░░░█░░░░░░░░░░░░███░░░░░░░░░░░░░░█░░░░░░░░░░░░░░░░███\r\n█░░▄▀▄▀▄▀▄▀▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀░░░░░░░░░░██░░▄▀░░█░░▄▀▄▀▄▀▄▀░░░░█░░▄▀▄▀▄▀▄▀▄▀░░█░░▄▀▄▀▄▀▄▀▄▀▄▀░░███\r\n█░░░░░░▄▀░░░░░░█░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀▄▀▄▀▄▀▄▀░░██░░▄▀░░█░░▄▀░░░░▄▀▄▀░░█░░▄▀░░░░░░░░░░█░░▄▀░░░░░░░░▄▀░░███\r\n█████░░▄▀░░█████░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀░░░░░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀░░█████████░░▄▀░░████░░▄▀░░███\r\n█████░░▄▀░░█████░░▄▀░░░░░░▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀░░░░░░░░░░█░░▄▀░░░░░░░░▄▀░░███\r\n█████░░▄▀░░█████░░▄▀▄▀▄▀▄▀▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀▄▀▄▀▄▀▄▀░░█░░▄▀▄▀▄▀▄▀▄▀▄▀░░███\r\n█████░░▄▀░░█████░░▄▀░░░░░░▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀░░░░░░░░░░█░░▄▀░░░░░░▄▀░░░░███\r\n█████░░▄▀░░█████░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░░░░░▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀░░█████████░░▄▀░░██░░▄▀░░█████\r\n█████░░▄▀░░█████░░▄▀░░██░░▄▀░░█░░▄▀░░░░░░▄▀░░█░░▄▀░░██░░▄▀▄▀▄▀▄▀▄▀░░█░░▄▀░░░░▄▀▄▀░░█░░▄▀░░░░░░░░░░█░░▄▀░░██░░▄▀░░░░░░█\r\n█████░░▄▀░░█████░░▄▀░░██░░▄▀░░█░░▄▀▄▀▄▀▄▀▄▀░░█░░▄▀░░██░░░░░░░░░░▄▀░░█░░▄▀▄▀▄▀▄▀░░░░█░░▄▀▄▀▄▀▄▀▄▀░░█░░▄▀░░██░░▄▀▄▀▄▀░░█\r\n█████░░░░░░█████░░░░░░██░░░░░░█░░░░░░░░░░░░░░█░░░░░░██████████░░░░░░█░░░░░░░░░░░░███░░░░░░░░░░░░░░█░░░░░░██░░░░░░░░░░█\r\n██████████████████████████████████████████████████████████████████████████████████████████████████████████████████████");
            
            Console.WriteLine("******************************");
            Console.WriteLine("Bem vindo ao Thunder Streaming"); 
            Console.WriteLine("******************************");
        }
    }
}
