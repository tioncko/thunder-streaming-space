using System.Text.RegularExpressions;
using thunder_streaming_space.Authentication;
using thunder_streaming_space.Database;
using thunder_streaming_space.JsonObjects;
using static thunder_streaming_space.Deserializers.ItemMovies;

namespace thunder_streaming_space.Searches
{
    internal class SearchByInitials : Search
    {
        public override void Execute(Client cli, JsonMovies json, DataSet ds)
        {
            base.Execute(cli, json, ds);
            ReleaseDB(ds);
            List<Movies> listMovies = ListMovies(cli, json, ds);
            
            ShowMenu(this.GetType().Name);
            Console.WriteLine("Informar as iniciais do título para pesquisa: [ex.: 'a', 'ab', 'abc', 'a-z', 'abcd']");
            string initials = Console.ReadLine()!;
            string regex = RegexInitials(initials).ToUpper();
            
            Dictionary<int, string> listInitials = new Dictionary<int, string>();

            try
            {
                foreach (char init in regex)
                {
                    listMovies.Select(item => new { item.Title, item.Id }).ToList()
                              .Where(x => x.Title!.StartsWith(init))
                              .ToList().ForEach(item =>
                              {
                                  listInitials.Add(item.Id!.Value, item.Title!);
                              });
                }

                //result.ForEach(item => {
                foreach (var item in listInitials)
                {
                    Console.WriteLine("******************************************************************");
                    Console.WriteLine($"Id: {item.Key}\nTítulo: {item.Value}");
                    //});
                }
                Console.WriteLine("******************************************************************");

                MovieDetails(cli, json, ds);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: " + e.Message);
            }
        }

        private string RegexInitials(string value) {
            string init = RegularExp(value);
            string[] initial;
            string alphabeth = "abcdefghijklmnopqrstuvwxyz";
            string ex = "";
            
            switch (init.Length) {
                case 1:
                    initial = new string[init.Length];
                    initial[0] = init;
                    ex = String.Concat(initial);
                    break;

                case 2:
                    string retTwo = StrDistinct(init);
                    initial = new string[retTwo.Length];
                    ex = StrFilter(retTwo, initial);
                    break;

                case 3:
                    string start = alphabeth.IndexOf(init.Substring(0, 1)) < alphabeth.IndexOf(init.Substring(2, 1)) ? init.Substring(0, 1) : init.Substring(2, 1);
                    string end = alphabeth.IndexOf(init.Substring(0, 1)) > alphabeth.IndexOf(init.Substring(2, 1)) ? init.Substring(0, 1) : init.Substring(2, 1);

                    if (init.Substring(1, 1).Equals("-") && !start.Equals("-") && !end.Equals("-"))
                    {
                        int idxStart = alphabeth.IndexOf(start);
                        string retThr = alphabeth.Substring(idxStart, alphabeth.Length - idxStart);

                        int idxEnd = retThr.IndexOf(end);
                        retThr = retThr.Substring(0, idxEnd + 1);
                        
                        ex = StrDistinct(retThr);
                    }

                    if (!init.Contains('-')) {
                        string retThr = StrDistinct(init);
                        initial = new string[retThr.Length];
                        ex = StrFilter(retThr, initial);
                    }
                    break;

                case >= 4:
                    if (init.Contains('-')) init = init.Replace("-", "");
                    
                    string retFour = StrDistinct(init);
                    initial = new string[retFour.Length];
                    ex = StrFilter(retFour, initial);
                    break;
            }

            return ex;
        }

        public string RegularExp(string text)
        {
            Regex regex = new Regex(@"[^\b][^a-zA-Z\w\.@\-]");
            string result = regex.Replace(text, "");
            
            Regex regex2 = new Regex(@"[\d]");
            return regex2.Replace(result, "");
        }

        public string StrDistinct(string text)
        {
            var str = text.Select(x => x.ToString())
                          .Distinct()
                          .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                          .ThenBy(x => x, StringComparer.Ordinal);
                          //.ToArray();
            return String.Concat(str);
        }

        public string StrFilter(string retDs, string[] initial) {
           
            for (int i = 0; i < retDs.Length; i++)
            {
                initial[i] = retDs.Substring(i, 1);
            }
            return String.Concat(initial);
        }
    }
}
