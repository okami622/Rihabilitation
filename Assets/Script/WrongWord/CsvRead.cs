using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class CsvRead : MonoBehaviour
{
    [SerializeField]
    private string filename = "WrongWordList.csv";
    //出力する場所のパスの指定と、ファイル名の指定  
    private List<string[]> csvDatas = new List<string[]>();
    private int linecount=0;

    void Awake()
    {
        string path = Application.streamingAssetsPath + "/まちがいさがし/" + filename;
        StreamReader readCsvObject = new StreamReader(path, Encoding.GetEncoding("utf-8"));

        while (!readCsvObject.EndOfStream)
        {
            string line = readCsvObject.ReadLine();
            csvDatas.Add(line.Split(','));
            linecount++;
        }
    }
    
    public int GetLnCnt()
    {
        return linecount;
    }

    public string[] GetCsvRead(int ln)
    {
        return csvDatas[ln];
    }
}