using CkziuApp.Logic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;
using CkziuApp.Interfaces;
using CkziuApp.Logic.XmlPlanStructure;


namespace CkziuApp.ViewModel.Pages
{
    public class PlanPageViewModel : ViewModelBase
    {
        #region DayItems
        private ObservableCollection<LessonItem> _MondayItems = new ObservableCollection<LessonItem>();
        public ObservableCollection<LessonItem> MondayItems
        {
            get { return _MondayItems; }
            set { Set(() => MondayItems, ref _MondayItems, value); }
        }
        private ObservableCollection<LessonItem> _TuesdayItems = new ObservableCollection<LessonItem>();
        public ObservableCollection<LessonItem> TuesdayItems
        {
            get { return _TuesdayItems; }
            set { Set(() => TuesdayItems, ref _TuesdayItems, value); }
        }
        private ObservableCollection<LessonItem> _WednesdayItems = new ObservableCollection<LessonItem>();
        public ObservableCollection<LessonItem> WednesdayItems
        {
            get { return _WednesdayItems; }
            set { Set(() => WednesdayItems, ref _WednesdayItems, value); }
        }
        private ObservableCollection<LessonItem> _ThursdayItems = new ObservableCollection<LessonItem>();
        public ObservableCollection<LessonItem> ThursdayItems
        {
            get { return _ThursdayItems; }
            set { Set(() => ThursdayItems, ref _ThursdayItems, value); }
        }
        private ObservableCollection<LessonItem> _FridayItems = new ObservableCollection<LessonItem>();
        public ObservableCollection<LessonItem> FridayItems
        {
            get { return _FridayItems; }
            set { Set(() => FridayItems, ref _FridayItems, value); }
        }
        #endregion

        #region OtherFields
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
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { Set(() => Title, ref _Title, value); }
        }
        #endregion

        #region commands
        public ICommand RefreshCommand { get; set; }
        public ICommand ChooseCommand { get; set; }
        #endregion

        public PlanPageViewModel()
        {
            IsLoading = false;
            IsListViewsVisible = true;
            RefreshCommand = new RelayCommand(RefreshPlan);
            ChooseCommand = new RelayCommand(ChooseGroupAsync);
            Start();
        }

        private async void Start()
        {
            IsLoading = true;
            IsListViewsVisible = false;

            string path = DependencyService.Get<ICrossPlatformDownloadManager>().DefaultPathToDownloadedFiles;
            string finalPath = Path.Combine(path, "plan.xml");
            bool FileExists = DependencyService.Get<ICrossPlatformDownloadManager>().DownloadedFileExists(finalPath);
            if ((Application.Current.Properties.Count <= 0) || (!FileExists))
            {
                string DownloadPath = await DependencyService.Get<ICrossPlatformDownloadManager>().DownloadFileAsync(Settings.MetaServer + "plan.xml", "plan.xml");
                if (DownloadPath == null)
                {
                    await App.Current.MainPage.DisplayAlert("Błąd", "Brak połączenia z internetem!", "Anuluj");
                    DependencyService.Get<IMyApplication>().Exit();
                }
                else
                    await ChoosedBranch.ShowDialogChooseBranch(DownloadPath);
            }
            else
                Settings.Load();
            LoadPlan(finalPath);

            IsLoading = false;
            IsListViewsVisible = true;
        }

        private async void ChooseGroupAsync()
        {
            if (!IsLoading)
            {
                string path = DependencyService.Get<ICrossPlatformDownloadManager>().DefaultPathToDownloadedFiles;
                string finalPath = Path.Combine(path, "plan.xml");
                bool FileExists = DependencyService.Get<ICrossPlatformDownloadManager>().DownloadedFileExists(finalPath);
                if (FileExists)
                {
                    IsLoading = true;
                    IsListViewsVisible = false;
                    await ChoosedBranch.ShowDialogChooseBranch(finalPath);
                    LoadPlan(finalPath);
                }
                else
                    await App.Current.MainPage.DisplayAlert("Błąd", "Nie można odnaleźć pliku!", "OK");
                IsLoading = false;
                IsListViewsVisible = true;
            }
        }

        private async void RefreshPlan()
        {
            if (!IsLoading)
            {
                IsLoading = true;
                IsListViewsVisible = false;
                string path = await DependencyService.Get<ICrossPlatformDownloadManager>().DownloadFileAsync(Settings.MetaServer + "plan.xml", "plan.xml");
                if (path != null)
                    LoadPlan(path);
                else
                    await App.Current.MainPage.DisplayAlert("Błąd", "Brak połączenia z internetem!", "Anuluj");
                IsLoading = false;
                IsListViewsVisible = true;
            }
        }

        private async void LoadPlan(string path)
        {
            Title = ChoosedBranch.Name;
            ClearPlan();

            try
            {
                XmlPlanInterpreter xmlInterpreter = new XmlPlanInterpreter(path);
                //
                if (IsChangesInListOfBranches(xmlInterpreter))
                {
                    ChoosedBranch.id = -1;

                    await ChoosedBranch.ShowDialogChooseBranch(path);
                    Title = ChoosedBranch.Name;
                }
                if (ChoosedBranch.id == -1)
                    Title = "Nie wybrano klasy!";
                //
                List<string> Hours = xmlInterpreter.GetHours();
                for (int a = 0; a != 5; a++)
                {
                    if (a == 0)
                    {
                        for (int n = 1; n <= Hours.Count; n++)
                            MondayItems.Add(new LessonItem(n.ToString(), Hours[n - 1], xmlInterpreter.GetPlanForHour(ChoosedBranch.BranchType, ChoosedBranch.id, n, "Poniedziałek"), 14));
                    }
                    if (a == 1)
                    {
                        for (int n = 1; n <= Hours.Count; n++)
                            TuesdayItems.Add(new LessonItem(n.ToString(), Hours[n - 1], xmlInterpreter.GetPlanForHour(ChoosedBranch.BranchType, ChoosedBranch.id, n, "Wtorek"), 14));
                    }
                    if (a == 2)
                    {
                        for (int n = 1; n <= Hours.Count; n++)
                            WednesdayItems.Add(new LessonItem(n.ToString(), Hours[n - 1], xmlInterpreter.GetPlanForHour(ChoosedBranch.BranchType, ChoosedBranch.id, n, "Środa"), 14));
                    }
                    if (a == 3)
                    {
                        for (int n = 1; n <= Hours.Count; n++)
                            ThursdayItems.Add(new LessonItem(n.ToString(), Hours[n - 1], xmlInterpreter.GetPlanForHour(ChoosedBranch.BranchType, ChoosedBranch.id, n, "Czwartek"), 14));
                    }
                    if (a == 4)
                    {
                        for (int n = 1; n <= Hours.Count; n++)
                            FridayItems.Add(new LessonItem(n.ToString(), Hours[n - 1], xmlInterpreter.GetPlanForHour(ChoosedBranch.BranchType, ChoosedBranch.id, n, "Piątek"), 14));
                    }
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Błąd!", "Wystąpił błąd podczas odczytywania pliku z planem. Prawdopodobnie plik jest uszkodzony lub używasz nieaktualnej wersji programu!", "OK");
            }
        }

        private bool IsChangesInListOfBranches(XmlPlanInterpreter xmlInterpreter)
        {
            if(ChoosedBranch.BranchType == "group")
            {
                foreach(Group group in xmlInterpreter.GetGroupsElements())
                {
                    if (group.gid == ChoosedBranch.id)
                        if (group.name != ChoosedBranch.Name)
                            return true;
                }                              
            }
            if(ChoosedBranch.BranchType == "teacher")
            {
                foreach (Teacher teacher in xmlInterpreter.GetTeachersElements())
                {
                    if (teacher.tid == ChoosedBranch.id)
                        if (teacher.name != ChoosedBranch.Name)
                            return true;
                }
            }
            if(ChoosedBranch.BranchType == "room")
            {
                foreach (Room room in xmlInterpreter.GetRoomsElements())
                {
                    if (room.rid == ChoosedBranch.id)
                        if (room.name != ChoosedBranch.Name)
                            return true;
                }
            }
            return false;
        }

        private void SetPlanHeaders()
        {
            MondayItems.Add(new LessonItem("Nr", "Godzina", "Nazwa", 16));
            TuesdayItems.Add(new LessonItem("Nr", "Godzina", "Nazwa", 16));
            WednesdayItems.Add(new LessonItem("Nr", "Godzina", "Nazwa", 16));
            ThursdayItems.Add(new LessonItem("Nr", "Godzina", "Nazwa", 16));
            FridayItems.Add(new LessonItem("Nr", "Godzina", "Nazwa", 16));
        }

        private void ClearPlan()
        {
            MondayItems.Clear();
            TuesdayItems.Clear();
            WednesdayItems.Clear();
            ThursdayItems.Clear();
            FridayItems.Clear();
            SetPlanHeaders();
        }
    }
    public class LessonItem
    {
        public string Number { get; set; }
        public string Hour { get; set; }
        public string Lesson { get; set; }
        public int FontSize { get; set; }
        public LessonItem(string Number, string Hour, string Lesson, int FontSize)
        {
            this.Number = Number;
            this.Hour = Hour;
            this.Lesson = Lesson;
            this.FontSize = FontSize;
        }
    }
}
