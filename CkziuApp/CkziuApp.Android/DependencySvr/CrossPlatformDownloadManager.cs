using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CkziuApp.Interfaces;
using System.Net;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(CkziuApp.Droid.DependencySvr.CrossPlatformDownloadManager))]
namespace CkziuApp.Droid.DependencySvr
{
    public class CrossPlatformDownloadManager : ICrossPlatformDownloadManager
    {
        public async Task<string> DownloadFileAsync(string url, string FileName)
        {     
            try
            {
                string path = Path.Combine(Android.App.Application.Context.GetExternalFilesDir(null).Path, FileName);
                WebClient webClent = new WebClient();
                await Task.Run(() => webClent.DownloadFile(url, path+".test")); //test download file
                await Task.Run(() => webClent.DownloadFile(url, path));
                return path;
            }
            catch(Exception)
            {
                return null;
            }
        }
        public string DefaultPathToDownloadedFiles { get { return Android.App.Application.Context.GetExternalFilesDir(null).Path; } }
        public bool DownloadedFileExists(string path)
        {
            if (System.IO.File.Exists(path))
                return true;
            return false;
        }
    }
}