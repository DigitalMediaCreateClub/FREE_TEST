using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DataSet
{
    public class OriEdge
    {
        public OriFace[] jointing = new OriFace[2];
        public OriVertex start { get; set; }
        public OriVertex end { get; set; }
        public Vector2 direction = new Vector2();
        private int joint_size = 0;
        public OriEdge() { initialize(); }
        public OriEdge(OriVertex x1,OriVertex x2)
        {
            initialize();
            this.start = x1;
            this.end = x2;
            direction = x1.p - x2.p;
        }
        public OriVertex oppositeVertex(OriVertex v)
        {
            return v == start ? end : start;
        }
        private void initialize()
        {
            jointing[0] = null;
            jointing[1] = null;
            start = null;
            end = null;
        }
        public void AddJointing(OriFace f)
        {
            if(joint_size!=2)
            {
                jointing[joint_size] = f;
                joint_size++;
            }
        }
        public override bool Equals(object obj)
        {
            if (obj == null||obj.GetType()!=this.GetType()) return false;
            OriEdge e = obj as OriEdge;
            return (((start.p == e.start.p) && (end.p == e.end.p)) || ((start.p == e.end.p) && (end.p == e.start.p)));
        }
        public override int GetHashCode()
        {
            return start.GetHashCode()^ end.GetHashCode();
        }
    }
}
