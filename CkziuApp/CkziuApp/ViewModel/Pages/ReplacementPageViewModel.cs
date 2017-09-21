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
    public class ReplacementPageViewModel : ViewModelBase
    {
        #region Fields
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
        private ObservableCollection<ReplacementItem> _ReplacementItems = new ObservableCollection<ReplacementItem>();
        public ObservableCollection<ReplacementItem> ReplacementItems
        {
            get { return _ReplacementItems; }
            set { Set(() => ReplacementItems, ref _ReplacementItems, value); }
        }
        #endregion

        public ICommand RefreshCommand { get; set; }

        public ReplacementPageViewModel()
        {
            RefreshCommand = new RelayCommand(RefreshPlan);
            Start();
        }

        private async void Start()
        {
            IsListViewsVisible = false;
            IsLoading = true;

            string path = DependencyService.Get<ICrossPlatformDownloadManager>().DefaultPathToDownloadedFiles;
            string finalPath = Path.Combine(path, "replacement.xml");
            bool FileExists = DependencyService.Get<ICrossPlatformDownloadManager>().DownloadedFileExists(finalPath);
            if (!FileExists)
            {
                string DownloadPath = await DependencyService.Get<ICrossPlatformDownloadManager>().DownloadFileAsync(Settings.MetaServer + "zastepstwa.xml", "replacement.xml");
                if (DownloadPath == null)
                    await App.Current.MainPage.DisplayAlert("Błąd", "Brak połączenia z internetem!", "Anuluj");
                else
                    LoadReplacement(finalPath);
            }else
                LoadReplacement(finalPath);

            IsListViewsVisible = true;
            IsLoading = false;
        }

        private async void RefreshPlan()
        {
            if (!IsLoading)
            {
                IsListViewsVisible = false;
                IsLoading = true;

                string DownloadPath = await DependencyService.Get<ICrossPlatformDownloadManager>().DownloadFileAsync(Settings.MetaServer + "zastepstwa.xml", "replacement.xml");
                if (DownloadPath == null)
                    await App.Current.MainPage.DisplayAlert("Błąd", "Brak połączenia z internetem!", "Anuluj");
                else
                    LoadReplacement(DownloadPath);

                IsListViewsVisible = true;
                IsLoading = false;
            }
        }

        private async void LoadReplacement(string path)
        {
            try
            {
                XmlReplacementInterpreter interpreter = new XmlReplacementInterpreter(path);
                ReplacementItems.Clear();
                foreach (var item in interpreter.GetReplacement())
                {
                    if (item.IsTitle)
                        ReplacementItems.Add(new ReplacementItem(0, "", item.Date + " " + item.AbsentTeacher, "Black", 16, FontAttributes.Bold, TextAlignment.Center));
                    else
                        ReplacementItems.Add(new ReplacementItem(20, item.No, item.Group + " " + item.Content + " " + item.Teacher, "Gray", 15, FontAttributes.None, TextAlignment.Start));
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Błąd!", "Wystąpił błąd podczas odczytywania pliku z zastępstwami. Prawdopodobnie plik jest uszkodzony lub używasz nieaktualnej wersji programu!", "OK");
            }
        }
    }
    public class ReplacementItem
    {
        public int ColumnWidth { get; set; }
        public string Number { get; set; }
        public string Content { get; set; }
        public string TextColor { get; set; }
        public int FontSize { get; set;}
        public FontAttributes FontAttributes { get; set; }
        public TextAlignment HorizontalTextAlignment { get; set; }
        public ReplacementItem(int ColumnWidth, string Number, string Content, string TextColor, int FontSize, FontAttributes FontAttributes, TextAlignment HorizontalTextAlignment)
        {
            this.ColumnWidth = ColumnWidth;
            this.Number = Number;
            this.Content = Content;
            this.TextColor = TextColor;
            this.FontSize = FontSize;
            this.FontAttributes = FontAttributes;
            this.HorizontalTextAlignment = HorizontalTextAlignment;
        }
    }
}
