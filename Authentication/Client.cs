using System.Net.Http.Headers;
using thunder_streaming_space.Properties;
using System.Net;
using thunder_streaming_space.Settings;
using thunder_streaming_space.Database;

namespace thunder_streaming_space.Authentication
{
    internal class Client : EntitySQLConn
    {
        static readonly HttpClient client = new HttpClient();

        public string GetBearerToken(string route)
        {
            try
            {
                #region posgreSQL
                //client.Timeout = TimeSpan.FromMinutes(10);

                //PgSQLConn dbconn = new PgSQLConn();
                //var cmd = dbconn.ReturnTableData("api_parameters");

                //while (cmd.Read()) { 
                //    API.Token = cmd["access_token"].ToString();
                //    API.TokenType = cmd["token_type"].ToString();
                //}
                //cmd.Close();
                #endregion

                foreach (var item in GetAPI()) { 
                    API.Token = item.Token;
                    API.TokenType = item.TokenType;
                }

                API.Endpoint = Parameters.Build().GetSection("TMDb").Value!;
                string url = $"{API.Endpoint}{route}";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(API.TokenType!, API.Token);

                HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return client.GetStringAsync(url).GetAwaiter().GetResult(); //or use GetAsync
                }
                else
                {
                    string code = "";
                    if (response.StatusCode == HttpStatusCode.NotFound) code = $"404 Error{HttpStatusCode.NotFound}";
                    return code;
                }
            }
            catch (HttpRequestException ex) //when (ex is { StatusCode: HttpStatusCode.NotFound})
            {
                return $"Status: [{ex.Message}]";
            }
        }
    }
}

#region OldParams
//private static readonly string connString = Parameters.Build().GetSection("ConnectionStrings").GetSection("pgsqlConnStr").Value!;
//API.Token = Parameters.Build().GetSection("token").GetSection("access_token").Value!;
//API.Token = Parameters.Build().GetSection("token").GetChildren().ToArray()[0].Value!);
//API.TokenType = Parameters.Build().GetSection("token").GetChildren().ToArray()[1].Value!);
//API.TokenType = Parameters.Build().GetSection("token").GetSection("token_type").Value!;
#endregion

#region Old tips
//public string? Endpoint { get; private set; }
//public string? ApiKey { get; private set; }
//public string? Token { get; private set; }

//client.DefaultRequestHeaders.Add("Authorization", "Bearer " + api_read_key);

//public string Basic(string route)
//{
//    API.Endpoint = Parameters.Build().GetSection("TMDb").Value!;
//    API.ApiKey = Parameters.Build().GetSection("token").GetSection("api_key").Value!;

//    //3/movie/157336?api_key=
//    var urlWithoutBasic = $"{API.Endpoint}{route}{API.ApiKey}";
//    return client.GetStringAsync(urlWithoutBasic).GetAwaiter().GetResult(); //or use GetStringAsync
//}
#endregion

