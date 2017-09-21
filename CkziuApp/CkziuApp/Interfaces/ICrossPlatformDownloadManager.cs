using System.Threading.Tasks;

namespace CkziuApp.Interfaces
{
    public interface ICrossPlatformDownloadManager
    {
        string DefaultPathToDownloadedFiles { get; }
        Task<string> DownloadFileAsync(string url, string fileName);
        bool DownloadedFileExists(string path);
    }
}
