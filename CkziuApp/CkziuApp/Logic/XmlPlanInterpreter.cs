using System;
using System.Collections.Generic;
using System.Xml.Linq;
using CkziuApp.Logic.XmlPlanStructure;
namespace CkziuApp.Logic
{
    public class XmlPlanInterpreter
    {
        public XDocument xmlDoc;
        public XmlPlanInterpreter(string Path)
        {
            xmlDoc = XDocument.Load(Path);
        }

        public List<Teacher> GetTeachersElements()
        {
            int i = 0;
            List<Teacher> TeacherList = new List<Teacher>();
            foreach (var element in xmlDoc.Descendants("teacher"))
                TeacherList.Add(new Teacher(i++, Convert.ToInt16(element.Attribute("tid").Value), element.Value));
            return TeacherList;
        }

        public List<Room> GetRoomsElements()
        {
            int i = 0;
            List<Room> RoomList = new List<Room>();
            foreach (var element in xmlDoc.Descendants("room"))
                RoomList.Add(new Room(i++, Convert.ToInt16(element.Attribute("rid").Value), element.Value));
            return RoomList;
        }

        public List<Group> GetGroupsElements()
        {
            int i = 0;
            List<Group> GroupList = new List<Group>();
            foreach (var element in xmlDoc.Descendants("group"))
                GroupList.Add(new Group(i++, Convert.ToInt16(element.Attribute("gid").Value), element.Value));
            return GroupList;
        }

        public List<string> GetHours()
        {
            List<string> HoursList = new List<string>();
            foreach (var element in xmlDoc.Descendants("otime"))
                HoursList.Add(element.Value);
            return HoursList;
        }
        /*            */

        public string GetPlanForHour(string Type, int id, int LessonNumber, string Day)
        {
            string _lesson = null;
            foreach (var day in xmlDoc.Descendants("weekday"))
            {
                if ((day.Attribute("name").Value == Day))
                {
                    foreach (var time in day.Descendants("time"))
                    {
                        if (time.Attribute("no").Value == LessonNumber.ToString())
                        {
                            foreach (var lesson in time.Descendants("class"))
                            {
                                if (lesson.Attribute(Type).Value == id.ToString())
                                {
                                    _lesson += lesson.Value;
                                    if (Type == "teacher")
                                    {
                                        _lesson += " " + GetBranchNameById("group", Convert.ToInt16(lesson.Attribute("group").Value));
                                        _lesson += " s." + GetBranchNameById("room", Convert.ToInt16(lesson.Attribute("room").Value));
                                    }
                                    if (Type == "group")
                                    {
                                        _lesson += " " + GetBranchNameById("teacher", Convert.ToInt16(lesson.Attribute("teacher").Value));
                                        _lesson += " s." + GetBranchNameById("room", Convert.ToInt16(lesson.Attribute("room").Value));
                                    }
                                    if (Type == "room")
                                    {
                                        _lesson += " n." + GetBranchNameById("teacher", Convert.ToInt16(lesson.Attribute("teacher").Value));
                                        _lesson += " kl." + GetBranchNameById("group", Convert.ToInt16(lesson.Attribute("group").Value));
                                    }
                                    _lesson += Environment.NewLine;
                                }
                            }
                        }
                    }
                }
            }
            return _lesson;
        }

        private string GetBranchNameById(string Branch, int id)
        {
            if (Branch == "teacher")
            {
                foreach(Teacher teacher in GetTeachersElements())
                {
                    if (teacher.tid == id)
                        return teacher.name;
                }
            }
            if (Branch == "group")
            {
                foreach (Group group in GetGroupsElements())
                {
                    if (group.gid == id)
                        return group.name;
                }
            }
            if (Branch == "room")
            {
                foreach (Room room in GetRoomsElements())
                {
                    if (room.rid == id)
                        return room.name;
                }
            }
            return null;
        }
    }
}
