using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

public class OrigamiManager : MonoBehaviour
{

    public DataSet.Origami ori;
    public DataSet.Orikata kata;
    [SerializeField]
    public Material irogami;
    [SerializeField]
    public Material irogamiSelected;
    [SerializeField]
    public string filename;
    public bool isCreateMeshWatcher;

    private int nowselecting;
    private int befselecting;



    // Use this for initialization
    void Start()
    {
        ori = new DataSet.Origami(irogami, irogamiSelected);
        kata = new DataSet.Orikata() ;
        load();
        GameObject.Find("ButtonManager").GetComponent<EdgesButtonController>().init();
        befselecting = -1;
    }

    void load()
    {
        int counter = 0;
        string line;
        string[] substring = { };

        List<List<int>> List_Asso = new List<List<int>>();
        int[] P_Asso;
        string path = Application.dataPath + "/" + filename;

        StreamReader file = new StreamReader(path);
        while ((line = file.ReadLine()) != null)
        {
            List<float> vertex = new List<float>();
            List<int> face = new List<int>();

            if (line != "")
            {
                switch (line[0])
                {
                    case 'v':
                        substring = line.Split(' ');

                        for (int i = 0; i < substring.Length; i++)
                        {
                            if (substring[i] != "v" && substring[i] != " ")
                            {
                                vertex.Add(Convert.ToSingle(substring[i]));
                            }
                        }
                        ori.RegisterVertex(vertex[0], vertex[1], vertex[2], vertex[3]);
                        break;
                    case 'f':
                        substring = line.Split(' ');
                        for (int i = 0; i < substring.Length; i++)
                        {
                            if (substring[i] != "f" && substring[i] != " ")
                            {
                                face.Add(Convert.ToInt32(substring[i]) - 1);
                            }
                        }
                        ori.Registerface(face.ToArray());
                        break;
                    case '#':
                        Console.WriteLine("無視");
                        break;
                    case '\n':
                        Console.WriteLine("無視");
                        break;
                    default:
                        substring = line.Split(' ');
                        List_Asso.Add(new List<int>());
                        for (int i = 0; i < substring.Length - 1; i++)
                        {
                            List_Asso[counter].Add(Convert.ToInt32(substring[i]));

                        }
                        counter += 1;
                        break;

                }
            }

        }


        file.Close();
        P_Asso = new int[substring.Length - 1];
        var ans = new List<List<int>>();

        bool[] b = new bool[List_Asso.Count];
        //2の数数えて優先度
        for (int k = 0; k < List_Asso.Count; k++)
        {
            var a = new List<int>();
            for (int i = 0; i < List_Asso.Count; i++)
            {

                bool flg = true;

                for (int j = 0; j < List_Asso.Count; j++)
                {
                    if (List_Asso[i][j] == 2)
                    {
                        flg = false;

                    }
                }
                if (flg == true && !b[i])
                {
                    ori.AssociateHightAndFace(k, i);
                    b[i] = true;
                    a.Add(i);
                }
             
            }

            for (int g = 0; g < a.Count; g++)
            {
                for (int h = 0; h < List_Asso.Count; h++)
                {
                   
                    List_Asso[a[g]][h] = 0;
                    List_Asso[h][a[g]] = 0;
                }
            }

        }
        ori.CompleteImport();

    }

    // Update is called once per frame
    void Update()
    {


    }
    public void selectface(string name)
    {
        disselect();
        nowselecting = int.Parse(name);

        if (nowselecting == befselecting)
        {
            befselecting = -1;
            return;
        }
        befselecting = nowselecting;
        foreach(DataSet.OriFace x in ori.surface)x.fm.reset();
        ori.surface[nowselecting].ChengeMaterial(irogamiSelected);
        GameObject.Find("SubCamera").GetComponent<WatchMesh>().MoveSubCamera(nowselecting);
        GameObject.Find("ButtonManager").GetComponent<EdgesButtonController>().Activete(nowselecting);
        GameObject.Find("ButtonManager").GetComponent<EdgesButtonController>().Activete(nowselecting);



    }
    public void disselect()
    {
        GameObject.Find("ButtonManager").GetComponent<EdgesButtonController>().Inactive();
        if (nowselecting != -1) ori.surface[nowselecting].ChengeMaterial(irogami);

    }
    public void settaiki(List<DataSet.Way> l)
    {
        kata.Write(l);
    }
    /// <summary>
    /// button関係
    /// </summary>

}
