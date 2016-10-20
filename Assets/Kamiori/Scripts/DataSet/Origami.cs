using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace DataSet {
    public class Origami{
        private List<OriFace> surface=new List<OriFace>();
        private List<OriVertex> vertexs = new List<OriVertex>();
        private HashSet<OriEdge> edges = new HashSet<OriEdge>();
        private bool usable = false;
        private int face_count=1;
        private GameObject oripaper;
        //hinge関係の定数
        private static int CLEARANCE=1;//開ける角度の幅
        private static int DAMPER = 3;//大きくすると角速度が遅くなる（バネの硬さ？）
        private static int SPRING = 10;//大きくすると角速度が早くなる（力の大きさ？）
        public Origami() { }
        public void RegisterVertex(float x_origin,float y_origin,float x,float y)
        {
            OriVertex v = new OriVertex(new Vector2(x_origin, y_origin), new Vector2(x, y));
            vertexs.Add(v);
        }
        public void Registerface(params int[] v)
        {
            OriFace f = new OriFace();
            f.CreateMesh(v, this.vertexs,face_count);
            face_count++;
            surface.Add(f);
        }
        public void AssociateHightAndFace(int hight,int num)
        {
            surface[num].hight = hight;
        }
        public void CompleteImport()
        {
            Sync();
            usable = true;

        }
        public void Sync()
        {
            foreach(var x in surface)
            {
                edges.UnionWith(x.edges);
            }
            int count = 0;
            foreach(var x in edges)
            {
                //edgesのjointingの順番整理
                int deepness=x.jointing[0].hight - x.jointing[1].hight;
                if(deepness<0)
                {
                    OriFace f = x.jointing[0];
                    x.jointing[0] = x.jointing[1];
                    x.jointing[1] = f;
                    deepness = -1 * deepness;
                }
                //HinjiJointの設定(親子関係の記述に気をつけなければいけない可能性あり、要デバッグ)
                HingeJoint hinge=x.jointing[0].Men.AddComponent<HingeJoint>();
                hinge.name = "Hingi" + count.ToString();
                hinge.connectedBody = x.jointing[1].Men.GetComponent<Rigidbody>();
                hinge.axis = x.direction;
                hinge.anchor = Vector2.Lerp(x.start.p, x.end.p, 0.5f);
                JointSpring hingeSpring = hinge.spring;
                hingeSpring.spring = SPRING;
                hingeSpring.damper = DAMPER;
                hingeSpring.targetPosition = deepness*CLEARANCE;
                hinge.spring = hingeSpring;
                hinge.useSpring = true;
                count++;
            }
           
        }
        public bool Usable()
        {
            return usable;
        }
    }
}
