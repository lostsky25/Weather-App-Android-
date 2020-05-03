using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherDifferentSource
{
    interface WeatherData
    {
        //General information
        /// <summary>
        /// Function with a postfix TenDays return some List<string>.
        /// There are ten days.
        /// [0] - is present day
        /// [1] - tomorrow
        /// etc.
        /// </summary>
        /// <returns>returns weather for ten days</returns>
        public List<string> GetMaxTemperatureTenDays();
        public List<string> GetMinTemperatureTenDays();
        public List<string> GetPrecipitation();
        public List<string> GetPressure();
        public List<string> GetHumidity();
        public List<string> GetWindSpeed();
        public List<string> GetWeatherType();
        //!General information

        //public string getWeatherTomorrow();
        public string GetTown();
        //public string getWeatherDescription();
        //public string getCountry();
    }
}
