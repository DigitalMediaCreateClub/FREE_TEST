using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class OrigamiManeger : MonoBehaviour {

    private DataSet.Origami paper;
	// Use this for initialization
	void Start () {
        //ファイル読み込み&Mesh生成
        //
	
	}
	
	// Update is called once per frame
	void Update () {
        if(paper.Usable())
        {

        }
	
	}
}
