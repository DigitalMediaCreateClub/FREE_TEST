using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DataSet
{
    public class OriEdge
    {
        static private float SPRING_LOCKED = 100000;
        static private float DAMPER_LOCKED = 100000;
        static private float SPRING_UNLOCKED = 0;
        static private float DAMPER_UNLOCKED = 0;
        static private float SPRING_MOVED = 100;
        static private float DAMPER_MOVED = 10;
        static private float MOTOR_TERGET = 20;
        static private float MOTOR_FORCE = 100;
        public OriFace[] jointing = new OriFace[2];
        public OriVertex start { get; set; }
        public OriVertex end { get; set; }
        public Vector2[] direction = new Vector2[2];
        public Vector2[] direction_p = new Vector2[2];
        public HingeJoint[] joint = new HingeJoint[2];
        public JointSpring[] spring = new JointSpring[2];
        public JointMotor[] motor = new JointMotor[2];
        /*
        public Vector2 direction = new Vector2();
        public Vector2 direction_p = new Vector2();
        public HingeJoint joint = new HingeJoint();
        public JointSpring spring = new JointSpring();
        public JointMotor motor = new JointMotor();
        */
        public bool isEnd;
        public int thick { get; set; }
        public int type;//0:cut 1:mountain 2:valley
        public bool ways;
        public int m;
        public int v;
        private int joint_size = 0;
        public OriEdge() { initialize(); }
        public OriEdge(OriVertex x1, OriVertex x2)
        {
            initialize();
            this.start = x1;
            this.end = x2;
            direction[0] = (x1.p_origin - x2.p_origin).normalized;
            direction[1] = (x2.p_origin - x1.p_origin).normalized;
            direction_p[0] = (x1.p - x2.p).normalized;
            direction_p[1] = (x2.p - x1.p).normalized;/*
            direction = (x1.p_origin - x2.p_origin).normalized;
            direction_p = (x1.p - x2.p).normalized;*/
        }
        public OriVertex oppositeVertex(OriVertex v)
        {
            return v == start ? end : start;
        }
        public bool IsExitVertex(OriVertex v)
        {
            if (v == start || v == end)
                return true;
            return false;
        }
        private void initialize()
        {
            jointing[0] = null;
            jointing[1] = null;
            start = null;
            end = null;
            isEnd = true;
            thick = 0;
            type = 0;
        }
        public void AddJointing(OriFace f, bool flag = false)
        {
            ways = flag;
            if (joint_size < 2)
            {

                jointing[joint_size] = f;

                joint_size++;
                thick = 0;
            }
            if (joint_size == 2)
            {
                isEnd = false;
                if (jointing[0].hight < jointing[1].hight)
                {
                    var tmp = jointing[0];
                    jointing[0] = jointing[1];
                    jointing[1] = tmp;

                }

            }
        }
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType()) return false;
            OriEdge e = obj as OriEdge;
            return (((start.p == e.start.p) && (end.p == e.end.p)) || ((start.p == e.end.p) && (end.p == e.start.p)));
        }
        public override int GetHashCode()
        {
            return start.GetHashCode() ^ end.GetHashCode();
        }

        /////Hinge用調節///////
        public void conect()
        {
            for (int i = 0; i < 2; i++)
             {
                 joint[i] = jointing[0].Men.AddComponent<HingeJoint>();
                 joint[i].enableCollision = true;
                 joint[i].connectedBody = jointing[1].Men.GetComponent<Rigidbody>();
                 joint[i].useSpring = true;
                 if (ways == true)
                 {
                     joint[i].anchor = Vector3.Lerp(start.p, end.p, 0.5f);
                     joint[i].axis = direction_p[i];
                 }
                 else
                 {
                     joint[i].anchor = Vector3.Lerp(start.p_origin, end.p_origin, 0.5f);
                     joint[i].axis = direction[i];

                 }
                 spring[i] = joint[i].spring;
                 motor[i] = joint[i].motor;
                 motor[i].freeSpin = true;
                 if (jointing[0].side == Vector3.forward)
                     type = 1;
                 else if (jointing[0].side == -Vector3.forward)
                     type = 2;
                 else
                 {
                     Debug.Log("Cross is Crazy" + jointing[0].side);

                 }
             }
            /*/

            joint = jointing[0].Men.AddComponent<HingeJoint>();
            joint.enableCollision = true;
            joint.connectedBody = jointing[1].Men.GetComponent<Rigidbody>();
            joint.useSpring = true;
            if (ways == true)
            {
                joint.anchor = Vector3.Lerp(start.p, end.p, 0.5f);
                joint.axis = direction_p;
            }
            else
            {
                joint.anchor = Vector3.Lerp(start.p_origin, end.p_origin, 0.5f);
                joint.axis = direction;

            }
            spring = joint.spring;
            motor = joint.motor;
            motor.freeSpin = true;
            if (jointing[0].side == Vector3.forward)
                type = 1;
            else if (jointing[0].side == -Vector3.forward)
                type = 2;
            else
            {
                Debug.Log("Cross is Crazy" + jointing[0].side);

            }*/
            thick = Mathf.Abs(jointing[0].hight - jointing[1].hight);
            Lock();
        }
        public void Lock()
        {
            for (int i = 0; i < 2; i++)
            {
                spring[i].spring = SPRING_LOCKED;
                spring[i].damper = DAMPER_LOCKED;
                joint[i].useSpring = true;
                joint[i].spring = spring[i];
            }
            /*
            spring.spring = SPRING_LOCKED;
            spring.damper = DAMPER_LOCKED;
            joint.useSpring = true;
            joint.spring = spring;*/
        }
        public void UnLock()
        {
            for (int i = 0; i < 2; i++)
            {
                spring[i].spring = SPRING_UNLOCKED;
                spring[i].damper = DAMPER_UNLOCKED;
                
                joint[i].spring = spring[i];joint[i].useSpring = true;
            }/*
            spring.spring = SPRING_UNLOCKED;
            spring.damper = DAMPER_UNLOCKED;
            joint.useSpring = true;
            joint.spring = spring;*/
        }
        public void FoldType(int t)
        {
            UnLock();
            int use = whichuse(t);
               if (t == 0)
               {
                      spring[use].spring = 100;
                       spring[use].damper = 70;
                       spring[use].targetPosition = 179;
                       joint[use].useSpring = true;
                joint[use].spring = spring[use];

               }
               else 
               {    motor[use].force = MOTOR_FORCE;
                       motor[use].targetVelocity =20;
                       joint[use].useMotor = true;
                       joint[use].motor = motor[use];

                    
                   }
               /*/
            if (t == 0)
            {
                spring.spring = 300;
                spring.damper = 300;
                spring.targetPosition = 179;
                joint.useSpring = true;
            }
            else
            {
                if (type != t)
                {
                    spring.spring = 300;
                    spring.damper = 300;
                    spring.targetPosition = 179;
                    joint.useSpring = true;
                }
            }
            joint.spring = spring;
            //*/
        }
        public void stop()
        {
            for (int i = 0; i < 2; i++)
            {
                spring[i].spring = 0;
                joint[i].spring = spring[i];
            }
        }
        public void Updatehight()
        {
            if (joint_size == 2)
                thick = Mathf.Abs(jointing[0].hight - jointing[1].hight);
        }
        public int whichuse(int t)
        {
            var x=jointing[0].side + joint[0].anchor;
            var crs = Vector3.Cross(x, jointing[1].side);
            if (t == 2) crs *= -1;
            var ans1=Vector3.Dot(crs.normalized, joint[0].anchor.normalized);
            if (ans1 > 90)
                return 0;
            return 1;
        }
    }
}
