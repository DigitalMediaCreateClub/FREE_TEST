using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace DataSet {
    public class Origami{
        public List<OriFace> surface=new List<OriFace>();
        public List<OriVertex> vertexs = new List<OriVertex>();
     //   private List<OriEdge> edges = new List<OriEdge>();
       // private List<OriTower> face = new List<OriTower>();

        private bool usable = false;
        private int face_count = 0;
        private int vertex_count = 0;
        private GameObject oripaper;
        private Material mat;
        private Material mat2;
        public static float PAPER_SIZE = 200f;
        public static float MAX_SIZE = 20f;
      public Origami(Material m,Material m2) {
           mat = m;
            mat2 = m2;
        }
        public void RegisterVertex(float x_origin, float y_origin, float x, float y)
        {
            x_origin *= MAX_SIZE / PAPER_SIZE;
            y_origin *= MAX_SIZE / PAPER_SIZE;
            x *= MAX_SIZE / PAPER_SIZE;
            y *= MAX_SIZE / PAPER_SIZE;
            
            OriVertex v = new OriVertex(new Vector2(x_origin, y_origin), new Vector2(x, y), vertex_count);

            vertexs.Add(v);
            vertex_count++;

        }

        public void Registerface(int[] v)
        {
            if(face_count==0){ EdgesDP.init(vertex_count); }
            OriFace f = new OriFace(mat);
            f.CreateMesh(v, this.vertexs,face_count);
            face_count++;
            surface.Add(f);
        }
        public void AssociateHightAndFace(int hight,int num)
        {
            surface[num].ChengeHight(hight);

        }
        public void CompleteImport()
        {
            foreach (OriFace f in surface)
            {
                f.Men.GetComponent<FaceManager>().Init(f,mat,mat2);
            }
            EdgesDP.ConectAll();
            usable = true;

        }

        public bool Usable()
        {
            return usable;
        }
        public void FoldAll()
        {

        }
    }
}
