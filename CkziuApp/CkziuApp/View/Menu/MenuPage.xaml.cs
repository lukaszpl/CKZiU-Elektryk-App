using CkziuApp.View.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CkziuApp.View.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public ListView ListView { get => listView; set => listView = value; }
        public MenuPage()
        {
            InitializeComponent();
            var assembly = typeof(CkziuApp.App);
            Logo.Source = ImageSource.FromResource("CkziuApp.Resources.ckziu.png", assembly);

            List<MenuItem> menuItems = new List<MenuItem>();
            menuItems.Add(new MenuItem("Plan", ImageSource.FromResource("CkziuApp.Resources.plan.png", assembly), typeof(PlanPage)));
            menuItems.Add(new MenuItem("Zastępstwa", ImageSource.FromResource("CkziuApp.Resources.replacement.png", assembly), typeof(ReplacementPage)));
            menuItems.Add(new MenuItem("Dodatkowe dni wolne", ImageSource.FromResource("CkziuApp.Resources.freedays.png", assembly), typeof(FreeDaysPage)));
            menuItems.Add(new MenuItem("O aplikacji", ImageSource.FromResource("CkziuApp.Resources.about.png", assembly), typeof(AboutPage)));
            listView.ItemsSource = menuItems;
        }
    }
    public class MenuItem
    {
        public string Title { get; set; }
        public ImageSource IconSource { get; set; }
        public Type TargetType { get; set; }
        public MenuItem(string Title, ImageSource IconSource, Type TargetType)
        {
            this.Title = Title;
            this.IconSource = IconSource;
            this.TargetType = TargetType;
        }
    }
}
