using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using WeatherApp.Weather;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using System.Linq.Expressions;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Content;
using WeatherDifferentSource.WeatherAPI;

namespace WeatherAppAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private DrawerLayout drawerLayout;
        private NavigationView navigationView;
        private ImageView currentWeatherImage;
        private TextView weatherType;
        private TextView temperature;
        private TextView weatherPressure;
        private TextView weatherHumidity;
        private TextView windSpeed;

        //Second section (additional)
        private TextView[] temperature_section;
        private TextView[] date_section;
        //private TextView first_temperature_section;
        //private TextView second_temperature_section;
        //private TextView third_temperature_section;
        //private TextView fourth_temperature_section;
        //private TextView fifth_temperature_section;
        //private TextView sixth_temperature_section;
        //private TextView seventh_temperature_section;
        //private TextView eight_temperature_section;
        //private TextView ninth_temperature_section;
        //private TextView tenth_temperature_section;


        private ImageView[] weather_image;
        //private ImageView first_weather_image;
        //private ImageView second_weather_image;
        //private ImageView third_weather_image;
        //private ImageView fourth_weather_image;
        //private ImageView fifth_weather_image;
        //private ImageView sixth_weather_image;
        //private ImageView seventh_weather_image;
        //private ImageView eight_weather_image;
        //private ImageView ninth_weather_image;
        //private ImageView tenth_weather_image;

        //private Weather weather;
        private WeatherAPI weatherAPI;
        private TextView town;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            weatherAPI = new WeatherAPI("gismetio", "https://www.gismeteo.com/weather-moscow-4368/10-days/");
            //weather = new Weather();



            UpdateWeatherAdditionalItems();
            UpdateWeatherMainItems();

            navigationView.NavigationItemSelected += OnNavigationItemSelected;
        }

        private void UpdateWeatherAdditionalItems()
        {
            //TextViews
            date_section = new TextView[10];

            date_section[0] = FindViewById<TextView>(Resource.Id.first_date_section);
            date_section[1] = FindViewById<TextView>(Resource.Id.second_date_section);
            date_section[2] = FindViewById<TextView>(Resource.Id.third_date_section);
            date_section[3] = FindViewById<TextView>(Resource.Id.fourth_date_section);
            date_section[4] = FindViewById<TextView>(Resource.Id.fifth_date_section);
            date_section[5] = FindViewById<TextView>(Resource.Id.sixth_date_section);
            date_section[6] = FindViewById<TextView>(Resource.Id.seventh_date_section);
            date_section[7] = FindViewById<TextView>(Resource.Id.eight_date_section);
            date_section[8] = FindViewById<TextView>(Resource.Id.ninth_date_section);
            date_section[9] = FindViewById<TextView>(Resource.Id.tenth_date_section);

            temperature_section = new TextView[10];

            //TextViews
            temperature_section[0] = FindViewById<TextView>(Resource.Id.first_temperature_section);
            temperature_section[1] = FindViewById<TextView>(Resource.Id.second_temperature_section);
            temperature_section[2] = FindViewById<TextView>(Resource.Id.third_temperature_section);
            temperature_section[3] = FindViewById<TextView>(Resource.Id.fourth_temperature_section);
            temperature_section[4] = FindViewById<TextView>(Resource.Id.fifth_temperature_section);
            temperature_section[5] = FindViewById<TextView>(Resource.Id.sixth_temperature_section);
            temperature_section[6] = FindViewById<TextView>(Resource.Id.seventh_temperature_section);
            temperature_section[7] = FindViewById<TextView>(Resource.Id.eight_temperature_section);
            temperature_section[8] = FindViewById<TextView>(Resource.Id.ninth_temperature_section);
            temperature_section[9] = FindViewById<TextView>(Resource.Id.tenth_temperature_section);

            for (int i = 0;  i < 10; i++)
            {
                temperature_section[i].Text = weatherAPI.GetMaxTemperatureTenDays()[i] + "°";
            }

            //ImageViews
            weather_image = new ImageView[10];

            weather_image[0] = FindViewById<ImageView>(Resource.Id.first_weather_image);
            weather_image[1] = FindViewById<ImageView>(Resource.Id.second_weather_image);
            weather_image[2] = FindViewById<ImageView>(Resource.Id.third_weather_image);
            weather_image[3] = FindViewById<ImageView>(Resource.Id.fourth_weather_image);
            weather_image[4] = FindViewById<ImageView>(Resource.Id.fifth_weather_image);
            weather_image[5] = FindViewById<ImageView>(Resource.Id.sixth_weather_image);
            weather_image[6] = FindViewById<ImageView>(Resource.Id.seventh_weather_image);
            weather_image[7] = FindViewById<ImageView>(Resource.Id.eight_weather_image);
            weather_image[8] = FindViewById<ImageView>(Resource.Id.ninth_weather_image);
            weather_image[9] = FindViewById<ImageView>(Resource.Id.tenth_weather_image);
        }

        private void UpdateWeatherMainItems()
        {
            currentWeatherImage = FindViewById<ImageView>(Resource.Id.weatherImage);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            
            town = FindViewById<TextView>(Resource.Id.town);
            weatherType = FindViewById<TextView>(Resource.Id.weatherType);
            windSpeed = FindViewById<TextView>(Resource.Id.windSpeed);
            temperature = FindViewById<TextView>(Resource.Id.temperature);
            weatherPressure = FindViewById<TextView>(Resource.Id.weatherPressure);
            weatherHumidity = FindViewById<TextView>(Resource.Id.weatherHumidity);

            currentWeatherImage.SetImageResource(Resource.Drawable.clouds);

            town.Text = weatherAPI.GetTown();
            weatherType.Text += weatherAPI.GetWeatherType()[0];

            weatherPressure.Text += weatherAPI.GetPressure()[0] + " mm.Hg";
            weatherHumidity.Text += weatherAPI.GetHumidity()[0] + " %";

            switch (UserData.UserData.temperatureFormat)
            {
                case "C°":
                    temperature.Text = weatherAPI.GetMaxTemperatureTenDays()[0] + " C°";
                    break;
                case "F°":
                    temperature.Text = weatherAPI.ConvertCelsiusToFahrenheit(weatherAPI.GetMaxTemperatureTenDays()[0]) + " F°";
                    break;
                default:
                    temperature.Text = weatherAPI.GetMaxTemperatureTenDays()[0] + " C°";
                    break;
            }

            switch (UserData.UserData.windSpeedFormat)
            {
                case "m/s":
                    windSpeed.Text += weatherAPI.GetWindSpeed()[0] + " m/s";
                    break;
                case "km/h":
                    windSpeed.Text += weatherAPI.ConvertToKilometersPerHour(weatherAPI.GetWindSpeed()[0]) + " km/h";
                    break;
                default:
                    windSpeed.Text += weatherAPI.GetWindSpeed()[0] + " m/s";
                    break;
            }
            
            for (int i = 0; i < 10; i++)
            {
                switch (weatherAPI.GetIconInfo(i))
                {
                    case "clouds_grey":
                        weather_image[i].SetImageResource(Resource.Drawable.clouds_grey);
                        break;
                    case "rain_grey":
                        weather_image[i].SetImageResource(Resource.Drawable.rain_grey);
                        break;
                    case "snow_grey":
                        weather_image[i].SetImageResource(Resource.Drawable.snow_grey);
                        break;
                    case "storm_grey":
                        weather_image[i].SetImageResource(Resource.Drawable.storm_grey);
                        break;
                    case "sun_grey":
                        weather_image[i].SetImageResource(Resource.Drawable.sun_grey);
                        break;
                    default:
                        weather_image[i].SetImageResource(Resource.Drawable.sun_grey);
                        break;
                }
            }

            int day = DateTime.Now.Day;
            for (int i = 0; i < 10; i++)
            {
                date_section[i].Text = day.ToString() + "th";
             
                if (day < DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
                    day++;
                else
                    day = 1;
            }


        }

        public void OnNavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.home):
                    e.MenuItem.SetCheckable(true);
                    Toast.MakeText(this, "Home selected", ToastLength.Short).Show();

                    break;

                case (Resource.Id.settings):
                    e.MenuItem.SetCheckable(true);
                    Toast.MakeText(this, "Settings selected", ToastLength.Short).Show();

                    Intent intent = new Intent(this, typeof(Settings));
                    this.StartActivity(intent);

                    break;
            }

            drawerLayout.CloseDrawers();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}