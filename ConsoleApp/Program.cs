using System;
using IdentityModel.Client;
using System.Net.Http;
using System.Threading.Tasks;   

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var discoveryDocument = await client.GetDiscoveryDocumentAsync("http://localhost:5006");
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = "client",
                    ClientSecret = "H+ Sport",
                    Scope = "hps-api"
                });
            if ( tokenResponse.Error != null )
            {
                Console.WriteLine(tokenResponse.Error);
            }

            Console.WriteLine($"Token: {tokenResponse.AccessToken}");
        }
    }
}
