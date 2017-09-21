using Xamarin.Forms;

namespace CkziuApp.Logic
{
    class Settings
    {
        public static string MetaServer { get { return "http://csharp-dev.pl/Elektryk/"; } }
        public static void Save()
        {
            Application.Current.Properties["BranchType"] = ChoosedBranch.BranchType;
            Application.Current.Properties["ID"] = ChoosedBranch.id;
            Application.Current.Properties["Name"] = ChoosedBranch.Name;
        }
        public static void Load()
        {
            if ((Application.Current.Properties.ContainsKey("ID") && (Application.Current.Properties.ContainsKey("BranchType") && (Application.Current.Properties.ContainsKey("Name")))))
            {
                ChoosedBranch.BranchType = (string)Application.Current.Properties["BranchType"];
                ChoosedBranch.id = (int)Application.Current.Properties["ID"];
                ChoosedBranch.Name = (string)Application.Current.Properties["Name"];
            }
        }
    }
}
