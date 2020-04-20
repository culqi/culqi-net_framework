using Culqi;
using System;
using System.Threading.Tasks;

namespace CulqiSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press Enter");
            Console.ReadLine();

            CulqiConfiguration.PublicApiKey = "pk_test_8MtrpF4Kw1pAjdYX";

            var tokenOptions = new TokenCreateOptions()
            {
                CardNumber = "4111111111111111",
                ExpirationYear = "2029",
                ExpirationMonth = "09",
                Cvv = "123",
                Email = "richard@piedpiper.com"
            };

            var tokenService = new TokenService();
            Token stripeToken = await tokenService.Create(tokenOptions);

            Console.WriteLine(stripeToken.Id);
            Console.ReadLine();
        }
    }
}
