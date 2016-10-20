using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace DataSet
{
    public class Way
    {
        public int face_num;
        public int edge_num;
        public int type;//0:expent 1:mountain 2:valley
        public Way(int f,int e,int t)
        {
            face_num = f;
            edge_num = e;
            type = t;
        }
    }
}
