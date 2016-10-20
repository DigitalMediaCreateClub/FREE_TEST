using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FaceManager : MonoBehaviour
{

    public bool isCreateMeshWatcher;
    public Material nomal_material;
    public Material mesh_material;
    public PhysicMaterial physic;
    public HingeJoint[] hinges;
    public JointSpring[] springs;
    public JointMotor[] motors;
    public List<GameObject> lines;
    public List<bool> linesfix=new List<bool>();
    public Color nomal_collor = Color.red;
    public Color fix_collor = Color.yellow;
    // public LineRenderer l;

    private Mesh mesh;
    private Mesh mesh_collider;
    private GameObject MeshWatcher;
    private static float UNIT_THICK = 0.1f;
    public static int SECTION = 50;
    private bool tmp;
    public DataSet.OriFace face;


    // Use this for initialization
    void Awake()
    {
        lines = new List<GameObject>();
        gameObject.transform.parent = GameObject.Find("FaceParent").transform;
        isCreateMeshWatcher = GameObject.Find("Origami").GetComponent<OrigamiManager>().isCreateMeshWatcher;
        tmp = isCreateMeshWatcher;
        //  l=gameObject.AddComponent<LineRenderer>();
        //   l.SetPositions(new Vector3[2]);
        var r=gameObject.AddComponent<Rigidbody>();
        r.useGravity = false;
        if (isCreateMeshWatcher == true)
        {
            MeshWatcher = new GameObject("meshwatch");
            MeshWatcher.AddComponent<MeshFilter>();
            MeshWatcher.AddComponent<MeshRenderer>();
            // MeshWatcher.AddComponent<LineRenderer>();
            MeshWatcher.tag = "MeshSample";
            MeshWatcher.layer = 12;
            MeshWatcher.transform.parent = GameObject.Find("MeshWatcher").transform;
            this.isCreateMeshWatcher = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        qwe();
    }

    public void Init(DataSet.OriFace f, Material m1, Material m2)
    {
        face = f;
        var a = gameObject.transform.position;
        a.z = UNIT_THICK * f.hight;
        gameObject.transform.position = a;
        //      Debug.Log(a.z);
        //        Debug.Log(gameObject.transform.position);

        nomal_material = m1;
        mesh_material = m2;
        mesh = new Mesh();
        mesh_collider = new Mesh();
        Vector3[] v = new Vector3[f.vertexs.Count];
        Vector3[] vo = new Vector3[f.vertexs.Count];
        Vector3[] v_coll = new Vector3[f.vertexs.Count * 2];
        List<int> tri = new List<int>();
        List<int> tri_coll = new List<int>();
        Vector3[] vec = new Vector3[2];
        gameObject.name = f.number.ToString();
        for (int i = 0; i < f.vertexs.Count; i++)
        {
            v[i] = f.vertexs[i].p;
            vo[i] = f.vertexs[i].p_origin;
            v_coll[i] = f.vertexs[i].p;
            v_coll[i + f.vertexs.Count] = f.vertexs[i].p;
            v_coll[i].z = UNIT_THICK / 2;
            v_coll[i + f.vertexs.Count].z = -UNIT_THICK / 2;
            /*
              List<DataSet.OriEdge> edge = f.EdgesIncludeVertex(f.vertexs[i]);
              v_coll[i] += new Vector3((f.vertexs[i].p - f.vertexs[(i - 1 + v.Length) % v.Length].p).normalized )* (edge[0].thick + 1) * UNIT_THICK);
              v_coll[i + f.vertexs.Count] += (Vector3.Cross(new Vector3(0, 0, 1), f.vertexs[i].p - f.vertexs[(i - 1 + v.Length) % v.Length].p).normalized * (edge[0].thick + 1) * UNIT_THICK);
              v_coll[i] += (Vector3.Cross(new Vector3(0, 0, 1), f.vertexs[(i + 1) % v.Length].p - f.vertexs[i].p).normalized * (edge[1].thick + 1) * UNIT_THICK);
              v_coll[i + f.vertexs.Count] += (Vector3.Cross(new Vector3(0, 0, 1), f.vertexs[(i + 1) % v.Length].p - f.vertexs[i].p).normalized * (edge[1].thick + 1) * UNIT_THICK);

      */        }


        List<Vector2> uv = new List<Vector2>();
        for (int i = 0; i < v.Length; i++)
        {
            uv.Add(new Vector2(v[i].x, v[i].y));
        }

        mesh_collider.vertices = v_coll;
        mesh.vertices = v;

        mesh.name = "mesh" + f.number.ToString();
        mesh_collider.name = "Cmesh" + f.number.ToString();


        for (int i = 0; i < f.index.Length - 2; i++)
        {
            tri.Add(0);
            tri.Add(i + 1);
            tri.Add(i + 2);
        }
        tri_coll.AddRange(tri);
        tri_coll.Reverse();
        tri_coll.AddRange(tri);
        for (int i = 0; i < f.vertexs.Count - 1; ++i)
        {
            tri_coll[i + f.vertexs.Count] += f.vertexs.Count;
            if (i == 0)
            {
                for (int j = 1; j < f.vertexs.Count; ++j)
                {
                    tri_coll.Add(0);
                    tri_coll.Add(f.vertexs.Count);
                    tri_coll.Add(j);
                    tri_coll.Add(j);
                    tri_coll.Add(f.vertexs.Count + j);
                    tri_coll.Add(f.vertexs.Count);
                }

            }
            else
            {
                tri_coll.Add(i);

                tri_coll.Add(f.vertexs.Count + i);
                tri_coll.Add(i + 1);
                tri_coll.Add(i + 1);
                tri_coll.Add(f.vertexs.Count + i + 1);
                tri_coll.Add(f.vertexs.Count + i);
            }
        }

        //Debug.Log(tri.Count);
        // Debug.Log(mesh.vertexCount - 1);
        tri_coll[mesh.vertexCount - 1] += f.vertexs.Count;

        mesh.triangles = tri.ToArray();
        mesh_collider.triangles = tri_coll.ToArray();

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh_collider.RecalculateNormals();
        mesh_collider.RecalculateBounds();

        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = nomal_material;
       // Debug.Log(Resources.Load<Texture2D>("texture/sm"));
       // gameObject.AddComponent<MeshRenderer>().material.mainTexture = Resources.Load<Texture2D>("texture/sm");
        MeshCollider coll = gameObject.AddComponent<MeshCollider>();
        coll.sharedMesh = mesh_collider;
        coll.convex = true;
        coll.material = physic;
        //   coll.isTrigger = true;

        for (int i = 0; i < face.edges.Count; i++)
        {
            if (face.edges[i].isEnd == false)
            {
                GameObject x = new GameObject();
                x.transform.parent = gameObject.transform;
                var l = x.AddComponent<LineRenderer>();
                l.SetVertexCount(2);
                l.SetPosition(0, transform.localToWorldMatrix * mesh.vertices[i]);
                l.SetPosition(1, transform.localToWorldMatrix * mesh.vertices[(i + 1) % mesh.vertexCount]);
                l.SetWidth(1f, 1f);
                l.SetColors(nomal_collor, nomal_collor);
                x.SetActive(false);
                lines.Add(x);
                linesfix.Add(false);
            }
        }
        if (GameObject.Find("Origami").GetComponent<OrigamiManager>().isCreateMeshWatcher == true)
        {
            Mesh mesho = new Mesh();
            mesho.vertices = vo;
            mesho.triangles = tri.ToArray();
            MeshWatcher.GetComponent<MeshFilter>().mesh = mesho;
            MeshWatcher.GetComponent<MeshRenderer>().material = nomal_material;
            MeshWatcher.transform.localPosition += new Vector3((f.number + 1) * SECTION, 0, 0);
            MeshWatcher.transform.parent = GameObject.Find("MeshWatcher").transform;
        }
    }
    
    public void SpotMesh(GameObject cam)
    {
        if (GameObject.Find("Origami").GetComponent<OrigamiManager>().isCreateMeshWatcher == true)
            GameObject.Find("SubCamera").transform.position = MeshWatcher.transform.position + new Vector3(0, 2, 0);

    }
    public void drawline(int num,bool fix=false)
    {
        reset();
        lines[num].SetActive(true);
        linesfix[num] = fix;
        Debug.Log("lineEnd");
    }
    public void reset()
    {
        for(int i=0;i<lines.Count;i++)
        {
            if(linesfix[i]==false)
            {
                lines[i].SetActive(false);
            }
        }
    }
    public void qwe()
    {

        for (int i = 0; i < lines.Count; i++)
        {
            
                lines[i].GetComponent<LineRenderer>().SetPosition(0, transform.parent.transform.localToWorldMatrix *mesh.vertices[i]);
                lines[i].GetComponent<LineRenderer>().SetPosition(1, transform.parent.localToWorldMatrix * mesh.vertices[(i + 1) % mesh.vertexCount]);
                lines[i].GetComponent<LineRenderer>().SetColors(nomal_collor, nomal_collor);
            
        }
    }
}
