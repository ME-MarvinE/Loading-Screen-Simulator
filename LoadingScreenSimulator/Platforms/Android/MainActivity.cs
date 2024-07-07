using Android.App;
using Android.Content.PM;
using Android.OS;

namespace LoadingScreenSimulator
{
    [Activity(Label = "Loading Screen Simulator", Icon = "@mipmap/icon", Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
    }
}
