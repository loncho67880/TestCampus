using Core.Models;
using Newtonsoft.Json;
using Repositories.Webservices;
using System.Xml.Linq;

namespace Application.Services
{
    public class CitiesServices : ICitiesServices
    {
        private readonly IWebservicesCities _webservicesCities;

        /// <summary>
        /// Json de las ciudades encontradas
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public CitiesServices(IWebservicesCities webservicesCities)
        {
            _webservicesCities = webservicesCities;
        }

        /// <summary>
        /// Json de las ciudades encontradas
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<Cities> getCities(string search)
        {
            return JsonConvert.DeserializeObject<List<Cities>>(_webservicesCities.getCities(search).Content);
        }
    }
}
