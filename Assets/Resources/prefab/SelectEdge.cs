using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectEdge : MonoBehaviour {

    int num;
    void Awake()
    {
        
    }
	// Use this for initialization
	void Start () {
	
	}
	public void drawline()
    {
        // GameObject.Find("ButtonManager").GetComponent<EdgesButtonController>.
        return;
    }
    public void SetNum(int num)
    {
        this.num = num;
        gameObject.GetComponentInChildren<Text>().text = "辺" + num.ToString();
     //   Debug.Log(gameObject.GetComponentInChildren<Text>().text);
    }
    public void SetName(string s)
    { 
        gameObject.name = s;
    }
    // Update is called once per frame
    void Update () {

    }
    public void TouchExtend()
    {
        transform.parent.gameObject.GetComponent<EdgesButtonController>().TouchExtend(num);
        Debug.Log("extend");
    }
    public void TouchMountain()
    {
        transform.parent.gameObject.GetComponent<EdgesButtonController>().TouchMountain(num);
        Debug.Log("Mountain");
    }
    public void TouchValley()
    {
        transform.parent.gameObject.GetComponent<EdgesButtonController>().TouchValley(num);
        Debug.Log("Varrey");
    }
    public void TouchButton()
    {
        transform.parent.gameObject.GetComponent<EdgesButtonController>().TouchButton(num);
    }
}
