using CkziuApp.Interfaces;
using CkziuApp.iOS.DependencySvr;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PCLStorage;

[assembly: Xamarin.Forms.Dependency(typeof(CrossPlatformDownloadManager))]
namespace CkziuApp.iOS.DependencySvr
{
    public class CrossPlatformDownloadManager : ICrossPlatformDownloadManager
    {
        public async Task<string> DownloadFileAsync(string url, string FileName)
        {
            string path = Path.Combine(FileSystem.Current.LocalStorage.Path, FileName);
            try
            {
                WebClient webClent = new WebClient();
                webClent.DownloadFile(url, path);
                await Task.Run(() => webClent.DownloadFile(url, path + ".test")); //test download file
                await Task.Run(() => webClent.DownloadFile(url, path));
                return path;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
    public string DefaultPathToDownloadedFiles { get { return FileSystem.Current.LocalStorage.Path; } }
    public bool DownloadedFileExists(string path)
    {
        if (System.IO.File.Exists(path))
            return true;
        return false;
    }
}
