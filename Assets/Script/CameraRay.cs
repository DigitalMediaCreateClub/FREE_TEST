using UnityEngine;
using System.Collections;

public class CameraRay : MonoBehaviour {

    Camera cameras;
	// Use this for initialization
	void Start () {
        cameras = GetComponent<Camera>();
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(cameras.ScreenPointToRay(Input.mousePosition),out hit, Mathf.Infinity))
            {
                hit.collider.gameObject.GetComponent<FaceManager>().SpotMesh(gameObject);
                GameObject.Find("Origami").GetComponent<OrigamiManager>().selectface(hit.collider.gameObject.name);
            }
        }
	
	}
}
