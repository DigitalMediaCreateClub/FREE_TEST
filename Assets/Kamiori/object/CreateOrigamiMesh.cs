using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]

public class CreateOrigamiMesh : MonoBehaviour {

    private DataSet.Origami model;
    private List<GameObject> face = new List<GameObject>();
    private int face_size;

	// Use this for initialization
	void Start () {
        //ファイル読み込み

        //Mesh設定
        for(int i=0;i<face_size;i++)
        {
            Mesh mesh = new Mesh();
        }

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
