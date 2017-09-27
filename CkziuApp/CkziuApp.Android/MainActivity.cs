using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Content;

namespace CkziuApp.Droid
{
    [Activity(Label = "CKZiU Elektryk", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            AndroidEnvironment.UnhandledExceptionRaiser += HandleAndroidException;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        private void HandleAndroidException(object sender, RaiseThrowableEventArgs e)
        {
            Intent intent = new Intent(this, typeof(CrashActivity));
            intent.PutExtra("Error_Text", e.Exception.Message);
            intent.SetFlags(ActivityFlags.NewTask);
            this.StartActivity(intent);          
            Java.Lang.JavaSystem.Exit(0); // Close this app process
        }
    }
}

