using CkziuApp.Interfaces;
using CkziuApp.UWP.DependencySvr;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;

[assembly: Xamarin.Forms.Dependency(typeof(CrossPlatformDownloadManager))]
namespace CkziuApp.UWP.DependencySvr
{
    public class CrossPlatformDownloadManager : ICrossPlatformDownloadManager
    {
        public async Task<string> DownloadFileAsync(string url, string fileName)
        {
            try
            {
                HttpClient client = new HttpClient(); // Create HttpClient
                byte[] buffer = await client.GetByteArrayAsync(url); // Download file

                StorageFile destinationFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                fileName, CreationCollisionOption.ReplaceExisting);

                using (Stream stream = await destinationFile.OpenStreamForWriteAsync())
                    stream.Write(buffer, 0, buffer.Length); // Save
                return destinationFile.Path;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string DefaultPathToDownloadedFiles { get { return ApplicationData.Current.LocalFolder.Path; } }
        public bool DownloadedFileExists(string path)
        {
            if (System.IO.File.Exists(path))
                return true;
            return false;
        }
    }
}
