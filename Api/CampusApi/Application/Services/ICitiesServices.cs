using Core.Models;

namespace Application.Services
{
    public interface ICitiesServices
    {
        /// <summary>
        /// Json de las ciudades encontradas
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<Cities> getCities(string search);
    }
}
