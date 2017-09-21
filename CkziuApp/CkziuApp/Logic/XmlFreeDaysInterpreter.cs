using System.Collections.Generic;
using System.Xml.Linq;

namespace CkziuApp.Logic
{
    public class XmlFreeDaysInterpreter
    {
        public XDocument xmlDoc;
        public XmlFreeDaysInterpreter(string Path)
        {
            xmlDoc = XDocument.Load(Path);
        }
        public List<FreeDayItem> GetFreeDays()
        {
            List<FreeDayItem> list = new List<FreeDayItem>();
            foreach (var day in xmlDoc.Descendants("day"))
                list.Add(new FreeDayItem(day.Attribute("date").Value, day.Value));
            return list;
        }
        public string GetTitle()
        {
            foreach (var freedays in xmlDoc.Descendants("freedays"))
                return freedays.Attribute("title").Value;
            return null;
        }
    }
    public class FreeDayItem
    {
        public string Date { get; set; }
        public string Content { get; set; }
        public FreeDayItem( string Date, string Content)
        {
            this.Date = Date;
            this.Content = Content;
        }
    }
}
