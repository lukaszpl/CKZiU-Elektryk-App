using CkziuApp.Interfaces;
using Windows.ApplicationModel.Core;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(CkziuApp.UWP.DependencySvr.MyApplication))]
namespace CkziuApp.UWP.DependencySvr
{
    public class MyApplication : IMyApplication
    {
        public void Exit()
        {
            CoreApplication.Exit();
        }
    }
}