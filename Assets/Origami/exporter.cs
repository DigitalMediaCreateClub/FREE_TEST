
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using UnityEngine;

class Filer
{
    static void import(string filepath)
    {
        String filename = Application.dataPath+filepath;

        System.Xml.Serialization.XmlSerializer serializer =
            new System.Xml.Serialization.XmlSerializer(typeof(DataSet.Orikata));

        StreamReader sr = new StreamReader(filename, false);

        DataSet.Orikata obj = (DataSet.Orikata)serializer.Deserialize(sr);
        sr.Close();
    }
    static void export(string filepath, DataSet.Orikata O_obj)
    {
        string fileName = Application.dataPath;
        if (File.Exists(fileName))
        {
            Console.WriteLine("ファイルは既にあるよん");
        }
        else
        {
            using (FileStream hStream = File.Create(fileName))
            {
                if (hStream != null)
                {
                    hStream.Close();
                }
            }
            Console.WriteLine("無かったからファイル作ったで～");
        }



   

     /*   O_obj.Shiken = 1;
        O_obj.SHIKEN.Add(new List<int>() { 1, 2, 3 });
        O_obj.SHIKEN.Add(new List<int>() { 3, 3, 4 });//試験的な値の代入*/

        System.Xml.Serialization.XmlSerializer serializer =new System.Xml.Serialization.XmlSerializer(typeof(DataSet.Orikata));

        StreamWriter sw = new StreamWriter(fileName, false);//false→同名ファイルの場合上書き
        serializer.Serialize(sw, O_obj);
        sw.Close();
    }


}

