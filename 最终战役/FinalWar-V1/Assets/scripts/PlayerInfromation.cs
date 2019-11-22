using System.Collections;
using System.Collections.Generic;

namespace artest.AIThirdPerson
{
    public class PlayerInfromation
    {
        public string id
        {
            set; get;
        }
        public string name
        {
            set; get;
        }
        public double money
        {
            set; get;
        }
        public double score
        {
            set; get;
        }
        public string time
        {
            set; get;
        }
        public int level
        {
            set; get;
        }
        public int castle
        {
            set; get;
        }
        public int defence
        {
            set; get;
        }
        public int propone
        {
            set; get;
        }
        public int proptwo
        {
            set; get;
        }
        public int propthree
        {
            set; get;

        }
        public override string ToString()
        {
            return "id: " + id + "  name:  " + name + "   money:   " + money + "   score:   " + score + "   time:   " + time
                + "level: " + level + "castle: " + castle + "defence: " + defence + "propone: " + propone + "proptwo: " + proptwo +
                "propthree: " + propthree;
        }
    }
}
