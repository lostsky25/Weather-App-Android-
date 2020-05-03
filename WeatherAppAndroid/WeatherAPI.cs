using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Text;
using System.Diagnostics;
using HtmlAgilityPack;
using WeatherApp.Weather;
using WeatherAppAndroid;

namespace WeatherDifferentSource.WeatherAPI
{
    class WeatherAPI : WeatherData, TemperatureConverter, SpeedConverter
    {
        private string url;
        private string additionalUrl;
        private string service;
        private List<string> maxTemperatureTenDays;
        private List<string> windSpeedTenDays;
        private List<string> minTemperatureTenDays;
        private List<string> precipitationTenDays;
        private List<string> humidityTenDays;
        private List<string> pressureTenDays;
        private List<string> typeOfWeatherTenDays;

        private HtmlDocument htmlDocument;
        private HtmlNodeCollection tenDays;
        private HtmlWeb htmlWeb;

        Regex rxCloud;
        Regex rxRain;
        Regex rxSnow;
        Regex rxStorm;
        Regex rxSun;

        public WeatherAPI(string service, string url)
        {
            this.url = url;
            
            switch (service)
            {
                case "gismetio":
                    additionalUrl = url + "/now/";
                    break;
                default:
                    additionalUrl = url;
                    break;
            }
            
            this.service = service;
            htmlWeb = new HtmlAgilityPack.HtmlWeb();

            htmlDocument = htmlWeb.Load(url);
        }

        public string GetIconInfo(int day)
        {
            GetWeatherType();

            rxCloud = new Regex(@"\b(\w*(cloudy)\w*)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            rxRain = new Regex(@"\b(\w*(rain)\w*)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            rxSnow = new Regex(@"\b(\w*(snow)\w*)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            rxStorm = new Regex(@"\b(\w*(thunderstorm)\w*)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            rxSun = new Regex(@"\b(\w*(fair)\w*)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (typeOfWeatherTenDays != null)
            {
                if (rxCloud.IsMatch(typeOfWeatherTenDays[day]))
                {
                    if (rxRain.IsMatch(typeOfWeatherTenDays[day]))
                    {
                        if (rxSnow.IsMatch(typeOfWeatherTenDays[day]))
                        {
                            if (rxStorm.IsMatch(typeOfWeatherTenDays[day]))
                            {
                                return "snow_grey";
                            }
                            else
                            {
                                return "snow_grey";
                            }
                        }
                        else
                        {
                            return "rain_grey";
                        }
                    }
                    else
                    {
                        return "clouds_grey";
                    }
                }
                else
                {
                    return "sun_grey";
                }
            }
            else
            {
                return "sun_grey";
            }
        }

        public List<string> GetPressure()
        {
            pressureTenDays = new List<string>();
            tenDays = htmlDocument.DocumentNode.SelectNodes("//*[contains(@class, 'widget__item')]/div[contains(@class, 'w-humidity widget__value w_humidity_type_')]");
            switch (service)
            {
                case "gismetio":
                    foreach (HtmlNode day in tenDays)
                    {
                        pressureTenDays.Add(day.InnerText);
                    }
                    return pressureTenDays;
                default:
                    return pressureTenDays;
            }
        }

        public List<string> GetHumidity()
        {
            humidityTenDays = new List<string>();
            tenDays = htmlDocument.DocumentNode.SelectNodes("//*[contains(@class, 'widget__item')]/div[contains(@class, 'w-humidity widget__value w_humidity_type_')]");
            switch (service)
            {
                case "gismetio":
                    foreach (HtmlNode day in tenDays)
                    {
                        humidityTenDays.Add(day.InnerText);
                    }
                    return humidityTenDays;
                default:
                    return humidityTenDays;
            }
        }

        public List<string> GetMaxTemperatureTenDays()
        {
            maxTemperatureTenDays = new List<string>();
            tenDays = htmlDocument.DocumentNode.SelectNodes("//*[contains(@class, 'maxt')]/span[contains(@class, 'unit unit_temperature_c')]");
            switch (service)
            {
                case "gismetio":
                    foreach (HtmlNode day in tenDays)
                    {
                        maxTemperatureTenDays.Add(day.InnerText);
                    }
                    break;
            }
            return maxTemperatureTenDays;
        }

        public List<string> GetMinTemperatureTenDays()
        {
            humidityTenDays = new List<string>();
            tenDays = htmlDocument.DocumentNode.SelectNodes("//*[contains(@class, 'mint')]/span[contains(@class, 'unit unit_temperature_c')]");
            switch (service)
            {
                case "gismetio":
                    foreach (HtmlNode day in tenDays)
                    {
                        humidityTenDays.Add(day.InnerText);
                    }
                    return humidityTenDays;
            }
            return minTemperatureTenDays;
        }

        public List<string> GetPrecipitation()
        {
            precipitationTenDays = new List<string>();
            tenDays = htmlDocument.DocumentNode.SelectNodes("//*[contains(@class, 'w_prec')]/div[contains(@class, 'w_prec__value')]");
            switch (service)
            {
                case "gismetio":
                    foreach (HtmlNode day in tenDays)
                    {
                        precipitationTenDays.Add(day.InnerText.Trim());
                    }
                    break;
            }
            return precipitationTenDays;
        }

        public string GetTown()
        {
            switch (service)
            {
                case "gismetio":
                    return htmlDocument.DocumentNode.SelectNodes("//*[contains(@class, 'js-crumb crumb link gray') and ./span]")[1].InnerText.Trim();
                default:
                    return "Failed";
            }
        }

        public List<string> GetWindSpeed()
        {
            windSpeedTenDays = new List<string>();
            tenDays = htmlDocument.DocumentNode.SelectNodes("//*[contains(@class, 'w_wind')]/span[contains(@class, 'unit unit_wind_m_s')]");
            
            switch (service)
            {
                case "gismetio":
                    for(int count = 0; count < 10; count++)
                    {
                        windSpeedTenDays.Add(tenDays[count].InnerText.Trim());
                    }
                    break;
            }
            return windSpeedTenDays;
        }

        public List<string> GetWeatherType()
        {
            typeOfWeatherTenDays = new List<string>();
            tenDays = htmlDocument.DocumentNode.SelectNodes("//*[contains(@class, 'widget__item')]/div[contains(@class, 'widget__value w_icon')]/span[@data-text]");
            
            switch (service)
            {
                case "gismetio":
                    for (int count = 0; count < 10; count++)
                    {
                        typeOfWeatherTenDays.Add(tenDays[count].Attributes[1].DeEntitizeValue);
                    }
                    break;
            }
            return typeOfWeatherTenDays;
        }

        public string ConvertKelvinToCelsius(string kelvin)
        {
            return Convert.ToInt32(Double.Parse(kelvin.Replace(".", ",")) - 273.15).ToString();
        }

        public string ConvertToKilometersPerHour(string meters)
        {
            return Convert.ToInt32(Double.Parse(meters.Replace(".", ",")) * 1000 / 3600).ToString();
        }

        public string ConvertKelvinToFahrenheit(string kelvin)
        {
            return Convert.ToInt32((Double.Parse(kelvin.Replace(".", ",")) - 273.15) * 9 / 5 + 32).ToString();
        }

        public string ConvertCelsiusToFahrenheit(string celsius)
        {
            return Convert.ToInt32(Double.Parse(celsius.Replace(".", ",")) * 9 / 5 + 32).ToString();
        }

        public string ConvertFahrenheitToCelsius(string celsius)
        {
            return Convert.ToInt32((Double.Parse(celsius.Replace(".", ",")) - 32) * 9 / 5).ToString();
        }
    }
}
