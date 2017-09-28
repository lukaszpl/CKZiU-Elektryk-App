using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Content.PM;

namespace CkziuApp.Droid
{
    [Activity( Label = "CrashDialog", LaunchMode = LaunchMode.SingleTask, Theme = "@style/alert_dialog", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.SmallestScreenSize | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.Navigation)]
    public class CrashActivity : Activity
    {
        private Button btnRestart;
        public string errorMessage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DialogScreen);
            errorMessage = Intent.GetStringExtra("Error_Text");
            this.SetFinishOnTouchOutside(false);
            InitView();
        }

        private void InitView()
        {
            btnRestart = (Button)FindViewById(Resource.Id.exitButton);
            btnRestart.Click += (sender, e) =>
            {
                Exit();
            };
            TextView errorText = FindViewById<TextView>(Resource.Id.crashText);
            errorText.Text = errorMessage;
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            Exit();
        }

        private void Exit()
        {
            Java.Lang.JavaSystem.Exit(0);
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}