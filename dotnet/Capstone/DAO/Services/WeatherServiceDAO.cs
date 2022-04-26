using Capstone.Models;
using RestSharp;
using System.Net.Http;
using Capstone.DAO.Interfaces;
namespace Capstone.DAO.Services
{
    public class WeatherServiceDAO : IWeatherDAO
    {
        protected static RestClient client = new RestClient("http://api.openweathermap.org");

        public Coordinate getCoordinates(int zipCode)
        {
            RestRequest request = new RestRequest($"/geo/1.0/zip?zip={zipCode},US&appid=8967dc1398824eb30d8ee9b314182915");
            IRestResponse<Coordinate> response = client.Get<Coordinate>(request);

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"There was an error getting your longitude.");
            }

            return response.Data;
        }

    }
}
