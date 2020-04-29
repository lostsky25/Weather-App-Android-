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
        private Weather weather;
        private TextView town;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            weather = new Weather();
            UpdateWeatherMainItems();

            navigationView.NavigationItemSelected += OnNavigationItemSelected;
        
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

            town.Text = weather.weatherTemplate.town;
            weatherType.Text += weather.weatherTemplate.weather[0].main;

            weatherPressure.Text += weather.weatherTemplate.mainInfo.pressure.Substring(0,
                weather.weatherTemplate.mainInfo.pressure.LastIndexOf('.')) + " mm.Hg";
            weatherHumidity.Text += weather.weatherTemplate.mainInfo.humidity + " %";

            switch (UserData.UserData.temperatureFormat)
            {
                case "C°":
                    temperature.Text = weather.ConvertToCelsius(weather.weatherTemplate.mainInfo.temperature) + " C°";
                    break;
                case "F°":
                    temperature.Text = weather.ConvertToFahrenheit(weather.weatherTemplate.mainInfo.temperature) + " F°";
                    break;
                default:
                    temperature.Text = weather.ConvertToCelsius(weather.weatherTemplate.mainInfo.temperature) + " C°";
                    break;
            }

            switch (UserData.UserData.windSpeedFormat)
            {
                case "m/s":
                    windSpeed.Text += weather.weatherTemplate.wind.speed.Substring(0,
                    weather.weatherTemplate.wind.speed.LastIndexOf('.')) + " m/s"; 
                    break;
                case "km/h":
                    windSpeed.Text += weather.ConvertToKilometersPerHour(weather.weatherTemplate.wind.speed.Substring(0,
                    weather.weatherTemplate.wind.speed.LastIndexOf('.'))) + " km/h";
                    break;
                default:
                    windSpeed.Text += weather.weatherTemplate.wind.speed.Substring(0,
                    weather.weatherTemplate.wind.speed.LastIndexOf('.')) + " m/s";
                    break;
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