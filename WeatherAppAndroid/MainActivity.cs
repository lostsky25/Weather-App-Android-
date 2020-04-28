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

namespace WeatherAppAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public SupportToolbar mToolbar;

        DrawerLayout drawerLayout;
        NavigationView navigationView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            WeatherGet weather = new WeatherGet();
            var currentWeatherImage = FindViewById<ImageView>(Resource.Id.weatherImage);
            
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            var town = FindViewById<TextView>(Resource.Id.town);
            var weatherType = FindViewById<TextView>(Resource.Id.weatherType);
            var temperature = FindViewById<TextView>(Resource.Id.temperature);

            currentWeatherImage.SetImageResource(Resource.Drawable.clouds);

            town.Text = weather.weatherTemplate.town;
            weatherType.Text = weather.weatherTemplate.weather[0].main;
            temperature.Text = weather.ConvertToCelsius(weather.weatherTemplate.mainInfo.temperature) + '°';
            
            navigationView.NavigationItemSelected += OnNavigationItemSelected;
        }

        private void OnNavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.home):
                    e.MenuItem.SetCheckable(true);
                    Toast.MakeText(this, "Home selected", ToastLength.Short).Show();

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