using CkziuApp.Interfaces;
using CkziuApp.Logic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;

namespace CkziuApp.ViewModel.Pages
{
    public class FreeDaysPageViewModel : ViewModelBase
    {
        #region Fields
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { Set(() => Title, ref _Title, value); }
        }
        private bool _IsListViewsVisible;
        public bool IsListViewsVisible
        {
            get { return _IsListViewsVisible; }
            set { Set(() => IsListViewsVisible, ref _IsListViewsVisible, value); }
        }
        private bool _IsLoading;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { Set(() => IsLoading, ref _IsLoading, value); }
        }
        private ObservableCollection<FreeDaysItem> _FreeDaysItems = new ObservableCollection<FreeDaysItem>();
        public ObservableCollection<FreeDaysItem> FreeDaysItems
        {
            get { return _FreeDaysItems; }
            set { Set(() => FreeDaysItems, ref _FreeDaysItems, value); }
        }
        #endregion

        public ICommand RefreshCommand { get; set; }
        public FreeDaysPageViewModel()
        {
            RefreshCommand = new RelayCommand(RefreshPlan);
            Title = "Dni wolne";
            Start();
            
        }

        private async void RefreshPlan()
        {
            if (!IsLoading)
            {
                IsListViewsVisible = false;
                IsLoading = true;

                string DownloadPath = await DependencyService.Get<ICrossPlatformDownloadManager>().DownloadFileAsync(Settings.MetaServer + "freedays.xml", "freedays.xml");
                if (DownloadPath == null)
                    await App.Current.MainPage.DisplayAlert("Błąd", "Brak połączenia z internetem!", "Anuluj");
                else
                    LoadFreeDays(DownloadPath);

                IsListViewsVisible = true;
                IsLoading = false;
            }
        }

        private async void Start()
        {
            IsListViewsVisible = false;
            IsLoading = true;

            string path = DependencyService.Get<ICrossPlatformDownloadManager>().DefaultPathToDownloadedFiles;
            string finalPath = Path.Combine(path, "freedays.xml");
            bool FileExists = DependencyService.Get<ICrossPlatformDownloadManager>().DownloadedFileExists(finalPath);
            if (!FileExists)
            {
                string DownloadPath = await DependencyService.Get<ICrossPlatformDownloadManager>().DownloadFileAsync(Settings.MetaServer + "freedays.xml", "freedays.xml");
                if (DownloadPath == null)
                    await App.Current.MainPage.DisplayAlert("Błąd", "Brak połączenia z internetem!", "Anuluj");
                else
                    LoadFreeDays(finalPath);
            }
            else
                LoadFreeDays(finalPath);

            IsListViewsVisible = true;
            IsLoading = false;
        }

        private async void LoadFreeDays(string path)
        {
            try
            {
                FreeDaysItems.Clear();
                bool i = true;
                XmlFreeDaysInterpreter xmlInterpreter = new XmlFreeDaysInterpreter(path);
                Title = xmlInterpreter.GetTitle();
                foreach(FreeDayItem item in xmlInterpreter.GetFreeDays())
                {
                    if(i)
                        FreeDaysItems.Add(new FreeDaysItem(item.Date, item.Content, "#f35e20", "Black"));
                    else
                        FreeDaysItems.Add(new FreeDaysItem(item.Date, item.Content, "#5c5c5c", "Gray"));
                    i = !i;
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Błąd!", "Wystąpił błąd podczas odczytywania pliku z dniami wolnymi. Prawdopodobnie plik jest uszkodzony lub używasz nieaktualnej wersji programu!", "OK");
            }
        }
    }
    public class FreeDaysItem
    {
        public string Date { get; set; }
        public string Content { get; set; }
        public string TextColorFirstColumn { get; set; }
        public string TextColorSecondColumn { get; set; }
        public FreeDaysItem(string Date, string Content, string TextColorFirstColumn, string TextColorSecondColumn)
        {
            this.Date = Date;
            this.Content = Content;
            this.TextColorFirstColumn = TextColorFirstColumn;
            this.TextColorSecondColumn = TextColorSecondColumn;
        }
    }
}
