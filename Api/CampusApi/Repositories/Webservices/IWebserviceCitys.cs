using RestSharp;

namespace Repositories.Webservices
{
    public interface IWebservicesCities
    {
        RestResponse getCities(string search);
    }
}
