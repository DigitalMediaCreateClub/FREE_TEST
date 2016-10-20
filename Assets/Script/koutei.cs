using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class koutei : MonoBehaviour {

    DataSet.Orikata orikata;
    Text child;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        orikata=GameObject.Find("Origami").GetComponent<OrigamiManager>().kata;
        child = GetComponentInChildren<Text>();
        child.text = orikata.seek + "/" + orikata.houhou.Count;
	}
}
