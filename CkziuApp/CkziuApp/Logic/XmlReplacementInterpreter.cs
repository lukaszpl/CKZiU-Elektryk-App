using System.Collections.Generic;
using System.Xml.Linq;

namespace CkziuApp.Logic
{
    public class XmlReplacementInterpreter
    {
        public XDocument xmlDoc;
        public XmlReplacementInterpreter(string Path)
        {
            xmlDoc = XDocument.Load(Path);
        }
        public List<ReplacementItem> GetReplacement()
        {
            List<ReplacementItem> list = new List<ReplacementItem>();
            foreach (var box in xmlDoc.Descendants("box"))
            {
                list.Add(new ReplacementItem(box.Attribute("day").Value, box.Attribute("date").Value, null, box.Attribute("name").Value, null, null, null, null, true));
                foreach (var item in box.Descendants("item"))
                {
                    if (list[0].No == null)
                    {
                        list[0].No = item.Attribute("lesson").Value;
                        list[0].Teacher = item.Attribute("teacher").Value;
                        list[0].Group = item.Attribute("group").Value;
                        list[0].Comments = item.Attribute("comments").Value;
                        list[0].Content = item.Value;
                    }
                    list.Add(new ReplacementItem(box.Attribute("day").Value, box.Attribute("date").Value, item.Attribute("lesson").Value, box.Attribute("name").Value, item.Attribute("teacher").Value, item.Attribute("group").Value, item.Attribute("comments").Value, item.Value, false));
                }
            }
            return list;
        }
    }
    public class ReplacementItem
    {
        public string Day { get; set; }
        public string Date { get; set; }
        public string No { get; set; }
        public string AbsentTeacher { get; set; }
        public string Teacher { get; set; }
        public string Group { get; set; }
        public string Comments { get; set; }
        public string Content { get; set; }
        public bool IsTitle { get; set; }
        public ReplacementItem(string Day, string Date, string No, string AbsentTeacher, string Teacher, string Group, string Comments, string Content, bool IsTitle)
        {
            this.Day = Day;
            this.Date = Date;
            this.No = No;
            this.AbsentTeacher = AbsentTeacher;
            this.Teacher = Teacher;
            this.Group = Group;
            this.Comments = Comments;
            this.Content = Content;
            this.IsTitle = IsTitle;
        }
    }
}
