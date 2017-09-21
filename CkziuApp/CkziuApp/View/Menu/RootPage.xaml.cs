using CkziuApp.View.Pages;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CkziuApp.View.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : MasterDetailPage
    {
        Color color = Color.FromHex("#0288d1");
        public RootPage(PlanPage planPage)
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;

            MenuPage menuPage = new MenuPage();
            menuPage.ListView.ItemSelected += OnItemSelectedAsync;

            Master = menuPage;
            NavigationPage navigationPage = new NavigationPage(planPage);
            navigationPage.BarBackgroundColor = color;
            Detail = navigationPage;
        }

        private async void OnItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuItem;
            if (item != null)
            {
                NavigationPage navigationPage = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                navigationPage.BarBackgroundColor = color;
                Detail = navigationPage;
                await Task.Delay(10);
                IsPresented = false;
            }
        }
    }
}
