using UnityEngine;
using System.Collections;

public class WatchMesh : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MoveSubCamera(int num)
    {
        transform.position = new Vector3(DataSet.OriFace.SECTION * (num + 1), 0f, 35f);

    }
}
