using Android.App;
using CkziuApp.Interfaces;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(CkziuApp.Droid.DependencySvr.MyApplication))]
namespace CkziuApp.Droid.DependencySvr
{
    public class MyApplication : IMyApplication
    {
        public void Exit()
        {
            var activity = (Activity)Forms.Context;
            activity.Finish();
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}