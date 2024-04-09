using System;
using System.Globalization;
using System.Text.Json;
using Application.Contracts;
using Models.Enums;
using Models.Filters;
using Newtonsoft.Json.Linq;

namespace Application.Services
{
    public class GeolocationService : IGeolocationService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<int> GetZone(LocationFilters filters)
        {
            try
            {
             
                string formattedLatitude = filters.Latitude.ToString("G", CultureInfo.InvariantCulture);
                string formattedLongitude = filters.Longitude.ToString("G", CultureInfo.InvariantCulture);

                // Construye la URL con los valores formateados
                string apiUrl = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={formattedLatitude}&lon={formattedLongitude}&json";

                _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("RentaCar/1.0");

                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    JToken typeToken = JObject.Parse(jsonResponse).SelectToken("type");

                    if (typeToken != null)
                    {
                        string typeValue = typeToken.ToString();

                        if (typeValue == "unclassified" || typeValue == "tertiary")
                            return (int)AreaEnum.AreaRural;
                        else if (typeValue == "residential")
                            return (int)AreaEnum.AreaUrbana;
                        else
                            return (int)AreaEnum.AreaMixta;
                    }
                }
                return 0; 
            } catch (HttpRequestException ex)
            {
                // Manejar errores de HTTP
                Console.WriteLine($"Error de HTTP: {ex.Message}");
                return 0; 
            } catch (JsonException ex)
            {
                // Manejar errores de deserialización JSON
                Console.WriteLine($"Error de JSON: {ex.Message}");
                return 0; 
            } catch (Exception ex)
            {
                // Manejar otros errores
                Console.WriteLine($"Error: {ex.Message}");
                return 0; 
            }
        }
    }
}