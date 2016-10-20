using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSet
{
   public class Orikata
    {
        public List<List<Way>> houhou;
        public int seek;
        public Orikata()
        {
            houhou = new List<List<Way>>();
            seek = 0;
        }
        public List<Way>Back()
        {
            if (seek <= 0||seek>=houhou.Count)
                return null;
            seek--;
            return houhou[seek];
        }
        public List<Way>Next()
        {
            if (seek < 0 || seek >= houhou.Count)
                return null;
            seek++;
            return houhou[seek-1];
        }
        public void Write(List< Way> w)
        {
            houhou.Insert(0, w);
        }
    }
}
