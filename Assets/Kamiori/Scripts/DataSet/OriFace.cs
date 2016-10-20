using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DataSet
{
    public class OriFace
    {
        //privateメンバ
        private Mesh mesh;
        private int number;
        //publicメンバ
        public HashSet<OriEdge> edges = new HashSet<OriEdge>();
        public List<OriVertex> vertexs = new List<OriVertex>();
        public GameObject Men;
        public Material irogami;//適当
        public int hight { get; set; }
        public OriFace()
        {
            Men = new GameObject();
            Men.AddComponent<MeshRenderer>();
            Men.AddComponent<Rigidbody>();
            Men.AddComponent<MeshCollider>();
            Men.AddComponent<Cloth>();
            Men.AddComponent<SkinnedMeshRenderer>();
            hight = 0;
        }
        public void CreateMesh(int[] num, List<OriVertex> v,int count)
        {

            number = count;

            //メッシュの生成//
            Vector3[] newVertices = new Vector3[num.Length];
            int[] newTriangles = new int[(num.Length - 2) * 3];
            for (int i=0;i<num.Length;i++)
            {
                AddVertex(v[i], i);
                newVertices[i] = new Vector3(v[i].p.x, v[i].p.y, 0);
            }
            CloseEdge();
            for (int i = 0; i < (num.Length - 2); i++)
            {
                newTriangles[i] = num[0];
                newTriangles[i + 1] =num[i + 1];
                newTriangles[i + 2] = num[i + 2];
            }
            mesh.vertices = newVertices;
            mesh.triangles = newTriangles;
            mesh = new Mesh();
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mesh.name = "OriFaceMesh" + number.ToString();
            
            //GameObjectに追加//
            Men.GetComponent<SkinnedMeshRenderer>().sharedMesh = mesh;
            Men.name = "OriFace" + number.ToString();
            Men.transform.parent = GameObject.Find("Origami").transform;

            //重力の廃止
            Men.GetComponent<Rigidbody>().useGravity = false;

        }
        public bool IsSameEdge(OriFace face)
        {
            return (SameEdge(this,face)==null)?false:true;
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
        private void AddVertex(OriVertex v,int num)
        {
            if (num != 0) edges.Add(new OriEdge(vertexs[edges.Count - 1], v));
            vertexs.Insert(num,v);
        }
        private void CloseEdge()
        {
            edges.Add(new OriEdge(vertexs[vertexs.Count - 1], vertexs[0]));

        }
 
    }
}
