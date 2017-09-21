using CkziuApp.Logic.XmlPlanStructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CkziuApp.Logic
{
    public class ChoosedBranch
    {
        public static string BranchType = "group";
        public static int id = -1;
        public static string Name;

        public static async Task ShowDialogChooseBranch(string DownloadPath)
        {
            try
            {
                XmlPlanInterpreter xmlInterpreter = new XmlPlanInterpreter(DownloadPath);

                List<string> strlist = new List<string>();
                List<int> intlist = new List<int>();

                List<Group> listOfGroups = new List<Group>();
                List<Teacher> listOfTeachers = new List<Teacher>();
                List<Room> listOfRooms = new List<Room>();

                listOfGroups.AddRange(xmlInterpreter.GetGroupsElements());
                listOfTeachers.AddRange(xmlInterpreter.GetTeachersElements());
                listOfRooms.AddRange(xmlInterpreter.GetRoomsElements());

                for (int i = 0; i < listOfGroups.Count; i++)
                {
                    strlist.Add(listOfGroups[i].name);
                    intlist.Add(listOfGroups[i].gid);
                }
                for (int i = 0; i < listOfTeachers.Count; i++)
                {
                    strlist.Add(listOfTeachers[i].name);
                    intlist.Add(listOfTeachers[i].tid);
                }
                for (int i = 0; i < listOfGroups.Count; i++)
                {
                    strlist.Add(listOfRooms[i].name);
                    intlist.Add(listOfRooms[i].rid);
                }

                string result = await App.Current.MainPage.DisplayActionSheet("Wybierz klasę", "Anuluj", null, strlist.ToArray());
                foreach (Group item in listOfGroups)
                {
                    if (item.name == result)
                    {
                        ChoosedBranch.BranchType = "group";
                        ChoosedBranch.id = item.gid;
                    }
                }
                foreach (Teacher item in listOfTeachers)
                {
                    if (item.name == result)
                    {
                        ChoosedBranch.BranchType = "teacher";
                        ChoosedBranch.id = item.tid;
                    }
                }
                foreach (Room item in listOfRooms)
                {
                    if (item.name == result)
                    {
                        ChoosedBranch.BranchType = "room";
                        ChoosedBranch.id = item.rid;
                    }
                }
                if ((result != "Anuluj") && (result != null))
                    ChoosedBranch.Name = result;
                Settings.Save();
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Błąd!", "Wystąpił błąd podczas odczytywania pliku z planem. Prawdopodobnie plik jest uszkodzony lub używasz nieaktualnej wersji programu", "OK");
            }
        }
    }
}
