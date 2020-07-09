using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace AirMonitor.Airly
{
    /// <summary>
    /// Class for getting data using Airly API
    /// </summary>
    public static class AirlyManager
    {
        /// <summary>
        /// Airly API key
        /// </summary>
        const string apiKey = "qu03XzBVjXdhucZyV0puZEwNNCkykyWk";

        static readonly HttpClient httpClient = new HttpClient();

        /// <summary>
        /// Gets measurements from nearest installation
        /// </summary>
        /// <returns>
        /// 
        /// If request fails returns <see langword="null"/>
        /// </returns>
        public static async Task<Measurement> GetNearestMeasurement(double latitude, double longitude)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://airapi.airly.eu/v2/measurements/nearest?indexType=AIRLY_CAQI&lat={latitude}&lng={longitude}&maxDistanceKM=-1");
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));
                request.Headers.Add("apikey", "qu03XzBVjXdhucZyV0puZEwNNCkykyWk");

                HttpResponseMessage response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();

                Measurements measurements = JsonConvert.DeserializeObject<Measurements>(responseContent);

                return measurements.current;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<Measurement> GetNearestMeasurement()
        {
            try
            {
                Location location = await Geolocation.GetLocationAsync();
#if DEBUG
                if (location != null) return await GetNearestMeasurement(50, 20);
#else
                if (location != null) return await GetNearestMeasurement(location.Latitude, location.Longitude);
#endif
                else return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
