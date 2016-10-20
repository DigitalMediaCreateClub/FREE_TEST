using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DataSet
{
    public class OriFace
    {
        //privateメンバ
        private Mesh mesh;
        public int number;
        private static float UNIT_THICK = 0.01f;
        private static int FACE_NUM = 0;
        public static int SECTION = 50;
        //publicメンバ
        public bool IsPlane;
        public List<OriEdge> edges = new List<OriEdge>();
        public List<OriVertex> vertexs = new List<OriVertex>();
        public GameObject Men;
        public GameObject MeshWatcher;
        public Material irogami;//適当
        public int hight { get; set; }
        public Vector3 side;
        public int[] index;
        public bool selecting;
        public FaceManager fm;
        public OriFace(Material m)
        {
            Men = new GameObject();
            fm=Men.AddComponent<FaceManager>();
            hight = 0;
            irogami=m;
            selecting = false;
        }

        public void CreateMesh(int[] num, List<OriVertex> v, int count)
        {
            index = num;
            number = count;
            FACE_NUM++;
            OriEdge x;
            for (int i = 0; i < num.Length; i++)
            {
        
                vertexs.Add(v[num[i]]);
                
                if (i != 0)
                {
                    if (!EdgesDP.check(num[i - 1], num[i]))
                    {
                        x = new OriEdge(v[num[i - 1]], v[num[i]]);
                        EdgesDP.make(num[i], num[i - 1], x);
                    }
                    else
                    {
                        x = EdgesDP.get(num[i], num[i - 1]);
                    }
                    x.AddJointing(this,true);
                    edges.Add(x);
                }
               
            }
            
            if (!EdgesDP.check(num[num.Length- 1], num[0]))
            {
                x = new OriEdge(v[num[num.Length - 1]], v[num[0]]);
                EdgesDP.make(num[0], num[num.Length - 1], x);
            }
            else
            {
                x = EdgesDP.get(num[0], num[num.Length - 1]);
            }
            x.AddJointing(this,true);
            edges.Add(x);

            IsPlane = true;

            side = Vector3.Cross(v[num[0]].p-v[num[1]].p, v[num[2]].p - v[num[1]].p).normalized;
        }

        public bool IsSameEdge(OriFace face)
        {
            return (SameEdge(this, face) == null) ? false : true;
        }
        public static OriEdge SameEdge(OriFace f1, OriFace f2)
        {
            foreach (OriEdge i in f1.edges)
                foreach (OriEdge j in f2.edges)
                {
                    if (i.Equals(j)) return j;
                }
            return null;
        }
        private void AddVertex(OriVertex v, int num)
        {
            vertexs.Add(v);
        }
        public List<OriEdge> EdgesIncludeVertex(OriVertex v)
        {
            List<OriEdge> edge = new List<OriEdge>();
            for (int i = 0; i < edges.Count; i++)
            {
                if (edges[i].IsExitVertex(v))
                {
                    edge.Add(edges[i]);
                }
            }
            return edge;
        }

        public void LunpEdge(int num)
        {
            LineRenderer line = MeshWatcher.GetComponent<LineRenderer>();
            List<Vector3> l=new List<Vector3>();
            l.AddRange(mesh.vertices);
            line.SetWidth(0.1f, 0.1f);
            line.SetVertexCount(2);
          //  line.SetPosition(0,l.indexof(edges[num].start.num]))
        }
        public void ChengeHight(int hight)
        {
            this.hight = hight;
            foreach(OriEdge e in edges)
            {
                e.Updatehight();
            }
        }
        public void ChengeMaterial(Material mat)
        {
            irogami = mat;
            Men.GetComponent<MeshRenderer>().material =mat;
            selecting = true;
        }
        public void drawline(int num)
        {
            fm.drawline(num-1);
        }
    }
}
