using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSet
{
    static class EdgesDP
    {
        static private bool[,] made;
        static private int lenge;
        static private OriEdge[,] edges;
        static public void init(int size)
        {
            lenge = size;
            made = new bool[size, size];
            edges = new OriEdge[size, size];
        }
        static public void make(int to, int from,OriEdge x)
        {
            made[from, to] = true;
            made[to, from] = true;
            edges[from, to] = x;
            edges[to, from] = x;
        }
        static public bool check(int to, int from)
        {
            return made[to, from];
        }
        static public OriEdge get(int to, int from)
        {
            return edges[to, from];
        }
        static public void ConectAll()
        {
            for(int i=0;i<lenge;i++)
            {
                for(int j=lenge-1;j>i;j--)
                {
                    if(edges[i,j]!=null&&edges[i,j].isEnd==false)
                        {
                            edges[i, j].conect();

                    }
                }
            }
        }
    }
}
