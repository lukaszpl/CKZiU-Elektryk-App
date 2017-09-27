using Android.App;
using Android.Content;
using Android.OS;
using Android.Content.PM;

namespace CkziuApp.Droid
{
    [Activity(Label = "CKZiU Elektryk", MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash", 
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }
    }
}