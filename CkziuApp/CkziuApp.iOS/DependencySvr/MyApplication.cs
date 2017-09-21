using CkziuApp.Interfaces;
using System.Threading;

[assembly: Xamarin.Forms.Dependency(typeof(CkziuApp.iOS.DependencySvr.MyApplication))]
namespace CkziuApp.iOS.DependencySvr
{
    public class MyApplication : IMyApplication
    {
        public void Exit()
        {
            Thread.CurrentThread.Abort();
        }
    }
}