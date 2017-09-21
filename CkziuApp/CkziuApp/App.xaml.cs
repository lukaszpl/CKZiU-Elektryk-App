using CkziuApp.View.Menu;
using CkziuApp.View.Pages;
using Xamarin.Forms;

namespace CkziuApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            PlanPage planPage = new PlanPage();
            RootPage rootPage = new RootPage(planPage);
            MainPage = rootPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
