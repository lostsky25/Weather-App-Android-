using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using WeatherApp.Template;
using WeatherAppAndroid;

namespace WeatherApp.Weather
{
    class Weather : TemperatureConverter, SpeedConverter
    {
        public WeatherTemplate weatherTemplate;

        public Weather()
        {
            string url = "https://samples.openweathermap.org/data/2.5/weather?lat=35&lon=139&appid=b6907d289e10d714a6e88b30761fae22";

            ReadJsonTemplate(GetJsonData(url));
        }

        /// <summary>
        /// Get JSON data from request.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>JSON data in string.</returns>
        public string GetJsonData(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Convert JSON data to WeatherTemplate object.
        /// </summary>
        /// <param name="data">Data for deserialize</param>
        internal void ReadJsonTemplate(string data)
        {
            weatherTemplate = new WeatherTemplate();

            try
            {
                weatherTemplate = JsonConvert.DeserializeObject<WeatherTemplate>(data);
            }
            catch (Exception ex)
            {
                Android.Util.Log.Error("Err: ", ex.Message.ToString());
            }
        }

        public string ConvertToCelsius(string kelvin)
        {
            return Convert.ToInt32(Double.Parse(kelvin.Replace(".", ",")) - 273.15).ToString();
        }

        public string ConvertToKilometersPerHour(string meters)
        {
            return Convert.ToInt32(Double.Parse(meters.Replace(".", ",")) * 1000 / 3600 ).ToString();
        }

        public string ConvertToFahrenheit(string kelvin)
        {
            return Convert.ToInt32((Double.Parse(kelvin.Replace(".", ",")) - 273.15) * 9/5 + 32).ToString();
        }
    }
}
