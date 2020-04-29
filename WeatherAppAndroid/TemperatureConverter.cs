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
        public string ConvertToCelsius(string fahrenheit);
        public string ConvertToFahrenheit(string celsius);
    }
}