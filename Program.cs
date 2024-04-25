using thunder_streaming_space.Authentication;
using thunder_streaming_space.Database;
using thunder_streaming_space.Menu;

internal class Program
{
    private static DataSet ds;
    private static Client cli;

    //static Program() => ds = new DataSet();
    static Program()
    { 
        ds = new DataSet();
        cli = new Client();
    }

    private static void Main(string[] args) => Start.Running(cli, ds);//.GetAwaiter().GetResult();
}

//#region old

//static void Mainy(string[] args)
//{
//    EntitySQLConn e = new EntitySQLConn();

//    foreach (var i in e.APIParameters!) { 
//        Console.WriteLine(i.TokenType);
//        Console.WriteLine(i.Token);
//    }
//}
/*
//string route2 = "/3/movie/1258792?api_key=";
//string route1 = "/3/movie/changes?page=1";
//string route2 = "/3/movie/746036";
foreach (var item in js.GetItemMovies(cli, "1"))
{
    Console.WriteLine(item);
    Console.WriteLine("====================================");
}

//Console.WriteLine(cli.GetBearerToken(route2));
//Console.WriteLine();
//Console.WriteLine(cli.GetBearerToken(route1));
//Console.WriteLine();
//Console.WriteLine(API.Endpoint);

 * Listar:
todos os titulos de filmes [nome, produtora, idioma, data de lançamento] -> ver detalhes do filme -> [abrir titulo por id]
todos os titulos por pagina [nome, produtora, idioma, data de lançamento] -> ver detalhes do filme -> [abrir titulo por id]
todos os titulos por genero [nome, produtora, idioma, data de lançamento] -> ver detalhes do filme -> [abrir titulo por id]
todos os titulos por data de lançamento [nome, produtora, idioma, data de lançamento] -> ver detalhes do filme -> [abrir titulo por id]
todos os titulos por produtora [nome, produtora, idioma, data de lançamento] -> ver detalhes do filme -> [abrir titulo por id] 
todos os titulos por idioma [nome, produtora, idioma, data de lançamento] -> ver detalhes do filme -> [abrir titulo por id]
todos os titulos por pais [nome, produtora, idioma, data de lançamento] -> ver detalhes do filme -> [abrir titulo por id]
todos os titulos por inicial [A-z] -> listar filmes por letra -> [nome, produtora, idioma, data de lançamento] -> ver detalhes do filme -> [abrir titulo por id]

filme por id
 */

//#endregion
