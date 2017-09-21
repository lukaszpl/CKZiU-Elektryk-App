namespace CkziuApp.Logic.XmlPlanStructure
{
    public class Teacher
    {
        public int id { get; set; }
        public int tid { get; set; }
        public string name { get; set; }

        public Teacher(int id, int tid, string name)
        {
            this.id = id;
            this.tid = tid;
            this.name = name;
        }
    }
    public class Room
    {
        public int id { get; set; }
        public int rid { get; set; }
        public string name { get; set; }

        public Room(int id, int rid, string name)
        {
            this.id = id;
            this.rid = rid;
            this.name = name;
        }
    }
    public class Group
    {
        public int id { get; set; }
        public int gid { get; set; }
        public string name { get; set; }

        public Group(int id, int gid, string name)
        {
            this.id = id;
            this.gid = gid;
            this.name = name;
        }
    }
}
