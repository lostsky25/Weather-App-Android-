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

using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using WeatherAppAndroid.UserData;

namespace WeatherAppAndroid
{
    [Activity(Label = "Settings")]
    public class Settings : Activity
    {
        private DrawerLayout drawerLayout;
        private RadioButton radioCelsius;
        private RadioButton radioFahrenheit;
        private RadioButton radioMetersPerSecond;
        private RadioButton radioKilometersPerHour;
        private NavigationView navigationView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_settings);

            //var temperatureFormatGroup = FindViewById<RadioGroup>(Resource.Id.temperatureFormatGroup);
            radioCelsius = FindViewById<RadioButton>(Resource.Id.radio_celsius);
            radioFahrenheit = FindViewById<RadioButton>(Resource.Id.radio_fahrenheit);
            radioMetersPerSecond = FindViewById<RadioButton>(Resource.Id.ms);
            radioKilometersPerHour = FindViewById<RadioButton>(Resource.Id.km);

            radioCelsius.Click += RadioButtonTemperatureClick;
            radioFahrenheit.Click += RadioButtonTemperatureClick;
            radioMetersPerSecond.Click += RadioButtonSpeedClick;
            radioKilometersPerHour.Click += RadioButtonSpeedClick;

            switch (UserData.UserData.temperatureFormat)
            {
                case "C°":
                    radioCelsius.Checked = true;
                    break;
                case "F°":
                    radioFahrenheit.Checked = true;
                    break;
                default:
                    break;
            }

            switch (UserData.UserData.windSpeedFormat)
            {
                case "m/s":
                    radioMetersPerSecond.Checked = true;
                    break;
                case "km/h":
                    radioKilometersPerHour.Checked = true;
                    break;
                default:
                    break;
            }

            //for (int i = 0; i < temperatureFormatGroup.ChildCount; i++)
            //{
            //    var child = temperatureFormatGroup.GetChildAt(i);
            //    if (child is RadioButton)
            //    {
            //        ((RadioButton)child).FocusChange += RadioButton_FocusChange;
            //    }
            //}

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            navigationView.NavigationItemSelected += OnNavigationItemSelected;
        }



        //private void RadioButton_FocusChange(object sender, View.FocusChangeEventArgs e)
        //{
        //    if (e.HasFocus)
        //    {
        //        ((RadioButton)sender).Checked = true;
        //    }
        //}

        private void RadioButtonTemperatureClick(object sender, EventArgs e)
        {
            UserData.UserData.temperatureFormat = ((RadioButton)sender).Text;
            //Toast.MakeText(this, UserData.UserData.temperatureFormat, ToastLength.Short).Show();

        }

        private void RadioButtonSpeedClick(object sender, EventArgs e)
        {
            UserData.UserData.windSpeedFormat = ((RadioButton)sender).Text;
            //Toast.MakeText(this, UserData.UserData.temperatureFormat, ToastLength.Short).Show();

        }

        private void OnNavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.home):
                    e.MenuItem.SetCheckable(true);
                    Toast.MakeText(this, "Home selected", ToastLength.Short).Show();

                    Intent intent = new Intent(this, typeof(MainActivity));
                    this.StartActivity(intent);

                    break;

                case (Resource.Id.settings):
                    e.MenuItem.SetCheckable(true);
                    Toast.MakeText(this, "Settings selected", ToastLength.Short).Show();


                    break;
            }

            drawerLayout.CloseDrawers();
        }
    }
}