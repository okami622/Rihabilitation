using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class FileRead : MonoBehaviour
{
    private string guitxt = "";

    void awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        guitxt = "";
    }

    public void FLoad(string fname)
    {
        Initialize();
        // FileReadTest.txtファイルを読み込む
        FileInfo fi = new FileInfo(Application.dataPath + "/StreamingAssets/" + fname);
        try
        {
            // 一行毎読み込み
            using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8))
            {
                guitxt = sr.ReadToEnd();
            }
        }
        catch (Exception e)
        {
            // 改行コード
            guitxt += SetDefaultText();
        }
    }

    public string RString()
    {
        return guitxt;
    }

    public string FRead(int element)
    {
        int j = 0;
        int stext = -1;
        int etext = 0;
        char[] TArray = guitxt.ToCharArray(); ;
        char[] WArray;
        string s;
        for(int i = 0; i < guitxt.Length; i++)
        {
            if (TArray[i] == ',')
            {
                if(j == element)
                {
                    Debug.Log(TArray[i]);
                    etext = i;
                    break;
                }
                else if(j + 1 == element)
                {
                    stext = i;
                }
                j++;
            }
        }
        WArray = new char[etext-stext - 1];
        for(int i = stext + 1; i < etext; i++)
        {
            WArray[i - (stext + 1)] = TArray[i];
        }
        s = new string(WArray);
        return s;
    }

    string SetDefaultText()
    {
        return "C#あ\n";
    }
}
