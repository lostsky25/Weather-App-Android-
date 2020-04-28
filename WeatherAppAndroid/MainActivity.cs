using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using WeatherApp.Weather;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace WeatherAppAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public SupportToolbar mToolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //WeatherGet weather = new WeatherGet();
            //var currentWeatherImage = FindViewById<ImageView>(Resource.Id.weatherImage);
            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(mToolbar);
            //var town = FindViewById<TextView>(Resource.Id.town);
            //var weatherType = FindViewById<TextView>(Resource.Id.weatherType);
            //var temperature = FindViewById<TextView>(Resource.Id.temperature);

            //currentWeatherImage.SetImageResource(Resource.Drawable.clouds);

            //town.Text = weather.weatherTemplate.town;
            //weatherType.Text = weather.weatherTemplate.weather[0].main;
            //temperature.Text = weather.ConvertToCelsius(weather.weatherTemplate.mainInfo.temperature) + '°';

            //Monday.Text = weather.weatherTemplate.weather[0].description;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}