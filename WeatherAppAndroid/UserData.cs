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

namespace WeatherAppAndroid.UserData
{
    static class UserData
    {
        public static string temperatureFormat { get; set; }
        public static string windSpeedFormat { get; set; }
        //public static string adnvancedMode { get; set; }
    }
}