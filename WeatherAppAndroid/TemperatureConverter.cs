using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace WeatherApp.Weather
{
    interface TemperatureConverter
    {
        public string ConvertKelvinToCelsius(string fahrenheit);
        public string ConvertKelvinToFahrenheit(string celsius);
        public string ConvertCelsiusToFahrenheit(string celsius);
        public string ConvertFahrenheitToCelsius(string celsius);
    }
}