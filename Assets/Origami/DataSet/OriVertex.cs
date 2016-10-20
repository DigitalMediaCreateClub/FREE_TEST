using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace DataSet
{
    public class OriVertex
    {
        public Vector2 p_origin = new Vector2(); //展開図の時の座標
        public Vector2 p = new Vector2();//折った後の座標
        public List<OriEdge> edges = new List<OriEdge>();
        public int num { get; set; }

        public OriVertex() { }
        public OriVertex(Vector2 v,int i) {
            this.p.Set(v.x,v.y);
            this.p_origin.Set(v.x,v.y);
<<<<<<< HEAD:Assets/Origami/DataSet/OriVertex.cs
            num = i;
        }
        public OriVertex(Vector2 v1, Vector2 v2,int i)
        {
            this.p_origin.Set(v1.x, v1.y);
            this.p.Set(v2.x, v2.y);

            num = i;
=======
        }
        public OriVertex(Vector2 v1, Vector2 v2)
        {
            this.p.Set(v2.x, v2.y);
            this.p_origin.Set(v1.x, v1.y);
>>>>>>> origin/master:Assets/Kamiori/Scripts/DataSet/OriVertex.cs
        }

        public void addEdge(OriEdge edge)
        {
            double angle = getAngle(edge);
            int egNum = edges.Count;
            bool added = false;
            for (int i = 0; i < egNum; i++)
            {
                double tmpAngle = getAngle(edges[i]);
                if (tmpAngle > angle)
                {
                    edges.Insert(i, edge);
                    added = true;
                    break;
                }
            }

            if (!added)
            {
                edges.Add(edge);
            }

        }

        public double getAngle(OriEdge edge)
        {
            Vector2 dir = new Vector2();
            if (edge.start == this)
            {
                dir.Set(edge.end.p.x - this.p.x, edge.end.p.y - this.p.y);
            }
            else
            {
                dir.Set(edge.start.p.x - this.p.x, edge.start.p.x - this.p.y);
            }

            return Vector2.Angle(Vector2.zero,dir);

        }

        public OriEdge getPrevEdge(OriEdge e)
        {
            int index = edges.LastIndexOf(e);
            int eNum = edges.Count;
            return edges[(index - 1 + eNum) % eNum];
        }
        public OriEdge getNextEdge(OriEdge e)
        {
            int index = edges.LastIndexOf(e);
            int eNum = edges.Count;
            return edges[(index + 1 + eNum) % eNum];
        }
        public override int GetHashCode()
        {
            return p_origin.GetHashCode()^p.GetHashCode();  
        }
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            OriVertex v = obj as OriVertex;
            return (p_origin == v.p_origin)&&(p==v.p);
        }

    }
}
