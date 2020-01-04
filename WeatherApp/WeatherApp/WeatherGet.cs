using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Template;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace WeatherApp
{
    class WeatherGet
    {
        private WeatherTemplate weatherTemplate;

        public WeatherGet()
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
            catch(Exception ex)
            {
                //
            }
        }
    }
}
