using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class EdgesButtonController : MonoBehaviour {

    public int now_face;
    public int edgenumall;
    public int page;
    public DataSet.Origami main;
    public DataSet.OriFace selectface;
    public List<DataSet.Way> taiki;
    public GameObject prefab;
    public List<List<GameObject>> buttons;
    public bool active;
    public int tmp = 90;
    public bool inited;
	// Use this for initialization
    void Awake()
    {
        taiki = new List<DataSet.Way>();
        prefab = (GameObject)Resources.Load("prefab/button");
        page = 0;
      //  now_face = -1;
    }
	void Start () {
    
    }
	public void init()
    {
        main = GameObject.Find("Origami").GetComponent<OrigamiManager>().ori;
        buttons = new List<List<GameObject>>();
       
        for (int i = 0; i < main.surface.Count; i++)
        {
            int k = 0;
            buttons.Add(new List<GameObject>());
            //Debug.Log("edges" + i+ ":"+main.surface[i].edges.Count);
            for (int j = 0; j < main.surface[i].edges.Count; j++)
            {
                if (main.surface[i].edges[j].isEnd == false)
                {
                    buttons[i].Add(Instantiate(prefab)as GameObject);
                    buttons[i][buttons[i].Count - 1].transform.SetParent(gameObject.transform, false);

                    buttons[i][buttons[i].Count - 1].transform.position += new Vector3(tmp *(k%3), 0, 0);
                  
                    buttons[i][buttons[i].Count - 1].GetComponent<SelectEdge>().SetNum(k+1);
                    buttons[i][buttons[i].Count - 1].GetComponent<SelectEdge>().SetName(i+":"+j+":"+k);
                    buttons[i][buttons[i].Count - 1].SetActive(false);
                    k++;
                }
            }
        }   
        active = false;
    }
    public void Activete(int face)
    {
        Debug.Log("face:" + face);
        now_face = face;
        
        selectface = main.surface[face];
        Debug.Log("face:" + main.surface[face]);
        Debug.Log("face:" + selectface);
        active = true;
        int temp=0;
        for(int i = page; i < buttons[now_face].Count && page + 3 > i; i++)
        {
            buttons[now_face][i].SetActive(true);
            temp++;
            //buttons[now_face][i].GetComponent<SelectEdge>().SetNum(i+1);
        }
        page+=temp;
    }
    public void Inactive()
    {
        active = false;
       // selectface.Men.GetComponent<FaceManager>().reset();
        // Debug.Log("Count:"+buttons[now_face].Count);
        // Debug.Log((page - 1 < buttons[now_face].Count)==true);
        for (int i = page - 1; i > page - 4 && i >= 0; i--)
        {
            //Debug.Log(buttons[now_face][i].name);
            buttons[now_face][i].SetActive(false);
        }
        now_face = -1;
        page = 0;
    }
	// Update is called once per frame
	void Update () {
	
	}

    public void TouchRight()
    {
        if (page == buttons[now_face].Count)
            return;
        Debug.Log(buttons.Count);
        Debug.Log(page);
        for (int i = 1; i <= 3; i++)
        {
            Debug.Log(buttons[now_face][page - i]);
            buttons[now_face][page - i].SetActive(false);
        }
        int temp=0;
        for(int i=0;i<buttons.Count&&i<3;i++)
        {
            buttons[now_face][i].SetActive(true);
            tmp++;
        }
        page += temp;
        Debug.Log("Right");
        selectface = null;
    } 
    public void TouchLeft()
    {
        if (page - 3 <= 0)
            return;
        int temp = 0;
        for (int i = page - 1; i % 3 == 0; i--) {
            buttons[now_face][i].SetActive(false);
            temp++;
        }
        page -= temp;
        for(int i=0;i<3;i++)
        {
            buttons[now_face][page - i].SetActive(true);
        }
        Debug.Log("Left");
    }
    public void TouchFold()
    {
        for(int i=0;i<taiki.Count;i++)
        {
            main.surface[taiki[i].face_num].edges[taiki[i].edge_num-1].FoldType(taiki[i].type);
        }
        GameObject.Find("Origami").GetComponent<OrigamiManager>().settaiki(taiki);
        taiki = new List<DataSet.Way>();
        Debug.Log("Fold");
    }
    public void TouchButton(int num)
    {
        Debug.Log("Touch");
        Debug.Log(selectface);
        selectface.drawline(num);
    }
    public void TouchExtend(int num)
    {
        taiki.Add(new DataSet.Way(now_face, num, 0));
        Debug.Log("extend");
    }
    public void TouchMountain(int num)
    {
        taiki.Add(new DataSet.Way(now_face, num,  1));
        Debug.Log("Mountain");
    }
    public void TouchValley(int num)
    {
        taiki.Add(new DataSet.Way(now_face, num, 2));
        Debug.Log("Varrey");
    }
}
