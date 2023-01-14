using RestSharp;

namespace Repositories.Webservices
{
    public class WebservicesCities : IWebservicesCities
    {
        public RestResponse getCities(string search)
        {
            var client = new RestClient($"https://andruxnet-world-cities-v1.p.rapidapi.com/?query={search}&searchby=city");
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("X-RapidAPI-Key", "16de5415f8msh39af8ea89da8726p103feajsn58851e3ce2f3");
            request.AddHeader("X-RapidAPI-Host", "andruxnet-world-cities-v1.p.rapidapi.com");
            var response = client.Execute(request);
            return response;
        }
    }
}
